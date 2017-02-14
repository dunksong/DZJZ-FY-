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
    /// 数据访问类:XT_DM_AJLBBM
	/// </summary>
	public partial class XT_DM_AJLBBM:IXT_DM_AJLBBM
	{
        public System.Web.HttpRequest context = null;
        public XT_DM_AJLBBM() { }
		#region  BasicMethod
        public void SetHttpContext(System.Web.HttpRequest _context)
        {
            this.context = _context;
        }
		/// <summary>
		/// 是否存在该记录
		/// </summary>
        public bool Exists(string AJLBBM)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from XT_DM_AJLBBM");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
            strSql.Append(" where AJLBBM=:AJLBBM ");
			OracleParameter[] parameters = {
					new OracleParameter(":AJLBBM", OracleType.VarChar,50)		};
            parameters[0].Value = AJLBBM;

            try
            {
                return DbHelperOra.Exists(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Exists(string AJLBBM)", "EDRS.OracleDAL.XT_DM_AJLBBM", strSql.ToString(), parameters);
            }
            return false;
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EDRS.Model.XT_DM_AJLBBM model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into XT_DM_AJLBBM(");
            strSql.Append("YWBM,AJLBBM,AJLBMC,AJSLJC,SFSC,XH)");
			strSql.Append(" values (");
            strSql.Append(":YWBM,:AJLBBM,:AJLBMC,:AJSLJC,:SFSC,:XH)");
			OracleParameter[] parameters = {
					new OracleParameter(":YWBM", OracleType.Char,2),
					new OracleParameter(":AJLBBM", OracleType.VarChar,50),
					new OracleParameter(":AJLBMC", OracleType.VarChar,300),
					new OracleParameter(":AJSLJC", OracleType.VarChar,60),
					new OracleParameter(":SFSC", OracleType.Char,1),
					new OracleParameter(":XH", OracleType.Number)};
            parameters[0].Value = model.YWBM;
            parameters[1].Value = model.AJLBBM;
            parameters[2].Value = model.AJLBMC;
            parameters[3].Value = model.AJSLJC;
            parameters[4].Value = model.SFSC;
            parameters[5].Value = model.XH;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Add(EDRS.Model.XT_DM_AJLBBM model)", "EDRS.OracleDAL.XT_DM_AJLBBM", strSql.ToString(), parameters);
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
		public bool Update(EDRS.Model.XT_DM_AJLBBM model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update XT_DM_AJLBBM set ");
            strSql.Append(" YWBM=:YWBM,AJLBBM=:AJLBBM,AJLBMC=:AJLBMC,AJSLJC=:AJSLJC,SFSC=:SFSC,XH=:XH ");
            strSql.Append(" where AJLBBM=:AJLBBM ");
			OracleParameter[] parameters = {
					new OracleParameter(":YWBM", OracleType.Char,2),
					new OracleParameter(":AJLBBM", OracleType.VarChar,50),
					new OracleParameter(":AJLBMC", OracleType.VarChar,300),
					new OracleParameter(":AJSLJC", OracleType.VarChar,60),
					new OracleParameter(":SFSC", OracleType.Char,1),
					new OracleParameter(":XH", OracleType.Number)};
            parameters[0].Value = model.YWBM;
            parameters[1].Value = model.AJLBBM;
            parameters[2].Value = model.AJLBMC;
            parameters[3].Value = model.AJSLJC;
            parameters[4].Value = model.SFSC;
            parameters[5].Value = model.XH;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Update(EDRS.Model.XT_DM_AJLBBM model)", "EDRS.OracleDAL.XT_DM_AJLBBM", strSql.ToString(), parameters);
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
        public bool Delete(string AJLBBM)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from XT_DM_AJLBBM ");
            strSql.Append(" where AJLBBM=:AJLBBM ");
			OracleParameter[] parameters = {
					new OracleParameter(":AJLBBM", OracleType.VarChar,50)		};
            parameters[0].Value = AJLBBM;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Delete(string AJLBBM)", "EDRS.OracleDAL.XT_DM_AJLBBM", strSql.ToString(), parameters);
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
        public EDRS.Model.XT_DM_AJLBBM GetModel(string AJLBBM)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select YWBM,AJLBBM,AJLBMC,AJSLJC,SFSC,XH from XT_DM_AJLBBM ");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
            strSql.Append(" where AJLBBM=:AJLBBM ");
			OracleParameter[] parameters = {
					new OracleParameter(":AJLBBM", OracleType.VarChar,50)		};
            parameters[0].Value = AJLBBM;

			EDRS.Model.XT_DM_AJLBBM model=new EDRS.Model.XT_DM_AJLBBM();
            DataSet ds = null;
            try
            {
                ds = DbHelperOra.Query(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public EDRS.Model.XT_DM_AJLBBM GetModel(string AJLBBM)", "EDRS.OracleDAL.XT_DM_AJLBBM", strSql.ToString(), parameters);
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
		public EDRS.Model.XT_DM_AJLBBM DataRowToModel(DataRow row)
		{
			EDRS.Model.XT_DM_AJLBBM model=new EDRS.Model.XT_DM_AJLBBM();
			if (row != null)
			{
                if (row["YWBM"] != null)
				{
                    model.YWBM = row["YWBM"].ToString();
				}
                if (row["AJLBBM"] != null)
				{
                    model.AJLBBM = row["AJLBBM"].ToString();
				}
                if (row["AJLBMC"] != null)
				{
                    model.AJLBMC = row["AJLBMC"].ToString();
				}
                if (row["AJSLJC"] != null)
				{
                    model.AJSLJC = row["AJSLJC"].ToString();
				}
                if (row["SFSC"] != null && row["SFSC"].ToString() != "")
				{
                    model.SFSC = row["SFSC"].ToString();
				}
                if (row["XH"] != null && row["XH"].ToString() != "")
				{
                    model.XH = int.Parse(row["XH"].ToString());
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
            strSql.Append("select YWBM,AJLBBM,AJLBMC,AJSLJC,SFSC,XH ");
			strSql.Append(" FROM XT_DM_AJLBBM ");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetList(string strWhere, params object[] objValues)", "EDRS.OracleDAL.XT_DM_AJLBBM", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
        public int GetRecordCount(string strWhere, params object[] objValues)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM XT_DM_AJLBBM ");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public int GetRecordCount(string strWhere, params object[] objValues)", "EDRS.OracleDAL.XT_DM_AJLBBM", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
                strSql.Append("order by T.ajlbbm desc");
			}
            strSql.Append(")AS Ro, T.*  from (select XT_DM_AJLBBM.*,xt_dm_ywbm.ywmc from XT_DM_AJLBBM left join xt_dm_ywbm on XT_DM_AJLBBM.YWBM = xt_dm_ywbm.ywbm) T ");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)", "EDRS.OracleDAL.XT_DM_AJLBBM", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
			parameters[0].Value = "XT_DM_AJLBBM";
			parameters[1].Value = "DEVSN";
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

