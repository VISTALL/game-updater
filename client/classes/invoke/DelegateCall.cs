using System;
using System.Windows.Forms;

namespace com.jds.AWLauncher.classes.invoke
{
    public class DelegateCall
    {
        private readonly ContainerControl _form;
        private readonly Delegate _delegate;
        private readonly Object[] _objects;

        private readonly object  _lock = new object();

        public DelegateCall(ContainerControl f, Delegate d, params Object[] p)
        {
            _form = f;
            _delegate = d;
            _objects = p;
        }

        public bool Invoke()
        {
            lock (_lock)
            {
                if (_form != null && _delegate != null)
                {
                    if (_form.Visible && !_form.IsDisposed && !_form.Disposing)
                    {

                        _form.Invoke(_delegate, _objects);
                    }

                    return _form.Visible;
                }

                return true;
            }
        }

        public override string ToString()
        {
            return "DelegateCall: Form: " + (_form != null
                       ? _form.GetType().Name
                       : "n/a") + "; Method: " + (_delegate != null ? _delegate.Method.Name : "n/a");
        }
    }
}