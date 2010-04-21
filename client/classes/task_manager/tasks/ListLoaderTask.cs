#region Usage

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using com.jds.GUpdater.classes.forms;
using com.jds.GUpdater.classes.games.propertyes;
using com.jds.GUpdater.classes.language.enums;
using com.jds.GUpdater.classes.listloader;
using com.jds.GUpdater.classes.listloader.enums;
using com.jds.GUpdater.classes.zip;
using log4net;

#endregion

namespace com.jds.GUpdater.classes.task_manager.tasks
{
    public class ListLoaderTask : AbstractTask
    {
        #region Constructor & Variables

        private static readonly ILog _log = LogManager.GetLogger(typeof (ListLoaderTask));

        private readonly Dictionary<ListFileType, LinkedList<ListFile>> _list =
            new Dictionary<ListFileType, LinkedList<ListFile>>();

        private readonly GameProperty _property;
        private readonly WebClient _webClient = new WebClient();
        private Thread _thread;

        public ListLoaderTask(GameProperty p)
        {
            _property = p;

            IsValid = false;
            Status = Status.FREE;

            _list.Add(ListFileType.CRITICAL, new LinkedList<ListFile>());
            _list.Add(ListFileType.NORMAL, new LinkedList<ListFile>());

            _webClient.DownloadDataCompleted += client_DownloadDataCompleted;
        }

        #endregion

        #region Берет  с инета список

        public override void Run()
        {
            if (_property == null || _property.Panel == null)
            {
                throw new NullReferenceException("Panel or GProperty is null!!!!!!. WTF???");
            }

            _thread = new Thread(ListDownloadThread)
                          {
                              Name = "Get List Thread " + _property.GetType().Name
                          };

            _thread.Start();
        }

        private void ListDownloadThread()
        {
            if (!_property.isEnable())
            {
                GoEnd(WordEnum.GAME_IS_DISABLED);
                return;
            }

            if (Status != Status.FREE || _webClient.IsBusy)
            {
                return;
            }

            MainForm.Instance.UpdateStatusLabel(WordEnum.STARTING_DOWNLOAD_LIST);
            MainForm.Instance.SetMainFormState(MainFormState.CHECKING);

            Status = Status.DOWNLOAD;
            IsValid = false;

            _webClient.DownloadDataAsync(new Uri(_property.listURL() + "/list.zip"));
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

                if (line.StartsWith("#Revision:"))
                {
                    Revision = int.Parse(line.Replace("#Revision:", "").Trim());
                    continue;
                }

                if (line.StartsWith("#"))
                {
                    continue;
                }

                try
                {
                    ListFile file = ListFile.parse(line);

                    _list[file.Type].AddLast(file);
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

            MainForm.Instance.UpdateStatusLabel(word);

            if (!(TaskManager.Instance.NextTask() is AnalyzerTask))
            {
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

        public Dictionary<ListFileType, LinkedList<ListFile>> Items
        {
            get { return _list; }
        }

        public bool IsValid { get; set; }

        public Status Status { get; set; }

        public int Revision { get; set; }

        public LinkedList<ListFile> Files(ListFileType t)
        {
            return _list[t];
        }

        #endregion
    }
}