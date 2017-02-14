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
                    //权限状态
                    EDRS.BLL.TYYW_GG_AJJBXX bll = new EDRS.BLL.TYYW_GG_AJJBXX(Request);
                    string data = bll.ListBin(this.Request, UserInfo.DWBM, UserInfo.GH,base.GetBmBm(), Jsbms, base.GetJsQxzt());                   
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
            //权限状态
            EDRS.BLL.TYYW_GG_AJJBXX bll = new EDRS.BLL.TYYW_GG_AJJBXX(Request);
            DataTable dt = bll.ListBinTable(Request, UserInfo.DWBM, UserInfo.GH,base.GetJsQxzt(),base.GetBmBm());
            if (dt != null && dt.Rows.Count > 0)
            {
                string ExcelFolder = "ExcelFolder";// Assistant.GetConfigString("ExcelFolder");
             
                string filename = DateTime.Now.ToString("yyyyMMddHHmmssff") + ".xls";
                string msg = string.Empty;
                if (dt.Rows.Count > 0)
                {
                    if (dt.Columns.Contains("ISREGARD"))
                        dt.Columns.Remove("ISREGARD");
                    if (dt.Columns.Contains("SFZZ"))
                        dt.Columns.Remove("SFZZ");
                    dt.Columns.Remove("RO");
                    dt.Columns.Remove("JZSCRXM");
                    dt.Columns.Remove("CJSJ");
                    dt.Columns.Remove("GGCJSJ");
                    dt.Columns.Remove("BMSAH");
                    dt.Columns.Remove("CBBM_MC");
                    dt.Columns.Remove("DQJD");
                    dt.Columns.Remove("AJZT");
                    dt.Columns.Remove("DQRQ");
                    dt.Columns.Remove("BJRQ");
                    dt.Columns.Remove("WCRQ");
                    dt.Columns.Remove("GDRQ");
                    dt.Columns.Remove("AJLB_BM");
                    dt.Columns.Remove("CBDW_BM");
                    dt.Columns.Remove("SFZH");
                    dt.Columns.Remove("TARYXX");
                    dt.Columns.Remove("SHR");
                    dt.Columns.Remove("ZJS");
                    dt.Columns.Remove("DJJ");
                    dt.Columns.Remove("ZYS");
                    dt.Columns.Remove("JZPZ");
                    dt.Columns.Remove("JZSHRBH");
                    dt.Columns.Remove("JZSHR");
                    dt.Columns.Remove("JZSHSJ");
                    dt.Columns.Remove("JZBH");

                    dt.Columns["ZZZT"].ColumnName = "制作状态";
                    dt.Columns["AJMC"].ColumnName = "案件名称";
                    dt.Columns["AJBH"].ColumnName = "案件编号";
                    dt.Columns["WSBH"].ColumnName = "文书编号";
                    dt.Columns["WSMC"].ColumnName = "文书名称";
                    dt.Columns["AJLB_MC"].ColumnName = "案件类别";
                    dt.Columns["XYR"].ColumnName = "嫌疑人姓名";
                    dt.Columns["SLRQ"].ColumnName = "立案时间";
                    if (dt.Columns.Contains("CBDW_MC"))
                        dt.Columns["CBDW_MC"].ColumnName = "立卷单位";
                    if (dt.Columns.Contains("AJ_DWMC"))
                        dt.Columns["AJ_DWMC"].ColumnName = "立卷单位";
                    dt.Columns["CBR"].ColumnName = "立卷人";

                    object zzzt;
                    object[] desc;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        zzzt = Enum.Parse(typeof(EnumZzzt), dt.Rows[i]["制作状态"].ToString(), true);
                        desc = zzzt.GetType().GetField(zzzt.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), true);
                        if (desc != null && desc.Length > 0)
                        {                            
                            dt.Rows[i]["制作状态"] = (desc[0] as DescriptionAttribute).Description;
                        }
                    }                    

                    msg = DataToExcel_Ex.Export(dt, "案件基本情况", Server.MapPath("/" + ExcelFolder + "/" + filename));
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