using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDRS.Common
{
    /// <summary>
    /// 设置提示类型
    /// </summary>
    public enum Prompt
    {
        error = 0, win = 1
    }

    public class ReturnString
    {
        /// <summary>
        /// 提示信息json字符串
        /// </summary>
        /// <param name="em"></param>
        /// <param name="msg"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string JsonToString(Enum em, string msg, string key)
        {
            return "{\"t\":\"" + em.ToString() + "\",\"v\":\"" + msg + "\",\"k\":\"" + (key == null ? "" : key.ToString()) + "\"}";
        }

        /// <summary>
        /// 提示信息json字符串
        /// </summary>
        /// <param name="em"></param>
        /// <param name="msg"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string JsonToString(Enum em, string msg, string key,string fn)
        {
            return "{\"t\":\"" + em.ToString() + "\",\"v\":\"" + msg + "\",\"k\":\"" + (key == null ? "" : key.ToString()) + "\",\"fn\":\""+fn+"\"}";
        }


    }
}
