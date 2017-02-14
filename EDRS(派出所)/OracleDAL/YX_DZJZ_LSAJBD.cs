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
	/// 数据访问类:YX_DZJZ_LSAJBD
	/// </summary>
	public partial class YX_DZJZ_LSAJBD:IYX_DZJZ_LSAJBD
    {
        public System.Web.HttpRequest context = null;
        public void SetHttpContext(System.Web.HttpRequest _context)
        {
            this.context = _context;
        }
		public YX_DZJZ_LSAJBD()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string YJXH)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from YX_DZJZ_LSAJBD");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
			strSql.Append(" where YJXH=:YJXH ");
			OracleParameter[] parameters = {
					new OracleParameter(":YJXH", OracleType.Number,22)			};
			parameters[0].Value = YJXH;
            try
            {
                return DbHelperOra.Exists(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Exists(string YJXH)", "EDRS.OracleDAL.YX_DZJZ_LSAJBD", strSql.ToString(), parameters);
            }
            return false;
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EDRS.Model.YX_DZJZ_LSAJBD model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into YX_DZJZ_LSAJBD(");
			strSql.Append("GH,BMSAH,MC,AJMC,AJLBBM,AJLBMC,YJKSSJ,YJJSSJ,YJZH,YJMM,JDSJ,JDR,JDRGH,JDBMBM,JDBMMC,JDDWBM,JDDWMC,SFSC,YJSQDH,YJXH,DWBM)");
			strSql.Append(" values (");
            strSql.Append(":GH,:BMSAH,:MC,:AJMC,:AJLBBM,:AJLBMC,:YJKSSJ,:YJJSSJ,:YJZH,:YJMM,:JDSJ,:JDR,:JDRGH,:JDBMBM,:JDBMMC,:JDDWBM,:JDDWMC,:SFSC,:YJSQDH,:YJXH,:DWBM)");
			OracleParameter[] parameters = {
					new OracleParameter(":GH", OracleType.VarChar,100),
					new OracleParameter(":BMSAH", OracleType.VarChar,100),
					new OracleParameter(":MC", OracleType.VarChar,60),
					new OracleParameter(":AJMC", OracleType.VarChar,300),
					new OracleParameter(":AJLBBM", OracleType.VarChar,50),
					new OracleParameter(":AJLBMC", OracleType.VarChar,50),					
					new OracleParameter(":YJKSSJ", OracleType.DateTime),
					new OracleParameter(":YJJSSJ", OracleType.DateTime),
					new OracleParameter(":YJZH", OracleType.VarChar,100),
					new OracleParameter(":YJMM", OracleType.Char,8),
					new OracleParameter(":JDSJ", OracleType.DateTime),
					new OracleParameter(":JDR", OracleType.VarChar,60),
					new OracleParameter(":JDRGH", OracleType.Char,4),
					new OracleParameter(":JDBMBM", OracleType.Char,4),
					new OracleParameter(":JDBMMC", OracleType.VarChar,300),
					new OracleParameter(":JDDWBM", OracleType.VarChar,50),
					new OracleParameter(":JDDWMC", OracleType.VarChar,300),
					new OracleParameter(":SFSC", OracleType.Char,1),
					new OracleParameter(":YJSQDH", OracleType.VarChar,50),
                    new OracleParameter(":YJXH", OracleType.VarChar,50),
                    new OracleParameter(":DWBM",OracleType.VarChar,50)};
			parameters[0].Value = model.GH ?? "";
            parameters[1].Value = model.BMSAH ?? "";
            parameters[2].Value = model.MC ?? "";
            parameters[3].Value = model.AJMC ?? "";
            parameters[4].Value = model.AJLBBM ?? "";
            parameters[5].Value = model.AJLBMC ?? "";
            parameters[6].Value = model.YJKSSJ;
            parameters[7].Value = model.YJJSSJ;
            parameters[8].Value = model.YJZH ?? "";
            parameters[9].Value = model.YJMM ?? "";
            parameters[10].Value = model.JDSJ;
            parameters[11].Value = model.JDR ?? "";
            parameters[12].Value = model.JDRGH ?? "";
            parameters[13].Value = model.JDBMBM ?? "";
            parameters[14].Value = model.JDBMMC ?? "";
            parameters[15].Value = model.JDDWBM ?? "";
            parameters[16].Value = model.JDDWMC ?? "";
            parameters[17].Value = model.SFSC ?? "";
            parameters[18].Value = model.YJSQDH ?? "";
            parameters[19].Value = model.YJXH ?? "";
            parameters[20].Value = model.DWBM ?? "";
            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Add(EDRS.Model.YX_DZJZ_LSAJBD model)", "EDRS.OracleDAL.YX_DZJZ_LSAJBD", strSql.ToString(), parameters);
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
		public bool Update(EDRS.Model.YX_DZJZ_LSAJBD model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update YX_DZJZ_LSAJBD set ");
			strSql.Append("MC=:MC,");
			strSql.Append("AJMC=:AJMC,");
			strSql.Append("AJLBBM=:AJLBBM,");
			strSql.Append("AJLBMC=:AJLBMC,");			
			strSql.Append("YJKSSJ=:YJKSSJ,");
			strSql.Append("YJJSSJ=:YJJSSJ,");
			strSql.Append("YJZH=:YJZH,");
			strSql.Append("YJMM=:YJMM,");
			strSql.Append("JDSJ=:JDSJ,");
			strSql.Append("JDR=:JDR,");
			strSql.Append("JDRGH=:JDRGH,");
			strSql.Append("JDBMBM=:JDBMBM,");
			strSql.Append("JDBMMC=:JDBMMC,");
			strSql.Append("JDDWBM=:JDDWBM,");
			strSql.Append("JDDWMC=:JDDWMC,");
			strSql.Append("SFSC=:SFSC,");
			strSql.Append("YJSQDH=:YJSQDH,");
            strSql.Append("GH=:GH,");
            strSql.Append("BMSAH=:BMSAH,");
            strSql.Append("DWBM=:DWBM");
			strSql.Append(" where YJXH=:YJXH ");
			OracleParameter[] parameters = {
					new OracleParameter(":MC", OracleType.VarChar,60),
					new OracleParameter(":AJMC", OracleType.VarChar,300),
					new OracleParameter(":AJLBBM", OracleType.VarChar,50),
					new OracleParameter(":AJLBMC", OracleType.VarChar,50),
					new OracleParameter(":YJKSSJ", OracleType.DateTime),
					new OracleParameter(":YJJSSJ", OracleType.DateTime),
					new OracleParameter(":YJZH", OracleType.VarChar,100),
					new OracleParameter(":YJMM", OracleType.Char,8),
					new OracleParameter(":JDSJ", OracleType.DateTime),
					new OracleParameter(":JDR", OracleType.VarChar,60),
					new OracleParameter(":JDRGH", OracleType.Char,4),
					new OracleParameter(":JDBMBM", OracleType.Char,4),
					new OracleParameter(":JDBMMC", OracleType.VarChar,300),
					new OracleParameter(":JDDWBM", OracleType.VarChar,50),
					new OracleParameter(":JDDWMC", OracleType.VarChar,300),
					new OracleParameter(":SFSC", OracleType.Char,1),
					new OracleParameter(":YJSQDH", OracleType.VarChar,50),
					new OracleParameter(":GH", OracleType.VarChar,100),
					new OracleParameter(":BMSAH", OracleType.VarChar,100),
					new OracleParameter(":YJXH", OracleType.VarChar,50),
                    new OracleParameter(":DWBM", OracleType.VarChar,50)};
			parameters[0].Value = model.MC;
			parameters[1].Value = model.AJMC;
			parameters[2].Value = model.AJLBBM;
			parameters[3].Value = model.AJLBMC;
			parameters[4].Value = model.YJKSSJ;
			parameters[5].Value = model.YJJSSJ;
			parameters[6].Value = model.YJZH;
			parameters[7].Value = model.YJMM;
			parameters[8].Value = model.JDSJ;
			parameters[9].Value = model.JDR;
			parameters[10].Value = model.JDRGH;
			parameters[11].Value = model.JDBMBM;
			parameters[12].Value = model.JDBMMC;
			parameters[13].Value = model.JDDWBM;
			parameters[14].Value = model.JDDWMC;
			parameters[15].Value = model.SFSC;
			parameters[16].Value = model.YJSQDH;
			parameters[17].Value = model.GH;
			parameters[18].Value = model.BMSAH;
			parameters[19].Value = model.YJXH;
            parameters[20].Value = model.DWBM;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Update(EDRS.Model.YX_DZJZ_LSAJBD model)", "EDRS.OracleDAL.YX_DZJZ_LSAJBD", strSql.ToString(), parameters);
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
		public bool Delete(string YJXH)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from YX_DZJZ_LSAJBD ");
            strSql.Append(" where YJXH=:YJXH");
			OracleParameter[] parameters = {
					new OracleParameter(":YJXH", OracleType.VarChar,50)			};
			parameters[1].Value = YJXH;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Delete(string YJXH)", "EDRS.OracleDAL.YX_DZJZ_LSAJBD", strSql.ToString(), parameters);
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
		public EDRS.Model.YX_DZJZ_LSAJBD GetModel(string YJXH)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select GH,BMSAH,YJXH,MC,AJMC,AJLBBM,AJLBMC,YJKSSJ,YJJSSJ,YJZH,YJMM,JDSJ,JDR,JDRGH,JDBMBM,JDBMMC,JDDWBM,JDDWMC,SFSC,YJSQDH,DWBM from YX_DZJZ_LSAJBD ");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
            strSql.Append(" where  YJXH=:YJXH ");
			OracleParameter[] parameters = {
					new OracleParameter(":YJXH", OracleType.VarChar,50)			};
			parameters[0].Value = YJXH;

			EDRS.Model.YX_DZJZ_LSAJBD model=new EDRS.Model.YX_DZJZ_LSAJBD();
            DataSet ds = null;
            try
            {
                ds = DbHelperOra.Query(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public EDRS.Model.YX_DZJZ_LSAJBD GetModel(string YJXH)", "EDRS.OracleDAL.YX_DZJZ_LSAJBD", strSql.ToString(), parameters);
            }
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
		public EDRS.Model.YX_DZJZ_LSAJBD DataRowToModel(DataRow row)
		{
			EDRS.Model.YX_DZJZ_LSAJBD model=new EDRS.Model.YX_DZJZ_LSAJBD();
			if (row != null)
			{
				if(row["GH"]!=null)
				{
					model.GH=row["GH"].ToString();
				}
                if (row["DWBM"] != null)
                {
                    model.DWBM = row["DWBM"].ToString();
                }
				if(row["BMSAH"]!=null)
				{
					model.BMSAH=row["BMSAH"].ToString();
				}
				if(row["YJXH"]!=null && row["YJXH"].ToString()!="")
				{
					model.YJXH=row["YJXH"].ToString();
				}
				if(row["MC"]!=null)
				{
					model.MC=row["MC"].ToString();
				}
				if(row["AJMC"]!=null)
				{
					model.AJMC=row["AJMC"].ToString();
				}
				if(row["AJLBBM"]!=null)
				{
					model.AJLBBM=row["AJLBBM"].ToString();
				}
				if(row["AJLBMC"]!=null)
				{
					model.AJLBMC=row["AJLBMC"].ToString();
				}
				
				if(row["YJKSSJ"]!=null && row["YJKSSJ"].ToString()!="")
				{
					model.YJKSSJ=DateTime.Parse(row["YJKSSJ"].ToString());
				}
				if(row["YJJSSJ"]!=null && row["YJJSSJ"].ToString()!="")
				{
					model.YJJSSJ=DateTime.Parse(row["YJJSSJ"].ToString());
				}
				if(row["YJZH"]!=null)
				{
					model.YJZH=row["YJZH"].ToString();
				}
				if(row["YJMM"]!=null)
				{
					model.YJMM=row["YJMM"].ToString();
				}
				if(row["JDSJ"]!=null && row["JDSJ"].ToString()!="")
				{
					model.JDSJ=DateTime.Parse(row["JDSJ"].ToString());
				}
				if(row["JDR"]!=null)
				{
					model.JDR=row["JDR"].ToString();
				}
				if(row["JDRGH"]!=null)
				{
					model.JDRGH=row["JDRGH"].ToString();
				}
				if(row["JDBMBM"]!=null)
				{
					model.JDBMBM=row["JDBMBM"].ToString();
				}
				if(row["JDBMMC"]!=null)
				{
					model.JDBMMC=row["JDBMMC"].ToString();
				}
				if(row["JDDWBM"]!=null)
				{
					model.JDDWBM=row["JDDWBM"].ToString();
				}
				if(row["JDDWMC"]!=null)
				{
					model.JDDWMC=row["JDDWMC"].ToString();
				}
				if(row["SFSC"]!=null)
				{
					model.SFSC=row["SFSC"].ToString();
				}
				if(row["YJSQDH"]!=null)
				{
					model.YJSQDH=row["YJSQDH"].ToString();
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
			strSql.Append("select GH,BMSAH,YJXH,MC,AJMC,AJLBBM,AJLBMC,YJKSSJ,YJJSSJ,YJZH,YJMM,JDSJ,JDR,JDRGH,JDBMBM,JDBMMC,JDDWBM,JDDWMC,SFSC,YJSQDH,DWBM ");
			strSql.Append(" FROM YX_DZJZ_LSAJBD ");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetList(string strWhere, params object[] objValues)", "EDRS.OracleDAL.YX_DZJZ_LSAJBD", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
        public int GetRecordCount(string strWhere, params object[] objValues)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM YX_DZJZ_LSAJBD T ");           
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public int GetRecordCount(string strWhere, params object[] objValues)", "EDRS.OracleDAL.YX_DZJZ_LSAJBD", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
				strSql.Append("order by T.YJXH desc");
			}
            strSql.AppendFormat(")AS Ro, T.*,j.ajbh,j.wsbh,j.wsmc,q.sqsm,q.yjsqdm from YX_DZJZ_LSYJSQ q left join  YX_DZJZ_LSAJBD{0} T on q.yjsqdh=t.yjsqdh left join yx_dzjz_jzjbxx{0} j on t.bmsah=j.bmsah ", ConfigHelper.GetConfigString("OrcDBLinq")); 
          
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)", "EDRS.OracleDAL.YX_DZJZ_LSAJBD", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
			parameters[0].Value = "YX_DZJZ_LSAJBD";
			parameters[1].Value = "YJXH";
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

