namespace ID3_Hunter
{
    partial class frmSort
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSort));
            this.grpSortingOptions = new System.Windows.Forms.GroupBox();
            this.chkUseArtistTitle = new System.Windows.Forms.CheckBox();
            this.optArtistSong = new System.Windows.Forms.RadioButton();
            this.optArtistAlbumSong = new System.Windows.Forms.RadioButton();
            this.txtExamples = new System.Windows.Forms.TextBox();
            this.btnSort = new System.Windows.Forms.Button();
            this.lblDirToSortTo = new System.Windows.Forms.Label();
            this.txtSelectedPath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.fbDirToSortTo = new System.Windows.Forms.FolderBrowserDialog();
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.grpSortingOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSortingOptions
            // 
            this.grpSortingOptions.Controls.Add(this.chkUseArtistTitle);
            this.grpSortingOptions.Controls.Add(this.optArtistSong);
            this.grpSortingOptions.Controls.Add(this.optArtistAlbumSong);
            this.grpSortingOptions.Location = new System.Drawing.Point(19, 67);
            this.grpSortingOptions.Name = "grpSortingOptions";
            this.grpSortingOptions.Size = new System.Drawing.Size(167, 128);
            this.grpSortingOptions.TabIndex = 0;
            this.grpSortingOptions.TabStop = false;
            this.grpSortingOptions.Text = "Choose a sorting option:";
            // 
            // chkUseArtistTitle
            // 
            this.chkUseArtistTitle.AutoSize = true;
            this.chkUseArtistTitle.Location = new System.Drawing.Point(6, 75);
            this.chkUseArtistTitle.Name = "chkUseArtistTitle";
            this.chkUseArtistTitle.Size = new System.Drawing.Size(123, 17);
            this.chkUseArtistTitle.TabIndex = 2;
            this.chkUseArtistTitle.Text = "Use Artist - Title.mp3";
            this.chkUseArtistTitle.UseVisualStyleBackColor = true;
            // 
            // optArtistSong
            // 
            this.optArtistSong.AutoSize = true;
            this.optArtistSong.Location = new System.Drawing.Point(6, 42);
            this.optArtistSong.Name = "optArtistSong";
            this.optArtistSong.Size = new System.Drawing.Size(113, 17);
            this.optArtistSong.TabIndex = 1;
            this.optArtistSong.Text = "2) Artist\\Song.mp3";
            this.optArtistSong.UseVisualStyleBackColor = true;
            // 
            // optArtistAlbumSong
            // 
            this.optArtistAlbumSong.AutoSize = true;
            this.optArtistAlbumSong.Checked = true;
            this.optArtistAlbumSong.Location = new System.Drawing.Point(6, 19);
            this.optArtistAlbumSong.Name = "optArtistAlbumSong";
            this.optArtistAlbumSong.Size = new System.Drawing.Size(147, 17);
            this.optArtistAlbumSong.TabIndex = 0;
            this.optArtistAlbumSong.TabStop = true;
            this.optArtistAlbumSong.Text = "1) Artist\\Album\\Song.mp3";
            this.optArtistAlbumSong.UseVisualStyleBackColor = true;
            // 
            // txtExamples
            // 
            this.txtExamples.Location = new System.Drawing.Point(192, 67);
            this.txtExamples.Multiline = true;
            this.txtExamples.Name = "txtExamples";
            this.txtExamples.ReadOnly = true;
            this.txtExamples.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtExamples.Size = new System.Drawing.Size(369, 160);
            this.txtExamples.TabIndex = 2;
            this.txtExamples.Text = resources.GetString("txtExamples.Text");
            // 
            // btnSort
            // 
            this.btnSort.Location = new System.Drawing.Point(63, 201);
            this.btnSort.Name = "btnSort";
            this.btnSort.Size = new System.Drawing.Size(75, 23);
            this.btnSort.TabIndex = 3;
            this.btnSort.Text = "Sort";
            this.btnSort.UseVisualStyleBackColor = true;
            this.btnSort.Click += new System.EventHandler(this.btnSort_Click);
            // 
            // lblDirToSortTo
            // 
            this.lblDirToSortTo.AutoSize = true;
            this.lblDirToSortTo.Location = new System.Drawing.Point(12, 15);
            this.lblDirToSortTo.Name = "lblDirToSortTo";
            this.lblDirToSortTo.Size = new System.Drawing.Size(106, 13);
            this.lblDirToSortTo.TabIndex = 4;
            this.lblDirToSortTo.Text = "Directory To Sort To:";
            // 
            // txtSelectedPath
            // 
            this.txtSelectedPath.Location = new System.Drawing.Point(124, 12);
            this.txtSelectedPath.Name = "txtSelectedPath";
            this.txtSelectedPath.ReadOnly = true;
            this.txtSelectedPath.Size = new System.Drawing.Size(356, 20);
            this.txtSelectedPath.TabIndex = 5;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(486, 12);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 6;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // pbProgress
            // 
            this.pbProgress.Location = new System.Drawing.Point(12, 38);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(549, 23);
            this.pbProgress.Step = 1;
            this.pbProgress.TabIndex = 7;
            this.pbProgress.Visible = false;
            // 
            // frmSort
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 245);
            this.Controls.Add(this.pbProgress);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtSelectedPath);
            this.Controls.Add(this.lblDirToSortTo);
            this.Controls.Add(this.btnSort);
            this.Controls.Add(this.txtExamples);
            this.Controls.Add(this.grpSortingOptions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSort";
            this.Text = "ID3 Hunter - Sorting";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSort_FormClosing);
            this.grpSortingOptions.ResumeLayout(false);
            this.grpSortingOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpSortingOptions;
        private System.Windows.Forms.CheckBox chkUseArtistTitle;
        private System.Windows.Forms.RadioButton optArtistSong;
        private System.Windows.Forms.RadioButton optArtistAlbumSong;
        private System.Windows.Forms.TextBox txtExamples;
        private System.Windows.Forms.Button btnSort;
        private System.Windows.Forms.Label lblDirToSortTo;
        private System.Windows.Forms.TextBox txtSelectedPath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.FolderBrowserDialog fbDirToSortTo;
        private System.Windows.Forms.ProgressBar pbProgress;
    }
}