using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using com.jds.Premaker.classes.files;
using com.jds.Premaker.classes.gui.clistview;
using com.jds.Premaker.classes.utiles;
using com.jds.Premaker.classes.windows.windows7;

namespace com.jds.Premaker.classes.forms
{
    public partial class GameFilesForm : Form
    {
        #region Constructor
        public GameFilesForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;            

            foreach (object obj in Enum.GetValues(typeof(ListFileType)))
            {
                ToolStripMenuItem item = new ToolStripMenuItem
                                             {
                                                 Name = obj.ToString(),
                                                 Text = obj.ToString(),
                                                 Tag = obj
                                             };
                item.Click += item_Click;
                markAsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { item });
            }
        }
        #endregion

        #region List Files
        void item_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
      
            if(_listFiles.SelectedItems == null)
            {
                return;
            }

            foreach(var Sitem in _listFiles.SelectedItems)
            {
                CListItem selected_item = Sitem as CListItem;
                if (selected_item != null && item != null)
                {
                    ComboBox box = selected_item.SubItems[0].Control as ComboBox;
                    if (box != null)
                    {
                        box.SelectedItem = item.Tag;
                    }
                }
            }
        }

        public void addFile(string fileName)
        {
            CListSubItem comboBox = new CListSubItem();
            CListSubItem fileNameLabel = new CListSubItem();

            //
            ComboBox box = new ComboBox();
            foreach (object obj in Enum.GetValues(typeof(ListFileType)))
            {
                box.Items.Add(obj);    
            }
            box.DropDownStyle = ComboBoxStyle.DropDownList;
            box.SelectedIndex = 0;
            comboBox.Control = box;

            //
            fileNameLabel.Text = fileName;


            CListItem item = new CListItem();
            item.SubItems.Add(comboBox);
            item.SubItems.Add(fileNameLabel);

            _listFiles.Items.Add(item);
        }
        #endregion

        #region Грузит и выгружает файлы со списка ил директории
        public void readDirectory(String dir)
        {
            ThreadStart dele = delegate
                                   {
                                       Windows7Taskbar.SetProgressState(Handle, ThumbnailProgressState.Indeterminate);
                                       Enabled = false; 
                                       
                                       List<String> list = new List<string>();

                                       read(list, dir);

                                       foreach (string s in list)
                                       {
                                           addFile(s);
                                       }

                                       Windows7Taskbar.SetProgressState(Handle, ThumbnailProgressState.NoProgress);
                                       Enabled = true;
                                   };
            Thread t = new Thread(dele);
            t.Start();
        }

        public bool read(List<String> list, String path)
        {
            String[] files = Directory.GetFiles(path); //берет список файлов

            foreach (String file in files) //листаем файлы
            {
                //убираем родительский адресс + меняеш хеши
                String fileName = file.Replace("\\", "/").Replace(Path, "");

                list.Add(fileName);
            }

            String[] directories = Directory.GetDirectories(path); //берет список папок      
            foreach (String dir in directories) //листаем
            {
                read(list, dir);
            }

            return true;
        }

        private void putToList()
        {
            Maker.Instance.Items.Clear();

            foreach (Object obj in _listFiles.Items)
            {
                CListItem item = obj as CListItem;
                if(item == null)
                {
                    continue;
                }

                ListFileType type = (ListFileType)((ComboBox)item.SubItems[0].Control).SelectedItem;
                String fileName = item.SubItems[1].Text;

                if(type == ListFileType.IGNORED)
                    continue;

                ListFile file = new ListFile();
                file.FileName = fileName;
                file.SourceDirectoryName = Path;
                file.DescriptionDirectoryName = MakeTo;
                file.FileType = type;

                Maker.Instance.Items.Add(file);
            }
        }

        #endregion

        #region Buttons Events 
        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowse.ShowDialog() != DialogResult.OK)
                return;

            String SelectedPath = folderBrowse.SelectedPath;
            StringUtil.slashes(SelectedPath, out SelectedPath);

            makeToFolderToolStripMenuItem.Enabled = true;

            _selectedPath.Text = SelectedPath;
            Path = SelectedPath;

            readDirectory(Path);
        }
        
        private void makeToFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveBrowse.ShowDialog() != DialogResult.OK)
                return;

            String SelectedPath = saveBrowse.SelectedPath;
            StringUtil.slashes(SelectedPath, out SelectedPath);

            makeToolStripMenuItem.Enabled = true;

            MakeTo = SelectedPath;
            _makeTo.Text = SelectedPath;
        }

        private void makeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThreadStart d = delegate
                                {
                                    putToList();

                                    Maker.Instance.DescriptionDirectory = MakeTo;
                                    Maker.Instance.make();  
                                };
            
            Thread thread = new Thread(d);
			thread.Start();	
        }
             
      
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region Properties
        public String Path
        {
            get; set;
        }

        public String MakeTo
        {
            get;
            set;
        }
        #endregion

        #region Progress Bar & Windows 7
        public void onStart()
        {
            _totalProgress.Minimum = 0;
            Windows7Taskbar.SetProgressState(Handle, ThumbnailProgressState.Normal);
        }

        public void onEnd()
        {
            _totalProgress.Minimum = 0;
            _totalProgress.Maximum = 100;
            _totalProgress.Value = 0;
            
            Windows7Taskbar.SetProgressState(Handle, ThumbnailProgressState.NoProgress);

            MessageBox.Show(this, "Done", "Premaker", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void setValue(int v)
        {
            _totalProgress.Value = v;
            
            Windows7Taskbar.SetProgressValue(Handle,  v, 100);
        }

        #endregion

        #region Status
        public void setStatus(String te)
        {
            _statusLabel.Text = te;
        }

        public void disableItems(bool e)
        {
            Enabled = e;
        }
        #endregion
    }
}
