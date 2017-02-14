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
	/// 数据访问类:YX_DZJZ_DYSQD
	/// </summary>
	public partial class YX_DZJZ_DYSQD:IYX_DZJZ_DYSQD
    {
        public System.Web.HttpRequest context = null;
        public void SetHttpContext(System.Web.HttpRequest _context)
        {
            this.context = _context;
        }
		public YX_DZJZ_DYSQD()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string DYSQDH)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from YX_DZJZ_DYSQD");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
			strSql.Append(" where DYSQDH=:DYSQDH ");
			OracleParameter[] parameters = {
					new OracleParameter(":DYSQDH", OracleType.VarChar,50)			};
			parameters[0].Value = DYSQDH;
            try
            {
                return DbHelperOra.Exists(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Exists(string DYSQDH)", "EDRS.OracleDAL.YX_DZJZ_DYSQD", strSql.ToString(), parameters);
            }
            return false;
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EDRS.Model.YX_DZJZ_DYSQD model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into YX_DZJZ_DYSQD(");
			strSql.Append("DYSQDH,SQDM,DYSQSJ,LSZH,SQBZ,DYZT,DYR,DYRGH,DYZFY,DYSJ,CLSM)");
			strSql.Append(" values (");
			strSql.Append(":DYSQDH,:SQDM,:DYSQSJ,:LSZH,:SQBZ,:DYZT,:DYR,:DYRGH,:DYZFY,:DYSJ,:CLSM)");
			OracleParameter[] parameters = {
					new OracleParameter(":DYSQDH", OracleType.VarChar,50),
					new OracleParameter(":SQDM", OracleType.VarChar,300),
					new OracleParameter(":DYSQSJ", OracleType.DateTime),
					new OracleParameter(":LSZH", OracleType.VarChar,100),
					new OracleParameter(":SQBZ", OracleType.VarChar,300),
					new OracleParameter(":DYZT", OracleType.Char,1),
					new OracleParameter(":DYR", OracleType.VarChar,60),
					new OracleParameter(":DYRGH", OracleType.Char,4),
					new OracleParameter(":DYZFY", OracleType.Number,8),
					new OracleParameter(":DYSJ", OracleType.DateTime),
					new OracleParameter(":CLSM", OracleType.VarChar,300)};
			parameters[0].Value = model.DYSQDH;
			parameters[1].Value = model.SQDM;
			parameters[2].Value = model.DYSQSJ;
			parameters[3].Value = model.LSZH;
			parameters[4].Value = model.SQBZ;
			parameters[5].Value = model.DYZT;
			parameters[6].Value = model.DYR;
			parameters[7].Value = model.DYRGH;
			parameters[8].Value = model.DYZFY;
			parameters[9].Value = model.DYSJ;
			parameters[10].Value = model.CLSM;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Add(EDRS.Model.YX_DZJZ_DYSQD model)", "EDRS.OracleDAL.YX_DZJZ_DYSQD", strSql.ToString(), parameters);
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
		public bool Update(EDRS.Model.YX_DZJZ_DYSQD model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update YX_DZJZ_DYSQD set ");
			strSql.Append("SQDM=:SQDM,");
			strSql.Append("DYSQSJ=:DYSQSJ,");
			strSql.Append("LSZH=:LSZH,");
			strSql.Append("SQBZ=:SQBZ,");
			strSql.Append("DYZT=:DYZT,");
			strSql.Append("DYR=:DYR,");
			strSql.Append("DYRGH=:DYRGH,");
			strSql.Append("DYZFY=:DYZFY,");
			strSql.Append("DYSJ=:DYSJ,");
			strSql.Append("CLSM=:CLSM");
			strSql.Append(" where DYSQDH=:DYSQDH ");
			OracleParameter[] parameters = {
					new OracleParameter(":SQDM", OracleType.VarChar,300),
					new OracleParameter(":DYSQSJ", OracleType.DateTime),
					new OracleParameter(":LSZH", OracleType.VarChar,100),
					new OracleParameter(":SQBZ", OracleType.VarChar,300),
					new OracleParameter(":DYZT", OracleType.Char,1),
					new OracleParameter(":DYR", OracleType.VarChar,60),
					new OracleParameter(":DYRGH", OracleType.Char,4),
					new OracleParameter(":DYZFY", OracleType.Number,8),
					new OracleParameter(":DYSJ", OracleType.DateTime),
					new OracleParameter(":CLSM", OracleType.VarChar,300),
					new OracleParameter(":DYSQDH", OracleType.VarChar,50)};
			parameters[0].Value = model.SQDM;
			parameters[1].Value = model.DYSQSJ;
			parameters[2].Value = model.LSZH;
			parameters[3].Value = model.SQBZ;
			parameters[4].Value = model.DYZT;
			parameters[5].Value = model.DYR;
			parameters[6].Value = model.DYRGH;
			parameters[7].Value = model.DYZFY;
			parameters[8].Value = model.DYSJ;
			parameters[9].Value = model.CLSM;
			parameters[10].Value = model.DYSQDH;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Update(EDRS.Model.YX_DZJZ_DYSQD model)", "EDRS.OracleDAL.YX_DZJZ_DYSQD", strSql.ToString(), parameters);
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
		public bool Delete(string DYSQDH)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from YX_DZJZ_DYSQD ");
			strSql.Append(" where DYSQDH=:DYSQDH ");
			OracleParameter[] parameters = {
					new OracleParameter(":DYSQDH", OracleType.VarChar,50)			};
			parameters[0].Value = DYSQDH;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Delete(string DYSQDH)", "EDRS.OracleDAL.YX_DZJZ_DYSQD", strSql.ToString(), parameters);
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
		public bool DeleteList(string DYSQDHlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from YX_DZJZ_DYSQD ");
			strSql.Append(" where DYSQDH in ("+DYSQDHlist + ")  ");
            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString());
            }
            catch (Exception ex)
            {
                System.Collections.Generic.Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
                parameters.Add("DYSQDH", DYSQDHlist);
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool DeleteList(string DYSQDHlist )", "EDRS.OracleDAL.YX_DZJZ_DYSQD", strSql.ToString(), parameters);
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
		public EDRS.Model.YX_DZJZ_DYSQD GetModel(string DYSQDH)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select DYSQDH,SQDM,DYSQSJ,LSZH,SQBZ,DYZT,DYR,DYRGH,DYZFY,DYSJ,CLSM from YX_DZJZ_DYSQD ");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
			strSql.Append(" where DYSQDH=:DYSQDH ");
			OracleParameter[] parameters = {
					new OracleParameter(":DYSQDH", OracleType.VarChar,50)			};
			parameters[0].Value = DYSQDH;

			EDRS.Model.YX_DZJZ_DYSQD model=new EDRS.Model.YX_DZJZ_DYSQD();
            DataSet ds = null;
            try
            {
                ds = DbHelperOra.Query(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public EDRS.Model.YX_DZJZ_DYSQD GetModel(string DYSQDH)", "EDRS.OracleDAL.YX_DZJZ_DYSQD", strSql.ToString(), parameters);
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
		public EDRS.Model.YX_DZJZ_DYSQD DataRowToModel(DataRow row)
		{
			EDRS.Model.YX_DZJZ_DYSQD model=new EDRS.Model.YX_DZJZ_DYSQD();
			if (row != null)
			{
				if(row["DYSQDH"]!=null)
				{
					model.DYSQDH=row["DYSQDH"].ToString();
				}
				if(row["SQDM"]!=null)
				{
					model.SQDM=row["SQDM"].ToString();
				}
				if(row["DYSQSJ"]!=null && row["DYSQSJ"].ToString()!="")
				{
					model.DYSQSJ=DateTime.Parse(row["DYSQSJ"].ToString());
				}
				if(row["LSZH"]!=null)
				{
					model.LSZH=row["LSZH"].ToString();
				}
				if(row["SQBZ"]!=null)
				{
					model.SQBZ=row["SQBZ"].ToString();
				}
				if(row["DYZT"]!=null)
				{
					model.DYZT=row["DYZT"].ToString();
				}
				if(row["DYR"]!=null)
				{
					model.DYR=row["DYR"].ToString();
				}
				if(row["DYRGH"]!=null)
				{
					model.DYRGH=row["DYRGH"].ToString();
				}
				if(row["DYZFY"]!=null && row["DYZFY"].ToString()!="")
				{
					model.DYZFY=decimal.Parse(row["DYZFY"].ToString());
				}
				if(row["DYSJ"]!=null && row["DYSJ"].ToString()!="")
				{
					model.DYSJ=DateTime.Parse(row["DYSJ"].ToString());
				}
				if(row["CLSM"]!=null)
				{
					model.CLSM=row["CLSM"].ToString();
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
			strSql.Append("select DYSQDH,SQDM,DYSQSJ,LSZH,SQBZ,DYZT,DYR,DYRGH,DYZFY,DYSJ,CLSM ");
			strSql.Append(" FROM YX_DZJZ_DYSQD ");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetList(string strWhere, params object[] objValues)", "EDRS.OracleDAL.YX_DZJZ_DYSQD", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
        public int GetRecordCount(string strWhere, params object[] objValues)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM YX_DZJZ_DYSQD ");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public int GetRecordCount(string strWhere, params object[] objValues)", "EDRS.OracleDAL.YX_DZJZ_DYSQD", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
				strSql.Append("order by T.DYSQDH desc");
			}
			strSql.AppendFormat(")AS Ro, T.*  from YX_DZJZ_DYSQD{0} T " ,ConfigHelper.GetConfigString("OrcDBLinq"));
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)", "EDRS.OracleDAL.YX_DZJZ_DYSQD", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
			parameters[0].Value = "YX_DZJZ_DYSQD";
			parameters[1].Value = "DYSQDH";
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

