
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
	/// 数据访问类:XT_DZJZ_JZMB
	/// </summary>
	public partial class XT_DZJZ_JZMB:IXT_DZJZ_JZMB
	{
        public System.Web.HttpRequest context = null;
        public void SetHttpContext(System.Web.HttpRequest _context)
        {
            this.context = _context;
        }
        public XT_DZJZ_JZMB() { }
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string MBJZBH)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from XT_DZJZ_JZMB");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
			strSql.Append(" where MBJZBH=:MBJZBH ");
			OracleParameter[] parameters = {
					new OracleParameter(":MBJZBH", OracleType.Char,14)			};
			parameters[0].Value = MBJZBH;

            try
            {
                return DbHelperOra.Exists(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Exists(string MBJZBH)", "EDRS.OracleDAL.XT_DZJZ_JZMB", strSql.ToString(), parameters);
            }
            return false;
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EDRS.Model.XT_DZJZ_JZMB model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into XT_DZJZ_JZMB(");
			strSql.Append("MBJZBH,AJLB,SYCH)");
			strSql.Append(" values (");
			strSql.Append(":MBJZBH,:AJLB,:SYCH)");
			OracleParameter[] parameters = {
					new OracleParameter(":MBJZBH", OracleType.Char,14),
					new OracleParameter(":AJLB", OracleType.VarChar,50),
					new OracleParameter(":SYCH", OracleType.Char,1)};
			parameters[0].Value = model.MBJZBH;
			parameters[1].Value = model.AJLB;
			parameters[2].Value = model.SYCH;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Add(EDRS.Model.XT_DZJZ_JZMB model)", "EDRS.OracleDAL.XT_DZJZ_JZMB", strSql.ToString(), parameters);
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
		/// 更新一条数据
		/// </summary>
		public bool Update(EDRS.Model.XT_DZJZ_JZMB model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update XT_DZJZ_JZMB set ");
			strSql.Append("AJLB=:AJLB,");
			strSql.Append("SYCH=:SYCH");
			strSql.Append(" where MBJZBH=:MBJZBH ");
			OracleParameter[] parameters = {
					new OracleParameter(":AJLB", OracleType.VarChar,50),
					new OracleParameter(":SYCH", OracleType.Char,1),
					new OracleParameter(":MBJZBH", OracleType.Char,14)};
			parameters[0].Value = model.AJLB;
			parameters[1].Value = model.SYCH;
			parameters[2].Value = model.MBJZBH;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Update(EDRS.Model.XT_DZJZ_JZMB model)", "EDRS.OracleDAL.XT_DZJZ_JZMB", strSql.ToString(), parameters);
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
		/// 删除一条数据
		/// </summary>
		public bool Delete(string MBJZBH)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from XT_DZJZ_JZMB ");
			strSql.Append(" where MBJZBH=:MBJZBH ");
			OracleParameter[] parameters = {
					new OracleParameter(":MBJZBH", OracleType.Char,14)			};
			parameters[0].Value = MBJZBH;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Delete(string MBJZBH)", "EDRS.OracleDAL.XT_DZJZ_JZMB", strSql.ToString(), parameters);
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
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string MBJZBHlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from XT_DZJZ_JZMB ");
			strSql.Append(" where MBJZBH in ("+MBJZBHlist + ")  ");
            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString());
            }
            catch (Exception ex)
            {
                System.Collections.Generic.Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
                parameters.Add("MBJZBH", MBJZBHlist);
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool DeleteList(string MBJZBHlist )", "EDRS.OracleDAL.XT_DZJZ_JZMB", strSql.ToString(), parameters);
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
		/// 得到一个对象实体
		/// </summary>
		public EDRS.Model.XT_DZJZ_JZMB GetModel(string MBJZBH)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select MBJZBH,AJLB,SYCH from XT_DZJZ_JZMB ");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
			strSql.Append(" where MBJZBH=:MBJZBH ");
			OracleParameter[] parameters = {
					new OracleParameter(":MBJZBH", OracleType.Char,14)			};
			parameters[0].Value = MBJZBH;

			EDRS.Model.XT_DZJZ_JZMB model=new EDRS.Model.XT_DZJZ_JZMB();
            DataSet ds = null;
            try
            {
                ds = DbHelperOra.Query(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public EDRS.Model.XT_DZJZ_JZMB GetModel(string MBJZBH)", "EDRS.OracleDAL.XT_DZJZ_JZMB", strSql.ToString(), parameters);
            }
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public EDRS.Model.XT_DZJZ_JZMB DataRowToModel(DataRow row)
		{
			EDRS.Model.XT_DZJZ_JZMB model=new EDRS.Model.XT_DZJZ_JZMB();
			if (row != null)
			{
				if(row["MBJZBH"]!=null)
				{
					model.MBJZBH=row["MBJZBH"].ToString();
				}
				if(row["AJLB"]!=null)
				{
					model.AJLB=row["AJLB"].ToString();
				}
				if(row["SYCH"]!=null)
				{
					model.SYCH=row["SYCH"].ToString();
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
        public DataSet GetList(string strWhere, params object[] objValues)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select MBJZBH,AJLB,SYCH ");            
			strSql.Append(" FROM XT_DZJZ_JZMB ");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where 1=1 "+strWhere);
			}
            try
            {
                return DbHelperOra.Query(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetList(string strWhere, params object[] objValues)", "EDRS.OracleDAL.XT_DZJZ_JZMB", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
        public int GetRecordCount(string strWhere, params object[] objValues)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM XT_DZJZ_JZMB ");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where 1=1 "+strWhere);
			}
            object obj = 0;
            try
            {
                obj = DbHelperOra.GetSingle(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public int GetRecordCount(string strWhere, params object[] objValues)", "EDRS.OracleDAL.XT_DZJZ_JZMB", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)
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
				strSql.Append("order by T.MBJZBH desc");
			}
			strSql.AppendFormat(")AS Ro, T.*  from XT_DZJZ_JZMB{0}T ",ConfigHelper.GetConfigString("OrcDBLinq"));
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)", "EDRS.OracleDAL.XT_DZJZ_JZMB", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			OracleParameter[] parameters = {
					new OracleParameter(":tblName", OracleType.VarChar, 255),
					new OracleParameter(":fldName", OracleType.VarChar, 255),
					new OracleParameter(":PageSize", OracleType.Number),
					new OracleParameter(":PageIndex", OracleType.Number),
					new OracleParameter(":IsReCount", OracleType.Clob),
					new OracleParameter(":OrderType", OracleType.Clob),
					new OracleParameter(":strWhere", OracleType.VarChar,1000),
					};
			parameters[0].Value = "XT_DZJZ_JZMB";
			parameters[1].Value = "MBJZBH";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperOra.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

