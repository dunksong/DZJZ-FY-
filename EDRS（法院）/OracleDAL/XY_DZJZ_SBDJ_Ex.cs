using System;
using System.Data;
using System.Text;
using System.Data.OracleClient;
using EDRS.IDAL;
using Maticsoft.DBUtility;
using EDRS.Common;//Please add references
namespace EDRS.OracleDAL
{
	/// <summary>
	/// 数据访问类:XY_DZJZ_SBDJ
	/// </summary>
	public partial class XY_DZJZ_SBDJ
    {
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string dwbm,string jsbm, string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)
        {

            string sql = "(select b.*,a.dwmc,a.fdwbm from xt_zzjg_dwbm a join xt_dm_qx c on  a.dwbm=c.qxbm and c.dwbm='" + dwbm + "' and jsbm in (" + jsbm + ") and qxlx=0 join XY_DZJZ_SBDJ b on c.qxbm =b.dwbm)";

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.DEVSN desc");
            }
            strSql.AppendFormat(")AS Ro, T.*  from {0} T ", sql);
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE 1=1 " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Ro between {0} and {1}", startIndex, endIndex);
            try
            {
                return DbHelperOra.Query(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetListByPage(string dwbm, string jsbm, string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)", "EDRS.OracleDAL.XY_DZJZ_SBDJ", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
        }



        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string dwbm, string jsbm, string strWhere, params object[] objValues)
        {

            string sql = "(select b.*,a.dwmc,a.fdwbm from xt_zzjg_dwbm a right join xt_dm_qx c on  a.dwbm=c.qxbm and c.dwbm='" + dwbm + "' and jsbm in (" + jsbm + ") and qxlx=0 right join XY_DZJZ_SBDJ b on c.qxbm =b.dwbm)";
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select count(1) FROM {0} ", sql);          
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where 1=1 " + strWhere);
            }
            object obj = null;
            try
            {
                obj = DbHelperOra.GetSingle(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public int GetRecordCount(string dwbm, string jsbm, string strWhere, params object[] objValues)", "EDRS.OracleDAL.XY_DZJZ_SBDJ", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

	}
}

