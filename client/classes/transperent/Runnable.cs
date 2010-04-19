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
                Process.Start(_run);
            }
            catch
            {
            }
        }
    }
}