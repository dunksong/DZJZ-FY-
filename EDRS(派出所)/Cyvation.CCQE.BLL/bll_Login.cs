using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Cyvation.CCQE.Common;

namespace Cyvation.CCQE.BLL
{
    public class bll_Login
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public static DataTable Login(string dwbm, string user, string passwd, out string errmsg)
        {
            DataTable dt = null;
            errmsg = string.Empty;
            ParamConvert pc = new ParamConvert();
            pc.Basic(true);
            pc.Add("p_dwbm", dwbm);
            pc.Add("p_user", user);
            passwd = MD5Encrypt.getMd5Hash(passwd);
            pc.Add("p_passwd", passwd);
            try
            {
                dt = pc.DoExecuteDataTable("pkg_zzjg_manage.proc_login");
            }
            catch (Exception e)
            {
                errmsg = e.Message;
            }
            return dt;
        }
    }
}
