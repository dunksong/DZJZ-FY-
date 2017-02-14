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
	/// 数据访问类:YX_AG_YJAP
	/// </summary>
	public partial class YX_AG_YJAP:IYX_AG_YJAP
    {
        public System.Web.HttpRequest context = null;
        public void SetHttpContext(System.Web.HttpRequest _context)
        {
            this.context = _context;
        }
		public YX_AG_YJAP()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(decimal YJBH)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from YX_AG_YJAP");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
			strSql.Append(" where YJBH=:YJBH ");
			OracleParameter[] parameters = {
					new OracleParameter(":YJBH", OracleType.Number,22)			};
			parameters[0].Value = YJBH;
            try
            {
                return DbHelperOra.Exists(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Exists(decimal YJBH)", "EDRS.OracleDAL.YX_AG_YJAP", strSql.ToString(), parameters);
            }
            return false;
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EDRS.Model.YX_AG_YJAP model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into YX_AG_YJAP(");
			strSql.Append("BMSAH,DJBH,YJBH,YJKSSJ,YJJZSJ,JZZTXS,YJFS,FZFS,FYDYSL,SFSC,CJSJ,ZHXGSJ,FQDWBM,FQL,CBDW_BM,CBDW_MC)");
			strSql.Append(" values (");
			strSql.Append(":BMSAH,:DJBH,:YJBH,:YJKSSJ,:YJJZSJ,:JZZTXS,:YJFS,:FZFS,:FYDYSL,:SFSC,:CJSJ,:ZHXGSJ,:FQDWBM,:FQL,:CBDW_BM,:CBDW_MC)");
			OracleParameter[] parameters = {
					new OracleParameter(":BMSAH", OracleType.VarChar,100),
					new OracleParameter(":DJBH", OracleType.Number,4),
					new OracleParameter(":YJBH", OracleType.Number,4),
					new OracleParameter(":YJKSSJ", OracleType.DateTime),
					new OracleParameter(":YJJZSJ", OracleType.DateTime),
					new OracleParameter(":JZZTXS", OracleType.Char,13),
					new OracleParameter(":YJFS", OracleType.Char,13),
					new OracleParameter(":FZFS", OracleType.Char,13),
					new OracleParameter(":FYDYSL", OracleType.Number,4),
					new OracleParameter(":SFSC", OracleType.Char,1),
					new OracleParameter(":CJSJ", OracleType.DateTime),
					new OracleParameter(":ZHXGSJ", OracleType.DateTime),
					new OracleParameter(":FQDWBM", OracleType.VarChar,50),
					new OracleParameter(":FQL", OracleType.VarChar,6),
					new OracleParameter(":CBDW_BM", OracleType.VarChar,50),
					new OracleParameter(":CBDW_MC", OracleType.VarChar,300)};
			parameters[0].Value = model.BMSAH;
			parameters[1].Value = model.DJBH;
			parameters[2].Value = model.YJBH;
			parameters[3].Value = model.YJKSSJ;
			parameters[4].Value = model.YJJZSJ;
			parameters[5].Value = model.JZZTXS;
			parameters[6].Value = model.YJFS;
			parameters[7].Value = model.FZFS;
			parameters[8].Value = model.FYDYSL;
			parameters[9].Value = model.SFSC;
			parameters[10].Value = model.CJSJ;
			parameters[11].Value = model.ZHXGSJ;
			parameters[12].Value = model.FQDWBM;
			parameters[13].Value = model.FQL;
			parameters[14].Value = model.CBDW_BM;
			parameters[15].Value = model.CBDW_MC;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Add(EDRS.Model.YX_AG_YJAP model)", "EDRS.OracleDAL.YX_AG_YJAP", strSql.ToString(), parameters);
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
		public bool Update(EDRS.Model.YX_AG_YJAP model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update YX_AG_YJAP set ");
			strSql.Append("BMSAH=:BMSAH,");
			strSql.Append("DJBH=:DJBH,");
			strSql.Append("YJKSSJ=:YJKSSJ,");
			strSql.Append("YJJZSJ=:YJJZSJ,");
			strSql.Append("JZZTXS=:JZZTXS,");
			strSql.Append("YJFS=:YJFS,");
			strSql.Append("FZFS=:FZFS,");
			strSql.Append("FYDYSL=:FYDYSL,");
			strSql.Append("SFSC=:SFSC,");
			strSql.Append("CJSJ=:CJSJ,");
			strSql.Append("ZHXGSJ=:ZHXGSJ,");
			strSql.Append("FQDWBM=:FQDWBM,");
			strSql.Append("FQL=:FQL,");
			strSql.Append("CBDW_BM=:CBDW_BM,");
			strSql.Append("CBDW_MC=:CBDW_MC");
			strSql.Append(" where YJBH=:YJBH ");
			OracleParameter[] parameters = {
					new OracleParameter(":BMSAH", OracleType.VarChar,100),
					new OracleParameter(":DJBH", OracleType.Number,4),
					new OracleParameter(":YJKSSJ", OracleType.DateTime),
					new OracleParameter(":YJJZSJ", OracleType.DateTime),
					new OracleParameter(":JZZTXS", OracleType.Char,13),
					new OracleParameter(":YJFS", OracleType.Char,13),
					new OracleParameter(":FZFS", OracleType.Char,13),
					new OracleParameter(":FYDYSL", OracleType.Number,4),
					new OracleParameter(":SFSC", OracleType.Char,1),
					new OracleParameter(":CJSJ", OracleType.DateTime),
					new OracleParameter(":ZHXGSJ", OracleType.DateTime),
					new OracleParameter(":FQDWBM", OracleType.VarChar,50),
					new OracleParameter(":FQL", OracleType.VarChar,6),
					new OracleParameter(":CBDW_BM", OracleType.VarChar,50),
					new OracleParameter(":CBDW_MC", OracleType.VarChar,300),
					new OracleParameter(":YJBH", OracleType.Number,4)};
			parameters[0].Value = model.BMSAH;
			parameters[1].Value = model.DJBH;
			parameters[2].Value = model.YJKSSJ;
			parameters[3].Value = model.YJJZSJ;
			parameters[4].Value = model.JZZTXS;
			parameters[5].Value = model.YJFS;
			parameters[6].Value = model.FZFS;
			parameters[7].Value = model.FYDYSL;
			parameters[8].Value = model.SFSC;
			parameters[9].Value = model.CJSJ;
			parameters[10].Value = model.ZHXGSJ;
			parameters[11].Value = model.FQDWBM;
			parameters[12].Value = model.FQL;
			parameters[13].Value = model.CBDW_BM;
			parameters[14].Value = model.CBDW_MC;
			parameters[15].Value = model.YJBH;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Update(EDRS.Model.YX_AG_YJAP model)", "EDRS.OracleDAL.YX_AG_YJAP", strSql.ToString(), parameters);
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
		public bool Delete(decimal YJBH)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from YX_AG_YJAP ");
			strSql.Append(" where YJBH=:YJBH ");
			OracleParameter[] parameters = {
					new OracleParameter(":YJBH", OracleType.Number,22)			};
			parameters[0].Value = YJBH;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Delete(decimal YJBH)", "EDRS.OracleDAL.YX_AG_YJAP", strSql.ToString(), parameters);
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
		public bool DeleteList(string YJBHlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from YX_AG_YJAP ");
			strSql.Append(" where YJBH in ("+YJBHlist + ")  ");
            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString());
            }
            catch (Exception ex)
            {
                System.Collections.Generic.Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
                parameters.Add("YJBH", YJBHlist);
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool DeleteList(string YJBHlist )", "EDRS.OracleDAL.YX_AG_YJAP", strSql.ToString(), parameters);
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
		public EDRS.Model.YX_AG_YJAP GetModel(decimal YJBH)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select BMSAH,DJBH,YJBH,YJKSSJ,YJJZSJ,JZZTXS,YJFS,FZFS,FYDYSL,SFSC,CJSJ,ZHXGSJ,FQDWBM,FQL,CBDW_BM,CBDW_MC from YX_AG_YJAP ");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
            strSql.Append(" where YJBH=:YJBH ");
			OracleParameter[] parameters = {
					new OracleParameter(":YJBH", OracleType.Number,22)			};
			parameters[0].Value = YJBH;

			EDRS.Model.YX_AG_YJAP model=new EDRS.Model.YX_AG_YJAP();
            DataSet ds = null;
            try
            {
                ds = DbHelperOra.Query(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public EDRS.Model.YX_AG_YJAP GetModel(decimal YJBH)", "EDRS.OracleDAL.YX_AG_YJAP", strSql.ToString(), parameters);
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
		public EDRS.Model.YX_AG_YJAP DataRowToModel(DataRow row)
		{
			EDRS.Model.YX_AG_YJAP model=new EDRS.Model.YX_AG_YJAP();
			if (row != null)
			{
				if(row["BMSAH"]!=null)
				{
					model.BMSAH=row["BMSAH"].ToString();
				}
				if(row["DJBH"]!=null && row["DJBH"].ToString()!="")
				{
					model.DJBH=decimal.Parse(row["DJBH"].ToString());
				}
				if(row["YJBH"]!=null && row["YJBH"].ToString()!="")
				{
					model.YJBH=decimal.Parse(row["YJBH"].ToString());
				}
				if(row["YJKSSJ"]!=null && row["YJKSSJ"].ToString()!="")
				{
					model.YJKSSJ=DateTime.Parse(row["YJKSSJ"].ToString());
				}
				if(row["YJJZSJ"]!=null && row["YJJZSJ"].ToString()!="")
				{
					model.YJJZSJ=DateTime.Parse(row["YJJZSJ"].ToString());
				}
				if(row["JZZTXS"]!=null)
				{
					model.JZZTXS=row["JZZTXS"].ToString();
				}
				if(row["YJFS"]!=null)
				{
					model.YJFS=row["YJFS"].ToString();
				}
				if(row["FZFS"]!=null)
				{
					model.FZFS=row["FZFS"].ToString();
				}
				if(row["FYDYSL"]!=null && row["FYDYSL"].ToString()!="")
				{
					model.FYDYSL=decimal.Parse(row["FYDYSL"].ToString());
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
				if(row["CBDW_BM"]!=null)
				{
					model.CBDW_BM=row["CBDW_BM"].ToString();
				}
				if(row["CBDW_MC"]!=null)
				{
					model.CBDW_MC=row["CBDW_MC"].ToString();
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
			strSql.Append("select BMSAH,DJBH,YJBH,YJKSSJ,YJJZSJ,JZZTXS,YJFS,FZFS,FYDYSL,SFSC,CJSJ,ZHXGSJ,FQDWBM,FQL,CBDW_BM,CBDW_MC ");
			strSql.Append(" FROM YX_AG_YJAP ");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetList(string strWhere, params object[] objValues)", "EDRS.OracleDAL.YX_AG_YJAP", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
        public int GetRecordCount(string strWhere, params object[] objValues)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM YX_AG_YJAP ");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public int GetRecordCount(string strWhere, params object[] objValues)", "EDRS.OracleDAL.YX_AG_YJAP", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
				strSql.Append("order by T.YJBH desc");
			}
			strSql.AppendFormat(")AS Ro, T.*  from YX_AG_YJAP{0} T ",ConfigHelper.GetConfigString("OrcDBLinq"));
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)", "EDRS.OracleDAL.YX_AG_YJAP", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
			parameters[0].Value = "YX_AG_YJAP";
			parameters[1].Value = "YJBH";
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

