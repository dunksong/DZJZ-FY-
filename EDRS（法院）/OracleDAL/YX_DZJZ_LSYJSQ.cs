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
	/// 数据访问类:YX_DZJZ_LSYJSQ
	/// </summary>
	public partial class YX_DZJZ_LSYJSQ:IYX_DZJZ_LSYJSQ
    {
        public System.Web.HttpRequest context = null;
        public void SetHttpContext(System.Web.HttpRequest _context)
        {
            this.context = _context;
        }
		public YX_DZJZ_LSYJSQ()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string YJSQDH)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from YX_DZJZ_LSYJSQ");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
			strSql.Append(" where YJSQDH=:YJSQDH ");
			OracleParameter[] parameters = {
					new OracleParameter(":YJSQDH", OracleType.VarChar,50)			};
			parameters[0].Value = YJSQDH;
            try
            {
                return DbHelperOra.Exists(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Exists(string YJSQDH)", "EDRS.OracleDAL.YX_DZJZ_LSYJSQ", strSql.ToString(), parameters);
            }
            return false;
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EDRS.Model.YX_DZJZ_LSYJSQ model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into YX_DZJZ_LSYJSQ(");
			strSql.Append("LSZH,SQSJ,SQSM,SFSC,SHRGH,SHR,SHSM,SHSJ,YJSQDM,SQDZT)");
			strSql.Append(" values (");
			strSql.Append(":LSZH,:SQSJ,:SQSM,:SFSC,:SHRGH,:SHR,:SHSM,:SHSJ,:YJSQDM,:SQDZT)");
			OracleParameter[] parameters = {
					new OracleParameter(":LSZH", OracleType.VarChar,100),					
					new OracleParameter(":SQSJ", OracleType.DateTime),
					new OracleParameter(":SQSM", OracleType.VarChar,300),
					new OracleParameter(":SFSC", OracleType.Char,1),
					new OracleParameter(":SHRGH", OracleType.Char,4),
					new OracleParameter(":SHR", OracleType.VarChar,60),
					new OracleParameter(":SHSM", OracleType.VarChar,300),
					new OracleParameter(":SHSJ", OracleType.DateTime),
					new OracleParameter(":YJSQDM", OracleType.VarChar,300),
					new OracleParameter(":SQDZT", OracleType.Char,5)};
            parameters[0].Value = (object)model.LSZH ?? DBNull.Value;           
            parameters[1].Value = (object)model.SQSJ ?? DBNull.Value;
            parameters[2].Value = (object)model.SQSM ?? DBNull.Value;
            parameters[3].Value = (object)model.SFSC ?? DBNull.Value;
            parameters[4].Value = (object)model.SHRGH ?? DBNull.Value;
            parameters[5].Value = (object)model.SHR ?? DBNull.Value;
            parameters[6].Value = (object)model.SHSM ?? DBNull.Value;
            parameters[7].Value = (object)model.SHSJ ?? DBNull.Value;
            parameters[8].Value = (object)model.YJSQDM ?? DBNull.Value;
            parameters[9].Value = (object)model.SQDZT ?? DBNull.Value;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Add(EDRS.Model.YX_DZJZ_LSYJSQ model)", "EDRS.OracleDAL.YX_DZJZ_LSYJSQ", strSql.ToString(), parameters);
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
		public bool Update(EDRS.Model.YX_DZJZ_LSYJSQ model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update YX_DZJZ_LSYJSQ set ");
			strSql.Append("LSZH=:LSZH,");
			strSql.Append("SQSJ=:SQSJ,");
			strSql.Append("SQSM=:SQSM,");
			strSql.Append("SFSC=:SFSC,");
			strSql.Append("SHRGH=:SHRGH,");
			strSql.Append("SHR=:SHR,");
			strSql.Append("SHSM=:SHSM,");
			strSql.Append("SHSJ=:SHSJ,");
			strSql.Append("YJSQDM=:YJSQDM,");
			strSql.Append("SQDZT=:SQDZT");
			strSql.Append(" where YJSQDH=:YJSQDH ");
			OracleParameter[] parameters = {
					new OracleParameter(":LSZH", OracleType.VarChar,100),
					new OracleParameter(":SQSJ", OracleType.DateTime),
					new OracleParameter(":SQSM", OracleType.VarChar,300),
					new OracleParameter(":SFSC", OracleType.Char,1),
					new OracleParameter(":SHRGH", OracleType.Char,4),
					new OracleParameter(":SHR", OracleType.VarChar,60),
					new OracleParameter(":SHSM", OracleType.VarChar,300),
					new OracleParameter(":SHSJ", OracleType.DateTime),
					new OracleParameter(":YJSQDM", OracleType.VarChar,300),
					new OracleParameter(":SQDZT", OracleType.Char,1),
					new OracleParameter(":YJSQDH", OracleType.VarChar,50)};
			parameters[0].Value = model.LSZH;
			parameters[1].Value = model.SQSJ;
			parameters[2].Value = model.SQSM;
			parameters[3].Value = model.SFSC;
			parameters[4].Value = model.SHRGH;
			parameters[5].Value = model.SHR;
			parameters[6].Value = model.SHSM;
			parameters[7].Value = model.SHSJ;
			parameters[8].Value = model.YJSQDM;
			parameters[9].Value = model.SQDZT;
			parameters[10].Value = model.YJSQDH;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Update(EDRS.Model.YX_DZJZ_LSYJSQ model)", "EDRS.OracleDAL.YX_DZJZ_LSYJSQ", strSql.ToString(), parameters);
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
		public bool Delete(string YJSQDH)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from YX_DZJZ_LSYJSQ ");
			strSql.Append(" where YJSQDH=:YJSQDH ");
			OracleParameter[] parameters = {
					new OracleParameter(":YJSQDH", OracleType.VarChar,50)			};
			parameters[0].Value = YJSQDH;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Delete(string YJSQDH)", "EDRS.OracleDAL.YX_DZJZ_LSYJSQ", strSql.ToString(), parameters);
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
		public bool DeleteList(string YJSQDHlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from YX_DZJZ_LSYJSQ ");
			strSql.Append(" where YJSQDH in ("+YJSQDHlist + ")  ");
            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString());
            }
            catch (Exception ex)
            {
                System.Collections.Generic.Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
                parameters.Add("YJSQDH", YJSQDHlist);
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool DeleteList(string YJSQDHlist )", "EDRS.OracleDAL.YX_DZJZ_LSYJSQ", strSql.ToString(), parameters);
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
        public EDRS.Model.YX_DZJZ_LSYJSQ GetModel(string YJSQDH)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select LSZH,YJSQDH,SQSJ,SQSM,SFSC,SHRGH,SHR,SHSM,SHSJ,YJSQDM,SQDZT from YX_DZJZ_LSYJSQ ");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
            strSql.Append(" where YJSQDH=:YJSQDH ");
            OracleParameter[] parameters = {
					new OracleParameter(":YJSQDH", OracleType.VarChar,50)			};
            parameters[0].Value = YJSQDH;

            EDRS.Model.YX_DZJZ_LSYJSQ model = new EDRS.Model.YX_DZJZ_LSYJSQ();
            DataSet ds = null;
            try
            {
                ds = DbHelperOra.Query(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public EDRS.Model.YX_DZJZ_LSYJSQ GetModel(string YJSQDH)", "EDRS.OracleDAL.YX_DZJZ_LSYJSQ", strSql.ToString(), parameters);
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
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
		public EDRS.Model.YX_DZJZ_LSYJSQ DataRowToModel(DataRow row)
		{
			EDRS.Model.YX_DZJZ_LSYJSQ model=new EDRS.Model.YX_DZJZ_LSYJSQ();
			if (row != null)
			{
				if(row["LSZH"]!=null)
				{
					model.LSZH=row["LSZH"].ToString();
				}
				if(row["YJSQDH"]!=null)
				{
					model.YJSQDH=row["YJSQDH"].ToString();
				}
				if(row["SQSJ"]!=null && row["SQSJ"].ToString()!="")
				{
					model.SQSJ=DateTime.Parse(row["SQSJ"].ToString());
				}
				if(row["SQSM"]!=null)
				{
					model.SQSM=row["SQSM"].ToString();
				}
				if(row["SFSC"]!=null)
				{
					model.SFSC=row["SFSC"].ToString();
				}
				if(row["SHRGH"]!=null)
				{
					model.SHRGH=row["SHRGH"].ToString();
				}
				if(row["SHR"]!=null)
				{
					model.SHR=row["SHR"].ToString();
				}
				if(row["SHSM"]!=null)
				{
					model.SHSM=row["SHSM"].ToString();
				}
				if(row["SHSJ"]!=null && row["SHSJ"].ToString()!="")
				{
					model.SHSJ=DateTime.Parse(row["SHSJ"].ToString());
				}
				if(row["YJSQDM"]!=null)
				{
					model.YJSQDM=row["YJSQDM"].ToString();
				}				
				if(row["SQDZT"]!=null)
				{
					model.SQDZT=row["SQDZT"].ToString();
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
			strSql.Append("select LSZH,YJSQDH,SQSJ,SQSM,SFSC,SHRGH,SHR,SHSM,SHSJ,YJSQDM,SQDZT ");
			strSql.Append(" FROM YX_DZJZ_LSYJSQ ");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetList(string strWhere, params object[] objValues)", "EDRS.OracleDAL.YX_DZJZ_LSYJSQ", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
        public int GetRecordCount(string strWhere, params object[] objValues)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM YX_DZJZ_LSYJSQ ");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public int GetRecordCount(string strWhere, params object[] objValues)", "EDRS.OracleDAL.YX_DZJZ_LSYJSQ", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
				strSql.Append("order by T.YJSQDH desc");
			}
			strSql.AppendFormat(")AS Ro, T.*  from YX_DZJZ_LSYJSQ{0} T ",ConfigHelper.GetConfigString("OrcDBLinq"));
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)", "EDRS.OracleDAL.YX_DZJZ_LSYJSQ", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
			parameters[0].Value = "YX_DZJZ_LSYJSQ";
			parameters[1].Value = "YJSQDH";
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

