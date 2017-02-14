using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EDRS.Common;
using Maticsoft.DBUtility;
using EDRS.IDAL;
using System.Data.OracleClient;

namespace EDRS.OracleDAL
{
    /// <summary>
    /// 统计数据类
    /// </summary>
    public partial class DataStatistics : IDataStatistics
    {
        public System.Web.HttpRequest context = null;
        public void SetHttpContext(System.Web.HttpRequest _context)
        {
            this.context = _context;
        }       

        #region 卷宗数量统计
        /// <summary>
        /// 卷宗数量统计
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="dwbm">单位编码</param>
        /// <param name="gh">工号</param>
        /// <param name="systemmark">系统编号</param>
        /// <param name="configid">配置类型编号</param>
        /// <param name="orderby">排序</param>       
        /// <param name="count">返回总数</param>
        /// <param name="objValues">参数值</param>
        /// <returns></returns>
        public DataSet GetJZNumberByReport(string strWhere, string dwbm, string gh, string systemmark, int configid, out Decimal count, params object[] objValues)
        {
            OracleParameter[] parameters = {
                    new OracleParameter("p_dwbm", OracleType.VarChar,50),       
                    new OracleParameter("p_gh", OracleType.Char,4),      
                    new OracleParameter("p_systemmark", OracleType.VarChar,50),
                    new OracleParameter("p_configid", OracleType.Number),
					new OracleParameter("p_where", OracleType.VarChar,4000),  
                    new OracleParameter("p_count", OracleType.Number,8),
                    new OracleParameter("p_cursor",OracleType.Cursor),
                    new OracleParameter("p_errmsg", OracleType.VarChar,500)
                                           };
            parameters[0].Value = dwbm;
            parameters[1].Value = gh;
            parameters[2].Value = systemmark;
            parameters[3].Value = configid;
            parameters[4].Value = strWhere;            
            parameters[5].Direction = ParameterDirection.Output;
            parameters[6].Direction = ParameterDirection.Output;
            parameters[7].Direction = ParameterDirection.Output;

            try
            {
                DataSet ds = DbHelperOra.RunProcedure("pkg_dzjz_report.proc_report_jznumber", parameters, "proc_report_jznumber");
                count = Convert.ToDecimal(parameters[5].Value);
                return ds;
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetJZNumberStatistics(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)", "EDRS.OracleDAL.DataStatistics", "pkg_dzjz_report.proc_report_jznumber", parameters);
            }
            count = 0;
            return null;

        } 
        #endregion

        #region 单位卷宗制作情况统计
        /// <summary>
        /// 单位卷宗制作情况统计
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="dwbm">单位编码</param>
        /// <param name="gh">工号</param>
        /// <param name="orderby">排序</param>
        /// <param name="startIndex">分页数</param>
        /// <param name="endIndex">每页显示数</param>
        /// <param name="count">返回总数</param>
        /// <param name="objValues">参数值</param>
        /// <returns></returns>
        public DataSet GetDwJzZzStatistics(string strWhere, string strWhereDw, string dwbm, string gh,string jsbm,string bmbm, string orderby, int startIndex, int endIndex, out int count, params object[] objValues)
        {
            OracleParameter[] parameters = {
                    new OracleParameter("p_dwbm", OracleType.VarChar,50),       
                    new OracleParameter("p_gh", OracleType.Char,4),       
                    new OracleParameter("p_jsbm", OracleType.VarChar,50),       
                    new OracleParameter("p_bmbm", OracleType.VarChar,50),       
					new OracleParameter("p_where", OracleType.VarChar,int.MaxValue),   
                    new OracleParameter("p_where_dw", OracleType.VarChar),   
                    new OracleParameter("p_order", OracleType.VarChar,100),                  
                    new OracleParameter("p_pageindex", OracleType.Number),
                    new OracleParameter("p_pagesize", OracleType.Number),
                    new OracleParameter("p_count", OracleType.Number,8),
                    new OracleParameter("p_cursor",OracleType.Cursor),
                    new OracleParameter("p_errmsg", OracleType.VarChar,4000)
                                           };
            parameters[0].Value = dwbm;
            parameters[1].Value = gh;
            parameters[2].Value = jsbm;
            parameters[3].Value = bmbm;
            parameters[4].Value = strWhere;
            parameters[5].Value = strWhereDw;
            parameters[6].Value = orderby;
            parameters[7].Value = startIndex;
            parameters[8].Value = endIndex;
            parameters[9].Direction = ParameterDirection.Output;
            parameters[10].Direction = ParameterDirection.Output;
            parameters[11].Direction = ParameterDirection.Output;

            try
            {
                DataSet ds = DbHelperOra.RunProcedure("pkg_dzjz_report.proc_report_dwjzzztj", parameters, "proc_report_dwjzzztj");
                if (parameters[9].Value != DBNull.Value)
                    count = Convert.ToInt32(parameters[9].Value);
                else
                    count = 0;
                return ds;
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetDwJzZzStatistics(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)", "EDRS.OracleDAL.DataStatistics", "pkg_dzjz_report.proc_report_dwjzzztj", parameters);
            }
            count = 0;
            return null;
        }
        #endregion

