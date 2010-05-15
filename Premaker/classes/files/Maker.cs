using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using com.jds.Premaker.classes.forms;
using log4net;
using zlib.Zip;

namespace com.jds.Premaker.classes.files
{
    public class Maker
    {
        private readonly String PASSWORD = "afsf325cf6y34g6a5frs4cf5";
        private readonly int COMPRESS_LEVEL = 9;
        private readonly int REVISION = 1;

        #region Instance & Variables

        public static Maker _instance;
        private static ILog _log = LogManager.GetLogger(typeof(Maker));

        private List<ListFile> _files = new List<ListFile>();

        public static Maker Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Maker();
                }

                return _instance;
            }
        }

        #endregion

        #region make
        public void make()
        {
            if (_files.Count == 0)
            {
                _log.Info("Files == 0");
                MessageBox.Show("Files is none", "Premaker", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            if (!Directory.Exists(DescriptionDirectory))
            {
                Directory.CreateDirectory(DescriptionDirectory);
            }

            FileInfo listZIP = new FileInfo(DescriptionDirectory + "/list.zip");
            ZipOutputStream zipListStream = new ZipOutputStream(listZIP.Create());
            zipListStream.SetLevel(COMPRESS_LEVEL); //уровень компресии
            zipListStream.Password = PASSWORD; //пароль

            ZipEntry listEntry = new ZipEntry("list.lst");
            listEntry.DateTime = DateTime.Now;
            zipListStream.PutNextEntry(listEntry);

            writeString(zipListStream, "#Revision:" + REVISION + "\n"); //пишем номер ревизии

            ChooseForm.Instance.FormAsGameFiles.onStart(); //прогресс бар включаем
            ChooseForm.Instance.FormAsGameFiles.disableItems(false); //отключаем что б не изменилось

            for (int i = 0; i < Items.Count; i++) //листаем
            {
                if (Break)
                {
                    ChooseForm.Instance.FormAsGameFiles.setStatus("Break");
                    ChooseForm.Instance.FormAsGameFiles.setValue(0);
                    return;
                }

                lock (zipListStream)
                {

                    ListFile item = Items[i];

                    try
                    {
                        if (!item.Exists())
                        {
                            _log.Info("WTF source file is deleted ??. File: " + item.SourceDirectoryName + item.FileName);
                            continue;
                        }

                        if (item.FileType != ListFileType.DELETE)
                        {
                            item.validateDirectoryes();

                            FileStream sourceStream = item.CreateSource();
                            FileStream descriptionStream = item.CreateDescription();

                            if (sourceStream == null || descriptionStream == null)
                            {
                                _log.Info("WTF streams NULL???. File: " + item.FileName);
                            }

                            ChooseForm.Instance.FormAsGameFiles.setStatus("Compressing " + item.FileName);

                            ZipOutputStream zipDescriptionStream = new ZipOutputStream(descriptionStream);
                                //открываем стрим
                            zipDescriptionStream.SetLevel(COMPRESS_LEVEL);
                            zipDescriptionStream.Password = PASSWORD;

                            ZipEntry entry = new ZipEntry(item.EntryName.Replace(".zip", ""));
                            entry.DateTime = item.SourceTime();
                            zipDescriptionStream.PutNextEntry(entry);

                            byte[] bytes = new byte[sourceStream.Length];
                            int readLen = sourceStream.Read(bytes, 0, bytes.Length);
                            sourceStream.Close();

                            if (readLen != bytes.Length)
                            {
                                _log.Info("WTF source file is not compled read??. File: " + item.FileName);
                                continue;
                            }

                            zipDescriptionStream.Write(bytes, 0, readLen);

                            zipDescriptionStream.Finish();
                            zipDescriptionStream.Close();
                        }

                        writeString(zipListStream, item.FileName + "\t" + item.FileType + "\t" + item.MD5 + "\n");
                    }
                    catch (Exception e)
                    {
                        _log.Info("Exception: file - " + item.FileName, e);
                    }
                    finally
                    {
                        int p = ((100 * i) / Items.Count);
                        if (p > 100)
                            p = 100;
                        ChooseForm.Instance.FormAsGameFiles.setValue(p);
                    }
                }
            }

            zipListStream.Finish();
            zipListStream.Close();

            ChooseForm.Instance.FormAsGameFiles.setStatus("Done");
            ChooseForm.Instance.FormAsGameFiles.disableItems(true);
            ChooseForm.Instance.FormAsGameFiles.onEnd();
        }

        public void writeString(Stream stream, String text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            stream.Write(bytes, 0, bytes.Length);
        }

        #endregion

        #region Properties

        public bool Break
        {
            get;
            set;
        }

        public List<ListFile> Items
        {
            get
            {
                return _files;
            }
        }

        public String DescriptionDirectory
        {
            get;
            set;
        }
        #endregion

        #region Info
        internal void InfoItems()
        {
            lock (_files)
            {
                foreach (ListFile f in _files)
                {
                    _log.Info("File: " + f.SourceDirectoryName + f.FileName + " critical: " + f.FileType + " exists: " + f.Exists());
                }
            }
        }
        #endregion
    }
}