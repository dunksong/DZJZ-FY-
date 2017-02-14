using System;
using System.Data;
using System.Text;
using System.Data.OracleClient;
using EDRS.IDAL;
using Maticsoft.DBUtility;
using EDRS.Common;
using System.Collections.Generic;//Please add references
namespace EDRS.OracleDAL
{
	/// <summary>
	/// 数据访问类:XY_DZJZ_XTPZ
	/// </summary>
	public partial class XY_DZJZ_XTPZ
    {
        /// <summary>
        /// 根据枚举值获取参数对象
        /// </summary>
        public EDRS.Model.XY_DZJZ_XTPZ GetModel(int configID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select PZBM,SYSTEMMARK,CONFIGID,CONFIGNAME,CONFIGVALUE from XY_DZJZ_XTPZ ");
            strSql.Append(" where CONFIGID=:CONFIGID ");
            OracleParameter[] parameters = {
					new OracleParameter(":CONFIGID", OracleType.Number)};
            parameters[0].Value = configID;

            EDRS.Model.XY_DZJZ_XTPZ model = new EDRS.Model.XY_DZJZ_XTPZ();
            DataSet ds = DbHelperOra.Query(strSql.ToString(), parameters);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }
	}
}

