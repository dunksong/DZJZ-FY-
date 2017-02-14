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
	/// 数据访问类:YX_DZJZ_LSGL
	/// </summary>
	public partial class YX_DZJZ_LSGL:IYX_DZJZ_LSGL
    {
        public System.Web.HttpRequest context = null;
        public void SetHttpContext(System.Web.HttpRequest _context)
        {
            this.context = _context;
        }
		public YX_DZJZ_LSGL()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string LSZH)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from YX_DZJZ_LSGL");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
			strSql.Append(" where LSZH=:LSZH ");
			OracleParameter[] parameters = {
					new OracleParameter(":LSZH", OracleType.VarChar,100)			};
			parameters[0].Value = LSZH;

            try
            {
                return DbHelperOra.Exists(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Exists(string LSZH)", "EDRS.OracleDAL.YX_DZJZ_LSGL", strSql.ToString(), parameters);
            }
            return false;
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EDRS.Model.YX_DZJZ_LSGL model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into YX_DZJZ_LSGL(");
            strSql.Append("LSZH,LSXM,LSDW,LSDWDZ,LSDWYZHM,LSLXDH,LSSJ,DELXR,DELXRDH,LSZGYXSJ,SFDXZG,LSXXBZ,CJSJ,ZHYCYJSJ,SFSC)");
			strSql.Append(" values (");
            strSql.Append(":LSZH,:LSXM,:LSDW,:LSDWDZ,:LSDWYZHM,:LSLXDH,:LSSJ,:DELXR,:DELXRDH,:LSZGYXSJ,:SFDXZG,:LSXXBZ,:CJSJ,:ZHYCYJSJ,:SFSC)");
			OracleParameter[] parameters = {
					new OracleParameter(":LSZH", OracleType.VarChar,100),
					new OracleParameter(":LSXM", OracleType.VarChar,60),
					new OracleParameter(":LSDW", OracleType.VarChar,300),
					new OracleParameter(":LSDWDZ", OracleType.VarChar,300),
					new OracleParameter(":LSDWYZHM", OracleType.Char,6),
					new OracleParameter(":LSLXDH", OracleType.VarChar,50),
					new OracleParameter(":LSSJ", OracleType.VarChar,50),
					new OracleParameter(":DELXR", OracleType.VarChar,60),
					new OracleParameter(":DELXRDH", OracleType.VarChar,50),
					new OracleParameter(":LSZGYXSJ", OracleType.DateTime),
					new OracleParameter(":SFDXZG", OracleType.Char,1),
					new OracleParameter(":LSXXBZ", OracleType.VarChar,300),
					new OracleParameter(":CJSJ", OracleType.DateTime),
                    new OracleParameter(":ZHYCYJSJ", OracleType.DateTime),
					new OracleParameter(":SFSC", OracleType.Char,1)};
			parameters[0].Value = model.LSZH;
			parameters[1].Value = model.LSXM;
			parameters[2].Value = model.LSDW;
			parameters[3].Value = model.LSDWDZ;
			parameters[4].Value = model.LSDWYZHM;
			parameters[5].Value = model.LSLXDH;
			parameters[6].Value = model.LSSJ;
			parameters[7].Value = model.DELXR;
			parameters[8].Value = model.DELXRDH;
			parameters[9].Value = model.LSZGYXSJ;
			parameters[10].Value = model.SFDXZG;
			parameters[11].Value = model.LSXXBZ;
			parameters[12].Value = model.CJSJ;
            parameters[13].Value = model.ZHYCYJSJ ?? (object)DBNull.Value;
            parameters[14].Value = model.SFSC;
            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Add(EDRS.Model.YX_DZJZ_LSGL model)", "EDRS.OracleDAL.YX_DZJZ_LSGL", strSql.ToString(), parameters);
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
		public bool Update(EDRS.Model.YX_DZJZ_LSGL model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update YX_DZJZ_LSGL set ");
			strSql.Append("LSXM=:LSXM,");
			strSql.Append("LSDW=:LSDW,");
			strSql.Append("LSDWDZ=:LSDWDZ,");
			strSql.Append("LSDWYZHM=:LSDWYZHM,");
			strSql.Append("LSLXDH=:LSLXDH,");
			strSql.Append("LSSJ=:LSSJ,");
			strSql.Append("DELXR=:DELXR,");
			strSql.Append("DELXRDH=:DELXRDH,");
			strSql.Append("LSZGYXSJ=:LSZGYXSJ,");
			strSql.Append("SFDXZG=:SFDXZG,");
			strSql.Append("LSXXBZ=:LSXXBZ,");
            strSql.Append("ZHYCYJSJ=:ZHYCYJSJ,");
            strSql.Append("SFSC=:SFSC");
			strSql.Append(" where LSZH=:LSZH ");
			OracleParameter[] parameters = {
					new OracleParameter(":LSXM", OracleType.VarChar,60),
					new OracleParameter(":LSDW", OracleType.VarChar,300),
					new OracleParameter(":LSDWDZ", OracleType.VarChar,300),
					new OracleParameter(":LSDWYZHM", OracleType.Char,6),
					new OracleParameter(":LSLXDH", OracleType.VarChar,50),
					new OracleParameter(":LSSJ", OracleType.VarChar,50),
					new OracleParameter(":DELXR", OracleType.VarChar,60),
					new OracleParameter(":DELXRDH", OracleType.VarChar,50),
					new OracleParameter(":LSZGYXSJ", OracleType.DateTime),
					new OracleParameter(":SFDXZG", OracleType.Char,1),
					new OracleParameter(":LSXXBZ", OracleType.VarChar,300),
					new OracleParameter(":LSZH", OracleType.VarChar,100),
                     new OracleParameter(":ZHYCYJSJ", OracleType.DateTime),
					new OracleParameter(":SFSC", OracleType.Char,1)};
			parameters[0].Value = model.LSXM;
			parameters[1].Value = model.LSDW;
			parameters[2].Value = model.LSDWDZ;
			parameters[3].Value = model.LSDWYZHM;
			parameters[4].Value = model.LSLXDH;
			parameters[5].Value = model.LSSJ;
			parameters[6].Value = model.DELXR;
			parameters[7].Value = model.DELXRDH;
			parameters[8].Value = model.LSZGYXSJ;
			parameters[9].Value = model.SFDXZG;
			parameters[10].Value = model.LSXXBZ;
			parameters[11].Value = model.LSZH;
            parameters[12].Value = model.ZHYCYJSJ ?? (object)DBNull.Value;
            parameters[13].Value = model.SFSC;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Update(EDRS.Model.YX_DZJZ_LSGL model)", "EDRS.OracleDAL.YX_DZJZ_LSGL", strSql.ToString(), parameters);
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
		public bool Delete(string LSZH)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("UPDATE YX_DZJZ_LSGL SET SFSC='Y'");
			strSql.Append(" where LSZH=:LSZH ");
			OracleParameter[] parameters = {
					new OracleParameter(":LSZH", OracleType.VarChar,100)			};
			parameters[0].Value = LSZH;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Delete(string LSZH)", "EDRS.OracleDAL.YX_DZJZ_LSGL", strSql.ToString(), parameters);
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
		public bool DeleteList(string LSZHlist )
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("UPDATE YX_DZJZ_LSGL SET SFSC='Y'");
			strSql.Append(" where LSZH in ("+LSZHlist + ")  ");
            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString());
            }
            catch (Exception ex)
            {
                System.Collections.Generic.Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
                parameters.Add("LSZH", LSZHlist);
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool DeleteList(string LSZHlist )", "EDRS.OracleDAL.YX_DZJZ_LSGL", strSql.ToString(), parameters);
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
        public EDRS.Model.YX_DZJZ_LSGL GetModel(string LSZH)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select LSZH,LSXM,LSDW,T2.DwMC LSDWMC,LSDWDZ,LSDWYZHM,LSLXDH,LSSJ,DELXR,DELXRDH,LSZGYXSJ,SFDXZG,LSXXBZ,LSZZWJ1,LSZZWJ2,LSZZWJ3,LSZZWJ4,CJSJ,ZHYCYJSJ,T1.SFSC ");
            strSql.Append(" FROM YX_DZJZ_LSGL T1 LEFT JOIN xt_zzjg_dwbm T2 ON(T1.Lsdw = T2.Dwbm) ");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
            strSql.Append(" where LSZH=:LSZH ");
            OracleParameter[] parameters = {
					new OracleParameter(":LSZH", OracleType.VarChar,100)			};
            parameters[0].Value = LSZH;

            EDRS.Model.YX_DZJZ_LSGL model = new EDRS.Model.YX_DZJZ_LSGL();
            DataSet ds = null;
            try
            {
                ds = DbHelperOra.Query(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public EDRS.Model.YX_DZJZ_LSGL GetModel(string LSZH)", "EDRS.OracleDAL.YX_DZJZ_LSGL", strSql.ToString(), parameters);
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
		public EDRS.Model.YX_DZJZ_LSGL DataRowToModel(DataRow row)
		{
			EDRS.Model.YX_DZJZ_LSGL model=new EDRS.Model.YX_DZJZ_LSGL();
			if (row != null)
			{
				if(row["LSZH"]!=null)
				{
					model.LSZH=row["LSZH"].ToString();
				}
				if(row["LSXM"]!=null)
				{
					model.LSXM=row["LSXM"].ToString();
				}
				if(row["LSDW"]!=null)
				{
					model.LSDW=row["LSDW"].ToString();
				}
                if (row["LSDWMC"] != null)
                {
                    model.LSDWMC = row["LSDWMC"].ToString();
                }
				if(row["LSDWDZ"]!=null)
				{
					model.LSDWDZ=row["LSDWDZ"].ToString();
				}
				if(row["LSDWYZHM"]!=null)
				{
					model.LSDWYZHM=row["LSDWYZHM"].ToString();
				}
				if(row["LSLXDH"]!=null)
				{
					model.LSLXDH=row["LSLXDH"].ToString();
				}
				if(row["LSSJ"]!=null)
				{
					model.LSSJ=row["LSSJ"].ToString();
				}
				if(row["DELXR"]!=null)
				{
					model.DELXR=row["DELXR"].ToString();
				}
				if(row["DELXRDH"]!=null)
				{
					model.DELXRDH=row["DELXRDH"].ToString();
				}
				if(row["LSZGYXSJ"]!=null && row["LSZGYXSJ"].ToString()!="")
				{
					model.LSZGYXSJ=DateTime.Parse(row["LSZGYXSJ"].ToString());
				}
				if(row["SFDXZG"]!=null)
				{
					model.SFDXZG=row["SFDXZG"].ToString();
				}
				if(row["LSXXBZ"]!=null)
				{
					model.LSXXBZ=row["LSXXBZ"].ToString();
				}
				if(row["LSZZWJ1"]!=null)
				{
					model.LSZZWJ1=row["LSZZWJ1"].ToString();
				}
				if(row["LSZZWJ2"]!=null)
				{
					model.LSZZWJ2=row["LSZZWJ2"].ToString();
				}
				if(row["LSZZWJ3"]!=null)
				{
					model.LSZZWJ3=row["LSZZWJ3"].ToString();
				}
				if(row["LSZZWJ4"]!=null)
				{
					model.LSZZWJ4=row["LSZZWJ4"].ToString();
				}
				if(row["CJSJ"]!=null && row["CJSJ"].ToString()!="")
				{
					model.CJSJ=DateTime.Parse(row["CJSJ"].ToString());
				}
				if(row["ZHYCYJSJ"]!=null && row["ZHYCYJSJ"].ToString()!="")
				{
					model.ZHYCYJSJ=DateTime.Parse(row["ZHYCYJSJ"].ToString());
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
            strSql.Append("select LSZH,LSXM,LSDW,T2.DwMC LSDWMC,LSDWDZ,LSDWYZHM,LSLXDH,LSSJ,DELXR,DELXRDH,LSZGYXSJ,SFDXZG,LSXXBZ,LSZZWJ1,LSZZWJ2,LSZZWJ3,LSZZWJ4,CJSJ,ZHYCYJSJ ");
            strSql.Append(" FROM YX_DZJZ_LSGL T1 LEFT JOIN xt_zzjg_dwbm T2 ON(T1.Lsdw = T2.Dwbm) ");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where T1.SFSC='N' "+strWhere);
			}
            try
            {
                return DbHelperOra.Query(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetList(string strWhere, params object[] objValues)", "EDRS.OracleDAL.YX_DZJZ_LSGL", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
        public int GetRecordCount(string strWhere, params object[] objValues)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM YX_DZJZ_LSGL T  ");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public int GetRecordCount(string strWhere, params object[] objValues)", "EDRS.OracleDAL.YX_DZJZ_LSGL", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
				strSql.Append("order by T.LSZH desc");
			}
			strSql.AppendFormat(")AS Ro, T.*  from YX_DZJZ_LSGL{0} T ",ConfigHelper.GetConfigString("OrcDBLinq"));
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)", "EDRS.OracleDAL.YX_DZJZ_LSGL", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
			parameters[0].Value = "YX_DZJZ_LSGL";
			parameters[1].Value = "LSZH";
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
        /// 更新最后一次阅卷时间
        /// </summary>
        /// <param name="LSZH"></param>
        /// <param name="zhxgsj"></param>
        /// <returns></returns>
        public bool UpdateZHXGSJ(string LSZH, DateTime zhxgsj)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update YX_DZJZ_LSGL set ");
            strSql.Append("ZHYCYJSJ=:ZHYCYJSJ,");
            strSql.Append(" where LSZH=:LSZH ");
            OracleParameter[] parameters = {
					new OracleParameter(":ZHYCYJSJ", OracleType.DateTime),
					new OracleParameter(":LSZH", OracleType.VarChar,100)};
            parameters[0].Value = zhxgsj;
            parameters[1].Value = LSZH;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool UpdateZHXGSJ(string LSZH, DateTime zhxgsj)", "EDRS.OracleDAL.YX_DZJZ_LSGL", strSql.ToString(), parameters);
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
		#endregion  ExtensionMethod
	}
}

