using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Cyvation.CCQE.Web;
using Cyvation.CCQE.Common;
using System.Web.Script.Serialization;
using EDRS.Common;

namespace WebUI.Handler.ZZJG
{
    /// <summary>
    /// DZJZ_Report 的摘要说明
    /// </summary>
    public class DZJZ_Report : AshxBase
    {
        
        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);
            if (UserInfo == null) return;
            context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            context.Response.ContentType = "text/plain";
            string action = context.Request["action"];

            switch (action)
            { 
                case "GetAjlx":
                    GetAjlx(context);
                    break;
                case "GetAjxx":
                    GetAjxx(context);
                    break;
                case "GetJxx":
                    GetJxx(context);
                    break;
                case "GetMlxx":
                    //GetMlxx(context);
                    break;
                case "GetWjxx":
                    //GetWjxx(context);
                    break;
                case "GetMakeCaseReport":
                    GetMakeCaseReport(context);
                    break;
                case "GetAJReportByUnit":
                    GetAJReportByUnit(context);
                    break;
                case "BusinessList":
                    GetBusinessList(context);
                    break;
                case "BusinessReport":
                    GetBusinessReport(context);
                    break;
                case "GetAllBusinessType":
                    GetAllBusinessType(context);
                    break;
                case "caseByMouth":
                    GetCaseByMouth(context);
                    break;
                case "GetMakeCaseReportDetail":
                    GetMakeCaseReportDetail(context);
                    break;
                case "GetAjlxList":
                    GetAjlxList(context);
                    break;
                case "GetDwbm":
                    EDRS.BLL.XT_DM_QX bll = new EDRS.BLL.XT_DM_QX(context.Request);
                    DataSet ds = bll.GetDwQxList(UserInfo.DWBM, Jsbms);
                    DataTable dt = null;
                    string key = context.Request.Form["key"];
                    if (key != null && !string.IsNullOrEmpty(key))
                    {
                        DataView dv = ds.Tables[0].DefaultView;
                        dv.RowFilter = string.Format("DWMC like '%{0}%'", key);
                        dt = dv.ToTable();
                    }
                    else
                    {
                        dt = ds.Tables[0].Copy();
                    }

                   // var obj = GetDwbm(ds);
                   // string jsonResult = new JavaScriptSerializer().Serialize(obj);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        dt.Columns["DWBM"].ColumnName = "id";
                        dt.Columns["FDWBM"].ColumnName = "pid";
                        dt.Columns["DWMC"].ColumnName = "text";
                        //DataColumn col = new DataColumn();
                        //col.ColumnName = "icon";
                        //col.DefaultValue = "'/images/icons/3.png'";
                        //dt.Columns.Add(col);
                        string str = EDRS.Common.JsonHelper.JsonString(dt);

                        context.Response.Write(str);
                        context.Response.End();
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(key))
                            context.Response.Write(ReturnString.JsonToString(Prompt.error, "未获取单位编码，请确认是否设置权限？", null));
                    }
                    break;
            }
        }

        private object GetDwbm(DataSet ds, string pid = "")
        {
            //if (!data.ContainsKey(type)) return null;
            var arr = new List<object>() { };
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return arr;
            }
            DataRow[] drs;
            if (string.IsNullOrEmpty(pid))
            {
                drs = ds.Tables[0].Select("Fdwbm is null");
            }
            else
            {
                drs = ds.Tables[0].Select("Fdwbm='" + pid + "'");
            }
            foreach(DataRow dr in drs)
            {
                var children = GetDwbm(ds, dr["dwbm"].ToString());
                var o = new Dictionary<string, object>();
                o["text"] = dr["dwmc"];
                o["id"] = dr["dwbm"];
                o["icon"] = "parent.tree_dw";
                if ((children as List<object>).Count > 0)
                {
                    o["children"] = children;
                }
                arr.Add(o);
            }
            return arr;
        }

        private void GetAjlxList(HttpContext context)
        {
            List<object> objValue = new List<object>();


            string where = " and jsbm in ( select jsbm from xt_qx_ryjsfp where trim(dwbm)= :dwbm and gh=:gh )";
            where += " and trim(dwbm)= :dwbm2 and qxlx=:qxlx";

            objValue.Add(this.UserInfo.DWBM);
            objValue.Add(this.UserInfo.GH);
            objValue.Add(this.UserInfo.DWBM);
            objValue.Add(1);
            string key = context.Request.Form["key"];
            if (!string.IsNullOrEmpty(key))
            {
                where += " and QXMC like :QXMC ";
                objValue.Add("%" + key + "%");
               
            }
            string ywbm = context.Request["ywbm"];
            if (!string.IsNullOrEmpty(ywbm))
            {
                where += "  and qxbm in (select ajlbbm from xt_dm_ajlbbm where ywbm = :ywbm)";
                objValue.Add(ywbm);

            }
            EDRS.BLL.XT_DM_QX bll = new EDRS.BLL.XT_DM_QX(context.Request);
            DataSet ds = bll.GetQxListByRole(where, objValue.ToArray());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                dt.Columns["QXBM"].ColumnName = "id";
                dt.Columns["QXMC"].ColumnName = "text";
                context.Response.Write(EDRS.Common.JsonHelper.JsonString(dt));
            }
            else
            {
                context.Response.Write(EDRS.Common.ReturnString.JsonToString(Prompt.error, "未找到相关类别", null));
            }
        }

        private void GetMakeCaseReportDetail(HttpContext context)
        {
            string groupType = context.Request["groupType"];
            string pageIndex = context.Request["page"];
            string pageSize = context.Request["pagesize"];
            //where
            string b_date = context.Request["b_date"];
            string e_date = context.Request["e_date"];
            string username = context.Request["username"];
            string unit = context.Request["txt_unit"];

            //排序
            string sortname = context.Request["sortname"];
            string sortorder = context.Request["sortorder"];
            if (groupType == "businessType" || (string.IsNullOrEmpty(sortname) || sortname.ToLower() == "ywmc"))
                sortname = "YWBM";
            if (groupType == "groupByType" || (string.IsNullOrEmpty(sortname) || sortname.ToLower() == "ajlbmc"))
                sortname = "AJLBBM";
            sortname += " " + sortorder;

            string where = "";
            List<object> values = new List<object>();
            if (!string.IsNullOrEmpty(b_date))
            {
                //JZ.Cjsj（创建时间） / JZ.JZSCSJ（上传时间）
                where += " and JZ.Cjsj >= to_date('" + Convert.ToDateTime(b_date).ToShortDateString() + "','yyyy-mm-dd')";
            }
            if (!string.IsNullOrEmpty(e_date))
            {
                where += " and JZ.Cjsj <= to_date('" + Convert.ToDateTime(e_date).AddDays(1).ToShortDateString() + "','yyyy-mm-dd')";

            }
            if (!string.IsNullOrEmpty(username))
            {
                where += " and JZ.JZSCRXM =";
                where += "'" + username + "'";
            }
            else
            {
                where += " and (JZ.JZSCRXM is null OR JZSCRXM = '')";
            }
            if (!string.IsNullOrEmpty(unit))
            {
                where += " and CBDW_BM = ";
                where += "'" + unit + "'";
            }
            //单位权限
            //List<string> unitRoleList = GetDwBm(context, UserRole.DWBM, UserRole.BMBM, UserRole.JSBM);
            string qxWhere = " AND trim(DWBM) = '" + UserInfo.DWBM + "' AND JSBM in (" + Jsbms + ") AND BMBM in (" + Bmbms + ") AND QXLX = 0";
            EDRS.BLL.EDRS_Report bll = new EDRS.BLL.EDRS_Report(context.Request);
            int count;
            DataSet ds = bll.GetCaseByPerson(groupType, where, qxWhere, Convert.ToInt32(pageSize), Convert.ToInt32(pageIndex), sortname, out count, values.ToArray());

            if (ds != null && ds.Tables.Count > 0)
            {
                //获取json数据
                OperateLog.AddLog(OperateLog.LogType.卷宗制作工作量统计Web, "查询卷宗制作工作量统计列表成功！", UserInfo, UserRole, context.Request);
                string json = "{\"Rows\":" + ds.Tables[0].ToDatagridJson() + ",\"Total\":" + count + "}";// EDRS.Common.JsonHelper.JsonString(ds.Tables[0]);
                context.Response.Write(json);
            }
            else
            {
                OperateLog.AddLog(OperateLog.LogType.卷宗制作工作量统计Web, "查询卷宗制作工作量统计列表失败！", UserInfo, UserRole, context.Request);
                context.Response.Write("{\"Rows\":" + "[]" + ",\"Total\":" + count + "}");
            }
        }

        private void GetCaseByMouth(HttpContext context)
        {
            string where = "";
            List<object> values = new List<object>();
            string unit = context.Request["unit"];//承办单位
            string timebegin = context.Request["timebegin"];//开始日期
            string timeend = context.Request["timeend"];

            //排序
            string sortname = context.Request["sortname"];
            string sortorder = context.Request["sortorder"];
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
                //权限获取案件类别编码
                //EDRS.BLL.XT_DM_QX bllQxdw = new EDRS.BLL.XT_DM_QX(context.Request);
                //DataSet dsLbQxdw = bllQxdw.GetDwList(base.Jsbms, base.UserInfo.DWBM, base.Bmbms, "");
                //if (dsLbQxdw != null && dsLbQxdw.Tables.Count > 0 && dsLbQxdw.Tables[0].Rows.Count > 0)
                //{
                //    string ajlbs = "";
                //    foreach (DataRow dr in dsLbQxdw.Tables[0].Rows)
                //    {
                //        ajlbs += dr["QXBM"] + ",";
                //    }
                //    where += " and CBDW_BM in (" + StringPlus.ReplaceSingle(ajlbs.Substring(0, ajlbs.Length - 1)) + ")";
                //}
                where += " and CBDW_BM in (select distinct QXBM FROM XT_DM_QX  where 1=1 AND  JSBM IN (" + base.Jsbms + ") AND QXLX=0 AND trim(DWBM) = '" + base.UserInfo.DWBM + "' AND BMBM in (" + base.Bmbms + ") )";
            }

            //strWhere += " and (select count(1) from yx_dzjz_jzml y where y.Bmsah=T1.Bmsah) > 0";//已关联
            if (!string.IsNullOrEmpty(timebegin))
            {
                where += " and aj.cjsj >= to_date('" + Convert.ToDateTime(timebegin).ToShortDateString() + "','yyyy-mm-dd')";
            }
            if (!string.IsNullOrEmpty(timeend))
            {
                where += " and aj.cjsj < to_date('" + Convert.ToDateTime(timeend).AddDays(1).ToShortDateString() + "','yyyy-mm-dd')";
            }
            //单位权限
            //List<string> unitRoleList = GetDwBm(UserRole.DWBM, UserRole.BMBM, UserRole.JSBM);

            //权限获取案件类别编码
            //EDRS.BLL.XT_DM_QX bllQx = new EDRS.BLL.XT_DM_QX(context.Request);
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
            where += " and trim(aj.ajlb_bm) in (select distinct QXBM FROM XT_DM_QX  where 1=1 AND  JSBM IN (" + base.Jsbms + ") AND QXLX=1 AND trim(DWBM) = '" + base.UserInfo.DWBM + "' AND BMBM in (" + base.Bmbms + ") )";

            EDRS.BLL.EDRS_Report bll = new EDRS.BLL.EDRS_Report(context.Request);
            string qxWhere = "";// " AND DWBM = '" + UserInfo.DWBM + "' AND JSBM in (" + Jsbms + ") AND BMBM in (" + Bmbms + ") AND QXLX = 0";
        
            
            DataSet ds = bll.GetCaseGroupMouth(where, qxWhere, sortname, values.ToArray());
            //ds.Tables[0].Columns[0].DataType = typeof(Int32);
            if (ds == null)
            {
                OperateLog.AddLog(OperateLog.LogType.卷宗月统计图Web, "查询卷宗月统计报表图表数据失败！", UserInfo, UserRole, context.Request);
            }
            else
            {
                OperateLog.AddLog(OperateLog.LogType.卷宗月统计图Web, "查询卷宗月统计报表图表数据成功！", UserInfo, UserRole, context.Request);
            }
            if (ds != null && ds.Tables.Count > 0)
            {
                string json = "{\"Rows\":" + ds.Tables[0].ToDatagridJson() + ",\"Total\":" + ds.Tables[0].Rows.Count + "}";// EDRS.Common.JsonHelper.JsonString(ds.Tables[0]);
                context.Response.Write(json);
            }
        }

        private void GetAllBusinessType(HttpContext context)
        {
            EDRS.BLL.EDRS_Report bll = new EDRS.BLL.EDRS_Report(context.Request);
            DataSet ds = bll.GetAllBusinessType();
            if (ds != null && ds.Tables.Count > 0)
            {
                context.Response.Write(ds.Tables[0].ToDatagridJson());
            }
        }

        public void GetBusinessReport(HttpContext context)
        {
            string strWhere = "";
            List<object> values = new List<object>();
            string pageIndex = context.Request["page"];
            string pageSize = context.Request["pagesize"];

            string unit = context.Request["unit"];//承办单位
            string timebegin = context.Request["timebegin"];//开始日期
            string timeend = context.Request["timeend"];//结束日期
            string count_start = context.Request["count_start"];
            string count_end = context.Request["count_end"];
            if (!string.IsNullOrEmpty(unit))
            {
                strWhere += " AND CBDW_MC LIKE '%" + unit + "%'";
            }
            if (!string.IsNullOrEmpty(timebegin))
            {
                strWhere += " and CJSJ >= to_date('" + Convert.ToDateTime(timebegin).ToShortDateString() + "','yyyy-mm-dd')";
            }
            if (!string.IsNullOrEmpty(timeend))
            {
                strWhere += " and CJSJ <= to_date('" + Convert.ToDateTime(timeend).AddDays(1).ToShortDateString() + "','yyyy-mm-dd')";
            }
            string havingWhere = " HAVING 1=1 ";
            if (Convert.ToInt32(count_start) > 0)
            {
                havingWhere += " AND SUM(CASE WHEN ISREGARD > 0 THEN 1 ELSE 0 END) >=" + count_start;
            }
            if (Convert.ToInt32(count_end) > 0)
            {
                havingWhere += " AND SUM(CASE WHEN ISREGARD > 0 THEN 1 ELSE 0 END) <=" + count_end;
            }
            string qxWhere = " AND trim(DWBM) = '" + UserInfo.DWBM + "' AND JSBM in (" + Jsbms + ") AND BMBM in (" + Bmbms + ") AND QXLX = 0";
            EDRS.BLL.EDRS_Report bll = new EDRS.BLL.EDRS_Report(context.Request);
            int count = 0;
            DataSet ds = bll.GetCaseBusinessReport(strWhere, havingWhere, qxWhere, Convert.ToInt32(pageSize), Convert.ToInt32(pageIndex), "", out count, values.ToArray());

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                OperateLog.AddLog(OperateLog.LogType.卷宗业务情况统计Web, "查询卷宗业务情况统计成功！", UserInfo, UserRole, context.Request);
                //获取json数据
                string json = "{\"Rows\":" + ds.Tables[0].ToDatagridJson() + ",\"Total\":" + count + "}";// EDRS.Common.JsonHelper.JsonString(ds.Tables[0]);
                context.Response.Write(json);
            }
            else
            {
                OperateLog.AddLog(OperateLog.LogType.卷宗业务情况统计Web, "查询卷宗业务情况统计失败！", UserInfo, UserRole, context.Request);
                //获取json数据
                string json = "{\"Rows\":" + "[]" + ",\"Total\":" + count + "}";// EDRS.Common.JsonHelper.JsonString(ds.Tables[0]);
                context.Response.Write(json);
            }
        }

        public void GetBusinessList(HttpContext context)
        {
            
                    //action: "BusinessList",
                    //key: $("#txt_key").val(),
                    //unit: $("#txt_unit").val(),
                    //casename: $("#txt_name").val(),
                    //dutyman: $("#txt_dutyman").val(),
                    //ywlx: $("#txt_ywlx").val(),
                    //timebegin: $('#txt_time_begin').val(),
                    //timeend: $('#txt_time_end').val()
            string strWhere = "";
            List<object> values = new List<object>();
            string pageIndex = context.Request["page"];
            string pageSize = context.Request["pagesize"];
            string key = context.Request["key"];//部门受案号
            string unit = context.Request["unit"];//承办单位
            string casename = context.Request["casename"];
            string dutyman = context.Request["dutyman"];
            string ywlx = context.Request["ywlx"];
            string timebegin = context.Request["timebegin"];//开始日期
            string timeend = context.Request["timeend"];//结束日期
            if (!string.IsNullOrEmpty(key))
            {
                strWhere += " AND BMSAH LIKE '%" + key + "%'";
            }
            if (!string.IsNullOrEmpty(unit))
            {
                strWhere += " AND CBDW_MC LIKE '%" + unit + "%'";
            }
            if (!string.IsNullOrEmpty(casename))
            {
                strWhere += " AND AJMC LIKE '%" + casename + "%'";
            }
            if (!string.IsNullOrEmpty(dutyman))
            {
                strWhere += " AND CBR LIKE '%" + dutyman + "%'";
            }
            if (!string.IsNullOrEmpty(ywlx))
            {
                strWhere += " AND LB.YWBM = '" + ywlx + "'";
            }
            if (!string.IsNullOrEmpty(timebegin))
            {
                strWhere += " and CJSJ >= to_date('" + Convert.ToDateTime(timebegin).ToShortDateString() + "','yyyy-mm-dd')";
            }
            if (!string.IsNullOrEmpty(timeend))
            {
                strWhere += " and CJSJ <= to_date('" + Convert.ToDateTime(timeend).AddDays(1).ToShortDateString() + "','yyyy-mm-dd')";
            }
            string qxWhere = " AND trim(DWBM) = '" + UserInfo.DWBM + "' AND JSBM in (" + Jsbms + ") AND BMBM in (" + Bmbms + ") AND QXLX = 0";
            EDRS.BLL.EDRS_Report bll = new EDRS.BLL.EDRS_Report(context.Request);
            int count = 0;
            DataSet ds = bll.GetCaseBusiness(strWhere, qxWhere, Convert.ToInt32(pageSize), Convert.ToInt32(pageIndex), "", out count, values.ToArray());

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                //获取json数据
                string json = "{\"Rows\":" + ds.Tables[0].ToDatagridJson() + ",\"Total\":" + count + "}";// EDRS.Common.JsonHelper.JsonString(ds.Tables[0]);
                context.Response.Write(json);
                OperateLog.AddLog(OperateLog.LogType.卷宗业务情况Web, "查询卷宗业务情况列表成功！", UserInfo, UserRole, context.Request);
            }
            else
            {
                //获取json数据
                string json = "{\"Rows\":" + "[]" + ",\"Total\":" + count + "}";// EDRS.Common.JsonHelper.JsonString(ds.Tables[0]);
                context.Response.Write(json);
                OperateLog.AddLog(OperateLog.LogType.卷宗业务情况Web, "查询卷宗业务情况列表失败！", UserInfo, UserRole, context.Request);
            }
        }
        #region 根据单位查询案件信息
        private void GetAJReportByUnit(HttpContext context)
        {
            string strWhere = "";
            List<object> values = new List<object>();
            int pageIndex = Convert.ToInt32(context.Request["page"]);
            int pageSize = Convert.ToInt32(context.Request["pagesize"]);
            string key = context.Request["key"];//部门受案号
            string timebegin = context.Request["timebegin"];//开始日期
            string timeend = context.Request["timeend"];//结束日期
            string unit = context.Request["unit"];//承办单位
            string ywbm = context.Request["ywbm"];
            string caseajlb = context.Request["caseajlb"];
            //排序
            string sortname = context.Request["sortname"];
            string sortorder = context.Request["sortorder"];
            if (string.IsNullOrEmpty(sortname) || sortname.ToLower() == "cbdw_mc")
                sortname = "CBDW_BM";
            sortname += " " + sortorder;

           // string qxWhere = "";
          //  qxWhere = " AND DWBM = '" + UserInfo.DWBM + "' AND JSBM in (" + Jsbms + ") AND BMBM in (" + Bmbms + ") AND QXLX = 0";
            //选中时，筛选单位，否则查询所有有权限数据
            EDRS.BLL.XT_DM_QX bllQx = new EDRS.BLL.XT_DM_QX(context.Request);
            if (!string.IsNullOrEmpty(unit))
            {
                string dwbms = unit.Replace(";", ",");
                strWhere += " and CBDW_BM in (" + StringPlus.ReplaceSingle(dwbms) + ")";
            }
            else if (string.IsNullOrEmpty(unit))
            {
               
                //DataSet dsDwQx = bllQx.GetDwList(base.Jsbms, base.UserInfo.DWBM, base.Bmbms, "");
                //if (dsDwQx != null && dsDwQx.Tables.Count > 0 && dsDwQx.Tables[0].Rows.Count > 0)
                //{
                //    string dwbms = "";
                //    foreach (DataRow dr in dsDwQx.Tables[0].Rows)
                //    {
                //        dwbms += dr["QXBM"] + ",";
                //    }
                   // strWhere += " and AJ.CBDW_BM in (" + StringPlus.ReplaceSingle(dwbms.Substring(0, dwbms.Length - 1)) + ")";
                strWhere += " and AJ.CBDW_BM in (select distinct QXBM FROM XT_DM_QX  where 1=1 AND  JSBM IN (" + base.Jsbms + ") AND QXLX=0 AND trim(DWBM) = '" + base.UserInfo.DWBM + "' AND BMBM in (" + base.Bmbms + ") )";
                 ////    where_dw = " and jz.dwbm in (" + StringPlus.ReplaceSingle(dwbms.Substring(0, dwbms.Length - 1)) + ")";
                //}
                //else
                //    strWhere += " and AJ.CBDW_BM = " + base.UserInfo.DWBM;
            }
            //案件类别
            if (!string.IsNullOrEmpty(caseajlb))
            {
                string caseajlbs = caseajlb.Replace(";", ",");
                strWhere += " and trim(AJ.AJLB_BM) in (" + caseajlbs + ")";
            }
            else if (string.IsNullOrEmpty(caseajlb))
            {
                //DataSet dsLbQx = bllQx.GetLBList(base.Jsbms, base.UserInfo.DWBM, base.Bmbms, "");
                //if (dsLbQx != null && dsLbQx.Tables.Count > 0 && dsLbQx.Tables[0].Rows.Count > 0)
                //{
                //    string ajlbs = "";
                //    foreach (DataRow dr in dsLbQx.Tables[0].Rows)
                //    {
                //        ajlbs += dr["QXBM"] + ",";
                //    }
                //    strWhere += " and trim(aj.ajlb_bm) in (" + StringPlus.ReplaceSingle(ajlbs.Substring(0, ajlbs.Length - 1)) + ")";
                strWhere += " and trim(aj.ajlb_bm) in (select distinct QXBM FROM XT_DM_QX  where 1=1 AND  JSBM IN (" + base.Jsbms + ") AND QXLX=1 AND trim(DWBM) = '" + base.UserInfo.DWBM + "' AND BMBM in (" + base.Bmbms + ") )";
                //}
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

     
            //单位权限

            //List<string> unitRoleList = GetDwBm(context,UserRole.DWBM, UserRole.BMBM, UserRole.JSBM);
            
            EDRS.BLL.EDRS_Report bll = new EDRS.BLL.EDRS_Report(context.Request);
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

                    EDRS.BLL.TYYW_GG_AJJBXX ajbll = new EDRS.BLL.TYYW_GG_AJJBXX(context.Request);
                    msg = ajbll.GetConfiguration(UserInfo.DWBM, UserInfo.GH, iceprx);
                    if (!string.IsNullOrEmpty(msg))
                        context.Response.Write(msg);

                    foreach (DataRow dr in dt.Rows)
                    {                        
                        dr["AJNUM"] = iceprx.GetAjjbxxCount(null, dr["CBDW_BM"].ToString(), b_date, e_date, true, out msg);
                        dr["WZNUM"] = int.Parse(dr["AJNUM"].ToString()) - int.Parse(dr["ZZNUM"].ToString());
                        dr["ZZLNUM"] = Math.Round((decimal.Parse(dr["ZZNUM"].ToString()) / decimal.Parse(dr["AJNUM"].ToString())) * 100, 2);
                    }
                }
                //获取json数据
                OperateLog.AddLog(OperateLog.LogType.卷宗业务情况Web, "查询单位卷宗统计列表数据成功！", UserInfo, UserRole, context.Request);
                string json = "{\"Rows\":" + dt.ToDatagridJson() + ",\"Total\":" + count + "}";// EDRS.Common.JsonHelper.JsonString(ds.Tables[0]);
                context.Response.Write(json);
            }else
            {
                OperateLog.AddLog(OperateLog.LogType.卷宗业务情况Web, "查询单位卷宗统计列表数据失败！", UserInfo, UserRole, context.Request);
                //获取json数据
                string json = "{\"Rows\":" + "[]" + ",\"Total\":" + count + "}";// EDRS.Common.JsonHelper.JsonString(ds.Tables[0]);
                context.Response.Write(json);
            }
        }
        #endregion
        #region 卷宗制作工作量查询 -- MakeCaseReport.aspx
        public void GetMakeCaseReport(HttpContext context)
        {
            int pageIndex = Convert.ToInt32(context.Request.Form["page"]);
            int pageSize = Convert.ToInt32(context.Request.Form["pagesize"]);
            //where
            string b_date = context.Request.Form["b_date"];
            string e_date = context.Request.Form["e_date"];
            string username = context.Request.Form["username"];
            string unit = context.Request.Form["unit"];

            //排序
            string sortname = context.Request.Form["sortname"];
            string sortorder = context.Request.Form["sortorder"];
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
                //EDRS.BLL.XT_DM_QX bllQx = new EDRS.BLL.XT_DM_QX(context.Request);
                //DataSet dsDwQx = bllQx.GetDwList(base.Jsbms, base.UserInfo.DWBM, base.Bmbms, "");
                //if (dsDwQx != null && dsDwQx.Tables.Count > 0 && dsDwQx.Tables[0].Rows.Count > 0)
                //{
                //    string dwbms = "";
                //    foreach (DataRow dr in dsDwQx.Tables[0].Rows)
                //    {
                //        dwbms += dr["QXBM"] + ",";
                //    }
                //    where += " and CBDW_BM in (" + StringPlus.ReplaceSingle(dwbms.Substring(0, dwbms.Length - 1)) + ")";
                //    // where_dw = " and jz.dwbm in (" + StringPlus.ReplaceSingle(dwbms.Substring(0, dwbms.Length - 1)) + ")";
                //}
                //else
                //    where += " and CBDW_BM =" + base.UserInfo.DWBM;
                where += " and CBDW_BM in (select distinct QXBM FROM XT_DM_QX  where 1=1 AND  JSBM IN (" + base.Jsbms + ") AND QXLX=0 AND trim(DWBM) = '" + base.UserInfo.DWBM + "' AND BMBM in (" + base.Bmbms + ") )";
            }       


            //单位权限
            //List<string> unitRoleList = GetDwBm(context, UserRole.DWBM, UserRole.BMBM, UserRole.JSBM);
            string qxWhere = " AND trim(DWBM) = '" + UserInfo.DWBM + "' AND JSBM in (" + Jsbms + ") AND BMBM in (" + Bmbms + ") AND QXLX = 0";
            EDRS.BLL.EDRS_Report bll = new EDRS.BLL.EDRS_Report(context.Request);
            int count;
            DataSet ds = bll.GetCaseByPerson(where, qxWhere, (pageSize * pageIndex), (pageSize * pageIndex) - pageSize + 1, sortname, out count, values.ToArray());

            if (ds != null && ds.Tables.Count > 0)
            {
                //获取json数据
                OperateLog.AddLog(OperateLog.LogType.卷宗制作工作量统计Web, "查询卷宗制作工作量统计列表成功！", UserInfo, UserRole, context.Request);
                string json = "{\"Rows\":" + ds.Tables[0].ToDatagridJson() + ",\"Total\":" + count + "}";// EDRS.Common.JsonHelper.JsonString(ds.Tables[0]);
                context.Response.Write(json);
            }
            else
            {
                OperateLog.AddLog(OperateLog.LogType.卷宗制作工作量统计Web, "查询卷宗制作工作量统计列表失败！", UserInfo, UserRole, context.Request);
                context.Response.Write("{\"Rows\":" + "[]" + ",\"Total\":" + count + "}");
            }
        }

        /// <summary>
        /// 获取案件类型权限编码集合
        /// </summary>
        /// <param name="dwbm"></param>
        /// <param name="bmbm"></param>
        /// <param name="jsbm"></param>
        /// <returns></returns>
        public List<string> GetAjTypeBm(HttpContext context,string dwbm, string bmbm, string jsbm)
        {
            EDRS.BLL.XT_DM_QX bll = new EDRS.BLL.XT_DM_QX(context.Request);
            DataSet ds = bll.GetLBList(jsbm, dwbm, bmbm, "");
            List<string> list = new List<string>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow ro in ds.Tables[0].Rows)
                    list.Add(ro["QXBM"].ToString());
            }
            return list;
        }
        #endregion
        #region 卷宗统计报表  -- CaseReport.aspx

        public void GetAjlx(HttpContext context)
        {
            EDRS.BLL.EDRS_Report bll = new EDRS.BLL.EDRS_Report(context.Request);

            string strWhere = "";
            List<object> values = new List<object>();
            string key = context.Request["key"];//部门受案号
            string casename = context.Request["casename"];//案件名称
            string dutyman = context.Request["dutyman"];//承办人
            string relevance = context.Request["relevance"];//是否关联
            string timebegin = context.Request["timebegin"];//开始日期
            string timeend = context.Request["timeend"];//结束日期
            string cmbajlb = context.Request["cmbajlb"];//案件类型
            string strWhere1 = "";
            if (!string.IsNullOrEmpty(key))
            {
                strWhere += " and AJ.BMSAH like :BMSAH";
                values.Add("%" + key + "%");
            }
            if (!string.IsNullOrEmpty(casename))
            {
                strWhere += " and AJ.AJMC like :AJMC";
                values.Add("%" + casename + "%");
            }
            if (!string.IsNullOrEmpty(dutyman))
            {
                strWhere += " and AJ.CBR like :CBR";
                values.Add("%" + dutyman + "%");
            }
            if (!string.IsNullOrEmpty(relevance) && relevance != "0")
            {
                if (relevance == "1")
                    strWhere += " and (select count(1) from yx_dzjz_jzjbxx y where y.Bmsah=AJ.Bmsah) > 0";
                else if (relevance == "2")
                    strWhere += " and (select count(1) from yx_dzjz_jzjbxx y where y.Bmsah=AJ.Bmsah) = 0";
            }
            if (!string.IsNullOrEmpty(timebegin))
            {
                strWhere += " and AJ.SLRQ >= :SLRQ";
                values.Add(Convert.ToDateTime(timebegin));
            }
            if (!string.IsNullOrEmpty(timeend))
            {
                strWhere += " and AJ.SLRQ <= :SLRQ1";
                values.Add(Convert.ToDateTime(timeend).AddDays(1));
            }
            if (!string.IsNullOrEmpty(cmbajlb))
            {
                strWhere1 += " and AJ.AJLB_Mc like :AJLB";
                values.Add("%" + cmbajlb + "%");
            }
            DataTable dt = bll.GetListGroupByAjLx(strWhere, strWhere1, values.ToArray());

            
            //获取json数据
            string json = "{\"Rows\":" + dt.ToDatagridJson() +"}";// EDRS.Common.JsonHelper.JsonString(ds.Tables[0]);
            context.Response.Write(json);

        }
        public void GetAjxx(HttpContext context)
        {
            string strWhere = "";
            List<object> values = new List<object>();
            string ajlb_bm = context.Request["ajlb_bm"];
            if (!string.IsNullOrEmpty(ajlb_bm))
            {
                strWhere += " AND trim(T1.ajlb_bm) = :AJLB_BM";
                values.Add(ajlb_bm.ToString());
            }
            string key = context.Request["key"];//部门受案号
            string casename = context.Request["casename"];//案件名称
            string dutyman = context.Request["dutyman"];//承办人
            string relevance = context.Request["relevance"];//是否关联
            string timebegin = context.Request["timebegin"];//开始日期
            string timeend = context.Request["timeend"];//结束日期
            if (!string.IsNullOrEmpty(key))
            {
                strWhere += " and T1.BMSAH like :BMSAH";
                values.Add("%" + key + "%");
            }
            if (!string.IsNullOrEmpty(casename))
            {
                strWhere += " and T1.AJMC like :AJMC";
                values.Add("%" + casename + "%");
            }
            if (!string.IsNullOrEmpty(dutyman))
            {
                strWhere += " and T1.CBR like :CBR";
                values.Add("%" + dutyman + "%");
            }
            if (!string.IsNullOrEmpty(relevance) && relevance != "0")
            {
                if (relevance == "1")
                    strWhere += " and (select count(1) from yx_dzjz_jzjbxx y where y.Bmsah=T1.Bmsah) > 0";
                else if (relevance == "2")
                    strWhere += " and (select count(1) from yx_dzjz_jzjbxx y where y.Bmsah=T1.Bmsah) = 0";
            }
            if (!string.IsNullOrEmpty(timebegin))
            {
                strWhere += " and SLRQ >= :SLRQ";
                values.Add(Convert.ToDateTime(timebegin));
            }
            if (!string.IsNullOrEmpty(timeend))
            {
                strWhere += " and SLRQ <= :SLRQ1";
                values.Add(Convert.ToDateTime(timeend).AddDays(1));
            }
            EDRS.BLL.EDRS_Report bll = new EDRS.BLL.EDRS_Report(context.Request);

            DataTable dt = bll.GetListGroupByAj(strWhere, values.ToArray());


            //获取json数据
            string json = "{\"Rows\":" + dt.ToDatagridJson() + "}";// EDRS.Common.JsonHelper.JsonString(ds.Tables[0]);
            context.Response.Write(json);
        }
        public void GetJxx(HttpContext context)
        {
            string strWhere = "";
            List<object> values = new List<object>();
            string mlbh = context.Request["bmsah"];
            if (!string.IsNullOrEmpty(mlbh))
            {
                strWhere += " AND T1.bmsah = :bmsah";
                values.Add(mlbh.ToString());
            }
            EDRS.BLL.EDRS_Report bll = new EDRS.BLL.EDRS_Report(context.Request);

            List<object> list = new List<object>();
            DataTable dt = bll.GetListGroupByJ(strWhere, values.ToArray());
            foreach (DataRow dr in dt.Rows)
            {
                var d = new Dictionary<string, object>();
                foreach (DataColumn dc in dt.Columns)
                {
                    if (dc.ColumnName.ToLower() == "cjsj" || dc.ColumnName.ToLower() == "zhxgsj")
                    {
                        d[dc.ColumnName.ToLower()] = Convert.ToDateTime(dr[dc.ColumnName]).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        d[dc.ColumnName.ToLower()] = dr[dc.ColumnName] == null ? "" : dr[dc.ColumnName].ToString();
                    }
                }
                object children = GetMlxx(dr["mlbh"].ToString(), context);
                if (children != null)
                {
                    d["children"] = children;
                }
                list.Add(d);
            }
            context.Response.Write("{ Rows :" + new JavaScriptSerializer().Serialize(list) + "}");

            //获取json数据
            //string json = "{\"Rows\":" + dt.ToDatagridJson() + "}";// EDRS.Common.JsonHelper.JsonString(ds.Tables[0]);
            //context.Response.Write(json);
        }
        public object GetMlxx(string mlbh,HttpContext context)
        {
            string strWhere = "";
            List<object> values = new List<object>();
            if (!string.IsNullOrEmpty(mlbh))
            {
                strWhere += " AND T1.fmlbh = :mlbh";
                values.Add(mlbh.ToString());
            }
            EDRS.BLL.EDRS_Report bll = new EDRS.BLL.EDRS_Report(context.Request);

            DataTable dt = bll.GetListGroupByM(strWhere, values.ToArray());
            if (dt == null || dt.Rows.Count == 0)
                return null;
            List<object> list = new List<object>();
            foreach (DataRow dr in dt.Rows)
            {
                var d = new Dictionary<string, object>();
                foreach (DataColumn dc in dt.Columns)
                {
                    if (dc.ColumnName.ToLower() == "cjsj" || dc.ColumnName.ToLower() == "zhxgsj")
                    {
                        d[dc.ColumnName.ToLower()] = Convert.ToDateTime(dr[dc.ColumnName]).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        d[dc.ColumnName.ToLower()] = dr[dc.ColumnName] == null ? "" : dr[dc.ColumnName].ToString();
                    }
                }
                object children =  GetWjxx(dr["mlbh"].ToString(), context);
                if (children != null)
                {
                    d["children"] = children;
                }
                list.Add(d);
            }
            //获取json数据
            //string json = "{\"Rows\":" + dt.ToDatagridJson() + "}";// EDRS.Common.JsonHelper.JsonString(ds.Tables[0]);
            return list;
        }
        public object GetWjxx(string mlbh,HttpContext context)
        {
            string strWhere = "";
            List<object> values = new List<object>();
            if (!string.IsNullOrEmpty(mlbh))
            {
                strWhere += " AND T1.fmlbh = :mlbh";
                values.Add(mlbh.ToString());
            }
            EDRS.BLL.EDRS_Report bll = new EDRS.BLL.EDRS_Report(context.Request);
            DataTable dt = bll.GetWList(strWhere, values.ToArray());
            if (dt == null || dt.Rows.Count == 0)
                return null;
            List<object> list = new List<object>();
            foreach (DataRow dr in dt.Rows)
            {
                var d = new Dictionary<string, object>();
                foreach (DataColumn dc in dt.Columns)
                {
                    if (dc.ColumnName.ToLower() == "cjsj" || dc.ColumnName.ToLower() == "zhxgsj")
                    {
                        d[dc.ColumnName.ToLower()] = Convert.ToDateTime(dr[dc.ColumnName]).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        d[dc.ColumnName.ToLower()] = dr[dc.ColumnName] == null ? "" : dr[dc.ColumnName].ToString();
                    }
                }
                //d["children"] = GetWjxx(dr["mlbh"].ToString(), context);
                list.Add(d);
            }
            return list;
            //获取json数据
            //string json = "{\"Rows\":" + dt.ToDatagridJson() + "}";// EDRS.Common.JsonHelper.JsonString(ds.Tables[0]);
            //context.Response.Write(json);
        }

        #endregion


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}