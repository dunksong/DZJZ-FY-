using System;
using System.Data;
using System.Text;
using System.Data.OracleClient;
using EDRS.IDAL;
using Maticsoft.DBUtility;//Please add references
namespace EDRS.OracleDAL
{
	/// <summary>
	/// 数据访问类:YX_DZJZ_FMDYMB
	/// </summary>
	public partial class YX_DZJZ_FMDYMB:IYX_DZJZ_FMDYMB
    {
        public System.Web.HttpRequest context = null;
        public void SetHttpContext(System.Web.HttpRequest _context)
        {
            this.context = _context;
        }
		public YX_DZJZ_FMDYMB()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string BM)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from YX_DZJZ_FMDYMB");
			strSql.Append(" where BM=:BM ");
			OracleParameter[] parameters = {
					new OracleParameter(":BM", OracleType.NVarChar)			};
			parameters[0].Value = BM;

			return DbHelperOra.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EDRS.Model.YX_DZJZ_FMDYMB model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into YX_DZJZ_FMDYMB(");
            strSql.Append("BT,SFMR,CZRGH,CZR,CZSJ,CZIP,SFSC,NR)");
			strSql.Append(" values (");
			strSql.Append(":BT,:SFMR,:CZRGH,:CZR,:CZSJ,:CZIP,:SFSC,:NR)");
			OracleParameter[] parameters = {
					new OracleParameter(":BT", OracleType.VarChar,500),
					new OracleParameter(":SFMR", OracleType.Char,1),
					new OracleParameter(":CZRGH", OracleType.Char,4),
					new OracleParameter(":CZR", OracleType.VarChar,60),
					new OracleParameter(":CZSJ", OracleType.DateTime),
					new OracleParameter(":CZIP", OracleType.VarChar,20),
					new OracleParameter(":SFSC", OracleType.Char,1),
					new OracleParameter(":NR", OracleType.VarChar,4000)};
                                          
            parameters[0].Value = model.BT;
            parameters[1].Value = model.SFMR;
            parameters[2].Value = model.CZRGH;
            parameters[3].Value = model.CZR;
            parameters[4].Value = model.CZSJ;
            parameters[5].Value = model.CZIP;
            parameters[6].Value = model.SFSC;
            parameters[7].Value = model.NR;

			int rows=DbHelperOra.ExecuteSql(strSql.ToString(),parameters);
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
		public bool Update(EDRS.Model.YX_DZJZ_FMDYMB model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update YX_DZJZ_FMDYMB set ");
			strSql.Append("BT=:BT,");
			strSql.Append("SFMR=:SFMR,");
			strSql.Append("CZRGH=:CZRGH,");
			strSql.Append("CZR=:CZR,");
			strSql.Append("CZSJ=:CZSJ,");
			strSql.Append("CZIP=:CZIP,");
            strSql.Append("SFSC=:SFSC,");
            strSql.Append("NR=:NR");
			strSql.Append(" where BM=:BM ");
			OracleParameter[] parameters = {
					new OracleParameter(":BT", OracleType.VarChar,500),
					new OracleParameter(":SFMR", OracleType.Char,1),
					new OracleParameter(":CZRGH", OracleType.Char,4),
					new OracleParameter(":CZR", OracleType.VarChar,60),
					new OracleParameter(":CZSJ", OracleType.DateTime),
					new OracleParameter(":CZIP", OracleType.VarChar,20),
					new OracleParameter(":SFSC", OracleType.Char,1),
					new OracleParameter(":BM", OracleType.NVarChar),
					new OracleParameter(":NR", OracleType.VarChar,4000)};
			parameters[0].Value = model.BT;
			parameters[1].Value = model.SFMR;
			parameters[2].Value = model.CZRGH;
			parameters[3].Value = model.CZR;
			parameters[4].Value = model.CZSJ;
			parameters[5].Value = model.CZIP;
			parameters[6].Value = model.SFSC;
            parameters[7].Value = model.BM;
            parameters[8].Value = model.NR;

			int rows=DbHelperOra.ExecuteSql(strSql.ToString(),parameters);
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
		public bool Delete(string BM)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from YX_DZJZ_FMDYMB ");
			strSql.Append(" where BM=:BM ");
			OracleParameter[] parameters = {
					new OracleParameter(":BM", OracleType.NVarChar)			};
			parameters[0].Value = BM;

			int rows=DbHelperOra.ExecuteSql(strSql.ToString(),parameters);
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
		public bool DeleteList(string BMlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from YX_DZJZ_FMDYMB ");
			strSql.Append(" where BM in ("+BMlist + ")  ");
			int rows=DbHelperOra.ExecuteSql(strSql.ToString());
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
		public EDRS.Model.YX_DZJZ_FMDYMB GetModel(string BM)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select BM,BT,SFMR,CZRGH,CZR,CZSJ,CZIP,SFSC,NR from YX_DZJZ_FMDYMB ");
			strSql.Append(" where BM=:BM ");
			OracleParameter[] parameters = {
					new OracleParameter(":BM", OracleType.NVarChar)			};
			parameters[0].Value = BM;

			EDRS.Model.YX_DZJZ_FMDYMB model=new EDRS.Model.YX_DZJZ_FMDYMB();
			DataSet ds=DbHelperOra.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
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
		public EDRS.Model.YX_DZJZ_FMDYMB DataRowToModel(DataRow row)
		{
			EDRS.Model.YX_DZJZ_FMDYMB model=new EDRS.Model.YX_DZJZ_FMDYMB();
			if (row != null)
			{
				if(row["BM"]!=null)
				{
					model.BM=row["BM"].ToString();
				}
				if(row["BT"]!=null)
				{
					model.BT=row["BT"].ToString();
				}
                if (row["NR"] != null)
                {
                    model.NR = row["NR"].ToString();
                }
				if(row["SFMR"]!=null)
				{
					model.SFMR=row["SFMR"].ToString();
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
            strSql.Append("select BM,BT,SFMR,CZRGH,CZR,CZSJ,CZIP,SFSC,NR ");
			strSql.Append(" FROM YX_DZJZ_FMDYMB ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            return DbHelperOra.Query(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
        public int GetRecordCount(string strWhere, params object[] objValues)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM YX_DZJZ_FMDYMB ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            object obj = DbHelperOra.GetSingle(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
				strSql.Append("order by T.BM desc");
			}
			strSql.Append(")AS Ro, T.*  from YX_DZJZ_FMDYMB T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Ro between {0} and {1}", startIndex, endIndex);
            return DbHelperOra.Query(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
			parameters[0].Value = "YX_DZJZ_FMDYMB";
			parameters[1].Value = "BM";
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

