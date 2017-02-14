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
	/// 数据访问类:XT_QX_ANDY
	/// </summary>
	public partial class XT_QX_ANDY:IXT_QX_ANDY
    {
        public HttpRequest context = null;//客户端对象，用于记录日志，客户端信息
        public void SetHttpContext(System.Web.HttpRequest _context)
        {
            this.context = _context;
        }
		public XT_QX_ANDY()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string ANBM)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from XT_QX_ANDY");
			strSql.Append(" where ANBM=:ANBM ");
			OracleParameter[] parameters = {
					new OracleParameter(":ANBM", OracleType.VarChar,50)			};
			parameters[0].Value = ANBM;

			return DbHelperOra.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EDRS.Model.XT_QX_ANDY model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into XT_QX_ANDY(");
            strSql.Append("ANBH,YMMC,ANMC)");
			strSql.Append(" values (");
            strSql.Append(":ANBH,:YMMC,:ANMC)");
			OracleParameter[] parameters = {					
					new OracleParameter(":ANBH", OracleType.VarChar,50),
					new OracleParameter(":YMMC", OracleType.VarChar,200),
                    new OracleParameter(":ANMC", OracleType.VarChar,100)};
			parameters[0].Value = model.ANBH;
            parameters[1].Value = model.YMMC;
            parameters[2].Value = model.ANMC;

			int rows=DbHelperOra.ExecuteSql(strSql.ToString(),parameters);
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
		public bool Update(EDRS.Model.XT_QX_ANDY model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update XT_QX_ANDY set ");		
			strSql.Append("ANBH=:ANBH,");
            strSql.Append("YMMC=:YMMC,");
            strSql.Append("ANMC=:ANMC");
			strSql.Append(" where ANBM=:ANBM ");
			OracleParameter[] parameters = {
					new OracleParameter(":ANBM", OracleType.VarChar,50),
					new OracleParameter(":ANBH", OracleType.VarChar,50),
					new OracleParameter(":YMMC", OracleType.VarChar,200),
                    new OracleParameter(":ANMC", OracleType.VarChar,100)};
			parameters[0].Value = model.ANBM;
			parameters[1].Value = model.ANBH;
            parameters[2].Value = model.YMMC;
            parameters[3].Value = model.ANMC;

			int rows=DbHelperOra.ExecuteSql(strSql.ToString(),parameters);
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
		public bool Delete(string ANBM)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from XT_QX_ANDY ");
			strSql.Append(" where ANBM=:ANBM ");
			OracleParameter[] parameters = {
					new OracleParameter(":ANBM", OracleType.VarChar,50)			};
			parameters[0].Value = ANBM;

			int rows=DbHelperOra.ExecuteSql(strSql.ToString(),parameters);
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
		public bool DeleteList(string ANBMlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from XT_QX_ANDY ");
			strSql.Append(" where ANBM in ("+ANBMlist + ")  ");
			int rows=DbHelperOra.ExecuteSql(strSql.ToString());
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
		public EDRS.Model.XT_QX_ANDY GetModel(string ANBM)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select ANBM,ANBH,YMMC,ANMC from XT_QX_ANDY ");
			strSql.Append(" where ANBM=:ANBM ");
			OracleParameter[] parameters = {
					new OracleParameter(":ANBM", OracleType.VarChar,50)			};
			parameters[0].Value = ANBM;

			EDRS.Model.XT_QX_ANDY model=new EDRS.Model.XT_QX_ANDY();
			DataSet ds=DbHelperOra.Query(strSql.ToString(),parameters);
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
		public EDRS.Model.XT_QX_ANDY DataRowToModel(DataRow row)
		{
			EDRS.Model.XT_QX_ANDY model=new EDRS.Model.XT_QX_ANDY();
			if (row != null)
			{
				if(row["ANBM"]!=null)
				{
					model.ANBM=row["ANBM"].ToString();
				}
				if(row["ANBH"]!=null)
				{
					model.ANBH=row["ANBH"].ToString();
				}
				if(row["YMMC"]!=null)
				{
					model.YMMC=row["YMMC"].ToString();
				}
                if (row["ANMC"] != null)
                {
                    model.ANMC = row["ANMC"].ToString();
                }
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select ANBM,ANBH,YMMC,ANMC ");
			strSql.Append(" FROM XT_QX_ANDY ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperOra.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
        public int GetRecordCount(string strWhere, params object[] objValues)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM XT_QX_ANDY ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where 1=1 "+strWhere);
			}
			object obj = DbHelperOra.GetSingle(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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

            string table = string.Format("(select * from XT_QX_ANDY a left join xt_qx_gndy b on a.ymmc = b.gnbm)");

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.ANBM desc");
            }
            strSql.AppendFormat(")AS Ro, T.*  from {0} T ", table);
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)", "EDRS.OracleDAL.XT_QX_ANDY", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
			parameters[0].Value = "XT_QX_ANDY";
			parameters[1].Value = "ANBM";
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

