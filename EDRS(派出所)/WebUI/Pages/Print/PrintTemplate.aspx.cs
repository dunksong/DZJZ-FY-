using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EDRS.BLL;
using EDRS.Common;

namespace WebUI.Pages.Print
{
    public partial class PrintTemplate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] t = Request.PathInfo.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            string type = Request["t"];

            if (!string.IsNullOrEmpty(type))
            {
                Response.ContentEncoding = Encoding.UTF8;
                Response.ContentType = "application/json";
                if (type.Equals("save"))
                    Response.Write(AddData());          

                Response.End();
            }
        }


        
        /// <summary>
        /// 添加配置数据
        /// </summary>
        /// <returns></returns>
        private string AddData()
        {
            string msg = string.Empty;
            //if (!ProvingFrom(ref msg))
            //{
            //    return ReturnString.JsonToString(Prompt.error, msg, null);
            //}
            YX_DZJZ_FMDYMB bll = new YX_DZJZ_FMDYMB(this.Request);

            EDRS.Model.YX_DZJZ_FMDYMB model = new EDRS.Model.YX_DZJZ_FMDYMB();
            model.BT = Request.Form.Get("title");
            model.NR = Request.Form.Get("value");       
            model.CZRGH = "";
            model.CZR ="";
            model.CZSJ = DateTime.Now;
            model.CZIP = Request.ServerVariables["REMOTE_ADDR"];
        

            if (bll.Add(model))
            {
                //数据日志
              //  OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "打印成功", "", UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.win, "保存成功", null);
            }
            //数据日志
           // OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "打印失败", "", UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "保存失败", null);
        }
    }
}