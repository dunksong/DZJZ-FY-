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
    public partial class DwMakeStatistics : BasePage
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
                if (type.Equals("GetDetail"))
                    Response.Write(GetDetail());
                if (type.Equals("GetDetailLb"))
                    Response.Write(GetDetailByLb());
                if (type.Equals("DeriveData"))
                    Response.Write(DeriveData());
                Response.End();
            }
        }

        #region 获取绑定列表
        /// <summary>
        /// 获取绑定列表
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        private DataTable GetList(ref int count)
        {           
            string page = Request["page"];
            string rows = Request["pagesize"];
            string key = Request["key"];
            string casename = Request["casename"];
            string dutyman = Request["dutyman"];
            string timebegin = Request["timebegin"];
            string timeend = Request["timeend"];
            string dwbm = Request["dwbm"];

            string sortname = Request["sortname"];// CBDW_MC
            string sortorder = Request["sortorder"];//asc
            if (string.IsNullOrEmpty(sortname) || sortname == "CBDW_MC")
                sortname = "CBDW_BM";
            sortname += " " + sortorder;
            int pageNumber = int.Parse(page);
            int pageSize = int.Parse(rows);
            string where = string.Empty;

            object[] values = new object[0];

            if (!string.IsNullOrEmpty(timebegin))
            {
                where += " and a.cjsj >= to_date('" + StringPlus.ReplaceSingle(timebegin) + "','yyyy-mm-dd')";
            }
            if (!string.IsNullOrEmpty(timeend))
            {
                where += " and a.cjsj <= to_date('" + Convert.ToDateTime(StringPlus.ReplaceSingle(timeend)).AddDays(1).ToString("yyyy-MM-dd") + "','yyyy-mm-dd')";
            }
            //获取单位编码
            string where_dw="";
            EDRS.BLL.XT_DM_QX bllQx = new EDRS.BLL.XT_DM_QX(this.Request);
            if (!string.IsNullOrEmpty(dwbm))
            {
              //  string dwbms = dwbm.Replace(";", ",");
                string[] dwbms = dwbm.Split(new char[]{';'},StringSplitOptions.RemoveEmptyEntries);
                if (dwbms.Length > 0)
                {
                    where += " and a.CBDW_BM in (";
                    for (int i = 0; i < dwbms.Length; i++)
                    {
                        where += "'" + dwbms[i] + "'";
                        if (i < dwbms.Length - 1)
                            where += ",";
                    }
                    where += ")";
                }
                //where += " and a.CBDW_BM in (" + dwbms + ")";
               

              //  where_dw = " and jz.dwbm in (" + dwbms + ")";
            }
            //else
            //{
                //where += " and jz.dwbm in (select distinct QXBM FROM XT_DM_QX  where 1=1 AND  JSBM IN (" + base.Jsbms + ") AND QXLX=0 AND trim(DWBM) = '" + base.UserDwbm.DWBM + "' AND BMBM in (" + base.Bmbms + ") )";
                //where_dw = " and jz.dwbm in (select distinct QXBM FROM XT_DM_QX  where 1=1 AND  JSBM IN (" + base.Jsbms + ") AND QXLX=0 AND trim(DWBM) = '" + base.UserDwbm.DWBM + "' AND BMBM in (" + base.Bmbms + ") )";
              
            //}
           // where_dw = string.Format(" and JSBM IN ({0}) AND trim(DWBM) = '{1}' AND BMBM in ({2}) ", base.Jsbms, base.UserDwbm.DWBM, base.Bmbms);
            //获取案件类别
          

            EDRS.BLL.DataStatistics bll = new EDRS.BLL.DataStatistics(this.Request);

            DataSet ds = bll.GetDwJzZzStatistics(where, where_dw, UserInfo.DWBM, UserInfo.GH, base.Jsbms, base.Bmbms, sortname, (pageSize * pageNumber) - pageSize + 1, (pageSize * pageNumber), out count, values);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        } 
        #endregion

        #region 绑定列表
        /// <summary>
        /// 绑定列表
        /// </summary>
        /// <returns></returns>
        public string ListBind()
        {
            int count = 0;
            DataTable dt = GetList(ref count);
            if (dt != null && dt.Rows.Count > 0)
            {
                string type = Request["type"];

                if (type == "Graph")
                {
                    //数据日志
                    OperateLog.AddLog(OperateLog.LogType.单位卷宗统计Web, "单位卷宗制作情况统计图形获取成功", UserInfo, UserRole, this.Request);
                    return StatisticsDataHelper.GetGraph(dt, new String[] { "AJNUM", "YZZNUM", "WZZNUM", "JNUM", "MLNUM", "WJNUM", "WJYNUM" }, "DWMC");
                }
                else
                {
                    //数据日志
                    OperateLog.AddLog(OperateLog.LogType.单位卷宗统计Web, "单位卷宗制作情况统计数据获取成功", UserInfo, UserRole, this.Request);
                    return "{\"Total\":" + count + ",\"Rows\":" + JsonHelper.JsonString(dt) + "}";
                }
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.单位卷宗统计Web, "未找到单位卷宗制作情况统计信息", UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "未找到单位卷宗制作情况统计信息", null);
        }        
        #endregion

        #region 获取案件统计详细
        /// <summary>
        /// 获取案件统计详细
        /// </summary>
        /// <returns></returns>
        private string GetDetail()
        {
            //string page = Request["page"];
            //string rows = Request["pagesize"];
            //string key = Request["key"];
            //string casename = Request["casename"];
            //string dutyman = Request["dutyman"];
            //string ywbm = Request["ywbm"];
            string timebegin = Request["timebegin"];
            string timeend = Request["timeend"];
            string dwbm = Request["dwbm"];
            //排序
            string sortname = Request["sortname"];
            string sortorder = Request["sortorder"];
            if (string.IsNullOrEmpty(sortname) || sortname == "YWMC")
                sortname = "YWBM";
            sortname += " " + sortorder;

            string where = string.Empty;

            object[] values = new object[0];
            
            if (!string.IsNullOrEmpty(timebegin))
            {
                where += " and a.CJSJ >= to_date('" + StringPlus.ReplaceSingle(timebegin) + "','yyyy-mm-dd')";
            }
            if (!string.IsNullOrEmpty(timeend))
            {
                where += " and a.CJSJ <= to_date('" + Convert.ToDateTime(StringPlus.ReplaceSingle(timeend)).AddDays(1).ToString("yyyy-MM-dd") + "','yyyy-mm-dd')";
            }
            string where_dw = "";
            if (!string.IsNullOrEmpty(dwbm))
            {
                string[] dwbms = dwbm.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                if (dwbms.Length > 0)
                {
                    where += " and a.CBDW_BM in (";
                    for (int i = 0; i < dwbms.Length; i++)
                    {
                        where += "'" + dwbms[i] + "'";
                        if (i < dwbms.Length - 1)
                            where += ",";
                    }
                    where += ")";
                }
            }

            EDRS.BLL.DataStatistics bll = new EDRS.BLL.DataStatistics(this.Request);

            DataSet ds = bll.GetDwJzZzStatisticsByYw(where, where_dw, UserInfo.DWBM, UserInfo.GH, base.Jsbms, base.Bmbms, "YWBM", values);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataView dv = ds.Tables[0].DefaultView;
                dv.Sort = sortname;
                return "{\"Total\":10,\"Rows\":" + JsonHelper.JsonString(dv.ToTable()) + "}";
            }
           
            return ReturnString.JsonToString(Prompt.error, "未找到统计信息", null);
        } 
        #endregion

        #region 获取案件统计详细
        /// <summary>
        /// 获取案件统计详细
        /// </summary>
        /// <returns></returns>
        private string GetDetailByLb()
        {
            string dwbm = Request["dwbm"];
            string timebegin = Request["timebegin"];
            string timeend = Request["timeend"];

            //排序
            string sortname = Request["sortname"];
            string sortorder = Request["sortorder"];
            if (string.IsNullOrEmpty(sortname) || sortname == "YWMC")
                sortname = "ajlb_bm";
            sortname += " " + sortorder;

            string where = string.Empty;

            object[] values = new object[0];

            if (!string.IsNullOrEmpty(timebegin))
            {
                where += " and a.CJSJ >= to_date('" + StringPlus.ReplaceSingle(timebegin) + "','yyyy-mm-dd')";
            }
            if (!string.IsNullOrEmpty(timeend))
            {
                where += " and a.CJSJ <= to_date('" + Convert.ToDateTime(StringPlus.ReplaceSingle(timeend)).AddDays(1).ToString("yyyy-MM-dd") + "','yyyy-mm-dd')";
            }
            string where_dw = "";
            if (!string.IsNullOrEmpty(dwbm))
            {
                string[] dwbms = dwbm.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                if (dwbms.Length > 0)
                {
                    where += " and a.CBDW_BM in (";
                    for (int i = 0; i < dwbms.Length; i++)
                    {
                        where += "'" + dwbms[i] + "'";
                        if (i < dwbms.Length - 1)
                            where += ",";
                    }
                    where += ")";
                }
            }

            EDRS.BLL.DataStatistics bll = new EDRS.BLL.DataStatistics(this.Request);

            DataSet ds = bll.GetDwJzZzStatisticsByLb(where,where_dw, UserInfo.DWBM, UserInfo.GH,base.Jsbms,base.Bmbms, "ajlb_bm", values);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                DataView dv = ds.Tables[0].DefaultView;
                dv.Sort = sortname;
                return "{\"Total\":10,\"Rows\":" + JsonHelper.JsonString(dv.ToTable()) + "}";
            }

            return ReturnString.JsonToString(Prompt.error, "未找到统计信息", null);
        }
        #endregion

        #region 导出Excel
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        private string DeriveData()
        {
            int count = 0;
            DataTable dt = GetList(ref count);
            if (dt != null && dt.Rows.Count > 0)
            {
                string ExcelFolder = "ExcelFolder";// Assistant.GetConfigString("ExcelFolder");
        
                string filename = DateTime.Now.ToString("yyyyMMddHHmmssff") + ".xls";
                string msg=string.Empty;
                if (dt != null && dt.Rows.Count > 0)
                {
                    dt.Columns.Remove("Ro");
                    dt.Columns.Remove("CBDW_BM");
                    dt.Columns["CBDW_MC"].ColumnName = "承办单位";
                    dt.Columns["AJNUM"].ColumnName = "制作案件数";
                    dt.Columns["JNUM"].ColumnName = "卷数";
                    dt.Columns["WJNUM"].ColumnName = "文件数";
                    dt.Columns["WJYNUM"].ColumnName = "文件页数";

                    msg = DataToExcel_Ex.Export(dt, "卷宗制作量统计",Server.MapPath("/" + ExcelFolder + "/" + filename));

                    //filename = dte.DataExcel(dt, "卷宗制作量统计", FilePath, nameList, null);                  
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