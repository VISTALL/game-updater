using com.jds.GUpdater.classes.games.propertyes;
using com.jds.GUpdater.classes.transperent;

namespace com.jds.GUpdater.classes.task_manager.tasks
{
    public class GameStartTask : AbstractTask
    {
        private readonly GameProperty _property;

        public GameStartTask(GameProperty p)
        {
            _property = p;
        }

        public override void Run()
        {
            if (_property.GetStartInfo() == null)
            {
                OnEnd();
                return;
            }

            TransrenetRunner.Run(new Runnable(_property.GetStartInfo()));
            OnEnd();
        }

        public override void Cancel()
        {
            //FIX ME надо ли?)
        }
    }
}