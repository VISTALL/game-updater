using System;
using System.Collections.Generic;
using System.Threading;
using com.jds.GUpdater.classes.utils;

namespace com.jds.GUpdater.classes.invoke
{
    public class InvokeManager
    {
        private volatile /*readonly*/ Queue<DelegateCall> _calls = new Queue<DelegateCall>(); 
        
        private readonly Thread _mainThread;
        private const int INTERVAL = 10;

        public InvokeManager(Type t)
        {
            ThreadStart d = delegate
            {
                while (!Shutdown)
                {
                    try
                    {
                      //  if (Free)
                        {
                            var delegateCall = Next;

                            if (delegateCall != null)
                            {
                                if (!delegateCall.Invoke())
                                {
                                    AddInvoke(delegateCall);
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                    
                    }   
                    finally
                    {
                        Thread.Sleep(INTERVAL);
                    }
                }
            }; 
            
            _mainThread = new Thread(d) {Name = "Invoke Manager Thread:  " + t.Name, Priority = ThreadPriority.Highest};
            _mainThread.Start();
        }

       
        public void AddInvoke(DelegateCall c)
        {
            if(!c.Invoke())
            {
                _calls.Enqueue(c);   
            }
        }

        public DelegateCall Next
        {
            get { return _calls.Count <= 0 ? null : _calls.Dequeue(); }
        }

        public bool Shutdown
        {
            get; set;
        }
    }
}
