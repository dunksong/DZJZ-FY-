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
	/// 数据访问类:XT_QX_JSGNFP
	/// </summary>
	public partial class XT_QX_JSGNFP
	{
        #region 根据单位编码和工号查询角色功能
        /// <summary>
        /// 根据单位编码和工号查询角色功能
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="objValues"></param>
        /// <returns></returns>
        public DataSet GetJSGNFPByGh(string strWhere, params object[] objValues)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(" select a.DWBM,a.JSBM,a.GNBM,a.BMBM from xt_qx_jsgnfp[0] a ", ConfigHelper.GetConfigString("OrcDBLinq"));
            strSql.AppendFormat(" join xt_qx_ryjsfp[0] b on a.dwbm = b.dwbm and a.bmbm=b.bmbm and a.jsbm=b.jsbm ", ConfigHelper.GetConfigString("OrcDBLinq"));
            strSql.AppendFormat(" where 1=1 {0}", strWhere);
            try
            {
                return DbHelperOra.Query(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetJSGNFPByGh(string strWhere, params object[] objValues)", "EDRS.OracleDAL.XT_QX_JSGNFP", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
        } 
        #endregion
	}
}

