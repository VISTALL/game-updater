#region Usage

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using com.jds.AWLauncher.classes.forms;
using com.jds.AWLauncher.classes.games.propertyes;
using com.jds.AWLauncher.classes.language.enums;
using com.jds.AWLauncher.classes.listloader;
using com.jds.AWLauncher.classes.listloader.enums;
using com.jds.AWLauncher.classes.utils;
using com.jds.AWLauncher.classes.zip;
using log4net;

#endregion

namespace com.jds.AWLauncher.classes.task_manager.tasks
{
    public class AnalyzerTask : AbstractTask
    {
        #region Constructor 
        private static readonly ILog _log = LogManager.GetLogger(typeof (AnalyzerTask));

        private readonly LinkedList<ListFile> _temp = new LinkedList<ListFile>();
        private const int BUFFER_SIZE = 8192;
        private const int SLEEP_TIME = 100;
        private String _toString;

        public AnalyzerTask(GameProperty property, ListFileType type)
        {
            ListType = type;
            CurrentProperty = property;
            Status = Status.FREE; //TODO not used?
        }
        #endregion

        #region Run Thead
        public override void Run()
        {
            _toString = "AWLauncher - Analize " + ListType + "; Game: " + CurrentProperty.GameEnum() + ":" + GetHashCode();

            var mainThead = new Thread(AnalizeToThread)
                                {
                                    Name = _toString,
                                    Priority = ThreadPriority.Lowest
                                };

            mainThead.Start();
        }

        private void AnalizeToThread()
        {
            if (!CurrentProperty.ListLoader.IsValid)
            {
                GoEnd(null, true, false);
                return;
            }

            MainForm.Instance.SetMainFormState(MainFormState.CHECKING);
            MainForm.Instance.UpdateProgressBar(0, true);
            MainForm.Instance.UpdateProgressBar(0, false);

            Status = Status.DOWNLOAD;

            if (ListType == ListFileType.NORMAL)
            {
                foreach (ListFile listFile in CurrentProperty.ListLoader.Items[ListFileType.CRITICAL])
                {
                    _temp.AddLast(listFile);
                }

                foreach (ListFile deleteFile in CurrentProperty.ListLoader.Items[ListFileType.DELETE])
                {
                    _temp.AddLast(deleteFile);
                }
            }

            foreach (ListFile listFile in CurrentProperty.ListLoader.Items[ListType])
            {
                _temp.AddLast(listFile);
            }

            DoCheckFiles();
        }
        #endregion

        #region GoEnd

        private void GoEnd(String word, bool free, bool status, params Object[] aa)
        {
            if (free)
            {
                Status = Status.FREE;
            }

            MainForm.Instance.SetMainFormState(MainFormState.NONE);
            
            if(status)
                MainForm.Instance.UpdateStatusLabel(String.Format(word, aa));

            MainForm.Instance.UpdateProgressBar(0, false);
            MainForm.Instance.UpdateProgressBar(0, true);

            _temp.Clear();

            OnEnd();
        }

        private void GoEnd(WordEnum? word, bool free, params Object[] aa)
        {
            if (free)
            {
                Status = Status.FREE;
            }

            if (word != null && word == WordEnum.UPDATE_DONE)
            {
                CurrentProperty[ListType] = true;
            }

            MainForm.Instance.SetMainFormState(MainFormState.NONE);

            if (word != null)
            {
                MainForm.Instance.UpdateStatusLabel((WordEnum)word, aa);
            }
            else
            {
                MainForm.Instance.UpdateStatusLabel("");   
            }

            MainForm.Instance.UpdateProgressBar(0, false);
            MainForm.Instance.UpdateProgressBar(0, true);

            _temp.Clear();

            OnEnd();
        }

        #endregion

