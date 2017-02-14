using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cyvation.CCQE.DataAccess
{
    class LogWriter
    {
        private LogWriter() { }

        public static void Write(Exception ex)
        {
            System.Diagnostics.Trace.WriteLine(ex);
        }

        public static void Write(string message, Exception ex)
        {
            System.Diagnostics.Trace.WriteLine(message + Environment.NewLine + ex.Message);
        }
    }
}
