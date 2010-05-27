using com.jds.AWLauncher.classes.gui;

namespace com.jds.AWLauncher.classes.forms
{
    sealed partial class MainForm
	{
		/// <summary>
		/// Требуется переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Обязательный метод для поддержки конструктора - не изменяйте
		/// содержимое данного метода при помощи редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this._infoStart = new System.Windows.Forms.Label();
            this._selectGameLabel = new System.Windows.Forms.Label();
            this._lastNews = new System.Windows.Forms.Label();
            this._homePage = new System.Windows.Forms.Label();
            this._separator1 = new System.Windows.Forms.Label();
            this._faqLabel = new System.Windows.Forms.Label();
            this._separator2 = new System.Windows.Forms.Label();
            this._forumLabel = new System.Windows.Forms.Label();
            this._separator3 = new System.Windows.Forms.Label();
            this._joinNowLabel = new System.Windows.Forms.Label();
            this._statusLabel = new System.Windows.Forms.Label();
            this._versionInfo = new System.Windows.Forms.Label();
            this._minimizedButton = new com.jds.AWLauncher.classes.gui.JImageButton();
            this._settingsButton = new com.jds.AWLauncher.classes.gui.JImageButton();
            this._fullCheck = new com.jds.AWLauncher.classes.gui.JImageButton();
            this._startButton = new com.jds.AWLauncher.classes.gui.JImageButton();
            this._fileProgressBar = new com.jds.AWLauncher.classes.gui.ColorProgressBar();
            this._totalProgress = new com.jds.AWLauncher.classes.gui.ColorProgressBar();
            this._closeBtn = new com.jds.AWLauncher.classes.gui.JImageButton();
            this._tabbedPane = new com.jds.AWLauncher.classes.gui.JTabbedPane();
            ((System.ComponentModel.ISupportInitialize)(this._minimizedButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._settingsButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._fullCheck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._startButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._closeBtn)).BeginInit();
            this.SuspendLayout();
            // 
            // _infoStart
            // 
            this._infoStart.BackColor = System.Drawing.Color.Transparent;
            this._infoStart.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(204)));
            this._infoStart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(137)))), ((int)(((byte)(113)))));
            this._infoStart.Location = new System.Drawing.Point(40, 349);
            this._infoStart.Name = "_infoStart";
            this._infoStart.Size = new System.Drawing.Size(118, 125);
            this._infoStart.TabIndex = 9;
            this._infoStart.Text = "---------------------------------------------";
            this._infoStart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._infoStart.Visible = false;
            // 
            // _selectGameLabel
            // 
            this._selectGameLabel.BackColor = System.Drawing.Color.Transparent;
            this._selectGameLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(204)));
            this._selectGameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(137)))), ((int)(((byte)(113)))));
            this._selectGameLabel.Location = new System.Drawing.Point(43, 239);
            this._selectGameLabel.Name = "_selectGameLabel";
            this._selectGameLabel.Size = new System.Drawing.Size(115, 14);
            this._selectGameLabel.TabIndex = 10;
            this._selectGameLabel.Text = "SELECT GAME";
            this._selectGameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._selectGameLabel.Visible = false;
            // 
            // _lastNews
            // 
            this._lastNews.BackColor = System.Drawing.Color.Transparent;
            this._lastNews.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this._lastNews.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(137)))), ((int)(((byte)(113)))));
            this._lastNews.Location = new System.Drawing.Point(185, 239);
            this._lastNews.Name = "_lastNews";
            this._lastNews.Size = new System.Drawing.Size(370, 14);
            this._lastNews.TabIndex = 11;
            this._lastNews.Text = "LAST NEWS";
            this._lastNews.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._lastNews.Visible = false;
            // 
            // _homePage
            // 
            this._homePage.AutoSize = true;
            this._homePage.BackColor = System.Drawing.Color.Transparent;
            this._homePage.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this._homePage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(137)))), ((int)(((byte)(113)))));
            this._homePage.Location = new System.Drawing.Point(23, 7);
            this._homePage.Margin = new System.Windows.Forms.Padding(0);
            this._homePage.Name = "_homePage";
            this._homePage.Size = new System.Drawing.Size(69, 16);
            this._homePage.TabIndex = 13;
            this._homePage.Tag = "http://awars.net";
            this._homePage.Text = "Homepage";
            this._homePage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._homePage.Visible = false;
            // 
            // _separator1
            // 
            this._separator1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._separator1.AutoSize = true;
            this._separator1.BackColor = System.Drawing.Color.Transparent;
            this._separator1.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this._separator1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(137)))), ((int)(((byte)(113)))));
            this._separator1.Location = new System.Drawing.Point(89, 7);
            this._separator1.Margin = new System.Windows.Forms.Padding(0);
            this._separator1.Name = "_separator1";
            this._separator1.Size = new System.Drawing.Size(14, 16);
            this._separator1.TabIndex = 14;
            this._separator1.Text = "|";
            this._separator1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._separator1.Visible = false;
            // 
            // _faqLabel
            // 
            this._faqLabel.AutoSize = true;
            this._faqLabel.BackColor = System.Drawing.Color.Transparent;
            this._faqLabel.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this._faqLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(137)))), ((int)(((byte)(113)))));
            this._faqLabel.Location = new System.Drawing.Point(98, 7);
            this._faqLabel.Margin = new System.Windows.Forms.Padding(0);
            this._faqLabel.Name = "_faqLabel";
            this._faqLabel.Size = new System.Drawing.Size(32, 16);
            this._faqLabel.TabIndex = 15;
            this._faqLabel.Tag = "http://awars.net/bystryy_start.html ";
            this._faqLabel.Text = "FAQ";
            this._faqLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._faqLabel.Visible = false;
            // 
            // _separator2
            // 
            this._separator2.AutoSize = true;
            this._separator2.BackColor = System.Drawing.Color.Transparent;
            this._separator2.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this._separator2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(137)))), ((int)(((byte)(113)))));
            this._separator2.Location = new System.Drawing.Point(130, 7);
            this._separator2.Margin = new System.Windows.Forms.Padding(0);
            this._separator2.Name = "_separator2";
            this._separator2.Size = new System.Drawing.Size(14, 16);
            this._separator2.TabIndex = 16;
            this._separator2.Text = "|";
            this._separator2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._separator2.Visible = false;
            // 
            // _forumLabel
            // 
            this._forumLabel.AutoSize = true;
            this._forumLabel.BackColor = System.Drawing.Color.Transparent;
            this._forumLabel.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this._forumLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(137)))), ((int)(((byte)(113)))));
            this._forumLabel.Location = new System.Drawing.Point(140, 7);
            this._forumLabel.Margin = new System.Windows.Forms.Padding(0);
            this._forumLabel.Name = "_forumLabel";
            this._forumLabel.Size = new System.Drawing.Size(45, 16);
            this._forumLabel.TabIndex = 17;
            this._forumLabel.Tag = "http://forum.aionwars.com";
            this._forumLabel.Text = "Forum";
            this._forumLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._forumLabel.Visible = false;
            // 
            // _separator3
            // 
            this._separator3.AutoSize = true;
            this._separator3.BackColor = System.Drawing.Color.Transparent;
            this._separator3.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this._separator3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(137)))), ((int)(((byte)(113)))));
            this._separator3.Location = new System.Drawing.Point(182, 7);
            this._separator3.Margin = new System.Windows.Forms.Padding(0);
            this._separator3.Name = "_separator3";
            this._separator3.Size = new System.Drawing.Size(14, 16);
            this._separator3.TabIndex = 18;
            this._separator3.Text = "|";
            this._separator3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._separator3.Visible = false;
            // 
            // _joinNowLabel
            // 
            this._joinNowLabel.AutoSize = true;
            this._joinNowLabel.BackColor = System.Drawing.Color.Transparent;
            this._joinNowLabel.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this._joinNowLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(137)))), ((int)(((byte)(113)))));
            this._joinNowLabel.Location = new System.Drawing.Point(192, 7);
            this._joinNowLabel.Margin = new System.Windows.Forms.Padding(0);
            this._joinNowLabel.Name = "_joinNowLabel";
            this._joinNowLabel.Size = new System.Drawing.Size(59, 16);
            this._joinNowLabel.TabIndex = 19;
            this._joinNowLabel.Tag = "http://awars.net/autoreg.html ";
            this._joinNowLabel.Text = "Join Now";
            this._joinNowLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._joinNowLabel.Visible = false;
            // 
            // _statusLabel
            // 
            this._statusLabel.BackColor = System.Drawing.Color.Transparent;
            this._statusLabel.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this._statusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(137)))), ((int)(((byte)(113)))));
            this._statusLabel.Location = new System.Drawing.Point(78, 470);
            this._statusLabel.Name = "_statusLabel";
            this._statusLabel.Size = new System.Drawing.Size(360, 13);
            this._statusLabel.TabIndex = 21;
            this._statusLabel.Text = "Status Label";
            this._statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._statusLabel.Visible = false;
            // 
            // _versionInfo
            // 
            this._versionInfo.BackColor = System.Drawing.Color.Transparent;
            this._versionInfo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._versionInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(137)))), ((int)(((byte)(113)))));
            this._versionInfo.Location = new System.Drawing.Point(63, 194);
            this._versionInfo.Name = "_versionInfo";
            this._versionInfo.Size = new System.Drawing.Size(480, 15);
            this._versionInfo.TabIndex = 25;
            this._versionInfo.Tag = "ASSEMBLY";
            this._versionInfo.Text = "Test info label";
            this._versionInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _minimizedButton
            // 
            this._minimizedButton.BackColor = System.Drawing.Color.Transparent;
            this._minimizedButton.Enable = true;
            this._minimizedButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this._minimizedButton.Info = null;
            this._minimizedButton.Location = new System.Drawing.Point(550, 8);
            this._minimizedButton.Margin = new System.Windows.Forms.Padding(0);
            this._minimizedButton.Name = "_minimizedButton";
            this._minimizedButton.Size = new System.Drawing.Size(16, 17);
            this._minimizedButton.TabIndex = 22;
            this._minimizedButton.TabStop = false;
            this._minimizedButton.Visible = false;
            this._minimizedButton.Click += new System.EventHandler(this._minimizedButton_Click);
            // 
            // _settingsButton
            // 
            this._settingsButton.BackColor = System.Drawing.Color.Transparent;
            this._settingsButton.Enable = true;
            this._settingsButton.Info = null;
            this._settingsButton.Location = new System.Drawing.Point(468, 5);
            this._settingsButton.Margin = new System.Windows.Forms.Padding(0);
            this._settingsButton.Name = "_settingsButton";
            this._settingsButton.Size = new System.Drawing.Size(77, 23);
            this._settingsButton.TabIndex = 20;
            this._settingsButton.TabStop = false;
            this._settingsButton.Visible = false;
            this._settingsButton.Click += new System.EventHandler(this._settingButton_Click);
            // 
            // _fullCheck
            // 
            this._fullCheck.BackColor = System.Drawing.Color.Transparent;
            this._fullCheck.Enable = true;
            this._fullCheck.Info = null;
            this._fullCheck.Location = new System.Drawing.Point(470, 496);
            this._fullCheck.Margin = new System.Windows.Forms.Padding(0);
            this._fullCheck.Name = "_fullCheck";
            this._fullCheck.Size = new System.Drawing.Size(81, 23);
            this._fullCheck.TabIndex = 12;
            this._fullCheck.TabStop = false;
            this._fullCheck.Visible = false;
            this._fullCheck.Click += new System.EventHandler(this._fullCheck_Click);
            // 
            // _startButton
            // 
            this._startButton.BackColor = System.Drawing.Color.Transparent;
            this._startButton.Enable = true;
            this._startButton.Info = null;
            this._startButton.Location = new System.Drawing.Point(468, 459);
            this._startButton.Margin = new System.Windows.Forms.Padding(0);
            this._startButton.Name = "_startButton";
            this._startButton.Size = new System.Drawing.Size(87, 35);
            this._startButton.TabIndex = 8;
            this._startButton.TabStop = false;
            this._startButton.Visible = false;
            this._startButton.Click += new System.EventHandler(this._startButton_Click);
            // 
            // _fileProgressBar
            // 
            this._fileProgressBar.BarColor = System.Drawing.Color.RoyalBlue;
            this._fileProgressBar.BorderColor = System.Drawing.Color.Transparent;
            this._fileProgressBar.FillStyle = com.jds.AWLauncher.classes.gui.ColorProgressBar.FillStyles.Solid;
            this._fileProgressBar.Location = new System.Drawing.Point(80, 489);
            this._fileProgressBar.Maximum = 100;
            this._fileProgressBar.Minimum = 0;
            this._fileProgressBar.Name = "_fileProgressBar";
            this._fileProgressBar.Size = new System.Drawing.Size(358, 6);
            this._fileProgressBar.Step = 10;
            this._fileProgressBar.TabIndex = 7;
            this._fileProgressBar.Value = 0;
            this._fileProgressBar.Visible = false;
            // 
            // _totalProgress
            // 
            this._totalProgress.BackColor = System.Drawing.Color.White;
            this._totalProgress.BarColor = System.Drawing.Color.Red;
            this._totalProgress.BorderColor = System.Drawing.Color.Transparent;
            this._totalProgress.FillStyle = com.jds.AWLauncher.classes.gui.ColorProgressBar.FillStyles.Solid;
            this._totalProgress.Location = new System.Drawing.Point(80, 496);
            this._totalProgress.Maximum = 100;
            this._totalProgress.Minimum = 0;
            this._totalProgress.Name = "_totalProgress";
            this._totalProgress.Size = new System.Drawing.Size(358, 6);
            this._totalProgress.Step = 10;
            this._totalProgress.TabIndex = 6;
            this._totalProgress.Value = 0;
            this._totalProgress.Visible = false;
            // 
            // _closeBtn
            // 
            this._closeBtn.BackColor = System.Drawing.Color.Transparent;
            this._closeBtn.Enable = true;
            this._closeBtn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this._closeBtn.Info = null;
            this._closeBtn.Location = new System.Drawing.Point(570, 8);
            this._closeBtn.Margin = new System.Windows.Forms.Padding(0);
            this._closeBtn.Name = "_closeBtn";
            this._closeBtn.Size = new System.Drawing.Size(16, 17);
            this._closeBtn.TabIndex = 5;
            this._closeBtn.TabStop = false;
            this._closeBtn.Visible = false;
            // 
            // _tabbedPane
            // 
            this._tabbedPane.AutoSize = true;
            this._tabbedPane.BackColor = System.Drawing.Color.Transparent;
            this._tabbedPane.IsSelectionDisabled = false;
            this._tabbedPane.Location = new System.Drawing.Point(50, 129);
            this._tabbedPane.Name = "_tabbedPane";
            this._tabbedPane.SelectedTab = null;
            this._tabbedPane.Size = new System.Drawing.Size(493, 377);
            this._tabbedPane.TabIndex = 3;
            this._tabbedPane.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(600, 578);
            this.Controls.Add(this._versionInfo);
            this.Controls.Add(this._separator3);
            this.Controls.Add(this._separator2);
            this.Controls.Add(this._separator1);
            this.Controls.Add(this._forumLabel);
            this.Controls.Add(this._minimizedButton);
            this.Controls.Add(this._statusLabel);
            this.Controls.Add(this._settingsButton);
            this.Controls.Add(this._joinNowLabel);
            this.Controls.Add(this._faqLabel);
            this.Controls.Add(this._homePage);
            this.Controls.Add(this._fullCheck);
            this.Controls.Add(this._lastNews);
            this.Controls.Add(this._selectGameLabel);
            this.Controls.Add(this._infoStart);
            this.Controls.Add(this._startButton);
            this.Controls.Add(this._fileProgressBar);
            this.Controls.Add(this._totalProgress);
            this.Controls.Add(this._closeBtn);
            this.Controls.Add(this._tabbedPane);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AWLauncher";
            this.TransparencyKey = System.Drawing.Color.Lime;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.SizeChanged += new System.EventHandler(this.Form_SizeChanged);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_Closing);
            ((System.ComponentModel.ISupportInitialize)(this._minimizedButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._settingsButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._fullCheck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._startButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._closeBtn)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        public JTabbedPane _tabbedPane;
        private JImageButton _closeBtn;
        private ColorProgressBar _totalProgress;
        private ColorProgressBar _fileProgressBar;
        private JImageButton _startButton;
        private System.Windows.Forms.Label _infoStart;
        private System.Windows.Forms.Label _selectGameLabel;
        private System.Windows.Forms.Label _lastNews;
        private JImageButton _fullCheck;
        private System.Windows.Forms.Label _homePage;
        private System.Windows.Forms.Label _separator1;
        private System.Windows.Forms.Label _faqLabel;
        private System.Windows.Forms.Label _separator2;
        private System.Windows.Forms.Label _forumLabel;
        private System.Windows.Forms.Label _separator3;
        private System.Windows.Forms.Label _joinNowLabel;
        private JImageButton _settingsButton;
        private System.Windows.Forms.Label _statusLabel;
        private JImageButton _minimizedButton;
        private System.Windows.Forms.Label _versionInfo;
        
	}
}

