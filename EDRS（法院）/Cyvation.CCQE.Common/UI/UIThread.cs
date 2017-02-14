using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cyvation.CCQE.Common
{
    public static class UIThread
    {
        
        public static void Invoke(this Control ctrl, MethodInvoker code)
        {
            if (ctrl == null || ctrl.Disposing) return;
            if (ctrl.InvokeRequired)
            {
                if (!(ctrl.IsDisposed && ctrl.Disposing))
                {
                    ctrl.Invoke(code);
                }
            }
            else
            {
                code.Invoke();
            }
        }

        public static void BeginInvoke(this Control ctrl, MethodInvoker code)
        {
            if (ctrl == null || ctrl.Disposing) return;
            if (ctrl.InvokeRequired)
            {
                if (!(ctrl.IsDisposed && ctrl.Disposing))
                {
                    ctrl.BeginInvoke(code);
                }
            }
            else
            {
                ctrl.BeginInvoke(code);
            }
        }
    }
}