        #region 单位卷宗制作情况业务统计
        /// <summary>
        /// 单位卷宗制作情况业务统计
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="dwbm">单位编码</param>
        /// <param name="gh">工号</param>       
        /// <param name="objValues">参数值</param>
        /// <returns></returns>
        public DataSet GetDwJzZzStatisticsByYw(string strWhere, string strWhereDw, string dwbm, string gh, string jsbm, string bmbm, string orderby, params object[] objValues)
        {
            OracleParameter[] parameters = {
                    new OracleParameter("p_dwbm", OracleType.VarChar,50),       
                    new OracleParameter("p_gh", OracleType.Char,4),       
                    new OracleParameter("p_jsbm", OracleType.VarChar,50),       
                    new OracleParameter("p_bmbm", OracleType.VarChar,50),       
					new OracleParameter("p_where", OracleType.VarChar,4000),  
					new OracleParameter("p_where_dw", OracleType.VarChar,4000),  
                    new OracleParameter("p_order", OracleType.VarChar,100),                  
                    new OracleParameter("p_cursor",OracleType.Cursor),
                    new OracleParameter("p_errmsg", OracleType.VarChar,500)
                                           };
            parameters[0].Value = dwbm;
            parameters[1].Value = gh;
            parameters[2].Value = jsbm;
            parameters[3].Value = bmbm;
            parameters[4].Value = strWhere;
            parameters[5].Value = strWhereDw;
            parameters[6].Value = orderby;
            parameters[7].Direction = ParameterDirection.Output;
            parameters[8].Direction = ParameterDirection.Output;

            try
            {
                DataSet ds = DbHelperOra.RunProcedure("pkg_dzjz_report.proc_report_dwjzzzyw", parameters, "proc_report_dwjzzzyw");              
                return ds;
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetDwJzZzStatisticsByYw(string strWhere, string dwbm, string gh, params object[] objValues)", "EDRS.OracleDAL.DataStatistics", "pkg_dzjz_report.proc_report_dwjzzzyw", parameters);
            }
            return null;
        }
        #endregion

        #region 单位卷宗制作情况案件类别统计
        /// <summary>
        /// 单位卷宗制作情况案件类别统计
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="dwbm">单位编码</param>
        /// <param name="gh">工号</param>       
        /// <param name="objValues">参数值</param>
        /// <returns></returns>
        public DataSet GetDwJzZzStatisticsByLb(string strWhere,string strWhereDw, string dwbm, string gh,string jsbm,string bmbm,string orderby, params object[] objValues)
        {
            OracleParameter[] parameters = {
                    new OracleParameter("p_dwbm", OracleType.VarChar,50),       
                    new OracleParameter("p_gh", OracleType.Char,4),       
                    new OracleParameter("p_jsbm", OracleType.VarChar,50),       
                    new OracleParameter("p_bmbm", OracleType.VarChar,50),       
					new OracleParameter("p_where", OracleType.VarChar,4000),  
					new OracleParameter("p_where_dw", OracleType.VarChar,4000),  
                    new OracleParameter("p_order", OracleType.VarChar,100),                  
                    new OracleParameter("p_cursor",OracleType.Cursor),
                    new OracleParameter("p_errmsg", OracleType.VarChar,500)
                                           };
            parameters[0].Value = dwbm;
            parameters[1].Value = gh;
            parameters[2].Value = jsbm;
            parameters[3].Value = bmbm;
            parameters[4].Value = strWhere;
            parameters[5].Value = strWhereDw;
            parameters[6].Value = orderby;
            parameters[7].Direction = ParameterDirection.Output;
            parameters[8].Direction = ParameterDirection.Output;

            try
            {
                DataSet ds = DbHelperOra.RunProcedure("pkg_dzjz_report.proc_report_dwjzzzlb", parameters, "proc_report_dwjzzzlb");
                return ds;
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetDwJzZzStatisticsByLb(string strWhere, string dwbm, string gh, params object[] objValues)", "EDRS.OracleDAL.DataStatistics", "pkg_dzjz_report.proc_report_dwjzzzlb", parameters);
            }
            return null;
        }
        #endregion

