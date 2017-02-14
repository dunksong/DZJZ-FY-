using System;
using System.Data;
using System.Text;
using System.Data.OracleClient;
using EDRS.IDAL;
using Maticsoft.DBUtility;
using EDRS.Common;
using System.Collections.Generic;
using System.Collections;//Please add references
namespace EDRS.OracleDAL
{
	/// <summary>
	/// 数据访问类:XT_QX_RYGNFP
	/// </summary>
	public partial class XT_QX_RYGNFP:IXT_QX_RYGNFP
    {
		
		/// <summary>
		/// 增加多条数据
		/// </summary>
		public bool AddList(List<EDRS.Model.XT_QX_RYGNFP> modelList)
		{
            int count = 0;
            Hashtable hash = new Hashtable();
            foreach (EDRS.Model.XT_QX_RYGNFP model in modelList)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into XT_QX_RYGNFP(");
                strSql.Append("DWBM,GH,GNBM,BMBM,GNCS,BZ)");
                strSql.Append(" values (");
                strSql.Append(":DWBM" + count + ",:GH" + count + ",:GNBM" + count + ",:BMBM" + count + ",:GNCS" + count + ",:BZ" + count + ")");
                OracleParameter[] parameters = {
					new OracleParameter(":DWBM" + count, OracleType.VarChar,50),
					new OracleParameter(":GH" + count, OracleType.Char,4),
					new OracleParameter(":GNBM" + count, OracleType.VarChar,50),
					new OracleParameter(":BMBM" + count, OracleType.Char,4),
					new OracleParameter(":GNCS" + count, OracleType.Clob,4000),
					new OracleParameter(":BZ" + count, OracleType.VarChar,900)};
                parameters[0].Value = model.DWBM;
                parameters[1].Value = model.GH;
                parameters[2].Value = model.GNBM;
                parameters[3].Value = model.BMBM;
                parameters[4].Value = string.IsNullOrWhiteSpace(model.GNCS) ? " " : model.GNCS;
                parameters[5].Value = model.BZ;
                hash.Add(strSql.ToString(), parameters);
                count++;
            }

            try
            {
                return DbHelperOra.ExecuteSqlTran(hash);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Add(EDRS.Model.XT_QX_RYGNFP model)", "EDRS.OracleDAL.XT_QX_RYGNFP",hash);
            }
            return false;
		}

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string listId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from XT_QX_RYGNFP ");
            strSql.Append(" where DWBM||GH||GNBM in (" + listId + ")");
         
            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString());
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Delete(string DWBM,string GH,string GNBM)", "EDRS.OracleDAL.XT_QX_RYGNFP", strSql.ToString());
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
	}
}

