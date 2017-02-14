using System;
using System.Data;
using System.Text;
using System.Data.OracleClient;
using EDRS.IDAL;
using Maticsoft.DBUtility;
using EDRS.Common;

namespace EDRS.OracleDAL
{
	/// <summary>
	/// 数据访问类:XT_ZZJG_RYBM
	/// </summary>
	public partial class XT_ZZJG_RYBM
    {
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListAndDWBM(string strWhere, params object[] objValues)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select GH,XT_ZZJG_RYBM.DWBM,DWMC,MC,DLBM,KL,YDDHHM,DZYJ,GZZH,YDWBM,YDWMC,SFLSRY,SFTZ,XT_ZZJG_RYBM.SFSC,XB,CAID ");
            strSql.Append(" FROM XT_ZZJG_RYBM ");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
            strSql.Append(" join xt_zzjg_dwbm ");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
            strSql.Append(" on XT_ZZJG_RYBM.DWBM=xt_zzjg_dwbm.DWBM ");          
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetListAndDWBM(string strWhere, params object[] objValues)", "EDRS.OracleDAL.XT_ZZJG_RYBM", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
        }

        public bool ExistsDlbm(string dwbm, string gh, string dlbm)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from XT_ZZJG_RYBM");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
            strSql.Append(" where DWBM=:DWBM AND Trim(DLBM) = Trim(:DLBM)");
            OracleParameter[] parameters = null;
            if (string.IsNullOrEmpty(gh))
            {
                parameters = new OracleParameter[]{
					new OracleParameter(":DWBM", OracleType.VarChar,50),
					new OracleParameter(":DLBM", OracleType.VarChar,60)
                                           };
                parameters[0].Value = dwbm;
                parameters[1].Value = dlbm;
            }
            else
            {
                strSql.Append(" AND GH<>:GH");
                parameters = new OracleParameter[] {
					new OracleParameter(":DWBM", OracleType.Char,12),
					new OracleParameter(":DLBM", OracleType.VarChar,60),
					new OracleParameter(":GH", OracleType.Char,6)
                                           };
                parameters[0].Value = dwbm;
                parameters[1].Value = dlbm;
                parameters[2].Value = gh;
            }  
            
            try
            {
                return DbHelperOra.Exists(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool ExistsDlbm(string dwbm,string dlbm)", "EDRS.OracleDAL.XT_ZZJG_RYBM", strSql.ToString(), parameters);
            }
            return false;
        }


        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCountAndGn(string strWhere, params object[] objValues)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select count(1) from xt_zzjg_rybm{0} T right join xt_qx_rygnfp{0} b on T.dwbm=b.dwbm and T.gh=b.gh", ConfigHelper.GetConfigString("OrcDBLinq"));

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where 1=1 " + strWhere);
            }
            object obj = 0;
            try
            {
                obj = DbHelperOra.GetSingle(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public int GetRecordCount(string strWhere, params object[] objValues)", "EDRS.OracleDAL.XT_ZZJG_RYBM", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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

        /// <summary>
		/// 分页获取数据列表
		/// </summary>
        public DataSet GetListByPageAndGn(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.GH desc");
			}
            strSql.AppendFormat(")AS Ro, T.GH,T.DWBM,T.MC,T.DLBM,T.KL,T.YDDHHM,T.DZYJ,T.GZZH,T.YDWBM,T.YDWMC,T.SFLSRY,T.SFTZ,T.SFSC,T.XB,T.CAID,c.GNBM,c.GNMC  from XT_ZZJG_RYBM{0} T ", ConfigHelper.GetConfigString("OrcDBLinq"));
            strSql.AppendFormat(" right join xt_qx_rygnfp{0} b on T.dwbm=b.dwbm and T.gh=b.gh", ConfigHelper.GetConfigString("OrcDBLinq"));
            strSql.AppendFormat(" left join xt_qx_gndy{0} c on b.gnbm=c.gnbm", ConfigHelper.GetConfigString("OrcDBLinq"));
            
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)", "EDRS.OracleDAL.XT_ZZJG_RYBM", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;

		}

	}
}

