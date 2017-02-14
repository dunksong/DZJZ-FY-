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
	/// 数据访问类:XY_DZJZ_SBDJ
	/// </summary>
	public partial class XY_DZJZ_SBDJ:IXY_DZJZ_SBDJ
    {
        public System.Web.HttpRequest context = null;
        public void SetHttpContext(System.Web.HttpRequest _context)
        {
            this.context = _context;
        }
		public XY_DZJZ_SBDJ()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string MAC,string DEVSN)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from XY_DZJZ_SBDJ");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
			strSql.Append(" where MAC=:MAC and DEVSN=:DEVSN ");
			OracleParameter[] parameters = {
					new OracleParameter(":MAC", OracleType.VarChar,20),
					new OracleParameter(":DEVSN", OracleType.VarChar,50)			};
			parameters[0].Value = MAC;
			parameters[1].Value = DEVSN;

            try
            {
                return DbHelperOra.Exists(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Exists(string MAC,string DEVSN)", "EDRS.OracleDAL.XY_DZJZ_SBDJ", strSql.ToString(), parameters);
            }
            return false;
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EDRS.Model.XY_DZJZ_SBDJ model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into XY_DZJZ_SBDJ(");
			strSql.Append("MAC,DEVSN,DEVTYPE,DEVFACTORY,DEVUSETIME,IP,DWBM)");
			strSql.Append(" values (");
            strSql.Append(":MAC,:DEVSN,:DEVTYPE,:DEVFACTORY,:DEVUSETIME,:IP,:DWBM)");
			OracleParameter[] parameters = {
					new OracleParameter(":MAC", OracleType.VarChar,20),
					new OracleParameter(":DEVSN", OracleType.VarChar,50),
					new OracleParameter(":DEVTYPE", OracleType.VarChar,50),
					new OracleParameter(":DEVFACTORY", OracleType.VarChar,50),
					new OracleParameter(":DEVUSETIME", OracleType.DateTime),
					new OracleParameter(":IP", OracleType.VarChar,20),
                    new OracleParameter(":DWBM", OracleType.VarChar,50)};
			parameters[0].Value = model.MAC;
			parameters[1].Value = model.DEVSN;
			parameters[2].Value = model.DEVTYPE;
			parameters[3].Value = model.DEVFACTORY;
			parameters[4].Value = model.DEVUSETIME;
			parameters[5].Value = model.IP;
            parameters[6].Value = model.DWBM;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Add(EDRS.Model.XY_DZJZ_SBDJ model)", "EDRS.OracleDAL.XY_DZJZ_SBDJ", strSql.ToString(), parameters);
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
		public bool Update(EDRS.Model.XY_DZJZ_SBDJ model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update XY_DZJZ_SBDJ set ");
			strSql.Append("DEVTYPE=:DEVTYPE,");
			strSql.Append("DEVFACTORY=:DEVFACTORY,");
			strSql.Append("DEVUSETIME=:DEVUSETIME,");
			strSql.Append("IP=:IP");
            strSql.Append("DWBM=:DWBM");
			strSql.Append(" where MAC=:MAC and DEVSN=:DEVSN ");
			OracleParameter[] parameters = {
					new OracleParameter(":DEVTYPE", OracleType.VarChar,50),
					new OracleParameter(":DEVFACTORY", OracleType.VarChar,50),
					new OracleParameter(":DEVUSETIME", OracleType.DateTime),
					new OracleParameter(":IP", OracleType.VarChar,20),
                    new OracleParameter(":DWBM", OracleType.VarChar,50),
					new OracleParameter(":MAC", OracleType.VarChar,20),
					new OracleParameter(":DEVSN", OracleType.VarChar,50)};
			parameters[0].Value = model.DEVTYPE;
			parameters[1].Value = model.DEVFACTORY;
			parameters[2].Value = model.DEVUSETIME;
			parameters[3].Value = model.IP;
            parameters[4].Value = model.DWBM;
			parameters[5].Value = model.MAC;
			parameters[6].Value = model.DEVSN;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Update(EDRS.Model.XY_DZJZ_SBDJ model)", "EDRS.OracleDAL.XY_DZJZ_SBDJ", strSql.ToString(), parameters);
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
		public bool Delete(string MAC,string DEVSN)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from XY_DZJZ_SBDJ ");
			strSql.Append(" where MAC=:MAC and DEVSN=:DEVSN ");
			OracleParameter[] parameters = {
					new OracleParameter(":MAC", OracleType.VarChar,20),
					new OracleParameter(":DEVSN", OracleType.VarChar,50)			};
			parameters[0].Value = MAC;
			parameters[1].Value = DEVSN;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Delete(string MAC,string DEVSN)", "EDRS.OracleDAL.XY_DZJZ_SBDJ", strSql.ToString(), parameters);
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
		public EDRS.Model.XY_DZJZ_SBDJ GetModel(string MAC,string DEVSN)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select MAC,DEVSN,DEVTYPE,DEVFACTORY,DEVUSETIME,IP,DWBM from XY_DZJZ_SBDJ ");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
			strSql.Append(" where MAC=:MAC and DEVSN=:DEVSN ");
			OracleParameter[] parameters = {
					new OracleParameter(":MAC", OracleType.VarChar,20),
					new OracleParameter(":DEVSN", OracleType.VarChar,50)			};
			parameters[0].Value = MAC;
			parameters[1].Value = DEVSN;

			EDRS.Model.XY_DZJZ_SBDJ model=new EDRS.Model.XY_DZJZ_SBDJ();
            DataSet ds = null;
            try
            {
                ds = DbHelperOra.Query(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public EDRS.Model.XY_DZJZ_SBDJ GetModel(string MAC,string DEVSN)", "EDRS.OracleDAL.XY_DZJZ_SBDJ", strSql.ToString(), parameters);
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
		public EDRS.Model.XY_DZJZ_SBDJ DataRowToModel(DataRow row)
		{
			EDRS.Model.XY_DZJZ_SBDJ model=new EDRS.Model.XY_DZJZ_SBDJ();
			if (row != null)
			{
				if(row["MAC"]!=null)
				{
					model.MAC=row["MAC"].ToString();
				}
				if(row["DEVSN"]!=null)
				{
					model.DEVSN=row["DEVSN"].ToString();
				}
				if(row["DEVTYPE"]!=null)
				{
					model.DEVTYPE=row["DEVTYPE"].ToString();
				}
				if(row["DEVFACTORY"]!=null)
				{
					model.DEVFACTORY=row["DEVFACTORY"].ToString();
				}
				if(row["DEVUSETIME"]!=null && row["DEVUSETIME"].ToString()!="")
				{
					model.DEVUSETIME=DateTime.Parse(row["DEVUSETIME"].ToString());
				}
				if(row["IP"]!=null)
				{
					model.IP=row["IP"].ToString();
				}
                if (row["DWBM"] != null)
                {
                    model.DWBM = row["DWBM"].ToString();
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
            strSql.Append("select MAC,DEVSN,DEVTYPE,DEVFACTORY,DEVUSETIME,IP,DWBM ");
			strSql.Append(" FROM XY_DZJZ_SBDJ ");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetList(string strWhere, params object[] objValues)", "EDRS.OracleDAL.XY_DZJZ_SBDJ", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
        public int GetRecordCount(string strWhere, params object[] objValues)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM XY_DZJZ_SBDJ ");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public int GetRecordCount(string strWhere, params object[] objValues)", "EDRS.OracleDAL.XY_DZJZ_SBDJ", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
				strSql.Append("order by T.DEVSN desc");
			}
			strSql.AppendFormat(")AS Ro, T.*  from XY_DZJZ_SBDJ{0} T ",ConfigHelper.GetConfigString("OrcDBLinq"));
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)", "EDRS.OracleDAL.XY_DZJZ_SBDJ", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
			parameters[0].Value = "XY_DZJZ_SBDJ";
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

