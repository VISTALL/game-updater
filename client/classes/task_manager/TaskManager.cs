using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using com.jds.AWLauncher.classes.task_manager.tasks;
using log4net;

namespace com.jds.AWLauncher.classes.task_manager
{
    public class TaskManager
    {
        private readonly Queue<AbstractTask> _queue = new Queue<AbstractTask>();
        private static readonly ILog _log = LogManager.GetLogger(typeof (TaskManager));

        private const int INTERVAL = 100;
        private Thread _mainThread;
        private bool _shutdown;

        #region Instance

        private static TaskManager _instance;

        public static TaskManager Instance
        {
            get { return _instance ?? (_instance = new TaskManager()); }
        }

        #endregion

        #region Task Thread

        public void Start()
        {
            _mainThread = new Thread(threadMethod) {Name = "GUpdater - Task Thread"};
            _mainThread.Start();
        }

        private void threadMethod()
        {
            while (!_shutdown)
            {
                try
                {
                    if (_queue.Count > 0)
                    {
                        if (ActiveTask == null)
                        {
                            var task = _queue.Dequeue();
                            
                            ActiveTask = task;

                            try
                            {
                                task.Run();
                            }   
                            catch(Exception e)
                            {
                                _log.Info("Exception: " + e, e);
                                ActiveTask = null;
                            }
                        }
                    }
                }
                finally
                {
                    Thread.Sleep(INTERVAL);
                }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Close(bool shutdown)
        {
            _shutdown = shutdown;

            RemoveAllTasks();

            if (ActiveTask != null)
            {
                ActiveTask.Cancel();
            }
        }

        #endregion

        #region Tasks Actions

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddTask(AbstractTask task)
        {
            _queue.Enqueue(task);
        }

        public void RemoveAllTasks()
        {
            _queue.Clear();
        }

        public void OnTaskDone(AbstractTask task)
        {
            if(ActiveTask == task)
            {
                ActiveTask = null;
            }
        }

        public AbstractTask NextTask
        {
            get
            {
                return _queue.Count > 0 ? _queue.Peek() : null;
            }
        }

        #endregion

        #region Properties

        public AbstractTask ActiveTask { get; set; }

        #endregion
    }
}