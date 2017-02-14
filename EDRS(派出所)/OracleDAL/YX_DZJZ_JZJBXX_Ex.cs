
using System;
using System.Data;
using System.Text;
using System.Data.OracleClient;
using EDRS.IDAL;
using Maticsoft.DBUtility;
using EDRS.Common;
using System.Collections.Generic;
using System.Collections;//Please add references
namespace EDRS.OracleDAL
{
	/// <summary>
	/// 数据访问类:YX_DZJZ_JZJBXX
	/// </summary>
	public partial class YX_DZJZ_JZJBXX
    {
      
		#region  BasicMethod

	
		/// <summary>
		/// 获取记录总数
		/// </summary>
        public int GetRecordCountPower(string strWhere, params object[] objValues)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM YX_DZJZ_JZJBXX T ");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
            strSql.AppendFormat(" left join TYYW_GG_AJJBXX{0} aj on T.BMSAH = aj.BMSAH", ConfigHelper.GetConfigString("OrcDBLinq"));

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
        public DataSet GetListByPagePower(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)
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
			strSql.AppendFormat(")AS Ro, T.* from YX_DZJZ_JZJBXX{0} T ",ConfigHelper.GetConfigString("OrcDBLinq"));
            strSql.AppendFormat(" left join TYYW_GG_AJJBXX{0} aj on T.BMSAH = aj.BMSAH", ConfigHelper.GetConfigString("OrcDBLinq"));
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

        /// <summary>
        /// 解锁
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool LockByModelList(List<EDRS.Model.YX_DZJZ_JZJBXX> modelList)
        {
            Hashtable sqlHash = new Hashtable();
            int count = 0;
            foreach (EDRS.Model.YX_DZJZ_JZJBXX model in modelList)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update YX_DZJZ_JZJBXX set ");
                strSql.Append(" JZXGH=:JZXGH" + count);
                strSql.Append(" where BMSAH=:BMSAH" + count);
                OracleParameter[] parameters = {				
					new OracleParameter(":BMSAH"+count, OracleType.VarChar,100),				
					new OracleParameter(":JZXGH"+count, OracleType.Clob,4000)};
                parameters[0].Value = model.BMSAH;
                parameters[1].Value = model.JZXGH;
                sqlHash.Add(strSql.ToString(),parameters);
                count++;
            }

            try
            {
                return DbHelperOra.ExecuteSqlTran(sqlHash);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Update(EDRS.Model.YX_DZJZ_JZJBXX model)", "EDRS.OracleDAL.YX_DZJZ_JZJBXX", sqlHash);
            }
                return false;
        }

        /// <summary>
        /// 获取卷宗基本信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetJzjbxxByBmsah(string bmsah, string dwbm)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select jz.JZBH,jz.JZMC, dw.dwmc ZZDWMC, bm.bmmc ZZBMMC, jz.jzscrxm ZZR,jz.JZSCSJ ZZSJ,count(ml.jzbh) JZSL from YX_DZJZ_JZJBXX jz");
            strSql.Append(" left join xt_zzjg_dwbm dw on jz.dwbm = dw.dwbm");
            strSql.Append(" left join yx_dzjz_jzml ml on jz.jzbh = ml.jzbh and ml.mllx=1 and ml.sfsc='N'");
            strSql.Append(" left join XT_QX_RYJSFP js on jz.dwbm = js.dwbm and js.gh=jz.jzscrgh");
            strSql.Append(" left join xt_zzjg_bmbm bm on js.bmbm = bm.bmbm and js.dwbm = bm.dwbm");
            strSql.Append(" where jz.bmsah=:BMSAH and jz.DWBM=:DWBM");
            strSql.Append(" group by jz.jzbh,jz.jzmc, dw.dwmc, bm.bmmc, jz.jzscrxm,jz.JZSCSJ");
            OracleParameter[] parameters = {				
					new OracleParameter(":BMSAH", OracleType.VarChar,100),				
					new OracleParameter(":DWBM", OracleType.VarChar,100)};
            parameters[0].Value = bmsah;
            parameters[1].Value = dwbm;

