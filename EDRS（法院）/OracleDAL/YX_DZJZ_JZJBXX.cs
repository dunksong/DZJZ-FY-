
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
	/// 数据访问类:YX_DZJZ_JZJBXX
	/// </summary>
	public partial class YX_DZJZ_JZJBXX:IYX_DZJZ_JZJBXX
    {
        public System.Web.HttpRequest context = null;
        public void SetHttpContext(System.Web.HttpRequest _context)
        {
            this.context = _context;
        }
		public YX_DZJZ_JZJBXX()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string JZBH)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from YX_DZJZ_JZJBXX");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
			strSql.Append(" where JZBH=:JZBH ");
			OracleParameter[] parameters = {
					new OracleParameter(":JZBH", OracleType.Char,14)			};
			parameters[0].Value = JZBH;
            try
            {
                return DbHelperOra.Exists(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Exists(string JZBH)", "EDRS.OracleDAL.YX_DZJZ_JZJBXX", strSql.ToString(), parameters);
            }
            return false;
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
        public bool Add(EDRS.Model.YX_DZJZ_JZJBXX model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into YX_DZJZ_JZJBXX(");
            strSql.Append("JZBH,SFSC,CJSJ,ZHXGSJ,FQDWBM,FQL,DWBM,BMSAH,TYSAH,JZMC,JZLJ,JZSCSJ,JZSCRGH,JZSCRXM,JZMS,JZXGH,SFKYGX,GXYWBMJH,MRSFGKPZ, ACCOMPLICES, AJMB_BM, AJMB_MC, IDNUMBER, ISRECORD, SUSPECTNAME,WSBH,AJBH,ZZZT,JZPZ,JZSHRBH,JZSHR,JZSHSJ,WSMC)");
            strSql.Append(" values (");
            strSql.Append(":JZBH,:SFSC,:CJSJ,:ZHXGSJ,:FQDWBM,:FQL,:DWBM,:BMSAH,:TYSAH,:JZMC,:JZLJ,:JZSCSJ,:JZSCRGH,:JZSCRXM,:JZMS,:JZXGH,:SFKYGX,:GXYWBMJH,:MRSFGKPZ, :ACCOMPLICES, :AJMB_BM, :AJMB_MC, :IDNUMBER, :ISRECORD, :SUSPECTNAME, :WSBH, :AJBH, :ZZZT,:JZPZ,:JZSHRBH,:JZSHR,:JZSHSJ,:WSMC)");
            OracleParameter[] parameters = {
					new OracleParameter(":JZBH", OracleType.Char,14),
					new OracleParameter(":SFSC", OracleType.Char,1),
					new OracleParameter(":CJSJ", OracleType.DateTime),
					new OracleParameter(":ZHXGSJ", OracleType.DateTime),
					new OracleParameter(":FQDWBM", OracleType.VarChar,50),
					new OracleParameter(":FQL", OracleType.VarChar,6),
					new OracleParameter(":DWBM", OracleType.VarChar,50),
					new OracleParameter(":BMSAH", OracleType.VarChar,100),
					new OracleParameter(":TYSAH", OracleType.Char,17),
					new OracleParameter(":JZMC", OracleType.VarChar,300),
					new OracleParameter(":JZLJ", OracleType.VarChar,500),
					new OracleParameter(":JZSCSJ", OracleType.DateTime),
					new OracleParameter(":JZSCRGH", OracleType.Char,4),
					new OracleParameter(":JZSCRXM", OracleType.VarChar,60),
					new OracleParameter(":JZMS", OracleType.VarChar,500),
					new OracleParameter(":JZXGH", OracleType.Clob,4000),
					new OracleParameter(":SFKYGX", OracleType.Char,1),
					new OracleParameter(":GXYWBMJH", OracleType.VarChar,300),
					new OracleParameter(":MRSFGKPZ", OracleType.Char,1),
                    new OracleParameter(":ACCOMPLICES", OracleType.VarChar,2000),
                    new OracleParameter(":AJMB_BM", OracleType.VarChar,500),
                    new OracleParameter(":AJMB_MC", OracleType.VarChar,500),
                    new OracleParameter(":IDNUMBER", OracleType.VarChar,500),
                    new OracleParameter(":ISRECORD", OracleType.VarChar,10),
                    new OracleParameter(":SUSPECTNAME", OracleType.VarChar,500),
                    new OracleParameter(":WSBH", OracleType.VarChar,100),
                    new OracleParameter(":AJBH", OracleType.VarChar,100),
                    new OracleParameter(":ZZZT", OracleType.VarChar,10),
                    new OracleParameter(":JZPZ", OracleType.VarChar,4000),
                    new OracleParameter(":JZSHRBH", OracleType.VarChar),
                    new OracleParameter(":JZSHR", OracleType.VarChar),
                    new OracleParameter(":JZSHSJ", OracleType.DateTime),
                    new OracleParameter(":WSMC",OracleType.VarChar)
                                           };
            parameters[0].Value = model.JZBH??"";
            parameters[1].Value = model.SFSC;
            parameters[2].Value = model.CJSJ;
            parameters[3].Value = model.ZHXGSJ;
            parameters[4].Value = model.FQDWBM;
            parameters[5].Value = model.FQL??"";
            parameters[6].Value = model.DWBM ?? "";
            parameters[7].Value = model.BMSAH ?? "";
            parameters[8].Value = model.TYSAH ?? "";
            parameters[9].Value = model.JZMC ?? "";
            parameters[10].Value = model.JZLJ ?? "";
            parameters[11].Value = (object)model.JZSCSJ ?? DBNull.Value;
            parameters[12].Value = model.JZSCRGH ?? "";
            parameters[13].Value = model.JZSCRXM ?? "";
            parameters[14].Value = model.JZMS ?? "";
            parameters[15].Value = model.JZXGH;
            parameters[16].Value = model.SFKYGX;
            parameters[17].Value = model.GXYWBMJH ?? "";
            parameters[18].Value = model.MRSFGKPZ;
            parameters[19].Value = model.Accomplices ?? "";
            parameters[20].Value = model.Ajmb_bm ?? "";
            parameters[21].Value = model.Ajmb_mc ?? "";
            parameters[22].Value = model.Idnumber ?? "";
            parameters[23].Value = model.Isrecord ?? "";
            parameters[24].Value = model.Suspectname ?? "";
            parameters[25].Value = model.WSBH ?? "";
            parameters[26].Value = model.AJBH ?? "";
            parameters[27].Value = model.ZZZT ?? "";
            parameters[28].Value = model.JZPZ ?? "";
            parameters[29].Value = model.JZSHRBH ?? "";
            parameters[30].Value = model.JZSHR ?? "";
            parameters[31].Value = (object)model.JZSHSJ ?? DBNull.Value;
            parameters[32].Value = model.WSMC ?? "";

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Add(EDRS.Model.YX_DZJZ_JZJBXX model)", "EDRS.OracleDAL.YX_DZJZ_JZJBXX", strSql.ToString(), parameters);
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
		public bool Update(EDRS.Model.YX_DZJZ_JZJBXX model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update YX_DZJZ_JZJBXX set ");
			strSql.Append("SFSC=:SFSC,");
			strSql.Append("CJSJ=:CJSJ,");
			strSql.Append("ZHXGSJ=:ZHXGSJ,");
			strSql.Append("FQDWBM=:FQDWBM,");
			strSql.Append("FQL=:FQL,");
			strSql.Append("DWBM=:DWBM,");
			strSql.Append("BMSAH=:BMSAH,");
			strSql.Append("TYSAH=:TYSAH,");
			strSql.Append("JZMC=:JZMC,");
			strSql.Append("JZLJ=:JZLJ,");
			strSql.Append("JZSCSJ=:JZSCSJ,");
			strSql.Append("JZSCRGH=:JZSCRGH,");
			strSql.Append("JZSCRXM=:JZSCRXM,");
			strSql.Append("JZMS=:JZMS,");
			strSql.Append("JZXGH=:JZXGH,");
			strSql.Append("SFKYGX=:SFKYGX,");
            strSql.Append("GXYWBMJH=:GXYWBMJH,");
            strSql.Append("MRSFGKPZ=:MRSFGKPZ,");
            strSql.Append("ZZZT=:ZZZT,");
            strSql.Append("JZPZ =:JZPZ,");
            strSql.Append("JZSHRBH =:JZSHRBH,");
            strSql.Append("JZSHR =:JZSHR,");
            strSql.Append("JZSHSJ =:JZSHSJ,");
            strSql.Append("AJBH = :AJBH,");
            strSql.Append("WSBH = :WSBH,");
            strSql.Append("WSMC = :WSMC");
			strSql.Append(" where JZBH=:JZBH ");
			OracleParameter[] parameters = {
					new OracleParameter(":SFSC", OracleType.Char,1),
					new OracleParameter(":CJSJ", OracleType.DateTime),
					new OracleParameter(":ZHXGSJ", OracleType.DateTime),
					new OracleParameter(":FQDWBM", OracleType.VarChar,50),
					new OracleParameter(":FQL", OracleType.VarChar,6),
					new OracleParameter(":DWBM", OracleType.VarChar,50),
					new OracleParameter(":BMSAH", OracleType.VarChar,100),
					new OracleParameter(":TYSAH", OracleType.Char,17),
					new OracleParameter(":JZMC", OracleType.VarChar,300),
					new OracleParameter(":JZLJ", OracleType.VarChar,500),
					new OracleParameter(":JZSCSJ", OracleType.DateTime),
					new OracleParameter(":JZSCRGH", OracleType.Char,4),
					new OracleParameter(":JZSCRXM", OracleType.VarChar,60),
					new OracleParameter(":JZMS", OracleType.VarChar,500),
					new OracleParameter(":JZXGH", OracleType.Clob,4000),
					new OracleParameter(":SFKYGX", OracleType.Char,1),
					new OracleParameter(":GXYWBMJH", OracleType.VarChar,300),
					new OracleParameter(":MRSFGKPZ", OracleType.Char,1),
					new OracleParameter(":JZBH", OracleType.Char,14),
                    new OracleParameter(":ZZZT", OracleType.VarChar,10),
                    new OracleParameter(":JZPZ", OracleType.VarChar,4000),
                    new OracleParameter(":JZSHRBH", OracleType.VarChar),
                    new OracleParameter(":JZSHR", OracleType.VarChar),
                    new OracleParameter(":AJBH", OracleType.VarChar),
                    new OracleParameter(":WSBH", OracleType.VarChar),
                    new OracleParameter(":WSMC", OracleType.VarChar),
                    new OracleParameter(":JZSHSJ", OracleType.DateTime)};
			parameters[0].Value = model.SFSC;
			parameters[1].Value = model.CJSJ;
			parameters[2].Value = model.ZHXGSJ;
			parameters[3].Value = model.FQDWBM;
			parameters[4].Value = model.FQL;
			parameters[5].Value = model.DWBM;
			parameters[6].Value = model.BMSAH;
			parameters[7].Value = model.TYSAH;
			parameters[8].Value = model.JZMC;
			parameters[9].Value = model.JZLJ;
			parameters[10].Value = model.JZSCSJ;
			parameters[11].Value = model.JZSCRGH;
			parameters[12].Value = model.JZSCRXM;
			parameters[13].Value = model.JZMS;
			parameters[14].Value = model.JZXGH;
			parameters[15].Value = model.SFKYGX;
			parameters[16].Value = model.GXYWBMJH;
			parameters[17].Value = model.MRSFGKPZ;
            parameters[18].Value = model.JZBH;
            parameters[19].Value = model.ZZZT;
            parameters[20].Value = model.JZPZ;
            parameters[21].Value = model.JZSHRBH;
            parameters[22].Value = model.JZSHR;
            parameters[23].Value = model.AJBH ?? "";
            parameters[24].Value = model.WSBH ?? "";
            parameters[25].Value = model.WSMC ?? "" ;
            parameters[26].Value = (object)model.JZSHSJ?? DBNull.Value ;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Update(EDRS.Model.YX_DZJZ_JZJBXX model)", "EDRS.OracleDAL.YX_DZJZ_JZJBXX", strSql.ToString(), parameters);
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
		public bool Delete(string JZBH)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from YX_DZJZ_JZJBXX ");
			strSql.Append(" where JZBH=:JZBH ");
			OracleParameter[] parameters = {
					new OracleParameter(":JZBH", OracleType.Char,14)			};
			parameters[0].Value = JZBH;

            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Delete(string JZBH)", "EDRS.OracleDAL.YX_DZJZ_JZJBXX", strSql.ToString(), parameters);
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
		public bool DeleteList(string JZBHlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from YX_DZJZ_JZJBXX ");
			strSql.Append(" where JZBH in ("+JZBHlist + ")  ");
            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(strSql.ToString());
            }
            catch (Exception ex)
            {
                System.Collections.Generic.Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
                parameters.Add("JZBH", JZBHlist);
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool DeleteList(string JZBHlist )", "EDRS.OracleDAL.YX_DZJZ_JZJBXX", strSql.ToString(), parameters);
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
		public EDRS.Model.YX_DZJZ_JZJBXX GetModel(string JZBH)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select JZBH,SFSC,CJSJ,ZHXGSJ,FQDWBM,FQL,DWBM,BMSAH,TYSAH,JZMC,JZLJ,JZSCSJ,JZSCRGH,JZSCRXM,JZMS,JZXGH,SFKYGX,GXYWBMJH,MRSFGKPZ,ZZZT,JZPZ,JZSHRBH,JZSHR,JZSHSJ,AJBH,WSBH,WSMC  from YX_DZJZ_JZJBXX ");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
            strSql.Append(" where JZBH=:JZBH ");
			OracleParameter[] parameters = {
					new OracleParameter(":JZBH", OracleType.Char,14)			};
			parameters[0].Value = JZBH;

			EDRS.Model.YX_DZJZ_JZJBXX model=new EDRS.Model.YX_DZJZ_JZJBXX();
            DataSet ds = null;
            try
            {
                ds = DbHelperOra.Query(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public EDRS.Model.YX_DZJZ_JZJBXX GetModel(string JZBH)", "EDRS.OracleDAL.YX_DZJZ_JZJBXX", strSql.ToString(), parameters);
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
		public EDRS.Model.YX_DZJZ_JZJBXX DataRowToModel(DataRow row)
		{
			EDRS.Model.YX_DZJZ_JZJBXX model=new EDRS.Model.YX_DZJZ_JZJBXX();
			if (row != null)
			{
				if(row["JZBH"]!=null)
				{
					model.JZBH=row["JZBH"].ToString();
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
				if(row["DWBM"]!=null)
				{
					model.DWBM=row["DWBM"].ToString();
				}
				if(row["BMSAH"]!=null)
				{
					model.BMSAH=row["BMSAH"].ToString();
				}
				if(row["TYSAH"]!=null)
				{
					model.TYSAH=row["TYSAH"].ToString();
				}
				if(row["JZMC"]!=null)
				{
					model.JZMC=row["JZMC"].ToString();
				}
				if(row["JZLJ"]!=null)
				{
					model.JZLJ=row["JZLJ"].ToString();
				}
				if(row["JZSCSJ"]!=null && row["JZSCSJ"].ToString()!="")
				{
					model.JZSCSJ=DateTime.Parse(row["JZSCSJ"].ToString());
				}
				if(row["JZSCRGH"]!=null)
				{
					model.JZSCRGH=row["JZSCRGH"].ToString();
				}
				if(row["JZSCRXM"]!=null)
				{
					model.JZSCRXM=row["JZSCRXM"].ToString();
				}
				if(row["JZMS"]!=null)
				{
					model.JZMS=row["JZMS"].ToString();
				}
                if (row["JZXGH"] != null)
                {
                    model.JZXGH=row["JZXGH"].ToString();
                }
				if(row["SFKYGX"]!=null)
				{
					model.SFKYGX=row["SFKYGX"].ToString();
				}
				if(row["GXYWBMJH"]!=null)
				{
					model.GXYWBMJH=row["GXYWBMJH"].ToString();
				}
				if(row["MRSFGKPZ"]!=null)
				{
					model.MRSFGKPZ=row["MRSFGKPZ"].ToString();
				}

                if (row["ZZZT"] != null)
                {
                    model.ZZZT = row["ZZZT"].ToString();
                }
                if (row["JZPZ"] != null)
                {
                    model.JZPZ = row["JZPZ"].ToString();
                }
                if (row["JZSHRBH"] != null)
                {
                    model.JZSHRBH = row["JZSHRBH"].ToString();
                }
                if (row["JZSHR"] != null)
                {
                    model.JZSHR = row["JZSHR"].ToString();
                }
                if (row["JZSHSJ"] != null && row["JZSHSJ"].ToString() != "")
                {
                    model.JZSHSJ = DateTime.Parse(row["JZSHSJ"].ToString());
                }
                if (row["AJBH"] != DBNull.Value)
                    model.AJBH = row["AJBH"].ToString();
                if (row["WSBH"] != DBNull.Value)
                    model.WSBH = row["WSBH"].ToString();
                if(row["WSMC"]!=DBNull.Value)
                    model.WSMC = row["WSMC"].ToString();
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
        public DataSet GetList(string strWhere, params object[] objValues)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select JZBH,SFSC,CJSJ,ZHXGSJ,FQDWBM,FQL,DWBM,BMSAH,TYSAH,JZMC,JZLJ,JZSCSJ,JZSCRGH,JZSCRXM,JZMS,JZXGH,SFKYGX,GXYWBMJH,MRSFGKPZ,ZZZT,JZPZ,JZSHRBH,JZSHR,JZSHSJ,AJBH,WSBH,WSMC ");
			strSql.Append(" FROM YX_DZJZ_JZJBXX ");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetList(string strWhere, params object[] objValues)", "EDRS.OracleDAL.YX_DZJZ_JZJBXX", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
        public int GetRecordCount(string strWhere, params object[] objValues)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM YX_DZJZ_JZJBXX ");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public int GetRecordCount(string strWhere, params object[] objValues)", "EDRS.OracleDAL.YX_DZJZ_JZJBXX", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
				strSql.Append("order by T.JZBH desc");
			}
			strSql.AppendFormat(")AS Ro, T.*  from YX_DZJZ_JZJBXX{0} T ",ConfigHelper.GetConfigString("OrcDBLinq"));
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)", "EDRS.OracleDAL.YX_DZJZ_JZJBXX", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
			parameters[0].Value = "YX_DZJZ_JZJBXX";
			parameters[1].Value = "JZBH";
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

