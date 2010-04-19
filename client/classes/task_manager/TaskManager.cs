using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using com.jds.GUpdater.classes.task_manager.tasks;

namespace com.jds.GUpdater.classes.task_manager
{
    public class TaskManager
    {
        private readonly Queue<AbstractTask> _queue = new Queue<AbstractTask>();

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
                        AbstractTask task = _queue.Peek();

                        bool runNeed = false;

                        if (ActiveTask != null && task.canParale() || ActiveTask == null)
                        {
                            runNeed = true;
                        }

                        if (runNeed)
                        {
                            task = _queue.Dequeue();

                            task.Run();

                            ActiveTask = task;
                        }
                    }
                }
                finally
                {
                    Thread.Sleep(100);
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
            if (task.canParale())
            {
                return;
            }

            ActiveTask = null;
        }

        public AbstractTask NextTask()
        {
            return _queue.Count > 0 ? _queue.Peek() : null;
        }

        #endregion

        #region Properties

        public AbstractTask ActiveTask { get; set; }

        #endregion
    }
}