using System;
using System.Data;
using System.Text;
using System.Data.OracleClient;
using EDRS.IDAL;
using Maticsoft.DBUtility;//请先添加引用
namespace EDRS.OracleDAL
{
	/// <summary>
	/// 数据访问类YX_DZJZ_LSYJXZSQ。
	/// </summary>
	public partial class YX_DZJZ_LSYJXZSQ
	{
        /// <summary>
        /// 删除多条数据
        /// </summary>
        public bool DeleteListBySFSC(string SQBMlist)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update YX_DZJZ_LSYJXZSQ ");
            strSql.Append("set SFSC='Y' where SQBM in (" + SQBMlist + ")    ");
            if (DbHelperOra.ExecuteSql(strSql.ToString()) > 0)
                return true;
            return false;
        }
	}
}

