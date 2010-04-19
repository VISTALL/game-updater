using System.Threading;
using com.jds.GUpdater.classes.forms;

namespace com.jds.GUpdater.classes.transperent
{
    public class TransrenetRunner
    {
        public static void Run(Runnable a)
        {
            for (int i = 1; i <= 100; i += 1)
            {
                float val = 100 - i;
                MainForm.Instance.setOpacity(val/100F);
                Thread.Sleep(10);
            }

            a.run();

            MainForm.Instance.CloseWithout();
        }
    }
}