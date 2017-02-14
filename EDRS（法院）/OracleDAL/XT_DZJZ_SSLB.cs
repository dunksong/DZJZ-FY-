using System;
using System.Data;
using System.Text;
using System.Data.OracleClient;
using EDRS.IDAL;
using Maticsoft.DBUtility;
using System.Web;//Please add references
namespace EDRS.OracleDAL
{
	/// <summary>
	/// 数据访问类:XT_DZJZ_SSLB
	/// </summary>
	public partial class XT_DZJZ_SSLB:IXT_DZJZ_SSLB
	{
        public HttpRequest context = null;//客户端对象，用于记录日志，客户端信息
        public void SetHttpContext(System.Web.HttpRequest _context)
        {
            this.context = _context;
        }

		public XT_DZJZ_SSLB()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
        public bool Exists(string SSLBBM)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from XT_DZJZ_SSLB");
            strSql.Append(" where SSLBBM=:SSLBBM ");
            OracleParameter[] parameters = {
					new OracleParameter(":SSLBBM", OracleType.Char,13)			};
            parameters[0].Value = SSLBBM;
            try
            {
                return DbHelperOra.Exists(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Exists(string SSLBBM)", "EDRS.OracleDAL.XT_DZJZ_SSLB", strSql.ToString(), parameters);
            } 
            return false;
        }


