using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using com.jds.Premaker.classes.files;
using com.jds.Premaker.classes.utiles;
using ICSharpCode.SharpZipLib.BZip2;
using ICSharpCode.SharpZipLib.Tar;
using zlib.BZip2;

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

        private void _openFolder_Click(object sender, EventArgs e)
        {
            if(_forlderBrowse.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            var path = _forlderBrowse.SelectedPath;
            StringUtil.slashes(path, out path);
            SelectedPath = path;
            read(SelectedPath);
            _makeTOBtn.Enabled = true;
            _pathBox.Text = path;

            var gExe = SelectedPath + "/GUpdater.exe";
            if (!File.Exists(gExe))
            {
                MessageBox.Show("Not GUpdater folder");
                return;
            }

            var ass = Assembly.LoadFile(gExe);
            _versionLabel.Text = ass.GetName().Version.ToString();
        }

        private void _makeTOBtn_Click(object sender, EventArgs e)
        {
            if (_saveFolderChoose.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            var path = _saveFolderChoose.SelectedPath;
            StringUtil.slashes(path, out path);

            DescriptionPath = path;
            _decsPathLabel.Text = DescriptionPath;

            _startBtn.Enabled = true;
        }

        private void _startBtn_Click(object sender, EventArgs e)
        {
            putToList();

            if (!Directory.Exists(SelectedPath))
            {
                Directory.CreateDirectory(SelectedPath);
            }
            var listFile = DescriptionPath + "/list.tar.bz2";
            if(File.Exists(listFile))
            {
                File.Delete(listFile);
            }
            var list = new FileInfo(listFile);

          //  var tarOutputStream = new TarOutputStream(list.Create());

         //   var tarEntry = new TarEntry(new TarHeader() {Name =  "list.lst"});
       //     tarOutputStream.PutNextEntry(tarEntry);

             for (var i = 0; i < _files.Count; i++) //листаем
             {
                 var item = _files[i];

                 item.validateDirectoryes();

                 var sourceStream = item.CreateSource();
                 var descriptionStream = item.CreateDescription();

                 BZip2OutputStream bZip2OutputStream = new BZip2OutputStream(descriptionStream, 9);
                 TarArchive archive = TarArchive.CreateOutputTarArchive(bZip2OutputStream);

                 TarEntry entry = TarEntry.CreateTarEntry(item.EntryName.Replace(".tar.bz2", ""));

                 entry.GetFileTarHeader(new TarHeader(), item.SourceDirectoryName + item.FileName);
                 archive.WriteEntry(entry, true);

                 var bytes = new byte[sourceStream.Length];
                 var readLen = sourceStream.Read(bytes, 0, bytes.Length);
                 sourceStream.Close();

                 bZip2OutputStream.Write(bytes, 0, readLen);
                 
                // var tarDescriptionStream = new TarOutputStream(descriptionStream); //открываем стрим
                // var bZip2OutputStream = new BZip2OutputStream(tarDescriptionStream);
                 
               //  var entry = new TarEntry(new TarHeader { Name = item.EntryName.Replace(".tar.bz2", "") });
                // tarDescriptionStream.PutNextEntry(entry);

                

                 //tarDescriptionStream.Write(bytes, 0, readLen);

                // tarDescriptionStream.Finish();
                 //tarDescriptionStream.Close();
                 //bZip2OutputStream.Close();

                // writeString(tarOutputStream, item.FileName + "\t" + item.MD5 + "\n");
                 archive.Close();
             }

           //  tarOutputStream.Finish();
          //   tarOutputStream.Close();
        }

        public void writeString(Stream stream, String text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            stream.Write(bytes, 0, bytes.Length);
        }

        public void putToList()
        {
            foreach(var obj in _filesList.Items)
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
            var files = Directory.GetFiles(path); //берет список файлов

            foreach (var file in files) //листаем файлы
            {
                //убираем родительский адресс + меняеш хеши
                var fileName = file.Replace("\\", "/").Replace(SelectedPath, "");

                _allFiles.Items.Add(fileName);
            }

            var directories = Directory.GetDirectories(path); //берет список папок      
            foreach (var dir in directories) //листаем
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

        public string SelectedPath
        {
            get; set;
        }

        public string DescriptionPath
        {
            get; set;
        }
    }
}
