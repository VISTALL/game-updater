using System;
using System.Drawing;
using System.Windows.Forms;
using com.jds.GUpdater.classes.date;
using com.jds.GUpdater.classes.events;

namespace com.jds.GUpdater.classes.gui
{
    public class RSSItem : UserControl
    {
        private Label _dateLabel;
        private Label _newsLabel;

        public RSSItem()
        {
            InitializeComponent();

            EventHandlers.Register(_newsLabel);
        }

        public void setLink(String g)
        {
            _newsLabel.Tag = g;
        }

        public void setNews(String news)
        {
            _newsLabel.Text = news;
        }

        public void setDate(String st)
        {
            DateTime time;
            if (Rfc822DateTime.TryParse(st, out time))
            {
                st = time.ToString("dd.MM.yy");
                _dateLabel.Text = "[" + st + "]";
            }
            else
                _dateLabel.Text = "[Incorrect date]";
        }

        public void doSize(Size s)
        {
            Size = s;
            _newsLabel.Width = s.Width;
        }

        private void InitializeComponent()
        {
            _dateLabel = new Label();
            _newsLabel = new Label();
            SuspendLayout();
            // 
            // _dateLabel
            // 
            _dateLabel.AutoSize = true;
            _dateLabel.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((204)));
            _dateLabel.ForeColor = Color.Firebrick;
            _dateLabel.Location = new Point(3, 4);
            _dateLabel.Name = "_dateLabel";
            _dateLabel.Size = new Size(59, 13);
            _dateLabel.TabIndex = 0;
            _dateLabel.Text = "[01.01.01]";
            // 
            // _newsLabel
            // 
            _newsLabel.BackColor = Color.Transparent;
            _newsLabel.Cursor = Cursors.Hand;
            _newsLabel.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((204)));
            _newsLabel.ForeColor = SystemColors.AppWorkspace;
            _newsLabel.Location = new Point(64, 2);
            _newsLabel.Name = "_newsLabel";
            _newsLabel.Size = new Size(250, 16);
            _newsLabel.TabIndex = 1;
            _newsLabel.Text = "NO NEWS";
            _newsLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // RSSItem
            // 
            BackColor = SystemColors.ActiveCaptionText;
            Controls.Add(_newsLabel);
            Controls.Add(_dateLabel);
            Name = "RSSItem";
            Size = new Size(316, 20);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}