using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Xml;
using com.jds.AWLauncher.classes.language;
using com.jds.AWLauncher.classes.language.enums;

namespace com.jds.AWLauncher.classes.gui
{
    public struct RSSItem2
    {
        internal String date;
        internal String news;
        internal String link;
        internal int x;
        internal int y;
        internal int width;
        internal int height;
    }

    public class RSSPanel : UserControl
    {
        private readonly RSSItem2?[] _items = new RSSItem2?[8];

        private WebClient client;
        private XmlNode nodeChannel;
        private XmlNode nodeItem;
        private XmlNode nodeRss;
        private XmlDocument rssDoc;
        private XmlTextReader rssReader;

        public RSSPanel()
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);

            URL = "http://awars.net/news/rss.xml";

            MouseMove += RSSPanel_MouseMove;
        }

        void RSSPanel_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        //[Browsable(false)]
        public String URL { get; set; }

        #region RefreshNews

        public void RefreshNews(Label l)
        {
            for (int i = 0; i < _items.Length; i ++ )
            {
                _items[i] = null;
            }

           Invalidate();

            l.Text = LanguageHolder.Instance()[WordEnum.PLEASE_WAIT];
            l.ForeColor = Color.FromArgb(157, 138, 113);
            l.Visible = true;

            client = new WebClient();
            client.DownloadDataAsync(new Uri(URL), l);
            client.DownloadDataCompleted += cl_DownloadDataCompleted;
        }

        private void cl_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            var lab = e.UserState as Label;

            if (e.Cancelled)
            {
                if (lab != null)
                {
                    lab.Text = LanguageHolder.Instance()[WordEnum.CANCEL_BY_USER];
                }
                return;
            }

            if (e.Error != null)
            {
                if (lab != null)
                {
                    lab.Text = LanguageHolder.Instance()[WordEnum.PROBLEM_WITH_INTERNET];
                }
                return;
            }
            byte[] data = e.Result;

            RefreshNews0(lab, data);
        }

        private void RefreshNews0(Label l, byte[] st)
        {
            rssReader = new XmlTextReader(new MemoryStream(st));
            rssDoc = new XmlDocument();
            rssDoc.Load(rssReader); //грузим док  

            // Loop for the <rss> tag
            for (int i = 0; i < rssDoc.ChildNodes.Count; i++)
            {
                // If it is the rss tag
                if (rssDoc.ChildNodes[i].Name == "rss")
                {
                    // <rss> tag found
                    nodeRss = rssDoc.ChildNodes[i];
                }
            }

            // Loop for the <channel> tag
            for (int i = 0; i < nodeRss.ChildNodes.Count; i++)
            {
                // If it is the channel tag
                if (nodeRss.ChildNodes[i].Name == "channel")
                {
                    // <channel> tag found
                    nodeChannel = nodeRss.ChildNodes[i];
                }
            }

            int readed = 0;

            // листаем
            for (int i = 1; i <= nodeChannel.ChildNodes.Count; i++)
            {
                if (readed == _items.Length) //list size
                    break;

                // находим итем новости
                if (nodeChannel.ChildNodes[i] != null && nodeChannel.ChildNodes[i].Name == "item")
                {
                    nodeItem = nodeChannel.ChildNodes[i];

                    RSSItem2 item = new RSSItem2();

                    item.date = nodeItem["pubDate"].InnerText;
                    item.news = nodeItem["title"].InnerText;
                    item.link = nodeItem["link"].InnerText;

                    _items[readed] = item;
                    readed++;
                }
            }
           
            l.Text = LanguageHolder.Instance()[WordEnum.PLEASE_WAIT];
            l.Visible = false;

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            SolidBrush b = new SolidBrush(BackColor);
            e.Graphics.FillRectangle(b, e.ClipRectangle); //заливаем фон

            int paintCount = 0;
            for(int i = 0; i < _items.Length; i ++)
            {
                RSSItem2? itemv = _items[i];
                if(itemv == null)
                {
                    continue;
                }
                
                const int height = 20;
                const int diff = 3;
                String formatDate = "[{0}]";
                Font f = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, (204));
                ;

                RSSItem2 item = itemv.Value;
                item.x = 0;
                item.y = paintCount * height + diff;
                item.width = Width;
                item.height = height;

                RectangleF t = new RectangleF(item.x, item.y, item.width, item.height);
                e.Graphics.FillRectangle(b, t);

                e.Graphics.DrawString(String.Format(formatDate, item.date),  f, new SolidBrush(Color.White), t);
                paintCount++;
            }
        }
       
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //base.OnPaintBackground(e);
        }

        #endregion

        public void Close()
        {
            client.CancelAsync();
        }
    }
}