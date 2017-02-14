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
    /// 数据访问类:YX_DZJZ_LSAJWJFP
	/// </summary>
	public partial class YX_DZJZ_LSAJWJFP:IYX_DZJZ_LSAJWJFP
    {
        public System.Web.HttpRequest context = null;
        public void SetHttpContext(System.Web.HttpRequest _context)
        {
            this.context = _context;
        }
        public YX_DZJZ_LSAJWJFP()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
        public bool Exists(string FPBM)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from YX_DZJZ_LSAJWJFP");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
            strSql.Append(" where FPBM=:FPBM");
			OracleParameter[] parameters = {
					new OracleParameter(":FPBM", OracleType.VarChar,50)
				};
            parameters[0].Value = FPBM;
            try
            {
                return DbHelperOra.Exists(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Exists(FPBM)", "EDRS.OracleDAL.YX_DZJZ_LSAJWJFP", strSql.ToString(), parameters);
            }
            return false;
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EDRS.Model.YX_DZJZ_LSAJWJFP model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into YX_DZJZ_LSAJWJFP(");
            strSql.Append("YJXH,WJXH,SFSC)");
			strSql.Append(" values (");
            strSql.Append(":YJXH,:WJXH,:SFSC)");
			OracleParameter[] parameters = {
					new OracleParameter(":YJXH", OracleType.VarChar,50),
					new OracleParameter(":WJXH", OracleType.VarChar,100),
					new OracleParameter(":SFSC", OracleType.Char,1)
				};
			parameters[0].Value = model.YJXH;
			parameters[1].Value = model.WJXH;
			parameters[2].Value = model.SFSC;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Add(EDRS.Model.YX_DZJZ_LSAJWJFP model)", "EDRS.OracleDAL.YX_DZJZ_LSAJWJFP", strSql.ToString(), parameters);
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
		public bool Update(EDRS.Model.YX_DZJZ_LSAJWJFP model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update YX_DZJZ_LSAJWJFP set ");
            strSql.Append("YJXH=:YJXH,");
            strSql.Append("WJXH=:WJXH,");
            strSql.Append("SFSC=:SFSC");
            strSql.Append(" where FPBM=:FPBM ");
			OracleParameter[] parameters = {
					new OracleParameter(":YJXH", OracleType.VarChar,50),
					new OracleParameter(":WJXH", OracleType.VarChar,100),
					new OracleParameter(":SFSC", OracleType.Char,1),
					new OracleParameter(":FPBM", OracleType.VarChar,50)};
            parameters[0].Value = model.YJXH;
            parameters[1].Value = model.WJXH;
            parameters[2].Value = model.SFSC;
            parameters[3].Value = model.FPBM;
            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Update(EDRS.Model.YX_DZJZ_LSAJWJFP model)", "EDRS.OracleDAL.YX_DZJZ_LSAJWJFP", strSql.ToString(), parameters);
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
        public bool Delete(string FPBM)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from YX_DZJZ_LSAJWJFP ");
            strSql.Append(" where FPBM=:FPBM ");
			OracleParameter[] parameters = {
					new OracleParameter(":FPBM", OracleType.VarChar,50)			};
            parameters[0].Value = FPBM;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Delete(string LSZH,string BMSAH,decimal YJXH)", "EDRS.OracleDAL.YX_DZJZ_LSAJWJFP", strSql.ToString(), parameters);
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
        public EDRS.Model.YX_DZJZ_LSAJWJFP GetModel(string FPBM)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select FPBM,YJXH,WJXH,ADDTIME,SFSC from YX_DZJZ_LSAJWJFP ");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
            strSql.Append(" where FPBM=:FPBM  ");
			OracleParameter[] parameters = {
					new OracleParameter(":FPBM", OracleType.VarChar,100)		};
            parameters[0].Value = FPBM;

			EDRS.Model.YX_DZJZ_LSAJWJFP model=new EDRS.Model.YX_DZJZ_LSAJWJFP();
            DataSet ds = null;
            try
            {
                ds = DbHelperOra.Query(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public EDRS.Model.YX_DZJZ_LSAJWJFP GetModel(string FPBM)", "EDRS.OracleDAL.YX_DZJZ_LSAJWJFP", strSql.ToString(), parameters);
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
		public EDRS.Model.YX_DZJZ_LSAJWJFP DataRowToModel(DataRow row)
		{
			EDRS.Model.YX_DZJZ_LSAJWJFP model=new EDRS.Model.YX_DZJZ_LSAJWJFP();
			if (row != null)
			{
                if (row["FPBM"] != null)
				{
                    model.FPBM = row["FPBM"].ToString();
				}
                if (row["YJXH"] != null)
				{
                    model.YJXH = row["YJXH"].ToString();
				}
                if (row["WJXH"] != null)
				{
                    model.WJXH = row["WJXH"].ToString();
				}
                if (row["ADDTIME"] != null)
                {
                    model.ADDTIME = Convert.ToDateTime(row["ADDTIME"]);
                }
				if(row["SFSC"]!=null)
				{
					model.SFSC=row["SFSC"].ToString();
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
            strSql.Append("select FPBM,YJXH,WJXH,ADDTIME,SFSC ");
			strSql.Append(" FROM YX_DZJZ_LSAJWJFP ");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetList(string strWhere, params object[] objValues)", "EDRS.OracleDAL.YX_DZJZ_LSAJWJFP", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
        public int GetRecordCount(string strWhere, params object[] objValues)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM YX_DZJZ_LSAJWJFP ");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public int GetRecordCount(string strWhere, params object[] objValues)", "EDRS.OracleDAL.YX_DZJZ_LSAJWJFP", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
                strSql.Append("order by T.FPBM desc");
			}
			strSql.AppendFormat(")AS Ro, T.*  from YX_DZJZ_LSAJWJFP{0} T ",ConfigHelper.GetConfigString("OrcDBLinq"));
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)", "EDRS.OracleDAL.YX_DZJZ_LSAJWJFP", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
			parameters[0].Value = "YX_DZJZ_LSAJWJFP";
			parameters[1].Value = "YJXH";
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

