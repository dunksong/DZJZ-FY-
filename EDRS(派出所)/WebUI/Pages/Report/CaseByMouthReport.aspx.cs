using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EDRS.Common;
using System.Data;
using System.Collections;
using Maticsoft.Common;
using System.Text.RegularExpressions;

namespace WebUI.Pages.Report
{
    public partial class CaseByMouthReport : BasePage
    {
        public int ThisYear = DateTime.Now.Year;
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
            string where = "";
            List<object> values = new List<object>();
            string unit = Request.Form["unit"];//承办单位
            string timebegin = Request.Form["timebegin"];//开始日期
            string timeend = Request.Form["timeend"];

            //排序
            string sortname = Request.Form["sortname"];
            string sortorder = Request.Form["sortorder"];
            if (string.IsNullOrEmpty(sortname) || sortname.ToLower() == "mm")
                sortname = "mm";
            sortname += " " + sortorder;

            if (!string.IsNullOrEmpty(unit))
            {
                where += " and CBDW_BM IN( ";
                where += "" + unit.Replace(";", ",") + ")";
            }
            else
            {
                where += " and CBDW_BM in (select distinct QXBM FROM XT_DM_QX  where 1=1 AND  JSBM IN (" + base.Jsbms + ") AND QXLX=0 AND trim(DWBM) = '" + base.UserInfo.DWBM + "' AND BMBM in (" + base.Bmbms + ") )";
            }

            if (!string.IsNullOrEmpty(timebegin))
            {
                where += " and aj.cjsj >= to_date('" + Convert.ToDateTime(timebegin).ToShortDateString() + "','yyyy-mm-dd')";
            }
            if (!string.IsNullOrEmpty(timeend))
            {
                where += " and aj.cjsj < to_date('" + Convert.ToDateTime(timeend).AddDays(1).ToShortDateString() + "','yyyy-mm-dd')";
            }
            where += " and trim(aj.ajlb_bm) in (select distinct QXBM FROM XT_DM_QX  where 1=1 AND  JSBM IN (" + base.Jsbms + ") AND QXLX=1 AND trim(DWBM) = '" + base.UserInfo.DWBM + "' AND BMBM in (" + base.Bmbms + ") )";

            EDRS.BLL.EDRS_Report bll = new EDRS.BLL.EDRS_Report(Request);
            string qxWhere = "";
            DataSet ds = bll.GetCaseGroupMouth(where, qxWhere, sortname, values.ToArray());

            if (ds != null && ds.Tables.Count > 0)
            {

                string ExcelFolder = "ExcelFolder";// Assistant.GetConfigString("ExcelFolder");
             
                //利用excel对象
              
                string filename = DateTime.Now.ToString("yyyyMMddHHmmssff") + ".xls";
                string msg = string.Empty; 
                if (ds.Tables[0].Rows.Count > 0)
                {                    
                    ds.Tables[0].Columns["MM"].ColumnName = "月份";
                    ds.Tables[0].Columns["AJCOUNT"].ColumnName = ((VersionName)0).ToString() + "数量";
                    ds.Tables[0].Columns["ZZCOUNT"].ColumnName = "案件制作数";
                    ds.Tables[0].Columns["JCOUNT"].ColumnName = "卷数";
                    ds.Tables[0].Columns["WCOUNT"].ColumnName = "文件数";
                    ds.Tables[0].Columns["PAGECOUNT"].ColumnName = "文件页数";
                    msg = DataToExcel_Ex.Export(ds.Tables[0], "卷宗月度统计表", Server.MapPath("/" + ExcelFolder + "/" + filename));
                   // filename = dte.DataExcel(ds.Tables[0], "卷宗月度统计表", FilePath, nameList, valueList);
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