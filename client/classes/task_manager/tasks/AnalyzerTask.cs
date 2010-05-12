#region Usage

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using com.jds.AWLauncher.classes.forms;
using com.jds.AWLauncher.classes.games.propertyes;
using com.jds.AWLauncher.classes.language;
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
        private static readonly ILog _log = LogManager.GetLogger(typeof (AnalyzerTask));
        private readonly LinkedList<ListFile> _temp = new LinkedList<ListFile>();
        private String _toString;
        private int _maxSize;

        private readonly WebClient _webClient = new WebClient();

        public AnalyzerTask(GameProperty property, ListFileType type)
        {
            ListType = type;
            CurrentProperty = property;
            Status = Status.FREE;

            _webClient.DownloadFileCompleted += webClient_DownloadFileCompleted;
            _webClient.DownloadProgressChanged += webClient_DownloadProgressChanged;
        }

        public override void Run()
        {
            _toString = "Analize " + ListType + "; Game: " + CurrentProperty.GameEnum() + ":" + GetHashCode();

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
                GoEnd((WordEnum?)null, true);
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
            }

            foreach (ListFile listFile in CurrentProperty.ListLoader.Items[ListType])
            {
                _temp.AddLast(listFile);
            }

            _maxSize = _temp.Count;

            CheckNextFile();
        }

        #region GoEnd
        private void GoEnd(String word, bool free, params Object[] aa)
        {
            if (free)
            {
                Status = Status.FREE;
            }

            MainForm.Instance.SetMainFormState(MainFormState.NONE);

            MainForm.Instance.UpdateStatusLabel(String.Format(word, aa));

            MainForm.Instance.UpdateProgressBar(0, false);
            MainForm.Instance.UpdateProgressBar(0, true);

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
                MainForm.Instance.UpdateStatusLabel(String.Format(LanguageHolder.Instance()[(WordEnum)word], aa));
            }

            MainForm.Instance.UpdateProgressBar(0, false);
            MainForm.Instance.UpdateProgressBar(0, true);

            OnEnd();
        }
        #endregion

        public override void Cancel()
        {
            Status = Status.CANCEL;
            if(_webClient.IsBusy)
            {
                _webClient.CancelAsync();
            }
        }

        public void CheckNextFile()
        {
            if (CurrentProperty == null)
            {
                //what the facker?
                return;
            }

            if (Status == Status.CANCEL)
            {
                GoEnd(WordEnum.CANCEL_BY_USER, true);
                return;
            }

            ListFile file = FirstFile();

            if (file == null)
            {
                GoEnd(WordEnum.UPDATE_DONE, true);
                return;
            }

            Thread.Sleep(100);

            int currentCount = _maxSize - _temp.Count;
            var persent = (int) ((100F*(currentCount))/_maxSize);

            MainForm.Instance.UpdateProgressBar(persent, true);

            string path = CurrentProperty.Path;
            string fileName = path + file.FileName.Replace("/", "\\");

            var info = new FileInfo(fileName);

            string word = LanguageHolder.Instance()[WordEnum.CHECKING_S1];

            MainForm.Instance.UpdateStatusLabel(String.Format(word, info.Name.Replace(".zip", "")));
            try
            {

                if (!info.Exists)
                {
                    DownloadFile(file); //грузим
                }
                else if (FileUtils.IsFileOpen(info))
                {
                    GoEnd(WordEnum.FILE_S1_IS_OPENED_UPDATE_CANCEL, true, info.Name.Replace(".zip", ""));
                    return;
                }
                else
                {
                    String checkSum = null;

                    try
                    {
                        checkSum = DTHasher.GetMD5Hash(fileName);
                    }
                    catch (Exception)
                    {
                        try
                        {
                            info.Delete();
                        }
                        catch
                        {
                        }

                        GoEnd(WordEnum.FILE_S1_IS_PROBLEMATIC_UPDATE_CANCEL_PLEASE_RECHECK, true,
                              info.Name.Replace(".zip", ""));
                        return;
                    }

                    if (checkSum == null)
                    {
                        DownloadFile(file); //грузим
                    }
                    else if (!checkSum.Equals(file.md5Checksum)) //файл не совпадает
                    {
                        DownloadFile(file); //грузим   
                    }
                    else if(checkSum.Equals(file.md5Checksum))
                    {
                        _temp.Remove(file);

                        CheckNextFile(); //идем дальше
                    }
                }
            }
            catch(Exception e)
            {
                _log.Info("Exception: " +e , e);

                DownloadFile(file); //грузим   
            }
        }

        public ListFile FirstFile()
        {
            return _temp.Count == 0 ? null : _temp.First.Value;
        }

        public void DownloadFile(ListFile file)
        {
            if (Status == Status.CANCEL)
            {
                GoEnd(WordEnum.CANCEL_BY_USER, true);
                return;
            }

            if(_webClient.IsBusy)
            {
                while (_webClient.IsBusy)
                {
                    Thread.Sleep(3000);
                }
            }

            string path = CurrentProperty.Path;
            string fileName = path + file.FileName.Replace("/", "\\") + ".zip";
            var url = new Uri(CurrentProperty.listURL() + file.FileName + ".zip");

            var info = new FileInfo(fileName);

            if (info.Directory != null)
            {
                if (!info.Directory.Exists)
                {
                    info.Directory.Create();
                }
            }    

            string word = LanguageHolder.Instance()[WordEnum.DOWNLOADING_S1];

            MainForm.Instance.UpdateStatusLabel(String.Format(word, info.Name.Replace(".zip", "")));

            _webClient.DownloadFileAsync(url, fileName, file);
        }


        void webClient_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (Status == Status.CANCEL || e.Cancelled)
            {
                GoEnd(WordEnum.CANCEL_BY_USER, false);
                return;
            }

            if (e.Error != null)
            {
                if (_log.IsDebugEnabled)
                {
                    _log.Info("Exception[241]: " + e.Error, e.Error);
                }
                GoEnd(e.Error.Message, true);
                return;
            }

            if(!(e.UserState is ListFile))
            {
                GoEnd("Unknown error", true);
                return;     
            }

            UnpackFile((ListFile)e.UserState);
        }
       
        static void webClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            MainForm.Instance.UpdateProgressBar(e.ProgressPercentage, false);      
        }

        public void UnpackFile(ListFile file)
        {
            if (Status == Status.CANCEL)
            {
                GoEnd(WordEnum.CANCEL_BY_USER, true);
                return;
            }

            string path = CurrentProperty.Path;
            string fileName = path + file.FileName.Replace("/", "\\");

            var descFile = new FileInfo(fileName);
            var zipFile = new FileInfo(fileName + ".zip");

            string word = LanguageHolder.Instance()[WordEnum.UNPACKING_S1];
            MainForm.Instance.UpdateStatusLabel(String.Format(word, zipFile.Name.Replace(".zip", "")));
            MainForm.Instance.UpdateProgressBar(0, false);

            var zipStream = new ZipInputStream(zipFile.OpenRead()) {Password = "afsf325cf6y34g6a5frs4cf5"};

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

                var persent = (int) ((100F*i)/size);

                if (persent != oldPersent)
                {
                    oldPersent = persent;
                    MainForm.Instance.UpdateProgressBar(persent, false);
                }
            }

            MainForm.Instance.UpdateProgressBar(100, false);

            stream.Close();

            descFile.LastWriteTime = fileEntry.DateTime;

            _temp.Remove(file);

            CheckNextFile();
        }

        #region Propertyes

        public Status Status { get; set; }

        public GameProperty CurrentProperty { get; set; }

        public ListFileType ListType { get; set; }

        #endregion

        public override string ToString()
        {
            return _toString;
        }
    }
}