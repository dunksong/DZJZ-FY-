
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
	/// 数据访问类:YX_DZJZ_JZMLWJ
	/// </summary>
	public partial class YX_DZJZ_JZMLWJ:IYX_DZJZ_JZMLWJ
    {
        public System.Web.HttpRequest context = null;
        public void SetHttpContext(System.Web.HttpRequest _context)
        {
            this.context = _context;
        }
		public YX_DZJZ_JZMLWJ()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string JZBH,string MLBH,string WJXH)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from YX_DZJZ_JZMLWJ");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
			strSql.Append(" where JZBH=:JZBH and MLBH=:MLBH and WJXH=:WJXH ");
			OracleParameter[] parameters = {
					new OracleParameter(":JZBH", OracleType.Char,14),
					new OracleParameter(":MLBH", OracleType.VarChar,100),
					new OracleParameter(":WJXH", OracleType.VarChar,100)			};
			parameters[0].Value = JZBH;
			parameters[1].Value = MLBH;
			parameters[2].Value = WJXH;
            try
            {
                return DbHelperOra.Exists(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Exists(string JZBH,string MLBH,string WJXH)", "EDRS.OracleDAL.YX_DZJZ_JZMLWJ", strSql.ToString(), parameters);
            }
            return false;
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EDRS.Model.YX_DZJZ_JZMLWJ model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into YX_DZJZ_JZMLWJ(");
			strSql.Append("JZBH,MLBH,WJXH,SFSC,CJSJ,ZHXGSJ,FQDWBM,FQL,DWBM,BMSAH,WJLJ,WJMC,WJXSMC,WJHZ,WJKSY,WJJSY,WJBZXX,WJYZBZ,WJSXH,WJZDY,SSLBBM,SSLBMC)");
			strSql.Append(" values (");
			strSql.Append(":JZBH,:MLBH,:WJXH,:SFSC,:CJSJ,:ZHXGSJ,:FQDWBM,:FQL,:DWBM,:BMSAH,:WJLJ,:WJMC,:WJXSMC,:WJHZ,:WJKSY,:WJJSY,:WJBZXX,:WJYZBZ,:WJSXH,:WJZDY,:SSLBBM,:SSLBMC)");
			OracleParameter[] parameters = {
					new OracleParameter(":JZBH", OracleType.Char,14),
					new OracleParameter(":MLBH", OracleType.VarChar,100),
					new OracleParameter(":WJXH", OracleType.VarChar,100),
					new OracleParameter(":SFSC", OracleType.Char,1),
					new OracleParameter(":CJSJ", OracleType.DateTime),
					new OracleParameter(":ZHXGSJ", OracleType.DateTime),
					new OracleParameter(":FQDWBM", OracleType.VarChar,50),
					new OracleParameter(":FQL", OracleType.VarChar,6),
					new OracleParameter(":DWBM", OracleType.VarChar,50),
					new OracleParameter(":BMSAH", OracleType.VarChar,100),
					new OracleParameter(":WJLJ", OracleType.VarChar,500),
					new OracleParameter(":WJMC", OracleType.VarChar,300),
					new OracleParameter(":WJXSMC", OracleType.VarChar,300),
					new OracleParameter(":WJHZ", OracleType.VarChar,50),
					new OracleParameter(":WJKSY", OracleType.Number,4),
					new OracleParameter(":WJJSY", OracleType.Number,4),
					new OracleParameter(":WJBZXX", OracleType.VarChar,500),
					new OracleParameter(":WJYZBZ", OracleType.VarChar,1000),
					new OracleParameter(":WJSXH", OracleType.Number,4),
					new OracleParameter(":WJZDY", OracleType.VarChar,1000),
					new OracleParameter(":SSLBBM", OracleType.Char,13),
					new OracleParameter(":SSLBMC", OracleType.VarChar,300)};
			parameters[0].Value = model.JZBH;
			parameters[1].Value = model.MLBH;
			parameters[2].Value = model.WJXH;
			parameters[3].Value = model.SFSC;
			parameters[4].Value = model.CJSJ;
			parameters[5].Value = model.ZHXGSJ;
			parameters[6].Value = model.FQDWBM;
			parameters[7].Value = model.FQL;
			parameters[8].Value = model.DWBM;
			parameters[9].Value = model.BMSAH;
			parameters[10].Value = model.WJLJ;
			parameters[11].Value = model.WJMC;
			parameters[12].Value = model.WJXSMC;
			parameters[13].Value = model.WJHZ;
			parameters[14].Value = model.WJKSY;
			parameters[15].Value = model.WJJSY;
			parameters[16].Value = model.WJBZXX;
			parameters[17].Value = model.WJYZBZ;
			parameters[18].Value = model.WJSXH;
			parameters[19].Value = model.WJZDY;
			parameters[20].Value = model.SSLBBM;
			parameters[21].Value = model.SSLBMC;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Add(EDRS.Model.YX_DZJZ_JZMLWJ model)", "EDRS.OracleDAL.YX_DZJZ_JZMLWJ", strSql.ToString(), parameters);
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
		public bool Update(EDRS.Model.YX_DZJZ_JZMLWJ model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update YX_DZJZ_JZMLWJ set ");
			strSql.Append("SFSC=:SFSC,");
			strSql.Append("CJSJ=:CJSJ,");
			strSql.Append("ZHXGSJ=:ZHXGSJ,");
			strSql.Append("FQDWBM=:FQDWBM,");
			strSql.Append("FQL=:FQL,");
			strSql.Append("DWBM=:DWBM,");
			strSql.Append("BMSAH=:BMSAH,");
			strSql.Append("WJLJ=:WJLJ,");
			strSql.Append("WJMC=:WJMC,");
			strSql.Append("WJXSMC=:WJXSMC,");
			strSql.Append("WJHZ=:WJHZ,");
			strSql.Append("WJKSY=:WJKSY,");
			strSql.Append("WJJSY=:WJJSY,");
			strSql.Append("WJBZXX=:WJBZXX,");
			strSql.Append("WJYZBZ=:WJYZBZ,");
			strSql.Append("WJSXH=:WJSXH,");
			strSql.Append("WJZDY=:WJZDY,");
			strSql.Append("SSLBBM=:SSLBBM,");
			strSql.Append("SSLBMC=:SSLBMC");
			strSql.Append(" where JZBH=:JZBH and MLBH=:MLBH and WJXH=:WJXH ");
			OracleParameter[] parameters = {
					new OracleParameter(":SFSC", OracleType.Char,1),
					new OracleParameter(":CJSJ", OracleType.DateTime),
					new OracleParameter(":ZHXGSJ", OracleType.DateTime),
					new OracleParameter(":FQDWBM", OracleType.VarChar,50),
					new OracleParameter(":FQL", OracleType.VarChar,6),
					new OracleParameter(":DWBM", OracleType.VarChar,50),
					new OracleParameter(":BMSAH", OracleType.VarChar,100),
					new OracleParameter(":WJLJ", OracleType.VarChar,500),
					new OracleParameter(":WJMC", OracleType.VarChar,300),
					new OracleParameter(":WJXSMC", OracleType.VarChar,300),
					new OracleParameter(":WJHZ", OracleType.VarChar,50),
					new OracleParameter(":WJKSY", OracleType.Number,4),
					new OracleParameter(":WJJSY", OracleType.Number,4),
					new OracleParameter(":WJBZXX", OracleType.VarChar,500),
					new OracleParameter(":WJYZBZ", OracleType.VarChar,1000),
					new OracleParameter(":WJSXH", OracleType.Number,4),
					new OracleParameter(":WJZDY", OracleType.VarChar,1000),
					new OracleParameter(":SSLBBM", OracleType.Char,13),
					new OracleParameter(":SSLBMC", OracleType.VarChar,300),
					new OracleParameter(":JZBH", OracleType.Char,14),
					new OracleParameter(":MLBH", OracleType.VarChar,100),
					new OracleParameter(":WJXH", OracleType.VarChar,100)};
			parameters[0].Value = model.SFSC;
			parameters[1].Value = model.CJSJ;
			parameters[2].Value = model.ZHXGSJ;
			parameters[3].Value = model.FQDWBM;
			parameters[4].Value = model.FQL;
			parameters[5].Value = model.DWBM;
			parameters[6].Value = model.BMSAH;
			parameters[7].Value = model.WJLJ;
			parameters[8].Value = model.WJMC;
			parameters[9].Value = model.WJXSMC;
			parameters[10].Value = model.WJHZ;
			parameters[11].Value = model.WJKSY;
			parameters[12].Value = model.WJJSY;
			parameters[13].Value = model.WJBZXX;
			parameters[14].Value = model.WJYZBZ;
			parameters[15].Value = model.WJSXH;
			parameters[16].Value = model.WJZDY;
			parameters[17].Value = model.SSLBBM;
			parameters[18].Value = model.SSLBMC;
			parameters[19].Value = model.JZBH;
			parameters[20].Value = model.MLBH;
			parameters[21].Value = model.WJXH;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Update(EDRS.Model.YX_DZJZ_JZMLWJ model)", "EDRS.OracleDAL.YX_DZJZ_JZMLWJ", strSql.ToString(), parameters);
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
		public bool Delete(string JZBH,string MLBH,string WJXH)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from YX_DZJZ_JZMLWJ ");
			strSql.Append(" where JZBH=:JZBH and MLBH=:MLBH and WJXH=:WJXH ");
			OracleParameter[] parameters = {
					new OracleParameter(":JZBH", OracleType.Char,14),
					new OracleParameter(":MLBH", OracleType.VarChar,100),
					new OracleParameter(":WJXH", OracleType.VarChar,100)			};
			parameters[0].Value = JZBH;
			parameters[1].Value = MLBH;
			parameters[2].Value = WJXH;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Delete(string JZBH,string MLBH,string WJXH)", "EDRS.OracleDAL.YX_DZJZ_JZMLWJ", strSql.ToString(), parameters);
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
		public EDRS.Model.YX_DZJZ_JZMLWJ GetModel(string JZBH,string MLBH,string WJXH)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select JZBH,MLBH,WJXH,SFSC,CJSJ,ZHXGSJ,FQDWBM,FQL,DWBM,BMSAH,WJLJ,WJMC,WJXSMC,WJHZ,WJKSY,WJJSY,WJBZXX,WJYZBZ,WJSXH,WJZDY,SSLBBM,SSLBMC from YX_DZJZ_JZMLWJ ");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
            strSql.Append(" where JZBH=:JZBH and MLBH=:MLBH and WJXH=:WJXH ");
			OracleParameter[] parameters = {
					new OracleParameter(":JZBH", OracleType.Char,14),
					new OracleParameter(":MLBH", OracleType.VarChar,100),
					new OracleParameter(":WJXH", OracleType.VarChar,100)			};
			parameters[0].Value = JZBH;
			parameters[1].Value = MLBH;
			parameters[2].Value = WJXH;

			EDRS.Model.YX_DZJZ_JZMLWJ model=new EDRS.Model.YX_DZJZ_JZMLWJ();
            DataSet ds = null;
            try
            {
                ds = DbHelperOra.Query(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public EDRS.Model.YX_DZJZ_JZMLWJ GetModel(string JZBH,string MLBH,string WJXH)", "EDRS.OracleDAL.YX_DZJZ_JZMLWJ", strSql.ToString(), parameters);
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
		public EDRS.Model.YX_DZJZ_JZMLWJ DataRowToModel(DataRow row)
		{
			EDRS.Model.YX_DZJZ_JZMLWJ model=new EDRS.Model.YX_DZJZ_JZMLWJ();
			if (row != null)
			{
				if(row["JZBH"]!=null)
				{
					model.JZBH=row["JZBH"].ToString();
				}
				if(row["MLBH"]!=null)
				{
					model.MLBH=row["MLBH"].ToString();
				}
				if(row["WJXH"]!=null)
				{
					model.WJXH=row["WJXH"].ToString();
				}
				if(row["SFSC"]!=null)
				{
					model.SFSC=row["SFSC"].ToString();
				}
				if(row["CJSJ"]!=null && row["CJSJ"].ToString()!="")
				{
					model.CJSJ=DateTime.Parse(row["CJSJ"].ToString());
				}
				if(row["ZHXGSJ"]!=null && row["ZHXGSJ"].ToString()!="")
				{
					model.ZHXGSJ=DateTime.Parse(row["ZHXGSJ"].ToString());
				}
				if(row["FQDWBM"]!=null && row["FQDWBM"].ToString()!="")
				{
					model.FQDWBM=decimal.Parse(row["FQDWBM"].ToString());
				}
				if(row["FQL"]!=null)
				{
					model.FQL=row["FQL"].ToString();
				}
				if(row["DWBM"]!=null)
				{
					model.DWBM=row["DWBM"].ToString();
				}
				if(row["BMSAH"]!=null)
				{
					model.BMSAH=row["BMSAH"].ToString();
				}
				if(row["WJLJ"]!=null)
				{
					model.WJLJ=row["WJLJ"].ToString();
				}
				if(row["WJMC"]!=null)
				{
					model.WJMC=row["WJMC"].ToString();
				}
				if(row["WJXSMC"]!=null)
				{
					model.WJXSMC=row["WJXSMC"].ToString();
				}
				if(row["WJHZ"]!=null)
				{
					model.WJHZ=row["WJHZ"].ToString();
				}
				if(row["WJKSY"]!=null && row["WJKSY"].ToString()!="")
				{
					model.WJKSY=decimal.Parse(row["WJKSY"].ToString());
				}
				if(row["WJJSY"]!=null && row["WJJSY"].ToString()!="")
				{
					model.WJJSY=decimal.Parse(row["WJJSY"].ToString());
				}
				if(row["WJBZXX"]!=null)
				{
					model.WJBZXX=row["WJBZXX"].ToString();
				}
				if(row["WJYZBZ"]!=null)
				{
					model.WJYZBZ=row["WJYZBZ"].ToString();
				}
				if(row["WJSXH"]!=null && row["WJSXH"].ToString()!="")
				{
					model.WJSXH=decimal.Parse(row["WJSXH"].ToString());
				}
				if(row["WJZDY"]!=null)
				{
					model.WJZDY=row["WJZDY"].ToString();
				}
				if(row["SSLBBM"]!=null)
				{
					model.SSLBBM=row["SSLBBM"].ToString();
				}
				if(row["SSLBMC"]!=null)
				{
					model.SSLBMC=row["SSLBMC"].ToString();
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
            strSql.Append("select JZBH,MLBH,WJXH,SFSC,CJSJ,ZHXGSJ,FQDWBM,FQL,DWBM,BMSAH,WJLJ,WJMC,WJXSMC,WJHZ,WJKSY,WJJSY,WJBZXX,WJYZBZ,WJSXH,WJZDY,SSLBBM,SSLBMC,WJDX");
			strSql.Append(" FROM YX_DZJZ_JZMLWJ ");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetList(string strWhere, params object[] objValues)", "EDRS.OracleDAL.YX_DZJZ_JZMLWJ", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
        public int GetRecordCount(string strWhere, params object[] objValues)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM YX_DZJZ_JZMLWJ ");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public int GetRecordCount(string strWhere, params object[] objValues)", "EDRS.OracleDAL.YX_DZJZ_JZMLWJ", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
				strSql.Append("order by T.WJXH desc");
			}
			strSql.AppendFormat(")AS Ro, T.*  from YX_DZJZ_JZMLWJ{0} T ",ConfigHelper.GetConfigString("OrcDBLinq"));
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)", "EDRS.OracleDAL.YX_DZJZ_JZMLWJ", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
			parameters[0].Value = "YX_DZJZ_JZMLWJ";
			parameters[1].Value = "WJXH";
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

