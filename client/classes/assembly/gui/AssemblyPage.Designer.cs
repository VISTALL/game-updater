namespace com.jds.GUpdater.classes.assembly.gui
{
    partial class AssemblyPage
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

		#region Код, автоматически созданный конструктором компонентов

		/// <summary> 
		/// Обязательный метод для поддержки конструктора - не изменяйте 
		/// содержимое данного метода при помощи редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
            this._versionLabel = new System.Windows.Forms.Label();
            this._version = new System.Windows.Forms.Label();
            this._currentVersionLabel = new System.Windows.Forms.Label();
            this._currentVersion = new System.Windows.Forms.Label();
            this._checkButton = new System.Windows.Forms.Button();
            this._fileProgressBar = new System.Windows.Forms.ProgressBar();
            this._totalProgress = new System.Windows.Forms.ProgressBar();
            this._statusLabel = new System.Windows.Forms.Label();
            this._updateBtn = new System.Windows.Forms.Button();
            this._versionTypeLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _versionLabel
            // 
            this._versionLabel.AutoSize = true;
            this._versionLabel.Location = new System.Drawing.Point(13, 16);
            this._versionLabel.Name = "_versionLabel";
            this._versionLabel.Size = new System.Drawing.Size(45, 13);
            this._versionLabel.TabIndex = 0;
            this._versionLabel.Text = "Version:";
            // 
            // _version
            // 
            this._version.Location = new System.Drawing.Point(129, 16);
            this._version.Name = "_version";
            this._version.Size = new System.Drawing.Size(132, 13);
            this._version.TabIndex = 1;
            this._version.Text = "0.0.0.0";
            this._version.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _currentVersionLabel
            // 
            this._currentVersionLabel.AutoSize = true;
            this._currentVersionLabel.Location = new System.Drawing.Point(13, 39);
            this._currentVersionLabel.Name = "_currentVersionLabel";
            this._currentVersionLabel.Size = new System.Drawing.Size(82, 13);
            this._currentVersionLabel.TabIndex = 2;
            this._currentVersionLabel.Text = "Current Version:";
            // 
            // _currentVersion
            // 
            this._currentVersion.Location = new System.Drawing.Point(129, 39);
            this._currentVersion.Name = "_currentVersion";
            this._currentVersion.Size = new System.Drawing.Size(132, 13);
            this._currentVersion.TabIndex = 3;
            this._currentVersion.Text = "0.0.0.0";
            this._currentVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _checkButton
            // 
            this._checkButton.Location = new System.Drawing.Point(602, 16);
            this._checkButton.Name = "_checkButton";
            this._checkButton.Size = new System.Drawing.Size(75, 23);
            this._checkButton.TabIndex = 4;
            this._checkButton.Text = "Check Version";
            this._checkButton.UseVisualStyleBackColor = true;
            this._checkButton.Click += new System.EventHandler(this._checkButton_Click);
            // 
            // _fileProgressBar
            // 
            this._fileProgressBar.Location = new System.Drawing.Point(16, 268);
            this._fileProgressBar.Name = "_fileProgressBar";
            this._fileProgressBar.Size = new System.Drawing.Size(661, 15);
            this._fileProgressBar.TabIndex = 5;
            // 
            // _totalProgress
            // 
            this._totalProgress.Location = new System.Drawing.Point(16, 289);
            this._totalProgress.Name = "_totalProgress";
            this._totalProgress.Size = new System.Drawing.Size(661, 15);
            this._totalProgress.TabIndex = 6;
            // 
            // _statusLabel
            // 
            this._statusLabel.AutoSize = true;
            this._statusLabel.Location = new System.Drawing.Point(13, 243);
            this._statusLabel.Name = "_statusLabel";
            this._statusLabel.Size = new System.Drawing.Size(0, 13);
            this._statusLabel.TabIndex = 7;
            // 
            // _updateBtn
            // 
            this._updateBtn.Enabled = false;
            this._updateBtn.Location = new System.Drawing.Point(602, 45);
            this._updateBtn.Name = "_updateBtn";
            this._updateBtn.Size = new System.Drawing.Size(75, 23);
            this._updateBtn.TabIndex = 8;
            this._updateBtn.Text = "Update";
            this._updateBtn.UseVisualStyleBackColor = true;
            this._updateBtn.Click += new System.EventHandler(this._updateBtn_Click);
            // 
            // _versionTypeLabel
            // 
            this._versionTypeLabel.Location = new System.Drawing.Point(267, 39);
            this._versionTypeLabel.Name = "_versionTypeLabel";
            this._versionTypeLabel.Size = new System.Drawing.Size(81, 13);
            this._versionTypeLabel.TabIndex = 9;
            this._versionTypeLabel.Text = "UNKNOWN";
            this._versionTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // AssemblyPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._versionTypeLabel);
            this.Controls.Add(this._updateBtn);
            this.Controls.Add(this._statusLabel);
            this.Controls.Add(this._totalProgress);
            this.Controls.Add(this._fileProgressBar);
            this.Controls.Add(this._checkButton);
            this.Controls.Add(this._currentVersion);
            this.Controls.Add(this._currentVersionLabel);
            this.Controls.Add(this._version);
            this.Controls.Add(this._versionLabel);
            this.Name = "AssemblyPage";
            this.Size = new System.Drawing.Size(689, 320);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.Label _versionLabel;
        private System.Windows.Forms.Label _version;
        private System.Windows.Forms.Label _currentVersionLabel;
        private System.Windows.Forms.Label _currentVersion;
        private System.Windows.Forms.Button _checkButton;
        private System.Windows.Forms.ProgressBar _fileProgressBar;
        private System.Windows.Forms.ProgressBar _totalProgress;
        private System.Windows.Forms.Label _statusLabel;
        private System.Windows.Forms.Button _updateBtn;
        private System.Windows.Forms.Label _versionTypeLabel;
	}
}
