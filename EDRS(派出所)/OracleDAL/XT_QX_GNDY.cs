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
	/// 数据访问类:XT_QX_GNDY
	/// </summary>
	public partial class XT_QX_GNDY:IXT_QX_GNDY
    {
        public System.Web.HttpRequest context = null;
        public void SetHttpContext(System.Web.HttpRequest _context)
        {
            this.context = _context;
        }
		public XT_QX_GNDY()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string GNBM,string DWBM)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from XT_QX_GNDY");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
			strSql.Append(" where GNBM=:GNBM and DWBM=:DWBM ");
			OracleParameter[] parameters = {
					new OracleParameter(":GNBM", OracleType.VarChar,50),
					new OracleParameter(":DWBM", OracleType.VarChar,50)			};
			parameters[0].Value = GNBM;
			parameters[1].Value = DWBM;

            try
            {
                return DbHelperOra.Exists(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Exists(string GNBM,string DWBM)", "EDRS.OracleDAL.XT_QX_GNDY", strSql.ToString(), parameters);
            }
            return false;
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EDRS.Model.XT_QX_GNDY model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into XT_QX_GNDY(");
			strSql.Append("GNBM,GNMC,FLBM,GNCXJ,GNCT,GNSM,GNXH,GNXSMC,CSCS,GNCS,DWBM,SFMTCK,SFSC)");
			strSql.Append(" values (");
			strSql.Append(":GNBM,:GNMC,:FLBM,:GNCXJ,:GNCT,:GNSM,:GNXH,:GNXSMC,:CSCS,:GNCS,:DWBM,:SFMTCK,:SFSC)");
			OracleParameter[] parameters = {
					new OracleParameter(":GNBM", OracleType.VarChar,50),
					new OracleParameter(":GNMC", OracleType.VarChar,300),
					new OracleParameter(":FLBM", OracleType.VarChar,50),
					new OracleParameter(":GNCXJ", OracleType.VarChar,1000),
					new OracleParameter(":GNCT", OracleType.VarChar,1000),
					new OracleParameter(":GNSM", OracleType.VarChar,600),
					new OracleParameter(":GNXH", OracleType.Number,6),
					new OracleParameter(":GNXSMC", OracleType.VarChar,300),
					new OracleParameter(":CSCS", OracleType.VarChar,50),
					new OracleParameter(":GNCS", OracleType.Clob,4000),
					new OracleParameter(":DWBM", OracleType.VarChar,50),
					new OracleParameter(":SFMTCK", OracleType.Char,1),
					new OracleParameter(":SFSC", OracleType.Char,1)};
			parameters[0].Value = model.GNBM;
			parameters[1].Value = model.GNMC;
			parameters[2].Value = model.FLBM;
			parameters[3].Value = model.GNCXJ;
			parameters[4].Value = model.GNCT;
			parameters[5].Value = model.GNSM;
			parameters[6].Value = model.GNXH;
			parameters[7].Value = model.GNXSMC;
			parameters[8].Value = model.CSCS;
		    parameters[9].Value = string.IsNullOrWhiteSpace(model.GNCS) ? " " : model.GNCS;
			parameters[10].Value = model.DWBM;
			parameters[11].Value = model.SFMTCK;
			parameters[12].Value = model.SFSC;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Add(EDRS.Model.XT_QX_GNDY model)", "EDRS.OracleDAL.XT_QX_GNDY", strSql.ToString(), parameters);
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
		public bool Update(EDRS.Model.XT_QX_GNDY model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update XT_QX_GNDY set ");
			strSql.Append("GNMC=:GNMC,");
			strSql.Append("FLBM=:FLBM,");
			strSql.Append("GNCXJ=:GNCXJ,");
			strSql.Append("GNCT=:GNCT,");
			strSql.Append("GNSM=:GNSM,");
			strSql.Append("GNXH=:GNXH,");
			strSql.Append("GNXSMC=:GNXSMC,");
			strSql.Append("CSCS=:CSCS,");
			strSql.Append("GNCS=:GNCS,");
			strSql.Append("SFMTCK=:SFMTCK,");
			strSql.Append("SFSC=:SFSC");
			strSql.Append(" where GNBM=:GNBM and DWBM=:DWBM ");
			OracleParameter[] parameters = {
					new OracleParameter(":GNMC", OracleType.VarChar,300),
					new OracleParameter(":FLBM", OracleType.VarChar,50),
					new OracleParameter(":GNCXJ", OracleType.VarChar,1000),
					new OracleParameter(":GNCT", OracleType.VarChar,1000),
					new OracleParameter(":GNSM", OracleType.VarChar,600),
					new OracleParameter(":GNXH", OracleType.Number,6),
					new OracleParameter(":GNXSMC", OracleType.VarChar,300),
					new OracleParameter(":CSCS", OracleType.VarChar,50),
					new OracleParameter(":GNCS", OracleType.Clob,4000),
					new OracleParameter(":SFMTCK", OracleType.Char,1),
					new OracleParameter(":SFSC", OracleType.Char,1),
					new OracleParameter(":GNBM", OracleType.VarChar,50),
					new OracleParameter(":DWBM", OracleType.VarChar,50)};
			parameters[0].Value = model.GNMC;
			parameters[1].Value = model.FLBM;
			parameters[2].Value = model.GNCXJ;
			parameters[3].Value = model.GNCT;
			parameters[4].Value = model.GNSM;
			parameters[5].Value = model.GNXH;
			parameters[6].Value = model.GNXSMC;
			parameters[7].Value = model.CSCS;
            parameters[8].Value = string.IsNullOrWhiteSpace(model.GNCS) ? " " : model.GNCS;
			parameters[9].Value = model.SFMTCK;
			parameters[10].Value = model.SFSC;
			parameters[11].Value = model.GNBM;
			parameters[12].Value = model.DWBM;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Update(EDRS.Model.XT_QX_GNDY model)", "EDRS.OracleDAL.XT_QX_GNDY", strSql.ToString(), parameters);
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
		public bool Delete(string GNBM,string DWBM)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from XT_QX_GNDY ");
			strSql.Append(" where GNBM=:GNBM and DWBM=:DWBM ");
			OracleParameter[] parameters = {
					new OracleParameter(":GNBM", OracleType.VarChar,50),
					new OracleParameter(":DWBM", OracleType.VarChar,50)			};
			parameters[0].Value = GNBM;
			parameters[1].Value = DWBM;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Delete(string GNBM,string DWBM)", "EDRS.OracleDAL.XT_QX_GNDY", strSql.ToString(), parameters);
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
		public EDRS.Model.XT_QX_GNDY GetModel(string GNBM,string DWBM)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select GNBM,GNMC,FLBM,GNCXJ,GNCT,GNSM,GNXH,GNXSMC,CSCS,DWBM,SFMTCK,SFSC from XT_QX_GNDY ");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
            strSql.Append(" where GNBM=:GNBM and DWBM=:DWBM ");
			OracleParameter[] parameters = {
					new OracleParameter(":GNBM", OracleType.VarChar,50),
					new OracleParameter(":DWBM", OracleType.VarChar,50)			};
			parameters[0].Value = GNBM;
			parameters[1].Value = DWBM;

			EDRS.Model.XT_QX_GNDY model=new EDRS.Model.XT_QX_GNDY();
            DataSet ds = null;
            try
            {
                ds = DbHelperOra.Query(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public EDRS.Model.XT_QX_GNDY GetModel(string GNBM,string DWBM)", "EDRS.OracleDAL.XT_QX_GNDY", strSql.ToString(), parameters);
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
		public EDRS.Model.XT_QX_GNDY DataRowToModel(DataRow row)
		{
			EDRS.Model.XT_QX_GNDY model=new EDRS.Model.XT_QX_GNDY();
			if (row != null)
			{
				if(row["GNBM"]!=null)
				{
					model.GNBM=row["GNBM"].ToString();
				}
				if(row["GNMC"]!=null)
				{
					model.GNMC=row["GNMC"].ToString();
				}
				if(row["FLBM"]!=null)
				{
					model.FLBM=row["FLBM"].ToString();
				}
				if(row["GNCXJ"]!=null)
				{
					model.GNCXJ=row["GNCXJ"].ToString();
				}
				if(row["GNCT"]!=null)
				{
					model.GNCT=row["GNCT"].ToString();
				}
				if(row["GNSM"]!=null)
				{
					model.GNSM=row["GNSM"].ToString();
				}
				if(row["GNXH"]!=null && row["GNXH"].ToString()!="")
				{
					model.GNXH=decimal.Parse(row["GNXH"].ToString());
				}
				if(row["GNXSMC"]!=null)
				{
					model.GNXSMC=row["GNXSMC"].ToString();
				}
				if(row["CSCS"]!=null)
				{
					model.CSCS=row["CSCS"].ToString();
				}
					//model.GNCS=row["GNCS"].ToString();
				if(row["DWBM"]!=null)
				{
					model.DWBM=row["DWBM"].ToString();
				}
				if(row["SFMTCK"]!=null)
				{
					model.SFMTCK=row["SFMTCK"].ToString();
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
			strSql.Append("select GNBM,GNMC,FLBM,GNCXJ,GNCT,GNSM,GNXH,GNXSMC,CSCS,DWBM,SFMTCK,SFSC ");
			strSql.Append(" FROM XT_QX_GNDY ");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetList(string strWhere, params object[] objValues)", "EDRS.OracleDAL.XT_QX_GNDY", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;

		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
        public int GetRecordCount(string strWhere, params object[] objValues)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM XT_QX_GNDY ");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public int GetRecordCount(string strWhere, params object[] objValues)", "EDRS.OracleDAL.XT_QX_GNDY", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
            strSql.AppendFormat(")AS Ro, T.GNBM,T.GNMC,T.FLBM,T.GNCXJ,T.GNCT,T.GNSM,T.GNXH,T.GNXSMC,T.CSCS,T.DWBM,T.SFMTCK,T.SFSC from XT_QX_GNDY{0} T ", ConfigHelper.GetConfigString("OrcDBLinq"));
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)", "EDRS.OracleDAL.XT_QX_GNDY", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
			parameters[0].Value = "XT_QX_GNDY";
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

