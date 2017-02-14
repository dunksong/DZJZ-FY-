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
    /// 数据访问类:XT_DM_YWBM
	/// </summary>
	public partial class XT_DM_YWBM
	{
        /// <summary>
        /// 删除多条数据
        /// </summary>
        public bool DeleteList(string ywbmList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update XT_DM_YWBM set SFSC='Y'");
            strSql.Append(" where YWBM in (" + ywbmList + ")    ");
            if (DbHelperOra.ExecuteSql(strSql.ToString()) > 0)
                return true;
            return false;
        }
	}
}

