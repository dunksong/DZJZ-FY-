using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net.Core;

namespace Cyvation.CCQE.Common
{
    public class LogExtentionImplete : LogImpl, ILogExtention
    {
        #region 字段属性
        // 类型全称
        private readonly static Type ThisDeclaringType = typeof(LogExtentionImplete);
        #endregion

        #region 构造函数
        public LogExtentionImplete(ILogger logger)
            : base(logger)
        {
        }
        #endregion

        #region 接口ILogExtention实现

        #region Debug
        public void Debug(string runInfo, object obj = null)
        {
            Debug(runInfo, null, obj);
        }
        public void Debug(string runInfo, System.Exception ex, object obj = null)
        {
            if (this.IsDebugEnabled)
            {
                LoggingEvent loggingEvent = new LoggingEvent(ThisDeclaringType, Logger.Repository, Logger.Name, Level.Debug, "", ex);
                if (obj != null)
                {
                    BaseUserInfo userInfo = obj as BaseUserInfo;
                    if (userInfo != null)
                    {
                        loggingEvent.Properties["DWBM"] = userInfo.DWMC;
                        loggingEvent.Properties["OperatorID"] = userInfo.GH;
                        loggingEvent.Properties["OperatorName"] = userInfo.MC;
                    }
                }
                loggingEvent.Properties["RunInfo"] = runInfo;
                Logger.Log(loggingEvent);
            }
        }
        public void Debug(string dwbm, string operatorName, string runInfo)
        {
            Debug(dwbm, "", operatorName, runInfo, null);
        }
        public void Debug(string dwbm, string operatorID, string operatorName, string runInfo)
        {
            Debug(dwbm, operatorID, operatorName, runInfo, null);
        }
        public void Debug(string dwbm, string operatorID, string operatorName, string runInfo, System.Exception ex)
        {
            if (this.IsDebugEnabled)
            {
                LoggingEvent loggingEvent = new LoggingEvent(ThisDeclaringType, Logger.Repository, Logger.Name, Level.Debug, "", ex);
                loggingEvent.Properties["DWBM"] = dwbm;
                loggingEvent.Properties["OperatorID"] = operatorID;
                loggingEvent.Properties["OperatorName"] = operatorName;
                loggingEvent.Properties["RunInfo"] = runInfo;
                Logger.Log(loggingEvent);
            }
        }
        #endregion

        #region Info
        public void Info(string message, object obj = null)
        {
            Info(message, null, obj);
        }
        public void Info(string message, Exception ex, object obj = null)
        {
            if (this.IsInfoEnabled)
            {
                LoggingEvent loggingEvent = new LoggingEvent(ThisDeclaringType, Logger.Repository, Logger.Name, Level.Info, message, ex);
                if (obj != null)
                {
                    BaseUserInfo userInfo = obj as BaseUserInfo;
                    if (userInfo != null)
                    {
                        loggingEvent.Properties["DWBM"] = userInfo.DWMC;
                        loggingEvent.Properties["OperatorID"] = userInfo.GH;
                        loggingEvent.Properties["OperatorName"] = userInfo.MC;
                    }
                }
                Logger.Log(loggingEvent);
            }
        }
        public void Info(string dwbm, string operatorName, string message)
        {
            Info(dwbm, "", operatorName, message, null);
        }
        public void Info(string dwbm, string operatorID, string operatorName, string message)
        {
            Info(dwbm, operatorID, operatorName, message, null);
        }
        public void Info(string dwbm, string operatorID, string operatorName, string message, Exception ex)
        {
            if (this.IsInfoEnabled)
            {
                LoggingEvent loggingEvent = new LoggingEvent(ThisDeclaringType, Logger.Repository, Logger.Name, Level.Info, message, ex);
                loggingEvent.Properties["DWBM"] = dwbm;
                loggingEvent.Properties["OperatorID"] = operatorID;
                loggingEvent.Properties["OperatorName"] = operatorName;
                Logger.Log(loggingEvent);
            }
        }
        #endregion

