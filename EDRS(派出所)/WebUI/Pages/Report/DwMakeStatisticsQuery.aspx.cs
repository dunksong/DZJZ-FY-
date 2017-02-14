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
    public partial class DwMakeStatisticsQuery : BasePage
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
                if (type.Equals("GetDwbm"))
                {
                    EDRS.BLL.XT_ZZJG_DWBM bll = new EDRS.BLL.XT_ZZJG_DWBM(Request);                    
                    Response.Write(bll.GetDwbm(UserInfo.DWBM, UserInfo.GH));
                }
                if (type.Equals("GetYwbm"))
                    Response.Write(GetYwbm());
                if (type.Equals("DeriveData"))
                    Response.Write(DeriveData());
                Response.End();
            }
        }
        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="key"></param>
        /// <param name="casename"></param>
        /// <param name="dutyman"></param>
        /// <param name="ywbm"></param>
        /// <param name="timebegin"></param>
        /// <param name="timeend"></param>
        /// <returns></returns>
        //[WebMethod]
        public string ListBind()
        {
            string page = Request["page"];
            string rows = Request["pagesize"];
            string key = Request["key"];
            string casename = Request["casename"];
            string dutyman = Request["dutyman"];
            string timebegin = Request["timebegin"];
            string timeend = Request["timeend"];
            string ywbm = Request["ywbm"];
            string dwbm = Request["dwbm"];
            string caseajlb = Request["caseajlb"];
            string sortName = Request["sortName"];
            string sortOrder = Request["sortOrder"];
            string orderBy = string.Empty;
            //自定义排序
            if (string.IsNullOrEmpty(sortName))
            {
                orderBy = "CBDW_BM ASC";
            }
            else
            {
                orderBy = sortName + " " + sortOrder;
            }
            int pageNumber = int.Parse(page);
            int pageSize = int.Parse(rows);
            string where = string.Empty;

            object[] values = new object[0];
            if (!string.IsNullOrEmpty(key))
            {
                where += " and a.AJBH like '%" + StringPlus.ReplaceSingle(key) + "%'";
            }
            if (!string.IsNullOrEmpty(casename))
            {
                where += " and AJMC like '%" + StringPlus.ReplaceSingle(casename) + "%'";
            }
            if (!string.IsNullOrEmpty(dutyman))
            {
                where += " and CBR like '%" + StringPlus.ReplaceSingle(dutyman) + "%'";
            }
            if (!string.IsNullOrEmpty(ywbm))
            {
                where += " and  c.YWBM='" + StringPlus.ReplaceSingle(ywbm) + "'";
            }
            if (!string.IsNullOrEmpty(timebegin))
            {
                where += " and b.cjsj >= to_date('" + StringPlus.ReplaceSingle(timebegin) + "','yyyy-mm-dd')";
            }
            if (!string.IsNullOrEmpty(timeend))
            {
                where += " and b.cjsj <= to_date('" + Convert.ToDateTime(StringPlus.ReplaceSingle(timeend)).AddDays(1).ToString("yyyy-MM-dd") + "','yyyy-mm-dd')";
            }
            if (!string.IsNullOrEmpty(dwbm))
            {
                string dwbms = dwbm.Replace(";", ",");
                where += " and trim(CBDW_BM) in (" + StringPlus.ReplaceSingle(dwbms) + ")";
            }
            if (!string.IsNullOrEmpty(caseajlb))
            {
                string caseajlbs = caseajlb.Replace(";", ",");
                where += " and trim(b.AJLB_BM) in ('" + caseajlbs + "')";
            }

            EDRS.BLL.DataStatistics bll = new EDRS.BLL.DataStatistics(this.Request);
            int count;
            DataSet ds = bll.GetDwJzZzQuery(where, UserInfo.DWBM, UserInfo.GH, orderBy + ",T.jnum desc", (pageSize * pageNumber) - pageSize + 1, (pageSize * pageNumber), out count, values);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.单位卷宗制作情况查询Web, "单位卷宗制作情况查询成功", UserInfo, UserRole, this.Request);
                DataTable dt = ds.Tables[0];               
                return "{\"Total\":" + count + ",\"Rows\":" + JsonHelper.JsonString(dt) + "}";
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.单位卷宗制作情况查询Web, "单位卷宗制作情况查询未找到信息", UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "未找到"+((VersionName)0).ToString()+"信息", null);
        }
        #endregion


        #region 获取业务类型编码
        /// <summary>
        /// 获取业务类型编码
        /// </summary>
        /// <returns></returns>
        private string GetYwbm()
        {
            EDRS.BLL.XT_DM_YWBM bll = new EDRS.BLL.XT_DM_YWBM(Request);
            DataSet ds = bll.GetListByPage(" and SFSC=:SFSC", "YWBM asc", 1, int.MaxValue, new object[] { "N" });
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                dt.Columns["YWBM"].ColumnName = "id";
                dt.Columns["YWMC"].ColumnName = "text";
                return JsonHelper.JsonString(dt);
            }
            return ReturnString.JsonToString(Prompt.error, "未找到相关业务类型", null);

        } 
        #endregion

        #region 导出Excel
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        private string DeriveData()
        {
            string page = Request["page"];
            string rows = Request["pagesize"];
            string key = Request["key"];
            string casename = Request["casename"];
            string dutyman = Request["dutyman"];
            string timebegin = Request["timebegin"];
            string timeend = Request["timeend"];
            string ywbm = Request["ywbm"];
            string dwbm = Request["dwbm"];
            string caseajlb = Request["caseajlb"];
            string sortName = Request["sortName"];
            string sortOrder = Request["sortOrder"];
            string orderBy = string.Empty;
            //自定义排序
            if (string.IsNullOrEmpty(sortName))
            {
                orderBy = "CBDW_BM ASC";
            }
            else
            {
                orderBy = sortName + " " + sortOrder;
            }
            int pageNumber = int.Parse(page);
            int pageSize = int.Parse(rows);
            string where = string.Empty;

            object[] values = new object[0];
            if (!string.IsNullOrEmpty(key))
            {
                where += " and a.AJBH like '%" + StringPlus.ReplaceSingle(key) + "%'";
            }
            if (!string.IsNullOrEmpty(casename))
            {
                where += " and AJMC like '%" + StringPlus.ReplaceSingle(casename) + "%'";
            }
            if (!string.IsNullOrEmpty(dutyman))
            {
                where += " and CBR like '%" + StringPlus.ReplaceSingle(dutyman) + "%'";
            }
            if (!string.IsNullOrEmpty(ywbm))
            {
                where += " and  c.YWBM='" + StringPlus.ReplaceSingle(ywbm) + "'";
            }
            if (!string.IsNullOrEmpty(timebegin))
            {
                where += " and b.cjsj >= to_date('" + StringPlus.ReplaceSingle(timebegin) + "','yyyy-mm-dd')";
            }
            if (!string.IsNullOrEmpty(timeend))
            {
                where += " and b.cjsj <= to_date('" + Convert.ToDateTime(StringPlus.ReplaceSingle(timeend)).AddDays(1).ToString("yyyy-MM-dd") + "','yyyy-mm-dd')";
            }
            if (!string.IsNullOrEmpty(dwbm))
            {
                string dwbms = dwbm.Replace(";", ",");
                where += " and trim(CBDW_BM) in (" + StringPlus.ReplaceSingle(dwbms) + ")";
            }
            if (!string.IsNullOrEmpty(caseajlb))
            {
                string caseajlbs = caseajlb.Replace(";", ",");
                where += " and trim(b.AJLB_BM) in ('" + caseajlbs + "')";
            }
            EDRS.BLL.DataStatistics bll = new EDRS.BLL.DataStatistics(this.Request);
            int count;
            DataSet ds = bll.GetDwJzZzQuery(where, UserInfo.DWBM, UserInfo.GH, orderBy + ",T.jnum desc", (pageSize * pageNumber) - pageSize + 1, (pageSize * pageNumber), out count, values);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string ExcelFolder = "ExcelFolder";// Assistant.GetConfigString("ExcelFolder");
           

                string filename = DateTime.Now.ToString("yyyyMMddHHmmssff") + ".xls";
                string msg=string.Empty;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0].Copy();

                    dt.Columns.Remove("RO");
                    dt.Columns.Remove("JZBH");
                    dt.Columns.Remove("BMSAH");
                    dt.Columns.Remove("AJLB_BM");
                    dt.Columns.Remove("CBDW_BM");
                    dt.Columns.Remove("CBBM_BM");
                    dt.Columns.Remove("CBBM_MC");
                    dt.Columns.Remove("CBRGH");
                   
                    dt.Columns["AJMC"].ColumnName = "案件名称";
                    dt.Columns["AJBH"].ColumnName = "案件编号";
                    dt.Columns["WSBH"].ColumnName = "文书编号";
                    dt.Columns["WSMC"].ColumnName = "文书名称";
                    dt.Columns["AJLB_MC"].ColumnName = "案件类别";
                    dt.Columns["XYR"].ColumnName = "嫌疑人姓名";
                    dt.Columns["SLRQ"].ColumnName = "立案时间";
                    dt.Columns["CBDW_MC"].ColumnName = "立卷单位";
                    dt.Columns["CBR"].ColumnName = "立卷人";
                    dt.Columns["JNUM"].ColumnName = "卷数";
                    dt.Columns["WJNUM"].ColumnName = "文件数";
                    dt.Columns["WJYNUM"].ColumnName = "文件页数";

                    msg = DataToExcel_Ex.Export(dt, "卷宗制作情况", Server.MapPath("/" + ExcelFolder + "/" + filename));
                    //filename = dte.DataExcel(ds.Tables[0], "卷宗制作情况", FilePath, nameList, null);
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