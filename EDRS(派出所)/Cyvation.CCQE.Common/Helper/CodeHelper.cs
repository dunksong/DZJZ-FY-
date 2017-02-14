using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cyvation.CCQE.Common
{
    /// <summary>
    /// 记录日志的帮助类
    /// </summary>
    public class CodeHelper
    {
        /// <summary>
        /// 取得当前源码的哪一行
        /// </summary>
        /// <returns></returns>
        public static int GetLineNum()
        {
            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(1, true);
            return st.GetFrame(0).GetFileLineNumber();
        }

        /// <summary>
        /// 取当前源码的源文件名
        /// </summary>
        /// <returns></returns>
        public static string GetCurSourceFileName()
        {
            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(1, true);

            return st.GetFrame(0).GetFileName();

        }
    }
}
