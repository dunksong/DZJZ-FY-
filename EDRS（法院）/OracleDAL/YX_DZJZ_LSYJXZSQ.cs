using System;
using System.Data;
using System.Text;
using System.Data.OracleClient;
using EDRS.IDAL;
using Maticsoft.DBUtility;//请先添加引用
namespace EDRS.OracleDAL
{
	/// <summary>
	/// 数据访问类YX_DZJZ_LSYJXZSQ。
	/// </summary>
	public partial class YX_DZJZ_LSYJXZSQ:IYX_DZJZ_LSYJXZSQ
	{
		public YX_DZJZ_LSYJXZSQ()
		{}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string SQBM)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from YX_DZJZ_LSYJXZSQ");
			strSql.Append(" where SQBM=:SQBM ");
			OracleParameter[] parameters = {
					new OracleParameter(":SQBM", OracleType.VarChar,50)};
			parameters[0].Value = SQBM;

			return DbHelperOra.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EDRS.Model.YX_DZJZ_LSYJXZSQ model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into YX_DZJZ_LSYJXZSQ(");
			strSql.Append("LSZH,LSXM,SSDW,AJMC,XYRMC,SQRMC,SQDW,XZDZ,SQDZT,SQSJ,BZ,SFSC,SHRGH,SHR,SHSJ)");
			strSql.Append(" values (");
			strSql.Append(":LSZH,:LSXM,:SSDW,:AJMC,:XYRMC,:SQRMC,:SQDW,:XZDZ,:SQDZT,:SQSJ,:BZ,:SFSC,:SHRGH,:SHR,:SHSJ)");
			OracleParameter[] parameters = {
					new OracleParameter(":LSZH", OracleType.VarChar,100),
					new OracleParameter(":LSXM", OracleType.VarChar,50),
					new OracleParameter(":SSDW", OracleType.VarChar,200),
					new OracleParameter(":AJMC", OracleType.VarChar,200),
					new OracleParameter(":XYRMC", OracleType.VarChar,50),
					new OracleParameter(":SQRMC", OracleType.VarChar,50),
					new OracleParameter(":SQDW", OracleType.VarChar,200),
					new OracleParameter(":XZDZ", OracleType.VarChar,500),
					new OracleParameter(":SQDZT", OracleType.Char,1),
					new OracleParameter(":SQSJ", OracleType.DateTime),
					new OracleParameter(":BZ", OracleType.VarChar,300),
					new OracleParameter(":SFSC", OracleType.Char,1),
					new OracleParameter(":SHRGH", OracleType.Char,4),
					new OracleParameter(":SHR", OracleType.VarChar,60),
					new OracleParameter(":SHSJ", OracleType.DateTime)};
			parameters[0].Value = model.LSZH;
			parameters[1].Value = model.LSXM;
			parameters[2].Value = model.SSDW;
			parameters[3].Value = model.AJMC;
			parameters[4].Value = model.XYRMC;
			parameters[5].Value = model.SQRMC;
			parameters[6].Value = model.SQDW;
			parameters[7].Value = model.XZDZ;
			parameters[8].Value = model.SQDZT;
			parameters[9].Value = model.SQSJ;
			parameters[10].Value = model.BZ;
			parameters[11].Value = model.SFSC;
			parameters[12].Value = model.SHRGH;
			parameters[13].Value = model.SHR;
			parameters[14].Value = model.SHSJ??(object)DBNull.Value;

			if(DbHelperOra.ExecuteSql(strSql.ToString(),parameters) > 0)
				return true;
			return false;
		}

