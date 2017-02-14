using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using Maticsoft.Common;
using EDRS.Common;
using System.Text;
using System.Text.RegularExpressions;

namespace WebUI.Pages.Report
{
    public partial class CaseUnitReport : BasePage
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
                if (type.Equals("GetDetail"))
                    Response.Write(GetDetail());
                if (type.Equals("GetDetailLb"))
                    Response.Write(GetDetailByLb());
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
            string strWhere = "";
            List<object> values = new List<object>();
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["pagesize"]);
            string key = Request["key"];//部门受案号
            string timebegin = Request["timebegin"];//开始日期
            string timeend = Request["timeend"];//结束日期
            string unit = Request["unit"];//承办单位
            string ywbm = Request["ywbm"];
            string caseajlb = Request["caseajlb"];
            //排序
            string sortname = Request["sortname"];
            string sortorder = Request["sortorder"];
            if (string.IsNullOrEmpty(sortname) || sortname.ToLower() == "cbdw_mc")
                sortname = "CBDW_BM";
            sortname += " " + sortorder;

            // string qxWhere = "";
            //  qxWhere = " AND DWBM = '" + UserInfo.DWBM + "' AND JSBM in (" + Jsbms + ") AND BMBM in (" + Bmbms + ") AND QXLX = 0";
            //选中时，筛选单位，否则查询所有有权限数据
            EDRS.BLL.XT_DM_QX bllQx = new EDRS.BLL.XT_DM_QX(Request);
            if (!string.IsNullOrEmpty(unit))
            {
                string dwbms = unit.Replace(";", ",");
                strWhere += " and CBDW_BM in (" + StringPlus.ReplaceSingle(dwbms) + ")";
            }
            else if (string.IsNullOrEmpty(unit))
            {
                strWhere += " and AJ.CBDW_BM in (select distinct QXBM FROM XT_DM_QX  where 1=1 AND  JSBM IN (" + base.Jsbms + ") AND QXLX=0 AND trim(DWBM) = '" + base.UserInfo.DWBM + "' AND BMBM in (" + base.Bmbms + ") )";
               
            }
            //案件类别
            if (!string.IsNullOrEmpty(caseajlb))
            {
                string caseajlbs = caseajlb.Replace(";", ",");
                strWhere += " and trim(AJ.AJLB_BM) in (" + caseajlbs + ")";
            }
            else if (string.IsNullOrEmpty(caseajlb))
            {
                strWhere += " and trim(aj.ajlb_bm) in (select distinct QXBM FROM XT_DM_QX  where 1=1 AND  JSBM IN (" + base.Jsbms + ") AND QXLX=1 AND trim(DWBM) = '" + base.UserInfo.DWBM + "' AND BMBM in (" + base.Bmbms + ") )";
            }
            //业务
            if (!string.IsNullOrEmpty(ywbm))
            {
                strWhere += " and  LB.YWBM='" + StringPlus.ReplaceSingle(ywbm) + "'";
            }

            //strWhere += " and (select count(1) from yx_dzjz_jzml y where y.Bmsah=T1.Bmsah) > 0";//已关联
            if (!string.IsNullOrEmpty(timebegin))
            {
                strWhere += " and aj.cjsj >= to_date('" + Convert.ToDateTime(timebegin).ToShortDateString() + "','yyyy-mm-dd')";
            }
            if (!string.IsNullOrEmpty(timeend))
            {
                strWhere += " and aj.cjsj  <= to_date('" + Convert.ToDateTime(timeend).AddDays(1).ToShortDateString() + "','yyyy-mm-dd')";
            }

            EDRS.BLL.EDRS_Report bll = new EDRS.BLL.EDRS_Report(Request);
            int count = 0;

            //1-8 修改存储过程
            DataSet ds = bll.GetCaseGroupByUnit(strWhere, UserInfo.DWBM, UserInfo.GH, (pageSize * pageIndex), (pageSize * pageIndex) - pageSize + 1, sortname, out count, values.ToArray());

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                DataTable dt = ds.Tables[0].Copy();

                //读取配置文件中的配置
                int? isLocalAjxx = EDRS.Common.ConfigHelper.GetConfigInt("IsLocalAjxx");
                if (isLocalAjxx == 1)
                {
                    string msg = "";
                    DateTime? b_date = null;
                    if (!string.IsNullOrEmpty(timebegin))
                        b_date = Convert.ToDateTime(timebegin);

                    DateTime? e_date = null;
                    if (!string.IsNullOrEmpty(timeend))
                        e_date = Convert.ToDateTime(timeend);
                    EDRS.Common.IceServicePrx iceprx = new IceServicePrx();

                    EDRS.BLL.TYYW_GG_AJJBXX ajbll = new EDRS.BLL.TYYW_GG_AJJBXX(Request);
                    msg = ajbll.GetConfiguration(UserInfo.DWBM, UserInfo.GH, iceprx);
                    if (!string.IsNullOrEmpty(msg))
                        ReturnString.JsonToString(Prompt.error, msg, null);

                    foreach (DataRow dr in dt.Rows)
                    {

                        dr["AJNUM"] = iceprx.GetAjjbxxCount(null, dr["CBDW_BM"].ToString(), b_date, e_date, true, out msg);
                        dr["WZNUM"] = int.Parse(dr["AJNUM"].ToString()) - int.Parse(dr["ZZNUM"].ToString());
                        dr["ZZLNUM"] = Math.Round((decimal.Parse(dr["ZZNUM"].ToString()) / decimal.Parse(dr["AJNUM"].ToString())) * 100, 2);
                    }
                }
                string ExcelFolder = "ExcelFolder";// Assistant.GetConfigString("ExcelFolder");
               

                //生成列的中文对应表
                //Dictionary<string, string> nameList = new Dictionary<string, string>();

                //nameList.Add("CBDW_MC", "承办单位");
                //nameList.Add("AJNUM", ((VersionName)0).ToString()+ "受理数");
                //nameList.Add("ZZNUM", "已制作数");
                //nameList.Add("WZNUM", "未制作数");
                //nameList.Add("ZZLNUM", "制作率");

                //Dictionary<string, object> valueList = new Dictionary<string, object>();
                //valueList.Add("ZZLNUM", "%");

                //利用excel对象

                string filename = DateTime.Now.ToString("yyyyMMddHHmmssff") + ".xls";
                string msgs = string.Empty;
                if (dt.Rows.Count > 0)
                {

                    dt.Columns.Remove("Ro");
                    dt.Columns.Remove("CBDW_BM");
                    dt.Columns["CBDW_MC"].ColumnName = "承办单位";
                    dt.Columns["AJNUM"].ColumnName = ((VersionName)0).ToString() + "受理数";
                    dt.Columns["ZZNUM"].ColumnName = "已制作数";
                    dt.Columns["WZNUM"].ColumnName = "未制作数";
                    dt.Columns["ZZLNUM"].ColumnName = "制作率";

                    msgs = DataToExcel_Ex.Export(dt, "卷宗制作率统计", Server.MapPath("/" + ExcelFolder + "/" + filename));
                    //filename = dte.DataExcel(dt, "卷宗制作率统计", FilePath, nameList, valueList);
                }

                if (string.IsNullOrEmpty(msgs))
                {
                    return ReturnString.JsonToString(Prompt.win, "/" + ExcelFolder + "/" + filename, null);
                }
                else
                    return ReturnString.JsonToString(Prompt.error, StringPlus.String2Json(msgs), null);                          
            }
            return ReturnString.JsonToString(Prompt.error, "导出失败", null);
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
                where += " and aj.cjsj >= to_date('" + StringPlus.ReplaceSingle(timebegin) + "','yyyy-mm-dd')";
            }
            if (!string.IsNullOrEmpty(timeend))
            {
                where += " and aj.cjsj <= to_date('" + Convert.ToDateTime(StringPlus.ReplaceSingle(timeend)).AddDays(1).ToString("yyyy-MM-dd") + "','yyyy-mm-dd')";
            }



            if (!string.IsNullOrEmpty(dwbm))
            {
                where += " and cbdw_bm = " + StringPlus.ReplaceSingle(dwbm) + "";
            }

            //权限获取案件类别编码
            //EDRS.BLL.XT_DM_QX bllQx = new EDRS.BLL.XT_DM_QX(Request);
            //DataSet dsLbQx = bllQx.GetLBList(base.Jsbms, base.UserInfo.DWBM, base.Bmbms, "");
            //if (dsLbQx != null && dsLbQx.Tables.Count > 0 && dsLbQx.Tables[0].Rows.Count > 0)
            //{
            //    string ajlbs = "";
            //    foreach (DataRow dr in dsLbQx.Tables[0].Rows)
            //    {
            //        ajlbs += dr["QXBM"] + ",";
            //    }
            //    where += " and trim(aj.ajlb_bm) in (" + StringPlus.ReplaceSingle(ajlbs.Substring(0, ajlbs.Length - 1)) + ")";
            //}

            where += " and aj.ajlb_bm in (select distinct QXBM FROM XT_DM_QX where JSBM IN (" + Jsbms + ") AND QXLX=1 AND trim(DWBM)='" + UserInfo.DWBM + "' AND BMBM IN (" + Bmbms + ")) ";

            EDRS.BLL.EDRS_Report bll = new EDRS.BLL.EDRS_Report(this.Request);
            int count = 0;
            DataSet ds = bll.GetCaseGroupByUnitYw(where, UserInfo.DWBM, UserInfo.GH, int.MaxValue, 1, "YWBM", out count, values);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                // DataTable dt = ds.Tables[0].Copy();

                ////读取配置文件中的配置
                //int? isLocalAjxx = EDRS.Common.ConfigHelper.GetConfigInt("IsLocalAjxx");
                //if (isLocalAjxx == 1)
                //{
                //    string msg = "";
                //    DateTime? b_date = null;
                //    if (!string.IsNullOrEmpty(timebegin))
                //        b_date = Convert.ToDateTime(timebegin);

                //    DateTime? e_date = null;
                //    if (!string.IsNullOrEmpty(timeend))
                //        e_date = Convert.ToDateTime(timeend);
                //    EDRS.Common.IceServicePrx iceprx = new IceServicePrx();

                //    EDRS.BLL.TYYW_GG_AJJBXX ajbll = new EDRS.BLL.TYYW_GG_AJJBXX(Request);
                //    msg = ajbll.GetConfiguration(UserInfo.DWBM, UserInfo.GH, iceprx);
                //    if (!string.IsNullOrEmpty(msg))
                //        Response.Write(msg);

                //    //获取业务编码对应的案件类别集合
                //    EDRS.BLL.XT_DM_AJLBBM ajlbBll = new EDRS.BLL.XT_DM_AJLBBM(Request);
                //    List<EDRS.Model.XT_DM_AJLBBM> ywds = ajlbBll.GetModelList(" and  SFSC='N'");
                //    List<string> ajlblist;

                //    foreach (DataRow dr in dt.Rows)
                //    {
                //        ajlblist = ywds.Where(p => p.YWBM == dr["YWBM"].ToString()).Select(o => o.YWBM).ToList();

                //        dr["AJNUM"] = iceprx.GetAjjbxxCount(ajlblist, dwbm, b_date, e_date, true, out msg);
                //        dr["WZNUM"] = int.Parse(dr["AJNUM"].ToString()) - int.Parse(dr["ZZNUM"].ToString());
                //        dr["ZZLNUM"] = Math.Round((decimal.Parse(dr["ZZNUM"].ToString()) / decimal.Parse(dr["AJNUM"].ToString())) * 100, 2);
                //    }

                //    return "{\"Total\":0,\"Rows\":" + JsonHelper.JsonString(dt) + "}";
                //}
                //else
                //{
                    DataView dv = ds.Tables[0].DefaultView;
                    dv.Sort = sortname;
                    return "{\"Total\":" + count + ",\"Rows\":" + JsonHelper.JsonString(dv.ToTable()) + "}";
               // }
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
                where += " and aj.cjsj >= to_date('" + StringPlus.ReplaceSingle(timebegin) + "','yyyy-mm-dd')";
            }
            if (!string.IsNullOrEmpty(timeend))
            {
                where += " and aj.cjsj <= to_date('" + Convert.ToDateTime(StringPlus.ReplaceSingle(timeend)).AddDays(1).ToString("yyyy-MM-dd") + "','yyyy-mm-dd')";
            }
            if (!string.IsNullOrEmpty(dwbm))
            {
                where += " and cbdw_bm = " + StringPlus.ReplaceSingle(dwbm) + "";
            }

            //权限获取案件类别编码
            //EDRS.BLL.XT_DM_QX bllQx = new EDRS.BLL.XT_DM_QX(Request);
            //DataSet dsLbQx = bllQx.GetLBList(base.Jsbms, base.UserInfo.DWBM, base.Bmbms, "");
            //if (dsLbQx != null && dsLbQx.Tables.Count > 0 && dsLbQx.Tables[0].Rows.Count > 0)
            //{
            //    string ajlbs = "";
            //    foreach (DataRow dr in dsLbQx.Tables[0].Rows)
            //    {
            //        ajlbs += dr["QXBM"] + ",";
            //    }
            //    where += " and trim(aj.ajlb_bm) in (" + StringPlus.ReplaceSingle(ajlbs.Substring(0, ajlbs.Length - 1)) + ")";
            //}
            where += " and aj.ajlb_bm in (select distinct QXBM FROM XT_DM_QX where JSBM IN (" + Jsbms + ") AND QXLX=1 AND trim(DWBM)='" + UserInfo.DWBM + "' AND BMBM IN (" + Bmbms + ")) ";


            EDRS.BLL.EDRS_Report bll = new EDRS.BLL.EDRS_Report(this.Request);
            int count = 0;
            DataSet ds = bll.GetCaseGroupByUnitLb(where, UserInfo.DWBM, UserInfo.GH, int.MaxValue, 1, "ajlb_bm", out count, values);

           
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                //DataTable dt = ds.Tables[0].Copy();

                ////读取配置文件中的配置
                //int? isLocalAjxx = EDRS.Common.ConfigHelper.GetConfigInt("IsLocalAjxx");
                //if (isLocalAjxx == 1)
                //{
                //    string msg = "";
                //    DateTime? b_date = null;
                //    if (!string.IsNullOrEmpty(timebegin))
                //        b_date = Convert.ToDateTime(timebegin);

                //    DateTime? e_date = null;
                //    if (!string.IsNullOrEmpty(timeend))
                //        e_date = Convert.ToDateTime(timeend);
                //    EDRS.Common.IceServicePrx iceprx = new IceServicePrx();

                //    EDRS.BLL.TYYW_GG_AJJBXX ajbll = new EDRS.BLL.TYYW_GG_AJJBXX(Request);
                //    msg = ajbll.GetConfiguration(UserInfo.DWBM, UserInfo.GH, iceprx);
                //    if (!string.IsNullOrEmpty(msg))
                //        Response.Write(msg);

                //    foreach (DataRow dr in dt.Rows)
                //    {
                //        dr["AJNUM"] = iceprx.GetAjjbxxCount(new List<string> { dr["ajlb_bm"].ToString() }, dwbm, b_date, e_date, true, out msg);
                //        dr["WZNUM"] = int.Parse(dr["AJNUM"].ToString()) - int.Parse(dr["ZZNUM"].ToString());
                //        dr["ZZLNUM"] = Math.Round((decimal.Parse(dr["ZZNUM"].ToString()) / decimal.Parse(dr["AJNUM"].ToString())) * 100, 2);
                //    }

                //    return "{\"Total\":0,\"Rows\":" + JsonHelper.JsonString(dt) + "}";
                //}
                //else
                //{
                    DataView dv = ds.Tables[0].DefaultView;
                    dv.Sort = sortname;
                    return "{\"Total\":" + count + ",\"Rows\":" + JsonHelper.JsonString(dv.ToTable()) + "}";
                //}
            }

            return ReturnString.JsonToString(Prompt.error, "未找到统计信息", null);
        }
        #endregion
    }
}