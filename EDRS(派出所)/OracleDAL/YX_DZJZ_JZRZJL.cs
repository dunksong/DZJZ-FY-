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
	/// 数据访问类:YX_DZJZ_JZRZJL
	/// </summary>
	public partial class YX_DZJZ_JZRZJL:IYX_DZJZ_JZRZJL
    {
        public System.Web.HttpRequest context = null;
        public void SetHttpContext(System.Web.HttpRequest _context)
        {
            this.context = _context;
        }
		public YX_DZJZ_JZRZJL()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(decimal XH)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from YX_DZJZ_JZRZJL");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
			strSql.Append(" where XH=:XH ");
			OracleParameter[] parameters = {
					new OracleParameter(":XH", OracleType.Number,22)			};
			parameters[0].Value = XH;

            try
            {
                return DbHelperOra.Exists(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Exists(decimal XH)", "EDRS.OracleDAL.YX_DZJZ_JZRZJL", strSql.ToString(), parameters);
            }
            return false;
		}

        #region 方法
        /// <summary>
        /// 取新ID
        /// </summary>
        /// <param name="dbs">配置信息</param>
        /// <param name="tablename">表名</param>
        /// <returns>ID值</returns>
        public int NewTableID(string tablename = "YX_DZJZ_JZRZJL")
        {
            string sql = "";//"select * from sqlite_master where type = 'table' and name = 'yy_tableid'";
            sql = String.Format("select TableID from MC_TABLEID where NAME = '{0}'", tablename);
            object returnValue = DbHelperOra.GetSingle(sql);
            int newid = 1;
            if (returnValue == null || returnValue == DBNull.Value)
            {
                sql = String.Format("insert into MC_TABLEID (NAME,TABLEID) values ('{0}',2)", tablename);
                DbHelperOra.ExecuteSql(sql);
            }
            else
            {
                newid = int.Parse(returnValue.ToString());
                int rid = newid + 1;
                sql = String.Format("update MC_TABLEID set TABLEID = {0} where NAME = '{1}'", rid, tablename);
                DbHelperOra.ExecuteSql(sql);
                newid = rid;
            }
            return newid;
        }
        #endregion

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EDRS.Model.YX_DZJZ_JZRZJL model)
		{
            //删除日志
            LogClearTable();

            model.CZSJ = DateTime.Now;
            //代理IP
            model.CZIP = context.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(model.CZIP))
            {
                //真实IP
                model.CZIP = context.ServerVariables["REMOTE_ADDR"];
            }
			StringBuilder strSql=new StringBuilder();
            //获取序号
            //try
            //{
            //    model.XH = NewTableID();
            //}
            //catch (Exception ex)
            //{
            //    EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Add(EDRS.Model.YX_DZJZ_JZRZJL model)|DbHelperOra.GetMaxID(\"XH\", \"YX_DZJZ_JZRZJL\")", "EDRS.OracleDAL.YX_DZJZ_JZRZJL", strSql.ToString());
            //    return false;
            //}
            string tableName = LogTable();
            if (string.IsNullOrEmpty(tableName))
                tableName = "YX_DZJZ_JZRZJL";

            strSql.AppendFormat("insert into {0}(", tableName);
			strSql.Append("DWBM,DWMC,BMBM,BMMC,CZRGH,CZR,CZSJ,CZIP,CZLX,RZNR,CZAJBMSAH,FQL)");
			strSql.Append(" values (");
			strSql.Append(":DWBM,:DWMC,:BMBM,:BMMC,:CZRGH,:CZR,:CZSJ,:CZIP,:CZLX,:RZNR,:CZAJBMSAH,:FQL)");
			OracleParameter[] parameters = {
				//	new OracleParameter(":XH", OracleType.Number,4),
					new OracleParameter(":DWBM", OracleType.VarChar,50),
					new OracleParameter(":DWMC", OracleType.VarChar,300),
					new OracleParameter(":BMBM", OracleType.Char,10),
					new OracleParameter(":BMMC", OracleType.VarChar,300),
					new OracleParameter(":CZRGH", OracleType.Char,4),
					new OracleParameter(":CZR", OracleType.VarChar,60),
					new OracleParameter(":CZSJ", OracleType.DateTime),
					new OracleParameter(":CZIP", OracleType.VarChar,20),
					new OracleParameter(":CZLX", OracleType.Char,2),
					new OracleParameter(":RZNR", OracleType.VarChar,300),
					new OracleParameter(":CZAJBMSAH", OracleType.VarChar,100),
					new OracleParameter(":FQL", OracleType.Char,4)};
			//parameters[0].Value = model.XH;
			parameters[0].Value = (object)model.DWBM??DBNull.Value;
			parameters[1].Value = model.DWMC;
			parameters[2].Value = model.BMBM;
			parameters[3].Value = model.BMMC;
            parameters[4].Value = (object)model.CZRGH ?? DBNull.Value;
            parameters[5].Value = (object)model.CZR ?? DBNull.Value;
			parameters[6].Value = model.CZSJ;
			parameters[7].Value = model.CZIP;
			parameters[8].Value = model.CZLX;
			parameters[9].Value = model.RZNR;
			parameters[10].Value = model.CZAJBMSAH;
			parameters[11].Value = DateTime.Now.Year;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Add(EDRS.Model.YX_DZJZ_JZRZJL model)", "EDRS.OracleDAL.YX_DZJZ_JZRZJL", strSql.ToString(), parameters);
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
		public bool Update(EDRS.Model.YX_DZJZ_JZRZJL model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update YX_DZJZ_JZRZJL set ");
			strSql.Append("DWBM=:DWBM,");
			strSql.Append("DWMC=:DWMC,");
			strSql.Append("BMBM=:BMBM,");
			strSql.Append("BMMC=:BMMC,");
			strSql.Append("CZRGH=:CZRGH,");
			strSql.Append("CZR=:CZR,");
			strSql.Append("CZSJ=:CZSJ,");
			strSql.Append("CZIP=:CZIP,");
			strSql.Append("CZLX=:CZLX,");
			strSql.Append("RZNR=:RZNR,");
			strSql.Append("CZAJBMSAH=:CZAJBMSAH,");
			strSql.Append("FQL=:FQL");
			strSql.Append(" where XH=:XH ");
			OracleParameter[] parameters = {
					new OracleParameter(":DWBM", OracleType.VarChar,50),
					new OracleParameter(":DWMC", OracleType.VarChar,300),
					new OracleParameter(":BMBM", OracleType.Char,10),
					new OracleParameter(":BMMC", OracleType.VarChar,300),
					new OracleParameter(":CZRGH", OracleType.Char,4),
					new OracleParameter(":CZR", OracleType.VarChar,60),
					new OracleParameter(":CZSJ", OracleType.DateTime),
					new OracleParameter(":CZIP", OracleType.VarChar,20),
					new OracleParameter(":CZLX", OracleType.Char,2),
					new OracleParameter(":RZNR", OracleType.VarChar,300),
					new OracleParameter(":CZAJBMSAH", OracleType.VarChar,100),
					new OracleParameter(":FQL", OracleType.Char,4),
					new OracleParameter(":XH", OracleType.Number,4)};
			parameters[0].Value = model.DWBM;
			parameters[1].Value = model.DWMC;
			parameters[2].Value = model.BMBM;
			parameters[3].Value = model.BMMC;
			parameters[4].Value = model.CZRGH;
			parameters[5].Value = model.CZR;
			parameters[6].Value = model.CZSJ;
			parameters[7].Value = model.CZIP;
			parameters[8].Value = model.CZLX;
			parameters[9].Value = model.RZNR;
			parameters[10].Value = model.CZAJBMSAH;
			parameters[11].Value = model.FQL;
			parameters[12].Value = model.XH;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Update(EDRS.Model.YX_DZJZ_JZRZJL model)", "EDRS.OracleDAL.YX_DZJZ_JZRZJL", strSql.ToString(), parameters);
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
		public bool Delete(decimal XH)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from YX_DZJZ_JZRZJL ");
			strSql.Append(" where XH=:XH ");
			OracleParameter[] parameters = {
					new OracleParameter(":XH", OracleType.Number,22)			};
			parameters[0].Value = XH;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Delete(decimal XH)", "EDRS.OracleDAL.YX_DZJZ_JZRZJL", strSql.ToString(), parameters);
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
			strSql.Append("delete from YX_DZJZ_JZRZJL ");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool DeleteList(string XHlist )", "EDRS.OracleDAL.YX_DZJZ_JZRZJL", strSql.ToString(), parameters);
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
        public EDRS.Model.YX_DZJZ_JZRZJL GetModel(decimal XH)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select XH,DWBM,DWMC,BMBM,BMMC,CZRGH,CZR,CZSJ,CZIP,CZLX,RZNR,CZAJBMSAH,FQL from YX_DZJZ_JZRZJL ");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
            strSql.Append(" where XH=:XH ");
            OracleParameter[] parameters = {
					new OracleParameter(":XH", OracleType.Number,22)			};
            parameters[0].Value = XH;

            EDRS.Model.YX_DZJZ_JZRZJL model = new EDRS.Model.YX_DZJZ_JZRZJL();
            DataSet ds = null;
            try
            {
                ds = DbHelperOra.Query(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public EDRS.Model.YX_DZJZ_JZRZJL GetModel(decimal XH)", "EDRS.OracleDAL.YX_DZJZ_JZRZJL", strSql.ToString(), parameters);
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
		public EDRS.Model.YX_DZJZ_JZRZJL DataRowToModel(DataRow row)
		{
			EDRS.Model.YX_DZJZ_JZRZJL model=new EDRS.Model.YX_DZJZ_JZRZJL();
			if (row != null)
			{
				if(row["XH"]!=null && row["XH"].ToString()!="")
				{
					model.XH=decimal.Parse(row["XH"].ToString());
				}
				if(row["DWBM"]!=null)
				{
					model.DWBM=row["DWBM"].ToString();
				}
				if(row["DWMC"]!=null)
				{
					model.DWMC=row["DWMC"].ToString();
				}
				if(row["BMBM"]!=null)
				{
					model.BMBM=row["BMBM"].ToString();
				}
				if(row["BMMC"]!=null)
				{
					model.BMMC=row["BMMC"].ToString();
				}
				if(row["CZRGH"]!=null)
				{
					model.CZRGH=row["CZRGH"].ToString();
				}
				if(row["CZR"]!=null)
				{
					model.CZR=row["CZR"].ToString();
				}
				if(row["CZSJ"]!=null && row["CZSJ"].ToString()!="")
				{
					model.CZSJ=DateTime.Parse(row["CZSJ"].ToString());
				}
				if(row["CZIP"]!=null)
				{
					model.CZIP=row["CZIP"].ToString();
				}
				if(row["CZLX"]!=null)
				{
					model.CZLX=row["CZLX"].ToString();
				}
				if(row["RZNR"]!=null)
				{
					model.RZNR=row["RZNR"].ToString();
				}
				if(row["CZAJBMSAH"]!=null)
				{
					model.CZAJBMSAH=row["CZAJBMSAH"].ToString();
				}
				if(row["FQL"]!=null)
				{
					model.FQL=row["FQL"].ToString();
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
			strSql.Append("select XH,DWBM,DWMC,BMBM,BMMC,CZRGH,CZR,CZSJ,CZIP,CZLX,RZNR,CZAJBMSAH,FQL ");
			strSql.Append(" FROM YX_DZJZ_JZRZJL ");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetList(string strWhere, params object[] objValues)", "EDRS.OracleDAL.YX_DZJZ_JZRZJL", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
        public int GetRecordCount(string strWhere, params object[] objValues)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM YX_DZJZ_JZRZJL ");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public int GetRecordCount(string strWhere, params object[] objValues)", "EDRS.OracleDAL.YX_DZJZ_JZRZJL", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
                strSql.Append("order by T.CZSJ desc");
			}
			strSql.AppendFormat(")AS Ro, T.*  from YX_DZJZ_JZRZJL{0} T ",ConfigHelper.GetConfigString("OrcDBLinq"));
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)", "EDRS.OracleDAL.YX_DZJZ_JZRZJL", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
			parameters[0].Value = "YX_DZJZ_JZRZJL";
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

