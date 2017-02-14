using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cyvation.CCQE.Common
{
    /// <summary>
    /// Log4扩展接口，实现自定义列记录
    /// </summary>
    public interface ILogExtention
    {
        /// <summary>
        /// 调试
        /// </summary>
        /// <param name="message">调试相关信息</param>
        /// <param name="obj">用户信息，当前为Userinfo</param>
        void Debug(string runInfo, object obj = null);
        /// <summary>
        /// 调试
        /// </summary>
        /// <param name="message">调试相关信息</param>
        /// <param name="ex">异常信息</param>
        /// <param name="obj">用户信息，当前为Userinfo</param>
        void Debug(string runInfo, Exception ex, object obj = null);
        /// <summary>
        /// 调试
        /// </summary>
        /// <param name="dwbm">单位编码</param>
        /// <param name="operatorName">操作人姓名</param>
        /// <param name="message">调试相关信息</param>
        void Debug(string dwbm, string operatorName, string runInfo);
        /// <summary>
        /// 调试
        /// </summary>
        /// <param name="dwbm">单位编码</param>
        /// <param name="operatorID">操作人工号</param>
        /// <param name="operatorName">操作人姓名</param>
        /// <param name="message">调试相关信息</param>
        void Debug(string dwbm, string operatorID, string operatorName, string runInfo);
        /// <summary>
        /// 调试
        /// </summary>
        /// <param name="dwbm">单位编码</param>
        /// <param name="operatorID">操作人工号</param>
        /// <param name="operatorName">操作人姓名</param>
        /// <param name="message">调试相关信息</param>
        /// <param name="ex">异常信息</param>
        void Debug(string dwbm, string operatorID, string operatorName, string runInfo, Exception ex);

        /// <summary>
        /// 操作日志信息
        /// </summary>
        /// <param name="message">操作相关信息</param>
        /// <param name="obj">用户信息，当前为Userinfo</param>
        void Info(string message, object obj = null);
        /// <summary>
        /// 操作日志信息
        /// </summary>
        /// <param name="message">操作相关信息</param>
        /// <param name="ex">异常信息</param>
        /// <param name="obj">用户信息，当前为Userinfo</param>
        void Info(string message, Exception ex, object obj = null);
        /// <summary>
        /// 操作日志信息
        /// </summary>
        /// <param name="dwbm">单位编码</param>
        /// <param name="operatorName">操作人姓名</param>
        /// <param name="obj">用户信息，当前为Userinfo</param>
        void Info(string dwbm, string operatorName, string message);
        /// <summary>
        /// 操作日志信息
        /// </summary>
        /// <param name="dwbm">单位编码</param>
        /// <param name="operatorID">操作人工号</param>
        /// <param name="operatorName">操作人姓名</param>
        /// <param name="obj">用户信息，当前为Userinfo</param>
        void Info(string dwbm, string operatorID, string operatorName, string message);
        /// <summary>
        /// 操作日志信息
        /// </summary>
        /// <param name="dwbm">单位编码</param>
        /// <param name="operatorID">操作人工号</param>
        /// <param name="operatorName">操作人姓名</param>
        /// <param name="message">调试相关信息</param>
        /// <param name="obj">用户信息，当前为Userinfo</param>
        void Info(string dwbm, string operatorID, string operatorName, string message, Exception ex);

        void Warn(string message, object obj = null);
        void Warn(string message, Exception ex, object obj = null);
        void Warn(string dwbm, string operatorName, string message);
        void Warn(string dwbm, string operatorID, string operatorName, string message);
        void Warn(string dwbm, string operatorID, string operatorName, string message, Exception ex);

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="message">错误日志信息</param>
        /// <param name="obj"></param>
        void Error(string message, string runInfo, object obj = null);
        void Error(string message, string runInfo, Exception ex, object obj = null);
        void Error(string dwbm, string operatorName, string message, string runInfo);
        void Error(string dwbm, string operatorID, string operatorName, string message, string runInfo);
        void Error(string dwbm, string operatorID, string operatorName, string message, string runInfo, Exception ex);

        void Fatal(string message, object obj = null);
        void Fatal(string message, Exception ex, object obj = null);
        void Fatal(string dwbm, string operatorName, string message);
        void Fatal(string dwbm, string operatorID, string operatorName, string message);
        void Fatal(string dwbm, string operatorID, string operatorName, string message, Exception ex);
    }
}
