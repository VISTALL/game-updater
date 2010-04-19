using System;
using System.Collections.Generic;
using System.Text;
using log4net;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using com.jds.Builder.classes.forms;

namespace com.jds.Builder.classes.files
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

            MainForm.Instance.onStart(); //прогресс бар включаем
            MainForm.Instance.disableItems(false); //отключаем что б не изменилось

            for (int i = 0; i < Items.Count; i++) //листаем
            {
                if (Break)
                {
                    MainForm.Instance.setStatus("Break");
                    MainForm.Instance.setValue(0);
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

                        item.validateDirectoryes();

                        FileStream sourceStream = item.CreateSource();
                        FileStream descriptionStream = item.CreateDescription();

                        if (sourceStream == null || descriptionStream == null)
                        {
                            _log.Info("WTF streams NULL???. File: " + item.FileName);
                        }

                        MainForm.Instance.setStatus("Compressing " + item.FileName);

                        ZipOutputStream zipDescriptionStream = new ZipOutputStream(descriptionStream); //открываем стрим
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

                        writeString(zipListStream, item.FileName + "\t" + item.IsCritical + "\t" + item.MD5 + "\n");
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
                        MainForm.Instance.setValue(p);
                    }
                }
            }

            zipListStream.Finish();
            zipListStream.Close();

            MainForm.Instance.disableItems(true);
            MainForm.Instance.onEnd();
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
                    _log.Info("File: " + f.SourceDirectoryName + f.FileName + " critical: " + f.IsCritical + " exists: " + f.Exists());
                }
            }
        }
        #endregion
    }
}