        #region Warn
        public void Warn(string message, object obj = null)
        {
            Warn(message, null, obj);
        }
        public void Warn(string message, Exception ex, object obj = null)
        {
            if (this.IsWarnEnabled)
            {
                LoggingEvent loggingEvent = new LoggingEvent(ThisDeclaringType, Logger.Repository, Logger.Name, Level.Warn, message, ex);
                if (obj != null)
                {
                    BaseUserInfo userInfo = obj as BaseUserInfo;
                    if (userInfo != null)
                    {
                        loggingEvent.Properties["DWBM"] = userInfo.DWMC;
                        loggingEvent.Properties["OperatorID"] = userInfo.GH;
                        loggingEvent.Properties["OperatorName"] = userInfo.MC;
                    }
                }
                Logger.Log(loggingEvent);
            }
        }
        public void Warn(string dwbm, string operatorName, string message)
        {
            Warn(dwbm, "", operatorName, message, null);
        }
        public void Warn(string dwbm, string operatorID, string operatorName, string message)
        {
            Warn(dwbm, operatorID, operatorName, message, null);
        }
        public void Warn(string dwbm, string operatorID, string operatorName, string message, Exception ex)
        {
            if (this.IsWarnEnabled)
            {
                LoggingEvent loggingEvent = new LoggingEvent(ThisDeclaringType, Logger.Repository, Logger.Name, Level.Warn, message, ex);
                loggingEvent.Properties["DWBM"] = dwbm;
                loggingEvent.Properties["OperatorID"] = operatorID;
                loggingEvent.Properties["OperatorName"] = operatorName;
                Logger.Log(loggingEvent);
            }
        }
        #endregion

        #region Error
        public void Error(string message, string runInfo, object obj = null)
        {
            Error(message, runInfo, null, obj);
        }
        public void Error(string message, string runInfo, Exception ex, object obj = null)
        {
            if (this.IsErrorEnabled)
            {
                LoggingEvent loggingEvent = new LoggingEvent(ThisDeclaringType, Logger.Repository, Logger.Name, Level.Error, message, ex);
                if (obj != null)
                {
                    BaseUserInfo userInfo = obj as BaseUserInfo;
                    if (userInfo != null)
                    {
                        loggingEvent.Properties["DWBM"] = userInfo.DWMC;
                        loggingEvent.Properties["OperatorID"] = userInfo.GH;
                        loggingEvent.Properties["OperatorName"] = userInfo.MC;
                    }
                }
                loggingEvent.Properties["RunInfo"] = runInfo;
                Logger.Log(loggingEvent);

                //出错时添加调试信息
                Debug(runInfo, obj);
            }
        }
        public void Error(string dwbm, string operatorName, string message, string runInfo)
        {
            Error(dwbm, "", operatorName, message, runInfo, null);
        }
        public void Error(string dwbm, string operatorID, string operatorName, string message, string runInfo)
        {
            Error(dwbm, operatorID, operatorName, message, runInfo, null);
        }
        public void Error(string dwbm, string operatorID, string operatorName, string message, string runInfo, Exception ex)
        {
            if (this.IsErrorEnabled)
            {
                LoggingEvent loggingEvent = new LoggingEvent(ThisDeclaringType, Logger.Repository, Logger.Name, Level.Error, message, ex);
                loggingEvent.Properties["DWBM"] = dwbm;
                loggingEvent.Properties["OperatorID"] = operatorID;
                loggingEvent.Properties["OperatorName"] = operatorName;
                loggingEvent.Properties["RunInfo"] = runInfo;
                Logger.Log(loggingEvent);

                //出错时添加调试信息
                Debug(dwbm, operatorID, operatorName, runInfo, ex);
            }
        }
        #endregion

        #region Fatal
        public void Fatal(string message, object obj = null)
        {
            Fatal(message, null, obj);
        }
        public void Fatal(string message, Exception ex, object obj = null)
        {
            if (this.IsFatalEnabled)
            {
                LoggingEvent loggingEvent = new LoggingEvent(ThisDeclaringType, Logger.Repository, Logger.Name, Level.Fatal, message, ex);
                if (obj != null)
                {
                    BaseUserInfo userInfo = obj as BaseUserInfo;
                    if (userInfo != null)
                    {
                        loggingEvent.Properties["DWBM"] = userInfo.DWMC;
                        loggingEvent.Properties["OperatorID"] = userInfo.GH;
                        loggingEvent.Properties["OperatorName"] = userInfo.MC;
                    }
                }
                Logger.Log(loggingEvent);
            }
        }
        public void Fatal(string dwbm, string operatorName, string message)
        {
            Fatal(dwbm, "", operatorName, message, null);
        }
        public void Fatal(string dwbm, string operatorID, string operatorName, string message)
        {
            Fatal(dwbm, operatorID, operatorName, message, null);
        }
        public void Fatal(string dwbm, string operatorID, string operatorName, string message, Exception ex)
        {
            if (this.IsFatalEnabled)
            {
                LoggingEvent loggingEvent = new LoggingEvent(ThisDeclaringType, Logger.Repository, Logger.Name, Level.Fatal, message, ex);
                loggingEvent.Properties["DWBM"] = dwbm;
                loggingEvent.Properties["OperatorID"] = operatorID;
                loggingEvent.Properties["OperatorName"] = operatorName;
                Logger.Log(loggingEvent);
            }
        }
        #endregion

        #endregion
    }
}