            try
            {
                return DbHelperOra.Query(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {

                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetJzjbxxByBmsah(string bmsah, string dwbm)", "EDRS.OracleDAL.YX_DZJZ_JZJBXX", strSql.ToString());
            }
            return null;
        }

        /// <summary>
        /// 获取卷宗基本信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetJzjbxxByBmsahList(string bmsahs, string dwbm)
        {
            StringBuilder strSql = new StringBuilder();
            //strSql.Append("select jz.JZBH,jz.JZMC, dw.dwmc ZZDWMC, bm.bmmc ZZBMMC, jz.jzscrxm ZZR,jz.JZSCSJ ZZSJ,count(ml.jzbh) JZSL,jz.WSBH from YX_DZJZ_JZJBXX jz");
            strSql.Append("select jz.JZBH,jz.JZMC, dw.dwmc ZZDWMC, jz.jzscrxm ZZR,jz.JZSCSJ ZZSJ,count(ml.jzbh) JZSL,jz.WSBH from YX_DZJZ_JZJBXX jz");
            strSql.Append(" left join xt_zzjg_dwbm dw on jz.dwbm = dw.dwbm");
            strSql.Append(" left join yx_dzjz_jzml ml on jz.jzbh = ml.jzbh and ml.mllx=1 and ml.sfsc='N'");
            strSql.Append(" left join XT_QX_RYJSFP js on jz.dwbm = js.dwbm and js.gh=jz.jzscrgh");
            //strSql.Append(" left join xt_zzjg_bmbm bm on js.bmbm = bm.bmbm and js.dwbm = bm.dwbm");
            strSql.AppendFormat(" where jz.bmsah in ({0}) ",bmsahs);
            //strSql.Append(" group by jz.jzbh,jz.jzmc, dw.dwmc, bm.bmmc, jz.jzscrxm,jz.JZSCSJ,jz.WSBH");
            strSql.Append(" group by jz.jzbh,jz.jzmc, dw.dwmc, jz.jzscrxm,jz.JZSCSJ,jz.WSBH");
            //OracleParameter[] parameters = {				
				//	new OracleParameter(":BMSAH", OracleType.VarChar,100),				
		//			new OracleParameter(":DWBM", OracleType.VarChar,100)};
          //  parameters[0].Value = bmsah;
            //parameters[0].Value = dwbm;

            try
            {
                //EDRS.Common.LogHelper.LogError(this.context, "测试", "", "public DataSet GetJzjbxxByBmsah(string bmsah, string dwbm)", "EDRS.OracleDAL.YX_DZJZ_JZJBXX", strSql.ToString() + "\n参数：" + bmsahs + "|" + dwbm);
                return DbHelperOra.Query(strSql.ToString());
            }
            catch (Exception ex)
            {

                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetJzjbxxByBmsah(string bmsah, string dwbm)", "EDRS.OracleDAL.YX_DZJZ_JZJBXX", strSql.ToString());
            }
            return null;
        }



        /// <summary>
        /// 获取卷宗基本信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetJzjbxxByAJBH(string ajbh, string dwbm)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select jz.JZBH,jz.JZMC, dw.dwmc ZZDWMC, bm.bmmc ZZBMMC, jz.jzscrxm ZZR,jz.JZSCSJ ZZSJ,count(ml.jzbh) JZSL from YX_DZJZ_JZJBXX jz");
            strSql.Append(" left join xt_zzjg_dwbm dw on jz.dwbm = dw.dwbm");
            strSql.Append(" left join yx_dzjz_jzml ml on jz.jzbh = ml.jzbh and ml.mllx=1 and ml.sfsc='N'");
            strSql.Append(" left join XT_QX_RYJSFP js on jz.dwbm = js.dwbm and js.gh=jz.jzscrgh");
            strSql.Append(" left join xt_zzjg_bmbm bm on js.bmbm = bm.bmbm and js.dwbm = bm.dwbm");
            strSql.Append(" where jz.AJBH=:AJBH and jz.DWBM=:DWBM");
            strSql.Append(" group by jz.jzbh,jz.jzmc, dw.dwmc, bm.bmmc, jz.jzscrxm,jz.JZSCSJ");
            OracleParameter[] parameters = {				
					new OracleParameter(":AJBH", OracleType.VarChar,100),				
					new OracleParameter(":DWBM", OracleType.VarChar,100)};
            parameters[0].Value = ajbh;
            parameters[1].Value = dwbm;

            try
            {
                return DbHelperOra.Query(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {

                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetJzjbxxByBmsah(string bmsah, string dwbm)", "EDRS.OracleDAL.YX_DZJZ_JZJBXX", strSql.ToString());
            }
            return null;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public EDRS.Model.YX_DZJZ_JZJBXX GetModelByBMSAH(string BMSAH)
        {

            StringBuilder strSql = new StringBuilder();
          
            strSql.Append("select JZBH,SFSC,CJSJ,ZHXGSJ,FQDWBM,FQL,DWBM,BMSAH,TYSAH,JZMC,JZLJ,JZSCSJ,JZSCRGH,JZSCRXM,JZMS,JZXGH,SFKYGX,GXYWBMJH,MRSFGKPZ,ZZZT,JZPZ,JZSHRBH,JZSHR,JZSHSJ,AJBH,WSBH,ZZZT,WSMC  from YX_DZJZ_JZJBXX ");

            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
            strSql.Append(" where BMSAH=:BMSAH ");
            OracleParameter[] parameters = {
					new OracleParameter(":BMSAH", OracleType.VarChar)			};
            parameters[0].Value = BMSAH;

            EDRS.Model.YX_DZJZ_JZJBXX model = new EDRS.Model.YX_DZJZ_JZJBXX();
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
        /// 批量审核数据
        /// </summary>
        public bool UpdateByZZZTList(string BMSAHlist, string SHR, DateTime SHSJ, string SHRGH, string ZZZT, string JZPZ)
        {
          //  strSql.Append("update YX_DZJZ_JZJBXX set JZPZ =:JZPZ, JZSHR =:JZSHR ,JZSHSJ =:JZSHSJ,JZSHRBH =:JZSHRBH,ZZZT=:ZZZT ");
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update YX_DZJZ_JZJBXX set ");     
            strSql.Append("ZZZT=:ZZZT,");
            strSql.Append("JZPZ =:JZPZ,");
            strSql.Append("JZSHRBH =:JZSHRBH,");
            strSql.Append("JZSHR =:JZSHR,");
            strSql.Append("JZSHSJ =:JZSHSJ");
            strSql.Append(" where JZBH in ("+BMSAHlist+") and ZZZT in (2,3,4) ");
            OracleParameter[] parameters = {				
                    new OracleParameter(":ZZZT", OracleType.VarChar,1),
                    new OracleParameter(":JZPZ", OracleType.VarChar,4000),
                    new OracleParameter(":JZSHRBH", OracleType.VarChar),
                    new OracleParameter(":JZSHR", OracleType.VarChar),
                    new OracleParameter(":JZSHSJ", OracleType.DateTime)};

            parameters[0].Value = ZZZT;
            parameters[1].Value = JZPZ;
            parameters[2].Value = SHRGH;
            parameters[3].Value = SHR;
            parameters[4].Value = SHSJ;         

            int rows = 0;
            try
            {
                //rows = DbHelperOra.ExecuteSql("update YX_DZJZ_JZJBXX set zzzt='2'");
               rows = DbHelperOra.ExecuteSql(strSql.ToString() ,parameters);
            }
            catch (Exception ex)
            {
                System.Collections.Generic.Dictionary<string, object> parameters2 = new System.Collections.Generic.Dictionary<string, object>();
                parameters2.Add("BMSAH", BMSAHlist);
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool UpdateByZZZTList(string BMSAHlist, string SHR, DateTime SHSJ, string SHRGH, string ZZZT, string JZPZ)", "EDRS.OracleDAL.YX_DZJZ_JZJBXX", strSql.ToString(), parameters2);
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

		#endregion  BasicMethod
	
	}
}

