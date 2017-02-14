using System;
using System.Data;
using System.Text;
using System.Data.OracleClient;
using EDRS.IDAL;
using Maticsoft.DBUtility;
using EDRS.Common;
using System.Collections.Generic;
using System.Collections;//Please add references
namespace EDRS.OracleDAL
{
	/// <summary>
	/// 数据访问类:YX_DZJZ_JZRZJL
	/// </summary>
	public partial class YX_DZJZ_JZRZJL
    {
      
		#region  BasicMethod

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool AddByModelList(List<EDRS.Model.YX_DZJZ_JZRZJL> modelList)
		{
            int xh = 0;
            //获取序号
            try
            {
                xh = DbHelperOra.GetMaxID("XH", "YX_DZJZ_JZRZJL");
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Add(EDRS.Model.YX_DZJZ_JZRZJL model)|DbHelperOra.GetMaxID(\"XH\", \"YX_DZJZ_JZRZJL\")", "EDRS.OracleDAL.YX_DZJZ_JZRZJL");
                return false;
            }

            
            int count = 0;
            Hashtable hash = new Hashtable();
            foreach (EDRS.Model.YX_DZJZ_JZRZJL model in modelList)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into YX_DZJZ_JZRZJL(");
                strSql.Append("XH,DWBM,DWMC,BMBM,BMMC,CZRGH,CZR,CZSJ,CZIP,CZLX,RZNR,CZAJBMSAH,FQL)");
                strSql.Append(" values (");
                strSql.Append(":XH" + count + ",:DWBM" + count + ",:DWMC" + count + ",:BMBM" + count + ",:BMMC" + count + ",:CZRGH" + count + ",:CZR" + count + ",:CZSJ" + count + ",:CZIP" + count + ",:CZLX" + count + ",:RZNR" + count + ",:CZAJBMSAH" + count + ",:FQL" + count + ")");
                OracleParameter[] parameters = {
					new OracleParameter(":XH" + count, OracleType.Number,4),
					new OracleParameter(":DWBM" + count, OracleType.VarChar,50),
					new OracleParameter(":DWMC" + count, OracleType.VarChar,300),
					new OracleParameter(":BMBM" + count, OracleType.Char,10),
					new OracleParameter(":BMMC" + count, OracleType.VarChar,300),
					new OracleParameter(":CZRGH" + count, OracleType.Char,4),
					new OracleParameter(":CZR" + count, OracleType.VarChar,60),
					new OracleParameter(":CZSJ" + count, OracleType.DateTime),
					new OracleParameter(":CZIP" + count, OracleType.VarChar,20),
					new OracleParameter(":CZLX" + count, OracleType.Char,2),
					new OracleParameter(":RZNR" + count, OracleType.VarChar,300),
					new OracleParameter(":CZAJBMSAH" + count, OracleType.VarChar,100),
					new OracleParameter(":FQL" + count, OracleType.Char,4)};
                parameters[0].Value = xh++;
                parameters[1].Value = model.DWBM;
                parameters[2].Value = model.DWMC;
                parameters[3].Value = model.BMBM;
                parameters[4].Value = model.BMMC;
                parameters[5].Value = model.CZRGH;
                parameters[6].Value = model.CZR;
                parameters[7].Value = model.CZSJ;
                parameters[8].Value = model.CZIP;
                parameters[9].Value = model.CZLX;
                parameters[10].Value = model.RZNR;
                parameters[11].Value = model.CZAJBMSAH;
                parameters[12].Value = DateTime.Now.Year;

                hash.Add(strSql.ToString(), parameters);
                count++; 
            }
           
            try
            {
                return DbHelperOra.ExecuteSqlTran(hash);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Add(EDRS.Model.YX_DZJZ_JZRZJL model)", "EDRS.OracleDAL.YX_DZJZ_JZRZJL",hash);
            }
            return false;
		}

		#endregion  BasicMethod

