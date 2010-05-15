namespace com.jds.Premaker.classes.forms
{
    partial class GameFilesForm
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
            this.components = new System.ComponentModel.Container();
            com.jds.Premaker.classes.gui.clistview.CColumn cColumn1 = new com.jds.Premaker.classes.gui.clistview.CColumn();
            com.jds.Premaker.classes.gui.clistview.CColumn cColumn2 = new com.jds.Premaker.classes.gui.clistview.CColumn();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameFilesForm));
            this._rightClick = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.markAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.makeToFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.makeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowse = new System.Windows.Forms.FolderBrowserDialog();
            this.saveBrowse = new System.Windows.Forms.FolderBrowserDialog();
            this._selectedPath = new System.Windows.Forms.TextBox();
            this._makeTo = new System.Windows.Forms.TextBox();
            this._totalProgress = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._listFiles = new com.jds.Premaker.classes.gui.CListView();
            this._statusLabel = new System.Windows.Forms.Label();
            this._rightClick.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _rightClick
            // 
            this._rightClick.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.markAsToolStripMenuItem});
            this._rightClick.Name = "_rightClick";
            this._rightClick.Size = new System.Drawing.Size(116, 26);
            this._rightClick.Text = "Choose action";
            // 
            // markAsToolStripMenuItem
            // 
            this.markAsToolStripMenuItem.Name = "markAsToolStripMenuItem";
            this.markAsToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.markAsToolStripMenuItem.Text = "Mark as";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(660, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mainToolStripMenuItem
            // 
            this.mainToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFolderToolStripMenuItem,
            this.toolStripSeparator1,
            this.makeToFolderToolStripMenuItem,
            this.makeToolStripMenuItem,
            this.toolStripSeparator3,
            this.exitToolStripMenuItem});
            this.mainToolStripMenuItem.Name = "mainToolStripMenuItem";
            this.mainToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.mainToolStripMenuItem.Text = "Main";
            // 
            // openFolderToolStripMenuItem
            // 
            this.openFolderToolStripMenuItem.Name = "openFolderToolStripMenuItem";
            this.openFolderToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.openFolderToolStripMenuItem.Text = "Open Folder";
            this.openFolderToolStripMenuItem.Click += new System.EventHandler(this.openFolderToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(150, 6);
            // 
            // makeToFolderToolStripMenuItem
            // 
            this.makeToFolderToolStripMenuItem.Enabled = false;
            this.makeToFolderToolStripMenuItem.Name = "makeToFolderToolStripMenuItem";
            this.makeToFolderToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.makeToFolderToolStripMenuItem.Text = "Make to Folder";
            this.makeToFolderToolStripMenuItem.Click += new System.EventHandler(this.makeToFolderToolStripMenuItem_Click);
            // 
            // makeToolStripMenuItem
            // 
            this.makeToolStripMenuItem.Enabled = false;
            this.makeToolStripMenuItem.Name = "makeToolStripMenuItem";
            this.makeToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.makeToolStripMenuItem.Text = "Make";
            this.makeToolStripMenuItem.Click += new System.EventHandler(this.makeToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(150, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // _selectedPath
            // 
            this._selectedPath.Location = new System.Drawing.Point(67, 705);
            this._selectedPath.Name = "_selectedPath";
            this._selectedPath.ReadOnly = true;
            this._selectedPath.Size = new System.Drawing.Size(581, 20);
            this._selectedPath.TabIndex = 2;
            // 
            // _makeTo
            // 
            this._makeTo.Location = new System.Drawing.Point(67, 734);
            this._makeTo.Name = "_makeTo";
            this._makeTo.ReadOnly = true;
            this._makeTo.Size = new System.Drawing.Size(581, 20);
            this._makeTo.TabIndex = 3;
            // 
            // _totalProgress
            // 
            this._totalProgress.Location = new System.Drawing.Point(12, 782);
            this._totalProgress.Name = "_totalProgress";
            this._totalProgress.Size = new System.Drawing.Size(636, 23);
            this._totalProgress.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 712);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Path:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 741);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Make to:";
            // 
            // _listFiles
            // 
            this._listFiles.AllowColumnResize = true;
            this._listFiles.AllowMultiselect = true;
            this._listFiles.AlternateBackground = System.Drawing.Color.DarkGreen;
            this._listFiles.AlternatingColors = false;
            this._listFiles.AutoHeight = true;
            this._listFiles.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this._listFiles.BackgroundStretchToFit = true;
            cColumn1.ActivatedEmbeddedType = com.jds.Premaker.classes.gui.GLActivatedEmbeddedTypes.None;
            cColumn1.CheckBoxes = false;
            cColumn1.ImageIndex = -1;
            cColumn1.Name = "Type";
            cColumn1.NumericSort = false;
            cColumn1.Text = "Type";
            cColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            cColumn1.Width = 100;
            cColumn2.ActivatedEmbeddedType = com.jds.Premaker.classes.gui.GLActivatedEmbeddedTypes.None;
            cColumn2.CheckBoxes = false;
            cColumn2.ImageIndex = -1;
            cColumn2.Name = "FileName";
            cColumn2.NumericSort = false;
            cColumn2.Text = "File Name";
            cColumn2.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            cColumn2.Width = 405;
            this._listFiles.Columns.AddRange(new com.jds.Premaker.classes.gui.clistview.CColumn[] {
            cColumn1,
            cColumn2});
            this._listFiles.ContextMenuStrip = this._rightClick;
            this._listFiles.ControlStyle = com.jds.Premaker.classes.gui.GLControlStyles.XP;
            this._listFiles.FullRowSelect = true;
            this._listFiles.GridColor = System.Drawing.Color.LightGray;
            this._listFiles.GridLines = com.jds.Premaker.classes.gui.GLGridLines.gridBoth;
            this._listFiles.GridLineStyle = com.jds.Premaker.classes.gui.GLGridLineStyles.gridSolid;
            this._listFiles.GridTypes = com.jds.Premaker.classes.gui.GLGridTypes.gridOnExists;
            this._listFiles.HeaderHeight = 22;
            this._listFiles.HeaderVisible = true;
            this._listFiles.HeaderWordWrap = false;
            this._listFiles.HotColumnTracking = true;
            this._listFiles.HotItemTracking = false;
            this._listFiles.HotTrackingColor = System.Drawing.Color.LightGray;
            this._listFiles.HoverEvents = false;
            this._listFiles.HoverTime = 1;
            this._listFiles.ImageList = null;
            this._listFiles.ItemHeight = 17;
            this._listFiles.ItemWordWrap = false;
            this._listFiles.Location = new System.Drawing.Point(12, 27);
            this._listFiles.Name = "_listFiles";
            this._listFiles.Selectable = true;
            this._listFiles.SelectedTextColor = System.Drawing.Color.Black;
            this._listFiles.SelectionColor = System.Drawing.Color.Khaki;
            this._listFiles.ShowBorder = true;
            this._listFiles.ShowFocusRect = false;
            this._listFiles.Size = new System.Drawing.Size(636, 672);
            this._listFiles.SortType = com.jds.Premaker.classes.gui.SortTypes.None;
            this._listFiles.SuperFlatHeaderColor = System.Drawing.Color.White;
            this._listFiles.TabIndex = 0;
            // 
            // _statusLabel
            // 
            this._statusLabel.AutoSize = true;
            this._statusLabel.Location = new System.Drawing.Point(12, 766);
            this._statusLabel.Name = "_statusLabel";
            this._statusLabel.Size = new System.Drawing.Size(0, 13);
            this._statusLabel.TabIndex = 7;
            // 
            // GameFilesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 817);
            this.Controls.Add(this._statusLabel);
            this.Controls.Add(this._totalProgress);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this._makeTo);
            this.Controls.Add(this._listFiles);
            this.Controls.Add(this._selectedPath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GameFilesForm";
            this.Text = "Game Files";
            this._rightClick.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private com.jds.Premaker.classes.gui.CListView _listFiles;
        private System.Windows.Forms.ContextMenuStrip _rightClick;
        private System.Windows.Forms.ToolStripMenuItem markAsToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFolderToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowse;
        private System.Windows.Forms.ToolStripMenuItem makeToFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem makeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog saveBrowse;
        private System.Windows.Forms.TextBox _selectedPath;
        private System.Windows.Forms.TextBox _makeTo;
        private System.Windows.Forms.ProgressBar _totalProgress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label _statusLabel;
    }
}