        #region 单位卷宗制作查询
        /// <summary>
        /// 单位卷宗制作查询
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="dwbm">单位编码</param>
        /// <param name="gh">工号</param>
        /// <param name="orderby">排序</param>
        /// <param name="startIndex">分页数</param>
        /// <param name="endIndex">每页显示数</param>
        /// <param name="count">返回总数</param>
        /// <param name="objValues">参数值</param>
        /// <returns></returns>
        public DataSet GetDwJzZzQuery(string strWhere, string dwbm, string gh, string orderby, int startIndex, int endIndex, out int count, params object[] objValues)
        {
            OracleParameter[] parameters = {
                    new OracleParameter("p_dwbm", OracleType.VarChar,50),       
                    new OracleParameter("p_gh", OracleType.Char,4),       
					new OracleParameter("p_where", OracleType.VarChar,4000),   
                    new OracleParameter("p_order", OracleType.VarChar,100),                  
                    new OracleParameter("p_pageindex", OracleType.Number),
                    new OracleParameter("p_pagesize", OracleType.Number),
                    new OracleParameter("p_count", OracleType.Number,8),
                    new OracleParameter("p_cursor",OracleType.Cursor),
                    new OracleParameter("p_errmsg", OracleType.VarChar,500)
                                           };
            parameters[0].Value = dwbm;
            parameters[1].Value = gh;
            parameters[2].Value = strWhere;
            parameters[3].Value = orderby;
            parameters[4].Value = startIndex;
            parameters[5].Value = endIndex;
            parameters[6].Direction = ParameterDirection.Output;
            parameters[7].Direction = ParameterDirection.Output;
            parameters[8].Direction = ParameterDirection.Output;

            try
            {
                DataSet ds = DbHelperOra.RunProcedure("pkg_dzjz_report.proc_report_dwjzzzcx", parameters, "proc_report_dwjzzzcx");
                count = Convert.ToInt32(parameters[6].Value);
                //OracleParameter op = parameters.FirstOrDefault(d => d.ParameterName == "p_count1");//.Value.ToString();
                //if (op != null)
                //    count = Convert.ToInt32(op.Value);
                return ds;
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetDwJzZzQuery(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)", "EDRS.OracleDAL.DataStatistics", "pkg_dzjz_report.proc_report_dwjzzzcx", parameters);
            }
            count = 0;
            return null;
        } 
        #endregion

        #region 单位案件文件大小统计
        /// <summary>
        /// 单位案件文件大小统计
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="dwbm">单位编码</param>
        /// <param name="gh">工号</param>
        /// <param name="orderby">排序</param>
        /// <param name="startIndex">分页数</param>
        /// <param name="endIndex">每页显示数</param>
        /// <param name="count">返回总数</param>
        /// <param name="objValues">参数值</param>
        /// <returns></returns>
        public DataSet GetDwAjWjSum(string strWhere, string dwbm, string gh, string orderby, int startIndex, int endIndex, out int count, params object[] objValues)
        {
            OracleParameter[] parameters = {
                    new OracleParameter("p_dwbm", OracleType.VarChar,50),       
                    new OracleParameter("p_gh", OracleType.Char,4),       
					new OracleParameter("p_where", OracleType.VarChar,4000),   
                    new OracleParameter("p_order", OracleType.VarChar,100),                  
                    new OracleParameter("p_pageindex", OracleType.Number),
                    new OracleParameter("p_pagesize", OracleType.Number),
                    new OracleParameter("p_count", OracleType.Number,8),
                    new OracleParameter("p_cursor",OracleType.Cursor),
                    new OracleParameter("p_errmsg", OracleType.VarChar,500)
                                           };
            parameters[0].Value = dwbm;
            parameters[1].Value = gh;
            parameters[2].Value = strWhere;
            parameters[3].Value = orderby;
            parameters[4].Value = startIndex;
            parameters[5].Value = endIndex;
            parameters[6].Direction = ParameterDirection.Output;
            parameters[7].Direction = ParameterDirection.Output;
            parameters[8].Direction = ParameterDirection.Output;

            try
            {
                DataSet ds = DbHelperOra.RunProcedure("pkg_dzjz_report.proc_report_dwjznumber", parameters, "proc_report_dwjznumber");
                count = Convert.ToInt32(parameters[6].Value);
                return ds;
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetDwAjWjSum(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)", "EDRS.OracleDAL.DataStatistics", "pkg_dzjz_report.proc_report_dwjznumber", parameters);
            }
            count = 0;
            return null;
        }
        #endregion


