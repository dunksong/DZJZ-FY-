using System;
using System.Data;
using System.Text;
using System.Data.OracleClient;
using EDRS.IDAL;
using Maticsoft.DBUtility;
using EDRS.Common;

namespace EDRS.OracleDAL
{
	/// <summary>
	/// 数据访问类:XT_ZZJG_RYBM
	/// </summary>
	public partial class XT_ZZJG_RYBM:IXT_ZZJG_RYBM
    {
        public System.Web.HttpRequest context = null;
        public void SetHttpContext(System.Web.HttpRequest _context)
        {
            this.context = _context;
        }
		public XT_ZZJG_RYBM()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string GH)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from XT_ZZJG_RYBM");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
			strSql.Append(" where GH=:GH ");
			OracleParameter[] parameters = {
					new OracleParameter(":GH", OracleType.Char,4)			};
			parameters[0].Value = GH;

            try
            {
                return DbHelperOra.Exists(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Exists(string GH)", "EDRS.OracleDAL.XT_ZZJG_RYBM", strSql.ToString(), parameters);
            }
            return false;
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EDRS.Model.XT_ZZJG_RYBM model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into XT_ZZJG_RYBM(");
			strSql.Append("GH,DWBM,MC,DLBM,KL,YDDHHM,DZYJ,GZZH,YDWBM,YDWMC,SFLSRY,SFTZ,ZP,SFSC,XB,CAID)");
			strSql.Append(" values (");
			strSql.Append(":GH,:DWBM,:MC,:DLBM,:KL,:YDDHHM,:DZYJ,:GZZH,:YDWBM,:YDWMC,:SFLSRY,:SFTZ,:ZP,:SFSC,:XB,:CAID)");
			OracleParameter[] parameters = {
					new OracleParameter(":GH", OracleType.Char,4),
					new OracleParameter(":DWBM", OracleType.VarChar,50),
					new OracleParameter(":MC", OracleType.VarChar,60),
					new OracleParameter(":DLBM", OracleType.VarChar,60),
					new OracleParameter(":KL", OracleType.VarChar,128),
					new OracleParameter(":YDDHHM", OracleType.VarChar,60),
					new OracleParameter(":DZYJ", OracleType.VarChar,60),
					new OracleParameter(":GZZH", OracleType.VarChar,20),
					new OracleParameter(":YDWBM", OracleType.VarChar,50),
					new OracleParameter(":YDWMC", OracleType.VarChar,300),
					new OracleParameter(":SFLSRY", OracleType.Char,1),
					new OracleParameter(":SFTZ", OracleType.Char,1),
					new OracleParameter(":ZP", OracleType.Blob),
					new OracleParameter(":SFSC", OracleType.Char,1),
					new OracleParameter(":XB", OracleType.Char,1),
					new OracleParameter(":CAID", OracleType.VarChar,100)};
			parameters[0].Value = model.GH;
			parameters[1].Value = model.DWBM;
			parameters[2].Value = model.MC;
			parameters[3].Value = model.DLBM;
			parameters[4].Value = model.KL;
			parameters[5].Value = model.YDDHHM;
			parameters[6].Value = model.DZYJ;
			parameters[7].Value = model.GZZH;
			parameters[8].Value = model.YDWBM;
			parameters[9].Value = model.YDWMC;
			parameters[10].Value = model.SFLSRY;
			parameters[11].Value = model.SFTZ;
            parameters[12].Value = model.ZP.Length == 0 ? new byte[1] : model.ZP;
			parameters[13].Value = model.SFSC;
			parameters[14].Value = model.XB;
			parameters[15].Value = model.CAID;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Add(EDRS.Model.XT_ZZJG_RYBM model)", "EDRS.OracleDAL.XT_ZZJG_RYBM", strSql.ToString(), parameters);
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
		public bool Update(EDRS.Model.XT_ZZJG_RYBM model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update XT_ZZJG_RYBM set ");
			strSql.Append("MC=:MC,");
			strSql.Append("DLBM=:DLBM,");
			strSql.Append("KL=:KL,");
			strSql.Append("YDDHHM=:YDDHHM,");
			strSql.Append("DZYJ=:DZYJ,");
			strSql.Append("GZZH=:GZZH,");
			strSql.Append("YDWBM=:YDWBM,");
			strSql.Append("YDWMC=:YDWMC,");
			strSql.Append("SFLSRY=:SFLSRY,");
			strSql.Append("SFTZ=:SFTZ,");
			strSql.Append("ZP=:ZP,");
			strSql.Append("SFSC=:SFSC,");
			strSql.Append("XB=:XB,");
			strSql.Append("CAID=:CAID");
            strSql.Append(" where DWBM=:DWBM and GH=:GH ");
			OracleParameter[] parameters = {
					new OracleParameter(":DWBM", OracleType.VarChar,50),
					new OracleParameter(":MC", OracleType.VarChar,60),
					new OracleParameter(":DLBM", OracleType.VarChar,60),
					new OracleParameter(":KL", OracleType.VarChar,128),
					new OracleParameter(":YDDHHM", OracleType.VarChar,60),
					new OracleParameter(":DZYJ", OracleType.VarChar,60),
					new OracleParameter(":GZZH", OracleType.VarChar,20),
					new OracleParameter(":YDWBM", OracleType.VarChar,50),
					new OracleParameter(":YDWMC", OracleType.VarChar,300),
					new OracleParameter(":SFLSRY", OracleType.Char,1),
					new OracleParameter(":SFTZ", OracleType.Char,1),
					new OracleParameter(":ZP", OracleType.Blob),
					new OracleParameter(":SFSC", OracleType.Char,1),
					new OracleParameter(":XB", OracleType.Char,1),
					new OracleParameter(":CAID", OracleType.VarChar,100),
					new OracleParameter(":GH", OracleType.Char,4)};
			parameters[0].Value = model.DWBM;
			parameters[1].Value = model.MC;
			parameters[2].Value = model.DLBM;
			parameters[3].Value = model.KL;
			parameters[4].Value = model.YDDHHM;
			parameters[5].Value = model.DZYJ;
			parameters[6].Value = model.GZZH;
			parameters[7].Value = model.YDWBM;
			parameters[8].Value = model.YDWMC;
			parameters[9].Value = model.SFLSRY;
			parameters[10].Value = model.SFTZ;
            parameters[11].Value = model.ZP == null ? new byte[1] : model.ZP;
			parameters[12].Value = model.SFSC;
			parameters[13].Value = model.XB;
			parameters[14].Value = model.CAID;
			parameters[15].Value = model.GH;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Update(EDRS.Model.XT_ZZJG_RYBM model)", "EDRS.OracleDAL.XT_ZZJG_RYBM", strSql.ToString(), parameters);
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
		public bool Delete(string GH)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from XT_ZZJG_RYBM ");
			strSql.Append(" where GH=:GH ");
			OracleParameter[] parameters = {
					new OracleParameter(":GH", OracleType.Char,4)			};
			parameters[0].Value = GH;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Delete(string GH)", "EDRS.OracleDAL.XT_ZZJG_RYBM", strSql.ToString(), parameters);
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
		public bool DeleteList(string GHlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from XT_ZZJG_RYBM ");
			strSql.Append(" where GH in ("+GHlist + ")  ");
            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString());
            }
            catch (Exception ex)
            {
                System.Collections.Generic.Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
                parameters.Add("GH", GHlist);
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool DeleteList(string GHlist )", "EDRS.OracleDAL.XT_ZZJG_RYBM", strSql.ToString(), parameters);
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
		public EDRS.Model.XT_ZZJG_RYBM GetModel(string GH)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select GH,DWBM,MC,DLBM,KL,YDDHHM,DZYJ,GZZH,YDWBM,YDWMC,SFLSRY,SFTZ,SFSC,XB,CAID from XT_ZZJG_RYBM ");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
            strSql.Append(" where GH=:GH ");
			OracleParameter[] parameters = {
					new OracleParameter(":GH", OracleType.Char,4)			};
			parameters[0].Value = GH;

			EDRS.Model.XT_ZZJG_RYBM model=new EDRS.Model.XT_ZZJG_RYBM();
            DataSet ds = null;
            try
            {
                ds = DbHelperOra.Query(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public EDRS.Model.XT_ZZJG_RYBM GetModel(string GH)", "EDRS.OracleDAL.XT_ZZJG_RYBM", strSql.ToString(), parameters);
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
		public EDRS.Model.XT_ZZJG_RYBM DataRowToModel(DataRow row)
		{
			EDRS.Model.XT_ZZJG_RYBM model=new EDRS.Model.XT_ZZJG_RYBM();
			if (row != null)
			{
				if(row["GH"]!=null)
				{
					model.GH=row["GH"].ToString();
				}
				if(row["DWBM"]!=null)
				{
					model.DWBM=row["DWBM"].ToString();
				}
                if (row["DWMC"] != null)
                {
                    model.DWMC = row["DWMC"].ToString();
                }
				if(row["MC"]!=null)
				{
					model.MC=row["MC"].ToString();
				}
				if(row["DLBM"]!=null)
				{
					model.DLBM=row["DLBM"].ToString();
				}
				if(row["KL"]!=null)
				{
					model.KL=row["KL"].ToString();
				}
				if(row["YDDHHM"]!=null)
				{
					model.YDDHHM=row["YDDHHM"].ToString();
				}
				if(row["DZYJ"]!=null)
				{
					model.DZYJ=row["DZYJ"].ToString();
				}
				if(row["GZZH"]!=null)
				{
					model.GZZH=row["GZZH"].ToString();
				}
				if(row["YDWBM"]!=null)
				{
					model.YDWBM=row["YDWBM"].ToString();
				}
				if(row["YDWMC"]!=null)
				{
					model.YDWMC=row["YDWMC"].ToString();
				}
				if(row["SFLSRY"]!=null)
				{
					model.SFLSRY=row["SFLSRY"].ToString();
				}
				if(row["SFTZ"]!=null)
				{
					model.SFTZ=row["SFTZ"].ToString();
				}
				if(row.Table.Columns.Contains("ZP") && row["ZP"]!=null && row["ZP"].ToString()!="")
				{
                    model.ZP = (byte[])row["ZP"];
				}
				if(row["SFSC"]!=null)
				{
					model.SFSC=row["SFSC"].ToString();
				}
				if(row["XB"]!=null)
				{
					model.XB=row["XB"].ToString();
				}
				if(row["CAID"]!=null)
				{
					model.CAID=row["CAID"].ToString();
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
			strSql.Append("select GH,DWBM,MC,DLBM,KL,YDDHHM,DZYJ,GZZH,YDWBM,YDWMC,SFLSRY,SFTZ,SFSC,XB,CAID ");
			strSql.Append(" FROM XT_ZZJG_RYBM ");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetList(string strWhere, params object[] objValues)", "EDRS.OracleDAL.XT_ZZJG_RYBM", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
        public int GetRecordCount(string strWhere, params object[] objValues)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM XT_ZZJG_RYBM ");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public int GetRecordCount(string strWhere, params object[] objValues)", "EDRS.OracleDAL.XT_ZZJG_RYBM", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
				strSql.Append("order by T.GH desc");
			}
            strSql.AppendFormat(")AS Ro, T.GH,T.DWBM,T.MC,T.DLBM,T.KL,T.YDDHHM,T.DZYJ,T.GZZH,T.YDWBM,T.YDWMC,T.SFLSRY,T.SFTZ,T.SFSC,T.XB,T.CAID  from XT_ZZJG_RYBM{0} T ", ConfigHelper.GetConfigString("OrcDBLinq"));
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)", "EDRS.OracleDAL.XT_ZZJG_RYBM", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
			parameters[0].Value = "XT_ZZJG_RYBM";
			parameters[1].Value = "GH";
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