		/// <summary>
		/// 增加多条数据
		/// </summary>
		public bool AddList(System.Collections.Generic.List<EDRS.Model.YX_DZJZ_LSYJXZSQ> models)
		{
			System.Collections.Hashtable hash = new System.Collections.Hashtable();
			foreach (EDRS.Model.YX_DZJZ_LSYJXZSQ model in models)
			{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into YX_DZJZ_LSYJXZSQ(");
			strSql.Append("SQBM,LSZH,LSXM,SSDW,AJMC,XYRMC,SQRMC,SQDW,XZDZ,SQDZT,SQSJ,BZ,SFSC,SHRGH,SHR,SHSJ)");
			strSql.Append(" values (");
			strSql.Append(":SQBM,:LSZH,:LSXM,:SSDW,:AJMC,:XYRMC,:SQRMC,:SQDW,:XZDZ,:SQDZT,:SQSJ,:BZ,:SFSC,:SHRGH,:SHR,:SHSJ)");
			OracleParameter[] parameters = {
					new OracleParameter(":SQBM", OracleType.VarChar,50),
					new OracleParameter(":LSZH", OracleType.VarChar,100),
					new OracleParameter(":LSXM", OracleType.VarChar,50),
					new OracleParameter(":SSDW", OracleType.VarChar,200),
					new OracleParameter(":AJMC", OracleType.VarChar,200),
					new OracleParameter(":XYRMC", OracleType.VarChar,50),
					new OracleParameter(":SQRMC", OracleType.VarChar,50),
					new OracleParameter(":SQDW", OracleType.VarChar,200),
					new OracleParameter(":XZDZ", OracleType.VarChar,500),
					new OracleParameter(":SQDZT", OracleType.Char,1),
					new OracleParameter(":SQSJ", OracleType.DateTime),
					new OracleParameter(":BZ", OracleType.VarChar,300),
					new OracleParameter(":SFSC", OracleType.Char,1),
					new OracleParameter(":SHRGH", OracleType.Char,4),
					new OracleParameter(":SHR", OracleType.VarChar,60),
					new OracleParameter(":SHSJ", OracleType.DateTime)};
			parameters[0].Value = model.SQBM;
			parameters[1].Value = model.LSZH;
			parameters[2].Value = model.LSXM;
			parameters[3].Value = model.SSDW;
			parameters[4].Value = model.AJMC;
			parameters[5].Value = model.XYRMC;
			parameters[6].Value = model.SQRMC;
			parameters[7].Value = model.SQDW;
			parameters[8].Value = model.XZDZ;
			parameters[9].Value = model.SQDZT;
			parameters[10].Value = model.SQSJ;
			parameters[11].Value = model.BZ;
			parameters[12].Value = model.SFSC;
			parameters[13].Value = model.SHRGH;
			parameters[14].Value = model.SHR;
			parameters[15].Value = model.SHSJ;

				hash.Add(strSql.ToString(), parameters);
			};
			if(DbHelperOra.ExecuteSqlTran(hash))
				return true;
			return false;
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(EDRS.Model.YX_DZJZ_LSYJXZSQ model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update YX_DZJZ_LSYJXZSQ set ");
			strSql.Append("LSZH=:LSZH,");
			strSql.Append("LSXM=:LSXM,");
			strSql.Append("SSDW=:SSDW,");
			strSql.Append("AJMC=:AJMC,");
			strSql.Append("XYRMC=:XYRMC,");
			strSql.Append("SQRMC=:SQRMC,");
			strSql.Append("SQDW=:SQDW,");
			strSql.Append("XZDZ=:XZDZ,");
			strSql.Append("SQDZT=:SQDZT,");
			strSql.Append("SQSJ=:SQSJ,");
			strSql.Append("BZ=:BZ,");
			strSql.Append("SFSC=:SFSC,");
			strSql.Append("SHRGH=:SHRGH,");
			strSql.Append("SHR=:SHR,");
			strSql.Append("SHSJ=:SHSJ");
			strSql.Append(" where SQBM=:SQBM ");
			OracleParameter[] parameters = {
					new OracleParameter(":SQBM", OracleType.VarChar,50),
					new OracleParameter(":LSZH", OracleType.VarChar,100),
					new OracleParameter(":LSXM", OracleType.VarChar,50),
					new OracleParameter(":SSDW", OracleType.VarChar,200),
					new OracleParameter(":AJMC", OracleType.VarChar,200),
					new OracleParameter(":XYRMC", OracleType.VarChar,50),
					new OracleParameter(":SQRMC", OracleType.VarChar,50),
					new OracleParameter(":SQDW", OracleType.VarChar,200),
					new OracleParameter(":XZDZ", OracleType.VarChar,500),
					new OracleParameter(":SQDZT", OracleType.Char,1),
					new OracleParameter(":SQSJ", OracleType.DateTime),
					new OracleParameter(":BZ", OracleType.VarChar,300),
					new OracleParameter(":SFSC", OracleType.Char,1),
					new OracleParameter(":SHRGH", OracleType.Char,4),
					new OracleParameter(":SHR", OracleType.VarChar,60),
					new OracleParameter(":SHSJ", OracleType.DateTime)};
			parameters[0].Value = model.SQBM;
			parameters[1].Value = model.LSZH;
			parameters[2].Value = model.LSXM;
			parameters[3].Value = model.SSDW;
			parameters[4].Value = model.AJMC;
			parameters[5].Value = model.XYRMC;
			parameters[6].Value = model.SQRMC;
			parameters[7].Value = model.SQDW;
			parameters[8].Value = model.XZDZ;
			parameters[9].Value = model.SQDZT;
			parameters[10].Value = model.SQSJ;
			parameters[11].Value = model.BZ;
			parameters[12].Value = model.SFSC;
			parameters[13].Value = model.SHRGH;
			parameters[14].Value = model.SHR;
            parameters[15].Value = model.SHSJ ?? (object)DBNull.Value;

			if(DbHelperOra.ExecuteSql(strSql.ToString(),parameters) > 0)
				return true;
			return false;
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string SQBM)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from YX_DZJZ_LSYJXZSQ ");
			strSql.Append(" where SQBM=:SQBM ");
			OracleParameter[] parameters = {
					new OracleParameter(":SQBM", OracleType.VarChar,50)};
			parameters[0].Value = SQBM;

			if(DbHelperOra.ExecuteSql(strSql.ToString(),parameters) > 0)
				return true;
			return false;
		}

		/// <summary>
		/// 删除多条数据
		/// </summary>
		public bool DeleteList(string SQBMlist)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from YX_DZJZ_LSYJXZSQ ");
			strSql.Append(" where SQBM in ("+SQBMlist+")    ");
			if(DbHelperOra.ExecuteSql(strSql.ToString()) > 0)
				return true;
			return false;
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public EDRS.Model.YX_DZJZ_LSYJXZSQ GetModel(string SQBM)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select SQBM,LSZH,LSXM,SSDW,AJMC,XYRMC,SQRMC,SQDW,XZDZ,SQDZT,SQSJ,BZ,SFSC,SHRGH,SHR,SHSJ from YX_DZJZ_LSYJXZSQ ");
			strSql.Append(" where SQBM=:SQBM ");
			OracleParameter[] parameters = {
					new OracleParameter(":SQBM", OracleType.VarChar,50)};
			parameters[0].Value = SQBM;

			EDRS.Model.YX_DZJZ_LSYJXZSQ model=new EDRS.Model.YX_DZJZ_LSYJXZSQ();
			DataSet ds=DbHelperOra.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				model.SQBM=ds.Tables[0].Rows[0]["SQBM"].ToString();
				model.LSZH=ds.Tables[0].Rows[0]["LSZH"].ToString();
				model.LSXM=ds.Tables[0].Rows[0]["LSXM"].ToString();
				model.SSDW=ds.Tables[0].Rows[0]["SSDW"].ToString();
				model.AJMC=ds.Tables[0].Rows[0]["AJMC"].ToString();
				model.XYRMC=ds.Tables[0].Rows[0]["XYRMC"].ToString();
				model.SQRMC=ds.Tables[0].Rows[0]["SQRMC"].ToString();
				model.SQDW=ds.Tables[0].Rows[0]["SQDW"].ToString();
				model.XZDZ=ds.Tables[0].Rows[0]["XZDZ"].ToString();
				model.SQDZT=ds.Tables[0].Rows[0]["SQDZT"].ToString();
				if(ds.Tables[0].Rows[0]["SQSJ"].ToString()!="")
				{
					model.SQSJ=DateTime.Parse(ds.Tables[0].Rows[0]["SQSJ"].ToString());
				}
				model.BZ=ds.Tables[0].Rows[0]["BZ"].ToString();
				model.SFSC=ds.Tables[0].Rows[0]["SFSC"].ToString();
				model.SHRGH=ds.Tables[0].Rows[0]["SHRGH"].ToString();
				model.SHR=ds.Tables[0].Rows[0]["SHR"].ToString();
				if(ds.Tables[0].Rows[0]["SHSJ"].ToString()!="")
				{
					model.SHSJ=DateTime.Parse(ds.Tables[0].Rows[0]["SHSJ"].ToString());
				}
				return model;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere,params object[] objValues)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select SQBM,LSZH,LSXM,SSDW,AJMC,XYRMC,SQRMC,SQDW,XZDZ,SQDZT,SQSJ,BZ,SFSC,SHRGH,SHR,SHSJ ");
			strSql.Append(" FROM YX_DZJZ_LSYJXZSQ ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where 1=1 "+strWhere);
			}
			return DbHelperOra.Query(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public int GetRecordCount(string strWhere,params object[] objValues)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM YX_DZJZ_LSYJXZSQ");			if(strWhere.Trim()!="")
			{
				strSql.Append(" where 1=1 "+strWhere);
			}
			object obj = DbHelperOra.GetSingle(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
			if(obj == null)
				return 0;
			return Convert.ToInt32(obj);
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex,params object[] objValues)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby);
			}
			else
			{
				strSql.Append("order by T.SQBM desc");
			}
			strSql.Append(")AS Ro, T.*  from YX_DZJZ_LSYJXZSQ T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE 1=1 " + strWhere);
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
			parameters[0].Value = "YX_DZJZ_LSYJXZSQ";
			parameters[1].Value = "ID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperOra.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  成员方法
	}
}

