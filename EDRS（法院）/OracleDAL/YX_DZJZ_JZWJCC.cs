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
	/// 数据访问类:YX_DZJZ_JZWJCC
	/// </summary>
	public partial class YX_DZJZ_JZWJCC:IYX_DZJZ_JZWJCC
    {
        public System.Web.HttpRequest context = null;
        public void SetHttpContext(System.Web.HttpRequest _context)
        {
            this.context = _context;
        }
		public YX_DZJZ_JZWJCC()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string BMSAH,string JZWJBH)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from YX_DZJZ_JZWJCC");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
			strSql.Append(" where BMSAH=:BMSAH and JZWJBH=:JZWJBH ");
			OracleParameter[] parameters = {
					new OracleParameter(":BMSAH", OracleType.VarChar,100),
					new OracleParameter(":JZWJBH", OracleType.VarChar,50)			};
			parameters[0].Value = BMSAH;
			parameters[1].Value = JZWJBH;
            try
            {
                return DbHelperOra.Exists(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Exists(string BMSAH,string JZWJBH)", "EDRS.OracleDAL.YX_DZJZ_JZWJCC", strSql.ToString(), parameters);
            }
            return false;
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EDRS.Model.YX_DZJZ_JZWJCC model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into YX_DZJZ_JZWJCC(");
			strSql.Append("BMSAH,TYSAH,JZWJBH,JZWJMC,JZWJXSMC,JZWJCFLJ,JZWJSCSJ,JZWJMD5,JZWJDX,FQL,SFSC,CJSJ,XZCS,WJHZ,WJSXH,WJBZXX,JZWJXXMC,JZMLBH,JBH)");
			strSql.Append(" values (");
			strSql.Append(":BMSAH,:TYSAH,:JZWJBH,:JZWJMC,:JZWJXSMC,:JZWJCFLJ,:JZWJSCSJ,:JZWJMD5,:JZWJDX,:FQL,:SFSC,:CJSJ,:XZCS,:WJHZ,:WJSXH,:WJBZXX,:JZWJXXMC,:JZMLBH,:JBH)");
			OracleParameter[] parameters = {
					new OracleParameter(":BMSAH", OracleType.VarChar,100),
					new OracleParameter(":TYSAH", OracleType.VarChar,100),
					new OracleParameter(":JZWJBH", OracleType.VarChar,50),
					new OracleParameter(":JZWJMC", OracleType.VarChar,300),
					new OracleParameter(":JZWJXSMC", OracleType.VarChar,300),
					new OracleParameter(":JZWJCFLJ", OracleType.VarChar,300),
					new OracleParameter(":JZWJSCSJ", OracleType.DateTime),
					new OracleParameter(":JZWJMD5", OracleType.VarChar,40),
					new OracleParameter(":JZWJDX", OracleType.Number,12),
					new OracleParameter(":FQL", OracleType.Char,4),
					new OracleParameter(":SFSC", OracleType.Char,1),
					new OracleParameter(":CJSJ", OracleType.DateTime),
					new OracleParameter(":XZCS", OracleType.Number,4),
					new OracleParameter(":WJHZ", OracleType.VarChar,20),
					new OracleParameter(":WJSXH", OracleType.Number,4),
					new OracleParameter(":WJBZXX", OracleType.VarChar,300),
					new OracleParameter(":JZWJXXMC", OracleType.VarChar,300),
					new OracleParameter(":JZMLBH", OracleType.VarChar,40),
					new OracleParameter(":JBH", OracleType.VarChar,40)};
			parameters[0].Value = model.BMSAH;
			parameters[1].Value = model.TYSAH;
			parameters[2].Value = model.JZWJBH;
			parameters[3].Value = model.JZWJMC;
			parameters[4].Value = model.JZWJXSMC;
			parameters[5].Value = model.JZWJCFLJ;
			parameters[6].Value = model.JZWJSCSJ;
			parameters[7].Value = model.JZWJMD5;
			parameters[8].Value = model.JZWJDX;
			parameters[9].Value = model.FQL;
			parameters[10].Value = model.SFSC;
			parameters[11].Value = model.CJSJ;
			parameters[12].Value = model.XZCS;
			parameters[13].Value = model.WJHZ;
			parameters[14].Value = model.WJSXH;
			parameters[15].Value = model.WJBZXX;
			parameters[16].Value = model.JZWJXXMC;
			parameters[17].Value = model.JZMLBH;
			parameters[18].Value = model.JBH;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Add(EDRS.Model.YX_DZJZ_JZWJCC model)", "EDRS.OracleDAL.YX_DZJZ_JZWJCC", strSql.ToString(), parameters);
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
		public bool Update(EDRS.Model.YX_DZJZ_JZWJCC model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update YX_DZJZ_JZWJCC set ");
			strSql.Append("TYSAH=:TYSAH,");
			strSql.Append("JZWJMC=:JZWJMC,");
			strSql.Append("JZWJXSMC=:JZWJXSMC,");
			strSql.Append("JZWJCFLJ=:JZWJCFLJ,");
			strSql.Append("JZWJSCSJ=:JZWJSCSJ,");
			strSql.Append("JZWJMD5=:JZWJMD5,");
			strSql.Append("JZWJDX=:JZWJDX,");
			strSql.Append("FQL=:FQL,");
			strSql.Append("SFSC=:SFSC,");
			strSql.Append("CJSJ=:CJSJ,");
			strSql.Append("XZCS=:XZCS,");
			strSql.Append("WJHZ=:WJHZ,");
			strSql.Append("WJSXH=:WJSXH,");
			strSql.Append("WJBZXX=:WJBZXX,");
			strSql.Append("JZWJXXMC=:JZWJXXMC,");
			strSql.Append("JZMLBH=:JZMLBH,");
			strSql.Append("JBH=:JBH");
			strSql.Append(" where BMSAH=:BMSAH and JZWJBH=:JZWJBH ");
			OracleParameter[] parameters = {
					new OracleParameter(":TYSAH", OracleType.VarChar,100),
					new OracleParameter(":JZWJMC", OracleType.VarChar,300),
					new OracleParameter(":JZWJXSMC", OracleType.VarChar,300),
					new OracleParameter(":JZWJCFLJ", OracleType.VarChar,300),
					new OracleParameter(":JZWJSCSJ", OracleType.DateTime),
					new OracleParameter(":JZWJMD5", OracleType.VarChar,40),
					new OracleParameter(":JZWJDX", OracleType.Number,12),
					new OracleParameter(":FQL", OracleType.Char,4),
					new OracleParameter(":SFSC", OracleType.Char,1),
					new OracleParameter(":CJSJ", OracleType.DateTime),
					new OracleParameter(":XZCS", OracleType.Number,4),
					new OracleParameter(":WJHZ", OracleType.VarChar,20),
					new OracleParameter(":WJSXH", OracleType.Number,4),
					new OracleParameter(":WJBZXX", OracleType.VarChar,300),
					new OracleParameter(":JZWJXXMC", OracleType.VarChar,300),
					new OracleParameter(":JZMLBH", OracleType.VarChar,40),
					new OracleParameter(":JBH", OracleType.VarChar,40),
					new OracleParameter(":BMSAH", OracleType.VarChar,100),
					new OracleParameter(":JZWJBH", OracleType.VarChar,50)};
			parameters[0].Value = model.TYSAH;
			parameters[1].Value = model.JZWJMC;
			parameters[2].Value = model.JZWJXSMC;
			parameters[3].Value = model.JZWJCFLJ;
			parameters[4].Value = model.JZWJSCSJ;
			parameters[5].Value = model.JZWJMD5;
			parameters[6].Value = model.JZWJDX;
			parameters[7].Value = model.FQL;
			parameters[8].Value = model.SFSC;
			parameters[9].Value = model.CJSJ;
			parameters[10].Value = model.XZCS;
			parameters[11].Value = model.WJHZ;
			parameters[12].Value = model.WJSXH;
			parameters[13].Value = model.WJBZXX;
			parameters[14].Value = model.JZWJXXMC;
			parameters[15].Value = model.JZMLBH;
			parameters[16].Value = model.JBH;
			parameters[17].Value = model.BMSAH;
			parameters[18].Value = model.JZWJBH;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Update(EDRS.Model.YX_DZJZ_JZWJCC model)", "EDRS.OracleDAL.YX_DZJZ_JZWJCC", strSql.ToString(), parameters);
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
		public bool Delete(string BMSAH,string JZWJBH)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from YX_DZJZ_JZWJCC ");
			strSql.Append(" where BMSAH=:BMSAH and JZWJBH=:JZWJBH ");
			OracleParameter[] parameters = {
					new OracleParameter(":BMSAH", OracleType.VarChar,100),
					new OracleParameter(":JZWJBH", OracleType.VarChar,50)			};
			parameters[0].Value = BMSAH;
			parameters[1].Value = JZWJBH;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Delete(string BMSAH,string JZWJBH)", "EDRS.OracleDAL.YX_DZJZ_JZWJCC", strSql.ToString(), parameters);
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
		public EDRS.Model.YX_DZJZ_JZWJCC GetModel(string BMSAH,string JZWJBH)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select BMSAH,TYSAH,JZWJBH,JZWJMC,JZWJXSMC,JZWJCFLJ,JZWJSCSJ,JZWJMD5,JZWJDX,FQL,SFSC,CJSJ,XZCS,WJHZ,WJSXH,WJBZXX,JZWJXXMC,JZMLBH,JBH from YX_DZJZ_JZWJCC ");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
            strSql.Append(" where BMSAH=:BMSAH and JZWJBH=:JZWJBH ");
			OracleParameter[] parameters = {
					new OracleParameter(":BMSAH", OracleType.VarChar,100),
					new OracleParameter(":JZWJBH", OracleType.VarChar,50)			};
			parameters[0].Value = BMSAH;
			parameters[1].Value = JZWJBH;

			EDRS.Model.YX_DZJZ_JZWJCC model=new EDRS.Model.YX_DZJZ_JZWJCC();
            DataSet ds = null;
            try
            {
                ds = DbHelperOra.Query(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public EDRS.Model.YX_DZJZ_JZWJCC GetModel(string BMSAH,string JZWJBH)", "EDRS.OracleDAL.YX_DZJZ_JZWJCC", strSql.ToString(), parameters);
            }
			if(ds != null && ds.Tables[0].Rows.Count>0)
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
		public EDRS.Model.YX_DZJZ_JZWJCC DataRowToModel(DataRow row)
		{
			EDRS.Model.YX_DZJZ_JZWJCC model=new EDRS.Model.YX_DZJZ_JZWJCC();
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
				if(row["JZWJBH"]!=null)
				{
					model.JZWJBH=row["JZWJBH"].ToString();
				}
				if(row["JZWJMC"]!=null)
				{
					model.JZWJMC=row["JZWJMC"].ToString();
				}
				if(row["JZWJXSMC"]!=null)
				{
					model.JZWJXSMC=row["JZWJXSMC"].ToString();
				}
				if(row["JZWJCFLJ"]!=null)
				{
					model.JZWJCFLJ=row["JZWJCFLJ"].ToString();
				}
				if(row["JZWJSCSJ"]!=null && row["JZWJSCSJ"].ToString()!="")
				{
					model.JZWJSCSJ=DateTime.Parse(row["JZWJSCSJ"].ToString());
				}
				if(row["JZWJMD5"]!=null)
				{
					model.JZWJMD5=row["JZWJMD5"].ToString();
				}
				if(row["JZWJDX"]!=null && row["JZWJDX"].ToString()!="")
				{
					model.JZWJDX=decimal.Parse(row["JZWJDX"].ToString());
				}
				if(row["FQL"]!=null)
				{
					model.FQL=row["FQL"].ToString();
				}
				if(row["SFSC"]!=null)
				{
					model.SFSC=row["SFSC"].ToString();
				}
				if(row["CJSJ"]!=null && row["CJSJ"].ToString()!="")
				{
					model.CJSJ=DateTime.Parse(row["CJSJ"].ToString());
				}
				if(row["XZCS"]!=null && row["XZCS"].ToString()!="")
				{
					model.XZCS=decimal.Parse(row["XZCS"].ToString());
				}
				if(row["WJHZ"]!=null)
				{
					model.WJHZ=row["WJHZ"].ToString();
				}
				if(row["WJSXH"]!=null && row["WJSXH"].ToString()!="")
				{
					model.WJSXH=decimal.Parse(row["WJSXH"].ToString());
				}
				if(row["WJBZXX"]!=null)
				{
					model.WJBZXX=row["WJBZXX"].ToString();
				}
				if(row["JZWJXXMC"]!=null)
				{
					model.JZWJXXMC=row["JZWJXXMC"].ToString();
				}
				if(row["JZMLBH"]!=null)
				{
					model.JZMLBH=row["JZMLBH"].ToString();
				}
				if(row["JBH"]!=null)
				{
					model.JBH=row["JBH"].ToString();
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
			strSql.Append("select BMSAH,TYSAH,JZWJBH,JZWJMC,JZWJXSMC,JZWJCFLJ,JZWJSCSJ,JZWJMD5,JZWJDX,FQL,SFSC,CJSJ,XZCS,WJHZ,WJSXH,WJBZXX,JZWJXXMC,JZMLBH,JBH ");
			strSql.Append(" FROM YX_DZJZ_JZWJCC ");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetList(string strWhere, params object[] objValues)", "EDRS.OracleDAL.YX_DZJZ_JZWJCC", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
        public int GetRecordCount(string strWhere, params object[] objValues)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM YX_DZJZ_JZWJCC ");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public int GetRecordCount(string strWhere, params object[] objValues)", "EDRS.OracleDAL.YX_DZJZ_JZWJCC", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
				strSql.Append("order by T.JZWJBH desc");
			}
			strSql.AppendFormat(")AS Ro, T.*  from YX_DZJZ_JZWJCC{0} T ",ConfigHelper.GetConfigString("OrcDBLinq"));
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)", "EDRS.OracleDAL.YX_DZJZ_JZWJCC", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
			parameters[0].Value = "YX_DZJZ_JZWJCC";
			parameters[1].Value = "JZWJBH";
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

