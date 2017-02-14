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
	/// 数据访问类:XT_QX_GNFL
	/// </summary>
	public partial class XT_QX_GNFL:IXT_QX_GNFL
    {
        public System.Web.HttpRequest context = null;
        public void SetHttpContext(System.Web.HttpRequest _context)
        {
            this.context = _context;
        }
		public XT_QX_GNFL()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string FLBM)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from XT_QX_GNFL");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
			strSql.Append(" where FLBM=:FLBM ");
			OracleParameter[] parameters = {
					new OracleParameter(":FLBM", OracleType.VarChar,50)			};
			parameters[0].Value = FLBM;

            try
            {
                return DbHelperOra.Exists(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Exists(string FLBM)", "EDRS.OracleDAL.XT_QX_GNFL", strSql.ToString(), parameters);
            }
            return false;
            
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EDRS.Model.XT_QX_GNFL model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into XT_QX_GNFL(");
			strSql.Append("FLBM,FLMC,FFLBM,FLXH,SFSC,DWBM,URLDZ)");
			strSql.Append(" values (");
			strSql.Append(":FLBM,:FLMC,:FFLBM,:FLXH,:SFSC,:DWBM,:URLDZ)");
			OracleParameter[] parameters = {
					new OracleParameter(":FLBM", OracleType.VarChar,50),
					new OracleParameter(":FLMC", OracleType.VarChar,30),
					new OracleParameter(":FFLBM", OracleType.VarChar,50),
					new OracleParameter(":FLXH", OracleType.Number,4),
					new OracleParameter(":SFSC", OracleType.Char,1),
					new OracleParameter(":DWBM", OracleType.VarChar,50),
					new OracleParameter(":URLDZ", OracleType.NVarChar)};
			parameters[0].Value = model.FLBM;
			parameters[1].Value = model.FLMC;
			parameters[2].Value = model.FFLBM;
			parameters[3].Value = model.FLXH;
			parameters[4].Value = model.SFSC;
			parameters[5].Value = model.DWBM;
			parameters[6].Value = model.URLDZ;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Add(EDRS.Model.XT_QX_GNFL model)", "EDRS.OracleDAL.XT_QX_GNFL", strSql.ToString(), parameters);
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
		public bool Update(EDRS.Model.XT_QX_GNFL model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update XT_QX_GNFL set ");
			strSql.Append("FLMC=:FLMC,");
			strSql.Append("FFLBM=:FFLBM,");
			strSql.Append("FLXH=:FLXH,");
			strSql.Append("SFSC=:SFSC,");
			strSql.Append("DWBM=:DWBM,");
			strSql.Append("URLDZ=:URLDZ");
			strSql.Append(" where FLBM=:FLBM ");
			OracleParameter[] parameters = {
					new OracleParameter(":FLMC", OracleType.VarChar,30),
					new OracleParameter(":FFLBM", OracleType.VarChar,50),
					new OracleParameter(":FLXH", OracleType.Number,4),
					new OracleParameter(":SFSC", OracleType.Char,1),
					new OracleParameter(":DWBM", OracleType.VarChar,50),
					new OracleParameter(":URLDZ", OracleType.NVarChar),
					new OracleParameter(":FLBM", OracleType.VarChar,50)};
			parameters[0].Value = model.FLMC;
			parameters[1].Value = model.FFLBM;
			parameters[2].Value = model.FLXH;
			parameters[3].Value = model.SFSC;
			parameters[4].Value = model.DWBM;
			parameters[5].Value = model.URLDZ;
			parameters[6].Value = model.FLBM;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Update(EDRS.Model.XT_QX_GNFL model)", "EDRS.OracleDAL.XT_QX_GNFL", strSql.ToString(), parameters);
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
		public bool Delete(string FLBM)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from XT_QX_GNFL ");
			strSql.Append(" where FLBM=:FLBM ");
			OracleParameter[] parameters = {
					new OracleParameter(":FLBM", OracleType.VarChar,50)			};
			parameters[0].Value = FLBM;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Delete(string FLBM)", "EDRS.OracleDAL.XT_QX_GNFL", strSql.ToString(), parameters);
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
		public bool DeleteList(string FLBMlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from XT_QX_GNFL ");
			strSql.Append(" where FLBM in ("+FLBMlist + ")  ");
            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString());
            }
            catch (Exception ex)
            {
                System.Collections.Generic.Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
                parameters.Add("FLBM", FLBMlist);
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool DeleteList(string FLBMlist )", "EDRS.OracleDAL.XT_QX_GNFL", strSql.ToString(), parameters);
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
		public EDRS.Model.XT_QX_GNFL GetModel(string FLBM)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select FLBM,FLMC,FFLBM,FLXH,SFSC,DWBM,URLDZ from XT_QX_GNFL ");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
			strSql.Append(" where FLBM=:FLBM ");
			OracleParameter[] parameters = {
					new OracleParameter(":FLBM", OracleType.VarChar,50)			};
			parameters[0].Value = FLBM;

			EDRS.Model.XT_QX_GNFL model=new EDRS.Model.XT_QX_GNFL();
            DataSet ds = null;
            try
            {
                ds = DbHelperOra.Query(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public EDRS.Model.XT_QX_GNFL GetModel(string FLBM)", "EDRS.OracleDAL.XT_QX_GNFL", strSql.ToString(), parameters);
            }
			if(ds!= null && ds.Tables[0].Rows.Count>0)
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
		public EDRS.Model.XT_QX_GNFL DataRowToModel(DataRow row)
		{
			EDRS.Model.XT_QX_GNFL model=new EDRS.Model.XT_QX_GNFL();
			if (row != null)
			{
				if(row["FLBM"]!=null)
				{
					model.FLBM=row["FLBM"].ToString();
				}
				if(row["FLMC"]!=null)
				{
					model.FLMC=row["FLMC"].ToString();
				}
				if(row["FFLBM"]!=null)
				{
					model.FFLBM=row["FFLBM"].ToString();
				}
				if(row["FLXH"]!=null && row["FLXH"].ToString()!="")
				{
					model.FLXH=decimal.Parse(row["FLXH"].ToString());
				}
				if(row["SFSC"]!=null)
				{
					model.SFSC=row["SFSC"].ToString();
				}
				if(row["DWBM"]!=null)
				{
					model.DWBM=row["DWBM"].ToString();
				}
				if(row["URLDZ"]!=null)
				{
					model.URLDZ=row["URLDZ"].ToString();
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
			strSql.Append("select FLBM,FLMC,FFLBM,FLXH,SFSC,DWBM,URLDZ ");
			strSql.Append(" FROM XT_QX_GNFL ");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetList(string strWhere, params object[] objValues)", "EDRS.OracleDAL.XT_QX_GNFL", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
            
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
        public int GetRecordCount(string strWhere, params object[] objValues)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM XT_QX_GNFL ");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, " public int GetRecordCount(string strWhere, params object[] objValues)", "EDRS.OracleDAL.XT_QX_GNFL", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
				strSql.Append("order by T.FLBM desc");
			}
			strSql.AppendFormat(")AS Ro, T.*  from XT_QX_GNFL{0} T ",ConfigHelper.GetConfigString("OrcDBLinq"));
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, " public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)", "EDRS.OracleDAL.XT_QX_GNFL", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
			parameters[0].Value = "XT_QX_GNFL";
			parameters[1].Value = "FLBM";
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

