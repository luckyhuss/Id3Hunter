using Google.API.Search;
using ParkSquare.Gracenote;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace ID3_Hunter
{

    public partial class frmMain : Form
    {
        private string[] strFilePathsALL;

        private string GRACENOTE_CLIENT_ID = "476330041-5515BC7CBA66D19D92632CE7CFFD35C0"; //Properties.Settings.Default.GraceNoteID;

        private string LAST_FM_API_KEY = "";

        private List<TagLib.File> listTagLibFiles;
        private List<string> listFileNames;
        private List<string> listFilePaths;

        private int intCurrentSongPosition;

        private AutoCompleteStringCollection autoCompleteAlbums;
        private AutoCompleteStringCollection autoCompleteArtists;
        private AutoCompleteStringCollection autoCompleteGenres;

        private GracenoteClient gcClient;

        private string invalidFilenameChars;

        private bool bolCancelProgress;

        public frmMain()
        {
            listTagLibFiles = new List<TagLib.File>();
            listFileNames = new List<string>();
            listFilePaths = new List<string>();

            autoCompleteAlbums = new AutoCompleteStringCollection();
            autoCompleteArtists = new AutoCompleteStringCollection();
            autoCompleteGenres = new AutoCompleteStringCollection();

            bolCancelProgress = false;

            invalidFilenameChars = new string(System.IO.Path.GetInvalidFileNameChars());

            gcClient = new GracenoteClient(GRACENOTE_CLIENT_ID);

            InitializeComponent();

            this.Text = "ID3 Hunter v" + System.Reflection.Assembly.GetEntryAssembly().GetName().Version;
            picAlbumArtwork.AllowDrop = true; //have to do this in code since the property is not listed
            new ToolTip().SetToolTip(picAlbumArtwork, "Left-Click to select from computer. Right click to search Google. Or find on your computer, and drag it into the PictureBox.");
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //confirm user wishes to exit the application
            if (MessageBox.Show("Are you sure you wish to exit the ID3 Hunter?",
                "ID3 Hunter - Quit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == System.Windows.Forms.DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void getFiles(params string[] fileTypes)
        {
            //create a list to hold all file names intermediately
            List<string> listAllFiles = new List<string>();

            foreach (string fileType in fileTypes)
            {
                listAllFiles.AddRange(Directory.GetFiles(txtSelectedPath.Text, "*" + fileType, SearchOption.AllDirectories));
            }

            strFilePathsALL = listAllFiles.ToArray(); //ultimately send it to strFilePathsALL for use
            listAllFiles.Clear();
        }

        public void SelectSongs()
        {
            try
            {
                tsNavigator.Enabled = false;
                getFiles("aac", "aiff", "flac", "m4a", "mp3", "mp4", "ogg", "wav", "wma");

                listTagLibFiles.Clear();
                listFileNames.Clear();
                listFilePaths.Clear();

                pbProgress.Value = 0;
                pbProgress.Maximum = strFilePathsALL.Length;
                pbProgress.Visible = true;

                TagLib.File tagLibFile;

                for (int counter = 0; counter < strFilePathsALL.Length; counter++)
                {
                    lblStatusChange.Text = "Creating TagLib file for " + Path.GetFileName(strFilePathsALL[counter]);
                    tagLibFile = TagLib.File.Create(strFilePathsALL[counter]);
                    listTagLibFiles.Add(tagLibFile);
                    listFileNames.Add(Path.GetFileName(strFilePathsALL[counter]));
                    listFilePaths.Add(strFilePathsALL[counter]);

                    //if there are performers in the file already
                    if (tagLibFile.Tag.Performers != null)
                    {
                        foreach (string artist in tagLibFile.Tag.Performers)
                        {
                            //iterate through them and add the artist to our AutoCompleteCollection for manual entry
                            if (!autoCompleteArtists.Contains(artist.Trim()))
                            {
                                autoCompleteArtists.Add(artist.Trim());
                            }
                        }
                    }
                    if (tagLibFile.Tag.Genres != null)
                    {
                        foreach (string genre in tagLibFile.Tag.Genres)
                        {
                            if (!autoCompleteGenres.Contains(genre.Trim()))
                            {
                                autoCompleteGenres.Add(genre.Trim());
                            }
                        }
                    }
                    if (tagLibFile.Tag.Album != null)
                    {
                        if (!autoCompleteAlbums.Contains(tagLibFile.Tag.Album.Trim()))
                        {
                            autoCompleteAlbums.Add(tagLibFile.Tag.Album.Trim());
                        }
                    }

                    tagLibFile = null;
                    pbProgress.PerformStep();
                    Application.DoEvents();
                }
                
                tslblItemCount.Text = listTagLibFiles.Count.ToString();
                if (listTagLibFiles.Count > 0)
                {
                    //move to beginning of the list and populate the textboxes with first song then enable editing
                    tsbtnMoveFirst_Click(tsbtnMoveFirst, EventArgs.Empty);
                    PopulateTextboxes(listTagLibFiles[intCurrentSongPosition]);
                    btnEdit.Enabled = true;
                    btnAutoDetectForSong.Enabled = true;
                }
                else
                {
                    tslblCurrentPosition.Text = "0";
                    tslblItemCount.Text = "0";
                    btnEdit.Enabled = false;
                    btnAutoDetectForSong.Enabled = false;
                    ClearFields();
                }
                //after populating our auto complete lists, set our TextBoxes to use them
                txtManAlbum.AutoCompleteCustomSource = autoCompleteAlbums;
                txtManArtist.AutoCompleteCustomSource = autoCompleteArtists;
                txtManGenre.AutoCompleteCustomSource = autoCompleteGenres;

                Utilities.CheckToolstripEnable(ref tsNavigator);
            }
            catch (Exception ex)
            {
                Utilities.GenericCatchBlock(ex);
            }
            finally
            {
                tsNavigator.Enabled = true;
                pbProgress.Visible = false;
            }
        }

        private void ClearFields()
        {
            lblStatusChange.Text = "Waiting";

            txtManAlbum.Clear();
            txtManAlbumArtist.Clear();
            txtManArtist.Clear();
            txtManDiscNum.Clear();
            txtManDiscTotal.Clear();
            txtManGenre.Clear();
            txtManTitle.Clear();
            txtManTrackNum.Clear();
            txtManTrackTotal.Clear();
            txtManYear.Clear();

            picAlbumArtwork.Image = null;
        }

        private bool CanConnectToLastFM()
        {
            if (new Ping().Send("www.last.fm", 1000).Status == IPStatus.Success) //ping last.fm to see if we can reach them first
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SetFieldsEditMode(bool bolEdit)
        {
            if (bolEdit == true)
            {
                txtManAlbum.ReadOnly = false;
                txtManAlbumArtist.ReadOnly = false;
                txtManArtist.ReadOnly = false;
                txtManDiscNum.ReadOnly = false;
                txtManDiscTotal.ReadOnly = false;
                txtManGenre.ReadOnly = false;
                txtManTitle.ReadOnly = false;
                txtManTrackNum.ReadOnly = false;
                txtManTrackTotal.ReadOnly = false;
                txtManYear.ReadOnly = false;

                btnSave.Enabled = true;
                btnEdit.Enabled = false;
                btnCancel.Enabled = true;
                if (picAlbumArtwork.Image != null)
                {
                    btnClearArtwork.Enabled = true;
                }
                tsNavigator.Enabled = false;
            }
            else
            {
                txtManAlbum.ReadOnly = true;
                txtManAlbumArtist.ReadOnly = true;
                txtManArtist.ReadOnly = true;
                txtManDiscNum.ReadOnly = true;
                txtManDiscTotal.ReadOnly = true;
                txtManGenre.ReadOnly = true;
                txtManTitle.ReadOnly = true;
                txtManTrackNum.ReadOnly = true;
                txtManTrackTotal.ReadOnly = true;
                txtManYear.ReadOnly = true;

                btnSave.Enabled = false;
                btnEdit.Enabled = true;
                btnCancel.Enabled = false;
                btnClearArtwork.Enabled = false;

                tsNavigator.Enabled = true;
            }
        }

        private void PopulateTextboxes(TagLib.File tagLibFile)
        {
            try
            {
                ClearFields();

                lblStatusChange.Text = "Viewing " + listFileNames[intCurrentSongPosition];

                txtManAlbum.Text = tagLibFile.Tag.Album;

                for (int counter = 0; counter < tagLibFile.Tag.AlbumArtists.Length; counter++)
                {
                    txtManAlbumArtist.Text = txtManAlbumArtist.Text + tagLibFile.Tag.AlbumArtists[counter] + ';';
                }
                if (txtManAlbumArtist.Text.EndsWith(";"))
                {
                    txtManAlbumArtist.Text = txtManAlbumArtist.Text.Substring(0, txtManAlbumArtist.Text.Length - 1);
                }

                for (int counter = 0; counter < tagLibFile.Tag.Performers.Length; counter++)
                {
                    txtManArtist.Text = txtManArtist.Text + tagLibFile.Tag.Performers[counter] + ';';
                }
                if (txtManArtist.Text.EndsWith(";"))
                {
                    txtManArtist.Text = txtManArtist.Text.Substring(0, txtManArtist.Text.Length - 1);
                }

                txtManDiscNum.Text = tagLibFile.Tag.Disc.ToString();
                txtManDiscTotal.Text = tagLibFile.Tag.DiscCount.ToString();

                for (int counter = 0; counter < tagLibFile.Tag.Genres.Length; counter++)
                {
                    txtManGenre.Text = txtManGenre.Text + tagLibFile.Tag.Genres[counter] + ';';
                }
                if (txtManGenre.Text.EndsWith(";"))
                {
                    txtManGenre.Text = txtManGenre.Text.Substring(0, txtManGenre.Text.Length - 1);
                }

                txtManTitle.Text = tagLibFile.Tag.Title;
                txtManTrackNum.Text = tagLibFile.Tag.Track.ToString();
                txtManTrackTotal.Text = tagLibFile.Tag.TrackCount.ToString();
                txtManYear.Text = tagLibFile.Tag.Year.ToString();

                if (tagLibFile.Tag.Pictures.Length >= 1)
                {
                    //tricky, but convert the Picture data in the song's metadata to a byte array, and use that to construct the image
                    var bin = (byte[])(tagLibFile.Tag.Pictures[0].Data.Data);
                    picAlbumArtwork.Image = Image.FromStream(new MemoryStream(bin)).GetThumbnailImage(picAlbumArtwork.Size.Width, picAlbumArtwork.Size.Height, null, IntPtr.Zero);
                }
                else
                {
                    picAlbumArtwork.Image = null;
                }

                btnSaveArtwork.Enabled = (picAlbumArtwork.Image == null) ? false : true; //if picAlbumArtwork.Image is null, btnSaveArtwork.Enabled is false, else it's true
            }
            catch (Exception ex)
            {
                Utilities.GenericCatchBlock(ex);
            }
        }

        private void SaveID3Tags(TagLib.File tagLibFile)
        {
            try
            {
                foreach (Control ctl in this.Controls)
                {
                    if (ctl.GetType() == typeof(TextBox))
                    {
                        ctl.Text = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(ctl.Text); //set each Textbox to use TitleCase
                    }
                }
                //stores the raw results from splitting the string at the semi-colon ";"
                string[] strArrayRawPerformers = txtManArtist.Text.Trim().Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                string[] strArrayRawGenres = txtManGenre.Text.Trim().Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                string[] strArrayRawAlbumArtists = txtManAlbumArtist.Text.Trim().Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                //stores the trimmed results
                string[] strArrayTrimmedPerformers = { "" };
                string[] strArrayTrimmedGenres = { "" };
                string[] strArrayTrimmedAlbumArtists = { "" };

                //Used for simplicity so don't need to worry about a dynamic string array
                List<string> listStrTemp = new List<string>();

                //trim any whitespace at the end/beginning of an artist, genre, and album artists.
                //User is likely to enter data in the form of "Avenged Sevenfold; Atreyu" so need to chop out blank space before Atreyu.
                foreach (string artist in strArrayRawPerformers)
                {
                    string strArtistToUse = artist.Trim();
                    //add each artist to our AutoCompleteStringCollection for later songs to use.
                    if (!autoCompleteArtists.Contains(strArtistToUse))
                    {
                        autoCompleteArtists.Add(strArtistToUse);
                    }
                    listStrTemp.Add(strArtistToUse);
                }
                //set our trimmedPerformers array to the items in listTemp
                strArrayTrimmedPerformers = listStrTemp.ToArray();
                //then reset it for the next set
                listStrTemp.Clear();

                //repeat from above
                foreach (string genre in strArrayRawGenres)
                {
                    string strGenreToUse = genre.Trim();
                    if (!autoCompleteGenres.Contains(strGenreToUse))
                    {
                        autoCompleteGenres.Add(strGenreToUse);
                    }
                    listStrTemp.Add(strGenreToUse);
                }
                strArrayTrimmedGenres = listStrTemp.ToArray();
                listStrTemp.Clear();

                foreach (string albumArtist in strArrayRawAlbumArtists)
                {
                    string strAlbumArtistToUse = albumArtist.Trim();
                    listStrTemp.Add(strAlbumArtistToUse);
                }
                strArrayTrimmedAlbumArtists = listStrTemp.ToArray();
                listStrTemp.Clear();

                if (!autoCompleteAlbums.Contains(txtManAlbum.Text.Trim()))
                {
                    autoCompleteAlbums.Add(txtManAlbum.Text.Trim());
                }

                tagLibFile.Tag.Performers = strArrayTrimmedPerformers;
                tagLibFile.Tag.Title = txtManTitle.Text.Trim();
                tagLibFile.Tag.Genres = strArrayTrimmedGenres;
                tagLibFile.Tag.Album = txtManAlbum.Text.Trim();
                tagLibFile.Tag.AlbumArtists = strArrayTrimmedAlbumArtists;
                tagLibFile.Tag.Disc = (txtManDiscNum.Text != "") ? Convert.ToUInt16(txtManDiscNum.Text.Trim()) : Convert.ToUInt16(1);
                tagLibFile.Tag.DiscCount = (txtManDiscTotal.Text != "") ? Convert.ToUInt16(txtManDiscTotal.Text.Trim()) : Convert.ToUInt16(1);
                tagLibFile.Tag.Track = (txtManTrackNum.Text != "") ? Convert.ToUInt16(txtManTrackNum.Text.Trim()) : Convert.ToUInt16(0);
                tagLibFile.Tag.TrackCount = (txtManTrackTotal.Text != "") ? Convert.ToUInt16(txtManTrackTotal.Text.Trim()) : Convert.ToUInt16(0);
                tagLibFile.Tag.Year = (txtManYear.Text != "") ? Convert.ToUInt16(txtManYear.Text.Trim()) : Convert.ToUInt16(0);

                if (picAlbumArtwork.Image != null)
                {
                    ImageConverter imgConverter = new ImageConverter();
                    byte[] bytes = (byte[])imgConverter.ConvertTo(picAlbumArtwork.Image, typeof(byte[]));

                    tagLibFile.Tag.Pictures = new TagLib.Picture[1] { new TagLib.Picture(new TagLib.ByteVector(bytes)) };
                }
                else
                {
                    tagLibFile.Tag.Pictures = null;
                }
                tagLibFile.Save();

                //set the updated AutoCompleteCollections to be used to reflect changes made in this save.
                txtManAlbum.AutoCompleteCustomSource = autoCompleteAlbums;
                txtManArtist.AutoCompleteCustomSource = autoCompleteArtists;
                txtManGenre.AutoCompleteCustomSource = autoCompleteGenres;
            }
            catch (Exception ex)
            {
                Utilities.GenericCatchBlock(ex);
            }
        }

        private void AutoDetectForSong(TagLib.File tagLibFile, string strFilePath, bool bolDetectingAll = false)
        {
            this.Cursor = Cursors.WaitCursor;

            Process procLastFmFpClient = new Process();
            try
            {
                bool bolDidGracenote = false;
                string strMBIDTrack = "";
                string strMBIDArtist = "";
                string strMBIDAlbum = "";
                string strUrlForArtwork = "";
                string strArtist = "";
                string strAlbum = "";
                string strGenre = "";
                string strTitle = "";
                string strTrackNum = "";
                string strTrackCount = "";
                string strURL = "";
                string strYear = "";

                string strOutputMessage = "";
                string strFingerprintID = "";
                string strErrorMessage = "";

                XmlDocument xmlDoc = new XmlDocument(); //use xmlDoc to get MBID, artist, song title, album title and album artwork

                if (tagLibFile.Tag.MusicBrainzTrackId != null) //if we have an MBID, we don't need to waste time fingerprinting it
                {
                    strMBIDTrack = tagLibFile.Tag.MusicBrainzTrackId;
                }
                else //no MBID, so fingerprint it for querying last.fm to get MBID
                {
                    if (Path.GetExtension(strFilePath).ToLower() == ".mp3") //Last.FM fingerprint client only supports MP3 files
                    {
                        procLastFmFpClient.StartInfo.UseShellExecute = false;
                        procLastFmFpClient.StartInfo.CreateNoWindow = true;
                        procLastFmFpClient.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        procLastFmFpClient.StartInfo.RedirectStandardOutput = true;
                        procLastFmFpClient.StartInfo.RedirectStandardError = true;
                        procLastFmFpClient.StartInfo.FileName = Application.StartupPath + "\\LastFM Stuff\\lastfmfpclient.exe";
                        procLastFmFpClient.StartInfo.Arguments = "-url \"" + strFilePath + "\"";

                        procLastFmFpClient.Start();
                        strOutputMessage = procLastFmFpClient.StandardOutput.ReadToEnd();
                        strFingerprintID = Path.GetFileName(strOutputMessage).Split('.')[0];
                        strErrorMessage = procLastFmFpClient.StandardError.ReadToEnd();
                        procLastFmFpClient.WaitForExit();

                        if (strErrorMessage != "")
                        {
                            throw new Exception(strErrorMessage);
                        }

                        strURL = "http://ws.audioscrobbler.com/2.0/?method=track.getfingerprintmetadata&fingerprintid=" + strFingerprintID + "&api_key=" + LAST_FM_API_KEY;

                        xmlDoc.Load(new XmlTextReader(strURL));

                        //if there is an mbid tag in the document, we'll just take that since it's a unique identifier
                        if (xmlDoc.SelectNodes("//track/mbid").Count > 0) strMBIDTrack = xmlDoc.SelectNodes("//track/mbid")[0].InnerText; //the InnerText of the first mbid tag is the mbid value of the most accurate match
                    }
                }

                //without an MBID, we can only get album artwork, artist and song title from Last.FM. Why can we not get album?? We can get the artwork...
                if (strMBIDTrack == "")
                {
                    if (xmlDoc.SelectNodes("//artist/name").Count > 0) strArtist = xmlDoc.SelectNodes("//artist/name")[0].InnerText;
                    if (xmlDoc.SelectNodes("//artist/mbid").Count > 0) strMBIDArtist = xmlDoc.SelectNodes("//artist/mbid")[0].InnerText;
                    if (xmlDoc.SelectNodes("//name").Count > 0) strTitle = xmlDoc.SelectNodes("//name")[0].InnerText;
                    if (xmlDoc.SelectNodes("//image").Count > 0) strUrlForArtwork = xmlDoc.SelectNodes("//image")[xmlDoc.SelectNodes("//image").Count - 1].InnerText;

                    //we have no MBID, so let's see if Gracenote can rise to the occassion
                    if (strArtist == "")
                    {
                        strArtist = (txtManArtist.Text.Trim() != "") ? txtManArtist.Text.Trim() : txtManAlbumArtist.Text.Trim();
                    }
                    if (strAlbum == "") strAlbum = txtManAlbum.Text.Trim();
                    if (strTitle == "") strTitle = txtManTitle.Text.Trim();

                    SearchCriteria criteria = new SearchCriteria();
                    criteria.AlbumTitle = strAlbum;
                    criteria.Artist = strArtist;
                    criteria.SearchMode = ParkSquare.Gracenote.Dto.SearchMode.BestMatchWithCoverArt;
                    criteria.SearchOptions = ParkSquare.Gracenote.Dto.SearchOptions.Cover;
                    criteria.TrackTitle = strTitle;

                    if (criteria.AlbumTitle != "" || criteria.Artist != "" || criteria.TrackTitle != "") //if all 3 are empty, an Exception will rise, so let's make sure we have at least one set
                    {
                        SearchResult gcSearch = gcClient.Search(criteria);

                        Album gnAlbum = gcSearch.Albums.FirstOrDefault(); //FirstOrDefault will set it to the first result, or null if none were returned

                        if (gnAlbum != null)
                        {
                            if (strTrackNum == "") strTrackNum = gnAlbum.MatchedTrackNumber.ToString();

                            Artwork gnArtwork = gnAlbum.Artwork.FirstOrDefault();
                            Track gnTracks = gnAlbum.Tracks.FirstOrDefault();

                            if (gnAlbum.Tracks.Count<Track>() > 0) //if the album returned some tracks
                            {
                                if (criteria.TrackTitle == null)
                                {
                                    //check if TrackTitle is null. If we specified the TrackTitle at the start, then this will only return 1 track, so we can't just say the Tracks.Count is the track count of the CD
                                    if (strTrackCount == "") strTrackCount = gnAlbum.Tracks.Count<Track>().ToString();
                                }

                                if (strTrackNum == "0")
                                {
                                    IEnumerator<Track> gnTrackIterator = gnAlbum.Tracks.GetEnumerator(); //don't use FirstOrDefault() here because we want just an iterator
                                    gnTrackIterator.MoveNext(); //start the iteartor

                                    string strPresumedSongTitle = "";
                                    //we presume the song title to be what is after the hyphen in lblStatusChange, but also before the file extension
                                    if (lblStatusChange.Text.Trim().Split('-').Length > 0) strPresumedSongTitle = lblStatusChange.Text.Trim().Split('-')[1].Split('.')[0].Trim();

                                    for (int counter = 0; counter < gnAlbum.Tracks.Count<Track>(); counter++)
                                    {
                                        if (strPresumedSongTitle == "") break; //can't check if there's nothing to check

                                        if (gnTrackIterator.Current.Title.Contains(strPresumedSongTitle)) //if the title of the current song contains the presumed song title
                                        {
                                            strTrackNum = gnTrackIterator.Current.Number.ToString();
                                            strTitle = gnTrackIterator.Current.Title;
                                            break;
                                        }
                                        gnTrackIterator.MoveNext();
                                    }
                                }
                            }

                            if (gnArtwork != null)
                            {
                                //http://cdn.last.fm/flatness/catalogue/noimage/ is what Last.FM returns when there is no album artwork, yet they still give it a tag...
                                if (strUrlForArtwork == "" || strUrlForArtwork.StartsWith("http://cdn.last.fm/flatness/catalogue/noimage/")) strUrlForArtwork = gnArtwork.Uri.ToString();
                            }
                            if (gnTracks != null)
                            {
                                if (strTitle == "") strTitle = gnTracks.Title;
                            }
                            if (strAlbum == "") strAlbum = gnAlbum.Title;
                            if (strArtist == "") strArtist = gnAlbum.Artist;
                            if (strGenre == "") strGenre = gnAlbum.Genre;
                            if (strTrackNum == "") strTrackNum = gnAlbum.MatchedTrackNumber.ToString(); //redundancy with populating strTrackNum above
                            if (strYear == "") strYear = gnAlbum.Year.ToString();
                        }
                        bolDidGracenote = true;
                    }
                    //end gracenote search
                }
                else //we have an MBID so let's use that to get all our info since we can get more from that
                {
                    strURL = "http://ws.audioscrobbler.com/2.0/?method=track.getInfo&autocorrect=1&api_key=" + LAST_FM_API_KEY + "&mbid=" + strMBIDTrack;

                    xmlDoc.Load(new XmlTextReader(strURL));

                    if (xmlDoc.SelectNodes("//album").Count > 0) //if there's an album tag returned by Last.FM
                    {
                        strAlbum = xmlDoc.SelectNodes("//album/title")[0].InnerText;
                        strMBIDAlbum = xmlDoc.SelectNodes("//album/mbid")[0].InnerText;
                        if (xmlDoc.SelectNodes("//album")[0].Attributes.Count != 0) strTrackNum = xmlDoc.SelectNodes("//album")[0].Attributes["position"].Value;
                        strUrlForArtwork = xmlDoc.SelectNodes("//album/image")[xmlDoc.SelectNodes("//album/image").Count - 1].InnerText;
                    }
                    if (xmlDoc.SelectNodes("//artist/name").Count > 0) strArtist = xmlDoc.SelectNodes("//artist/name")[0].InnerText;
                    if (xmlDoc.SelectNodes("//toptags/tag/name").Count > 0) strGenre = xmlDoc.SelectNodes("//toptags/tag/name")[0].InnerText;
                    if (xmlDoc.SelectNodes("//artist/mbid").Count > 0) strMBIDArtist = xmlDoc.SelectNodes("//artist/mbid")[0].InnerText;
                    if (xmlDoc.SelectNodes("//name").Count > 0) strTitle = xmlDoc.SelectNodes("//name")[0].InnerText;

                    if (strMBIDAlbum != "") //we have an MBID for the album
                    {
                        strURL = "http://ws.audioscrobbler.com/2.0/?method=album.getinfo&api_key=" + LAST_FM_API_KEY + "&mbid=" + strMBIDAlbum; //get album-specific info

                        xmlDoc.Load(new XmlTextReader(strURL));

                        strTrackCount = xmlDoc.SelectNodes("//album/tracks/track").Count.ToString();

                        if (xmlDoc.SelectNodes("//album/releasedate").Count > 0)
                        {
                            if (Utilities.IsDate(xmlDoc.SelectNodes("//album/releasedate")[0].InnerText))
                            {
                                strYear = Convert.ToDateTime(xmlDoc.SelectNodes("//album/releasedate")[0].InnerText).Year.ToString();
                            }
                        }
                    }
                }

                if (bolDidGracenote == false)
                {
                    //start Gracenote search for backup
                    //for details about Gracenote search, see above use
                    if (strArtist == "")
                    {
                        strArtist = (txtManArtist.Text.Trim() != "") ? txtManArtist.Text.Trim() : txtManAlbumArtist.Text.Trim();
                    }
                    if (strAlbum == "") strAlbum = txtManAlbum.Text.Trim();
                    if (strTitle == "") strTitle = txtManTitle.Text.Trim();

                    SearchCriteria criteria2 = new SearchCriteria();
                    criteria2.AlbumTitle = strAlbum;
                    criteria2.Artist = strArtist;
                    criteria2.SearchMode = ParkSquare.Gracenote.Dto.SearchMode.BestMatchWithCoverArt;
                    criteria2.SearchOptions = ParkSquare.Gracenote.Dto.SearchOptions.Cover;
                    criteria2.TrackTitle = strTitle;

                    if (criteria2.AlbumTitle != "" || criteria2.Artist != "" || criteria2.TrackTitle != "")
                    {
                        SearchResult gcSearch = gcClient.Search(criteria2);

                        Album gnAlbum = gcSearch.Albums.FirstOrDefault();

                        if (gnAlbum != null)
                        {
                            if (strTrackNum == "") strTrackNum = gnAlbum.MatchedTrackNumber.ToString();

                            Artwork gnArtwork = gnAlbum.Artwork.FirstOrDefault();
                            Track gnTracks = gnAlbum.Tracks.FirstOrDefault();

                            if (gnAlbum.Tracks.Count<Track>() > 0)
                            {
                                if (criteria2.TrackTitle == null)
                                {
                                    if (strTrackCount == "") strTrackCount = gnAlbum.Tracks.Count<Track>().ToString();
                                }

                                if (strTrackNum == "0")
                                {
                                    IEnumerator<Track> gnTrackIterator = gnAlbum.Tracks.GetEnumerator();
                                    gnTrackIterator.MoveNext();

                                    string strPresumedSongTitle = "";
                                    if (lblStatusChange.Text.Trim().Split('-').Length > 0) strPresumedSongTitle = lblStatusChange.Text.Trim().Split('-')[1].Split('.')[0].Trim();

                                    for (int counter = 0; counter < gnAlbum.Tracks.Count<Track>(); counter++)
                                    {
                                        if (strPresumedSongTitle == "") break;

                                        if (gnTrackIterator.Current.Title.Contains(strPresumedSongTitle))
                                        {
                                            strTrackNum = gnTrackIterator.Current.Number.ToString();
                                            strTitle = gnTrackIterator.Current.Title;
                                            break;
                                        }
                                        gnTrackIterator.MoveNext();
                                    }
                                }
                            }

                            if (gnArtwork != null)
                            {
                                if (strUrlForArtwork == "" || strUrlForArtwork.StartsWith("http://cdn.last.fm/flatness/catalogue/noimage/")) strUrlForArtwork = gnArtwork.Uri.ToString();
                            }
                            if (gnTracks != null)
                            {
                                if (strTitle == "") strTitle = gnTracks.Title;
                            }
                            if (strAlbum == "") strAlbum = gnAlbum.Title;
                            if (strArtist == "") strArtist = gnAlbum.Artist;
                            if (strGenre == "") strGenre = gnAlbum.Genre;
                            if (strTrackNum == "") strTrackNum = gnAlbum.MatchedTrackNumber.ToString(); //redundancy with populating strTrackNum above
                            if (strYear == "") strYear = gnAlbum.Year.ToString();
                        }
                    }
                    //end gracenote search
                }

                //make our artist, album, genre and title look pretty
                strArtist = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(strArtist);
                strAlbum = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(strAlbum);
                strGenre = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(strGenre);
                strTitle = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(strTitle);

                //add the artist to our AutoCompleteStringCollection for later songs to use.
                if (!autoCompleteArtists.Contains(strArtist))
                {
                    autoCompleteArtists.Add(strArtist);
                }
                //add the album to our AutoCompleteStringCollection for later songs to use.
                if (!autoCompleteAlbums.Contains(strAlbum))
                {
                    autoCompleteAlbums.Add(strAlbum);
                }
                //add the genre to our AutoCompleteStringCollection for later songs to use.
                if (!autoCompleteGenres.Contains(strGenre))
                {
                    autoCompleteGenres.Add(strGenre);
                }

                //time to actually set the ID3 tags for the song
                tagLibFile.Tag.Performers = new string[] { strArtist };
                tagLibFile.Tag.AlbumArtists = new string[] { strArtist };
                tagLibFile.Tag.Album = strAlbum;
                tagLibFile.Tag.Genres = new string[] { strGenre };
                tagLibFile.Tag.Track = (strTrackNum != "") ? Convert.ToUInt16(strTrackNum) : Convert.ToUInt16(0);
                tagLibFile.Tag.TrackCount = (strTrackCount != "") ? Convert.ToUInt16(strTrackCount) : Convert.ToUInt16(0);
                tagLibFile.Tag.Title = strTitle;
                tagLibFile.Tag.Year = (strYear != "") ? Convert.ToUInt16(strYear) : Convert.ToUInt16(0);
                tagLibFile.Tag.MusicBrainzArtistId = strMBIDArtist;
                tagLibFile.Tag.MusicBrainzDiscId = strMBIDAlbum;
                tagLibFile.Tag.MusicBrainzTrackId = strMBIDTrack;
                if (tagLibFile.Tag.Disc == 0) tagLibFile.Tag.Disc = 1; //default to disc 1 of 1 until Last.FM or Gracenote adds disc info to their API
                if (tagLibFile.Tag.DiscCount == 0) tagLibFile.Tag.DiscCount = 1;

                //if we have a URL for the album artwork, but it's not the default Last.FM link to "noimage"
                if (strUrlForArtwork != "" && !strUrlForArtwork.StartsWith("http://cdn.last.fm/flatness/catalogue/noimage/"))
                {
                    using (WebClient webClient = new WebClient())
                    {
                        byte[] albumArtworkBytes = webClient.DownloadData(strUrlForArtwork);
                        tagLibFile.Tag.Pictures = new TagLib.Picture[1] { new TagLib.Picture(new TagLib.ByteVector(albumArtworkBytes)) };
                    }
                }
                else
                {
                    tagLibFile.Tag.Pictures = null;
                }

                tagLibFile.Save();

                PopulateTextboxes(tagLibFile); //update the form for the user to see the changes
            }
            catch (Exception ex)
            {
                Utilities.GenericCatchBlock(ex);
            }
            finally
            {
                procLastFmFpClient.Close();
                this.Cursor = Cursors.Default;
            }
        }

        private void tslblCurrentPosition_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (tslblCurrentPosition.Text != "0")
                {
                    PopulateTextboxes(listTagLibFiles[intCurrentSongPosition]);
                }
            }
            catch (Exception ex)
            {
                Utilities.GenericCatchBlock(ex);
            }
        }


        #region Button Click Methods

        private void btnAutoDetectForSong_Click(object sender, EventArgs e)
        {
            try
            {
                if (!System.IO.File.Exists(Application.StartupPath + "\\LastFM Stuff\\lastfmfpclient.exe"))
                {
                    MessageBox.Show("The Last.FM fingerprint client was not found. Auto-detection is impossible without it.",
                                    "ID3 Hunter - Missing Fingerprint Client",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (!CanConnectToLastFM())
                {
                    MessageBox.Show("It seems you can't reach Last.FM at the moment. Please try again later.",
                                    "ID3 Hunter - Network Connection Issue",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                AutoDetectForSong(listTagLibFiles[intCurrentSongPosition], listFilePaths[intCurrentSongPosition]);
                PopulateTextboxes(listTagLibFiles[intCurrentSongPosition]);
            }
            catch (Exception ex)
            {
                Utilities.GenericCatchBlock(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SetFieldsEditMode(false);
            PopulateTextboxes(listTagLibFiles[intCurrentSongPosition]);
        }

        private void btnCancelAutoDetect_Click(object sender, EventArgs e)
        {
            bolCancelProgress = true;

            btnCancelAutoDetect.Visible = false;
        }

        private void btnClearArtwork_Click(object sender, EventArgs e)
        {
            try
            {
                picAlbumArtwork.Image = null;
                btnClearArtwork.Enabled = false;
                btnSaveArtwork.Enabled = false;
            }
            catch (Exception ex)
            {
                Utilities.GenericCatchBlock(ex);
            }
        }

        private void btnSaveArtwork_Click(object sender, EventArgs e)
        {
            try
            {
                if (picAlbumArtwork.Image != null)
                {
                    Regex r = new Regex(string.Format("[{0}]", Regex.Escape(invalidFilenameChars)));

                    string strArtworkFileName = "";

                    //set our filename to Artist - Album, but default to Unknown Artist - Unknown Album
                    if (txtManArtist.Text != "")
                    {
                        strArtworkFileName = txtManArtist.Text;
                    }
                    else if (txtManAlbumArtist.Text != "")
                    {
                        strArtworkFileName = txtManAlbumArtist.Text;
                    }
                    else
                    {
                        strArtworkFileName = "Unknown Artist";
                    }
                    if (txtManAlbum.Text != "")
                    {
                        strArtworkFileName = strArtworkFileName + " - " + txtManAlbum.Text;
                    }
                    else
                    {
                        strArtworkFileName = strArtworkFileName + " - Unknown Album";
                    }

                    strArtworkFileName = r.Replace(strArtworkFileName, "");  //escape out the bad filename chars so no exceptions. Replace them with the empty string
                    strArtworkFileName = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Album Artworks\\" + strArtworkFileName;

                    //create the directory if we need it
                    if (!System.IO.Directory.Exists(System.IO.Directory.GetParent(strArtworkFileName).ToString()))
                    {
                        System.IO.Directory.CreateDirectory(System.IO.Directory.GetParent(strArtworkFileName).ToString());
                    }

                    //auto-append a number at the end so we don't over-write existing album artwork
                    int intNumAppended = 0;
                    string strArtworkFileNameFinal = strArtworkFileName;
                    while (System.IO.File.Exists(strArtworkFileNameFinal + ".png"))
                    {
                        strArtworkFileNameFinal = strArtworkFileName + " (" + intNumAppended++ + ")";
                    }
                    strArtworkFileNameFinal += ".png";
                    picAlbumArtwork.Image.Save(strArtworkFileNameFinal, System.Drawing.Imaging.ImageFormat.Png); //and finally save our artwork as a PNG
                }
            }
            catch (Exception ex)
            {
                Utilities.GenericCatchBlock(ex);
            }
        }

        private void btnSelectDir_Click(object sender, EventArgs e)
        {
            if (fbDirSelector.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                if (fbDirSelector.SelectedPath != null)
                {
                    ClearFields();
                    txtSelectedPath.Text = fbDirSelector.SelectedPath;
                    SelectSongs();
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            SetFieldsEditMode(true);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveID3Tags(listTagLibFiles[intCurrentSongPosition]);

                SetFieldsEditMode(false);

                Utilities.CheckToolstripEnable(ref tsNavigator);
            }
            catch (Exception ex)
            {
                Utilities.GenericCatchBlock(ex);
            }
        }

        #endregion


        #region ToolStrip Button Click Event Handlers

        private void tsbtnMoveFirst_Click(object sender, EventArgs e)
        {
            try
            {
                intCurrentSongPosition = 0;
                tslblCurrentPosition.Text = (intCurrentSongPosition + 1).ToString();
            }
            catch (Exception ex)
            {
                Utilities.GenericCatchBlock(ex);
            }
            finally
            {
                Utilities.CheckToolstripEnable(ref tsNavigator);
            }
        }

        private void tsBtnMovePrevious_Click(object sender, EventArgs e)
        {
            try
            {
                intCurrentSongPosition--;
                tslblCurrentPosition.Text = (intCurrentSongPosition + 1).ToString();
            }
            catch (Exception ex)
            {
                Utilities.GenericCatchBlock(ex);
            }
            finally
            {
                Utilities.CheckToolstripEnable(ref tsNavigator);
            }
        }

        private void tsbtnMoveNext_Click(object sender, EventArgs e)
        {
            try
            {
                intCurrentSongPosition++;
                tslblCurrentPosition.Text = (intCurrentSongPosition + 1).ToString();
            }
            catch (Exception ex)
            {
                Utilities.GenericCatchBlock(ex);
            }
            finally
            {
                Utilities.CheckToolstripEnable(ref tsNavigator);
            }
        }

        private void tsBtnMoveLast_Click(object sender, EventArgs e)
        {
            try
            {
                intCurrentSongPosition = Convert.ToInt16(tslblItemCount.Text) - 1;
                tslblCurrentPosition.Text = (intCurrentSongPosition + 1).ToString();
            }
            catch (Exception ex)
            {
                Utilities.GenericCatchBlock(ex);
            }
            finally
            {
                Utilities.CheckToolstripEnable(ref tsNavigator);
            }
        }

        #endregion


        #region Menu Item Methods

        private void miClearAllTags_Click(object sender, EventArgs e)
        {
            try
            {
                if (listTagLibFiles.Count > 0)
                {
                    if (MessageBox.Show("Are you sure you wish to remove all tags for all songs in the folder and subfolders you selected?",
                                        "ID3 Hunter - Tag Removal Confirmation",
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }
                    bolCancelProgress = false;
                    btnCancelAutoDetect.Visible = true;
                    btnEdit.Enabled = false;
                    pbProgress.Value = 0;
                    pbProgress.Maximum = listTagLibFiles.Count;
                    pbProgress.Visible = true;
                    for (int counter = 0; counter < listTagLibFiles.Count; counter++)
                    {
                        if (bolCancelProgress == true) //if the user clicked the Cancel button
                        {
                            break;
                        }
                        lblStatusChange.Text = "Removing tags for " + listFileNames[counter];
                        listTagLibFiles[counter].RemoveTags(TagLib.TagTypes.AllTags);
                        listTagLibFiles[counter].Save();
                        pbProgress.PerformStep();
                        Application.DoEvents();
                    }
                    ClearFields();
                    lblStatusChange.Text = "Viewing " + listFileNames[intCurrentSongPosition];
                    MessageBox.Show("All tags have been successfully removed.", "ID3 Hunter - Tag Removal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("There are currently no songs to remove tags from.", "ID3 Hunter - No Songs Available", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Utilities.GenericCatchBlock(ex);
            }
            finally
            {
                btnCancelAutoDetect.Visible = false;
                pbProgress.Visible = false;
                btnEdit.Enabled = true;
            }
        }

        private void miSortSongs_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSelectedPath.Text == "")
                {
                    MessageBox.Show("You must first select a path of the songs you wish to detect tags for.", "ID3 Hunter - No Path Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (listTagLibFiles.Count == 0)
                {
                    MessageBox.Show("No songs were found in " + txtSelectedPath.Text, "ID3 Hunter - No Songs Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //if there are songs to sort, then open the sorter form
                frmSort frmSorter = new frmSort(txtSelectedPath.Text, strFilePathsALL);
                frmSorter.ShowDialog();
            }
            catch (Exception ex)
            {
                Utilities.GenericCatchBlock(ex);
            }
        }

        private void miExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void miAutoDetectID3Tags_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSelectedPath.Text == "")
                {
                    MessageBox.Show("You must first select a path of the songs you wish to detect tags for.", "ID3 Hunter - No Path Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (listTagLibFiles.Count == 0)
                {
                    MessageBox.Show("No songs were found in " + txtSelectedPath.Text, "ID3 Hunter - No Songs Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!System.IO.File.Exists(Application.StartupPath + "\\LastFM Stuff\\lastfmfpclient.exe"))
                {
                    MessageBox.Show("The Last.FM fingerprint client was not found. Auto-detection is impossible without it.",
                                    "ID3 Hunter - Missing Fingerprint Client",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (!CanConnectToLastFM())
                {
                    MessageBox.Show("It seems you can't reach Last.FM at the moment. Please try again later.",
                                    "ID3 Hunter - Network Connection Issue",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (MessageBox.Show("Are you sure you wish to auto-detect ID3 tags for all songs in the directory/sub-directories in " + txtSelectedPath.Text + "?\n" +
                                    "This cannot can be undone!",
                                    "ID3 Hunter - Auto Detect Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
                tsbtnMoveFirst_Click(tsbtnMoveFirst, EventArgs.Empty);
                btnCancelAutoDetect.Visible = true;
                btnEdit.Enabled = false;
                pbProgress.Value = 0;
                pbProgress.Maximum = listTagLibFiles.Count;
                pbProgress.Visible = true;
                double dblPercentage = 0.0;
                bolCancelProgress = false;
                tsNavigator.Enabled = false; //disable navigation so user can't interrupt
                btnAutoDetectForSong.Enabled = false;

                for (int counter = 0; counter < listTagLibFiles.Count; counter++)
                {
                    if (bolCancelProgress == true) //if the user hit the Cancel button
                    {
                        tsNavigator.Enabled = true;
                        break;
                    }
                    dblPercentage = ((double)pbProgress.Value / (double)pbProgress.Maximum) * 100.0;
                    lblStatusChange.Text = String.Format("{0:N0}", dblPercentage) + "% - Tagging " + listFileNames[counter]; //keep a percentage for user to better judge the progress
                    AutoDetectForSong(listTagLibFiles[counter], listFilePaths[counter], true);
                    if (tslblCurrentPosition.Text != tslblItemCount.Text) tsbtnMoveNext_Click(tsbtnMoveNext, EventArgs.Empty); //move next through the list until we can't anymore (we should be done now)
                    pbProgress.PerformStep();
                    Application.DoEvents();
                }
                lblStatusChange.Text = "Viewing " + listFileNames[intCurrentSongPosition];
                PopulateTextboxes(listTagLibFiles[intCurrentSongPosition]);

                MessageBox.Show("Auto-detection of tags has completed.", "ID3 Hunter - Tag Detection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Utilities.GenericCatchBlock(ex);
            }
            finally
            {
                btnAutoDetectForSong.Enabled = true;
                btnCancelAutoDetect.Visible = false;
                pbProgress.Visible = false;
                btnEdit.Enabled = true;
                tsNavigator.Enabled = true;
            }
        }

        #endregion


        private void picAlbumArtwork_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (listTagLibFiles.Count == 0)
                {
                    //if the .Count is 0, then there are no songs
                    return;
                }
                //there are songs, but we just aren't editing. So prompt user if they wish to edit.
                else if (btnEdit.Enabled == true)
                {
                    if (MessageBox.Show("Do you wish to edit this song?", "ID3 Hunter - Edit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        btnEdit_Click(btnEdit, EventArgs.Empty);
                    }
                    else
                    {
                        return;
                    }
                }

                btnClearArtwork.Enabled = false;
                if (e.Button == System.Windows.Forms.MouseButtons.Left) //user left-clicked
                {
                    if (ofdAlbumArtSelector.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                    {
                        if (ofdAlbumArtSelector.FileName != null)
                        {
                            //set the image in the PictureBox to the image selected
                            picAlbumArtwork.Image = new Bitmap(ofdAlbumArtSelector.FileName.ToString());
                        }
                    }
                }
                else if (e.Button == System.Windows.Forms.MouseButtons.Right) //user right-clicked
                {
                    if (MessageBox.Show("Do you want ID3 Hunter to perform a Google search to get the artwork?" + 
                        "This could take a moment depending on your internet speed. If you change artwork or view a different song before it is done, it will overwrite what you have in your PictureBox.", 
                        "ID3 Hunter - Perform Google Search?", 
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }
                    
                    string strAlbum = txtManAlbum.Text.Trim(); //set up our album and artist
                    string strArtist = txtManArtist.Text.Trim();

                    //get album and artist from user if none was detected
                    if (strArtist == "") strArtist = txtManAlbumArtist.Text.Trim();

                    if (strArtist == "")
                    {
                        strArtist = Microsoft.VisualBasic.Interaction.InputBox("No artist was detected. Enter the artist name of the album you wish to find the artwork for: ", 
                                                                               "ID3 Hunter - Album Artwork Search").Trim();
                    }

                    if (strAlbum == "")
                    {
                        strAlbum = Microsoft.VisualBasic.Interaction.InputBox("No album was detected. Enter the album name you wish to find the artwork for: ",
                                                                              "ID3 Hunter - Album Artwork Search").Trim();
                    }

                    if (strAlbum == "") //if there's still no album name, we can't really do a search for the artwork, now can we???
                    {
                        MessageBox.Show("We cannot perform an artwork search without an album.", "ID3 Hunter - Artwork Search Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    //we have an album and (hopefully) an artist by this point, so let's proceed!
                    GimageSearchClient client = new GimageSearchClient("http://www.google.com");
                    IList<IImageResult> searchResultsImages;

                    //unfortunately, the way that this is done, it has to be async so we can't block the user to wait until it is done.
                    //This is acceptable though since the worst that can happen is it sets the picture box image to the wrong artwork. This will cause no ill effect
                    //  so long as the user did not move to another song (on a different album) and then begin editing it.
                    IAsyncResult result = client.BeginSearch(strArtist + " " + strAlbum + " album", //the search query. Concatenating artist and album together and finally 'album' at the end
                            10, //return 10 results so we can get the highest resolution one
                            ((arResult) =>
                            {
                                int intNumOfPixels = 0;
                                int intIndexToDownload = 0;
                                searchResultsImages = client.EndSearch(arResult);
                                for (int counter = 0; counter < searchResultsImages.Count; counter ++)
                                {
                                    if ((searchResultsImages[counter].Width * searchResultsImages[counter].Height) > intNumOfPixels) //if the current result has more pixels than the previous result
                                    {
                                        intNumOfPixels = searchResultsImages[counter].Width * searchResultsImages[counter].Height; //set the new standard for highest resolution
                                        intIndexToDownload = counter; //and say we want this result
                                    }
                                }
                                using (WebClient webClient = new WebClient())
                                {
                                    byte[] albumArtworkBytes = webClient.DownloadData(searchResultsImages[intIndexToDownload].Url);
                                    picAlbumArtwork.Image = Image.FromStream(new MemoryStream(albumArtworkBytes)).GetThumbnailImage(picAlbumArtwork.Size.Width, picAlbumArtwork.Size.Height, null, IntPtr.Zero);
                                }
                            }),
                            null);
                }
            }
            catch (Exception ex)
            {
                Utilities.GenericCatchBlock(ex);
            }
            finally
            {
                if (picAlbumArtwork.Image != null)
                {
                    btnSaveArtwork.Enabled = true;
                    btnClearArtwork.Enabled = true;
                }
            }
        }

        private void Textbox_Tags_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (tslblItemCount.Text != "0" && btnEdit.Enabled == true)
            {
                if (MessageBox.Show("Do you wish to edit this song?", "ID3 Hunter - Edit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    btnEdit_Click(btnEdit, EventArgs.Empty);
                }
            }
        }

        private void picAlbumArtwork_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                if (btnEdit.Enabled == true)
                {
                    if (MessageBox.Show("Do you wish to edit this song?", "ID3 Hunter - Edit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        btnEdit_Click(btnEdit, EventArgs.Empty);
                        picAlbumArtwork_DragDrop(sender, e);
                    }
                }
                else
                {
                    string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                    //check the first file coming in. If it is a png, jpg or jpeg, then we'll set it to be the Image for the PictureBox
                    if (files.Length > 0 && (Path.GetExtension(files[0].ToLower()) == ".png" || Path.GetExtension(files[0].ToLower()) == ".jpg" || Path.GetExtension(files[0].ToLower()) == ".jpeg"))
                    {
                        picAlbumArtwork.Image = Image.FromFile(files[0]);
                    }
                    else
                    {
                        MessageBox.Show("Drag and drop an image with PNG, JPG or JPEG as its extension.",
                                        "ID3 Hunter - Invalid File Format", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                Utilities.GenericCatchBlock(ex);
            }
        }

        private void picAlbumArtwork_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }
    }
}
