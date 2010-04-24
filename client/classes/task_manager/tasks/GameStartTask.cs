using com.jds.GUpdater.classes.games.propertyes;
using com.jds.GUpdater.classes.transperent;

namespace com.jds.GUpdater.classes.task_manager.tasks
{
    public class GameStartTask : AbstractTask
    {
        private readonly GameProperty _property;
        private string _toString;

        public GameStartTask(GameProperty p)
        {
            _property = p;
        }

        public override void Run()
        {
            _toString = "Game Start " + _property.GameEnum() + ":" + GetHashCode(); 
            
            OnEnd(); //FIX ME надо ли?

            if (_property  == null || _property.GetStartInfo() == null)
            {
                return;
            }

            TransrenetRunner.Run(new Runnable(_property.GetStartInfo()));
        }

        public override void Cancel()
        {
            //FIX ME надо ли?)
        }

        public override string ToString()
        {
            return _toString;
        }
    }
}