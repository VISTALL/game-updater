using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using com.jds.Premaker.classes.files;
using com.jds.Premaker.classes.utiles;
using zlib.Zip;

namespace com.jds.Premaker.classes.forms
{
    public partial class AWFilesForm : Form
    {
        private readonly List<ListFile> _files = new List<ListFile>();

        public AWFilesForm()
        {
            InitializeComponent();

            _forlderBrowse.SelectedPath = ".";
        }

        public string SelectedPath { get; set; }

        public string DescriptionPath { get; set; }

        public string Version { get; set; }

        public string ServerVersion { get; set; }
       
        public void writeString(Stream stream, String text)
        {
            var bytes = Encoding.UTF8.GetBytes(text);
            stream.Write(bytes, 0, bytes.Length);
        }

        #region Open
        private void _openFolder_Click(object sender, EventArgs e)
        {
            if (_forlderBrowse.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            var path = _forlderBrowse.SelectedPath;
            StringUtil.slashes(path, out path);
            SelectedPath = path;
            read(SelectedPath);
            _makeTOBtn.Enabled = true;
            _pathBox.Text = path;

            var gExe = SelectedPath + "/AWLauncher.exe";
            if (!File.Exists(gExe))
            {
                MessageBox.Show("Not AWLauncher folder");
                return;
            }

            var ass = Assembly.LoadFile(gExe);
            _versionLabel.Text = ass.GetName().Version.ToString();
            Version = ass.GetName().Version.ToString();
        }
        #endregion

        #region Description Button
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
        #endregion

        #region Make Buttom
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

                                    var listEntry = new ZipEntry("list.lst") {DateTime = DateTime.Now};
                                    listStream.PutNextEntry(listEntry);

                                    onStart();

                                    writeString(listStream, "#Version: " + Version + "\n");

                                    for (var i = 0; i < _files.Count; i++) //листаем
                                    {
                                        var item = _files[i];

                                        item.validateDirectoryes();

                                        FileStream sourceStream = item.CreateSource();
                                        FileStream descriptionStream = item.CreateDescription();

                                        var zipDescriptionStream = new ZipOutputStream(descriptionStream);
                                        //открываем стрим
                                        zipDescriptionStream.SetLevel(9);
                                        // zipDescriptionStream.Password = PASSWORD;

                                        var entry = new ZipEntry(item.EntryName.Replace(".zip", ""))
                                                        {DateTime = item.SourceTime()};
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

                                        var p = ((100*i)/_files.Count);
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

        #endregion
               
        #region Progress Bar
        public void onStart()
        {
            ThreadStart a = delegate
                                {
                                    _progressBar.Visible = true;
                                    _progressBar.Value = 0;

                                 /*   if (TaskbarManager.IsPlatformSupported)
                                    {
                                        TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Normal);
                                    }*/
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

                                /*    if (TaskbarManager.IsPlatformSupported)
                                    {
                                        TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);
                                    }*/
                                };

            Invoke(a);
        }

        public void setValue(int v)
        {
            ThreadStart a = delegate
                                {
                                    _progressBar.Value = v;

                                  /*  if (TaskbarManager.IsPlatformSupported)
                                    {
                                        TaskbarManager.Instance.SetProgressValue(v, 100);
                                    }*/
                                };
            Invoke(a);
        }

        #endregion

        #region Get Version From Web
        public void GetVersionFromWeb()
        {
            var webClient = new WebClient();
            webClient.DownloadDataCompleted += webClient_DownloadDataCompleted;
            webClient.DownloadDataAsync(new Uri("http://jdevelopstation.com/awlauncher/list.zip"));
        }

        void webClient_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            if(e.Cancelled || e.Error != null)
            {
                _serverVersion.Text = "Undefined";
                Enabled = true;
                return;
            }

            GoNexStep(e.Result);
        }

        public void GoNexStep(byte[] data)
        {
            var zipStream = new ZipInputStream(new MemoryStream(data));

            byte[] array = null;

            if ((zipStream.GetNextEntry()) != null)
            {
                array = new byte[zipStream.Length];
                zipStream.Read(array, 0, array.Length);
            }

            zipStream.Close();

            if (array == null)
            {
                _serverVersion.Text = "Undefined";
                Enabled = true;
                return;
            }

            string encodingBytes = Encoding.UTF8.GetString(array);
            string[] lines = encodingBytes.Split('\n');

            foreach (string line in lines)
            {
                if (line.Trim().Equals(""))
                    continue;

                if (line.StartsWith("#Version:"))
                {
                    ServerVersion = line.Replace("#Version:", "").Trim();
                    _serverVersion.Text = ServerVersion;
                }
            }
            Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _serverVersion.Text = "Loading...";
            Enabled = false;
            GetVersionFromWeb();
        }
        #endregion

        #region List Actions
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
            if (_allFiles.SelectedItem != null && !_filesList.Items.Contains(_allFiles.SelectedItem))
            {
                _filesList.Items.Add(_allFiles.SelectedItem);
            }
        }

        private void _filesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_filesList.SelectedItem != null) _filesList.Items.Remove(_filesList.SelectedItem);
        }
        #endregion
    }
}