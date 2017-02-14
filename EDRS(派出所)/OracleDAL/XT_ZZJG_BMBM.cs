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
	/// 数据访问类:XT_ZZJG_BMBM
	/// </summary>
	public partial class XT_ZZJG_BMBM:IXT_ZZJG_BMBM
    {
        public System.Web.HttpRequest context = null;
        public void SetHttpContext(System.Web.HttpRequest _context)
        {
            this.context = _context;
        }
		public XT_ZZJG_BMBM()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string BMBM)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from XT_ZZJG_BMBM");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
			strSql.Append(" where BMBM=:BMBM ");
			OracleParameter[] parameters = {
					new OracleParameter(":BMBM", OracleType.Char,4)			};
			parameters[0].Value = BMBM;

            try
            {
                return DbHelperOra.Exists(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Exists(string BMBM)", "EDRS.OracleDAL.XT_ZZJG_BMBM", strSql.ToString(), parameters);
            }
            return false;
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EDRS.Model.XT_ZZJG_BMBM model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into XT_ZZJG_BMBM(");
			strSql.Append("BMBM,DWBM,FBMBM,BMMC,BMJC,BMAHJC,BMWHJC,SFLSJG,SFCBBM,BMXH,BZ,SFSC,BMYS)");
			strSql.Append(" values (");
			strSql.Append(":BMBM,:DWBM,:FBMBM,:BMMC,:BMJC,:BMAHJC,:BMWHJC,:SFLSJG,:SFCBBM,:BMXH,:BZ,:SFSC,:BMYS)");
			OracleParameter[] parameters = {
					new OracleParameter(":BMBM", OracleType.Char,4),
					new OracleParameter(":DWBM", OracleType.VarChar,50),
					new OracleParameter(":FBMBM", OracleType.Char,4),
					new OracleParameter(":BMMC", OracleType.VarChar,300),
					new OracleParameter(":BMJC", OracleType.VarChar,60),
					new OracleParameter(":BMAHJC", OracleType.VarChar,60),
					new OracleParameter(":BMWHJC", OracleType.VarChar,60),
					new OracleParameter(":SFLSJG", OracleType.Char,1),
					new OracleParameter(":SFCBBM", OracleType.Char,1),
					new OracleParameter(":BMXH", OracleType.Number,4),
					new OracleParameter(":BZ", OracleType.VarChar,900),
					new OracleParameter(":SFSC", OracleType.Char,1),
					new OracleParameter(":BMYS", OracleType.Char,1)};
			parameters[0].Value = model.BMBM;
			parameters[1].Value = model.DWBM;
			parameters[2].Value = model.FBMBM;
			parameters[3].Value = model.BMMC;
			parameters[4].Value = model.BMJC;
			parameters[5].Value = model.BMAHJC;
			parameters[6].Value = model.BMWHJC;
			parameters[7].Value = model.SFLSJG;
			parameters[8].Value = model.SFCBBM;
			parameters[9].Value = model.BMXH;
			parameters[10].Value = model.BZ;
			parameters[11].Value = model.SFSC;
			parameters[12].Value = model.BMYS;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Add(EDRS.Model.XT_ZZJG_BMBM model)", "EDRS.OracleDAL.XT_ZZJG_BMBM", strSql.ToString(), parameters);
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
		public bool Update(EDRS.Model.XT_ZZJG_BMBM model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update XT_ZZJG_BMBM set ");
			strSql.Append("FBMBM=:FBMBM,");
			strSql.Append("BMMC=:BMMC,");
			strSql.Append("BMJC=:BMJC,");
			strSql.Append("BMAHJC=:BMAHJC,");
			strSql.Append("BMWHJC=:BMWHJC,");
			strSql.Append("SFLSJG=:SFLSJG,");
			strSql.Append("SFCBBM=:SFCBBM,");
			strSql.Append("BMXH=:BMXH,");
			strSql.Append("BZ=:BZ,");
			strSql.Append("SFSC=:SFSC,");
			strSql.Append("BMYS=:BMYS");
            strSql.Append(" where BMBM=:BMBM and DWBM=:DWBM ");
			OracleParameter[] parameters = {
					new OracleParameter(":DWBM", OracleType.VarChar,50),
					new OracleParameter(":FBMBM", OracleType.Char,4),
					new OracleParameter(":BMMC", OracleType.VarChar,300),
					new OracleParameter(":BMJC", OracleType.VarChar,60),
					new OracleParameter(":BMAHJC", OracleType.VarChar,60),
					new OracleParameter(":BMWHJC", OracleType.VarChar,60),
					new OracleParameter(":SFLSJG", OracleType.Char,1),
					new OracleParameter(":SFCBBM", OracleType.Char,1),
					new OracleParameter(":BMXH", OracleType.Number,4),
					new OracleParameter(":BZ", OracleType.VarChar,900),
					new OracleParameter(":SFSC", OracleType.Char,1),
					new OracleParameter(":BMYS", OracleType.Char,1),
					new OracleParameter(":BMBM", OracleType.Char,4)};
			parameters[0].Value = model.DWBM;
			parameters[1].Value = model.FBMBM;
			parameters[2].Value = model.BMMC;
			parameters[3].Value = model.BMJC;
			parameters[4].Value = model.BMAHJC;
			parameters[5].Value = model.BMWHJC;
			parameters[6].Value = model.SFLSJG;
			parameters[7].Value = model.SFCBBM;
			parameters[8].Value = model.BMXH;
			parameters[9].Value = model.BZ;
			parameters[10].Value = model.SFSC;
			parameters[11].Value = model.BMYS;
			parameters[12].Value = model.BMBM;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Update(EDRS.Model.XT_ZZJG_BMBM model)", "EDRS.OracleDAL.XT_ZZJG_BMBM", strSql.ToString(), parameters);
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
		public bool Delete(string BMBM)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from XT_ZZJG_BMBM ");
			strSql.Append(" where BMBM=:BMBM ");
			OracleParameter[] parameters = {
					new OracleParameter(":BMBM", OracleType.Char,4)			};
			parameters[0].Value = BMBM;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Delete(string BMBM)", "EDRS.OracleDAL.XT_ZZJG_BMBM", strSql.ToString(), parameters);
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
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string BMBMlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from XT_ZZJG_BMBM ");
			strSql.Append(" where BMBM in ("+BMBMlist + ")  ");
            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString());
            }
            catch (Exception ex)
            {
                System.Collections.Generic.Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
                parameters.Add("BMBM", BMBMlist);
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool DeleteList(string BMBMlist )", "EDRS.OracleDAL.XT_ZZJG_BMBM", strSql.ToString(), parameters);
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
        public EDRS.Model.XT_ZZJG_BMBM GetModel(string DWBM, string BMBM)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select BMBM,DWBM,FBMBM,BMMC,BMJC,BMAHJC,BMWHJC,SFLSJG,SFCBBM,BMXH,BZ,SFSC,BMYS from XT_ZZJG_BMBM ");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
            strSql.Append(" where BMBM=:BMBM and DWBM=:DWBM ");
            OracleParameter[] parameters = {
					new OracleParameter(":BMBM", OracleType.Char,4)	,
                    new OracleParameter(":DWBM", OracleType.VarChar,50)		};
            parameters[0].Value = BMBM;
            parameters[1].Value = DWBM;
            EDRS.Model.XT_ZZJG_BMBM model = new EDRS.Model.XT_ZZJG_BMBM();
            DataSet ds = null;
            try
            {
                ds = DbHelperOra.Query(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public EDRS.Model.XT_ZZJG_BMBM GetModel(string DWBM,string BMBM)", "EDRS.OracleDAL.XT_ZZJG_BMBM", strSql.ToString(), parameters);
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
		public EDRS.Model.XT_ZZJG_BMBM DataRowToModel(DataRow row)
		{
			EDRS.Model.XT_ZZJG_BMBM model=new EDRS.Model.XT_ZZJG_BMBM();
			if (row != null)
			{
				if(row["BMBM"]!=null)
				{
					model.BMBM=row["BMBM"].ToString();
				}
				if(row["DWBM"]!=null)
				{
					model.DWBM=row["DWBM"].ToString();
				}
				if(row["FBMBM"]!=null)
				{
					model.FBMBM=row["FBMBM"].ToString();
				}
				if(row["BMMC"]!=null)
				{
					model.BMMC=row["BMMC"].ToString();
				}
				if(row["BMJC"]!=null)
				{
					model.BMJC=row["BMJC"].ToString();
				}
				if(row["BMAHJC"]!=null)
				{
					model.BMAHJC=row["BMAHJC"].ToString();
				}
				if(row["BMWHJC"]!=null)
				{
					model.BMWHJC=row["BMWHJC"].ToString();
				}
				if(row["SFLSJG"]!=null)
				{
					model.SFLSJG=row["SFLSJG"].ToString();
				}
				if(row["SFCBBM"]!=null)
				{
					model.SFCBBM=row["SFCBBM"].ToString();
				}
				if(row["BMXH"]!=null && row["BMXH"].ToString()!="")
				{
					model.BMXH=decimal.Parse(row["BMXH"].ToString());
				}
				if(row["BZ"]!=null)
				{
					model.BZ=row["BZ"].ToString();
				}
				if(row["SFSC"]!=null)
				{
					model.SFSC=row["SFSC"].ToString();
				}
				if(row["BMYS"]!=null)
				{
					model.BMYS=row["BMYS"].ToString();
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
			strSql.Append("select BMBM,DWBM,FBMBM,BMMC,BMJC,BMAHJC,BMWHJC,SFLSJG,SFCBBM,BMXH,BZ,SFSC,BMYS ");
			strSql.Append(" FROM XT_ZZJG_BMBM ");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetList(string strWhere, params object[] objValues)", "EDRS.OracleDAL.XT_ZZJG_BMBM", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
        public int GetRecordCount(string strWhere, params object[] objValues)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM XT_ZZJG_BMBM ");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public int GetRecordCount(string strWhere, params object[] objValues)", "EDRS.OracleDAL.XT_ZZJG_BMBM", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
				strSql.Append("order by T.BMBM desc");
			}
			strSql.AppendFormat(")AS Ro, T.*  from XT_ZZJG_BMBM{0} T ",ConfigHelper.GetConfigString("OrcDBLinq"));
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)", "EDRS.OracleDAL.XT_ZZJG_BMBM", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
			parameters[0].Value = "XT_ZZJG_BMBM";
			parameters[1].Value = "BMBM";
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

