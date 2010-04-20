//#define NET_3_5

#region Usage
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
#if NET_3_5
using Microsoft.WindowsAPICodePack.Taskbar;
#endif
using com.jds.Builder.classes.files;
using com.jds.Builder.classes.utils;
#endregion

namespace com.jds.Builder.classes.forms
{  
    
    public partial class MainForm : Form
    {
        
        private String PATH; //выбраный путь
        private String TO_SAVE; //путь для сохранения
		private Thread thread;

        #region Instance & Constructor
        private static MainForm _instance;

        public static MainForm Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MainForm();
                }

                return _instance;
            }
        }

        private MainForm()
        {
			//отключаем проверку на кросс тридовые свзяи
			System.Windows.Forms.Form.CheckForIllegalCrossThreadCalls = false;

			InitializeComponent();
        }

        private void MFormClosed(object sender, FormClosedEventArgs e)
        {
            if (thread != null)
                thread.Interrupt();
        }

        #endregion

        #region Грузит имья файлов с директорий
        public bool read(String path)
        {		
			String[] files = Directory.GetFiles(path); //берет список файлов

            foreach (String file in files) //листаем файлы
            {
				//убираем родительский адресс + меняеш хеши
                String fileName = file.Replace("\\", "/").Replace(PATH, ""); 
                
				listOfFiles.Items.Add(fileName);
            }

            String[] directories = Directory.GetDirectories(path); //берет список папок      
			foreach (String dir in directories) //листаем
            {
                read(dir);
            }

            return true;
        }
        #endregion

        #region Make Event
        private void button2_Click(object sender, EventArgs e)
        {
			thread = new Thread(new ThreadStart(threadmake));
			thread.Start();	
		}

		public void threadmake()
		{
            putToList();

            Maker.Instance.DescriptionDirectory = TO_SAVE;
            Maker.Instance.make();            
		}

		private void putToList()
		{
            lock (Maker.Instance.Items)
            {
                foreach (Object obj in toConvert.Items)
                {
                    ListFile file = new ListFile();
                    file.FileName = obj.ToString();
                    file.SourceDirectoryName = PATH; 
                    file.DescriptionDirectoryName = TO_SAVE;
                    file.IsCritical = false;
                    Maker.Instance.Items.Add(file);
                }
            }

            lock (Maker.Instance.Items)
            {
                foreach (Object obj in critBox.Items)
                {
                    ListFile file = new ListFile();
                    file.FileName = obj.ToString();
                    file.SourceDirectoryName = PATH;
                    file.DescriptionDirectoryName = TO_SAVE;
                    file.IsCritical = true;
                    Maker.Instance.Items.Add(file);
                }
            }
#if INFO_LIST
            Maker.Instance.InfoItems();
#endif
        }

        public void disableItems(bool e)
        {
            listOfFiles.Enabled = e;
            toConvert.Enabled = e;
            critBox.Enabled = e;
            stopButton.Enabled = !e;
        }

        public void setStatus(String te)
        {
            CompressLabel.Text = te;
        }

        #endregion 
      
        #region Progress Bar & Windows 7
        public void onStart()
        {
            progBar.Minimum = 0;
#if NET_3_5
            if (TaskbarManager.IsPlatformSupported)
            {
                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Normal);
            }
#endif
        }

        public void onEnd()
        {
            progBar.Minimum = 0;
            progBar.Maximum = 100;
            progBar.Value = 0;
#if NET_3_5
            if (TaskbarManager.IsPlatformSupported)
            {
                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress); 
            }
