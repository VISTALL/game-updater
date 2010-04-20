using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using com.jds.Premaker.classes.files;
using com.jds.Premaker.classes.utiles;
using Microsoft.WindowsAPICodePack.Taskbar;
using zlib.Zip;

namespace com.jds.Premaker.classes.forms
{
    public partial class GUFilesForm : Form
    {
        private readonly List<ListFile> _files = new List<ListFile>();

        public GUFilesForm()
        {
            InitializeComponent();

            _forlderBrowse.SelectedPath = ".";
        }

        public string SelectedPath { get; set; }

        public string DescriptionPath { get; set; }

        private void _openFolder_Click(object sender, EventArgs e)
        {
            if (_forlderBrowse.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            string path = _forlderBrowse.SelectedPath;
            StringUtil.slashes(path, out path);
            SelectedPath = path;
            read(SelectedPath);
            _makeTOBtn.Enabled = true;
            _pathBox.Text = path;

            string gExe = SelectedPath + "/GUpdater.exe";
            if (!File.Exists(gExe))
            {
                MessageBox.Show("Not GUpdater folder");
                return;
            }

            Assembly ass = Assembly.LoadFile(gExe);
            _versionLabel.Text = ass.GetName().Version.ToString();
        }

        private void _makeTOBtn_Click(object sender, EventArgs e)
        {
            if (_saveFolderChoose.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string path = _saveFolderChoose.SelectedPath;
            StringUtil.slashes(path, out path);

            DescriptionPath = path;
            _decsPath.Text = DescriptionPath;

            _startBtn.Enabled = true;
        }

        private void _startBtn_Click(object sender, EventArgs e)
        {
            ThreadStart a = delegate
                                {
                                    putToList();

                                    if (!Directory.Exists(SelectedPath))
                                    {
                                        Directory.CreateDirectory(SelectedPath);
                                    }
                                    string listFile = DescriptionPath + "/list.zip";
                                    if (File.Exists(listFile))
                                    {
                                        File.Delete(listFile);
                                    }
                                    var list = new FileInfo(listFile);
                                    var listStream = new ZipOutputStream(list.Create());

                                    var listEntry = new ZipEntry("list.lst");
                                    listEntry.DateTime = DateTime.Now;
                                    listStream.PutNextEntry(listEntry);

                                    onStart();

                                    for (int i = 0; i < _files.Count; i++) //листаем
                                    {
                                        ListFile item = _files[i];

                                        item.validateDirectoryes();

                                        FileStream sourceStream = item.CreateSource();
                                        FileStream descriptionStream = item.CreateDescription();

                                        var zipDescriptionStream = new ZipOutputStream(descriptionStream);
                                        //открываем стрим
                                        zipDescriptionStream.SetLevel(9);
                                        // zipDescriptionStream.Password = PASSWORD;

                                        var entry = new ZipEntry(item.EntryName.Replace(".zip", ""));
                                        entry.DateTime = item.SourceTime();
                                        zipDescriptionStream.PutNextEntry(entry);

                                        var bytes = new byte[sourceStream.Length];
                                        int readLen = sourceStream.Read(bytes, 0, bytes.Length);
                                        sourceStream.Close();

                                        if (readLen != bytes.Length)
                                        {
                                            continue;
                                        }

                                        zipDescriptionStream.Write(bytes, 0, readLen);

                                        zipDescriptionStream.Finish();
                                        zipDescriptionStream.Close();

                                        writeString(listStream, item.FileName + "\t" + item.MD5 + "\n");

                                        int p = ((100*i)/_files.Count);
                                        if (p > 100)
                                            p = 100;

                                        setValue(p);
                                    }

                                    listStream.Finish();
                                    listStream.Close();

                                    onEnd();
                                };

            new Thread(a).Start();
        }

        public void onStart()
        {
            ThreadStart a = delegate
                                {
                                    _progressBar.Visible = true;
                                    _progressBar.Value = 0;

                                    if (TaskbarManager.IsPlatformSupported)
                                    {
                                        TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Normal);
                                    }
                                };
            Invoke(a);
        }

        public void onEnd()
        {
            ThreadStart a = delegate
                                {
                                    setValue(100);

                                    MessageBox.Show("Finish");

                                    _progressBar.Visible = false;

                                    if (TaskbarManager.IsPlatformSupported)
                                    {
                                        TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);
                                    }
                                };

            Invoke(a);
        }

        public void setValue(int v)
        {
            ThreadStart a = delegate
                                {
                                    _progressBar.Value = v;

                                    if (TaskbarManager.IsPlatformSupported)
                                    {
                                        TaskbarManager.Instance.SetProgressValue(v, 100);
                                    }
                                };
            Invoke(a);
        }  

        public void writeString(Stream stream, String text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            stream.Write(bytes, 0, bytes.Length);
        }

        public void putToList()
        {
            foreach (object obj in _filesList.Items)
            {
                var file = new ListFile
                               {
                                   FileName = obj.ToString(),
                                   SourceDirectoryName = SelectedPath,
                                   DescriptionDirectoryName = DescriptionPath
                               };
                _files.Add(file);
            }
        }

        public void read(String path)
        {
            string[] files = Directory.GetFiles(path); //берет список файлов

            foreach (string file in files) //листаем файлы
            {
                //убираем родительский адресс + меняеш хеши
                string fileName = file.Replace("\\", "/").Replace(SelectedPath, "");

                _allFiles.Items.Add(fileName);
            }

            string[] directories = Directory.GetDirectories(path); //берет список папок      
            foreach (string dir in directories) //листаем
            {
                read(dir);
            }
        }

        private void _allFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_filesList.Items.Contains(_allFiles.SelectedItem))
            {
                _filesList.Items.Add(_allFiles.SelectedItem);
            }
        }

        private void _filesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            _filesList.Items.Remove(_filesList.SelectedItem);
        }
    }
}