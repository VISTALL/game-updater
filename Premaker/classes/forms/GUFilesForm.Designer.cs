namespace com.jds.Premaker.classes.forms
{
    partial class GUFilesForm
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
            this._filesList = new System.Windows.Forms.ListBox();
            this._openFolder = new System.Windows.Forms.Button();
            this._allFiles = new System.Windows.Forms.ListBox();
            this._forlderBrowse = new System.Windows.Forms.FolderBrowserDialog();
            this.label1 = new System.Windows.Forms.Label();
            this._versionLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._pathBox = new System.Windows.Forms.TextBox();
            this._makeTOBtn = new System.Windows.Forms.Button();
            this._startBtn = new System.Windows.Forms.Button();
            this._saveFolderChoose = new System.Windows.Forms.FolderBrowserDialog();
            this._decsPath = new System.Windows.Forms.TextBox();
            this._decsPathLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _filesList
            // 
            this._filesList.FormattingEnabled = true;
            this._filesList.Location = new System.Drawing.Point(279, 33);
            this._filesList.Name = "_filesList";
            this._filesList.Size = new System.Drawing.Size(171, 394);
            this._filesList.TabIndex = 0;
            this._filesList.SelectedIndexChanged += new System.EventHandler(this._filesList_SelectedIndexChanged);
            // 
            // _openFolder
            // 
            this._openFolder.Location = new System.Drawing.Point(12, 12);
            this._openFolder.Name = "_openFolder";
            this._openFolder.Size = new System.Drawing.Size(75, 23);
            this._openFolder.TabIndex = 1;
            this._openFolder.Text = "Open Folder";
            this._openFolder.UseVisualStyleBackColor = true;
            this._openFolder.Click += new System.EventHandler(this._openFolder_Click);
            // 
            // _allFiles
            // 
            this._allFiles.FormattingEnabled = true;
            this._allFiles.Location = new System.Drawing.Point(102, 33);
            this._allFiles.Name = "_allFiles";
            this._allFiles.Size = new System.Drawing.Size(171, 394);
            this._allFiles.TabIndex = 2;
            this._allFiles.SelectedIndexChanged += new System.EventHandler(this._allFiles_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(99, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Version:";
            // 
            // _versionLabel
            // 
            this._versionLabel.AutoSize = true;
            this._versionLabel.Location = new System.Drawing.Point(150, 17);
            this._versionLabel.Name = "_versionLabel";
            this._versionLabel.Size = new System.Drawing.Size(40, 13);
            this._versionLabel.TabIndex = 4;
            this._versionLabel.Text = "0.0.0.0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 459);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Path:";
            // 
            // _pathBox
            // 
            this._pathBox.Location = new System.Drawing.Point(102, 452);
            this._pathBox.Name = "_pathBox";
            this._pathBox.ReadOnly = true;
            this._pathBox.Size = new System.Drawing.Size(348, 20);
            this._pathBox.TabIndex = 6;
            // 
            // _makeTOBtn
            // 
            this._makeTOBtn.Enabled = false;
            this._makeTOBtn.Location = new System.Drawing.Point(12, 41);
            this._makeTOBtn.Name = "_makeTOBtn";
            this._makeTOBtn.Size = new System.Drawing.Size(75, 23);
            this._makeTOBtn.TabIndex = 7;
            this._makeTOBtn.Text = "Description";
            this._makeTOBtn.UseVisualStyleBackColor = true;
            this._makeTOBtn.Click += new System.EventHandler(this._makeTOBtn_Click);
            // 
            // _startBtn
            // 
            this._startBtn.Enabled = false;
            this._startBtn.Location = new System.Drawing.Point(12, 104);
            this._startBtn.Name = "_startBtn";
            this._startBtn.Size = new System.Drawing.Size(75, 23);
            this._startBtn.TabIndex = 8;
            this._startBtn.Text = "Start";
            this._startBtn.UseVisualStyleBackColor = true;
            this._startBtn.Click += new System.EventHandler(this._startBtn_Click);
            // 
            // _decsPath
            // 
            this._decsPath.Location = new System.Drawing.Point(102, 478);
            this._decsPath.Name = "_decsPath";
            this._decsPath.ReadOnly = true;
            this._decsPath.Size = new System.Drawing.Size(348, 20);
            this._decsPath.TabIndex = 10;
            // 
            // _decsPathLabel
            // 
            this._decsPathLabel.AutoSize = true;
            this._decsPathLabel.Location = new System.Drawing.Point(12, 485);
            this._decsPathLabel.Name = "_decsPathLabel";
            this._decsPathLabel.Size = new System.Drawing.Size(54, 13);
            this._decsPathLabel.TabIndex = 9;
            this._decsPathLabel.Text = "Dsc Path:";
            // 
            // GUFilesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 510);
            this.Controls.Add(this._decsPath);
            this.Controls.Add(this._decsPathLabel);
            this.Controls.Add(this._startBtn);
            this.Controls.Add(this._makeTOBtn);
            this.Controls.Add(this._pathBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._versionLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._allFiles);
            this.Controls.Add(this._openFolder);
            this.Controls.Add(this._filesList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "GUFilesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "GUpdater files";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox _filesList;
        private System.Windows.Forms.Button _openFolder;
        private System.Windows.Forms.ListBox _allFiles;
        private System.Windows.Forms.FolderBrowserDialog _forlderBrowse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label _versionLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox _pathBox;
        private System.Windows.Forms.Button _makeTOBtn;
        private System.Windows.Forms.Button _startBtn;
        private System.Windows.Forms.FolderBrowserDialog _saveFolderChoose;
        private System.Windows.Forms.TextBox _decsPath;
        private System.Windows.Forms.Label _decsPathLabel;
    }
}