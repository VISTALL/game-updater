#region Usage

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using com.jds.AWLauncher.classes.language;
using com.jds.AWLauncher.classes.language.enums;
using com.jds.AWLauncher.classes.listloader;
using com.jds.AWLauncher.classes.listloader.enums;
using com.jds.AWLauncher.classes.version_control.gui;
using com.jds.AWLauncher.classes.zip;

#endregion

namespace com.jds.AWLauncher.classes.task_manager.tasks
{
    public class GUAnalyzerTask : AbstractTask
    {
        private readonly LinkedList<ListFile> _temp = new LinkedList<ListFile>();
        private int _maxSize;

        public GUAnalyzerTask()
        {
            Status = Status.FREE;
        }

        public override void Run()
        {
            var mainThead = new Thread(AnalizeToThread)
                                {
                                    Name = "Analize AWLauncher",
                                    Priority = ThreadPriority.Lowest
                                };

            mainThead.Start();
        }

        private void AnalizeToThread()
        {
            if (!AssemblyPage.Instance().ListLoader.IsValid)
            {
                return;
            }

            AssemblyPage.Instance().SetState(MainFormState.CHECKING);
            AssemblyPage.Instance().UpdateProgressBar(0, true);
            AssemblyPage.Instance().UpdateProgressBar(0, false);

            Status = Status.DOWNLOAD;

            foreach (ListFile listFile in AssemblyPage.Instance().ListLoader.Items)
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
                AssemblyPage.Instance().SetState(MainFormState.DONE);
            }
            else
            {
                AssemblyPage.Instance().SetState(MainFormState.NONE);
            }

            AssemblyPage.Instance().UpdateStatusLabel(String.Format(LanguageHolder.Instance()[word], aa), false);
            AssemblyPage.Instance().UpdateProgressBar(0, false);
            AssemblyPage.Instance().UpdateProgressBar(0, true);

            OnEnd();
        }

        public override void Cancel()
        {
            Status = Status.CANCEL;
        }

        public void CheckNextFile()
        {
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

            AssemblyPage.Instance().UpdateProgressBar(persent, true);

            string path = Directory.GetCurrentDirectory();
            string fileName = path + file.FileName.Replace("/", "\\");

            var info = new FileInfo(fileName);

            string word = LanguageHolder.Instance()[WordEnum.CHECKING_S1];

            AssemblyPage.Instance().UpdateStatusLabel(String.Format(word, info.Name.Replace(".zip", "")), true);

           /* if (FileUtils.IsFileOpen(info))
            {
                GoEnd(WordEnum.FILE_S1_IS_OPENED_UPDATE_CANCEL, true, info.Name.Replace(".zip", ""));
                return;
            }*/

            String checkSum;
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
                {}
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

            string path = Directory.GetCurrentDirectory();
            string fileName = path + file.FileName.Replace("/", "\\") + ".new";
            var url = new Uri("http://jdevelopstation.com/awlauncher" + file.FileName + ".zip");

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

            string word = LanguageHolder.Instance()[WordEnum.DOWNLOADING_S1];
            AssemblyPage.Instance().UpdateStatusLabel(String.Format(word, info.Name.Replace(".zip", "")), true);

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
                        AssemblyPage.Instance().UpdateProgressBar(persent, false);
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

            string path = Directory.GetCurrentDirectory();
            string fileName = path + file.FileName.Replace("/", "\\");

           // var descFile = new FileInfo(fileName);
            var newFile = new FileInfo(fileName + ".new");

            string word = LanguageHolder.Instance()[WordEnum.UNPACKING_S1];

            AssemblyPage.Instance().UpdateStatusLabel(String.Format(word, newFile.Name.Replace(".new", "")), true);
            AssemblyPage.Instance().UpdateProgressBar(0, false);

            var zipStream = new ZipInputStream(newFile.OpenRead());

            ZipEntry fileEntry;
            byte[] bytes = null;

            if ((fileEntry = zipStream.GetNextEntry()) != null)
            {
                bytes = new byte[zipStream.Length];
                zipStream.Read(bytes, 0, bytes.Length);
            }

            zipStream.Close();
            newFile.Delete(); //удаляем зип файл

           // descFile.Delete(); //удаляем старый файл

            if (bytes == null)
            {
                GoEnd(WordEnum.PROBLEM_WITH_SERVER, true);
                return;
            }

            FileStream fileStream = newFile.Create(); 
            
            int size = bytes.Length;
            int oldPersent = 0;

            for (int i = 0; i < bytes.Length; i++)
            {
                if (Status == Status.CANCEL)
                {
                    GoEnd(WordEnum.CANCEL_BY_USER, false);
                    break;
                }

                fileStream.WriteByte(bytes[i]);

                var persent = (int) ((100F*i)/size);

                if (persent != oldPersent)
                {
                    oldPersent = persent;
                    AssemblyPage.Instance().UpdateProgressBar(persent, false);
                }
            }

            AssemblyPage.Instance().UpdateProgressBar(100, false);

            fileStream.Close();

            newFile.LastWriteTime = fileEntry.DateTime;

            _temp.Remove(file);

            CheckNextFile();
        }

        #region Propertyes

        public Status Status { get; set; }
        #endregion
    }
}