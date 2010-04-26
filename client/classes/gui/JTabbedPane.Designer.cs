namespace com.jds.AWLauncher.classes.gui
{
	partial class JTabbedPane
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
            this.tabPage = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // tabPage
            // 
            this.tabPage.BackColor = System.Drawing.Color.Transparent;
            this.tabPage.ForeColor = System.Drawing.Color.Transparent;
            this.tabPage.Location = new System.Drawing.Point(0, 0);
            this.tabPage.Name = "tabPage";
            this.tabPage.Size = new System.Drawing.Size(102, 303);
            this.tabPage.TabIndex = 1;
            // 
            // JTabbedPane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tabPage);
            this.Name = "JTabbedPane";
            this.Size = new System.Drawing.Size(400, 306);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel tabPage;


	}
}