        #region 业绩统计
        /// <summary>
        /// 业绩统计
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="orderby"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <param name="count"></param>
        /// <param name="objValues"></param>
        /// <returns></returns>
        public DataTable GetYJTJ(string strWhere,string strWhereAj,string strWhereRy, string orderby, int startIndex, int endIndex, out int count, params object[] objValues)
        {
            OracleParameter[] parameters = {                  
					new OracleParameter("p_where", OracleType.VarChar,4000),   
					new OracleParameter("p_where_aj", OracleType.VarChar,4000),   
					new OracleParameter("p_where_ry", OracleType.VarChar,4000),   
                    new OracleParameter("p_order", OracleType.VarChar,100),                  
                    new OracleParameter("p_pageindex", OracleType.Number),
                    new OracleParameter("p_pagesize", OracleType.Number),
                    new OracleParameter("p_count", OracleType.Number,8),
                    new OracleParameter("p_cursor",OracleType.Cursor),
                    new OracleParameter("p_errmsg", OracleType.VarChar,500)
                                           };
            parameters[0].Value = strWhere;
            parameters[1].Value = strWhereAj;
            parameters[2].Value = strWhereRy;
            parameters[3].Value = orderby;
            parameters[4].Value = startIndex;
            parameters[5].Value = endIndex;
            parameters[6].Direction = ParameterDirection.Output;
            parameters[7].Direction = ParameterDirection.Output;
            parameters[8].Direction = ParameterDirection.Output;

            try
            {
                DataSet ds = DbHelperOra.RunProcedure("pkg_dzjz_report.proc_report_getzzl", parameters, "proc_report_getzzl");
                count = Convert.ToInt32(parameters[6].Value);
                //OracleParameter op = parameters.FirstOrDefault(d => d.ParameterName == "p_count1");//.Value.ToString();
                //if (op != null)
                //    count = Convert.ToInt32(op.Value);
                if(ds != null && ds.Tables.Count> 0)
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetYJTJ(string strWhere,string strWhereAj, string orderby, int startIndex, int endIndex, out int count, params object[] objValues)", "EDRS.OracleDAL.DataStatistics", "pkg_dzjz_report.proc_report_getzzl", parameters);
            }
            count = 0;
            return null;


            //count = 0;
            //string tableName = string.Format("(select DWBM,DWMC,CZR,count(AJBH) AJCOUNT,sum(JNUM) JCOUNT,sum(MLNUM) MLCOUNT,sum(WJYNUM) WJYCOUNT from YX_DZJZ_JZZZTJ where 1=1 {0} group by DWBM,DWMC,CZR )", strWhere);

            //StringBuilder strSql = new StringBuilder();

            //strSql.Append(" SELECT * FROM ( ");
            //strSql.Append(" SELECT ROW_NUMBER() OVER (");
            //if (!string.IsNullOrEmpty(orderby.Trim()))
            //{
            //    strSql.Append("order by T." + orderby);
            //}
            //else
            //{
            //    strSql.Append("order by T.BMSAH desc");
            //}
            //strSql.AppendFormat(")AS Ro, T.*  from {0} T ", tableName);
            ////if (!string.IsNullOrEmpty(strWhere.Trim()))
            ////{
            ////    strSql.Append(" WHERE 1=1 " + strWhere);
            ////}
            //strSql.Append(" ) TT");
            //strSql.AppendFormat(" WHERE TT.Ro between {0} and {1} ", startIndex, endIndex);

            //try
            //{
            //    DataSet ds = DbHelperOra.Query(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            //    if (ds != null && ds.Tables.Count > 0)
            //    {
            //        count = GetYJTJCount(strWhere, objValues);
            //        return ds.Tables[0];
            //    }

            //}

            //catch (Exception ex)
            //{
            //    EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)", "EDRS.OracleDAL.TYYW_GG_AJJBXX", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            //}
            //return null;
        }
        #endregion

        #region 业绩统计行数
        /// <summary>
        /// 业绩统计行数
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="objValues"></param>
        /// <returns></returns>
        public int GetYJTJCount(string strWhere, params object[] objValues)
        {
            string tableName = string.Format("(select DWBM,DWMC,CZR,count(AJBH) AJCOUNT,sum(JNUM) JCOUNT,sum(MLNUM) MLCOUNT,sum(WJYNUM) WJYCOUNT from YX_DZJZ_JZZZTJ where 1=1 {0} group by DWBM,DWMC,CZR )", strWhere);
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select count(1) from {0} T ", tableName);

            object ds = DbHelperOra.GetSingle(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            if (ds != null)
                return Convert.ToInt32(ds);
            return 0;
        }
        #endregion
    }
}
