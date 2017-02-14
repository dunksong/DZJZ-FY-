using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Cyvation.CCQE.Common
{
    public static class ScriptHelper
    {

        /// <summary>
        /// 用Popup显示提示信息
        /// </summary>
        /// <param name="msg"></param>
        public static void Alert(this Page page, string msg)
        {
            string key = "msg";
            if (!page.ClientScript.IsClientScriptBlockRegistered(page.GetType(), key))
            {
                msg = string.Format("Alert('{0}');", msg.Replace("\n", "").Replace("'", "\""));
                page.ClientScript.RegisterClientScriptBlock(page.GetType(), key, msg, true);
            }
        }

        /// <summary>
        /// 用Popup显示提示信息
        /// </summary>
        /// <param name="msg"></param>
        public static void ParentAlert(this Page page, string msg)
        {
            string key = "msg";
            if (!page.ClientScript.IsClientScriptBlockRegistered(page.GetType(), key))
            {
                msg = string.Format("window.parent.Alert('{0}');", msg.Replace("\n", "").Replace("'", "\""));
                page.ClientScript.RegisterClientScriptBlock(page.GetType(), key, msg, true);
            }
        }

        /// <summary>
        /// 注册并执行JS脚本
        /// </summary>
        /// <param name="script"></param>
        public static void Script(this Page page, string script)
        {
            if (string.IsNullOrEmpty(script)) return;
            string key = "fun" + DateTime.Now.Millisecond;
            if (!page.ClientScript.IsClientScriptBlockRegistered(page.GetType(), key))
            {
                page.ClientScript.RegisterClientScriptBlock(page.GetType(), key, script, true);
            }
        }

        /// <summary>
        /// 弹出提示信息，关闭提示信息后关闭Popup
        /// </summary>
        /// <param name="page"></param>
        /// <param name="msg">提示信息</param>
        public static void AlertAndDo(this Page page, string msg, string doFn)
        {
            string key = "msg";
            if (!page.ClientScript.IsClientScriptBlockRegistered(page.GetType(), key))
            {
                msg = string.Format("AlertAndDo('{0}', {1});", msg.Replace("\n", "").Replace("'", "\""), doFn);
                page.ClientScript.RegisterClientScriptBlock(page.GetType(), key, msg, true);
            }
        }

        /// <summary>
        /// 弹出提示信息，关闭提示信息后关闭Popup
        /// </summary>
        /// <param name="page"></param>
        /// <param name="msg">提示信息</param>
        public static void AlertAndClose(this Page page, string msg)
        {
            string key = "msg";
            if (!page.ClientScript.IsClientScriptBlockRegistered(page.GetType(), key))
            {
                msg = string.Format("AlertAndClose('{0}');", msg.Replace("\n", "").Replace("'", "\""));
                page.ClientScript.RegisterClientScriptBlock(page.GetType(), key, msg, true);
            }
        }

        /// <summary>
        /// 弹出提示信息，关闭提示信息后跳转
        /// </summary>
        public static void AlertAndSkip(this Page page, string msg, string url)
        {
            Script(page, string.Format("AlertAndSkip('{0}', '{1}');", msg.Replace("\n", "").Replace("'", "\""), url));
        }

        /// <summary>
        /// 弹出提示信息，关闭提示信息后关闭Popup，并父界面跳转
        /// </summary>
        public static void AlertCloseAndParentSkip(this Page page, string msg, string url)
        {
            Script(page, string.Format("popupAlertCloseAndParentSkip('{0}', '{1}');", msg.Replace("\n", "").Replace("'", "\""), url));
        }

        /// <summary>
        /// 弹出提示信息，关闭提示信息后关闭Popup，并父界面跳转
        /// </summary>
        public static void ParentAlertCloseAndSkip(this Page page, string msg, string url)
        {
            Script(page, string.Format("window.parent.AlertAndSkip('{0}', '{1}');", msg.Replace("\n", "").Replace("'", "\""), url));
        }

        /// <summary>
        /// 关闭Popup，不刷新
        /// </summary>
        public static void Close(this Page page)
        {
            Script(page, "popupClose();");
        }

        /// <summary>
        /// 关闭Popup并跳转
        /// </summary>
        /// <param name="page"></param>
        /// <param name="url">跳转地址</param>
        public static void CloseAndSkip(this Page page, string url)
        {
            Script(page, "CloseAndSkip('" + url + "');");
        }

        /// <summary>
        /// 关闭Popup，父界面跳转
        /// </summary>
        public static void CloseAndParentSkip(this Page page, string url)
        {
            Script(page, "CloseAndParentSkip('" + url + "');");
        }

        /// <summary>
        /// 页面跳转
        /// </summary>
        public static void Skip(this Page page, string url)
        {
            Script(page, "Skip('" + url + "')");
        }
    }
}
