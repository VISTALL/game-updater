using System;
using System.Diagnostics;
using System.Threading;

namespace com.jds.AWLauncher.classes.utils
{
    public class ProcessR
    {
        public static void Start(String a)
        {
            ThreadStart threadStart = () => Process.Start(a);
            
            Start(threadStart);
        }

        private static void Start(ThreadStart a)
        {
            var t = new Thread(a);
            t.Start();   
        }
    }
}
