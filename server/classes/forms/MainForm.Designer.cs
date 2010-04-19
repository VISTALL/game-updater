namespace com.jds.Builder.classes.forms
{
    partial class MainForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.folderBrowse = new System.Windows.Forms.FolderBrowserDialog();
            this.listOfFiles = new System.Windows.Forms.ListBox();
            this.path = new System.Windows.Forms.TextBox();
            this.toConvert = new System.Windows.Forms.ListBox();
            this.addButton = new System.Windows.Forms.Button();
            this.MakeBtn = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.addAllBtn = new System.Windows.Forms.Button();
            this.removeBtn = new System.Windows.Forms.Button();
            this.removeAllBtn = new System.Windows.Forms.Button();
            this.copyRoot = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.saveBrowse = new System.Windows.Forms.FolderBrowserDialog();
            this.saveToBtn = new System.Windows.Forms.Button();
            this.progBar = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.critBox = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.addCritBtn = new System.Windows.Forms.Button();
            this.delCritBtn = new System.Windows.Forms.Button();
            this.addAlCritBtn = new System.Windows.Forms.Button();
            this.dellCritBtn = new System.Windows.Forms.Button();
            this.CompressLabel = new System.Windows.Forms.Label();
            this.ProgressLabel = new System.Windows.Forms.Label();
            this.stopButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Open";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listOfFiles
            // 
            this.listOfFiles.FormattingEnabled = true;
            this.listOfFiles.HorizontalScrollbar = true;
            this.listOfFiles.Location = new System.Drawing.Point(109, 24);
            this.listOfFiles.Name = "listOfFiles";
            this.listOfFiles.ScrollAlwaysVisible = true;
            this.listOfFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listOfFiles.Size = new System.Drawing.Size(269, 563);
            this.listOfFiles.TabIndex = 1;
            // 
            // path
            // 
            this.path.Location = new System.Drawing.Point(109, 601);
            this.path.Name = "path";
            this.path.ReadOnly = true;
            this.path.Size = new System.Drawing.Size(981, 20);
            this.path.TabIndex = 2;
            // 
            // toConvert
            // 
            this.toConvert.FormattingEnabled = true;
            this.toConvert.HorizontalScrollbar = true;
            this.toConvert.Location = new System.Drawing.Point(465, 24);
            this.toConvert.Name = "toConvert";
            this.toConvert.ScrollAlwaysVisible = true;
            this.toConvert.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.toConvert.Size = new System.Drawing.Size(269, 563);
            this.toConvert.TabIndex = 3;
            // 
            // addButton
            // 
            this.addButton.Enabled = false;
            this.addButton.Location = new System.Drawing.Point(384, 24);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 4;
            this.addButton.Text = ">";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // MakeBtn
            // 
            this.MakeBtn.Enabled = false;
            this.MakeBtn.Location = new System.Drawing.Point(15, 133);
            this.MakeBtn.Name = "MakeBtn";
            this.MakeBtn.Size = new System.Drawing.Size(75, 23);
            this.MakeBtn.TabIndex = 5;
            this.MakeBtn.Text = "Make";
            this.MakeBtn.UseVisualStyleBackColor = true;
            this.MakeBtn.Click += new System.EventHandler(this.button2_Click);
            // 
            // clearButton
            // 
            this.clearButton.Enabled = false;
            this.clearButton.Location = new System.Drawing.Point(12, 53);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(75, 23);
            this.clearButton.TabIndex = 6;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // addAllBtn
            // 
            this.addAllBtn.Enabled = false;
            this.addAllBtn.Location = new System.Drawing.Point(384, 53);
            this.addAllBtn.Name = "addAllBtn";
            this.addAllBtn.Size = new System.Drawing.Size(75, 23);
            this.addAllBtn.TabIndex = 7;
            this.addAllBtn.Text = ">>";
            this.addAllBtn.UseVisualStyleBackColor = true;
            this.addAllBtn.Click += new System.EventHandler(this.addAllBtn_Click);
            // 
            // removeBtn
            // 
            this.removeBtn.Enabled = false;
            this.removeBtn.Location = new System.Drawing.Point(384, 104);
            this.removeBtn.Name = "removeBtn";
            this.removeBtn.Size = new System.Drawing.Size(75, 23);
            this.removeBtn.TabIndex = 8;
            this.removeBtn.Text = "<";
            this.removeBtn.UseVisualStyleBackColor = true;
            this.removeBtn.Click += new System.EventHandler(this.removeBtn_Click);
            // 
            // removeAllBtn
            // 
            this.removeAllBtn.Enabled = false;
            this.removeAllBtn.Location = new System.Drawing.Point(384, 133);
            this.removeAllBtn.Name = "removeAllBtn";
            this.removeAllBtn.Size = new System.Drawing.Size(75, 23);
            this.removeAllBtn.TabIndex = 9;
            this.removeAllBtn.Text = "<<";
            this.removeAllBtn.UseVisualStyleBackColor = true;
            this.removeAllBtn.Click += new System.EventHandler(this.removeAllBtn_Click);
            // 
            // copyRoot
            // 
            this.copyRoot.Location = new System.Drawing.Point(109, 627);
            this.copyRoot.Name = "copyRoot";
            this.copyRoot.ReadOnly = true;
            this.copyRoot.Size = new System.Drawing.Size(981, 20);
            this.copyRoot.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 604);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Current Root:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 630);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "To root:";
            // 
            // saveToBtn
            // 
            this.saveToBtn.Enabled = false;
            this.saveToBtn.Location = new System.Drawing.Point(15, 104);
            this.saveToBtn.Name = "saveToBtn";
            this.saveToBtn.Size = new System.Drawing.Size(75, 23);
            this.saveToBtn.TabIndex = 13;
            this.saveToBtn.Text = "Make To...";
            this.saveToBtn.UseVisualStyleBackColor = true;
            this.saveToBtn.Click += new System.EventHandler(this.saveToBtn_Click);
            // 
            // progBar
            // 
            this.progBar.Location = new System.Drawing.Point(109, 674);
            this.progBar.Name = "progBar";
            this.progBar.Size = new System.Drawing.Size(981, 20);
            this.progBar.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(106, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "List to add:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(462, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Normal Update";
            // 
            // critBox
            // 
            this.critBox.FormattingEnabled = true;
            this.critBox.HorizontalScrollbar = true;
            this.critBox.Location = new System.Drawing.Point(821, 24);
            this.critBox.Name = "critBox";
            this.critBox.ScrollAlwaysVisible = true;
            this.critBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.critBox.Size = new System.Drawing.Size(269, 563);
            this.critBox.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(818, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Critical Update";
            // 
            // addCritBtn
            // 
            this.addCritBtn.Enabled = false;
            this.addCritBtn.Location = new System.Drawing.Point(740, 24);
            this.addCritBtn.Name = "addCritBtn";
            this.addCritBtn.Size = new System.Drawing.Size(75, 23);
            this.addCritBtn.TabIndex = 19;
            this.addCritBtn.Text = ">";
            this.addCritBtn.UseVisualStyleBackColor = true;
            this.addCritBtn.Click += new System.EventHandler(this.addCritBtn_Click);
            // 
            // delCritBtn
            // 
            this.delCritBtn.Enabled = false;
            this.delCritBtn.Location = new System.Drawing.Point(740, 104);
            this.delCritBtn.Name = "delCritBtn";
            this.delCritBtn.Size = new System.Drawing.Size(75, 23);
            this.delCritBtn.TabIndex = 20;
            this.delCritBtn.Text = "<";
            this.delCritBtn.UseVisualStyleBackColor = true;
            this.delCritBtn.Click += new System.EventHandler(this.delCritBtn_Click);
            // 
            // addAlCritBtn
            // 
            this.addAlCritBtn.Enabled = false;
            this.addAlCritBtn.Location = new System.Drawing.Point(740, 53);
            this.addAlCritBtn.Name = "addAlCritBtn";
            this.addAlCritBtn.Size = new System.Drawing.Size(75, 23);
            this.addAlCritBtn.TabIndex = 21;
            this.addAlCritBtn.Text = ">>";
            this.addAlCritBtn.UseVisualStyleBackColor = true;
            this.addAlCritBtn.Click += new System.EventHandler(this.addAlCritBtn_Click);
            // 
            // dellCritBtn
            // 
            this.dellCritBtn.Enabled = false;
            this.dellCritBtn.Location = new System.Drawing.Point(740, 133);
            this.dellCritBtn.Name = "dellCritBtn";
            this.dellCritBtn.Size = new System.Drawing.Size(75, 23);
            this.dellCritBtn.TabIndex = 22;
            this.dellCritBtn.Text = "<<";
            this.dellCritBtn.UseVisualStyleBackColor = true;
            this.dellCritBtn.Click += new System.EventHandler(this.dellCritBtn_Click);
            // 
            // CompressLabel
            // 
            this.CompressLabel.AutoSize = true;
            this.CompressLabel.Location = new System.Drawing.Point(107, 658);
            this.CompressLabel.Name = "CompressLabel";
            this.CompressLabel.Size = new System.Drawing.Size(0, 13);
            this.CompressLabel.TabIndex = 24;
            // 
            // ProgressLabel
            // 
            this.ProgressLabel.AutoSize = true;
            this.ProgressLabel.Location = new System.Drawing.Point(107, 702);
            this.ProgressLabel.Name = "ProgressLabel";
            this.ProgressLabel.Size = new System.Drawing.Size(0, 13);
            this.ProgressLabel.TabIndex = 25;
            // 
            // stopButton
            // 
            this.stopButton.Enabled = false;
            this.stopButton.Location = new System.Drawing.Point(15, 162);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 26;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // Builder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1103, 704);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.ProgressLabel);
            this.Controls.Add(this.CompressLabel);
            this.Controls.Add(this.progBar);
            this.Controls.Add(this.dellCritBtn);
            this.Controls.Add(this.addAlCritBtn);
            this.Controls.Add(this.delCritBtn);
            this.Controls.Add(this.addCritBtn);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.critBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.addAllBtn);
            this.Controls.Add(this.saveToBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.copyRoot);
            this.Controls.Add(this.removeAllBtn);
            this.Controls.Add(this.removeBtn);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.MakeBtn);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.toConvert);
            this.Controls.Add(this.path);
            this.Controls.Add(this.listOfFiles);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Builder";
            this.Text = "Builder";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MFormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowse;
        private System.Windows.Forms.ListBox listOfFiles;
        private System.Windows.Forms.TextBox path;
        private System.Windows.Forms.ListBox toConvert;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button MakeBtn;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button addAllBtn;
        private System.Windows.Forms.Button removeBtn;
        private System.Windows.Forms.Button removeAllBtn;
        private System.Windows.Forms.TextBox copyRoot;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FolderBrowserDialog saveBrowse;
        private System.Windows.Forms.Button saveToBtn;
        private System.Windows.Forms.ProgressBar progBar;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ListBox critBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button addCritBtn;
		private System.Windows.Forms.Button delCritBtn;
		private System.Windows.Forms.Button addAlCritBtn;
        private System.Windows.Forms.Button dellCritBtn;
		private System.Windows.Forms.Label CompressLabel;
		private System.Windows.Forms.Label ProgressLabel;
		private System.Windows.Forms.Button stopButton;
    }
}

