using System;
using System.Windows.Forms;

namespace com.jds.AWLauncher.classes.invoke
{
    public class DelegateCall
    {
        private readonly WeakReference _form;
        private readonly WeakReference _delegate;
        private readonly Object[] _objects;

        public DelegateCall(ContainerControl f, Delegate d, params Object[] p)
        {
            _form = new WeakReference(f);
            _delegate = new WeakReference(d);
            _objects = p;
        }

        public bool Invoke()
        {
            var form = _form.IsAlive ? (ContainerControl)_form.Target : null;
            var @delegate = _delegate.IsAlive ? (Delegate)_delegate.Target : null;

            if(form != null && @delegate != null)
            {
                if(form.Visible)
                {
                    form.Invoke(@delegate, _objects);
                }

               return form.Visible;
            }

            return true;
        }
    }
}