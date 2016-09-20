namespace ID3_Hunter
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Label lblSelectedDir;
            System.Windows.Forms.Label lblStatus;
            System.Windows.Forms.Label lblManOf1;
            System.Windows.Forms.Label lblManTrackNum;
            System.Windows.Forms.Label lblManYear;
            System.Windows.Forms.Label lblManAlbumArtist;
            System.Windows.Forms.Label lblManAlbumArtwork;
            System.Windows.Forms.Label lblManAlbum;
            System.Windows.Forms.Label lblManGenre;
            System.Windows.Forms.Label lblManTitle;
            System.Windows.Forms.Label lblManArtist;
            System.Windows.Forms.Label lblManDiscNum;
            System.Windows.Forms.Label lblManOf2;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.tsNavigator = new System.Windows.Forms.ToolStrip();
            this.tsbtnMoveFirst = new System.Windows.Forms.ToolStripButton();
            this.tsBtnMovePrevious = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tslblCurrentPosition = new System.Windows.Forms.ToolStripLabel();
            this.tslblOf = new System.Windows.Forms.ToolStripLabel();
            this.tslblItemCount = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnMoveNext = new System.Windows.Forms.ToolStripButton();
            this.tsBtnMoveLast = new System.Windows.Forms.ToolStripButton();
            this.btnSelectDir = new System.Windows.Forms.Button();
            this.fbDirSelector = new System.Windows.Forms.FolderBrowserDialog();
            this.txtSelectedPath = new System.Windows.Forms.TextBox();
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.lblStatusChange = new System.Windows.Forms.Label();
            this.btnClearArtwork = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.picAlbumArtwork = new System.Windows.Forms.PictureBox();
            this.txtManTrackTotal = new System.Windows.Forms.TextBox();
            this.txtManTrackNum = new System.Windows.Forms.TextBox();
            this.txtManYear = new System.Windows.Forms.TextBox();
            this.txtManAlbumArtist = new System.Windows.Forms.TextBox();
            this.txtManAlbum = new System.Windows.Forms.TextBox();
            this.txtManGenre = new System.Windows.Forms.TextBox();
            this.txtManTitle = new System.Windows.Forms.TextBox();
            this.txtManArtist = new System.Windows.Forms.TextBox();
            this.btnEdit = new System.Windows.Forms.Button();
            this.msMainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miClearAllTags = new System.Windows.Forms.ToolStripMenuItem();
            this.miSortSongs = new System.Windows.Forms.ToolStripMenuItem();
            this.miExit = new System.Windows.Forms.ToolStripMenuItem();
            this.miAutoDetectID3Tags = new System.Windows.Forms.ToolStripMenuItem();
            this.txtManDiscNum = new System.Windows.Forms.TextBox();
            this.txtManDiscTotal = new System.Windows.Forms.TextBox();
            this.ofdAlbumArtSelector = new System.Windows.Forms.OpenFileDialog();
            this.btnAutoDetectForSong = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnCancelAutoDetect = new System.Windows.Forms.Button();
            this.btnSaveArtwork = new System.Windows.Forms.Button();
            lblSelectedDir = new System.Windows.Forms.Label();
            lblStatus = new System.Windows.Forms.Label();
            lblManOf1 = new System.Windows.Forms.Label();
            lblManTrackNum = new System.Windows.Forms.Label();
            lblManYear = new System.Windows.Forms.Label();
            lblManAlbumArtist = new System.Windows.Forms.Label();
            lblManAlbumArtwork = new System.Windows.Forms.Label();
            lblManAlbum = new System.Windows.Forms.Label();
            lblManGenre = new System.Windows.Forms.Label();
            lblManTitle = new System.Windows.Forms.Label();
            lblManArtist = new System.Windows.Forms.Label();
            lblManDiscNum = new System.Windows.Forms.Label();
            lblManOf2 = new System.Windows.Forms.Label();
            this.tsNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAlbumArtwork)).BeginInit();
            this.msMainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSelectedDir
            // 
            lblSelectedDir.AutoSize = true;
            lblSelectedDir.Location = new System.Drawing.Point(145, 31);
            lblSelectedDir.Name = "lblSelectedDir";
            lblSelectedDir.Size = new System.Drawing.Size(97, 13);
            lblSelectedDir.TabIndex = 3;
            lblSelectedDir.Text = "Selected Directory:";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new System.Drawing.Point(12, 67);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new System.Drawing.Size(40, 13);
            lblStatus.TabIndex = 6;
            lblStatus.Text = "Status:";
            // 
            // lblManOf1
            // 
            lblManOf1.AutoSize = true;
            lblManOf1.Location = new System.Drawing.Point(132, 307);
            lblManOf1.Name = "lblManOf1";
            lblManOf1.Size = new System.Drawing.Size(16, 13);
            lblManOf1.TabIndex = 27;
            lblManOf1.Text = "of";
            // 
            // lblManTrackNum
            // 
            lblManTrackNum.AutoSize = true;
            lblManTrackNum.Location = new System.Drawing.Point(37, 307);
            lblManTrackNum.Name = "lblManTrackNum";
            lblManTrackNum.Size = new System.Drawing.Size(48, 13);
            lblManTrackNum.TabIndex = 24;
            lblManTrackNum.Text = "Track #:";
            // 
            // lblManYear
            // 
            lblManYear.AutoSize = true;
            lblManYear.Location = new System.Drawing.Point(53, 333);
            lblManYear.Name = "lblManYear";
            lblManYear.Size = new System.Drawing.Size(32, 13);
            lblManYear.TabIndex = 23;
            lblManYear.Text = "Year:";
            // 
            // lblManAlbumArtist
            // 
            lblManAlbumArtist.AutoSize = true;
            lblManAlbumArtist.Location = new System.Drawing.Point(9, 281);
            lblManAlbumArtist.Name = "lblManAlbumArtist";
            lblManAlbumArtist.Size = new System.Drawing.Size(76, 13);
            lblManAlbumArtist.TabIndex = 22;
            lblManAlbumArtist.Text = "Album Artist(s):";
            // 
            // lblManAlbumArtwork
            // 
            lblManAlbumArtwork.AutoSize = true;
            lblManAlbumArtwork.Location = new System.Drawing.Point(358, 162);
            lblManAlbumArtwork.Name = "lblManAlbumArtwork";
            lblManAlbumArtwork.Size = new System.Drawing.Size(78, 13);
            lblManAlbumArtwork.TabIndex = 21;
            lblManAlbumArtwork.Text = "Album Artwork:";
            // 
            // lblManAlbum
            // 
            lblManAlbum.AutoSize = true;
            lblManAlbum.Location = new System.Drawing.Point(46, 255);
            lblManAlbum.Name = "lblManAlbum";
            lblManAlbum.Size = new System.Drawing.Size(39, 13);
            lblManAlbum.TabIndex = 20;
            lblManAlbum.Text = "Album:";
            // 
            // lblManGenre
            // 
            lblManGenre.AutoSize = true;
            lblManGenre.Location = new System.Drawing.Point(35, 229);
            lblManGenre.Name = "lblManGenre";
            lblManGenre.Size = new System.Drawing.Size(50, 13);
            lblManGenre.TabIndex = 19;
            lblManGenre.Text = "Genre(s):";
            // 
            // lblManTitle
            // 
            lblManTitle.AutoSize = true;
            lblManTitle.Location = new System.Drawing.Point(55, 203);
            lblManTitle.Name = "lblManTitle";
            lblManTitle.Size = new System.Drawing.Size(30, 13);
            lblManTitle.TabIndex = 18;
            lblManTitle.Text = "Title:";
            // 
            // lblManArtist
            // 
            lblManArtist.AutoSize = true;
            lblManArtist.Location = new System.Drawing.Point(41, 177);
            lblManArtist.Name = "lblManArtist";
            lblManArtist.Size = new System.Drawing.Size(44, 13);
            lblManArtist.TabIndex = 17;
            lblManArtist.Text = "Artist(s):";
            // 
            // lblManDiscNum
            // 
            lblManDiscNum.AutoSize = true;
            lblManDiscNum.Location = new System.Drawing.Point(208, 307);
            lblManDiscNum.Name = "lblManDiscNum";
            lblManDiscNum.Size = new System.Drawing.Size(41, 13);
            lblManDiscNum.TabIndex = 43;
            lblManDiscNum.Text = "Disc #:";
            // 
            // lblManOf2
            // 
            lblManOf2.AutoSize = true;
            lblManOf2.Location = new System.Drawing.Point(296, 307);
            lblManOf2.Name = "lblManOf2";
            lblManOf2.Size = new System.Drawing.Size(16, 13);
            lblManOf2.TabIndex = 27;
            lblManOf2.Text = "of";
            // 
            // tsNavigator
            // 
            this.tsNavigator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tsNavigator.Dock = System.Windows.Forms.DockStyle.None;
            this.tsNavigator.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnMoveFirst,
            this.tsBtnMovePrevious,
            this.toolStripSeparator1,
            this.tslblCurrentPosition,
            this.tslblOf,
            this.tslblItemCount,
            this.toolStripSeparator2,
            this.tsbtnMoveNext,
            this.tsBtnMoveLast});
            this.tsNavigator.Location = new System.Drawing.Point(0, 415);
            this.tsNavigator.Name = "tsNavigator";
            this.tsNavigator.Size = new System.Drawing.Size(151, 25);
            this.tsNavigator.TabIndex = 1;
            this.tsNavigator.Text = "toolStrip1";
            // 
            // tsbtnMoveFirst
            // 
            this.tsbtnMoveFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnMoveFirst.Enabled = false;
            this.tsbtnMoveFirst.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnMoveFirst.Image")));
            this.tsbtnMoveFirst.Name = "tsbtnMoveFirst";
            this.tsbtnMoveFirst.RightToLeftAutoMirrorImage = true;
            this.tsbtnMoveFirst.Size = new System.Drawing.Size(23, 22);
            this.tsbtnMoveFirst.Text = "Move first";
            this.tsbtnMoveFirst.Click += new System.EventHandler(this.tsbtnMoveFirst_Click);
            // 
            // tsBtnMovePrevious
            // 
            this.tsBtnMovePrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnMovePrevious.Enabled = false;
            this.tsBtnMovePrevious.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnMovePrevious.Image")));
            this.tsBtnMovePrevious.Name = "tsBtnMovePrevious";
            this.tsBtnMovePrevious.RightToLeftAutoMirrorImage = true;
            this.tsBtnMovePrevious.Size = new System.Drawing.Size(23, 22);
            this.tsBtnMovePrevious.Text = "Move previous";
            this.tsBtnMovePrevious.Click += new System.EventHandler(this.tsBtnMovePrevious_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tslblCurrentPosition
            // 
            this.tslblCurrentPosition.Name = "tslblCurrentPosition";
            this.tslblCurrentPosition.Size = new System.Drawing.Size(13, 22);
            this.tslblCurrentPosition.Text = "0";
            this.tslblCurrentPosition.TextChanged += new System.EventHandler(this.tslblCurrentPosition_TextChanged);
            // 
            // tslblOf
            // 
            this.tslblOf.Name = "tslblOf";
            this.tslblOf.Size = new System.Drawing.Size(18, 22);
            this.tslblOf.Text = "of";
            // 
            // tslblItemCount
            // 
            this.tslblItemCount.Name = "tslblItemCount";
            this.tslblItemCount.Size = new System.Drawing.Size(13, 22);
            this.tslblItemCount.Text = "0";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbtnMoveNext
            // 
            this.tsbtnMoveNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnMoveNext.Enabled = false;
            this.tsbtnMoveNext.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnMoveNext.Image")));
            this.tsbtnMoveNext.Name = "tsbtnMoveNext";
            this.tsbtnMoveNext.RightToLeftAutoMirrorImage = true;
            this.tsbtnMoveNext.Size = new System.Drawing.Size(23, 22);
            this.tsbtnMoveNext.Text = "Move next";
            this.tsbtnMoveNext.Click += new System.EventHandler(this.tsbtnMoveNext_Click);
            // 
            // tsBtnMoveLast
            // 
            this.tsBtnMoveLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnMoveLast.Enabled = false;
            this.tsBtnMoveLast.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnMoveLast.Image")));
            this.tsBtnMoveLast.Name = "tsBtnMoveLast";
            this.tsBtnMoveLast.RightToLeftAutoMirrorImage = true;
            this.tsBtnMoveLast.Size = new System.Drawing.Size(23, 22);
            this.tsBtnMoveLast.Text = "Move last";
            this.tsBtnMoveLast.Click += new System.EventHandler(this.tsBtnMoveLast_Click);
            // 
            // btnSelectDir
            // 
            this.btnSelectDir.Location = new System.Drawing.Point(12, 26);
            this.btnSelectDir.Name = "btnSelectDir";
            this.btnSelectDir.Size = new System.Drawing.Size(127, 23);
            this.btnSelectDir.TabIndex = 1;
            this.btnSelectDir.Text = "Select Directory...";
            this.btnSelectDir.UseVisualStyleBackColor = true;
            this.btnSelectDir.Click += new System.EventHandler(this.btnSelectDir_Click);
            // 
            // fbDirSelector
            // 
            this.fbDirSelector.ShowNewFolderButton = false;
            // 
            // txtSelectedPath
            // 
            this.txtSelectedPath.Location = new System.Drawing.Point(248, 28);
            this.txtSelectedPath.Name = "txtSelectedPath";
            this.txtSelectedPath.ReadOnly = true;
            this.txtSelectedPath.Size = new System.Drawing.Size(367, 20);
            this.txtSelectedPath.TabIndex = 2;
            // 
            // pbProgress
            // 
            this.pbProgress.Location = new System.Drawing.Point(12, 90);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(599, 23);
            this.pbProgress.Step = 1;
            this.pbProgress.TabIndex = 5;
            this.pbProgress.Visible = false;
            // 
            // lblStatusChange
            // 
            this.lblStatusChange.AutoSize = true;
            this.lblStatusChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusChange.Location = new System.Drawing.Point(49, 61);
            this.lblStatusChange.Name = "lblStatusChange";
            this.lblStatusChange.Size = new System.Drawing.Size(62, 20);
            this.lblStatusChange.TabIndex = 7;
            this.lblStatusChange.Text = "Waiting";
            this.lblStatusChange.UseMnemonic = false;
            // 
            // btnClearArtwork
            // 
            this.btnClearArtwork.Enabled = false;
            this.btnClearArtwork.Location = new System.Drawing.Point(527, 149);
            this.btnClearArtwork.Name = "btnClearArtwork";
            this.btnClearArtwork.Size = new System.Drawing.Size(84, 23);
            this.btnClearArtwork.TabIndex = 15;
            this.btnClearArtwork.Text = "Clear Artwork";
            this.btnClearArtwork.UseVisualStyleBackColor = true;
            this.btnClearArtwork.Click += new System.EventHandler(this.btnClearArtwork_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(251, 372);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(99, 23);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // picAlbumArtwork
            // 
            this.picAlbumArtwork.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picAlbumArtwork.Location = new System.Drawing.Point(361, 178);
            this.picAlbumArtwork.Name = "picAlbumArtwork";
            this.picAlbumArtwork.Size = new System.Drawing.Size(250, 250);
            this.picAlbumArtwork.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picAlbumArtwork.TabIndex = 38;
            this.picAlbumArtwork.TabStop = false;
            this.picAlbumArtwork.DragDrop += new System.Windows.Forms.DragEventHandler(this.picAlbumArtwork_DragDrop);
            this.picAlbumArtwork.DragEnter += new System.Windows.Forms.DragEventHandler(this.picAlbumArtwork_DragEnter);
            this.picAlbumArtwork.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picAlbumArtwork_MouseClick);
            // 
            // txtManTrackTotal
            // 
            this.txtManTrackTotal.Location = new System.Drawing.Point(156, 304);
            this.txtManTrackTotal.MaxLength = 2;
            this.txtManTrackTotal.Name = "txtManTrackTotal";
            this.txtManTrackTotal.ReadOnly = true;
            this.txtManTrackTotal.Size = new System.Drawing.Size(35, 20);
            this.txtManTrackTotal.TabIndex = 9;
            this.txtManTrackTotal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Textbox_Tags_KeyPress);
            // 
            // txtManTrackNum
            // 
            this.txtManTrackNum.Location = new System.Drawing.Point(91, 304);
            this.txtManTrackNum.MaxLength = 2;
            this.txtManTrackNum.Name = "txtManTrackNum";
            this.txtManTrackNum.ReadOnly = true;
            this.txtManTrackNum.Size = new System.Drawing.Size(35, 20);
            this.txtManTrackNum.TabIndex = 8;
            this.txtManTrackNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Textbox_Tags_KeyPress);
            // 
            // txtManYear
            // 
            this.txtManYear.Location = new System.Drawing.Point(91, 330);
            this.txtManYear.MaxLength = 4;
            this.txtManYear.Name = "txtManYear";
            this.txtManYear.ReadOnly = true;
            this.txtManYear.Size = new System.Drawing.Size(50, 20);
            this.txtManYear.TabIndex = 12;
            this.txtManYear.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Textbox_Tags_KeyPress);
            // 
            // txtManAlbumArtist
            // 
            this.txtManAlbumArtist.Location = new System.Drawing.Point(91, 278);
            this.txtManAlbumArtist.MaxLength = 255;
            this.txtManAlbumArtist.Name = "txtManAlbumArtist";
            this.txtManAlbumArtist.ReadOnly = true;
            this.txtManAlbumArtist.Size = new System.Drawing.Size(264, 20);
            this.txtManAlbumArtist.TabIndex = 7;
            this.txtManAlbumArtist.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Textbox_Tags_KeyPress);
            // 
            // txtManAlbum
            // 
            this.txtManAlbum.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtManAlbum.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtManAlbum.Location = new System.Drawing.Point(91, 252);
            this.txtManAlbum.MaxLength = 255;
            this.txtManAlbum.Name = "txtManAlbum";
            this.txtManAlbum.ReadOnly = true;
            this.txtManAlbum.Size = new System.Drawing.Size(264, 20);
            this.txtManAlbum.TabIndex = 6;
            this.txtManAlbum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Textbox_Tags_KeyPress);
            // 
            // txtManGenre
            // 
            this.txtManGenre.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtManGenre.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtManGenre.Location = new System.Drawing.Point(91, 226);
            this.txtManGenre.MaxLength = 255;
            this.txtManGenre.Name = "txtManGenre";
            this.txtManGenre.ReadOnly = true;
            this.txtManGenre.Size = new System.Drawing.Size(264, 20);
            this.txtManGenre.TabIndex = 5;
            this.txtManGenre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Textbox_Tags_KeyPress);
            // 
            // txtManTitle
            // 
            this.txtManTitle.Location = new System.Drawing.Point(91, 200);
            this.txtManTitle.MaxLength = 255;
            this.txtManTitle.Name = "txtManTitle";
            this.txtManTitle.ReadOnly = true;
            this.txtManTitle.Size = new System.Drawing.Size(264, 20);
            this.txtManTitle.TabIndex = 4;
            this.txtManTitle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Textbox_Tags_KeyPress);
            // 
            // txtManArtist
            // 
            this.txtManArtist.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtManArtist.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtManArtist.Location = new System.Drawing.Point(91, 174);
            this.txtManArtist.MaxLength = 255;
            this.txtManArtist.Name = "txtManArtist";
            this.txtManArtist.ReadOnly = true;
            this.txtManArtist.Size = new System.Drawing.Size(264, 20);
            this.txtManArtist.TabIndex = 3;
            this.txtManArtist.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Textbox_Tags_KeyPress);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEdit.Enabled = false;
            this.btnEdit.Location = new System.Drawing.Point(143, 372);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(99, 23);
            this.btnEdit.TabIndex = 13;
            this.btnEdit.Text = "&Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // msMainMenu
            // 
            this.msMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.miAutoDetectID3Tags});
            this.msMainMenu.Location = new System.Drawing.Point(0, 0);
            this.msMainMenu.Name = "msMainMenu";
            this.msMainMenu.Size = new System.Drawing.Size(626, 24);
            this.msMainMenu.TabIndex = 42;
            this.msMainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miClearAllTags,
            this.miSortSongs,
            this.miExit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // miClearAllTags
            // 
            this.miClearAllTags.Name = "miClearAllTags";
            this.miClearAllTags.Size = new System.Drawing.Size(146, 22);
            this.miClearAllTags.Text = "Clear All Tags";
            this.miClearAllTags.Click += new System.EventHandler(this.miClearAllTags_Click);
            // 
            // miSortSongs
            // 
            this.miSortSongs.Name = "miSortSongs";
            this.miSortSongs.Size = new System.Drawing.Size(146, 22);
            this.miSortSongs.Text = "&Sort Songs";
            this.miSortSongs.Click += new System.EventHandler(this.miSortSongs_Click);
            // 
            // miExit
            // 
            this.miExit.Name = "miExit";
            this.miExit.Size = new System.Drawing.Size(146, 22);
            this.miExit.Text = "E&xit";
            this.miExit.Click += new System.EventHandler(this.miExit_Click);
            // 
            // miAutoDetectID3Tags
            // 
            this.miAutoDetectID3Tags.Name = "miAutoDetectID3Tags";
            this.miAutoDetectID3Tags.Size = new System.Drawing.Size(147, 20);
            this.miAutoDetectID3Tags.Text = "Auto Detect All ID3 Tags";
            this.miAutoDetectID3Tags.Click += new System.EventHandler(this.miAutoDetectID3Tags_Click);
            // 
            // txtManDiscNum
            // 
            this.txtManDiscNum.Location = new System.Drawing.Point(255, 304);
            this.txtManDiscNum.MaxLength = 2;
            this.txtManDiscNum.Name = "txtManDiscNum";
            this.txtManDiscNum.ReadOnly = true;
            this.txtManDiscNum.Size = new System.Drawing.Size(35, 20);
            this.txtManDiscNum.TabIndex = 10;
            this.txtManDiscNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Textbox_Tags_KeyPress);
            // 
            // txtManDiscTotal
            // 
            this.txtManDiscTotal.Location = new System.Drawing.Point(320, 304);
            this.txtManDiscTotal.MaxLength = 2;
            this.txtManDiscTotal.Name = "txtManDiscTotal";
            this.txtManDiscTotal.ReadOnly = true;
            this.txtManDiscTotal.Size = new System.Drawing.Size(35, 20);
            this.txtManDiscTotal.TabIndex = 11;
            this.txtManDiscTotal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Textbox_Tags_KeyPress);
            // 
            // ofdAlbumArtSelector
            // 
            this.ofdAlbumArtSelector.Filter = "Image Files|*.jpg;*.jpeg;*.png";
            // 
            // btnAutoDetectForSong
            // 
            this.btnAutoDetectForSong.Enabled = false;
            this.btnAutoDetectForSong.Location = new System.Drawing.Point(91, 135);
            this.btnAutoDetectForSong.Name = "btnAutoDetectForSong";
            this.btnAutoDetectForSong.Size = new System.Drawing.Size(179, 23);
            this.btnAutoDetectForSong.TabIndex = 2;
            this.btnAutoDetectForSong.Text = "Auto Detect For This Song";
            this.btnAutoDetectForSong.UseVisualStyleBackColor = true;
            this.btnAutoDetectForSong.Click += new System.EventHandler(this.btnAutoDetectForSong_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(38, 373);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(99, 23);
            this.btnCancel.TabIndex = 44;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnCancelAutoDetect
            // 
            this.btnCancelAutoDetect.Location = new System.Drawing.Point(452, 119);
            this.btnCancelAutoDetect.Name = "btnCancelAutoDetect";
            this.btnCancelAutoDetect.Size = new System.Drawing.Size(109, 40);
            this.btnCancelAutoDetect.TabIndex = 45;
            this.btnCancelAutoDetect.Text = "Cancel Auto-Detect / Clearing";
            this.btnCancelAutoDetect.UseVisualStyleBackColor = true;
            this.btnCancelAutoDetect.Visible = false;
            this.btnCancelAutoDetect.Click += new System.EventHandler(this.btnCancelAutoDetect_Click);
            // 
            // btnSaveArtwork
            // 
            this.btnSaveArtwork.Enabled = false;
            this.btnSaveArtwork.Location = new System.Drawing.Point(437, 149);
            this.btnSaveArtwork.Name = "btnSaveArtwork";
            this.btnSaveArtwork.Size = new System.Drawing.Size(84, 23);
            this.btnSaveArtwork.TabIndex = 46;
            this.btnSaveArtwork.Text = "Save Artwork";
            this.btnSaveArtwork.UseVisualStyleBackColor = true;
            this.btnSaveArtwork.Click += new System.EventHandler(this.btnSaveArtwork_Click);
            // 
            // frmMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 443);
            this.Controls.Add(this.btnCancelAutoDetect);
            this.Controls.Add(this.btnSaveArtwork);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAutoDetectForSong);
            this.Controls.Add(this.txtManDiscTotal);
            this.Controls.Add(this.txtManDiscNum);
            this.Controls.Add(lblManDiscNum);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnClearArtwork);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.picAlbumArtwork);
            this.Controls.Add(this.txtManTrackTotal);
            this.Controls.Add(this.txtManTrackNum);
            this.Controls.Add(this.txtManYear);
            this.Controls.Add(this.txtManAlbumArtist);
            this.Controls.Add(this.txtManAlbum);
            this.Controls.Add(this.txtManGenre);
            this.Controls.Add(this.txtManTitle);
            this.Controls.Add(this.txtManArtist);
            this.Controls.Add(lblManOf2);
            this.Controls.Add(lblManOf1);
            this.Controls.Add(lblManTrackNum);
            this.Controls.Add(lblManYear);
            this.Controls.Add(lblManAlbumArtist);
            this.Controls.Add(lblManAlbumArtwork);
            this.Controls.Add(lblManAlbum);
            this.Controls.Add(lblManGenre);
            this.Controls.Add(lblManTitle);
            this.Controls.Add(lblManArtist);
            this.Controls.Add(this.lblStatusChange);
            this.Controls.Add(lblStatus);
            this.Controls.Add(this.pbProgress);
            this.Controls.Add(this.txtSelectedPath);
            this.Controls.Add(lblSelectedDir);
            this.Controls.Add(this.btnSelectDir);
            this.Controls.Add(this.tsNavigator);
            this.Controls.Add(this.msMainMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.msMainMenu;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "ID3 Hunter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.tsNavigator.ResumeLayout(false);
            this.tsNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAlbumArtwork)).EndInit();
            this.msMainMenu.ResumeLayout(false);
            this.msMainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsNavigator;
        private System.Windows.Forms.ToolStripButton tsbtnMoveFirst;
        private System.Windows.Forms.ToolStripButton tsBtnMovePrevious;
        private System.Windows.Forms.ToolStripLabel tslblCurrentPosition;
        private System.Windows.Forms.ToolStripLabel tslblOf;
        private System.Windows.Forms.ToolStripLabel tslblItemCount;
        private System.Windows.Forms.ToolStripButton tsbtnMoveNext;
        private System.Windows.Forms.ToolStripButton tsBtnMoveLast;
        private System.Windows.Forms.Button btnSelectDir;
        private System.Windows.Forms.FolderBrowserDialog fbDirSelector;
        private System.Windows.Forms.ProgressBar pbProgress;
        private System.Windows.Forms.Label lblStatusChange;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Button btnClearArtwork;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.PictureBox picAlbumArtwork;
        private System.Windows.Forms.TextBox txtManTrackTotal;
        private System.Windows.Forms.TextBox txtManTrackNum;
        private System.Windows.Forms.TextBox txtManYear;
        private System.Windows.Forms.TextBox txtManAlbumArtist;
        private System.Windows.Forms.TextBox txtManAlbum;
        private System.Windows.Forms.TextBox txtManGenre;
        private System.Windows.Forms.TextBox txtManTitle;
        private System.Windows.Forms.TextBox txtManArtist;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.MenuStrip msMainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.TextBox txtManDiscNum;
        private System.Windows.Forms.TextBox txtManDiscTotal;
        private System.Windows.Forms.ToolStripMenuItem miClearAllTags;
        private System.Windows.Forms.ToolStripMenuItem miExit;
        private System.Windows.Forms.ToolStripMenuItem miAutoDetectID3Tags;
        private System.Windows.Forms.OpenFileDialog ofdAlbumArtSelector;
        private System.Windows.Forms.Button btnAutoDetectForSong;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ToolStripMenuItem miSortSongs;
        internal System.Windows.Forms.TextBox txtSelectedPath;
        private System.Windows.Forms.Button btnCancelAutoDetect;
        private System.Windows.Forms.Button btnSaveArtwork;
    }
}

