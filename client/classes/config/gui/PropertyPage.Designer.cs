namespace com.jds.GUpdater.classes.config.gui
{
	partial class PropertyPage
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
            this._property = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // _property
            // 
            this._property.CommandsVisibleIfAvailable = false;
            this._property.Dock = System.Windows.Forms.DockStyle.Fill;
            this._property.Location = new System.Drawing.Point(0, 0);
            this._property.Name = "_property";
            this._property.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this._property.Size = new System.Drawing.Size(689, 320);
            this._property.TabIndex = 0;
            // 
            // PropertyPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._property);
            this.Name = "PropertyPage";
            this.Size = new System.Drawing.Size(689, 320);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PropertyGrid _property;
	}
}
