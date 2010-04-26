using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Xml;
using com.jds.AWLauncher.classes.language;
using com.jds.AWLauncher.classes.language.enums;

namespace com.jds.AWLauncher.classes.gui
{
    public class RSSPanel : UserControl
    {
        private readonly List<RSSItem> _items = new List<RSSItem>();
        private WebClient client;
        private XmlNode nodeChannel;
        private XmlNode nodeItem;
        private XmlNode nodeRss;
        private XmlDocument rssDoc;
        private XmlTextReader rssReader;

        public RSSPanel()
        {
            InitializeComponent();
            URL = "http://ru.aionwars.com/rss.xml";

            //CheckForIllegalCrossThreadCalls = false;
        }

        //[Browsable(false)]
        public String URL { get; set; }

        #region RefreshNews

        private const int SIZE = 8;

        public void RefreshNews(Label l)
        {
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

            // листаем
            for (int i = 1; i <= nodeChannel.ChildNodes.Count; i++)
            {
                if (_items.Count == SIZE) //list size
                    break;

                // находим итем новости
                if (nodeChannel.ChildNodes[i].Name == "item")
                {
                    nodeItem = nodeChannel.ChildNodes[i];

                    var item = new RSSItem {Visible = false, BackColor = Color.Black};

                    item.setDate(nodeItem["pubDate"].InnerText);
                    item.setNews(nodeItem["title"].InnerText);
                    item.setLink(nodeItem["link"].InnerText);

                    int y = getNextY(item.Height);

                    item.doSize(new Size(Size.Width, item.Height));
                    item.Location = new Point(0, y);

                    _items.Add(item);
                    Controls.Add(item);
                }
            }

            foreach (RSSItem i in _items)
            {
                i.Visible = true;
            }

            l.Text = LanguageHolder.Instance()[WordEnum.PLEASE_WAIT];
            l.Visible = false;
        }

        private int getNextY(int l)
        {
            if (_items.Count == 0)
                return 0;

            return _items.Count*l;
        }

        #endregion

        public void Close()
        {
            client.CancelAsync();
        }

        #region InitializeComponent

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // RSSPanel
            // 
            Name = "RSSPanel";
            Size = new Size(591, 347);
            ResumeLayout(false);
        }

        #endregion
    }
}