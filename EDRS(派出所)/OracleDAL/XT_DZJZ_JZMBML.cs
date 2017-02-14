
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
	/// 数据访问类:XT_DZJZ_JZMBML
	/// </summary>
	public partial class XT_DZJZ_JZMBML:IXT_DZJZ_JZMBML
    {
        public XT_DZJZ_JZMBML() { }
        public System.Web.HttpRequest context = null;
        public void SetHttpContext(System.Web.HttpRequest _context)
        {
            this.context = _context;
        }
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string MBJZBH,string MLBH)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from XT_DZJZ_JZMBML");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
			strSql.Append(" where MBJZBH=:MBJZBH and MLBH=:MLBH ");
			OracleParameter[] parameters = {
					new OracleParameter(":MBJZBH", OracleType.Char,14),
					new OracleParameter(":MLBH", OracleType.VarChar,100)			};
			parameters[0].Value = MBJZBH;
			parameters[1].Value = MLBH;
            try
            {
                return DbHelperOra.Exists(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Exists(string MBJZBH,string MLBH)", "EDRS.OracleDAL.XT_DZJZ_JZMBML", strSql.ToString(), parameters);
            }
            return false;
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EDRS.Model.XT_DZJZ_JZMBML model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into XT_DZJZ_JZMBML(");
			strSql.Append("MBJZBH,MLBH,FMLBH,MLXSMC,MLBM,MLLX,MLXX,MLSXH,SSLBBM,SSLBMC,SFBX,SFBXQFBXH)");
			strSql.Append(" values (");
			strSql.Append(":MBJZBH,:MLBH,:FMLBH,:MLXSMC,:MLBM,:MLLX,:MLXX,:MLSXH,:SSLBBM,:SSLBMC,:SFBX,:SFBXQFBXH)");
			OracleParameter[] parameters = {
					new OracleParameter(":MBJZBH", OracleType.Char,14),
					new OracleParameter(":MLBH", OracleType.VarChar,100),
					new OracleParameter(":FMLBH", OracleType.VarChar,100),
					new OracleParameter(":MLXSMC", OracleType.VarChar,300),
					new OracleParameter(":MLBM", OracleType.VarChar,300),
					new OracleParameter(":MLLX", OracleType.Char,1),
					new OracleParameter(":MLXX", OracleType.VarChar,500),
					new OracleParameter(":MLSXH", OracleType.Number,4),
					new OracleParameter(":SSLBBM", OracleType.Char,13),
					new OracleParameter(":SSLBMC", OracleType.VarChar,300),
					new OracleParameter(":SFBX", OracleType.Char,1),
					new OracleParameter(":SFBXQFBXH", OracleType.Char,1)};
			parameters[0].Value = model.MBJZBH;
			parameters[1].Value = model.MLBH;
			parameters[2].Value = model.FMLBH;
			parameters[3].Value = model.MLXSMC;
			parameters[4].Value = model.MLBM;
			parameters[5].Value = model.MLLX;
			parameters[6].Value = model.MLXX;
			parameters[7].Value = model.MLSXH;
			parameters[8].Value = model.SSLBBM;
			parameters[9].Value = model.SSLBMC;
			parameters[10].Value = model.SFBX;
			parameters[11].Value = model.SFBXQFBXH;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Add(EDRS.Model.XT_DZJZ_JZMBML model)", "EDRS.OracleDAL.XT_DZJZ_JZMBML", strSql.ToString(), parameters);
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
		public bool Update(EDRS.Model.XT_DZJZ_JZMBML model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update XT_DZJZ_JZMBML set ");
			strSql.Append("FMLBH=:FMLBH,");
			strSql.Append("MLXSMC=:MLXSMC,");
			strSql.Append("MLBM=:MLBM,");
			strSql.Append("MLLX=:MLLX,");
			strSql.Append("MLXX=:MLXX,");
			strSql.Append("MLSXH=:MLSXH,");
			strSql.Append("SSLBBM=:SSLBBM,");
			strSql.Append("SSLBMC=:SSLBMC,");
			strSql.Append("SFBX=:SFBX,");
			strSql.Append("SFBXQFBXH=:SFBXQFBXH");
			strSql.Append(" where MBJZBH=:MBJZBH and MLBH=:MLBH ");
			OracleParameter[] parameters = {
					new OracleParameter(":FMLBH", OracleType.VarChar,100),
					new OracleParameter(":MLXSMC", OracleType.VarChar,300),
					new OracleParameter(":MLBM", OracleType.VarChar,300),
					new OracleParameter(":MLLX", OracleType.Char,1),
					new OracleParameter(":MLXX", OracleType.VarChar,500),
					new OracleParameter(":MLSXH", OracleType.Number,4),
					new OracleParameter(":SSLBBM", OracleType.Char,13),
					new OracleParameter(":SSLBMC", OracleType.VarChar,300),
					new OracleParameter(":SFBX", OracleType.Char,1),
					new OracleParameter(":SFBXQFBXH", OracleType.Char,1),
					new OracleParameter(":MBJZBH", OracleType.Char,14),
					new OracleParameter(":MLBH", OracleType.VarChar,100)};
			parameters[0].Value = model.FMLBH;
			parameters[1].Value = model.MLXSMC;
			parameters[2].Value = model.MLBM;
			parameters[3].Value = model.MLLX;
			parameters[4].Value = model.MLXX;
			parameters[5].Value = model.MLSXH;
			parameters[6].Value = model.SSLBBM;
			parameters[7].Value = model.SSLBMC;
			parameters[8].Value = model.SFBX;
			parameters[9].Value = model.SFBXQFBXH;
			parameters[10].Value = model.MBJZBH;
			parameters[11].Value = model.MLBH;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Update(EDRS.Model.XT_DZJZ_JZMBML model)", "EDRS.OracleDAL.XT_DZJZ_JZMBML", strSql.ToString(), parameters);
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
		public bool Delete(string MBJZBH,string MLBH)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from XT_DZJZ_JZMBML ");
			strSql.Append(" where MBJZBH=:MBJZBH and MLBH=:MLBH ");
			OracleParameter[] parameters = {
					new OracleParameter(":MBJZBH", OracleType.Char,14),
					new OracleParameter(":MLBH", OracleType.VarChar,100)			};
			parameters[0].Value = MBJZBH;
			parameters[1].Value = MLBH;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Delete(string MBJZBH,string MLBH)", "EDRS.OracleDAL.XT_DZJZ_JZMBML", strSql.ToString(), parameters);
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
		public EDRS.Model.XT_DZJZ_JZMBML GetModel(string MBJZBH,string MLBH)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select MBJZBH,MLBH,FMLBH,MLXSMC,MLBM,MLLX,MLXX,MLSXH,SSLBBM,SSLBMC,SFBX,SFBXQFBXH from XT_DZJZ_JZMBML ");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
            strSql.Append(" where MBJZBH=:MBJZBH and MLBH=:MLBH ");
			OracleParameter[] parameters = {
					new OracleParameter(":MBJZBH", OracleType.Char,14),
					new OracleParameter(":MLBH", OracleType.VarChar,100)			};
			parameters[0].Value = MBJZBH;
			parameters[1].Value = MLBH;

			EDRS.Model.XT_DZJZ_JZMBML model=new EDRS.Model.XT_DZJZ_JZMBML();
            DataSet ds = null;
            try
            {
                ds = DbHelperOra.Query(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public EDRS.Model.XT_DZJZ_JZMBML GetModel(string MBJZBH,string MLBH)", "EDRS.OracleDAL.XT_DZJZ_JZMBML", strSql.ToString(), parameters);
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
		public EDRS.Model.XT_DZJZ_JZMBML DataRowToModel(DataRow row)
		{
			EDRS.Model.XT_DZJZ_JZMBML model=new EDRS.Model.XT_DZJZ_JZMBML();
			if (row != null)
			{
				if(row["MBJZBH"]!=null)
				{
					model.MBJZBH=row["MBJZBH"].ToString();
				}
				if(row["MLBH"]!=null)
				{
					model.MLBH=row["MLBH"].ToString();
				}
				if(row["FMLBH"]!=null)
				{
					model.FMLBH=row["FMLBH"].ToString();
				}
				if(row["MLXSMC"]!=null)
				{
					model.MLXSMC=row["MLXSMC"].ToString();
				}
				if(row["MLBM"]!=null)
				{
					model.MLBM=row["MLBM"].ToString();
				}
				if(row["MLLX"]!=null)
				{
					model.MLLX=row["MLLX"].ToString();
				}
				if(row["MLXX"]!=null)
				{
					model.MLXX=row["MLXX"].ToString();
				}
				if(row["MLSXH"]!=null && row["MLSXH"].ToString()!="")
				{
					model.MLSXH=decimal.Parse(row["MLSXH"].ToString());
				}
				if(row["SSLBBM"]!=null)
				{
					model.SSLBBM=row["SSLBBM"].ToString();
				}
				if(row["SSLBMC"]!=null)
				{
					model.SSLBMC=row["SSLBMC"].ToString();
				}
				if(row["SFBX"]!=null)
				{
					model.SFBX=row["SFBX"].ToString();
				}
				if(row["SFBXQFBXH"]!=null)
				{
					model.SFBXQFBXH=row["SFBXQFBXH"].ToString();
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
			strSql.Append("select MBJZBH,MLBH,FMLBH,MLXSMC,MLBM,MLLX,MLXX,MLSXH,SSLBBM,SSLBMC,SFBX,SFBXQFBXH ");
			strSql.Append(" FROM XT_DZJZ_JZMBML ");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetList(string strWhere, params object[] objValues)", "EDRS.OracleDAL.XT_DZJZ_JZMBML", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
        public int GetRecordCount(string strWhere, params object[] objValues)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM XT_DZJZ_JZMBML ");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where 1=1 "+strWhere);
			}
            object obj = null;
            try
            {
                obj = DbHelperOra.GetSingle(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public int GetRecordCount(string strWhere, params object[] objValues)", "EDRS.OracleDAL.XT_DZJZ_JZMBML", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
				strSql.Append("order by T.MLBH desc");
			}
			strSql.AppendFormat(")AS Ro, T.*  from XT_DZJZ_JZMBML{0} T ",ConfigHelper.GetConfigString("OrcDBLinq"));
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)", "EDRS.OracleDAL.XT_DZJZ_JZMBML", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
			parameters[0].Value = "XT_DZJZ_JZMBML";
			parameters[1].Value = "MLBH";
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

