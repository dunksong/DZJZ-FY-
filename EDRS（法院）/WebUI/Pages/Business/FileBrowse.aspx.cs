using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using EDRS.Common;
using EDRS.BLL;
using System.IO;
using System.Text.RegularExpressions;

namespace WebUI.Pages.Business
{
    public partial class FileBrowse : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] t = Request.PathInfo.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            string type = Request["t"];

            if (!string.IsNullOrEmpty(type))
            {
                Response.ContentEncoding = Encoding.UTF8;
                Response.ContentType = "application/json";
                if (type.Equals("ListBind"))
                {
                    int count = 0;
                    TYYW_GG_AJJBXX bll = new TYYW_GG_AJJBXX(Request);
                    DataTable dt = bll.ListBind(this.Request, UserInfo.DWBM, UserInfo.GH, "1,2,3,4,5,6","", ref count);
                    Response.Write("{\"Total\":" + count + ",\"Rows\":" + JsonHelper.JsonString(dt) + "}");
                }
                if (type.Equals("GetMlTree"))
                {
                    EDRS.BLL.YX_DZJZ_JZML bll = new EDRS.BLL.YX_DZJZ_JZML(Request);
                    Response.Write(bll.GetMlTree(Request, false, true));
                }
                Response.End();
            }
        }

     

        #region 绑定列表
        /// <summary>
        /// 绑定列表
        /// </summary>
        /// <returns></returns>
        public string ListBind()
        {
            string page = Request["page"];
            string rows = Request["pagesize"];
            string key = Request["key"];

            int pageNumber = int.Parse(page);
            int pageSize = int.Parse(rows);
            string where = string.Empty;

            object[] values = new object[1];
            if (!string.IsNullOrEmpty(key))
            {
                where += " and CONFIGNAME like :CONFIGNAME ";
                values[0] = "%" + key + "%";
            }

            EDRS.BLL.YX_DZJZ_FMDY bll = new EDRS.BLL.YX_DZJZ_FMDY(this.Request);

            DataSet ds = bll.GetListByPage(where, "", (pageSize * pageNumber) - pageSize + 1, (pageSize * pageNumber), values);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "绑定打印封面数据成功", UserInfo, UserRole, this.Request);
                int count = bll.GetRecordCount(where, values);

                return "{\"Total\":" + count + ",\"Rows\":" + JsonHelper.JsonString(ds.Tables[0]) + "}";
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "绑定打印封面数据未找到信息", UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "未找到信息", null);
        } 
        #endregion

      
    }
}