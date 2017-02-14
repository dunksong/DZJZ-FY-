
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
	/// 数据访问类:YX_DZJZ_JZML
	/// </summary>
	public partial class YX_DZJZ_JZML:IYX_DZJZ_JZML
    {
        public System.Web.HttpRequest context = null;
        public void SetHttpContext(System.Web.HttpRequest _context)
        {
            this.context = _context;
        }
		public YX_DZJZ_JZML()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string JZBH,string MLBH)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from YX_DZJZ_JZML");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
			strSql.Append(" where JZBH=:JZBH and MLBH=:MLBH ");
			OracleParameter[] parameters = {
					new OracleParameter(":JZBH", OracleType.Char,14),
					new OracleParameter(":MLBH", OracleType.VarChar,100)			};
			parameters[0].Value = JZBH;
			parameters[1].Value = MLBH;
            try
            {
                return DbHelperOra.Exists(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Exists(string JZBH,string MLBH)", "EDRS.OracleDAL.YX_DZJZ_JZML", strSql.ToString(), parameters);
            }
            return false;
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EDRS.Model.YX_DZJZ_JZML model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into YX_DZJZ_JZML(");
			strSql.Append("JZBH,MLBH,SFSC,CJSJ,ZHXGSJ,FQDWBM,FQL,DWBM,BMSAH,FMLBH,MLXSMC,MLXX,MLSXH,MLBM,MLLX,SSLBBM,SSLBMC,SFSM)");
			strSql.Append(" values (");
			strSql.Append(":JZBH,:MLBH,:SFSC,:CJSJ,:ZHXGSJ,:FQDWBM,:FQL,:DWBM,:BMSAH,:FMLBH,:MLXSMC,:MLXX,:MLSXH,:MLBM,:MLLX,:SSLBBM,:SSLBMC,:SFSM)");
			OracleParameter[] parameters = {
					new OracleParameter(":JZBH", OracleType.Char,14),
					new OracleParameter(":MLBH", OracleType.VarChar,100),
					new OracleParameter(":SFSC", OracleType.Char,1),
					new OracleParameter(":CJSJ", OracleType.DateTime),
					new OracleParameter(":ZHXGSJ", OracleType.DateTime),
					new OracleParameter(":FQDWBM", OracleType.VarChar,50),
					new OracleParameter(":FQL", OracleType.VarChar,6),
					new OracleParameter(":DWBM", OracleType.VarChar,50),
					new OracleParameter(":BMSAH", OracleType.VarChar,100),
					new OracleParameter(":FMLBH", OracleType.VarChar,100),
					new OracleParameter(":MLXSMC", OracleType.VarChar,300),
					new OracleParameter(":MLXX", OracleType.VarChar,500),
					new OracleParameter(":MLSXH", OracleType.Number,4),
					new OracleParameter(":MLBM", OracleType.VarChar,300),
					new OracleParameter(":MLLX", OracleType.Char,1),
					new OracleParameter(":SSLBBM", OracleType.Char,13),
					new OracleParameter(":SSLBMC", OracleType.VarChar,1000),
					new OracleParameter(":SFSM", OracleType.Char,1)};
			parameters[0].Value = model.JZBH;
			parameters[1].Value = model.MLBH;
			parameters[2].Value = model.SFSC;
			parameters[3].Value = model.CJSJ;
			parameters[4].Value = model.ZHXGSJ;
			parameters[5].Value = model.FQDWBM;
			parameters[6].Value = model.FQL;
			parameters[7].Value = model.DWBM;
			parameters[8].Value = model.BMSAH;
			parameters[9].Value = model.FMLBH;
			parameters[10].Value = model.MLXSMC;
			parameters[11].Value = model.MLXX;
			parameters[12].Value = model.MLSXH;
			parameters[13].Value = model.MLBM;
			parameters[14].Value = model.MLLX;
			parameters[15].Value = model.SSLBBM;
			parameters[16].Value = model.SSLBMC;
			parameters[17].Value = model.SFSM;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Add(EDRS.Model.YX_DZJZ_JZML model)", "EDRS.OracleDAL.YX_DZJZ_JZML", strSql.ToString(), parameters);
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
		public bool Update(EDRS.Model.YX_DZJZ_JZML model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update YX_DZJZ_JZML set ");
			strSql.Append("SFSC=:SFSC,");
			strSql.Append("CJSJ=:CJSJ,");
			strSql.Append("ZHXGSJ=:ZHXGSJ,");
			strSql.Append("FQDWBM=:FQDWBM,");
			strSql.Append("FQL=:FQL,");
			strSql.Append("DWBM=:DWBM,");
			strSql.Append("BMSAH=:BMSAH,");
			strSql.Append("FMLBH=:FMLBH,");
			strSql.Append("MLXSMC=:MLXSMC,");
			strSql.Append("MLXX=:MLXX,");
			strSql.Append("MLSXH=:MLSXH,");
			strSql.Append("MLBM=:MLBM,");
			strSql.Append("MLLX=:MLLX,");
			strSql.Append("SSLBBM=:SSLBBM,");
			strSql.Append("SSLBMC=:SSLBMC,");
			strSql.Append("SFSM=:SFSM");
			strSql.Append(" where JZBH=:JZBH and MLBH=:MLBH ");
			OracleParameter[] parameters = {
					new OracleParameter(":SFSC", OracleType.Char,1),
					new OracleParameter(":CJSJ", OracleType.DateTime),
					new OracleParameter(":ZHXGSJ", OracleType.DateTime),
					new OracleParameter(":FQDWBM", OracleType.VarChar,50),
					new OracleParameter(":FQL", OracleType.VarChar,6),
					new OracleParameter(":DWBM", OracleType.VarChar,50),
					new OracleParameter(":BMSAH", OracleType.VarChar,100),
					new OracleParameter(":FMLBH", OracleType.VarChar,100),
					new OracleParameter(":MLXSMC", OracleType.VarChar,300),
					new OracleParameter(":MLXX", OracleType.VarChar,500),
					new OracleParameter(":MLSXH", OracleType.Number,4),
					new OracleParameter(":MLBM", OracleType.VarChar,300),
					new OracleParameter(":MLLX", OracleType.Char,1),
					new OracleParameter(":SSLBBM", OracleType.Char,13),
					new OracleParameter(":SSLBMC", OracleType.VarChar,1000),
					new OracleParameter(":SFSM", OracleType.Char,1),
					new OracleParameter(":JZBH", OracleType.Char,14),
					new OracleParameter(":MLBH", OracleType.VarChar,100)};
			parameters[0].Value = model.SFSC;
			parameters[1].Value = model.CJSJ;
			parameters[2].Value = model.ZHXGSJ;
			parameters[3].Value = model.FQDWBM;
			parameters[4].Value = model.FQL;
			parameters[5].Value = model.DWBM;
			parameters[6].Value = model.BMSAH;
			parameters[7].Value = model.FMLBH;
			parameters[8].Value = model.MLXSMC;
			parameters[9].Value = model.MLXX;
			parameters[10].Value = model.MLSXH;
			parameters[11].Value = model.MLBM;
			parameters[12].Value = model.MLLX;
			parameters[13].Value = model.SSLBBM;
			parameters[14].Value = model.SSLBMC;
			parameters[15].Value = model.SFSM;
			parameters[16].Value = model.JZBH;
			parameters[17].Value = model.MLBH;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Update(EDRS.Model.YX_DZJZ_JZML model)", "EDRS.OracleDAL.YX_DZJZ_JZML", strSql.ToString(), parameters);
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
		public bool Delete(string JZBH,string MLBH)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from YX_DZJZ_JZML ");
			strSql.Append(" where JZBH=:JZBH and MLBH=:MLBH ");
			OracleParameter[] parameters = {
					new OracleParameter(":JZBH", OracleType.Char,14),
					new OracleParameter(":MLBH", OracleType.VarChar,100)			};
			parameters[0].Value = JZBH;
			parameters[1].Value = MLBH;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Delete(string JZBH,string MLBH)", "EDRS.OracleDAL.YX_DZJZ_JZML", strSql.ToString(), parameters);
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
        public EDRS.Model.YX_DZJZ_JZML GetModel(string JZBH, string MLBH)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select JZBH,MLBH,SFSC,CJSJ,ZHXGSJ,FQDWBM,FQL,DWBM,BMSAH,FMLBH,MLXSMC,MLXX,MLSXH,MLBM,MLLX,SSLBBM,SSLBMC,SFSM from YX_DZJZ_JZML ");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
            strSql.Append(" where JZBH=:JZBH and MLBH=:MLBH ");
            OracleParameter[] parameters = {
					new OracleParameter(":JZBH", OracleType.Char,14),
					new OracleParameter(":MLBH", OracleType.VarChar,100)			};
            parameters[0].Value = JZBH;
            parameters[1].Value = MLBH;

            EDRS.Model.YX_DZJZ_JZML model = new EDRS.Model.YX_DZJZ_JZML();
            DataSet ds = null;
            try
            {
                ds = DbHelperOra.Query(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public EDRS.Model.YX_DZJZ_JZML GetModel(string JZBH,string MLBH)", "EDRS.OracleDAL.YX_DZJZ_JZML", strSql.ToString(), parameters);
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
		public EDRS.Model.YX_DZJZ_JZML DataRowToModel(DataRow row)
		{
			EDRS.Model.YX_DZJZ_JZML model=new EDRS.Model.YX_DZJZ_JZML();
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
				if(row["FMLBH"]!=null)
				{
					model.FMLBH=row["FMLBH"].ToString();
				}
				if(row["MLXSMC"]!=null)
				{
					model.MLXSMC=row["MLXSMC"].ToString();
				}
				if(row["MLXX"]!=null)
				{
					model.MLXX=row["MLXX"].ToString();
				}
				if(row["MLSXH"]!=null && row["MLSXH"].ToString()!="")
				{
					model.MLSXH=decimal.Parse(row["MLSXH"].ToString());
				}
				if(row["MLBM"]!=null)
				{
					model.MLBM=row["MLBM"].ToString();
				}
				if(row["MLLX"]!=null)
				{
					model.MLLX=row["MLLX"].ToString();
				}
				if(row["SSLBBM"]!=null)
				{
					model.SSLBBM=row["SSLBBM"].ToString();
				}
				if(row["SSLBMC"]!=null)
				{
					model.SSLBMC=row["SSLBMC"].ToString();
				}
				if(row["SFSM"]!=null)
				{
					model.SFSM=row["SFSM"].ToString();
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
			strSql.Append("select JZBH,MLBH,SFSC,CJSJ,ZHXGSJ,FQDWBM,FQL,DWBM,BMSAH,FMLBH,MLXSMC,MLXX,MLSXH,MLBM,MLLX,SSLBBM,SSLBMC,SFSM ");
			strSql.Append(" FROM YX_DZJZ_JZML ");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where 1=1 "+strWhere);
			}
            strSql.Append(" ORDER BY MLSXH");
            try
            {
                return DbHelperOra.Query(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetList(string strWhere, params object[] objValues)", "EDRS.OracleDAL.YX_DZJZ_JZML", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
        public int GetRecordCount(string strWhere, params object[] objValues)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM YX_DZJZ_JZML ");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public int GetRecordCount(string strWhere, params object[] objValues)", "EDRS.OracleDAL.YX_DZJZ_JZML", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
				strSql.Append("order by T.MLBH desc");
			}
			strSql.AppendFormat(")AS Ro, T.*  from YX_DZJZ_JZML{0} T ",ConfigHelper.GetConfigString("OrcDBLinq"));
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)", "EDRS.OracleDAL.YX_DZJZ_JZML", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
			parameters[0].Value = "YX_DZJZ_JZML";
			parameters[1].Value = "MLBH";
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

