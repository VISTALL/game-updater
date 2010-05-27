using com.jds.AWLauncher.classes.gui;

namespace com.jds.AWLauncher.classes.games.gui
{
	partial class SimpleGamePanel
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
            this.rssPanel1 = new com.jds.AWLauncher.classes.gui.RSSPanel();
            this.StatusRSS = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // rssPanel1
            // 
            this.rssPanel1.BackColor = System.Drawing.Color.Transparent;
            this.rssPanel1.Location = new System.Drawing.Point(47, 4);
            this.rssPanel1.Name = "rssPanel1";
            this.rssPanel1.Size = new System.Drawing.Size(353, 167);
            this.rssPanel1.TabIndex = 6;
            this.rssPanel1.URL = "http://awars.net/news/rss.xml ";
            // 
            // StatusRSS
            // 
            this.StatusRSS.AutoSize = true;
            this.StatusRSS.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.StatusRSS.Location = new System.Drawing.Point(48, 6);
            this.StatusRSS.Name = "StatusRSS";
            this.StatusRSS.Size = new System.Drawing.Size(0, 13);
            this.StatusRSS.TabIndex = 8;
            // 
            // SimpleGamePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.StatusRSS);
            this.Controls.Add(this.rssPanel1);
            this.Name = "SimpleGamePanel";
            this.Size = new System.Drawing.Size(400, 300);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private RSSPanel rssPanel1;
        private System.Windows.Forms.Label StatusRSS;

	}
}
