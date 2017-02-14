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
	/// 数据访问类:XT_QX_JSBM
	/// </summary>
	public partial class XT_QX_JSBM
	{
		
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
        public DataSet GetListByPageAlly(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)
		{
            string tableName = "(select j.*,d.dwmc,b.bmmc from XT_QX_JSBM j left join XT_ZZJG_DWBM d on j.dwbm = d.dwbm left join XT_ZZJG_BMBM b on j.bmbm = b.bmbm)";

			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.SPJSBM desc");
			}
            strSql.AppendFormat(")AS Ro, T.*  from {0} T ", tableName + ConfigHelper.GetConfigString("OrcDBLinq"));
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, " public DataSet GetListByPageAlly(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)", "EDRS.OracleDAL.XT_QX_JSBM", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
		}

        /// <summary>
        /// 获得数据列表排序
        /// </summary>
        public DataSet GetListOrBCount(string strWhere, string order, params object[] objValues)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from (select JSBM,DWBM,BMBM,JSMC,JSXH,SPJSBM ");
            strSql.Append(" FROM XT_QX_JSBM");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
            if (string.IsNullOrEmpty(order))
                order = "JSBM asc";
            strSql.AppendFormat(" order by {0})", order);
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where 1=1 " + strWhere);
            }
            try
            {
                return DbHelperOra.Query(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetListOrBCount(string strWhere, string order, params object[] objValues)", "EDRS.OracleDAL.XT_QX_JSBM", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;

        }

        #region 根据单位编码和工号获取角色
        /// <summary>
        /// 根据单位编码和工号获取角色
        /// </summary>
        /// <param name="dwbm"></param>
        /// <param name="gh"></param>
        /// <returns></returns>
        public DataSet GetJSBMbyUser(string dwbm, string gh)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select b.JSBM,b.DWBM,b.BMBM,bm.BMMC,b.JSMC,b.JSXH,b.SPJSBM,b.QXZT from xt_qx_ryjsfp a ");
            strSql.Append(" join XT_QX_JSBM b on a.jsbm = b.jsbm and a.dwbm=b.dwbm and a.bmbm= b.bmbm ");
            strSql.Append(" left join XT_ZZJG_BMBM bm on b.BMBM = bm.BMBM and bm.dwbm=a.dwbm");
            strSql.Append(" where a.dwbm=:dwbm and a.gh=:gh");
            OracleParameter[] parameters = {
					new OracleParameter(":DWBM", OracleType.VarChar,50),
					new OracleParameter(":gh", OracleType.Char,4)};
            parameters[0].Value = dwbm;
            parameters[1].Value = gh;
            try
            {
                return DbHelperOra.Query(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetJSBMbyUser(string dwbm,string gh) ", "EDRS.OracleDAL.XT_QX_JSBM", strSql.ToString(), parameters);
            }
            return null;
        } 
        #endregion
	}
}

