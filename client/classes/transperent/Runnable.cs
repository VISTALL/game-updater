using System.Diagnostics;

namespace com.jds.GUpdater.classes.transperent
{
    public class Runnable
    {
        private readonly ProcessStartInfo _run;

        public Runnable(ProcessStartInfo d)
        {
            _run = d;
        }

        public void run()
        {
            try
            {
                if (_run != null)
                {
                    Process.Start(_run);
                }
            }
            catch
            {
            }
        }
    }
}