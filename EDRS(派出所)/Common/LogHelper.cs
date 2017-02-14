using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.OracleClient;
using System.Configuration;
using System.Collections;
using System.Web;

namespace EDRS.Common
{
    public static class LogHelper
    {
        private static string LogPath = AppDomain.CurrentDomain.BaseDirectory + ((ConfigurationManager.AppSettings["LogPath"] == null) ? "LogInfo" : ConfigurationManager.AppSettings["LogPath"].ToString());
        /// <summary>
        /// 底层日志写入
        /// </summary>
        /// <param name="errorClass">错误类型（Exception 类型）</param>
        /// <param name="errorMsg">错误消息</param>
        /// <param name="function">错误的执行方法</param>
        /// <param name="className">错误所在的类</param>
        /// <param name="param">执行参数</param>
        public static void LogError(HttpRequest Request, string errorClass, string errorMsg, string function, string className, string sqlStr, OracleParameter[] param)
        {
            string browser = "";
            string browserVersion = "";
            string url = "";
            if (Request != null)
            {
                browser = Request.Browser.Type;//浏览器版本
                browserVersion = Request.Browser.Version;//浏览器版本号
                if (Request != null && Request.UrlReferrer != null)
                    url = Request.UrlReferrer.ToString();//请求地址
                else if(Request != null)
                    url = Request.Url.ToString();
            }
            if (!Directory.Exists(LogPath))
            {
                Directory.CreateDirectory(LogPath);
            }
            string fileName = DateTime.Now.ToString("yyyyMMdd").ToString() + ".txt";
            string path = LogPath + "\\" + fileName;
            FileStream fs = null;
            if (File.Exists(path))
            {
                fs = new FileStream(path, FileMode.Append);
            }
            else
            {
                fs = new FileStream(path, FileMode.Create);
            }
            StreamWriter dw = new StreamWriter(fs);
            StringBuilder writeStr = new StringBuilder();
            writeStr.Append("日志时间：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            writeStr.Append(@"
");
            writeStr.Append("发起路径：" + url);
            writeStr.Append(@"
");
            writeStr.Append("客户端浏览器：" + browser);
            writeStr.Append(@"
");
            writeStr.Append("错误类型：" + errorClass);
            writeStr.Append(@"
");
            writeStr.Append("错误信息：" + errorMsg);
            writeStr.Append(@"
");
            writeStr.Append("所在方法：" + function);
            writeStr.Append(@"
");
            writeStr.Append("所在类库：" + className);
            writeStr.Append(@"
");
            writeStr.Append("SQL语句：" + sqlStr);
            writeStr.Append(@"
");
            writeStr.Append("参数列表：{");
            if (param != null)
            {
                foreach (OracleParameter op in param)
                {
                    writeStr.Append("  [" + op.ParameterName + "：" + (op.Value == null ? "null" : op.Value.ToString() )+ "]  ");
                }
            }
            writeStr.Append("  }");
            writeStr.Append(@"

");
            dw.WriteLine(writeStr);
            dw.Close();
            fs.Close();
            dw.Dispose();
            fs.Dispose();
        }
        /// <summary>
        /// 无参数的日志写入
        /// </summary>
        /// <param name="errorClass">错误类型（Exception 类型）</param>
        /// <param name="errorMsg">错误消息</param>
        /// <param name="function">错误的执行方法</param>
        /// <param name="className">错误所在的类</param>
        public static void LogError(HttpRequest Request,string errorClass, string errorMsg, string function, string className)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            LogError(Request, errorClass, errorMsg, function, className, "", param);
        }
        /// <summary>
        /// 无参数的日志写入
        /// </summary>
        /// <param name="errorClass">错误类型（Exception 类型）</param>
        /// <param name="errorMsg">错误消息</param>
        /// <param name="function">错误的执行方法</param>
        /// <param name="className">错误所在的类</param>
        public static void LogError(HttpRequest Request, string errorClass, string errorMsg, string function, string className,string strSql)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            LogError(Request, errorClass, errorMsg, function, className, strSql, param);
        }
        /// <summary>
        /// 界面层日志写入
        /// </summary>
        /// <param name="errorClass">错误类型（Exception 类型）</param>
        /// <param name="errorMsg">错误消息</param>
        /// <param name="function">错误的执行方法</param>
        /// <param name="className">错误所在的类</param>
        /// <param name="param">执行参数</param>
        public static void LogError(HttpRequest Request, string errorClass, string errorMsg, string function, string className, string sqlStr, Dictionary<string, object> param)
        {
            string browser = "";
            string browserVersion = "";
            string url = "";
            if (Request != null)
            {
                browser = Request.Browser.Type;//浏览器版本
                browserVersion = Request.Browser.Version;//浏览器版本号
                if (Request != null && Request.UrlReferrer != null)
                    url = Request.UrlReferrer.ToString();//请求地址
                else if (Request != null)
                    url = Request.Url.ToString();
            }

            if (!Directory.Exists(LogPath))
            {
                Directory.CreateDirectory(LogPath);
            }
            string fileName = DateTime.Now.ToString("yyyyMMdd").ToString() + ".txt";
            string path = LogPath + "\\" + fileName;
            FileStream fs = null;
            if (File.Exists(path))
            {
                fs = new FileStream(path, FileMode.Append);
            }
            else
            {
                fs = new FileStream(path, FileMode.Create);
            }
            StreamWriter dw = new StreamWriter(fs);
            StringBuilder writeStr = new StringBuilder();
            //日志时间  错误类型（Exception类型）    错误消息   错误所在方法  |  错误所在类  |  {  [参数1：参数1值]    [参数2：参数2值]    }
            writeStr.Append("日志时间：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            writeStr.Append(@"
");
            writeStr.Append("发起路径：" + url);
            writeStr.Append(@"
");
            writeStr.Append("客户端浏览器：" + browser);
            writeStr.Append(@"
");
            writeStr.Append("错误类型：" + errorClass);
            writeStr.Append(@"
");
            writeStr.Append("错误信息：" + errorMsg);
            writeStr.Append(@"
");
            writeStr.Append("所在方法：" + function);
            writeStr.Append(@"
");
            writeStr.Append("所在类库：" + className);
            writeStr.Append(@"
");
            writeStr.Append("SQL语句：" + sqlStr);
            writeStr.Append(@"
");
            writeStr.Append("参数列表：{");
            if (param != null)
            {
                foreach (string key in param.Keys)
                {
                    writeStr.Append("[" + key + ":" + param[key] + "]");
                }
            }
            writeStr.Append("}");
            writeStr.Append(@"



");
            dw.WriteLine(writeStr);
            dw.Close();
            fs.Close();
            dw.Dispose();
            fs.Dispose();
        }
        /// <summary>
        /// 事务的错误日志写入
        /// </summary>
        /// <param name="errorClass">错误类型（Exception 类型）</param>
        /// <param name="errorMsg">错误消息</param>
        /// <param name="function">错误的执行方法</param>
        /// <param name="className">错误所在的类</param>
        /// <param name="param">执行参数；key：sql语句，value：OracleParameter[]</param>
        public static void LogError(HttpRequest Request, string errorClass, string errorMsg, string function, string className, Hashtable param)
        {
            string browser = "";
            string browserVersion = "";
            string url = "";
            if (Request != null)
            {
                browser = Request.Browser.Type;//浏览器版本
                browserVersion = Request.Browser.Version;//浏览器版本号
                if (Request != null && Request.UrlReferrer != null)
                    url = Request.UrlReferrer.ToString();//请求地址
                else if (Request != null)
                    url = Request.Url.ToString();
            }

            if (!Directory.Exists(LogPath))
            {
                Directory.CreateDirectory(LogPath);
            }
            string fileName = DateTime.Now.ToString("yyyyMMdd").ToString() + ".txt";
            string path = LogPath + "\\" + fileName;
            FileStream fs = null;
            if (File.Exists(path))
            {
                fs = new FileStream(path, FileMode.Append);
            }
            else
            {
                fs = new FileStream(path, FileMode.Create);
            }
            StreamWriter dw = new StreamWriter(fs);
            //日志时间  错误类型（Exception类型）    错误消息   错误所在方法  |  错误所在类  |  {  [参数1：参数1值]    [参数2：参数2值]    }
            StringBuilder writeStr = new StringBuilder();
            writeStr.Append("日志时间：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            writeStr.Append(@"
");
            writeStr.Append("发起路径：" + url);
            writeStr.Append(@"
");
            writeStr.Append("客户端浏览器：" + browser);
            writeStr.Append(@"
");
            writeStr.Append("错误类型：" + errorClass);
            writeStr.Append(@"
");
            writeStr.Append("错误信息：" + errorMsg);
            writeStr.Append(@"
");
            writeStr.Append("所在方法：" + function);
            writeStr.Append(@"
");
            writeStr.Append("所在类库：" + className);
            if (param != null)
            {
                int i = 1;
                writeStr.Append(@"
  事务执行列表：");
                foreach (DictionaryEntry myDE in param)
                {
                    writeStr.Append(@"
  " + i.ToString() + "：" + myDE.Key + "  |  {");
                    i++;
                    OracleParameter[] cmdParms = (OracleParameter[])myDE.Value;
                    foreach (OracleParameter op in cmdParms)
                    {
                        writeStr.Append("  [" + op.ParameterName + ":" + op.Value + "]  ");
                    }
                    writeStr.Append("  }");
                }
            }
            writeStr.Append("}");
            writeStr.Append(@"



");
            dw.WriteLine(writeStr);
            dw.Close();
            fs.Close();
            dw.Dispose();
            fs.Dispose();
        }

        /// <summary>
        /// 底层日志写入
        /// </summary>
        /// <param name="errorClass">错误类型（Exception 类型）</param>
        /// <param name="errorMsg">错误消息</param>
        /// <param name="function">错误的执行方法</param>
        /// <param name="className">错误所在的类</param>
        /// <param name="param">执行参数</param>
        public static void LogError(HttpRequest Request, string errorMsg, string function,string errorType)
        {
            string browser = "";
            string browserVersion = "";
            string url = "";
            if (Request != null)
            {
                browser = Request.Browser.Type;//浏览器版本
                browserVersion = Request.Browser.Version;//浏览器版本号
                url = Request.Url.ToString();//请求地址
            }
            if (!Directory.Exists(LogPath))
            {
                Directory.CreateDirectory(LogPath);
            }
            string fileName = errorType+ DateTime.Now.ToString("yyyyMMdd").ToString() + ".txt";
            string path = LogPath + "\\" + fileName;
            FileStream fs = null;
            if (File.Exists(path))
            {
                fs = new FileStream(path, FileMode.Append);
            }
            else
            {
                fs = new FileStream(path, FileMode.Create);
            }
            StreamWriter dw = new StreamWriter(fs);
            StringBuilder writeStr = new StringBuilder();
            writeStr.Append("日志时间：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            writeStr.Append(@"
");
            writeStr.Append("发起路径：" + url);
            writeStr.Append(@"
");
            writeStr.Append("客户端浏览器：" + browser);
            writeStr.Append(@"
");
            writeStr.Append("错误信息：" + errorMsg);
            writeStr.Append(@"
");
            writeStr.Append("所在方法：" + function);
            writeStr.Append(@"
");
            dw.WriteLine(writeStr);
            dw.Close();
            fs.Close();
            dw.Dispose();
            fs.Dispose();
        }
    }
}
