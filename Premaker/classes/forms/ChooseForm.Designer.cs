namespace com.jds.Premaker.classes.forms
{
    partial class ChooseForm
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
            this._gameFiles = new System.Windows.Forms.RadioButton();
            this._gupdateFiles = new System.Windows.Forms.RadioButton();
            this._goButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _gameFiles
            // 
            this._gameFiles.AutoSize = true;
            this._gameFiles.Checked = true;
            this._gameFiles.Location = new System.Drawing.Point(12, 12);
            this._gameFiles.Name = "_gameFiles";
            this._gameFiles.Size = new System.Drawing.Size(113, 17);
            this._gameFiles.TabIndex = 0;
            this._gameFiles.TabStop = true;
            this._gameFiles.Text = "Create file of game";
            this._gameFiles.UseVisualStyleBackColor = true;
            // 
            // _gupdateFiles
            // 
            this._gupdateFiles.AutoSize = true;
            this._gupdateFiles.Location = new System.Drawing.Point(12, 35);
            this._gupdateFiles.Name = "_gupdateFiles";
            this._gupdateFiles.Size = new System.Drawing.Size(155, 17);
            this._gupdateFiles.TabIndex = 1;
            this._gupdateFiles.TabStop = true;
            this._gupdateFiles.Text = "Create files of AWLauncher";
            this._gupdateFiles.UseVisualStyleBackColor = true;
            // 
            // _goButton
            // 
            this._goButton.Location = new System.Drawing.Point(156, 12);
            this._goButton.Name = "_goButton";
            this._goButton.Size = new System.Drawing.Size(122, 40);
            this._goButton.TabIndex = 2;
            this._goButton.Text = "GO";
            this._goButton.UseVisualStyleBackColor = true;
            this._goButton.Click += new System.EventHandler(this._goButton_Click);
            // 
            // ChooseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 61);
            this.Controls.Add(this._goButton);
            this.Controls.Add(this._gupdateFiles);
            this.Controls.Add(this._gameFiles);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ChooseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Premaker";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton _gameFiles;
        private System.Windows.Forms.RadioButton _gupdateFiles;
        private System.Windows.Forms.Button _goButton;
    }
}

