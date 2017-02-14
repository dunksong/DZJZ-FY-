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
	/// 数据访问类:YX_DZJZ_WJSQDY
	/// </summary>
	public partial class YX_DZJZ_WJSQDY:IYX_DZJZ_WJSQDY
    {
        public System.Web.HttpRequest context = null;
        public void SetHttpContext(System.Web.HttpRequest _context)
        {
            this.context = _context;
        }
		public YX_DZJZ_WJSQDY()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string XH)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from YX_DZJZ_WJSQDY");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
			strSql.Append(" where XH=:XH ");
			OracleParameter[] parameters = {
					new OracleParameter(":XH", OracleType.VarChar,10)			};
			parameters[0].Value = XH;
            try
            {
                return DbHelperOra.Exists(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Exists(string XH)", "EDRS.OracleDAL.YX_DZJZ_WJSQDY", strSql.ToString(), parameters);
            }
            return false;
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EDRS.Model.YX_DZJZ_WJSQDY model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into YX_DZJZ_WJSQDY(");
			strSql.Append("LSZH,BMSAH,YJXH,JZWJBH,SQSJ,SQFS,DYSJ,DYFS,DYFY,DYR,DYRGH,DYBMBM,DYBMMC,DYDWBM,DYDWMC,SFSC,DYSQDH)");
			strSql.Append(" values (");
			strSql.Append(":LSZH,:BMSAH,:YJXH,:JZWJBH,:SQSJ,:SQFS,:DYSJ,:DYFS,:DYFY,:DYR,:DYRGH,:DYBMBM,:DYBMMC,:DYDWBM,:DYDWMC,:SFSC,:DYSQDH)");
			OracleParameter[] parameters = {
					new OracleParameter(":LSZH", OracleType.VarChar,100),
					new OracleParameter(":BMSAH", OracleType.VarChar,100),
					new OracleParameter(":YJXH", OracleType.VarChar,50),
					new OracleParameter(":JZWJBH", OracleType.VarChar,50),
					new OracleParameter(":SQSJ", OracleType.DateTime),
					new OracleParameter(":SQFS", OracleType.Number,4),
					new OracleParameter(":DYSJ", OracleType.DateTime),
					new OracleParameter(":DYFS", OracleType.Number,4),
					new OracleParameter(":DYFY", OracleType.Number,8),
					new OracleParameter(":DYR", OracleType.VarChar,60),
					new OracleParameter(":DYRGH", OracleType.Char,4),
					new OracleParameter(":DYBMBM", OracleType.Char,4),
					new OracleParameter(":DYBMMC", OracleType.VarChar,300),
					new OracleParameter(":DYDWBM", OracleType.VarChar,50),
					new OracleParameter(":DYDWMC", OracleType.VarChar,300),
					new OracleParameter(":SFSC", OracleType.Char,1),
					new OracleParameter(":DYSQDH", OracleType.VarChar,50)};
			parameters[0].Value = model.LSZH;
			parameters[1].Value = model.BMSAH;
			parameters[2].Value = model.YJXH;
			parameters[3].Value = model.JZWJBH;
			parameters[4].Value = model.SQSJ;
			parameters[5].Value = model.SQFS;
			parameters[6].Value = model.DYSJ ?? (object)DBNull.Value;
            parameters[7].Value = model.DYFS ?? (object)DBNull.Value;
			parameters[8].Value = model.DYFY ?? (object)DBNull.Value;
			parameters[9].Value = model.DYR;
			parameters[10].Value = model.DYRGH;
			parameters[11].Value = model.DYBMBM;
			parameters[12].Value = model.DYBMMC;
			parameters[13].Value = model.DYDWBM;
			parameters[14].Value = model.DYDWMC;
			parameters[15].Value = model.SFSC;
			parameters[16].Value = model.DYSQDH;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Add(EDRS.Model.YX_DZJZ_WJSQDY model)", "EDRS.OracleDAL.YX_DZJZ_WJSQDY", strSql.ToString(), parameters);
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
		public bool Update(EDRS.Model.YX_DZJZ_WJSQDY model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update YX_DZJZ_WJSQDY set ");
			strSql.Append("LSZH=:LSZH,");
			strSql.Append("BMSAH=:BMSAH,");
			strSql.Append("YJXH=:YJXH,");
			strSql.Append("JZWJBH=:JZWJBH,");
			strSql.Append("SQSJ=:SQSJ,");
			strSql.Append("SQFS=:SQFS,");
			strSql.Append("DYSJ=:DYSJ,");
			strSql.Append("DYFS=:DYFS,");
			strSql.Append("DYFY=:DYFY,");
			strSql.Append("DYR=:DYR,");
			strSql.Append("DYRGH=:DYRGH,");
			strSql.Append("DYBMBM=:DYBMBM,");
			strSql.Append("DYBMMC=:DYBMMC,");
			strSql.Append("DYDWBM=:DYDWBM,");
			strSql.Append("DYDWMC=:DYDWMC,");
			strSql.Append("SFSC=:SFSC,");
			strSql.Append("DYSQDH=:DYSQDH");
			strSql.Append(" where XH=:XH ");
			OracleParameter[] parameters = {
					new OracleParameter(":LSZH", OracleType.VarChar,100),
					new OracleParameter(":BMSAH", OracleType.VarChar,100),
					new OracleParameter(":YJXH", OracleType.VarChar,50),
					new OracleParameter(":JZWJBH", OracleType.VarChar,50),
					new OracleParameter(":SQSJ", OracleType.DateTime),
					new OracleParameter(":SQFS", OracleType.Number,4),
					new OracleParameter(":DYSJ", OracleType.DateTime),
					new OracleParameter(":DYFS", OracleType.Number,4),
					new OracleParameter(":DYFY", OracleType.Number,8),
					new OracleParameter(":DYR", OracleType.VarChar,60),
					new OracleParameter(":DYRGH", OracleType.Char,4),
					new OracleParameter(":DYBMBM", OracleType.Char,4),
					new OracleParameter(":DYBMMC", OracleType.VarChar,300),
					new OracleParameter(":DYDWBM", OracleType.VarChar,50),
					new OracleParameter(":DYDWMC", OracleType.VarChar,300),
					new OracleParameter(":SFSC", OracleType.Char,1),
					new OracleParameter(":DYSQDH", OracleType.VarChar,50),
					new OracleParameter(":XH", OracleType.VarChar,50)};
			parameters[0].Value = model.LSZH;
			parameters[1].Value = model.BMSAH;
			parameters[2].Value = model.YJXH;
			parameters[3].Value = model.JZWJBH;
			parameters[4].Value = model.SQSJ;
			parameters[5].Value = model.SQFS;
			parameters[6].Value = model.DYSJ;
			parameters[7].Value = model.DYFS;
			parameters[8].Value = model.DYFY;
			parameters[9].Value = model.DYR;
			parameters[10].Value = model.DYRGH;
			parameters[11].Value = model.DYBMBM;
			parameters[12].Value = model.DYBMMC;
			parameters[13].Value = model.DYDWBM;
			parameters[14].Value = model.DYDWMC;
			parameters[15].Value = model.SFSC;
			parameters[16].Value = model.DYSQDH;
			parameters[17].Value = model.XH;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Update(EDRS.Model.YX_DZJZ_WJSQDY model)", "EDRS.OracleDAL.YX_DZJZ_WJSQDY", strSql.ToString(), parameters);
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
		public bool Delete(string XH)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from YX_DZJZ_WJSQDY ");
			strSql.Append(" where XH=:XH ");
			OracleParameter[] parameters = {
					new OracleParameter(":XH", OracleType.VarChar,50)			};
			parameters[0].Value = XH;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Delete(string XH)", "EDRS.OracleDAL.YX_DZJZ_WJSQDY", strSql.ToString(), parameters);
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
		public bool DeleteList(string XHlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from YX_DZJZ_WJSQDY ");
			strSql.Append(" where XH in ("+XHlist + ")  ");
            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString());
            }
            catch (Exception ex)
            {
                System.Collections.Generic.Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
                parameters.Add("XH", XHlist);
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool DeleteList(string XHlist )", "EDRS.OracleDAL.YX_DZJZ_WJSQDY", strSql.ToString(), parameters);
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
        public EDRS.Model.YX_DZJZ_WJSQDY GetModel(string XH)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select LSZH,BMSAH,YJXH,JZWJBH,SQSJ,SQFS,DYSJ,DYFS,DYFY,DYR,DYRGH,DYBMBM,DYBMMC,DYDWBM,DYDWMC,SFSC,DYSQDH,XH from YX_DZJZ_WJSQDY ");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
            strSql.Append(" where XH=:XH ");
            OracleParameter[] parameters = {
					new OracleParameter(":XH", OracleType.VarChar,50)			};
            parameters[0].Value = XH;

            EDRS.Model.YX_DZJZ_WJSQDY model = new EDRS.Model.YX_DZJZ_WJSQDY();
            DataSet ds = null;
            try
            {
                ds = DbHelperOra.Query(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "ppublic EDRS.Model.YX_DZJZ_WJSQDY GetModel(string XH)", "EDRS.OracleDAL.YX_DZJZ_WJSQDY", strSql.ToString(), parameters);
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
		public EDRS.Model.YX_DZJZ_WJSQDY DataRowToModel(DataRow row)
		{
			EDRS.Model.YX_DZJZ_WJSQDY model=new EDRS.Model.YX_DZJZ_WJSQDY();
			if (row != null)
			{
				if(row["LSZH"]!=null)
				{
					model.LSZH=row["LSZH"].ToString();
				}
				if(row["BMSAH"]!=null)
				{
					model.BMSAH=row["BMSAH"].ToString();
				}
				if(row["YJXH"]!=null && row["YJXH"].ToString()!="")
				{
					model.YJXH=row["YJXH"].ToString();
				}
				if(row["JZWJBH"]!=null)
				{
					model.JZWJBH=row["JZWJBH"].ToString();
				}
				if(row["SQSJ"]!=null && row["SQSJ"].ToString()!="")
				{
					model.SQSJ=DateTime.Parse(row["SQSJ"].ToString());
				}
				if(row["SQFS"]!=null && row["SQFS"].ToString()!="")
				{
					model.SQFS=decimal.Parse(row["SQFS"].ToString());
				}
				if(row["DYSJ"]!=null && row["DYSJ"].ToString()!="")
				{
					model.DYSJ=DateTime.Parse(row["DYSJ"].ToString());
				}
				if(row["DYFS"]!=null && row["DYFS"].ToString()!="")
				{
					model.DYFS=decimal.Parse(row["DYFS"].ToString());
				}
				if(row["DYFY"]!=null && row["DYFY"].ToString()!="")
				{
					model.DYFY=decimal.Parse(row["DYFY"].ToString());
				}
				if(row["DYR"]!=null)
				{
					model.DYR=row["DYR"].ToString();
				}
				if(row["DYRGH"]!=null)
				{
					model.DYRGH=row["DYRGH"].ToString();
				}
				if(row["DYBMBM"]!=null)
				{
					model.DYBMBM=row["DYBMBM"].ToString();
				}
				if(row["DYBMMC"]!=null)
				{
					model.DYBMMC=row["DYBMMC"].ToString();
				}
				if(row["DYDWBM"]!=null)
				{
					model.DYDWBM=row["DYDWBM"].ToString();
				}
				if(row["DYDWMC"]!=null)
				{
					model.DYDWMC=row["DYDWMC"].ToString();
				}
				if(row["SFSC"]!=null)
				{
					model.SFSC=row["SFSC"].ToString();
				}
				if(row["DYSQDH"]!=null)
				{
					model.DYSQDH=row["DYSQDH"].ToString();
				}
				if(row["XH"]!=null)
				{
					model.XH=row["XH"].ToString();
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
			strSql.Append("select LSZH,BMSAH,YJXH,JZWJBH,SQSJ,SQFS,DYSJ,DYFS,DYFY,DYR,DYRGH,DYBMBM,DYBMMC,DYDWBM,DYDWMC,SFSC,DYSQDH,XH ");
			strSql.Append(" FROM YX_DZJZ_WJSQDY ");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetList(string strWhere, params object[] objValues)", "EDRS.OracleDAL.YX_DZJZ_WJSQDY", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
        public int GetRecordCount(string strWhere, params object[] objValues)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM YX_DZJZ_WJSQDY T  ");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq")); 
            if (strWhere.Trim() != "")
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public int GetRecordCount(string strWhere, params object[] objValues)", "EDRS.OracleDAL.YX_DZJZ_WJSQDY", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
				strSql.Append("order by T.XH desc");
			}
			strSql.AppendFormat(")AS Ro, T.*  from YX_DZJZ_WJSQDY{0} T ",ConfigHelper.GetConfigString("OrcDBLinq"));
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)", "EDRS.OracleDAL.YX_DZJZ_WJSQDY", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
			parameters[0].Value = "YX_DZJZ_WJSQDY";
			parameters[1].Value = "XH";
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