		/// <summary>
		/// 增加一条数据
		/// </summary>
        public bool Add(EDRS.Model.XT_DZJZ_SSLB model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into XT_DZJZ_SSLB(");
            strSql.Append("SSLBBM,FSSLBBM,SSLBLX,SSLBMC,SSLBSX,SSLBSM)");
            strSql.Append(" values (");
            strSql.Append(":SSLBBM,:FSSLBBM,:SSLBLX,:SSLBMC,:SSLBSX,:SSLBSM)");
            OracleParameter[] parameters = {
					new OracleParameter(":SSLBBM", OracleType.Char,13),
					new OracleParameter(":FSSLBBM", OracleType.Char,13),
					new OracleParameter(":SSLBLX", OracleType.Char,1),
					new OracleParameter(":SSLBMC", OracleType.VarChar,300),
					new OracleParameter(":SSLBSX", OracleType.Number,8),
					new OracleParameter(":SSLBSM", OracleType.VarChar,4000)};
            parameters[0].Value = model.SSLBBM;
            parameters[1].Value = model.FSSLBBM;
            parameters[2].Value = model.SSLBLX;
            parameters[3].Value = model.SSLBMC;
            parameters[4].Value = model.SSLBSX;
            parameters[5].Value = model.SSLBSM;
            try
            {
                int rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
                if (rows > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Add(EDRS.Model.XT_DZJZ_SSLB model)", "EDRS.OracleDAL.XT_DZJZ_SSLB", strSql.ToString(), parameters);
            }
            return false;
        }
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(EDRS.Model.XT_DZJZ_SSLB model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update XT_DZJZ_SSLB set ");
			strSql.Append("FSSLBBM=:FSSLBBM,");
			strSql.Append("SSLBLX=:SSLBLX,");
			strSql.Append("SSLBMC=:SSLBMC,");
			strSql.Append("SSLBSX=:SSLBSX,");
			strSql.Append("SSLBSM=:SSLBSM");
			strSql.Append(" where SSLBBM=:SSLBBM ");
			OracleParameter[] parameters = {
					new OracleParameter(":FSSLBBM", OracleType.Char,13),
					new OracleParameter(":SSLBLX", OracleType.Char,1),
					new OracleParameter(":SSLBMC", OracleType.VarChar,300),
					new OracleParameter(":SSLBSX", OracleType.Number,8),
					new OracleParameter(":SSLBSM", OracleType.VarChar,4000),
					new OracleParameter(":SSLBBM", OracleType.Char,13)};
			parameters[0].Value = model.FSSLBBM;
			parameters[1].Value = model.SSLBLX;
			parameters[2].Value = model.SSLBMC;
			parameters[3].Value = model.SSLBSX;
			parameters[4].Value = model.SSLBSM;
			parameters[5].Value = model.SSLBBM;
            try
            {
                int rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
                if (rows > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Update(EDRS.Model.XT_DZJZ_SSLB model)", "EDRS.OracleDAL.XT_DZJZ_SSLB", strSql.ToString(), parameters);
            }
            return false;
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
        public bool Delete(string SSLBBM)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from XT_DZJZ_SSLB ");
            strSql.Append(" where SSLBBM=:SSLBBM ");
            OracleParameter[] parameters = {
					new OracleParameter(":SSLBBM", OracleType.Char,13)			};
            parameters[0].Value = SSLBBM;
            try
            {
                int rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
                if (rows > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Delete(string SSLBBM)", "EDRS.OracleDAL.XT_DZJZ_SSLB", strSql.ToString(), parameters);
            }
            return false;
        }
		/// <summary>
		/// 批量删除数据
		/// </summary>
        public bool DeleteList(string SSLBBMlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from XT_DZJZ_SSLB ");
            strSql.Append(" where SSLBBM in (" + SSLBBMlist + ")  ");
            try
            {
                int rows = DbHelperOra.ExecuteSql(strSql.ToString());
                if (rows > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool DeleteList(SSLBBMlist)", "EDRS.OracleDAL.XT_DZJZ_SSLB", strSql.ToString());
            }
            return false;
        }


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public EDRS.Model.XT_DZJZ_SSLB GetModel(string SSLBBM)
		{			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select SSLBBM,FSSLBBM,SSLBLX,SSLBMC,SSLBSX,SSLBSM from XT_DZJZ_SSLB ");
			strSql.Append(" where SSLBBM=:SSLBBM ");
			OracleParameter[] parameters = {
					new OracleParameter(":SSLBBM", OracleType.Char,13)			};
			parameters[0].Value = SSLBBM;

			EDRS.Model.XT_DZJZ_SSLB model=new EDRS.Model.XT_DZJZ_SSLB();
            try
            {
                DataSet ds = DbHelperOra.Query(strSql.ToString(), parameters);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return DataRowToModel(ds.Tables[0].Rows[0]);
                }
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public EDRS.Model.XT_DZJZ_SSLB GetModel(string SSLBBM)", "EDRS.OracleDAL.XT_DZJZ_SSLB", strSql.ToString());
            }
            return null;
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public EDRS.Model.XT_DZJZ_SSLB DataRowToModel(DataRow row)
		{
			EDRS.Model.XT_DZJZ_SSLB model=new EDRS.Model.XT_DZJZ_SSLB();
			if (row != null)
			{
				if(row["SSLBBM"]!=null)
				{
					model.SSLBBM=row["SSLBBM"].ToString();
				}
				if(row["FSSLBBM"]!=null)
				{
					model.FSSLBBM=row["FSSLBBM"].ToString();
				}
				if(row["SSLBLX"]!=null)
				{
					model.SSLBLX=row["SSLBLX"].ToString();
				}
				if(row["SSLBMC"]!=null)
				{
					model.SSLBMC=row["SSLBMC"].ToString();
				}
				if(row["SSLBSX"]!=null && row["SSLBSX"].ToString()!="")
				{
					model.SSLBSX=decimal.Parse(row["SSLBSX"].ToString());
				}
				if(row["SSLBSM"]!=null)
				{
					model.SSLBSM=row["SSLBSM"].ToString();
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SSLBBM,FSSLBBM,SSLBLX,SSLBMC,SSLBSX,SSLBSM ");
            strSql.Append(" FROM XT_DZJZ_SSLB ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where 1=1 " + strWhere);
            }
            try
            {
                return DbHelperOra.Query(strSql.ToString());
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetList(string strWhere)", "EDRS.OracleDAL.XT_DZJZ_SSLB", strSql.ToString());
            }
            return null;
        }

		/// <summary>
		/// 获取记录总数
		/// </summary>
        public int GetRecordCount(string strWhere, params object[] objValues)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM XT_DZJZ_SSLB ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where 1=1 " + strWhere);
            }
            try
            {
                object obj = DbHelperOra.GetSingle(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
                             
                if (obj != null)
                {
                    return Convert.ToInt32(obj);
                }
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public int GetRecordCount(string strWhere, params object[] objValues)", "EDRS.OracleDAL.XT_DZJZ_SSLB", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return 0;
        }
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.SSLBBM desc");
            }
            strSql.Append(")AS Ro, T.*  from XT_DZJZ_SSLB T ");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)", "EDRS.OracleDAL.XT_DZJZ_SSLB", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
			parameters[0].Value = "XT_DZJZ_SSLB";
			parameters[1].Value = "SSLBBM";
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

