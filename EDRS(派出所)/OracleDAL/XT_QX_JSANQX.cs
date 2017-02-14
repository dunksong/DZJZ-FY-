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
	/// 数据访问类:XT_QX_JSANQX
	/// </summary>
	public partial class XT_QX_JSANQX:IXT_QX_JSANQX
	{
        public HttpRequest context = null;//客户端对象，用于记录日志，客户端信息
        public void SetHttpContext(System.Web.HttpRequest _context)
        {
            this.context = _context;
        }
		
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string QXBM)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from XT_QX_JSANQX");
			strSql.Append(" where QXBM=:QXBM ");
			OracleParameter[] parameters = {
					new OracleParameter(":QXBM", OracleType.VarChar,50)			};
			parameters[0].Value = QXBM;

			return DbHelperOra.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EDRS.Model.XT_QX_JSANQX model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into XT_QX_JSANQX(");
			strSql.Append("ANBH,DWBM,JSBM,BMBM)");
			strSql.Append(" values (");
			strSql.Append(":ANBH,:DWBM,:JSBM,:BMBM)");
			OracleParameter[] parameters = {				
					new OracleParameter(":ANBH", OracleType.VarChar,50),
					new OracleParameter(":DWBM", OracleType.VarChar,50),
					new OracleParameter(":JSBM", OracleType.Char,3),
					new OracleParameter(":BMBM", OracleType.Char,4)};

			parameters[0].Value = model.ANBH;
			parameters[1].Value = model.DWBM;
			parameters[2].Value = model.JSBM;
			parameters[3].Value = model.BMBM;

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
		public bool Update(EDRS.Model.XT_QX_JSANQX model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update XT_QX_JSANQX set ");
			strSql.Append("QXBM=:QXBM,");
			strSql.Append("ANBH=:ANBH,");
			strSql.Append("DWBM=:DWBM,");
			strSql.Append("JSBM=:JSBM,");
			strSql.Append("BMBM=:BMBM");
			strSql.Append(" where QXBM=:QXBM ");
			OracleParameter[] parameters = {
					new OracleParameter(":QXBM", OracleType.VarChar,50),
					new OracleParameter(":ANBH", OracleType.VarChar,50),
					new OracleParameter(":DWBM", OracleType.VarChar,50),
					new OracleParameter(":JSBM", OracleType.Char,3),
					new OracleParameter(":BMBM", OracleType.Char,4)};
			parameters[0].Value = model.QXBM;
			parameters[1].Value = model.ANBH;
			parameters[2].Value = model.DWBM;
			parameters[3].Value = model.JSBM;
			parameters[4].Value = model.BMBM;

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
		public bool Delete(string QXBM)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from XT_QX_JSANQX ");
			strSql.Append(" where QXBM=:QXBM ");
			OracleParameter[] parameters = {
					new OracleParameter(":QXBM", OracleType.VarChar,50)			};
			parameters[0].Value = QXBM;

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
		public bool DeleteList(string QXBMlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from XT_QX_JSANQX ");
			strSql.Append(" where QXBM in ("+QXBMlist + ")  ");
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
		public EDRS.Model.XT_QX_JSANQX GetModel(string QXBM)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select QXBM,ANBH,DWBM,JSBM,BMBM from XT_QX_JSANQX ");
			strSql.Append(" where QXBM=:QXBM ");
			OracleParameter[] parameters = {
					new OracleParameter(":QXBM", OracleType.VarChar,50)			};
			parameters[0].Value = QXBM;

			EDRS.Model.XT_QX_JSANQX model=new EDRS.Model.XT_QX_JSANQX();
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
		public EDRS.Model.XT_QX_JSANQX DataRowToModel(DataRow row)
		{
			EDRS.Model.XT_QX_JSANQX model=new EDRS.Model.XT_QX_JSANQX();
			if (row != null)
			{
				if(row["QXBM"]!=null)
				{
					model.QXBM=row["QXBM"].ToString();
				}
				if(row["ANBH"]!=null)
				{
					model.ANBH=row["ANBH"].ToString();
				}
				if(row["DWBM"]!=null)
				{
					model.DWBM=row["DWBM"].ToString();
				}
				if(row["JSBM"]!=null)
				{
					model.JSBM=row["JSBM"].ToString();
				}
				if(row["BMBM"]!=null)
				{
					model.BMBM=row["BMBM"].ToString();
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
			strSql.Append("select QXBM,ANBH,DWBM,JSBM,BMBM ");
			strSql.Append(" FROM XT_QX_JSANQX ");
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
			strSql.Append("select count(1) FROM XT_QX_JSANQX ");
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
            string table = string.Format(@"(select a.qxbm,a.dwbm,a.jsbm,a.bmbm,b.*,c.gnbm,c.gnmc,c.gnxh from XT_QX_JSANQX a 
left join XT_QX_ANDY b on a.anbh = b.anbm left join xt_qx_gndy c on b.ymmc = c.gnbm )");

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.QXBM desc");
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
			parameters[0].Value = "XT_QX_JSANQX";
			parameters[1].Value = "QXBM";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperOra.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod
        /// <summary>
        /// 根据单位部门角色编码获取按钮权限列表
        /// </summary>
        /// <param name="dwbm"></param>
        /// <param name="bmbm"></param>
        /// <param name="jsbms"></param>
        /// <returns></returns>
        public DataSet GetAnQxListByUser(string dwbm,string bmbms,string jsbms)
        { 
            //select a.*,b.qxbm from xt_qx_andy a left join xt_qx_jsanqx b on a.anbm = b.anbh and DWBM='440300000000' and BMBM='0000' and JSBM='000'
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.*,c.gnbm,c.gnmc,c.gnct,b.qxbm from xt_qx_andy a left join xt_qx_gndy c on a.ymmc = c.gnbm left join xt_qx_jsanqx b on a.anbm = b.anbh");
            if (!string.IsNullOrEmpty(dwbm))
                strSql.AppendFormat(" and b.dwbm='{0}'",dwbm);
            if (!string.IsNullOrEmpty(bmbms))
                strSql.AppendFormat(" and bmbm in ({0})", bmbms);
            if (!string.IsNullOrEmpty(jsbms))
                strSql.AppendFormat(" and jsbm in ({0})", jsbms);
            return DbHelperOra.Query(strSql.ToString());
        }

		#endregion  ExtensionMethod
	}
}

