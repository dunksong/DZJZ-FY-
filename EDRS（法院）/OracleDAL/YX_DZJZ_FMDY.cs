using System;
using System.Data;
using System.Text;
using System.Data.OracleClient;
using EDRS.IDAL;
using Maticsoft.DBUtility;//Please add references
namespace EDRS.OracleDAL
{
	/// <summary>
	/// 数据访问类:YX_DZJZ_FMDY
	/// </summary>
	public partial class YX_DZJZ_FMDY:IYX_DZJZ_FMDY
	{
        public System.Web.HttpRequest context = null;
        public void SetHttpContext(System.Web.HttpRequest _context)
        {
            this.context = _context;
        }
		public YX_DZJZ_FMDY()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string BM)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from YX_DZJZ_FMDY");
			strSql.Append(" where BM=:BM ");
			OracleParameter[] parameters = {
					new OracleParameter(":BM", OracleType.NVarChar)			};
			parameters[0].Value = BM;

			return DbHelperOra.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EDRS.Model.YX_DZJZ_FMDY model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into YX_DZJZ_FMDY(");
			strSql.Append("BT,FBT,AJMC,AJBH,FZXYR,LASJ,JASJ,LJDW,LJR,SHR,BAGJ,DJJ,GJY,CZRGH,CZR,CZSJ,CZIP,CZLX)");
			strSql.Append(" values (");
			strSql.Append(":BT,:FBT,:AJMC,:AJBH,:FZXYR,:LASJ,:JASJ,:LJDW,:LJR,:SHR,:BAGJ,:DJJ,:GJY,:CZRGH,:CZR,:CZSJ,:CZIP,:CZLX)");
			OracleParameter[] parameters = {
					new OracleParameter(":BT", OracleType.VarChar,500),
					new OracleParameter(":FBT", OracleType.VarChar,500),
					new OracleParameter(":AJMC", OracleType.VarChar,500),
					new OracleParameter(":AJBH", OracleType.VarChar,500),
					new OracleParameter(":FZXYR", OracleType.VarChar,2000),
					new OracleParameter(":LASJ", OracleType.DateTime),
					new OracleParameter(":JASJ", OracleType.DateTime),
					new OracleParameter(":LJDW", OracleType.VarChar,300),
					new OracleParameter(":LJR", OracleType.VarChar,300),
					new OracleParameter(":SHR", OracleType.VarChar,300),
					new OracleParameter(":BAGJ", OracleType.Number,18),
					new OracleParameter(":DJJ", OracleType.VarChar,50),
					new OracleParameter(":GJY", OracleType.Number,18),
					new OracleParameter(":CZRGH", OracleType.Char,4),
					new OracleParameter(":CZR", OracleType.VarChar,60),
					new OracleParameter(":CZSJ", OracleType.DateTime),
					new OracleParameter(":CZIP", OracleType.VarChar,20),
					new OracleParameter(":CZLX", OracleType.VarChar,2)};

            parameters[0].Value = model.BT;
            parameters[1].Value = model.FBT;
            parameters[2].Value = model.AJMC;
            parameters[3].Value = model.AJBH;
            parameters[4].Value = model.FZXYR;
            parameters[5].Value = model.LASJ  ?? (object)DBNull.Value;
            parameters[6].Value = model.JASJ??(object)DBNull.Value;
            parameters[7].Value = model.LJDW;
            parameters[8].Value = model.LJR;
            parameters[9].Value = model.SHR;
            parameters[10].Value = model.BAGJ;
            parameters[11].Value = model.DJJ;
            parameters[12].Value = model.GJY;
            parameters[13].Value = model.CZRGH;
            parameters[14].Value = model.CZR;
            parameters[15].Value = model.CZSJ;
            parameters[16].Value = model.CZIP;
            parameters[17].Value = model.CZLX;

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
		public bool Update(EDRS.Model.YX_DZJZ_FMDY model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update YX_DZJZ_FMDY set ");
			strSql.Append("BT=:BT,");
			strSql.Append("FBT=:FBT,");
			strSql.Append("AJMC=:AJMC,");
			strSql.Append("AJBH=:AJBH,");
			strSql.Append("FZXYR=:FZXYR,");
			strSql.Append("LASJ=:LASJ,");
			strSql.Append("JASJ=:JASJ,");
			strSql.Append("LJDW=:LJDW,");
			strSql.Append("LJR=:LJR,");
			strSql.Append("SHR=:SHR,");
			strSql.Append("BAGJ=:BAGJ,");
			strSql.Append("DJJ=:DJJ,");
			strSql.Append("GJY=:GJY,");
			strSql.Append("CZRGH=:CZRGH,");
			strSql.Append("CZR=:CZR,");
			strSql.Append("CZSJ=:CZSJ,");
			strSql.Append("CZIP=:CZIP,");
			strSql.Append("CZLX=:CZLX");
			strSql.Append(" where BM=:BM ");
			OracleParameter[] parameters = {
					new OracleParameter(":BT", OracleType.VarChar,500),
					new OracleParameter(":FBT", OracleType.VarChar,500),
					new OracleParameter(":AJMC", OracleType.VarChar,500),
					new OracleParameter(":AJBH", OracleType.VarChar,500),
					new OracleParameter(":FZXYR", OracleType.VarChar,2000),
					new OracleParameter(":LASJ", OracleType.DateTime),
					new OracleParameter(":JASJ", OracleType.DateTime),
					new OracleParameter(":LJDW", OracleType.VarChar,300),
					new OracleParameter(":LJR", OracleType.VarChar,300),
					new OracleParameter(":SHR", OracleType.VarChar,300),
					new OracleParameter(":BAGJ", OracleType.Number,18),
					new OracleParameter(":DJJ", OracleType.VarChar,50),
					new OracleParameter(":GJY", OracleType.Number,18),
					new OracleParameter(":CZRGH", OracleType.Char,4),
					new OracleParameter(":CZR", OracleType.VarChar,60),
					new OracleParameter(":CZSJ", OracleType.DateTime),
					new OracleParameter(":CZIP", OracleType.VarChar,20),
					new OracleParameter(":CZLX", OracleType.VarChar,2),
					new OracleParameter(":BM", OracleType.NVarChar)};
			parameters[0].Value = model.BT;
			parameters[1].Value = model.FBT;
			parameters[2].Value = model.AJMC;
			parameters[3].Value = model.AJBH;
			parameters[4].Value = model.FZXYR;
			parameters[5].Value = model.LASJ;
			parameters[6].Value = model.JASJ;
			parameters[7].Value = model.LJDW;
			parameters[8].Value = model.LJR;
			parameters[9].Value = model.SHR;
			parameters[10].Value = model.BAGJ;
			parameters[11].Value = model.DJJ;
			parameters[12].Value = model.GJY;
			parameters[13].Value = model.CZRGH;
			parameters[14].Value = model.CZR;
			parameters[15].Value = model.CZSJ;
			parameters[16].Value = model.CZIP;
			parameters[17].Value = model.CZLX;
			parameters[18].Value = model.BM;

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
			strSql.Append("delete from YX_DZJZ_FMDY ");
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
			strSql.Append("delete from YX_DZJZ_FMDY ");
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
		public EDRS.Model.YX_DZJZ_FMDY GetModel(string BM)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select BM,BT,FBT,AJMC,AJBH,FZXYR,LASJ,JASJ,LJDW,LJR,SHR,BAGJ,DJJ,GJY,CZRGH,CZR,CZSJ,CZIP,CZLX from YX_DZJZ_FMDY ");
			strSql.Append(" where BM=:BM ");
			OracleParameter[] parameters = {
					new OracleParameter(":BM", OracleType.NVarChar)			};
			parameters[0].Value = BM;

			EDRS.Model.YX_DZJZ_FMDY model=new EDRS.Model.YX_DZJZ_FMDY();
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
		public EDRS.Model.YX_DZJZ_FMDY DataRowToModel(DataRow row)
		{
			EDRS.Model.YX_DZJZ_FMDY model=new EDRS.Model.YX_DZJZ_FMDY();
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
				if(row["FBT"]!=null)
				{
					model.FBT=row["FBT"].ToString();
				}
				if(row["AJMC"]!=null)
				{
					model.AJMC=row["AJMC"].ToString();
				}
				if(row["AJBH"]!=null)
				{
					model.AJBH=row["AJBH"].ToString();
				}
				if(row["FZXYR"]!=null)
				{
					model.FZXYR=row["FZXYR"].ToString();
				}
				if(row["LASJ"]!=null && row["LASJ"].ToString()!="")
				{
					model.LASJ=DateTime.Parse(row["LASJ"].ToString());
				}
				if(row["JASJ"]!=null && row["JASJ"].ToString()!="")
				{
					model.JASJ=DateTime.Parse(row["JASJ"].ToString());
				}
				if(row["LJDW"]!=null)
				{
					model.LJDW=row["LJDW"].ToString();
				}
				if(row["LJR"]!=null)
				{
					model.LJR=row["LJR"].ToString();
				}
				if(row["SHR"]!=null)
				{
					model.SHR=row["SHR"].ToString();
				}
				if(row["BAGJ"]!=null && row["BAGJ"].ToString()!="")
				{
					model.BAGJ=decimal.Parse(row["BAGJ"].ToString());
				}
				if(row["DJJ"]!=null)
				{
					model.DJJ=row["DJJ"].ToString();
				}
				if(row["GJY"]!=null && row["GJY"].ToString()!="")
				{
					model.GJY=decimal.Parse(row["GJY"].ToString());
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
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
        public DataSet GetList(string strWhere, params object[] objValues)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select BM,BT,FBT,AJMC,AJBH,FZXYR,LASJ,JASJ,LJDW,LJR,SHR,BAGJ,DJJ,GJY,CZRGH,CZR,CZSJ,CZIP,CZLX ");
            strSql.Append(" FROM YX_DZJZ_FMDY ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            try
            {
                return DbHelperOra.Query(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetList(string strWhere, params object[] objValues)", "EDRS.OracleDAL.YX_DZJZ_FMDY", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
        }

		/// <summary>
		/// 获取记录总数
		/// </summary>
        public int GetRecordCount(string strWhere, params object[] objValues)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM YX_DZJZ_FMDY ");
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.BM desc");
            }
            strSql.Append(")AS Ro, T.*  from YX_DZJZ_FMDY T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Ro between {0} and {1}", startIndex, endIndex);
            try
            {
                return DbHelperOra.Query(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)", "EDRS.OracleDAL.YX_DZJZ_FMDY", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
			parameters[0].Value = "YX_DZJZ_FMDY";
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

