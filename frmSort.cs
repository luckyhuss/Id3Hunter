using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ID3_Hunter
{
    public partial class frmSort : Form
    {
        private bool bolSortRunning = false;

        private string strDirectory;
        private string[] strFilePaths;
        private string invalidFilenameChars;

        public frmSort(string strDirectoryParam, string[] strFilePathsParam)
        {
            strFilePaths = strFilePathsParam;
            strDirectory = strDirectoryParam;

            invalidFilenameChars = new string(System.IO.Path.GetInvalidFileNameChars());

            InitializeComponent();
        }

        private void frmSort_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bolSortRunning == true)
            {
                MessageBox.Show("The sort operation is in progress. If you cancel it, unexpected results may happen!",
                                "ID3 Hunter - Closing Cancelled",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Asterisk);
                e.Cancel = true;
            }

            MessageBox.Show("Sorting form is closing. You may want to re-select your directory to update any changes that may have been made.",
                            "ID3 Hunter - Re-Select Directory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void MoveFile(string strFilePath)
        {
            try
            {
                if (!System.IO.File.Exists(strFilePath))
                {
                    return;
                }

                Regex r = new Regex(string.Format("[{0}]", Regex.Escape(invalidFilenameChars)));

                string strFileExtension = System.IO.Path.GetExtension(strFilePath);

                TagLib.File tagLibFileCurrentSong = TagLib.File.Create(strFilePath);

                string strDetectedAlbum = tagLibFileCurrentSong.Tag.Album;
                string strDetectedArtist = tagLibFileCurrentSong.Tag.FirstPerformer;
                string strDetectedTitle = tagLibFileCurrentSong.Tag.Title;
                string strDetectedTrackNum = tagLibFileCurrentSong.Tag.Track.ToString();

                //if the tag is null, use a default value
                if (strDetectedAlbum == null)
                {
                    strDetectedAlbum = "Unknown Album";
                }
                if (strDetectedArtist == null)
                {
                    strDetectedArtist = "Unknown Artist";
                }
                if (strDetectedTitle == null)
                {
                    strDetectedTitle = "Unknown Title";
                }
                if (strDetectedTrackNum == null)
                {
                    strDetectedTrackNum = "00";
                }

                //ensure that track num is always 2 digits for cleanliness and consistency
                strDetectedTrackNum = strDetectedTrackNum.PadLeft(2, '0');

                strDetectedArtist = r.Replace(strDetectedArtist, "");
                strDetectedAlbum = r.Replace(strDetectedAlbum, "");
                strDetectedTitle = r.Replace(strDetectedTitle, "");

                //build the new filename
                string strPathToMoveTo = txtSelectedPath.Text;
                string strNewFileName;

                if (chkUseArtistTitle.Checked == true)
                {
                    strNewFileName = strDetectedArtist + " - " + strDetectedTitle + strFileExtension;
                }
                else
                {
                    strNewFileName = strDetectedTrackNum + " " + strDetectedTitle + strFileExtension;
                }

                if (optArtistAlbumSong.Checked == true)
                {
                    strPathToMoveTo = strPathToMoveTo + "\\" + strDetectedArtist + "\\" + strDetectedAlbum + "\\" + strNewFileName;
                }
                else if (optArtistSong.Checked == true)
                {
                    strPathToMoveTo = strPathToMoveTo + "\\" + strDetectedArtist + "\\" + strNewFileName;
                }
                                
                //get the directory that the song is being moved to and create it if it doesn't exist
                if (!System.IO.Directory.Exists(System.IO.Directory.GetParent(strPathToMoveTo).ToString()))
                {
                    System.IO.Directory.CreateDirectory(System.IO.Directory.GetParent(strPathToMoveTo).ToString());
                }
                if (!System.IO.File.Exists(strPathToMoveTo))
                {
                    System.IO.File.Move(strFilePath, strPathToMoveTo);   
                }

                tagLibFileCurrentSong.Dispose();
            }
            catch (Exception ex)
            {
                Utilities.GenericCatchBlock(ex);
            }
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSelectedPath.Text.Trim() == null)
                {
                    MessageBox.Show("You must first select a directory to sort your files to.",
                                    "ID3 Hunter - No Path Selected",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Asterisk);
                    return;
                }
                if (MessageBox.Show("Are you sure you wish to sort your audio files based on their ID3 tags for all songs in the directory/sub-directories in " + strDirectory + "?\n" +
                                    "This cannot can be undone!",
                                    "ID3 Hunter - Sort Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                bolSortRunning = true;

                pbProgress.Value = 0;
                pbProgress.Maximum = strFilePaths.Length;
                pbProgress.Visible = true;
                foreach (string strFile in strFilePaths)
                {
                    MoveFile(strFile);
                    pbProgress.PerformStep();
                    Application.DoEvents();
                }
                bolSortRunning = false;
                MessageBox.Show("Sorting has completed.", "ID3 Hunter - Sorting Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Utilities.GenericCatchBlock(ex);
            }
            finally
            {
                pbProgress.Value = 0;
                pbProgress.Visible = false;
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                if (fbDirToSortTo.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    if (fbDirToSortTo.SelectedPath != null)
                    {
                        txtSelectedPath.Text = fbDirToSortTo.SelectedPath;
                    }
                }
            }
            catch (Exception ex)
            {
                Utilities.GenericCatchBlock(ex);
            }
        }

    }
}
