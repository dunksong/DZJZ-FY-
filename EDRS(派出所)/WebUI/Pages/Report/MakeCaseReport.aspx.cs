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
    public partial class MakeCaseReport : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request["t"];

            if (!string.IsNullOrEmpty(type))
            {
                Response.ContentEncoding = Encoding.UTF8;
                Response.ContentType = "application/json";
                if (type.Equals("DeriveData"))
                    Response.Write(DeriveData());
                Response.End();
            }
        }


        #region 导出Excel
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        private string DeriveData()
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
            if (string.IsNullOrEmpty(sortname) || sortname.ToLower() == "cbdw_mc")
                sortname = "CBDW_BM";
            sortname += " " + sortorder;

            string where = "";
            List<object> values = new List<object>();
            if (!string.IsNullOrEmpty(b_date))
            {
                //JZ.Cjsj（创建时间） / JZ.JZSCSJ（上传时间）
                where += " and t1.Cjsj >= to_date('" + Convert.ToDateTime(b_date).ToShortDateString() + "','yyyy-mm-dd')";
            }
            if (!string.IsNullOrEmpty(e_date))
            {
                where += " and t1.Cjsj <= to_date('" + Convert.ToDateTime(e_date).AddDays(1).ToShortDateString() + "','yyyy-mm-dd')";

            }
            if (!string.IsNullOrEmpty(username))
            {
                where += " and JZ.JZSCRXM LIKE ";
                where += "'%" + username + "%'";
            }
            if (!string.IsNullOrEmpty(unit))
            {
                string dwbms = unit.Replace(";", ",");
                where += " and CBDW_BM in (" + StringPlus.ReplaceSingle(dwbms) + ")";
            }
            else
            {
                where += " and CBDW_BM in (select distinct QXBM FROM XT_DM_QX  where 1=1 AND  JSBM IN (" + base.Jsbms + ") AND QXLX=0 AND trim(DWBM) = '" + base.UserInfo.DWBM + "' AND BMBM in (" + base.Bmbms + ") )";
            }
          
            string qxWhere = " AND trim(DWBM) = '" + UserInfo.DWBM + "' AND JSBM in (" + Jsbms + ") AND BMBM in (" + Bmbms + ") AND QXLX = 0";
            EDRS.BLL.EDRS_Report bll = new EDRS.BLL.EDRS_Report(Request);
            int count;
            DataSet ds = bll.GetCaseByPerson(where, qxWhere, Convert.ToInt32(pageSize), Convert.ToInt32(pageIndex), sortname, out count, values.ToArray());

            if (ds != null && ds.Tables.Count > 0)
            {
                string ExcelFolder = "ExcelFolder";// Assistant.GetConfigString("ExcelFolder");              
              
                //利用excel对象
                string filename = DateTime.Now.ToString("yyyyMMddHHmmssff") + ".xls";
                string msg = string.Empty;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ds.Tables[0].Columns.Remove("RO");
                    ds.Tables[0].Columns.Remove("CBDW_BM");
                    ds.Tables[0].Columns.Remove("MCOUNT");
                    ds.Tables[0].Columns["CBDW_MC"].ColumnName = "承办单位";
                    ds.Tables[0].Columns["JZSCRXM"].ColumnName = "制作人员";
                    ds.Tables[0].Columns["AJCOUNT"].ColumnName = ((VersionName)0).ToString()+"数量";
                    ds.Tables[0].Columns["JCOUNT"].ColumnName = "卷数";
                    ds.Tables[0].Columns["WCOUNT"].ColumnName = "文件数";
                    ds.Tables[0].Columns["PAGECOUNT"].ColumnName = "文件页数";
                    msg = DataToExcel_Ex.Export(ds.Tables[0], "卷宗制作工作量统计", Server.MapPath("/" + ExcelFolder + "/" + filename));
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