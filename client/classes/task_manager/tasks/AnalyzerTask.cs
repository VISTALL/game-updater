#region Usage

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using com.jds.GUpdater.classes.forms;
using com.jds.GUpdater.classes.games.propertyes;
using com.jds.GUpdater.classes.language;
using com.jds.GUpdater.classes.language.enums;
using com.jds.GUpdater.classes.listloader;
using com.jds.GUpdater.classes.listloader.enums;
using com.jds.GUpdater.classes.utils;
using ICSharpCode.SharpZipLib.Zip;

#endregion

namespace com.jds.GUpdater.classes.task_manager.tasks
{
    public class AnalyzerTask : AbstractTask
    {
        private readonly LinkedList<ListFile> _temp = new LinkedList<ListFile>();
        private int _maxSize;

        public AnalyzerTask(GameProperty property, ListFileType type)
        {
            ListType = type;
            CurrentProperty = property;
            Status = Status.FREE;
        }

        public override void Run()
        {
            var mainThead = new Thread(AnalizeToThread)
                                {
                                    Name = "Analize " + ListType + ";Game: " + CurrentProperty.GameEnum(),
                                    Priority = ThreadPriority.Lowest
                                };

            mainThead.Start();
        }

        private void AnalizeToThread()
        {
            if (!CurrentProperty.ListLoader.IsValid)
            {
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

        private void GoEnd(WordEnum word, bool free, params Object[] aa)
        {
            if (free)
            {
                Status = Status.FREE;
            }

            if (word == WordEnum.UPDATE_DONE)
            {
                CurrentProperty[ListType] = true;
            }

            MainForm.Instance.SetMainFormState(MainFormState.NONE);
            MainForm.Instance.UpdateStatusLabel(String.Format(LanguageHolder.Instance[word], aa));
            MainForm.Instance.UpdateProgressBar(0, false);
            MainForm.Instance.UpdateProgressBar(0, true);

            OnEnd();
        }

        public override void Cancel()
        {
            Status = Status.CANCEL;
        }

        public void CheckNextFile()
        {
            if (CurrentProperty == null)
                return;

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

            int currentCount = _maxSize - _temp.Count;
            var persent = (int) ((100F*(currentCount))/_maxSize);

            MainForm.Instance.UpdateProgressBar(persent, true);

            string path = CurrentProperty.Path;
            string fileName = path + file.FileName.Replace("/", "\\");

            var info = new FileInfo(fileName);

            string word = LanguageHolder.Instance[WordEnum.CHECKING_S1];

            MainForm.Instance.UpdateStatusLabel(String.Format(word, info.Name.Replace(".zip", "")));

            if (FileUtils.IsFileOpen(info))
            {
                GoEnd(WordEnum.FILE_S1_IS_OPENED_UPDATE_CANCEL, true, info.Name.Replace(".zip", ""));
                return;
            }

            String checkSum;
            try
            {
                checkSum = DTHasher.GetMD5Hash(fileName);
            }
            catch (Exception)
            {
                GoEnd(WordEnum.FILE_S1_IS_PROBLEMATIC_UPDATE_CANCEL_PLEASE_RECHECK, true, info.Name.Replace(".zip", ""));
                return;
            }

            if (checkSum == null)
            {
                GoEnd(WordEnum.PROBLEM_WITH_SERVER, true);
                return;
            }

            if (!info.Exists)
            {
                DownloadFile(file); //грузим
            }
            else if (!checkSum.Equals(file.md5Checksum)) //файл не совпадает
            {
                DownloadFile(file); //грузим
            }
            else
            {
                _temp.Remove(file);

                CheckNextFile(); //идем дальше
            }
        }

        public ListFile FirstFile()
        {
            return _temp.Count == 0 ? null : _temp.First.Value;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DownloadFile(ListFile file)
        {
            if (Status == Status.CANCEL)
            {
                GoEnd(WordEnum.CANCEL_BY_USER, true);
                return;
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

            var request = (HttpWebRequest) WebRequest.Create(url);
            var response = (HttpWebResponse) request.GetResponse();
            response.Close();

            long iSize = response.ContentLength;
            int iRunningByteTotal = 0;

            var client = new WebClient();

            Stream remoteStream;
            try
            {
                remoteStream = client.OpenRead(url);
            }
            catch (WebException)
            {
                GoEnd(WordEnum.PROBLEM_WITH_INTERNET, true);
                return;
            }
            catch (Exception)
            {
                GoEnd(WordEnum.PROBLEM_WITH_SERVER, true);
                return;
            }

            var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
            var byteBuffer = new byte[8192];

            string word = LanguageHolder.Instance[WordEnum.DOWNLOADING_S1];
            MainForm.Instance.UpdateStatusLabel(String.Format(word, info.Name.Replace(".zip", "")));

            bool exception = false;
            try
            {
                int oldPersent = 0;
                int iByteSize;

                while ((iByteSize = remoteStream.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
                {
                    if (Status == Status.CANCEL)
                    {
                        GoEnd(WordEnum.CANCEL_BY_USER, false);
                        break;
                    }

                    fileStream.Write(byteBuffer, 0, iByteSize);
                    iRunningByteTotal += iByteSize;

                    var persent = (int) ((100F*iRunningByteTotal)/iSize);
                    if (persent != oldPersent)
                    {
                        oldPersent = persent;
                        MainForm.Instance.UpdateProgressBar(persent, false);
                    }
                }
            }
            catch (WebException)
            {
                exception = true;
                GoEnd(WordEnum.PROBLEM_WITH_INTERNET, true);
            }
            catch (Exception)
            {
                exception = true;
                GoEnd(WordEnum.PROBLEM_WITH_SERVER, true);
            }

            remoteStream.Close();
            fileStream.Close();

            if (!exception)
            {
                UnpackFile(file);
            }
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

            string word = LanguageHolder.Instance[WordEnum.UNPACKING_S1];
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
    }
}