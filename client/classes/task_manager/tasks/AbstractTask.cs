namespace com.jds.AWLauncher.classes.task_manager.tasks
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