#endif
        }

        public void setValue(int v)
        {
            progBar.Value = v;
#if NET_3_5
            if (TaskbarManager.IsPlatformSupported)
            {
                TaskbarManager.Instance.SetProgressValue(v, 100);
            }
#endif
        }

        #endregion

        #region Open Directory / Make Directory

        private void button1_Click(object sender, EventArgs e)
		{
			if (folderBrowse.ShowDialog() != DialogResult.OK)
				return;

			String SelectedPath = folderBrowse.SelectedPath;
            StringUtil.slashes(SelectedPath, out SelectedPath);

            path.Text = SelectedPath;
            PATH = SelectedPath;

            Maker.Instance.Items.Clear();
			listOfFiles.Items.Clear();

			addButton.Enabled = true;
			removeAllBtn.Enabled = true;
			removeBtn.Enabled = true;
			addAllBtn.Enabled = true;
			clearButton.Enabled = true;
			saveToBtn.Enabled = true;
			addAlCritBtn.Enabled = true;
			addCritBtn.Enabled = true;
			delCritBtn.Enabled = true;
			dellCritBtn.Enabled = true; 
			
			read(PATH);
        }
        
        private void saveToBtn_Click(object sender, EventArgs e)
        {
            if (saveBrowse.ShowDialog() != DialogResult.OK)
                return;

            String SelectedPath = saveBrowse.SelectedPath;
            StringUtil.slashes(SelectedPath, out SelectedPath);
            Maker.Instance.DescriptionDirectory = TO_SAVE;
            TO_SAVE = SelectedPath;

            copyRoot.Text = SelectedPath;
            MakeBtn.Enabled = true;
        }

        #endregion

        #region Add/Add All to Normal List
        private void addButton_Click(object sender, EventArgs e)
        {
           // String file = (String)listOfFiles.SelectedItem;
			//if (file == null)
			//	return;
           // Object[] objects = listOfFiles.SelectedItems;

            lock (listOfFiles)
            {
                for (int i = 0; i < listOfFiles.SelectedItems.Count; i++)
                {
                    String file = (String)listOfFiles.SelectedItems[i];
                   
                    if (!toConvert.Items.Contains(file))
                    {
                        toConvert.Items.Add(file);
                    }
                }
            }            
        }

        private void addAllBtn_Click(object sender, EventArgs e)
        {
            foreach (Object st in listOfFiles.Items)
            {
                if (!toConvert.Items.Contains(st))
                    toConvert.Items.Add(st);
            }
        }
        #endregion

        #region Remove/Remove ALl from Normal List
        private void removeBtn_Click(object sender, EventArgs e)
        {
            lock (toConvert.SelectedItems)
            {
                for (int i = 0; i < toConvert.SelectedItems.Count; i++)
                {
                    toConvert.Items.Remove(toConvert.SelectedItems[i]);
                }
            }
        }

        private void removeAllBtn_Click(object sender, EventArgs e)
        {
            toConvert.Items.Clear();
        }
        #endregion

        #region Add/Add All to Critical List
        private void addCritBtn_Click(object sender, EventArgs e)
        {
            //String file = (String)toConvert.SelectedItem;
            //if (file == null)
            //	return;
            /// Object[] objects = ;

            lock (toConvert)
            {

                for (int i = 0; i < toConvert.SelectedItems.Count; i++)
                {
                    String file = (String)toConvert.SelectedItems[i];

                    if (!critBox.Items.Contains(file))
                    {
                        toConvert.Items.Remove(file);

                        critBox.Items.Add(file);
                    }
                }
            }
        }

        private void addAlCritBtn_Click(object sender, EventArgs e)
        {
            foreach (Object obj in toConvert.Items)
            {
                critBox.Items.Add(obj);
            }

            toConvert.Items.Clear();
        }
        #endregion

        #region Remove/Remove All from Crical List
        private void delCritBtn_Click(object sender, EventArgs e)
        {
            //String file = (String)critBox.SelectedItem;
            //if (file == null)
            //	return;

            lock (critBox)
            {
                for (int i = 0; i < critBox.SelectedItems.Count; i++)
                {
                    String file = (String)critBox.SelectedItems[i];

                    if (!toConvert.Items.Contains(file))
                    {
                        critBox.Items.Remove(file);
                        toConvert.Items.Add(file);
                    }
                }
            }
        }

        private void dellCritBtn_Click(object sender, EventArgs e)
        {
            foreach (Object obj in critBox.Items)
            {
                toConvert.Items.Add(obj);
            }

            critBox.Items.Clear();
        }
        #endregion

        #region Clear & Stop Click
        private void clearButton_Click(object sender, EventArgs e)
        {
            addButton.Enabled = false;
            removeAllBtn.Enabled = false;
            removeBtn.Enabled = false;
            addAllBtn.Enabled = false;
            MakeBtn.Enabled = false;
            clearButton.Enabled = false;
            saveToBtn.Enabled = false;
			addAlCritBtn.Enabled = false;
			addCritBtn.Enabled = false;
			delCritBtn.Enabled = false;
			dellCritBtn.Enabled = false;
            path.Text = "";
            listOfFiles.Items.Clear();
            toConvert.Items.Clear();
        }          


        private void stopButton_Click(object sender, EventArgs e)
		{
            Maker.Instance.Break = true;

            if (thread != null)
            {
                thread.Interrupt();
            }

            disableItems(true);
        }
        #endregion

    }
}
