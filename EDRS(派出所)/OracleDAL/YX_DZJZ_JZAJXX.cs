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
	/// 数据访问类:YX_DZJZ_JZAJXX
	/// </summary>
	public partial class YX_DZJZ_JZAJXX:IYX_DZJZ_JZAJXX
    {
        public System.Web.HttpRequest context = null;
        public void SetHttpContext(System.Web.HttpRequest _context)
        {
            this.context = _context;
        }
		public YX_DZJZ_JZAJXX()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string BMSAH)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from YX_DZJZ_JZAJXX");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
			strSql.Append(" where BMSAH=:BMSAH ");
			OracleParameter[] parameters = {
					new OracleParameter(":BMSAH", OracleType.VarChar,100)			};
			parameters[0].Value = BMSAH;

            try
            {
                return DbHelperOra.Exists(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Exists(string BMSAH)", "EDRS.OracleDAL.YX_DZJZ_JZAJXX", strSql.ToString(), parameters);
            }
            return false;
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EDRS.Model.YX_DZJZ_JZAJXX model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into YX_DZJZ_JZAJXX(");
			strSql.Append("BMSAH,TYSAH,AJMC,AJLB_BM,AJLB_MC,CBDWBM,CBDWMC,CBBMBM,CBBMMC,CBRGH,CBR,ZZSJ,JZSL,JZWJS,JZYS,JZSZKJ,FQL,ZJYCXGSJ,ZJYCXGRGH,ZJYCXGRXM,SFSC)");
			strSql.Append(" values (");
			strSql.Append(":BMSAH,:TYSAH,:AJMC,:AJLB_BM,:AJLB_MC,:CBDWBM,:CBDWMC,:CBBMBM,:CBBMMC,:CBRGH,:CBR,:ZZSJ,:JZSL,:JZWJS,:JZYS,:JZSZKJ,:FQL,:ZJYCXGSJ,:ZJYCXGRGH,:ZJYCXGRXM,:SFSC)");
			OracleParameter[] parameters = {
					new OracleParameter(":BMSAH", OracleType.VarChar,100),
					new OracleParameter(":TYSAH", OracleType.VarChar,100),
					new OracleParameter(":AJMC", OracleType.VarChar,300),
					new OracleParameter(":AJLB_BM", OracleType.VarChar,50),
					new OracleParameter(":AJLB_MC", OracleType.VarChar,300),
					new OracleParameter(":CBDWBM", OracleType.VarChar,50),
					new OracleParameter(":CBDWMC", OracleType.VarChar,300),
					new OracleParameter(":CBBMBM", OracleType.Char,4),
					new OracleParameter(":CBBMMC", OracleType.VarChar,300),
					new OracleParameter(":CBRGH", OracleType.Char,4),
					new OracleParameter(":CBR", OracleType.VarChar,60),
					new OracleParameter(":ZZSJ", OracleType.DateTime),
					new OracleParameter(":JZSL", OracleType.Number,4),
					new OracleParameter(":JZWJS", OracleType.Number,4),
					new OracleParameter(":JZYS", OracleType.Number,4),
					new OracleParameter(":JZSZKJ", OracleType.Number,12),
					new OracleParameter(":FQL", OracleType.Char,4),
					new OracleParameter(":ZJYCXGSJ", OracleType.DateTime),
					new OracleParameter(":ZJYCXGRGH", OracleType.Char,4),
					new OracleParameter(":ZJYCXGRXM", OracleType.VarChar,60),
					new OracleParameter(":SFSC", OracleType.Char,1)};
			parameters[0].Value = model.BMSAH??"";
            parameters[1].Value = model.TYSAH ?? "";
            parameters[2].Value = model.AJMC ?? "";
            parameters[3].Value = model.AJLB_BM ?? "";
            parameters[4].Value = model.AJLB_MC ?? "";
            parameters[5].Value = model.CBDWBM ?? "";
            parameters[6].Value = model.CBDWMC ?? "";
            parameters[7].Value = model.CBBMBM ?? "";
            parameters[8].Value = model.CBBMMC ?? "";
            parameters[9].Value = model.CBRGH ?? "";
            parameters[10].Value = model.CBR ?? "";
            parameters[11].Value = model.ZZSJ ?? DateTime.Now;
            parameters[12].Value = model.JZSL ?? 0;
            parameters[13].Value = model.JZWJS ?? 0;
            parameters[14].Value = model.JZYS ?? 0;
            parameters[15].Value = model.JZSZKJ ?? 0;
            parameters[16].Value = model.FQL;
            parameters[17].Value = model.ZJYCXGSJ ?? DateTime.Now;
            parameters[18].Value = model.ZJYCXGRGH ?? "";
            parameters[19].Value = model.ZJYCXGRXM ?? "";
            parameters[20].Value = model.SFSC ?? "";

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Add(EDRS.Model.YX_DZJZ_JZAJXX model)", "EDRS.OracleDAL.YX_DZJZ_JZAJXX", strSql.ToString(), parameters);
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
		public bool Update(EDRS.Model.YX_DZJZ_JZAJXX model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update YX_DZJZ_JZAJXX set ");
			strSql.Append("TYSAH=:TYSAH,");
			strSql.Append("AJMC=:AJMC,");
			strSql.Append("AJLB_BM=:AJLB_BM,");
			strSql.Append("AJLB_MC=:AJLB_MC,");
			strSql.Append("CBDWBM=:CBDWBM,");
			strSql.Append("CBDWMC=:CBDWMC,");
			strSql.Append("CBBMBM=:CBBMBM,");
			strSql.Append("CBBMMC=:CBBMMC,");
			strSql.Append("CBRGH=:CBRGH,");
			strSql.Append("CBR=:CBR,");
			strSql.Append("ZZSJ=:ZZSJ,");
			strSql.Append("JZSL=:JZSL,");
			strSql.Append("JZWJS=:JZWJS,");
			strSql.Append("JZYS=:JZYS,");
			strSql.Append("JZSZKJ=:JZSZKJ,");
			strSql.Append("FQL=:FQL,");
			strSql.Append("ZJYCXGSJ=:ZJYCXGSJ,");
			strSql.Append("ZJYCXGRGH=:ZJYCXGRGH,");
			strSql.Append("ZJYCXGRXM=:ZJYCXGRXM,");
			strSql.Append("SFSC=:SFSC");
			strSql.Append(" where BMSAH=:BMSAH ");
			OracleParameter[] parameters = {
					new OracleParameter(":TYSAH", OracleType.VarChar,100),
					new OracleParameter(":AJMC", OracleType.VarChar,300),
					new OracleParameter(":AJLB_BM", OracleType.VarChar,50),
					new OracleParameter(":AJLB_MC", OracleType.VarChar,300),
					new OracleParameter(":CBDWBM", OracleType.VarChar,50),
					new OracleParameter(":CBDWMC", OracleType.VarChar,300),
					new OracleParameter(":CBBMBM", OracleType.Char,4),
					new OracleParameter(":CBBMMC", OracleType.VarChar,300),
					new OracleParameter(":CBRGH", OracleType.Char,4),
					new OracleParameter(":CBR", OracleType.VarChar,60),
					new OracleParameter(":ZZSJ", OracleType.DateTime),
					new OracleParameter(":JZSL", OracleType.Number,4),
					new OracleParameter(":JZWJS", OracleType.Number,4),
					new OracleParameter(":JZYS", OracleType.Number,4),
					new OracleParameter(":JZSZKJ", OracleType.Number,12),
					new OracleParameter(":FQL", OracleType.Char,4),
					new OracleParameter(":ZJYCXGSJ", OracleType.DateTime),
					new OracleParameter(":ZJYCXGRGH", OracleType.Char,4),
					new OracleParameter(":ZJYCXGRXM", OracleType.VarChar,60),
					new OracleParameter(":SFSC", OracleType.Char,1),
					new OracleParameter(":BMSAH", OracleType.VarChar,100)};
			parameters[0].Value = model.TYSAH;
			parameters[1].Value = model.AJMC;
			parameters[2].Value = model.AJLB_BM;
			parameters[3].Value = model.AJLB_MC;
			parameters[4].Value = model.CBDWBM;
			parameters[5].Value = model.CBDWMC;
			parameters[6].Value = model.CBBMBM;
			parameters[7].Value = model.CBBMMC;
			parameters[8].Value = model.CBRGH;
			parameters[9].Value = model.CBR;
			parameters[10].Value = model.ZZSJ;
			parameters[11].Value = model.JZSL;
			parameters[12].Value = model.JZWJS;
			parameters[13].Value = model.JZYS;
			parameters[14].Value = model.JZSZKJ;
			parameters[15].Value = model.FQL;
			parameters[16].Value = model.ZJYCXGSJ;
			parameters[17].Value = model.ZJYCXGRGH;
			parameters[18].Value = model.ZJYCXGRXM;
			parameters[19].Value = model.SFSC;
			parameters[20].Value = model.BMSAH;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Update(EDRS.Model.YX_DZJZ_JZAJXX model)", "EDRS.OracleDAL.YX_DZJZ_JZAJXX", strSql.ToString(), parameters);
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
		public bool Delete(string BMSAH)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from YX_DZJZ_JZAJXX ");
			strSql.Append(" where BMSAH=:BMSAH ");
			OracleParameter[] parameters = {
					new OracleParameter(":BMSAH", OracleType.VarChar,100)			};
			parameters[0].Value = BMSAH;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Delete(string BMSAH)", "EDRS.OracleDAL.YX_DZJZ_JZAJXX", strSql.ToString(), parameters);
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
		public bool DeleteList(string BMSAHlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from YX_DZJZ_JZAJXX ");
			strSql.Append(" where BMSAH in ("+BMSAHlist + ")  ");
            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString());
            }
            catch (Exception ex)
            {
                System.Collections.Generic.Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
                parameters.Add("BMSAH", BMSAHlist);
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool DeleteList(string BMSAHlist )", "EDRS.OracleDAL.YX_DZJZ_JZAJXX", strSql.ToString(), parameters);
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
        public EDRS.Model.YX_DZJZ_JZAJXX GetModel(string BMSAH)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select BMSAH,TYSAH,AJMC,AJLB_BM,AJLB_MC,CBDWBM,CBDWMC,CBBMBM,CBBMMC,CBRGH,CBR,ZZSJ,JZSL,JZWJS,JZYS,JZSZKJ,FQL,ZJYCXGSJ,ZJYCXGRGH,ZJYCXGRXM,SFSC from YX_DZJZ_JZAJXX ");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
            strSql.Append(" where BMSAH=:BMSAH ");
            OracleParameter[] parameters = {
					new OracleParameter(":BMSAH", OracleType.VarChar,100)			};
            parameters[0].Value = BMSAH;

            EDRS.Model.YX_DZJZ_JZAJXX model = new EDRS.Model.YX_DZJZ_JZAJXX();
            DataSet ds = null;
            try
            {
                ds = DbHelperOra.Query(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public EDRS.Model.YX_DZJZ_JZAJXX GetModel(string BMSAH)", "EDRS.OracleDAL.YX_DZJZ_JZAJXX", strSql.ToString(), parameters);
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
		public EDRS.Model.YX_DZJZ_JZAJXX DataRowToModel(DataRow row)
		{
			EDRS.Model.YX_DZJZ_JZAJXX model=new EDRS.Model.YX_DZJZ_JZAJXX();
			if (row != null)
			{
				if(row["BMSAH"]!=null)
				{
					model.BMSAH=row["BMSAH"].ToString();
				}
				if(row["TYSAH"]!=null)
				{
					model.TYSAH=row["TYSAH"].ToString();
				}
				if(row["AJMC"]!=null)
				{
					model.AJMC=row["AJMC"].ToString();
				}
				if(row["AJLB_BM"]!=null)
				{
					model.AJLB_BM=row["AJLB_BM"].ToString();
				}
				if(row["AJLB_MC"]!=null)
				{
					model.AJLB_MC=row["AJLB_MC"].ToString();
				}
				if(row["CBDWBM"]!=null)
				{
					model.CBDWBM=row["CBDWBM"].ToString();
				}
				if(row["CBDWMC"]!=null)
				{
					model.CBDWMC=row["CBDWMC"].ToString();
				}
				if(row["CBBMBM"]!=null)
				{
					model.CBBMBM=row["CBBMBM"].ToString();
				}
				if(row["CBBMMC"]!=null)
				{
					model.CBBMMC=row["CBBMMC"].ToString();
				}
				if(row["CBRGH"]!=null)
				{
					model.CBRGH=row["CBRGH"].ToString();
				}
				if(row["CBR"]!=null)
				{
					model.CBR=row["CBR"].ToString();
				}
				if(row["ZZSJ"]!=null && row["ZZSJ"].ToString()!="")
				{
					model.ZZSJ=DateTime.Parse(row["ZZSJ"].ToString());
				}
				if(row["JZSL"]!=null && row["JZSL"].ToString()!="")
				{
					model.JZSL=decimal.Parse(row["JZSL"].ToString());
				}
				if(row["JZWJS"]!=null && row["JZWJS"].ToString()!="")
				{
					model.JZWJS=decimal.Parse(row["JZWJS"].ToString());
				}
				if(row["JZYS"]!=null && row["JZYS"].ToString()!="")
				{
					model.JZYS=decimal.Parse(row["JZYS"].ToString());
				}
				if(row["JZSZKJ"]!=null && row["JZSZKJ"].ToString()!="")
				{
					model.JZSZKJ=decimal.Parse(row["JZSZKJ"].ToString());
				}
				if(row["FQL"]!=null)
				{
					model.FQL=row["FQL"].ToString();
				}
				if(row["ZJYCXGSJ"]!=null && row["ZJYCXGSJ"].ToString()!="")
				{
					model.ZJYCXGSJ=DateTime.Parse(row["ZJYCXGSJ"].ToString());
				}
				if(row["ZJYCXGRGH"]!=null)
				{
					model.ZJYCXGRGH=row["ZJYCXGRGH"].ToString();
				}
				if(row["ZJYCXGRXM"]!=null)
				{
					model.ZJYCXGRXM=row["ZJYCXGRXM"].ToString();
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
			strSql.Append("select BMSAH,TYSAH,AJMC,AJLB_BM,AJLB_MC,CBDWBM,CBDWMC,CBBMBM,CBBMMC,CBRGH,CBR,ZZSJ,JZSL,JZWJS,JZYS,JZSZKJ,FQL,ZJYCXGSJ,ZJYCXGRGH,ZJYCXGRXM,SFSC ");
			strSql.Append(" FROM YX_DZJZ_JZAJXX ");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetList(string strWhere, params object[] objValues)", "EDRS.OracleDAL.YX_DZJZ_JZAJXX", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
        public int GetRecordCount(string strWhere, params object[] objValues)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM YX_DZJZ_JZAJXX ");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public int GetRecordCount(string strWhere, params object[] objValues)", "EDRS.OracleDAL.YX_DZJZ_JZAJXX", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
				strSql.Append("order by T.BMSAH desc");
			}
			strSql.AppendFormat(")AS Ro, T.*  from YX_DZJZ_JZAJXX{0} T ",ConfigHelper.GetConfigString("OrcDBLinq"));
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)", "EDRS.OracleDAL.YX_DZJZ_JZAJXX", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
			parameters[0].Value = "YX_DZJZ_JZAJXX";
			parameters[1].Value = "BMSAH";
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

