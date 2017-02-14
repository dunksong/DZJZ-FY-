using System;
using System.Data;
using System.Text;
using System.Data.OracleClient;
using EDRS.IDAL;
using Maticsoft.DBUtility;
using System.Web;
using EDRS.Common;//Please add references
namespace EDRS.OracleDAL
{
	/// <summary>
	/// 数据访问类:XT_DZJZ_SSLB
	/// </summary>
	public partial class XT_DZJZ_SSLB
	{
        #region 获取树形结构当前节点所有父节点
        /// <summary>
        /// 获取树形结构当前节点所有父节点
        /// </summary>
        /// <param name="strWhere">数据查询条件</param>
        /// <param name="withWhere">循环开始条件</param>
        /// <param name="direction">查询方向（true父级向子级查询，false子级向父级）</param>
        /// <param name="objValues">参数</param>
        /// <returns>DataSet</returns>
        public DataSet GetTreeList(string strWhere, string withWhere, bool direction, params object[] objValues)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select distinct {1} ISLEAF,SSLBBM, FSSLBBM, SSLBLX, SSLBMC, SSLBSX, SSLBSM FROM XT_DZJZ_SSLB{0} ", ConfigHelper.GetConfigString("OrcDBLinq"), direction ? "CONNECT_BY_ISLEAF" : "(case when CONNECT_BY_ISLEAF=1 then '0' else '1' end)");
            strSql.AppendFormat(" where 1=1 {0}", strWhere);
            strSql.AppendFormat(" start with {0}", withWhere);
            strSql.AppendFormat(" connect by {0}", (direction ? " FSSLBBM= prior SSLBBM " : " prior FSSLBBM=SSLBBM "));

            strSql.Append(" order by SSLBSX");
            try
            {
                return DbHelperOra.Query(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetTreeList(string strWhere,string withWhere,bool direction, params object[] objValues)", "EDRS.OracleDAL.XT_DZJZ_SSLB", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
        }
        #endregion

        /// <summary>
        /// 获取最大顺序数
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="objValues"></param>
        /// <returns></returns>
        public object GetMaxSx(string strWhere, params object[] objValues)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select * from (select sslbsx from xt_dzjz_sslb where 1=1 {0} order by sslbsx desc ) a where rownum =1", strWhere);
            try
            {
                return DbHelperOra.GetSingle(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public object GetMaxSx(string strWhere, params object[] objValues)", "EDRS.OracleDAL.XT_DZJZ_SSLB", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;

        }
	}
}

