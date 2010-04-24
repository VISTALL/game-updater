using System;

namespace com.jds.GUpdater.classes.task_manager.tasks
{
    public abstract class AbstractTask
    {
        public abstract void Run();
        public abstract void Cancel();

        protected void OnEnd()
        {           
            TaskManager.Instance.OnTaskDone(this);
        }
    }
}