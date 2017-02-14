using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Cyvation.CCQE.BLL;
using Cyvation.CCQE.Common;
using System.Configuration;

namespace Cyvation.CCQE.Web
{
    /// <summary>
    /// DwSource 的摘要说明
    /// </summary>
    public class DwSource : AshxBase
    {

        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);
            //string dwbm = UserInfo.DWBM;
            string dwbm = ConfigurationManager.AppSettings.Get("DWBM");
            if (UserInfo != null)
            {
                dwbm = UserInfo.DWBM;
            }
            string errmsg = string.Empty;
            try
            {
                DataTable dt = ZzjgManage.GetAllDwByDwbm(dwbm, out errmsg);
                if (dt != null && string.IsNullOrEmpty(errmsg))
                {
                    //找到顶级编码
                    DataRow[] dr = dt.Select("BM" + " = '" + ConfigurationManager.AppSettings.Get("DWBM").ToString() + "'");
                    string topFbm = dr[0]["FBM"].ToString();
                    topFbm = "1" + topFbm;
                    //转化编码字段
                    dt = DataProHelper.ProBMAddOne(dt, "BM");
                    dt = DataProHelper.ProBMAddOne(dt, "FBM");
                    //获取json数据
                    string json = dt.ToTreeJson("BM", "FBM", "MC", topFbm);
                    context.Response.Write(json);
                }
                else
                {
                    /*
                    * 记录日志
                    */
                    string errInfo = string.Empty;
                    errInfo += "当前文件:" + CodeHelper.GetCurSourceFileName() + "\r\n";
                    errInfo += "当前行:" + CodeHelper.GetLineNum().ToString() + "\r\n";
                    errInfo += "调用函数异常:" + "public static DataTable GetAllDwByDwbm(string dwbm, out string errmsg);" + "\r\n";
                    errInfo += "传入参数:" + dwbm.ToString() + "\r\n";
                    errInfo += errmsg;
                    Logger.Error("获取单位失败", errInfo);
                    context.Response.Write(errInfo);
                }
            }
            catch (Exception e)
            {
                context.Response.Write(e.Message);
            }
            finally
            {
                context.Response.End();
            }
        }
    }
}