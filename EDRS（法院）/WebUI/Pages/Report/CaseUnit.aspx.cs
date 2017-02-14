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
using System.ComponentModel;

namespace WebUI.Pages.Report
{
    public partial class CaseUnit : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request["t"];

            if (!string.IsNullOrEmpty(type))
            {
                Response.ContentEncoding = Encoding.UTF8;
                Response.ContentType = "application/json";
                if (type.Equals("ListBind"))
                {
                    EDRS.BLL.TYYW_GG_AJJBXX bll = new EDRS.BLL.TYYW_GG_AJJBXX(Request);
                    string data = bll.ListBin(this.Request, UserInfo.DWBM, UserInfo.GH,Bmbms,Jsbms);                   
                    Response.Write(data);
                }
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
            EDRS.BLL.TYYW_GG_AJJBXX bll = new EDRS.BLL.TYYW_GG_AJJBXX(Request);
            DataTable dt = bll.ListBinTable(Request, UserInfo.DWBM, UserInfo.GH);
            if (dt != null && dt.Rows.Count > 0)
            {
                string ExcelFolder = "ExcelFolder";// Assistant.GetConfigString("ExcelFolder");
             
                string filename = DateTime.Now.ToString("yyyyMMddHHmmssff") + ".xls";
                string msg = string.Empty;
                if (dt.Rows.Count > 0)
                {                  
                    dt.Columns["ZZZT"].ColumnName = "制作状态";
                    dt.Columns["BMSAH"].ColumnName = "案号名称";
                    dt.Columns["AJMC"].ColumnName = "案由";
                    //dt.Columns["AJBH"].ColumnName = "案件编号";
                    //dt.Columns["WSBH"].ColumnName = "文书编号";
                    //dt.Columns["WSMC"].ColumnName = "文书名称";
                    dt.Columns["AJLB_MC"].ColumnName = "案件类别";
                    //dt.Columns["XYR"].ColumnName = "嫌疑人姓名";
                    dt.Columns["SARQ"].ColumnName = "收案日期";
                    dt.Columns["JARQ"].ColumnName = "结案日期";
                    dt.Columns["GDRQ"].ColumnName = "归档日期";
                    if (dt.Columns.Contains("CBDW_MC"))
                        dt.Columns["CBDW_MC"].ColumnName = "单位";
                    if (dt.Columns.Contains("AJ_DWMC"))
                        dt.Columns["AJ_DWMC"].ColumnName = "单位";
                    dt.Columns["CBR"].ColumnName = "承办人";

                    DataTable dtCopy = dt.Copy();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (!Regex.IsMatch(dt.Columns[i].ColumnName.ToString(), @"[\u4e00-\u9fbb]+"))
                        {
                            dtCopy.Columns.Remove(dt.Columns[i].ColumnName.ToString());
                        }
                    }

                    object zzzt;
                    object[] desc;
                    for (int i = 0; i < dtCopy.Rows.Count; i++)
                    {
                        zzzt = Enum.Parse(typeof(EnumZzzt), dtCopy.Rows[i]["制作状态"].ToString(), true);
                        desc = zzzt.GetType().GetField(zzzt.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), true);
                        if (desc != null && desc.Length > 0)
                        {
                            dtCopy.Rows[i]["制作状态"] = (desc[0] as DescriptionAttribute).Description;
                        }
                    }

                    msg = DataToExcel_Ex.Export(dtCopy, "案件基本情况", Server.MapPath("/" + ExcelFolder + "/" + filename));
                    // filename = dte.DataExcel(dt, ((VersionName)0).ToString()+"基本情况", FilePath, nameList, valueList);
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