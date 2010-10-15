using System;
using System.Diagnostics;
using System.Threading;
using com.jds.AWLauncher.classes.forms;

namespace com.jds.AWLauncher.classes.utils
{
    public class TransrenetRunner
    {
       // public static IntPtr ID;

        public static void Run(ProcessStartInfo a)
        {
            for (var i = 1; i <= 100; i += 1)
            {
                float val = 100 - i;
                MainForm.Instance.setOpacity(val/100F);
                Thread.Sleep(10);
            }

            ThreadStart threadStart = delegate
            {
                Process pp = Process.Start(a);
                if (pp == null)
                {
                    return;
                }
            };

            Thread t = new Thread(threadStart);
            t.Start();   

            MainForm.Instance.CloseWithout(0);
        }
    }
}