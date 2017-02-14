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
    /// 数据访问类:XT_DM_AJLBBM
	/// </summary>
	public partial class XT_DM_AJLBBM
    {
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetSSLBList(string strWhere, params object[] objValues)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SSLBBM,SSLBMC,FSSLBBM,SSLBSX ");
            strSql.Append(" FROM XT_DZJZ_SSLB ");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where 1=1 " + strWhere);
            }
            strSql.Append(" ORDER BY SSLBSX");
            try
            {
                return DbHelperOra.Query(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetSSLBList(string strWhere, params object[] objValues)", "EDRS.OracleDAL.TYYW_GG_AJJBXX_Ex", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(EDRS.Model.XT_DM_AJLBBM model,string ajlbbm)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update XT_DM_AJLBBM set ");
            strSql.Append(" YWBM=:YWBM,AJLBBM=:AJLBBM,AJLBMC=:AJLBMC,AJSLJC=:AJSLJC,SFSC=:SFSC,XH=:XH ");
            strSql.Append(" where AJLBBM=:AJLBBM2 ");
            OracleParameter[] parameters = {
					new OracleParameter(":YWBM", OracleType.Char,2),
					new OracleParameter(":AJLBBM", OracleType.VarChar,50),
					new OracleParameter(":AJLBMC", OracleType.VarChar,300),
					new OracleParameter(":AJSLJC", OracleType.VarChar,60),
					new OracleParameter(":SFSC", OracleType.Char,1),
					new OracleParameter(":XH", OracleType.Number),
					new OracleParameter(":AJLBBM2", OracleType.VarChar,50)};
            parameters[0].Value = model.YWBM;
            parameters[1].Value = model.AJLBBM;
            parameters[2].Value = model.AJLBMC;
            parameters[3].Value = model.AJSLJC;
            parameters[4].Value = model.SFSC;
            parameters[5].Value = model.XH;
            parameters[6].Value = ajlbbm;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Update(EDRS.Model.XT_DM_AJLBBM model)", "EDRS.OracleDAL.XT_DM_AJLBBM", strSql.ToString(), parameters);
            }
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除多条数据
        /// </summary>
        public bool DeleteList(string ajlbbmList)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update XT_DM_AJLBBM set SFSC='Y'");
            strSql.Append(" where AJLBBM in (" + ajlbbmList + ")    ");
            if (DbHelperOra.ExecuteSql(strSql.ToString()) > 0)
                return true;
            return false;
        }
	}
}

