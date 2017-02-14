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
	/// 数据访问类:XT_QX_JSGNFP
	/// </summary>
	public partial class XT_QX_JSGNFP:IXT_QX_JSGNFP
    {
        public System.Web.HttpRequest context = null;
        public void SetHttpContext(System.Web.HttpRequest _context)
        {
            this.context = _context;
        }
		public XT_QX_JSGNFP()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string DWBM,string JSBM,string GNBM)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from XT_QX_JSGNFP");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
			strSql.Append(" where DWBM=:DWBM and JSBM=:JSBM and GNBM=:GNBM ");
			OracleParameter[] parameters = {
					new OracleParameter(":DWBM", OracleType.VarChar,50),
					new OracleParameter(":JSBM", OracleType.Char,3),
					new OracleParameter(":GNBM", OracleType.VarChar,50)			};
			parameters[0].Value = DWBM;
			parameters[1].Value = JSBM;
			parameters[2].Value = GNBM;
            try
            {
                return DbHelperOra.Exists(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Exists(string DWBM,string JSBM,string GNBM)", "EDRS.OracleDAL.XT_QX_JSGNFP", strSql.ToString(), parameters);
            }
            return false;
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EDRS.Model.XT_QX_JSGNFP model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into XT_QX_JSGNFP(");
			strSql.Append("DWBM,JSBM,GNBM,GNCS,BMBM,BZ)");
			strSql.Append(" values (");
			strSql.Append(":DWBM,:JSBM,:GNBM,:GNCS,:BMBM,:BZ)");
			OracleParameter[] parameters = {
					new OracleParameter(":DWBM", OracleType.VarChar,50),
					new OracleParameter(":JSBM", OracleType.Char,3),
					new OracleParameter(":GNBM", OracleType.VarChar,50),
					new OracleParameter(":GNCS", OracleType.VarChar,600),
					new OracleParameter(":BMBM", OracleType.Char,4),
					new OracleParameter(":BZ", OracleType.VarChar,900)};
			parameters[0].Value = model.DWBM;
			parameters[1].Value = model.JSBM;
			parameters[2].Value = model.GNBM;
			parameters[3].Value = model.GNCS;
			parameters[4].Value = model.BMBM;
			parameters[5].Value = model.BZ;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Add(EDRS.Model.XT_QX_JSGNFP model)", "EDRS.OracleDAL.XT_QX_JSGNFP", strSql.ToString(), parameters);
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
		public bool Update(EDRS.Model.XT_QX_JSGNFP model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update XT_QX_JSGNFP set ");
			strSql.Append("GNCS=:GNCS,");
			strSql.Append("BMBM=:BMBM,");
			strSql.Append("BZ=:BZ");
			strSql.Append(" where DWBM=:DWBM and JSBM=:JSBM and GNBM=:GNBM ");
			OracleParameter[] parameters = {
					new OracleParameter(":GNCS", OracleType.VarChar,600),
					new OracleParameter(":BMBM", OracleType.Char,4),
					new OracleParameter(":BZ", OracleType.VarChar,900),
					new OracleParameter(":DWBM", OracleType.VarChar,50),
					new OracleParameter(":JSBM", OracleType.Char,3),
					new OracleParameter(":GNBM", OracleType.VarChar,50)};
			parameters[0].Value = model.GNCS;
			parameters[1].Value = model.BMBM;
			parameters[2].Value = model.BZ;
			parameters[3].Value = model.DWBM;
			parameters[4].Value = model.JSBM;
			parameters[5].Value = model.GNBM;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Update(EDRS.Model.XT_QX_JSGNFP model)", "EDRS.OracleDAL.XT_QX_JSGNFP", strSql.ToString(), parameters);
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
		public bool Delete(string DWBM,string JSBM,string GNBM)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from XT_QX_JSGNFP ");
			strSql.Append(" where DWBM=:DWBM and JSBM=:JSBM and GNBM=:GNBM ");
			OracleParameter[] parameters = {
					new OracleParameter(":DWBM", OracleType.VarChar,50),
					new OracleParameter(":JSBM", OracleType.Char,3),
					new OracleParameter(":GNBM", OracleType.VarChar,50)			};
			parameters[0].Value = DWBM;
			parameters[1].Value = JSBM;
			parameters[2].Value = GNBM;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Delete(string DWBM,string JSBM,string GNBM)", "EDRS.OracleDAL.XT_QX_JSGNFP", strSql.ToString(), parameters);
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
        public EDRS.Model.XT_QX_JSGNFP GetModel(string DWBM, string JSBM, string GNBM)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DWBM,JSBM,GNBM,GNCS,BMBM,BZ from XT_QX_JSGNFP ");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
            strSql.Append(" where DWBM=:DWBM and JSBM=:JSBM and GNBM=:GNBM ");
            OracleParameter[] parameters = {
					new OracleParameter(":DWBM", OracleType.VarChar,50),
					new OracleParameter(":JSBM", OracleType.Char,3),
					new OracleParameter(":GNBM", OracleType.VarChar,50)			};
            parameters[0].Value = DWBM;
            parameters[1].Value = JSBM;
            parameters[2].Value = GNBM;

            EDRS.Model.XT_QX_JSGNFP model = new EDRS.Model.XT_QX_JSGNFP();
            DataSet ds = null;
            try
            {
                ds = DbHelperOra.Query(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public EDRS.Model.XT_QX_JSGNFP GetModel(string DWBM,string JSBM,string GNBM)", "EDRS.OracleDAL.XT_QX_JSGNFP", strSql.ToString(), parameters);
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
		public EDRS.Model.XT_QX_JSGNFP DataRowToModel(DataRow row)
		{
			EDRS.Model.XT_QX_JSGNFP model=new EDRS.Model.XT_QX_JSGNFP();
			if (row != null)
			{
				if(row["DWBM"]!=null)
				{
					model.DWBM=row["DWBM"].ToString();
				}
				if(row["JSBM"]!=null)
				{
					model.JSBM=row["JSBM"].ToString();
				}
				if(row["GNBM"]!=null)
				{
					model.GNBM=row["GNBM"].ToString();
				}
				if(row["GNCS"]!=null)
				{
					model.GNCS=row["GNCS"].ToString();
				}
				if(row["BMBM"]!=null)
				{
					model.BMBM=row["BMBM"].ToString();
				}
				if(row["BZ"]!=null)
				{
					model.BZ=row["BZ"].ToString();
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
			strSql.Append("select DWBM,JSBM,GNBM,GNCS,BMBM,BZ ");
			strSql.Append(" FROM XT_QX_JSGNFP ");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetList(string strWhere, params object[] objValues)", "EDRS.OracleDAL.XT_QX_JSGNFP", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
        public int GetRecordCount(string strWhere, params object[] objValues)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM XT_QX_JSGNFP ");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public int GetRecordCount(string strWhere, params object[] objValues)", "EDRS.OracleDAL.XT_QX_JSGNFP", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
				strSql.Append("order by T.GNBM desc");
			}
            strSql.AppendFormat(")AS Ro, T.DWBM,T.JSBM,T.GNBM,T.GNCS,T.BMBM,T.BZ from XT_QX_JSGNFP{0} T ", ConfigHelper.GetConfigString("OrcDBLinq"));
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)", "EDRS.OracleDAL.XT_QX_JSGNFP", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
			parameters[0].Value = "XT_QX_JSGNFP";
			parameters[1].Value = "GNBM";
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

