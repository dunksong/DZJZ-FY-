using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDRS.Common;
using System.Text;
using System.Data;

namespace WebUI.Pages.LSYJ
{
    public partial class ReadingShow : BasePage
    {

        /// <summary>
        /// 获取当前时间
        /// </summary>
        protected DateTime nowTime = DateTime.Now;

        #region 页面加载
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["YJUser"] != null)
            //    YJUser = JsonHelper.JsonString(Session["YJUser"] as EDRS.Model.YX_DZJZ_LSAJBD);

            string type = Request["t"];

            if (!string.IsNullOrEmpty(type))
            {
                Response.ContentEncoding = Encoding.UTF8;
                Response.ContentType = "application/json";

                if (type.Equals("ReadLogin"))
                    Response.Write(ReadLogin());
                if (type.Equals("GetMlTree"))
                {
                    EDRS.BLL.YX_DZJZ_JZML bll = new EDRS.BLL.YX_DZJZ_JZML(Request);
                    Response.Write(bll.GetMlTree(Request, false, true));
                }
                if (type.Equals("ListBind"))
                    Response.Write(ListBind());
                Response.End();
            }
        } 
        #endregion

        #region 登录账号阅卷
        /// <summary>
        /// 登录账号阅卷
        /// </summary>
        /// <returns></returns>
        private string ReadLogin()
        {

            string where = string.Empty;

            //string yjzh = Request.Form["txt_yjzh"].Trim();
            //string yjmm = Request.Form["txt_yjmm"].Trim();

            string yjxh = Request["yjxh"];

            //where += " and YJZH='" + StringPlus.ReplaceSingle(yjzh) + "' and YJMM='" + StringPlus.ReplaceSingle(yjmm) + "' ";
            where += " and YJXH='"+StringPlus.ReplaceSingle(yjxh)+"'";
            where += " and GH='" + UserInfo.GH + "'";
            where += " and DWBM='" + UserInfo.DWBM + "'";
            where += " and YJKSSJ <=to_date('" + DateTime.Now + "','yyyy-mm-dd hh24:mi:ss')  ";
            where += " and YJJSSJ >=to_date('" + DateTime.Now + "','yyyy-mm-dd hh24:mi:ss')  ";
            object[] values = new object[0];

            EDRS.BLL.YX_DZJZ_LSAJBD bll = new EDRS.BLL.YX_DZJZ_LSAJBD(Request);
            List<EDRS.Model.YX_DZJZ_LSAJBD> listmodel = bll.GetModelList(where, values);
            if (listmodel != null && listmodel.Count > 0)
            {
                Session["YjData"] = listmodel[0];
                return JsonHelper.JsonString(listmodel);
            }
            return ReturnString.JsonToString(Prompt.error, "请确认阅卷时间是否未开始或者已结束", null);
        } 
        #endregion
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
            string where = " and T.SFSC='N'";

            object[] values = new object[1];
            if (!string.IsNullOrEmpty(key))
            {
                where += " and YJSQDH like :YJSQDH";
                values[0] = "%" + key + "%";
            }
            where += " and T.GH='" + UserInfo.GH + "' and T.DWBM='" + UserInfo.DWBM + "'";
          
            EDRS.BLL.YX_DZJZ_LSAJBD bll = new EDRS.BLL.YX_DZJZ_LSAJBD(this.Request);

            DataSet ds = bll.GetListByPage(where, "YJJSSJ desc,JDSJ desc", (pageSize * pageNumber) - pageSize + 1, (pageSize * pageNumber), values);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.阅卷Web, "获取阅卷绑定列表成功", UserInfo, UserRole, this.Request);
                int count = bll.GetRecordCount(where, values);
                return "{\"Total\":" + count + ",\"Rows\":" + JsonHelper.JsonString(ds.Tables[0]) + "}";
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.阅卷Web, "获取阅卷绑定列表-未找到信息", UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "未找到阅卷绑定信息", null);
        }
        #endregion
    }
}