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
	/// 数据访问类:XT_ZZJG_DWBM
	/// </summary>
	public partial class XT_ZZJG_DWBM:IXT_ZZJG_DWBM
    {
        public System.Web.HttpRequest context = null;
        public void SetHttpContext(System.Web.HttpRequest _context)
        {
            this.context = _context;
        }
		public XT_ZZJG_DWBM()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string DWBM)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from XT_ZZJG_DWBM");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
			strSql.Append(" where DWBM=:DWBM ");
			OracleParameter[] parameters = {
					new OracleParameter(":DWBM", OracleType.VarChar,50)			};
			parameters[0].Value = DWBM;
            try
            {
                return DbHelperOra.Exists(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Exists(string DWBM)", "EDRS.OracleDAL.XT_ZZJG_DWBM", strSql.ToString(), parameters);
            }
            return false;
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EDRS.Model.XT_ZZJG_DWBM model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into XT_ZZJG_DWBM(");
			strSql.Append("DWBM,DWMC,DWJC,DWJB,FDWBM,SFSC)");
			strSql.Append(" values (");
			strSql.Append(":DWBM,:DWMC,:DWJC,:DWJB,:FDWBM,:SFSC)");
			OracleParameter[] parameters = {
					new OracleParameter(":DWBM", OracleType.VarChar,50),
					new OracleParameter(":DWMC", OracleType.VarChar,300),
					new OracleParameter(":DWJC", OracleType.VarChar,60),
					new OracleParameter(":DWJB", OracleType.Char,1),
					new OracleParameter(":FDWBM", OracleType.VarChar,50),
					new OracleParameter(":SFSC", OracleType.Char,1)};
			parameters[0].Value = model.DWBM;
			parameters[1].Value = model.DWMC;
			parameters[2].Value = model.DWJC;
			parameters[3].Value = model.DWJB;
			parameters[4].Value = model.FDWBM;
			parameters[5].Value = model.SFSC;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Add(EDRS.Model.XT_ZZJG_DWBM model)", "EDRS.OracleDAL.XT_ZZJG_DWBM", strSql.ToString(), parameters);
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
		public bool Update(EDRS.Model.XT_ZZJG_DWBM model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update XT_ZZJG_DWBM set ");
			strSql.Append("DWMC=:DWMC,");
			strSql.Append("DWJC=:DWJC,");
			strSql.Append("DWJB=:DWJB,");
			strSql.Append("FDWBM=:FDWBM,");
			strSql.Append("SFSC=:SFSC");
			strSql.Append(" where DWBM=:DWBM ");
			OracleParameter[] parameters = {
					new OracleParameter(":DWMC", OracleType.VarChar,300),
					new OracleParameter(":DWJC", OracleType.VarChar,60),
					new OracleParameter(":DWJB", OracleType.Char,1),
					new OracleParameter(":FDWBM", OracleType.VarChar,50),
					new OracleParameter(":SFSC", OracleType.Char,1),
					new OracleParameter(":DWBM", OracleType.VarChar,50)};
			parameters[0].Value = model.DWMC;
			parameters[1].Value = model.DWJC;
			parameters[2].Value = model.DWJB;
			parameters[3].Value = model.FDWBM;
			parameters[4].Value = model.SFSC;
			parameters[5].Value = model.DWBM;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Update(EDRS.Model.XT_ZZJG_DWBM model)", "EDRS.OracleDAL.XT_ZZJG_DWBM", strSql.ToString(), parameters);
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
		public bool Delete(string DWBM)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from XT_ZZJG_DWBM ");
			strSql.Append(" where DWBM=:DWBM ");
			OracleParameter[] parameters = {
					new OracleParameter(":DWBM", OracleType.VarChar,50)			};
			parameters[0].Value = DWBM;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Delete(string DWBM)", "EDRS.OracleDAL.XT_ZZJG_DWBM", strSql.ToString(), parameters);
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
		public bool DeleteList(string DWBMlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from XT_ZZJG_DWBM ");
			strSql.Append(" where DWBM in ("+DWBMlist + ")  ");
            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString());
            }
            catch (Exception ex)
            {
                System.Collections.Generic.Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
                parameters.Add("DWBM", DWBMlist);
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool DeleteList(string DWBMlist )", "EDRS.OracleDAL.XT_ZZJG_DWBM", strSql.ToString(), parameters);
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
        public EDRS.Model.XT_ZZJG_DWBM GetModel(string DWBM)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DWBM,DWMC,DWJC,DWJB,FDWBM,SFSC from XT_ZZJG_DWBM ");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
            strSql.Append(" where DWBM=:DWBM ");
            OracleParameter[] parameters = {
					new OracleParameter(":DWBM", OracleType.VarChar,50)			};
            parameters[0].Value = DWBM;

            EDRS.Model.XT_ZZJG_DWBM model = new EDRS.Model.XT_ZZJG_DWBM();
            DataSet ds = null;
            try
            {
                ds = DbHelperOra.Query(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public EDRS.Model.XT_ZZJG_DWBM GetModel(string DWBM)", "EDRS.OracleDAL.XT_ZZJG_DWBM", strSql.ToString(), parameters);
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
		public EDRS.Model.XT_ZZJG_DWBM DataRowToModel(DataRow row)
		{
			EDRS.Model.XT_ZZJG_DWBM model=new EDRS.Model.XT_ZZJG_DWBM();
			if (row != null)
			{
				if(row["DWBM"]!=null)
				{
					model.DWBM=row["DWBM"].ToString();
				}
				if(row["DWMC"]!=null)
				{
					model.DWMC=row["DWMC"].ToString();
				}
				if(row["DWJC"]!=null)
				{
					model.DWJC=row["DWJC"].ToString();
				}
				if(row["DWJB"]!=null)
				{
					model.DWJB=row["DWJB"].ToString();
				}
				if(row["FDWBM"]!=null)
				{
					model.FDWBM=row["FDWBM"].ToString();
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
			strSql.Append("select DWBM,DWMC,DWJC,DWJB,FDWBM,SFSC ");
			strSql.Append(" FROM XT_ZZJG_DWBM ");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetList(string strWhere, params object[] objValues)", "EDRS.OracleDAL.XT_ZZJG_DWBM", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
        public int GetRecordCount(string strWhere, params object[] objValues)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM XT_ZZJG_DWBM ");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public int GetRecordCount(string strWhere, params object[] objValues)", "EDRS.OracleDAL.XT_ZZJG_DWBM", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
				strSql.Append("order by T.DWBM desc");
			}
            strSql.AppendFormat(")AS Ro, T.*  from XT_ZZJG_DWBM{0} T ", ConfigHelper.GetConfigString("OrcDBLinq"));
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)", "EDRS.OracleDAL.XT_ZZJG_DWBM", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
			parameters[0].Value = "XT_ZZJG_DWBM";
			parameters[1].Value = "DWBM";
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

