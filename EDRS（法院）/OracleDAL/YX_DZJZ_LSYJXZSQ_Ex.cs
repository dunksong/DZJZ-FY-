using System;
using System.Data;
using System.Text;
using System.Data.OracleClient;
using EDRS.IDAL;
using Maticsoft.DBUtility;//�����������
namespace EDRS.OracleDAL
{
	/// <summary>
	/// ���ݷ�����YX_DZJZ_LSYJXZSQ��
	/// </summary>
	public partial class YX_DZJZ_LSYJXZSQ
	{
        /// <summary>
        /// ɾ����������
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

