#region Usage

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using com.jds.AWLauncher.classes.forms;
using com.jds.AWLauncher.classes.language.enums;
using com.jds.AWLauncher.classes.listloader;
using com.jds.AWLauncher.classes.listloader.enums;
using com.jds.AWLauncher.classes.version_control;
using com.jds.AWLauncher.classes.version_control.gui;
using com.jds.AWLauncher.classes.zip;
using log4net;

#endregion

namespace com.jds.AWLauncher.classes.task_manager.tasks
{
    public class GUListLoaderTask : AbstractTask
    {
        #region Constructor & Variables

        private static readonly ILog _log = LogManager.GetLogger(typeof(GUListLoaderTask));

        private readonly LinkedList<ListFile> _items = new LinkedList<ListFile>();

        private readonly WebClient _webClient = new WebClient();
        private Thread _thread;

        public GUListLoaderTask()
        {
            IsValid = false;
            Status = Status.FREE;

            _webClient.DownloadDataCompleted += client_DownloadDataCompleted;
        }

        #endregion

        #region Берет  с инета список

        public override void Run()
        {
            _thread = new Thread(ListDownloadThread)
                          {
                              Name = "Get List Thread for AWLauncher"
                          };

            _thread.Start();
        }

        private void ListDownloadThread()
        {
            if (Status != Status.FREE || _webClient.IsBusy)
            {
                return;
            }

            AssemblyPage.Instance().UpdateStatusLabel(WordEnum.STARTING_DOWNLOAD_LIST, true);

            AssemblyPage.Instance().SetState(MainFormState.CHECKING);
            MainForm.Instance.SetMainFormState(MainFormState.CHECKING);

            Status = Status.DOWNLOAD;
            IsValid = false;

            _webClient.DownloadDataAsync(new Uri("http://jdevelopstation.com/awlauncher/list.zip"));
        }

        private void client_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                GoEnd(WordEnum.CANCEL_BY_USER);
                return;
            }

            if (e.Error != null)
            {
                if (e.Error is WebException)
                {
                    GoEnd(WordEnum.PROBLEM_WITH_INTERNET);
                    return;
                }
                else
                {
                    _log.Info("Exception: while downloading list: " + e.Error.Message, e.Error);

                    GoEnd(WordEnum.ERROR_DOWNLOAD_LIST);
                    return;
                }
            }

            if (e.Result == null)
            {
                GoEnd(WordEnum.PROBLEM_WITH_SERVER);
                return;
            }

            GoNextStep(e.Result);
        }

        /// <summary>
        /// С текущего масива грузит список файлов
        /// </summary>
        /// <param name="zipArray"></param>
        private void GoNextStep(byte[] zipArray)
        {
            var zipStream = new ZipInputStream(new MemoryStream(zipArray))
                                {
                                    Password = "afsf325cf6y34g6a5frs4cf5"
                                };

            byte[] array = null;

            if ((zipStream.GetNextEntry()) != null)
            {
                array = new byte[zipStream.Length];
                zipStream.Read(array, 0, array.Length);
            }

            zipStream.Close();

            if (array == null)
            {
                GoEnd(WordEnum.PROBLEM_WITH_SERVER);
                return;
            }

            string encodingBytes = Encoding.UTF8.GetString(array);
            string[] lines = encodingBytes.Split('\n');

            foreach (string line in lines)
            {
                if (line.Trim().Equals(""))
                    continue;

                if (Status == Status.CANCEL)
                {
                    GoEnd(WordEnum.CANCEL_BY_USER);
                    return;
                }

                if (line.StartsWith("#Version:"))
                {
                    String ver = line.Replace("#Version:", "").Trim();
                    VersionType v = AssemblyPage.CalcType(ver);

                    MainForm.Instance.SetVersionType(ver, v);
                    AssemblyPage.Instance().SetVersionType(ver, v);
                    continue;
                }

                if (line.StartsWith("#"))
                {
                    continue;
                }

                try
                {
                    ListFile file = ListFile.parseIgnoreCritical((line));

                    _items.AddLast(file);
                }
                catch (Exception e)
                {
                    _log.Info("Exception for line " + line + " " + e, e);
                }
            }

            IsValid = true;

            GoEnd(WordEnum.ENDING_DOWNLOAD_LIST);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void GoEnd(WordEnum word)
        {
            Status = Status.FREE;

            AssemblyPage.Instance().UpdateStatusLabel(word, false);

           // if (!(TaskManager.Instance.NextTask() is AnalyzerTask))
            {
                AssemblyPage.Instance().SetState(MainFormState.NONE);
                MainForm.Instance.SetMainFormState(MainFormState.NONE);
            }

            OnEnd();
        }

        public override void Cancel()
        {
            Status = Status.CANCEL;

            if (_webClient.IsBusy)
            {
                _webClient.CancelAsync();
            }
        }

        #endregion

        #region Properties & Getters

        public LinkedList<ListFile> Items
        {
            get { return _items; }
        }

        public bool IsValid { get; set; }

        public Status Status { get; set; }

        #endregion
    }
}