        #region Do Check Files
        public void DoCheckFiles()
        {
            int count = 0;
            int oldFilePersent = -1;
            int oldTotalPersent = -1;

            foreach (ListFile file in _temp)
            {
                if (Status == Status.CANCEL)
                {
                    GoEnd(WordEnum.CANCEL_BY_USER, true);
                    return;
                }

                String path = CurrentProperty.Path;

                String fileName = path + file.FileName.Replace("/", "\\");
                String zipFileName = fileName + ".zip";

                FileInfo info = new FileInfo(fileName);
                FileInfo zipInfo = new FileInfo(zipFileName);

                MainForm.Instance.UpdateStatusLabel(WordEnum.CHECKING_S1, info.Name);

                try
                {
                    if (!info.Exists && file.Type != ListFileType.DELETE)
                    {
                        goto DownloadFile;
                    }
                    
                    if (FileUtils.IsFileOpen(info))
                    {
                        GoEnd(WordEnum.FILE_S1_IS_OPENED_UPDATE_CANCEL, true, info.Name);
                        return;
                    }

                    if(FileUtils.IsFileOpen(zipInfo))
                    {
                        GoEnd(WordEnum.FILE_S1_IS_OPENED_UPDATE_CANCEL, true, zipInfo.Name);
                        return;   
                    }

                    if (file.Type != ListFileType.DELETE)
                    {
                        String checkSum = DTHasher.GetMD5Hash(fileName);

                        if (!checkSum.Equals(file.md5Checksum)) //файл не совпадает
                        {
                            goto DownloadFile;
                        }
                    }
                    else
                    {
                        goto DeleteFile;
                    }

                    goto UpdateLabel;
                }
                catch (Exception e)
                {
                    _log.Info("Exception: " + e, e);

                    if (file.Type != ListFileType.DELETE)
                    {
                        goto DownloadFile;
                    }
                }

            //удаления файла
            DeleteFile:
                {
                    if (info.Exists)
                    {
                        MainForm.Instance.UpdateStatusLabel(WordEnum.DELETE_S1, info.Name);

                        info.Delete();
                    } 
                
                    goto UpdateLabel;
                }

            //загрузка файла
            DownloadFile:
                {
                    Uri url = new Uri(CurrentProperty.listURL() + file.FileName + ".zip");

                    if (info.Directory != null)
                    {
                        if (!info.Directory.Exists)
                        {
                            info.Directory.Create();
                        }
                    }

                    try
                    {
                        MainForm.Instance.UpdateStatusLabel(WordEnum.DOWNLOADING_S1, info.Name);

                        WebRequest request = WebRequest.Create(url);
                        using (WebResponse response = request.GetResponse())
                        {
                            int allSize = (int)response.ContentLength;
                            int currentRead = 0;
                            
                            using (Stream remoteStream = response.GetResponseStream())
                            {
                                using (Stream localStream = new FileStream(zipFileName, FileMode.Create, FileAccess.Write, FileShare.None))
                                {
                                    byte[] buffer = new byte[BUFFER_SIZE];

                                    int readSize;
                                    while ((readSize = remoteStream.Read(buffer, 0, buffer.Length)) > 0)
                                    {
                                        if (Status == Status.CANCEL)
                                        {
                                            GoEnd(WordEnum.CANCEL_BY_USER, false);
                                            return;
                                        }

                                        localStream.Write(buffer, 0, readSize);
                                        currentRead += readSize;

                                        var persent = (int)((100F * currentRead) / allSize);
                                        
                                        if (persent != oldFilePersent)
                                        {
                                            oldFilePersent = persent;
                                            MainForm.Instance.UpdateProgressBar(persent, false);
                                        }
                                    }
                                }
                            }
                        }

                        UnpackFile(file);
                    }
                    catch(WebException e)
                    {
                        GoEnd(e.Message, true, true);    
                        return;
                    }
                    catch (Exception e)
                    {
                        if(_log.IsDebugEnabled)
                        {
                            _log.Debug("Exception: " + e.Message, e);
                        }
                        new ExceptionForm(e);
                        GoEnd(null, true);    
                        return;
                    }
                }
             
            //обновления главной формы
            UpdateLabel:
                {
                    count++;

                    int persent = (int) ((100F*count)/_temp.Count);
                    if (oldTotalPersent != persent)
                    {
                        oldTotalPersent = persent;
                        MainForm.Instance.UpdateProgressBar(persent, true);
                    }
                }

                //Thread.Sleep(SLEEP_TIME);
            }

            GoEnd(WordEnum.UPDATE_DONE, true);
        }
        #endregion

        #region Unpack File

        public void UnpackFile(ListFile file)
        {
            if (Status == Status.CANCEL)
            {
                GoEnd(WordEnum.CANCEL_BY_USER, true);
                return;
            }

            string path = CurrentProperty.Path;
            string fileName = path + file.FileName.Replace("/", "\\");

            FileInfo descFile = new FileInfo(fileName);
            FileInfo zipFile = new FileInfo(fileName + ".zip");
            
            if (!zipFile.Exists)
            {
                GoEnd(WordEnum.PROBLEM_WITH_SERVER, true);
                return;
            }

            MainForm.Instance.UpdateStatusLabel(WordEnum.UNPACKING_S1, zipFile.Name.Replace(".zip", ""));
            MainForm.Instance.UpdateProgressBar(0, false);

            ZipInputStream zipStream = new ZipInputStream(zipFile.OpenRead()) { Password = "afsf325cf6y34g6a5frs4cf5" };

            ZipEntry fileEntry;
            byte[] listDate = null;

            if ((fileEntry = zipStream.GetNextEntry()) != null)
            {
                listDate = new byte[zipStream.Length];
                zipStream.Read(listDate, 0, listDate.Length);
            }

            zipStream.Close();
            zipFile.Delete(); //удаляем зип файл

            descFile.Delete(); //удаляем старый файл

            if (listDate == null)
            {
                GoEnd(WordEnum.PROBLEM_WITH_SERVER, true);
                return;
            }

            Stream stream = descFile.Create();

            int size = listDate.Length;
            int oldPersent = 0;

            for (int i = 0; i < listDate.Length; i++)
            {
                if (Status == Status.CANCEL)
                {
                    GoEnd(WordEnum.CANCEL_BY_USER, false);
                    break;
                }

                stream.WriteByte(listDate[i]);

                int persent = (int) ((100F*i)/size);

                if (persent != oldPersent)
                {
                    oldPersent = persent;
                    MainForm.Instance.UpdateProgressBar(persent, false);
                }
            }

            MainForm.Instance.UpdateProgressBar(100, false);

            stream.Close();

            descFile.LastWriteTime = fileEntry.DateTime;
        }

        #endregion

        #region Propertyes

        public Status Status { get; set; }

        public GameProperty CurrentProperty { get; set; }

        public ListFileType ListType { get; set; }
       
        #endregion

        #region Override
        public override void Cancel()
        {
            Status = Status.CANCEL;
        }
        
        public override string ToString()
        {
            return _toString;
        }
        #endregion
    }
}