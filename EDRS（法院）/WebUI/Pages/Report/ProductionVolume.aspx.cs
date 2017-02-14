using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EDRS.Common;
using System.Collections;
using System.Data;
using Maticsoft.Common;
using System.Text.RegularExpressions;

namespace WebUI
{
    public partial class ProductionVolume : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request.Form["t"];

            if (!string.IsNullOrEmpty(type))
            {
                Response.ContentEncoding = Encoding.UTF8;
                Response.ContentType = "application/json";
                if (type.Equals("ListBind"))
                    Response.Write(ListBind());
                if (type.Equals("DeriveData"))
                    Response.Write(DeriveData());
                Response.End();
            }
        }

        #region 绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        private DataTable GetListBind(out int count)
        {
            int pageIndex = Convert.ToInt32(Request.Form["page"]);
            int pageSize = Convert.ToInt32(Request.Form["pagesize"]);
            //where
            string b_date = Request.Form["b_date"];
            string e_date = Request.Form["e_date"];
            string username = Request.Form["username"];
            string unit = Request.Form["unit"];

            //排序
            string sortname = Request.Form["sortname"];
            string sortorder = Request.Form["sortorder"];
            if (string.IsNullOrEmpty(sortname) || sortname.ToLower() == "DWMC")
                sortname = "cbdw_bm";
            sortname += " " + sortorder;

            string where = "";
            string whereAj = "";
            string whereRy = "";
            List<object> values = new List<object>();
            if (!string.IsNullOrEmpty(b_date))
            {
                //JZ.Cjsj（创建时间） / JZ.JZSCSJ（上传时间）
                where += " and JZCZSJ >= to_date('" + Convert.ToDateTime(b_date).ToShortDateString() + "','yyyy-mm-dd')";
                whereAj += " and JZSCSJ >= to_date('" + Convert.ToDateTime(b_date).ToShortDateString() + "','yyyy-mm-dd')";
            }
            if (!string.IsNullOrEmpty(e_date))
            {
                where += " and JZCZSJ <= to_date('" + Convert.ToDateTime(e_date).AddDays(1).ToShortDateString() + "','yyyy-mm-dd')";
                whereAj += " and JZSCSJ <= to_date('" + Convert.ToDateTime(e_date).AddDays(1).ToShortDateString() + "','yyyy-mm-dd')";
            }
            if (!string.IsNullOrEmpty(username))
            {
                whereRy += " and MC LIKE '%" + username + "%'";
               
            }
            if (!string.IsNullOrEmpty(unit))
            {
                string dwbms = unit.Replace(";", ",");
                where += " and a.DWBM in (" + StringPlus.ReplaceSingle(dwbms) + ")";
                whereAj += " and cbdw_bm in (" + StringPlus.ReplaceSingle(dwbms) + ")";
                whereRy += " and a.DWBM in (" + StringPlus.ReplaceSingle(dwbms) + ")";
            }
            else
            {
                where += " and a.DWBM in (select distinct QXBM FROM XT_DM_QX  where 1=1 AND  JSBM IN (" + base.Jsbms + ") AND QXLX=0 AND trim(DWBM) = '" + base.UserInfo.DWBM + "' AND BMBM in (" + base.Bmbms + ") )";
                whereAj += " and cbdw_bm in (select distinct QXBM FROM XT_DM_QX  where 1=1 AND  JSBM IN (" + base.Jsbms + ") AND QXLX=0 AND trim(DWBM) = '" + base.UserInfo.DWBM + "' AND BMBM in (" + base.Bmbms + ") )";
                whereRy += " and a.DWBM in (select distinct QXBM FROM XT_DM_QX  where 1=1 AND  JSBM IN (" + base.Jsbms + ") AND QXLX=0 AND trim(DWBM) = '" + base.UserInfo.DWBM + "' AND BMBM in (" + base.Bmbms + ") )";
            }

            EDRS.BLL.DataStatistics bll = new EDRS.BLL.DataStatistics(Request);

            DataTable dt = bll.GetYJTJ(where, whereAj, whereRy, sortname, pageIndex,pageSize, out count, values.ToArray());
          
            return dt;
        }
        #endregion

        #region 绑定列表
        /// <summary>
        /// 绑定列表
        /// </summary>
        /// <returns></returns>
        private string ListBind()
        {
            int count;
            DataTable ds = GetListBind(out count);
            if (ds != null && ds.Rows.Count > 0)
            {
                //获取json数据
                OperateLog.AddLog(OperateLog.LogType.卷宗制作工作量统计Web, "查询卷宗制作工作量统计列表成功！", UserInfo, UserRole, Request);
                return "{\"Rows\":" + JsonHelper.JsonString(ds) + ",\"Total\":" + count + "}";
            }
            else
            {
                OperateLog.AddLog(OperateLog.LogType.卷宗制作工作量统计Web, "查询卷宗制作工作量统计列表失败！", UserInfo, UserRole, Request);
                return "{\"Rows\":" + "[]" + ",\"Total\":" + count + "}";
            }
        }
        #endregion

        #region 导出Excel
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        private string DeriveData()
        {
            int count;
            DataTable ds = GetListBind(out count);
            if (ds != null && ds.Rows.Count > 0)
            {
                string ExcelFolder = "ExcelFolder";// Assistant.GetConfigString("ExcelFolder");              
              
                //利用excel对象
                string filename = DateTime.Now.ToString("yyyyMMddHHmmssff") + ".xls";
                string msg = string.Empty;
                if (ds.Rows.Count > 0)
                {
                    ds.Columns.Remove("RO");
                    ds.Columns.Remove("CBDW_BM");
                    ds.Columns.Remove("JZSCRGH");
                    ds.Columns["CBDW_MC"].ColumnName = "承办单位";
                    ds.Columns["JZSCRXM"].ColumnName = "制作人员";
                    ds.Columns["AJCOUNT"].ColumnName = ((VersionName)0).ToString()+"数量";
                    ds.Columns["JCOUNT"].ColumnName = "卷数";
                    ds.Columns["MLCOUNT"].ColumnName = "目录数";
                    ds.Columns["WJCOUNT"].ColumnName = "文件页数";
                    msg = DataToExcel_Ex.Export(ds, "卷宗制作工作量统计", Server.MapPath("/" + ExcelFolder + "/" + filename));
                   // filename = dte.DataExcel(ds.Tables[0], "卷宗制作工作量统计", FilePath, nameList, null);
                }
                if (string.IsNullOrEmpty(msg))
                {
                    return ReturnString.JsonToString(Prompt.win, "/" + ExcelFolder + "/" + filename, null);
                }
                else
                    return ReturnString.JsonToString(Prompt.error, StringPlus.String2Json(msg), null);                

            }
            return ReturnString.JsonToString(Prompt.error, "导出失败", null);
        }
        #endregion
    }
}