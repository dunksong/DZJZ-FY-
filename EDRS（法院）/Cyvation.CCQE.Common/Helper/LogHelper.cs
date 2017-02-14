using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using log4net.Core;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace Cyvation.CCQE.Common
{
    public static class LogHelper
    {
        // 日志记录
        private static Lazy<ILogExtention> _logger;

        /// <summary>
        /// Log4net日志操作方法
        /// </summary>
        public static ILogExtention Logger
        {
            get
            {
                if (null == _logger)
                {

                    _logger = new Lazy<ILogExtention>(() => LogManagerExtention.GetLogger("MyLogger"), isThreadSafe: true);
                }

                return _logger.Value;
            }
        }

        public static ILogExtention Log(this object obj)
        {
            return LogManagerExtention.GetLogger(obj.GetType());
        }

        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="ex"></param>
        #region static void WriteLog(Type t, Exception ex)

        public static void WriteLog(Type t, Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Error("Error", ex);
        }

        #endregion

        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="msg"></param>
        #region static void WriteLog(Type t, string msg)

        public static void WriteLog(Type t, string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Error(msg);
        }
        #endregion
    }
}
