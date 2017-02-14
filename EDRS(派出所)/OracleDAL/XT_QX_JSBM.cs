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
	/// 数据访问类:XT_QX_JSBM
    /// 修改主键把名称也加上问题
	/// </summary>
	public partial class XT_QX_JSBM:IXT_QX_JSBM
    {
        public System.Web.HttpRequest context = null;
        public void SetHttpContext(System.Web.HttpRequest _context)
        {
            this.context = _context;
        }
		public XT_QX_JSBM()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
        public bool Exists(string JSBM, string DWBM, string BMBM)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from XT_QX_JSBM");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
            strSql.Append(" where JSBM=:JSBM and DWBM=:DWBM and BMBM=:BMBM ");
			OracleParameter[] parameters = {
					new OracleParameter(":JSBM", OracleType.Char,3),
					new OracleParameter(":DWBM", OracleType.VarChar,50),
					new OracleParameter(":BMBM", OracleType.Char,4)			};
			parameters[0].Value = JSBM;
            parameters[1].Value = DWBM;
            parameters[2].Value = BMBM;

            try
            {
                return DbHelperOra.Exists(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, " public bool Exists(string JSBM, string DWBM, string BMBM)", "EDRS.OracleDAL.XT_QX_JSBM", strSql.ToString(), parameters);
            }
            return false;
            
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EDRS.Model.XT_QX_JSBM model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into XT_QX_JSBM(");
			strSql.Append("JSBM,DWBM,BMBM,JSMC,JSXH,SPJSBM)");
			strSql.Append(" values (");
			strSql.Append(":JSBM,:DWBM,:BMBM,:JSMC,:JSXH,:SPJSBM)");
			OracleParameter[] parameters = {
					new OracleParameter(":JSBM", OracleType.Char,3),
					new OracleParameter(":DWBM", OracleType.VarChar,50),
					new OracleParameter(":BMBM", OracleType.Char,4),
					new OracleParameter(":JSMC", OracleType.VarChar,60),
					new OracleParameter(":JSXH", OracleType.Number,4),
					new OracleParameter(":SPJSBM", OracleType.Char,2)};
			parameters[0].Value = model.JSBM;
			parameters[1].Value = model.DWBM;
			parameters[2].Value = model.BMBM;
			parameters[3].Value = model.JSMC;
			parameters[4].Value = model.JSXH;
			parameters[5].Value = model.SPJSBM;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, " public bool Add(EDRS.Model.XT_QX_JSBM model)", "EDRS.OracleDAL.XT_QX_JSBM", strSql.ToString(), parameters);
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
		public bool Update(EDRS.Model.XT_QX_JSBM model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update XT_QX_JSBM set ");
            strSql.Append("JSMC=:JSMC,");
            strSql.Append("JSXH=:JSXH,");
            strSql.Append("SPJSBM=:SPJSBM");
            strSql.Append(" where JSBM=:JSBM and DWBM=:DWBM and BMBM=:BMBM");
			OracleParameter[] parameters = {
					new OracleParameter(":DWBM", OracleType.VarChar,50),
					new OracleParameter(":BMBM", OracleType.Char,4),
					new OracleParameter(":JSBM", OracleType.Char,3),
					new OracleParameter(":JSMC", OracleType.VarChar,60),
					new OracleParameter(":JSXH", OracleType.Number,4),
					new OracleParameter(":SPJSBM", OracleType.Char,2)};
			parameters[0].Value = model.DWBM;
			parameters[1].Value = model.BMBM;
			parameters[2].Value = model.JSBM;
			parameters[3].Value = model.JSMC;
			parameters[4].Value = model.JSXH;
			parameters[5].Value = model.SPJSBM;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, " public bool Update(EDRS.Model.XT_QX_JSBM model)", "EDRS.OracleDAL.XT_QX_JSBM", strSql.ToString(), parameters);
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
        public bool Delete(string JSBM, string DWBM, string BMBM)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from XT_QX_JSBM ");
            strSql.Append(" where JSBM=:JSBM and DWBM=:DWBM and BMBM=:BMBM ");
            OracleParameter[] parameters = {
					new OracleParameter(":JSBM", OracleType.Char,3),
					new OracleParameter(":DWBM", OracleType.VarChar,50),
					new OracleParameter(":BMBM", OracleType.Char,4)			};
            parameters[0].Value = JSBM;
            parameters[1].Value = DWBM;
            parameters[2].Value = BMBM;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, " public bool Delete(string JSBM, string DWBM, string BMBM)", "EDRS.OracleDAL.XT_QX_JSBM", strSql.ToString(), parameters);
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
        public EDRS.Model.XT_QX_JSBM GetModel(string JSBM, string DWBM, string BMBM)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select JSBM,DWBM,BMBM,JSMC,JSXH,SPJSBM from XT_QX_JSBM ");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
            strSql.Append(" where JSBM=:JSBM and DWBM=:DWBM and BMBM=:BMBM ");
            OracleParameter[] parameters = {
					new OracleParameter(":JSBM", OracleType.Char,3),
					new OracleParameter(":DWBM", OracleType.VarChar,50),
					new OracleParameter(":BMBM", OracleType.Char,4)			};
            parameters[0].Value = JSBM;
            parameters[1].Value = DWBM;
            parameters[2].Value = BMBM;

            EDRS.Model.XT_QX_JSBM model = new EDRS.Model.XT_QX_JSBM();
            DataSet ds = null;
            try
            {
                ds = DbHelperOra.Query(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public EDRS.Model.XT_QX_JSBM GetModel(string JSBM, string DWBM, string BMBM)", "EDRS.OracleDAL.XT_QX_JSBM", strSql.ToString(), parameters);
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
		public EDRS.Model.XT_QX_JSBM DataRowToModel(DataRow row)
		{
			EDRS.Model.XT_QX_JSBM model=new EDRS.Model.XT_QX_JSBM();
			if (row != null)
			{
				if(row["JSBM"]!=null)
				{
					model.JSBM=row["JSBM"].ToString();
                }
                if (row["DWBM"] != null)
                {
                    model.DWBM = row["DWBM"].ToString();
                }
				if(row["BMBM"]!=null)
				{
					model.BMBM=row["BMBM"].ToString();
                }
                if (row["BMMC"] != null)
                {
                    model.BMMC = row["BMMC"].ToString();
                }
				if(row["JSMC"]!=null)
				{
					model.JSMC=row["JSMC"].ToString();
				}
				if(row["JSXH"]!=null && row["JSXH"].ToString()!="")
				{
					model.JSXH=decimal.Parse(row["JSXH"].ToString());
				}
				if(row["SPJSBM"]!=null)
				{
					model.SPJSBM=row["SPJSBM"].ToString();
				}
                if (row["QXZT"] != null)
                {
                    model.QXZT = row["QXZT"].ToString();
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
			strSql.Append("select JSBM,DWBM,BMBM,JSMC,JSXH,SPJSBM ");
			strSql.Append(" FROM XT_QX_JSBM ");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetList(string strWhere, params object[] objValues)", "EDRS.OracleDAL.XT_QX_JSBM", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
        public int GetRecordCount(string strWhere, params object[] objValues)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM XT_QX_JSBM ");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public int GetRecordCount(string strWhere, params object[] objValues)", "EDRS.OracleDAL.XT_QX_JSBM", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
				strSql.Append("order by T.SPJSBM desc");
			}
            strSql.AppendFormat(")AS Ro, T.*  from XT_QX_JSBM{0} T ", ConfigHelper.GetConfigString("OrcDBLinq"));
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)", "EDRS.OracleDAL.XT_QX_JSBM", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
			parameters[0].Value = "XT_QX_JSBM";
			parameters[1].Value = "SPJSBM";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperOra.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		
	}
}