        #region 动态创建日志表
        /// <summary>
        /// 动态创建日志表
        /// </summary>
        /// <returns></returns>
        private string LogTable()
        {
            DataSet ds = DbHelperOra.Query("select object_name,created from(select object_name,created from user_objects where object_name like 'YX_DZJZ_JZRZJL%' order by created desc ) where rownum = 1");
           
                int logTime = ConfigHelper.GetConfigInt("LogTime");
                if (logTime <= 0)
                    return null;
                if (ds == null || DateTime.Now.Subtract(Convert.ToDateTime(ds.Tables[0].Rows[0]["created"])).Days >= DateTime.Now.Subtract(DateTime.Now.AddMonths(-logTime)).Days)
                //一分钟创建一张表
                //if (ds == null || DateTime.Now.Subtract(Convert.ToDateTime(ds)).Minutes > 1)
                {
                    string tableName = "YX_DZJZ_JZRZJL_" + DateTime.Now.ToString("yyyyMMdd");
                    StringBuilder str = new StringBuilder();
                    str.Append(" CREATE TABLE " + tableName);
                    str.Append(" (");
                    str.Append(" XH NVARCHAR2(100) default sys_guid() NOT NULL PRIMARY KEY,");
                    str.Append(" DWBM VARCHAR2(50),");
                    str.Append(" DWMC VARCHAR2(300),");
                    str.Append(" BMBM CHAR(10),");
                    str.Append(" BMMC VARCHAR2(300),");
                    str.Append(" CZRGH CHAR(4),");
                    str.Append(" CZR VARCHAR2(60),");
                    str.Append(" CZSJ DATE,");
                    str.Append(" CZIP VARCHAR2(20),");
                    str.Append(" CZLX CHAR(2),");
                    str.Append(" RZNR VARCHAR2(300),");
                    str.Append(" CZAJBMSAH VARCHAR2(100),");
                    str.Append(" FQL CHAR(4) NOT NULL");
                    str.Append(" )");
                    try
                    {
                        DbHelperOra.GetSingle(str.ToString());
                        return tableName;
                    }
                    catch (Exception ex)
                    {
                        EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "动态创建日志表 private string LogTable()", "EDRS.OracleDAL.YX_DZJZ_JZRZJL", str.ToString());
                    }
                }
                else
                {
                    return ds.Tables[0].Rows[0]["object_name"].ToString();
                }
            
            return null;
        } 
        #endregion

        #region 清理日志记录表
        /// <summary>
        /// 清理日志记录表
        /// </summary>
        private void LogClearTable()
        {
            int time = ConfigHelper.GetConfigInt("LogClearTime");
            if (time <= 0)
                return;
            DateTime date = DateTime.Now.AddMonths(-time);
            OracleParameter[] parameters = {
                    new OracleParameter("p_time",OracleType.DateTime),                    
                    new OracleParameter("p_errmsg",OracleType.VarChar,512)
                };
            parameters[0].Value = date;
            parameters[1].Direction = ParameterDirection.Output;
            try
            {
                OracleDataReader dr = DbHelperOra.RunProcedure("pkg_zzjg_manage.proc_delete_jzrzjl", parameters);
                dr.Close();
                if (parameters[1].Value != null && !string.IsNullOrEmpty(parameters[1].Value.ToString()))
                    EDRS.Common.LogHelper.LogError(this.context, "Exception", parameters[1].Value.ToString(), "private void LogClearTable()", "EDRS.OracleDAL.YX_DZJZ_JZRZJL", "PKG_ZZJG_MANAGE.proc_delete_jzrzjl", parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "private void LogClearTable()", "EDRS.OracleDAL.YX_DZJZ_JZRZJL", "PKG_ZZJG_MANAGE.proc_delete_jzrzjl", parameters);
            }
        } 
        #endregion

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPageProc(DateTime startTime, DateTime endTime, string strWhere, string orderby, int startIndex, int endIndex, ref int count)
        {
            OracleParameter[] parameters = {
                    //new OracleParameter("p_starttime",OracleType.DateTime),
                    //new OracleParameter("p_endtime",OracleType.DateTime),
                    new OracleParameter("p_where",OracleType.VarChar,int.MaxValue),            
                    new OracleParameter("p_order",OracleType.VarChar,500),
                    new OracleParameter("p_pagesize",OracleType.Number),
                    new OracleParameter("p_pageindex",OracleType.Number),
                    new OracleParameter("p_count",OracleType.Number,8),
                    new OracleParameter("p_cursor",OracleType.Cursor),
                    new OracleParameter("p_errmsg",OracleType.VarChar,512)
                };
            //parameters[0].Value = startTime;
            //parameters[1].Value = endTime;
            parameters[0].Value = strWhere;         
            parameters[1].Value = orderby;
            parameters[2].Value = endIndex;
            parameters[3].Value = startIndex;
            parameters[4].Direction = ParameterDirection.Output;
            parameters[5].Direction = ParameterDirection.Output;
            parameters[6].Direction = ParameterDirection.Output;
            try
            {
                DataSet ds = DbHelperOra.RunProcedure("pkg_zzjg_manage.proc_get_jzrzjl", parameters, "proc_get_jzrzjl");
                if (ds != null && ds.Tables.Count > 0)
                {
                    count = Convert.ToInt32(parameters[4].Value);
                    return ds;
                }
                if (parameters[6].Value != null)
                    EDRS.Common.LogHelper.LogError(this.context, "Exception", parameters[6].Value.ToString(), "public DataSet GetListByPageProc(string strWhere, string orderby, int startIndex, int endIndex, ref int count)", "EDRS.OracleDAL.YX_DZJZ_JZRZJL", "PKG_ZZJG_MANAGE.PROC_GET_JZRZJL", parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetListByPageProc(string strWhere, string orderby, int startIndex, int endIndex, ref int count)", "EDRS.OracleDAL.YX_DZJZ_JZRZJL", "PKG_ZZJG_MANAGE.PROC_GET_JZRZJL", parameters);
            }
            return null;
        }
	}
}

