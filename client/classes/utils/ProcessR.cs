using System;
using System.Diagnostics;
using System.Threading;

namespace com.jds.AWLauncher.classes.utils
{
    public class ProcessR
    {
        public static void Start(String a)
        {
            ThreadStart threadStart = delegate
                                          {
                                              try { Process.Start(a); }
                                              catch
                                              {
                                              }
                                          };
            
            Start(threadStart);
        }

        public static void Start(ProcessStartInfo a)
        {
            ThreadStart threadStart = delegate
            {
                try{Process.Start(a);}
                catch
                {
                }
            };
            Start(threadStart);
        }

        private static void Start(ThreadStart a)
        {
            var t = new Thread(a);
            t.Start();   
        }
    }
}
