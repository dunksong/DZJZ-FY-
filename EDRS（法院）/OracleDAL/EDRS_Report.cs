using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using System.Web;
using System.Data;

using EDRS.IDAL;
using Maticsoft.DBUtility;
using EDRS.Common;
//Please add references
namespace EDRS.OracleDAL
{
    public class EDRS_Report : IEDRS_Report
    {
        public HttpRequest context = null;//客户端对象，用于记录日志，客户端信息
        public void SetHttpContext(System.Web.HttpRequest _context)
        {
            this.context = _context;
        }


        #region 卷宗统计报表  -- CaseReport

        /// <summary>
        /// 根据分类获取案件
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="objValues"></param>
        /// <returns></returns>
        public DataTable GetListGroupByAjLx(string strWhere, string strWhere1, params object[] objValues)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT 
                                            COUNT(1)  AJCount,
                                            SUM(Case When IsRegard > 0 then 1 else 0 end) RegardCount,
                                            COUNT(1) - SUM(Case When IsRegard > 0 then 1 else 0 end) NotRegardCount,
                                            AJLB_Bm,
                                            AJLB_Mc ");
            strSql.Append(@" FROM 
                                        (
                                            SELECT 
                                                (select count(1) from yx_dzjz_jzjbxx ML where ML.Bmsah=AJ.Bmsah) IsRegard ,
                                                AJ.Ajlb_Bm,
                                                AJ.Ajlb_Mc 
                                            FROM 
                                            TYYW_GG_AJJBXX AJ
                                            WHERE 1=1");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" " + strWhere);
            }
            strSql.AppendFormat(") AJ");
            strSql.AppendFormat(" WHERE 1=1 " + strWhere1);
            strSql.Append(" GROUP BY AJLB_Bm,AJLB_Mc");
            strSql.Append(" having COUNT(1) > 0 ");//去除无案件的类别
            try
            {
                DataSet ds = DbHelperOra.Query(strSql.ToString(), ParameterHelp.ParameterReset(strWhere + strWhere1, objValues));
                if (ds != null && ds.Tables.Count > 0)
                {
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataTable GetListGroupByAjLx(string strWhere, params object[] objValues)", "EDRS.OracleDAL.EDRS_Report", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
        }
        /// <summary>
        /// 根据案件信息赛选案件
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="objValues"></param>
        /// <returns></returns>
        public DataTable GetListGroupByAj(string strWhere, params object[] objValues)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
                        select JCount,MCount,WCount,PageCount,(select count(1) from yx_dzjz_jzjbxx y where y.Bmsah=T1.Bmsah) IsRegard,
                        T1.AJMC,
                        T1.BMSAH,
                        T1.AJLB_MC,
                        T1.CBDW_MC,
                        T1.CBBM_MC,
                        T1.CBR,
                        T1.DQJD,
                        T1.SLRQ,
                        T1.AJZT,
                        T1.DQRQ,
                        T1.BJRQ,
                        T1.WCRQ,
                        T1.GDRQ,
                        T1.AJLB_BM,
                        T1.CBDW_BM  
                        ");
            strSql.Append(@"
                        from TYYW_GG_AJJBXX T1
                        LEFT JOIN
                        (
                            SELECT BMSAH,COUNT(1) JCount FROM YX_DZJZ_JZML WHERE MLLX = 1 AND SFSC='N' GROUP BY BMSAH
                        ) T2 ON (T1.BMSAH = T2.BMSAH)
                        LEFT JOIN 
                        (
                            SELECT BMSAH,COUNT(1) MCount FROM YX_DZJZ_JZML WHERE MLLX = 2 AND SFSC='N' GROUP BY BMSAH
                        ) T3 ON (T1.BMSAH = T3.BMSAH)
                        LEFT JOIN 
                        (
                            SELECT BMSAH,COUNT(1) WCount FROM YX_DZJZ_JZML WHERE MLLX = 3 AND SFSC='N' GROUP BY BMSAH
                        ) T4 ON (T1.BMSAH = T4.BMSAH)
                        LEFT JOIN
                        (
                             SELECT BMSAH,COUNT(1) PageCount FROM yx_dzjz_jzmlwj WHERE SFSC = 'N' GROUP BY BMSAH
                        ) T5 ON (T1.BMSAH = T5.BMSAH)");
            strSql.Append(@" where T1.SFSC='N' ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(strWhere);
            }
            try
            {
                DataSet ds = DbHelperOra.Query(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
                if (ds != null && ds.Tables.Count > 0)
                {
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataTable GetListGroupByAj(string strWhere, params object[] objValues)", "EDRS.OracleDAL.EDRS_Report", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
        }
        /// <summary>
        /// 根据卷筛选案件
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="objValues"></param>
        /// <returns></returns>
        public DataTable GetListGroupByJ(string strWhere, params object[] objValues)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT T1.BMSAH,T1.MLBH,CJSJ,ZHXGSJ,MLXSMC,SSLBBM,SSLBMC,T1.MLLX");
            strSql.Append(@" FROM YX_DZJZ_JZML T1");
            //strSql.Append(@"LEFT JOIN 
            //(
            //    SELECT FMLBH,COUNT(1) MCount FROM YX_DZJZ_JZML WHERE MLLX = 2 AND SFSC = 'N' GROUP BY FMLBH
            //) T2 ON (T1.MLBH = T2.FMLBH)
            //LEFT JOIN 
            //(
            //    SELECT CASE WHEN T1.FMLBH IS NULL THEN T2.FMLBH ELSE T1.FMLBH END FMLBH,T2.WCount FROM
            //    (
            //         SELECT FMLBH,COUNT(1) WCount FROM YX_DZJZ_JZML WHERE MLLX = 3  AND SFSC = 'N' GROUP BY FMLBH
            //    ) T2 
            //    LEFT JOIN
            //    (
            //           SELECT FMLBH,MLBH FROM YX_DZJZ_JZML WHERE MLLX = 2  AND SFSC = 'N'
            //    ) T1
            //    ON (T1.MLBH = T2.FMLBH)
            //) T3 ON (T1.MLBH = T3.FMLBH)");
            strSql.Append(" WHERE T1.MLLX = 1");
            strSql.Append(@" AND T1.SFSC = 'N'");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" order by T1.MLSXH");
            try
            {
                DataSet ds = DbHelperOra.Query(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
                if (ds != null && ds.Tables.Count > 0)
                {
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataTable GetListGroupByJ(string strWhere, params object[] objValues)", "EDRS.OracleDAL.EDRS_Report", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
        }
        /// <summary>
        /// 根据文件赛选案件
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="objValues"></param>
        /// <returns></returns>
        public DataTable GetListGroupByM(string strWhere, params object[] objValues)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT T1.BMSAH,T1.MLBH,T1.CJSJ,T1.ZHXGSJ,T1.MLXSMC,T1.SSLBBM,T1.SSLBMC,T1.MLLX,
                                        (SELECT COUNT(1) FROM YX_DZJZ_JZML T2 WHERE T2.FMLBH = T1.MLBH) WCount");
            strSql.Append(@"  FROM YX_DZJZ_JZML T1");
            strSql.Append(@" WHERE T1.SFSC = 'N'");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(strWhere);
            }
            strSql.Append("  order by T1.MLSXH");
            try
            {
                DataSet ds = DbHelperOra.Query(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
                if (ds != null && ds.Tables.Count > 0)
                {
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataTable GetListGroupByM(string strWhere, params object[] objValues)", "EDRS.OracleDAL.EDRS_Report", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
        }
        public DataTable GetWList(string strWhere, params object[] objValues)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT T1.BMSAH,MLBH,CJSJ,ZHXGSJ,MLXSMC,SSLBBM,SSLBMC,T1.MLLX ");
            strSql.Append(@" FROM YX_DZJZ_JZML T1 WHERE  T1.SFSC = 'N'");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(strWhere);
            }
            strSql.Append("  order by T1.MLSXH");
            try
            {
                DataSet ds = DbHelperOra.Query(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
                if (ds != null && ds.Tables.Count > 0)
                {
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public  DataTable GetWList(string strWhere, params object[] objValues)", "EDRS.OracleDAL.EDRS_Report", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
        }
        public DataTable GetMLList(string strWhere, params object[] objValues)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT distinct T1.BMSAH,MLBH,CJSJ,ZHXGSJ,MLXSMC,SSLBBM,SSLBMC,MLLX ");
            strSql.Append(@" FROM YX_DZJZ_JZML T1 WHERE  T1.SFSC = 'N'");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(strWhere);
            }
            try
            {
                DataSet ds = DbHelperOra.Query(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
                if (ds != null && ds.Tables.Count > 0)
                {
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataTable GetMLList(string strWhere, params object[] objValues)", "EDRS.OracleDAL.EDRS_Report", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
        }
        #endregion


        #region 卷宗制作工作量查询 -- MakeCaseReport.aspx
        public DataSet GetCaseByPerson(string strWhere, string roleDwbmWhere, int pageSize, int pageIndex, string orderby, out int count, params object[] objValues)
        {
            OracleParameter[] parameters = {
					new OracleParameter("p_where", OracleType.VarChar,int.MaxValue),
                    new OracleParameter("p_rwhere", OracleType.VarChar,1000),   
                     new OracleParameter("p_order", OracleType.VarChar,1000),       
                    new OracleParameter("p_pageindex", OracleType.Number),
                    new OracleParameter("p_pagesize", OracleType.Number),
                    new OracleParameter("p_count", OracleType.Number,8),
                    new OracleParameter("p_cursor",OracleType.Cursor),
                    new OracleParameter("p_errmsg", OracleType.VarChar,100)
                                           };
            
            parameters[0].Value = strWhere;
            parameters[1].Value = roleDwbmWhere;
            parameters[2].Value = orderby;
            parameters[3].Value = pageIndex;
            parameters[4].Value = pageSize;
            parameters[5].Direction = ParameterDirection.Output;
            parameters[6].Direction = ParameterDirection.Output;
            parameters[7].Direction = ParameterDirection.Output;

            try
            {
                DataSet ds = DbHelperOra.RunProcedure("pkg_dzjz_report.proc_report_getCaseByPerson", parameters, "proc_report_GetCaseByPerson");
                count = Convert.ToInt32(parameters[5].Value);
                return ds;
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetCaseByPerson(string strWhere, string roleDwbmWhere, int pageSize, int pageIndex, string orderby, out int count, params object[] objValues)", "EDRS.OracleDAL.DataStatistics", "pkg_dzjz_report.proc_report_jznumber", parameters);
            }
            count = 0;
            return null;
        }


        public DataSet GetCaseByPerson(string groupType,string strWhere, string roleDwbmWhere, int pageSize, int pageIndex, string orderby, out int count, params object[] objValues)
        {
            OracleParameter[] parameters = {
					new OracleParameter("p_where", OracleType.VarChar,1000),
                    new OracleParameter("p_rwhere", OracleType.VarChar,1000),             
                    new OracleParameter("p_order", OracleType.VarChar,1000),             
                    new OracleParameter("p_pageindex", OracleType.Number),
                    new OracleParameter("p_pagesize", OracleType.Number),
                    new OracleParameter("p_count", OracleType.Number,8),
                    new OracleParameter("p_cursor",OracleType.Cursor),
                    new OracleParameter("p_errmsg", OracleType.VarChar,100)
                                           };

            parameters[0].Value = strWhere;
            parameters[1].Value = roleDwbmWhere;
            parameters[2].Value = orderby;
            parameters[3].Value = pageIndex;
            parameters[4].Value = pageSize;
            parameters[5].Direction = ParameterDirection.Output;
            parameters[6].Direction = ParameterDirection.Output;
            parameters[7].Direction = ParameterDirection.Output;

            try
            {
                //根据业务
                string proc = "proc_report_getcasebypersonB";
                //根据类型
                if (groupType == "groupByType")
                {
                    proc = "proc_report_getcasebypersonL";
                }
                DataSet ds = null;
                ds = DbHelperOra.RunProcedure("pkg_dzjz_report." + proc, parameters, proc);
                    count = Convert.ToInt32(parameters[5].Value);
                return ds;
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetCaseByPerson(string strWhere, string roleDwbmWhere, int pageSize, int pageIndex, string orderby, out int count, params object[] objValues)", "EDRS.OracleDAL.DataStatistics", "pkg_dzjz_report.proc_report_jznumber", parameters);
            }
            count = 0;
            return null;
        }
        #endregion

        #region 根据单位获取案件制作率
        public DataSet GetCaseGroupByUnit(string where, string dwbm,string gh, int pageSize, int pageIndex, string orderby, out int count, params object[] objValues)
        {
            OracleParameter[] parameters = {
                    new OracleParameter("p_dwbm", OracleType.Char,6),     
                    new OracleParameter("p_gh", OracleType.Char,4),     
					new OracleParameter("p_where", OracleType.VarChar,int.MaxValue),                  
                    new OracleParameter("p_order", OracleType.VarChar,1000),   
                    new OracleParameter("p_pageindex", OracleType.Number),
                    new OracleParameter("p_pagesize", OracleType.Number),
                    new OracleParameter("p_count", OracleType.Number,8),
                    new OracleParameter("p_cursor",OracleType.Cursor),
                    new OracleParameter("p_errmsg", OracleType.VarChar,100)
                                           };
            parameters[0].Value = dwbm;
            parameters[1].Value = gh;
            parameters[2].Value = where;
            parameters[3].Value = orderby;
            parameters[4].Value = pageIndex;
            parameters[5].Value = pageSize;
            parameters[6].Direction = ParameterDirection.Output;
            parameters[7].Direction = ParameterDirection.Output;
            parameters[8].Direction = ParameterDirection.Output;

            try
            {
                DataSet ds = DbHelperOra.RunProcedure("pkg_dzjz_report.proc_report_CaseGroupByUnit", parameters, "proc_report_CaseGroupByUnit");
                count = Convert.ToInt32(parameters[6].Value);
                return ds;
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetCaseGroupByUnit(string where, string dwbm,string gh, int pageSize, int pageIndex, string orderby, out int count, params object[] objValues)", "EDRS.OracleDAL.DataStatistics", "pkg_dzjz_report.proc_report_jznumber", parameters);
            }
            count = 0;
            return null;
        }
        /// <summary>
        /// 按类别制作率
        /// </summary>
        /// <param name="where"></param>
        /// <param name="dwbm"></param>
        /// <param name="gh"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="orderby"></param>
        /// <param name="count"></param>
        /// <param name="objValues"></param>
        /// <returns></returns>
        public DataSet GetCaseGroupByUnitLb(string where, string dwbm, string gh, int pageSize, int pageIndex, string orderby, out int count, params object[] objValues)
        {
            OracleParameter[] parameters = {
                    new OracleParameter("p_dwbm", OracleType.Char,6),     
                    new OracleParameter("p_gh", OracleType.Char,4),     
					new OracleParameter("p_where", OracleType.VarChar,int.MaxValue),                  
                    new OracleParameter("p_order", OracleType.VarChar,1000),   
                    new OracleParameter("p_pageindex", OracleType.Number),
                    new OracleParameter("p_pagesize", OracleType.Number),
                    new OracleParameter("p_count", OracleType.Number,8),
                    new OracleParameter("p_cursor",OracleType.Cursor),
                    new OracleParameter("p_errmsg", OracleType.VarChar,100)
                                           };
            parameters[0].Value = dwbm;
            parameters[1].Value = gh;
            parameters[2].Value = where;
            parameters[3].Value = orderby;
            parameters[4].Value = pageIndex;
            parameters[5].Value = pageSize;
            parameters[6].Direction = ParameterDirection.Output;
            parameters[7].Direction = ParameterDirection.Output;
            parameters[8].Direction = ParameterDirection.Output;

            try
            {
                DataSet ds = DbHelperOra.RunProcedure("pkg_dzjz_report.proc_report_CaseGroupByUnitlb", parameters, "proc_report_CaseGroupByUnitlb");
                count = Convert.ToInt32(parameters[6].Value);
                return ds;
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet proc_report_CaseGroupByUnitlb(string where, string dwbm,string gh, int pageSize, int pageIndex, string orderby, out int count, params object[] objValues)", "EDRS.OracleDAL.DataStatistics", "pkg_dzjz_report.proc_report_CaseGroupByUnitlb", parameters);
            }
            count = 0;
            return null;
        }
        /// <summary>
        /// 按业务制作率
        /// </summary>
        /// <param name="where"></param>
        /// <param name="dwbm"></param>
        /// <param name="gh"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="orderby"></param>
        /// <param name="count"></param>
        /// <param name="objValues"></param>
        /// <returns></returns>
        public DataSet GetCaseGroupByUnitYw(string where, string dwbm, string gh, int pageSize, int pageIndex, string orderby, out int count, params object[] objValues)
        {
            OracleParameter[] parameters = {
                    new OracleParameter("p_dwbm", OracleType.Char,6),     
                    new OracleParameter("p_gh", OracleType.Char,4),     
					new OracleParameter("p_where", OracleType.VarChar,int.MaxValue),                  
                    new OracleParameter("p_order", OracleType.VarChar,1000),   
                    new OracleParameter("p_pageindex", OracleType.Number),
                    new OracleParameter("p_pagesize", OracleType.Number),
                    new OracleParameter("p_count", OracleType.Number,8),
                    new OracleParameter("p_cursor",OracleType.Cursor),
                    new OracleParameter("p_errmsg", OracleType.VarChar,100)
                                           };
            parameters[0].Value = dwbm;
            parameters[1].Value = gh;
            parameters[2].Value = where;
            parameters[3].Value = orderby;
            parameters[4].Value = pageIndex;
            parameters[5].Value = pageSize;
            parameters[6].Direction = ParameterDirection.Output;
            parameters[7].Direction = ParameterDirection.Output;
            parameters[8].Direction = ParameterDirection.Output;

            try
            {
                DataSet ds = DbHelperOra.RunProcedure("pkg_dzjz_report.proc_report_CaseGroupByUnityw", parameters, "proc_report_CaseGroupByUnityw");
                count = Convert.ToInt32(parameters[6].Value);
                return ds;
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet proc_report_CaseGroupByUnityw(string where, string dwbm,string gh, int pageSize, int pageIndex, string orderby, out int count, params object[] objValues)", "EDRS.OracleDAL.DataStatistics", "pkg_dzjz_report.proc_report_CaseGroupByUnityw", parameters);
            }
            count = 0;
            return null;
        }
        #endregion
        #region 按月统计案件
        public DataSet GetCaseGroupMouth(string where, string roleDwbmWhere, string orderby, params object[] objValues)
        {
            OracleParameter[] parameters = {
					new OracleParameter("p_where", OracleType.VarChar,int.MaxValue),
                    new OracleParameter("p_rwhere", OracleType.VarChar,int.MaxValue),   
                    new OracleParameter("p_order", OracleType.VarChar,1000),   
                    new OracleParameter("p_cursor",OracleType.Cursor),
                    new OracleParameter("p_errmsg", OracleType.VarChar,100)
                                           };

            parameters[0].Value = where;
            parameters[1].Value = roleDwbmWhere;
            parameters[2].Value = orderby;
            parameters[3].Direction = ParameterDirection.Output;
            parameters[4].Direction = ParameterDirection.Output;

            try
            {
                DataSet ds = DbHelperOra.RunProcedure("pkg_dzjz_report.proc_report_CaseGroupMouth", parameters, "proc_report_CaseGroupMouth");
                return ds;
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetCaseGroupMouth(string where, string roleDwbmWhere, params object[] objValues)", "EDRS.OracleDAL.DataStatistics", "pkg_dzjz_report.proc_report_jznumber", parameters);
            }
            return null;
        }
        #endregion

        #region 卷宗业务情况

        public DataSet GetAllBusinessType()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT YWBM,YWMC FROM XT_DM_YWBM");
            try
            {
                DataSet ds = DbHelperOra.Query(strSql.ToString(), null);
                return ds;
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetAllBusinessType()", "EDRS.OracleDAL.EDRS_Report", strSql.ToString());
            }
            return null;
        }
        //卷宗业务情况查询
        public DataSet GetCaseBusiness(string where, string roleDwbmWhere, int pageSize, int pageIndex, string orderby, out int count, params object[] objValues)
        {
            OracleParameter[] parameters = {
					new OracleParameter("p_where", OracleType.VarChar,1000),
                    new OracleParameter("p_rwhere", OracleType.VarChar,1000),                    
                    new OracleParameter("p_pageindex", OracleType.Number),
                    new OracleParameter("p_pagesize", OracleType.Number),
                    new OracleParameter("p_count", OracleType.Number,8),
                    new OracleParameter("p_cursor",OracleType.Cursor),
                    new OracleParameter("p_errmsg", OracleType.VarChar,100)
                                           };

            parameters[0].Value = where;
            parameters[1].Value = roleDwbmWhere;
            parameters[2].Value = pageIndex;
            parameters[3].Value = pageSize;
            parameters[4].Direction = ParameterDirection.Output;
            parameters[5].Direction = ParameterDirection.Output;
            parameters[6].Direction = ParameterDirection.Output;

            try
            {
                DataSet ds = DbHelperOra.RunProcedure("pkg_dzjz_report.proc_report_casebusiness", parameters, "proc_report_casebusiness");
                count = Convert.ToInt32(parameters[4].Value);
                return ds;
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetCaseBusiness(string where, string roleDwbmWhere, int pageSize, int pageIndex, string orderby, out int count, params object[] objValues)", "EDRS.OracleDAL.DataStatistics", "pkg_dzjz_report.proc_report_jznumber", parameters);
            }
            count = 0;
            return null;
        }
        //卷宗业务情况汇总
        public DataSet GetCaseBusinessReport(string where, string havingWhere, string roleDwbmWhere, int pageSize, int pageIndex, string orderby, out int count, params object[] objValues)
        {
            count = 0;
            OracleParameter[] parameters = {
					new OracleParameter("p_where", OracleType.VarChar,1000),
					new OracleParameter("p_hwhere", OracleType.VarChar,1000),
                    new OracleParameter("p_rwhere", OracleType.VarChar,1000),            
                    new OracleParameter("p_cursor",OracleType.Cursor),
                    new OracleParameter("p_errmsg", OracleType.VarChar,100)
                                           };

            parameters[0].Value = where;
            parameters[1].Value = havingWhere;
            parameters[2].Value = roleDwbmWhere;
            parameters[3].Direction = ParameterDirection.Output;
            parameters[4].Direction = ParameterDirection.Output;

            try
            {
                DataSet ds = DbHelperOra.RunProcedure("pkg_dzjz_report.proc_report_casebusinessreport", parameters, "proc_report_casebusinessreport");
                return ds;
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetCaseGroupByUnit(string where, string roleDwbmWhere, int pageSize, int pageIndex, string orderby, out int count, params object[] objValues)", "EDRS.OracleDAL.DataStatistics", "pkg_dzjz_report.proc_report_jznumber", parameters);
            }
            count = 0;
            return null;
        }
        #endregion
    }
}
