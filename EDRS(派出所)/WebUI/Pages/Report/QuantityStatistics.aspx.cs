using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EDRS.Common;
using System.Data;

namespace WebUI.Pages.Report
{
    public partial class QuantityStatistics :BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request["t"];

            if (!string.IsNullOrEmpty(type))
            {
                Response.ContentEncoding = Encoding.UTF8;
                Response.ContentType = "application/json";
                if (type.Equals("ListBind"))
                    Response.Write(ListBind());
                if (type.Equals("GetDwSum"))
                    Response.Write(GetDwSum());
                Response.End();
            }
        }
        /// <summary>
        /// 绑定列表
        /// </summary>
        /// <returns></returns>
        public string ListBind()
        {
            string type = Request["type"];
            string page = Request["page"];
            string rows = Request["pagesize"];
            string key = Request["key"];
            string casename = Request["casename"];
            string dutyman = Request["dutyman"];
            string dwbm = Request["dwbm"];
            string timebegin = Request["timebegin"];
            string timeend = Request["timeend"];
            int pageNumber = int.Parse(page);
            int pageSize = int.Parse(rows);
            string where = string.Empty;

            object[] values = new object[0];

            if (!string.IsNullOrEmpty(key))
            {
                where += " and a.BMSAH like '%" + StringPlus.ReplaceSingle(key) + "%'";
             
            }
            if (!string.IsNullOrEmpty(casename))
            {
                where += " and a.JZMC like '%" + StringPlus.ReplaceSingle(casename) + "%'";

            }
            if (!string.IsNullOrEmpty(dwbm))
            {
                where += " and c.DWMC like '%" + StringPlus.ReplaceSingle(dwbm) + "%'";
            }
            if (!string.IsNullOrEmpty(dutyman))
            {
                where += " and a.JZSCRXM like '%" + StringPlus.ReplaceSingle(dutyman) + "%'";

            }
            if (!string.IsNullOrEmpty(timebegin))
            {
                where += " and a.CJSJ >= to_date('" + StringPlus.ReplaceSingle(timebegin) + "','yyyy-mm-dd')";
              
            }
            if (!string.IsNullOrEmpty(timeend))
            {
                where += " and a.CJSJ <= to_date('" + Convert.ToDateTime(StringPlus.ReplaceSingle(timeend)).AddDays(1).ToString("yyyy-MM-dd") + "','yyyy-mm-dd')";
                
            }
            
            EDRS.BLL.DataStatistics bll = new EDRS.BLL.DataStatistics(this.Request);
            Decimal count;
            DataSet ds = bll.GetJZNumberByReport(where, UserInfo.DWBM,UserInfo.GH, "1", (int)EnumConfig.文件存储空间大小分配 ,out count, values);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (type == "Graph")
                {
                    //数据日志
                    OperateLog.AddLog(OperateLog.LogType.卷宗数量统计Web, "卷宗数量统计图形获取成功", UserInfo, UserRole, this.Request);
                    return StatisticsDataHelper.GetGraph(dt, new String[] { "WJDX" }, "WJDX") + "," + (count*1024*1024*1024);
                }
                else
                {
                    //数据日志
                    OperateLog.AddLog(OperateLog.LogType.卷宗数量统计Web, "卷宗数量统计列表获取成功", UserInfo, UserRole, this.Request);
                    return "{\"Total\":" + 1 + ",\"Rows\":" + JsonHelper.JsonString(dt) + "}";
                }
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.卷宗数量统计Web, "未找到卷宗统计信息", UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "未找到卷宗统计信息", null);
        }

        #region 获取案件统计详细
        /// <summary>
        /// 获取案件统计详细
        /// </summary>
        /// <returns></returns>
        private string GetDwSum()
        {
            string page = Request["page"];
            string rows = Request["pagesize"];         
            
            int pageNumber = int.Parse(page);
            int pageSize = int.Parse(rows);

            //排序
            string sortname = Request["sortname"];
            string sortorder = Request["sortorder"];
            if (string.IsNullOrEmpty(sortname) || sortname == "DWMC")
                sortname = "DWBM";
            sortname += " " + sortorder;

            string where = string.Empty;

            object[] values = new object[0];

            //EDRS.BLL.XT_DM_QX bllQx = new EDRS.BLL.XT_DM_QX(Request);
            //DataSet dsDwQx = bllQx.GetDwList(StringPlus.ReplaceSingle(base.Jsbms), base.UserInfo.DWBM, StringPlus.ReplaceSingle(base.Bmbms), "");
            //if (dsDwQx != null && dsDwQx.Tables.Count > 0 && dsDwQx.Tables[0].Rows.Count > 0)
            //{
            //    string dwbms = "";
            //    foreach (DataRow dr in dsDwQx.Tables[0].Rows)
            //    {
            //        dwbms += dr["QXBM"] + ",";
            //    }
            //    where += " and b.DWBM in (" + StringPlus.ReplaceSingle(dwbms.Substring(0, dwbms.Length - 1)) + ")";
               
            //}
            //else
            //    where += " and b.DWBM =" + base.UserInfo.DWBM;

            where += " and b.DWBM in (select distinct QXBM FROM XT_DM_QX  where 1=1 AND  JSBM IN (" + base.Jsbms + ") AND QXLX=0 AND trim(DWBM) = '" + base.UserInfo.DWBM + "' AND BMBM in (" + base.Bmbms + ") )";


            EDRS.BLL.DataStatistics bll = new EDRS.BLL.DataStatistics(this.Request);
            int count;
            DataSet ds = bll.GetDwAjWjSum(where, UserInfo.DWBM, UserInfo.GH, sortname, (pageSize * pageNumber) - pageSize + 1, (pageSize * pageNumber), out count, values);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                return "{\"Total\":" + count + ",\"Rows\":" + JsonHelper.JsonString(dt) + "}";
            }          
            return ReturnString.JsonToString(Prompt.error, "未找到统计信息", null);
        }
        #endregion

    }
}