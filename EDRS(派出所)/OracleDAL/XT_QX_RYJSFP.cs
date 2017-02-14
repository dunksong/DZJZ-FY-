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
	/// 数据访问类:XT_QX_RYJSFP
	/// </summary>
	public partial class XT_QX_RYJSFP:IXT_QX_RYJSFP
    {
        public System.Web.HttpRequest context = null;
        public void SetHttpContext(System.Web.HttpRequest _context)
        {
            this.context = _context;
        }
		public XT_QX_RYJSFP()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string DWBM,string BMBM,string JSBM,string GH)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from XT_QX_RYJSFP");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
			strSql.Append(" where DWBM=:DWBM and BMBM=:BMBM and JSBM=:JSBM and GH=:GH ");
			OracleParameter[] parameters = {
					new OracleParameter(":DWBM", OracleType.VarChar,50),
					new OracleParameter(":BMBM", OracleType.Char,4),
					new OracleParameter(":JSBM", OracleType.Char,3),
					new OracleParameter(":GH", OracleType.Char,4)			};
			parameters[0].Value = DWBM;
			parameters[1].Value = BMBM;
			parameters[2].Value = JSBM;
			parameters[3].Value = GH;

            try
            {
                return DbHelperOra.Exists(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Exists(string DWBM,string BMBM,string JSBM,string GH)", "EDRS.OracleDAL.XT_QX_RYJSFP", strSql.ToString(), parameters);
            }
            return false;
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EDRS.Model.XT_QX_RYJSFP model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into XT_QX_RYJSFP(");
			strSql.Append("DWBM,BMBM,JSBM,GH,ZJLDGH)");
			strSql.Append(" values (");
			strSql.Append(":DWBM,:BMBM,:JSBM,:GH,:ZJLDGH)");
			OracleParameter[] parameters = {
					new OracleParameter(":DWBM", OracleType.VarChar,50),
					new OracleParameter(":BMBM", OracleType.Char,4),
					new OracleParameter(":JSBM", OracleType.Char,3),
					new OracleParameter(":GH", OracleType.Char,4),
					new OracleParameter(":ZJLDGH", OracleType.Char,4)};
			parameters[0].Value = model.DWBM;
			parameters[1].Value = model.BMBM;
			parameters[2].Value = model.JSBM;
			parameters[3].Value = model.GH;
			parameters[4].Value = model.ZJLDGH;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Add(EDRS.Model.XT_QX_RYJSFP model)", "EDRS.OracleDAL.XT_QX_RYJSFP", strSql.ToString(), parameters);
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
		public bool Update(EDRS.Model.XT_QX_RYJSFP model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update XT_QX_RYJSFP set ");
			strSql.Append("ZJLDGH=:ZJLDGH");
			strSql.Append(" where DWBM=:DWBM and BMBM=:BMBM and JSBM=:JSBM and GH=:GH ");
			OracleParameter[] parameters = {
					new OracleParameter(":ZJLDGH", OracleType.Char,4),
					new OracleParameter(":DWBM", OracleType.VarChar,50),
					new OracleParameter(":BMBM", OracleType.Char,4),
					new OracleParameter(":JSBM", OracleType.Char,3),
					new OracleParameter(":GH", OracleType.Char,4)};
			parameters[0].Value = model.ZJLDGH;
			parameters[1].Value = model.DWBM;
			parameters[2].Value = model.BMBM;
			parameters[3].Value = model.JSBM;
			parameters[4].Value = model.GH;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Update(EDRS.Model.XT_QX_RYJSFP model)", "EDRS.OracleDAL.XT_QX_RYJSFP", strSql.ToString(), parameters);
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
		public bool Delete(string DWBM,string BMBM,string JSBM,string GH)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from XT_QX_RYJSFP ");
			strSql.Append(" where DWBM=:DWBM and BMBM=:BMBM and JSBM=:JSBM and GH=:GH ");
			OracleParameter[] parameters = {
					new OracleParameter(":DWBM", OracleType.VarChar,50),
					new OracleParameter(":BMBM", OracleType.Char,4),
					new OracleParameter(":JSBM", OracleType.Char,3),
					new OracleParameter(":GH", OracleType.Char,4)			};
			parameters[0].Value = DWBM;
			parameters[1].Value = BMBM;
			parameters[2].Value = JSBM;
			parameters[3].Value = GH;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Delete(string DWBM,string BMBM,string JSBM,string GH)", "EDRS.OracleDAL.XT_QX_RYJSFP", strSql.ToString(), parameters);
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
		public EDRS.Model.XT_QX_RYJSFP GetModel(string DWBM,string BMBM,string JSBM,string GH)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select DWBM,BMBM,JSBM,GH,ZJLDGH from XT_QX_RYJSFP ");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
			strSql.Append(" where DWBM=:DWBM and BMBM=:BMBM and JSBM=:JSBM and GH=:GH ");
			OracleParameter[] parameters = {
					new OracleParameter(":DWBM", OracleType.VarChar,50),
					new OracleParameter(":BMBM", OracleType.Char,4),
					new OracleParameter(":JSBM", OracleType.Char,3),
					new OracleParameter(":GH", OracleType.Char,4)			};
			parameters[0].Value = DWBM;
			parameters[1].Value = BMBM;
			parameters[2].Value = JSBM;
			parameters[3].Value = GH;

			EDRS.Model.XT_QX_RYJSFP model=new EDRS.Model.XT_QX_RYJSFP();
            DataSet ds = null;
            try
            {
                ds = DbHelperOra.Query(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public EDRS.Model.XT_QX_RYJSFP GetModel(string DWBM,string BMBM,string JSBM,string GH)", "EDRS.OracleDAL.XT_QX_RYJSFP", strSql.ToString(), parameters);
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
		public EDRS.Model.XT_QX_RYJSFP DataRowToModel(DataRow row)
		{
			EDRS.Model.XT_QX_RYJSFP model=new EDRS.Model.XT_QX_RYJSFP();
			if (row != null)
			{
				if(row["DWBM"]!=null)
				{
					model.DWBM=row["DWBM"].ToString();
				}
				if(row["BMBM"]!=null)
				{
					model.BMBM=row["BMBM"].ToString();
				}
				if(row["JSBM"]!=null)
				{
					model.JSBM=row["JSBM"].ToString();
				}
				if(row["GH"]!=null)
				{
					model.GH=row["GH"].ToString();
				}
				if(row["ZJLDGH"]!=null)
				{
					model.ZJLDGH=row["ZJLDGH"].ToString();
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
			strSql.Append("select DWBM,BMBM,JSBM,GH,ZJLDGH ");
			strSql.Append(" FROM XT_QX_RYJSFP ");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetList(string strWhere, params object[] objValues)", "EDRS.OracleDAL.XT_QX_RYJSFP", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;

		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
        public int GetRecordCount(string strWhere, params object[] objValues)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM XT_QX_RYJSFP ");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public int GetRecordCount(string strWhere, params object[] objValues)", "EDRS.OracleDAL.XT_QX_RYJSFP", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
				strSql.Append("order by T.GH desc");
			}
			strSql.AppendFormat(")AS Ro, T.*  from XT_QX_RYJSFP{0} T ",ConfigHelper.GetConfigString("OrcDBLinq"));
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)", "EDRS.OracleDAL.XT_QX_RYJSFP", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
			parameters[0].Value = "XT_QX_RYJSFP";
			parameters[1].Value = "GH";
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

