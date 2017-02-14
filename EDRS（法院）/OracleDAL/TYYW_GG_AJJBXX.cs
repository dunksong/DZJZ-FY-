
using System;
using System.Data;
using System.Text;
using System.Data.OracleClient;
using System.Web;
using EDRS.IDAL;
using Maticsoft.DBUtility;
using EDRS.Common;//Please add references
namespace EDRS.OracleDAL
{
	/// <summary>
	/// 数据访问类:TYYW_GG_AJJBXX
	/// </summary>
	public partial class TYYW_GG_AJJBXX:ITYYW_GG_AJJBXX
    {
        public HttpRequest context = null;//客户端对象，用于记录日志，客户端信息
        public void SetHttpContext(System.Web.HttpRequest _context)
        {
            this.context = _context;
        }
        public TYYW_GG_AJJBXX() { }
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string BMSAH)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select count(1) from TYYW_GG_AJJBXX");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
			strSql.Append(" where BMSAH=:BMSAH ");
			OracleParameter[] parameters = {
					new OracleParameter(":BMSAH", OracleType.VarChar,100)			};
			parameters[0].Value = BMSAH;
            try
            {
                return DbHelperOra.Exists(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Exists(string BMSAH)", "EDRS.OracleDAL.TYYW_GG_AJJBXX", strSql.ToString(), parameters);
            }
            return false;
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EDRS.Model.TYYW_GG_AJJBXX model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into TYYW_GG_AJJBXX(");
            strSql.Append("BMSAH,TYSAH,SFSC,SFYGXTSJ,CBDW_BM,CBDW_MC,FQDWBM,FQL,CJSJ,ZHXGSJ,SLRQ,AJMC,AJLB_BM,AJLB_MC,ZCJG_DWDM,ZCJG_DWMC,YSDW_DWDM,YSDW_DWMC,YSWSWH,YSAY_AYDM,YSAY_AYMC,YSQTAY_AYDMS,YSQTAY_AYMCS,CBRGH,CBR,CBBM_BM,CBBM_MC,AJZT,SFSWAJ,SFDBAJ,ZXHD_MC,WCRQ,GDRQ,GDRGH,GDR,AQZY,DQJD,BLKSRQ,BLTS,DQRQ,BJRQ,YJZT,JYYJZT,JYYJHCQXYRGS,LCSLBH,FXDJ,SFGZAJ,FZ,YSYJ_DM,YSYJ_MC,SFJBAJ,ZXHD_DM,DQYJJD,YASCSSJD_DM,YASCSSJD_MC,YSRJDH,XYR,SFZH,TARYXX,SHR,SHSJ,ZJS,DJJ,ZYS)");
			strSql.Append(" values (");
            strSql.Append(":BMSAH,:TYSAH,:SFSC,:SFYGXTSJ,:CBDW_BM,:CBDW_MC,:FQDWBM,:FQL,:CJSJ,:ZHXGSJ,:SLRQ,:AJMC,:AJLB_BM,:AJLB_MC,:ZCJG_DWDM,:ZCJG_DWMC,:YSDW_DWDM,:YSDW_DWMC,:YSWSWH,:YSAY_AYDM,:YSAY_AYMC,:YSQTAY_AYDMS,:YSQTAY_AYMCS,:CBRGH,:CBR,:CBBM_BM,:CBBM_MC,:AJZT,:SFSWAJ,:SFDBAJ,:ZXHD_MC,:WCRQ,:GDRQ,:GDRGH,:GDR,:AQZY,:DQJD,:BLKSRQ,:BLTS,:DQRQ,:BJRQ,:YJZT,:JYYJZT,:JYYJHCQXYRGS,:LCSLBH,:FXDJ,:SFGZAJ,:FZ,:YSYJ_DM,:YSYJ_MC,:SFJBAJ,:ZXHD_DM,:DQYJJD,:YASCSSJD_DM,:YASCSSJD_MC,:YSRJDH,:XYR,:SFZH,:TARYXX,:SHR,:SHSJ,:ZJS,:DJJ,:ZYS)");
			OracleParameter[] parameters = {
					new OracleParameter(":BMSAH", OracleType.VarChar,100),
					new OracleParameter(":TYSAH", OracleType.Char,17),
					new OracleParameter(":SFSC", OracleType.Char,1),
					new OracleParameter(":SFYGXTSJ", OracleType.Char,1),
					new OracleParameter(":CBDW_BM", OracleType.VarChar,50),
					new OracleParameter(":CBDW_MC", OracleType.VarChar,300),
					new OracleParameter(":FQDWBM", OracleType.Number,4),
					new OracleParameter(":FQL", OracleType.VarChar,6),
					new OracleParameter(":CJSJ", OracleType.DateTime),
					new OracleParameter(":ZHXGSJ", OracleType.DateTime),
					new OracleParameter(":SLRQ", OracleType.DateTime),
					new OracleParameter(":AJMC", OracleType.VarChar,300),
					new OracleParameter(":AJLB_BM", OracleType.VarChar,50),
					new OracleParameter(":AJLB_MC", OracleType.VarChar,300),
					new OracleParameter(":ZCJG_DWDM", OracleType.Char,13),
					new OracleParameter(":ZCJG_DWMC", OracleType.VarChar,300),
					new OracleParameter(":YSDW_DWDM", OracleType.Char,13),
					new OracleParameter(":YSDW_DWMC", OracleType.VarChar,300),
					new OracleParameter(":YSWSWH", OracleType.VarChar,300),
					new OracleParameter(":YSAY_AYDM", OracleType.Char,13),
					new OracleParameter(":YSAY_AYMC", OracleType.VarChar,300),
					new OracleParameter(":YSQTAY_AYDMS", OracleType.VarChar,130),
					new OracleParameter(":YSQTAY_AYMCS", OracleType.VarChar,3000),
					new OracleParameter(":CBRGH", OracleType.Char,4),
					new OracleParameter(":CBR", OracleType.VarChar,60),
					new OracleParameter(":CBBM_BM", OracleType.Char,4),
					new OracleParameter(":CBBM_MC", OracleType.VarChar,300),
					new OracleParameter(":AJZT", OracleType.Char,1),
					new OracleParameter(":SFSWAJ", OracleType.Char,1),
					new OracleParameter(":SFDBAJ", OracleType.Char,1),
					new OracleParameter(":ZXHD_MC", OracleType.VarChar,300),
					new OracleParameter(":WCRQ", OracleType.DateTime),
					new OracleParameter(":GDRQ", OracleType.DateTime),
					new OracleParameter(":GDRGH", OracleType.Char,4),
					new OracleParameter(":GDR", OracleType.VarChar,60),
					new OracleParameter(":AQZY", OracleType.VarChar,4000),
					new OracleParameter(":DQJD", OracleType.VarChar,3000),
					new OracleParameter(":BLKSRQ", OracleType.DateTime),
					new OracleParameter(":BLTS", OracleType.Number,4),
					new OracleParameter(":DQRQ", OracleType.DateTime),
					new OracleParameter(":BJRQ", OracleType.DateTime),
					new OracleParameter(":YJZT", OracleType.Char,1),
					new OracleParameter(":JYYJZT", OracleType.Char,1),
					new OracleParameter(":JYYJHCQXYRGS", OracleType.Number,4),
					new OracleParameter(":LCSLBH", OracleType.Char,2),
					new OracleParameter(":FXDJ", OracleType.Char,1),
					new OracleParameter(":SFGZAJ", OracleType.Char,1),
					new OracleParameter(":FZ", OracleType.VarChar,4000),
					new OracleParameter(":YSYJ_DM", OracleType.Char,13),
					new OracleParameter(":YSYJ_MC", OracleType.VarChar,300),
					new OracleParameter(":SFJBAJ", OracleType.Char,1),
					new OracleParameter(":ZXHD_DM", OracleType.Char,13),
					new OracleParameter(":DQYJJD", OracleType.VarChar,300),
					new OracleParameter(":YASCSSJD_DM", OracleType.Char,13),
					new OracleParameter(":YASCSSJD_MC", OracleType.VarChar,300),
					new OracleParameter(":YSRJDH", OracleType.VarChar,300),
                    new OracleParameter(":XYR", OracleType.VarChar,500),
                    new OracleParameter(":SFZH", OracleType.VarChar,100),
                    new OracleParameter(":TARYXX", OracleType.VarChar,2000),
                    new OracleParameter(":SHR", OracleType.VarChar,100),
                    new OracleParameter(":SHSJ", OracleType.DateTime),
                    new OracleParameter(":ZJS", OracleType.Number,18),
                    new OracleParameter(":DJJ", OracleType.VarChar,100),
                    new OracleParameter(":ZYS", OracleType.Number,18)
                                           };
           	parameters[0].Value = model.BMSAH ?? "";
			parameters[1].Value = model.TYSAH ?? "";
            parameters[2].Value = model.SFSC ?? "";
			parameters[3].Value = model.SFYGXTSJ ?? "";
			parameters[4].Value = model.CBDW_BM ?? "";
			parameters[5].Value = model.CBDW_MC ?? "";
            parameters[6].Value = model.FQDWBM;
			parameters[7].Value = model.FQL ?? "";
            parameters[8].Value = model.CJSJ;
            parameters[9].Value = model.ZHXGSJ;
            parameters[10].Value = model.SLRQ;
			parameters[11].Value = model.AJMC ?? "";
			parameters[12].Value = model.AJLB_BM ?? "";
			parameters[13].Value = model.AJLB_MC ?? "";
			parameters[14].Value = model.ZCJG_DWDM ?? "";
			parameters[15].Value = model.ZCJG_DWMC ?? "";
			parameters[16].Value = model.YSDW_DWDM ?? "";
			parameters[17].Value = model.YSDW_DWMC ?? "";
			parameters[18].Value = model.YSWSWH ?? "";
			parameters[19].Value = model.YSAY_AYDM ?? "";
			parameters[20].Value = model.YSAY_AYMC ?? "";
			parameters[21].Value = model.YSQTAY_AYDMS ?? "";
			parameters[22].Value = model.YSQTAY_AYMCS ?? "";
			parameters[23].Value = model.CBRGH ?? "";
			parameters[24].Value = model.CBR ?? "";
			parameters[25].Value = model.CBBM_BM ?? "";
			parameters[26].Value = model.CBBM_MC ?? "";
			parameters[27].Value = model.AJZT ?? "";
			parameters[28].Value = model.SFSWAJ ?? "";
			parameters[29].Value = model.SFDBAJ ?? "";
			parameters[30].Value = model.ZXHD_MC ?? "";
            parameters[31].Value = model.WCRQ ?? Convert.ToDateTime("1900-01-01");
            parameters[32].Value = model.GDRQ ?? Convert.ToDateTime("1900-01-01");
			parameters[33].Value = model.GDRGH ?? "";
			parameters[34].Value = model.GDR ?? "";
			parameters[35].Value = model.AQZY ?? "";
			parameters[36].Value = model.DQJD ?? "";
            parameters[37].Value = model.BLKSRQ ?? Convert.ToDateTime("1900-01-01");
            parameters[38].Value = model.BLTS ?? 0;
            parameters[39].Value = model.DQRQ ?? Convert.ToDateTime("1900-01-01");
            parameters[40].Value = model.BJRQ ?? Convert.ToDateTime("1900-01-01");
			parameters[41].Value = model.YJZT ?? "";
			parameters[42].Value = model.JYYJZT ?? "";
            parameters[43].Value = model.JYYJHCQXYRGS ?? 0;
			parameters[44].Value = model.LCSLBH ?? "";
			parameters[45].Value = model.FXDJ ?? "";
			parameters[46].Value = model.SFGZAJ ?? "";
			parameters[47].Value = model.FZ ?? "";
			parameters[48].Value = model.YSYJ_DM ?? "";
			parameters[49].Value = model.YSYJ_MC ?? "";
			parameters[50].Value = model.SFJBAJ ?? "";
			parameters[51].Value = model.ZXHD_DM ?? "";
			parameters[52].Value = model.DQYJJD ?? "";
			parameters[53].Value = model.YASCSSJD_DM ?? "";
			parameters[54].Value = model.YASCSSJD_MC ?? "";
			parameters[55].Value = model.YSRJDH ?? "";
            parameters[56].Value = model.XYR ?? "";
            parameters[57].Value = model.SFZH ?? "";
            parameters[58].Value = model.TARYXX ?? "";
            parameters[59].Value = model.SHR ?? "";
            parameters[60].Value = model.SHSJ ?? DateTime.Now;
            parameters[61].Value = model.ZJS;
            parameters[62].Value = model.DJJ ?? "";
            parameters[63].Value = model.ZYS;

            try
            {
                int rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
                if (rows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Add(EDRS.Model.TYYW_GG_AJJBXX model)", "EDRS.OracleDAL.TYYW_GG_AJJBXX", strSql.ToString(), parameters);
            }
            return false;
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(EDRS.Model.TYYW_GG_AJJBXX model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update TYYW_GG_AJJBXX set ");
			strSql.Append("TYSAH=:TYSAH,");
			strSql.Append("SFSC=:SFSC,");
			strSql.Append("SFYGXTSJ=:SFYGXTSJ,");
			strSql.Append("CBDW_BM=:CBDW_BM,");
			strSql.Append("CBDW_MC=:CBDW_MC,");
			strSql.Append("FQDWBM=:FQDWBM,");
			strSql.Append("FQL=:FQL,");
			strSql.Append("CJSJ=:CJSJ,");
			strSql.Append("ZHXGSJ=:ZHXGSJ,");
			strSql.Append("SLRQ=:SLRQ,");
			strSql.Append("AJMC=:AJMC,");
			strSql.Append("AJLB_BM=:AJLB_BM,");
			strSql.Append("AJLB_MC=:AJLB_MC,");
			strSql.Append("ZCJG_DWDM=:ZCJG_DWDM,");
			strSql.Append("ZCJG_DWMC=:ZCJG_DWMC,");
			strSql.Append("YSDW_DWDM=:YSDW_DWDM,");
			strSql.Append("YSDW_DWMC=:YSDW_DWMC,");
			strSql.Append("YSWSWH=:YSWSWH,");
			strSql.Append("YSAY_AYDM=:YSAY_AYDM,");
			strSql.Append("YSAY_AYMC=:YSAY_AYMC,");
			strSql.Append("YSQTAY_AYDMS=:YSQTAY_AYDMS,");
			strSql.Append("YSQTAY_AYMCS=:YSQTAY_AYMCS,");
			strSql.Append("CBRGH=:CBRGH,");
			strSql.Append("CBR=:CBR,");
			strSql.Append("CBBM_BM=:CBBM_BM,");
			strSql.Append("CBBM_MC=:CBBM_MC,");
			strSql.Append("AJZT=:AJZT,");
			strSql.Append("SFSWAJ=:SFSWAJ,");
			strSql.Append("SFDBAJ=:SFDBAJ,");
			strSql.Append("ZXHD_MC=:ZXHD_MC,");
			strSql.Append("WCRQ=:WCRQ,");
			strSql.Append("GDRQ=:GDRQ,");
			strSql.Append("GDRGH=:GDRGH,");
			strSql.Append("GDR=:GDR,");
			strSql.Append("AQZY=:AQZY,");
			strSql.Append("DQJD=:DQJD,");
			strSql.Append("BLKSRQ=:BLKSRQ,");
			strSql.Append("BLTS=:BLTS,");
			strSql.Append("DQRQ=:DQRQ,");
			strSql.Append("BJRQ=:BJRQ,");
			strSql.Append("YJZT=:YJZT,");
			strSql.Append("JYYJZT=:JYYJZT,");
			strSql.Append("JYYJHCQXYRGS=:JYYJHCQXYRGS,");
			strSql.Append("LCSLBH=:LCSLBH,");
			strSql.Append("FXDJ=:FXDJ,");
			strSql.Append("SFGZAJ=:SFGZAJ,");
			strSql.Append("FZ=:FZ,");
			strSql.Append("YSYJ_DM=:YSYJ_DM,");
			strSql.Append("YSYJ_MC=:YSYJ_MC,");
			strSql.Append("SFJBAJ=:SFJBAJ,");
			strSql.Append("ZXHD_DM=:ZXHD_DM,");
			strSql.Append("DQYJJD=:DQYJJD,");
			strSql.Append("YASCSSJD_DM=:YASCSSJD_DM,");
			strSql.Append("YASCSSJD_MC=:YASCSSJD_MC,");
			strSql.Append("YSRJDH=:YSRJDH,");
            strSql.Append("XYR=:XYR,");
            strSql.Append("SFZH=:SFZH,");
            strSql.Append("TARYXX=:TARYXX,");
            strSql.Append("SHR=:SHR,");
            strSql.Append("SHSJ=:SHSJ,");
            strSql.Append("ZJS=:ZJS,");
            strSql.Append("DJJ=:DJJ,");
            strSql.Append("ZYS=:ZYS");

			strSql.Append(" where BMSAH=:BMSAH ");
			OracleParameter[] parameters = {
					new OracleParameter(":TYSAH", OracleType.Char,17),
					new OracleParameter(":SFSC", OracleType.Char,1),
					new OracleParameter(":SFYGXTSJ", OracleType.Char,1),
					new OracleParameter(":CBDW_BM", OracleType.VarChar,50),
					new OracleParameter(":CBDW_MC", OracleType.VarChar,300),
					new OracleParameter(":FQDWBM", OracleType.Number,4),
					new OracleParameter(":FQL", OracleType.VarChar,6),
					new OracleParameter(":CJSJ", OracleType.DateTime),
					new OracleParameter(":ZHXGSJ", OracleType.DateTime),
					new OracleParameter(":SLRQ", OracleType.DateTime),
					new OracleParameter(":AJMC", OracleType.VarChar,300),
					new OracleParameter(":AJLB_BM", OracleType.VarChar,50),
					new OracleParameter(":AJLB_MC", OracleType.VarChar,300),
					new OracleParameter(":ZCJG_DWDM", OracleType.Char,13),
					new OracleParameter(":ZCJG_DWMC", OracleType.VarChar,300),
					new OracleParameter(":YSDW_DWDM", OracleType.Char,13),
					new OracleParameter(":YSDW_DWMC", OracleType.VarChar,300),
					new OracleParameter(":YSWSWH", OracleType.VarChar,300),
					new OracleParameter(":YSAY_AYDM", OracleType.Char,13),
					new OracleParameter(":YSAY_AYMC", OracleType.VarChar,300),
					new OracleParameter(":YSQTAY_AYDMS", OracleType.VarChar,130),
					new OracleParameter(":YSQTAY_AYMCS", OracleType.VarChar,3000),
					new OracleParameter(":CBRGH", OracleType.Char,4),
					new OracleParameter(":CBR", OracleType.VarChar,60),
					new OracleParameter(":CBBM_BM", OracleType.Char,4),
					new OracleParameter(":CBBM_MC", OracleType.VarChar,300),
					new OracleParameter(":AJZT", OracleType.Char,1),
					new OracleParameter(":SFSWAJ", OracleType.Char,1),
					new OracleParameter(":SFDBAJ", OracleType.Char,1),
					new OracleParameter(":ZXHD_MC", OracleType.VarChar,300),
					new OracleParameter(":WCRQ", OracleType.DateTime),
					new OracleParameter(":GDRQ", OracleType.DateTime),
					new OracleParameter(":GDRGH", OracleType.Char,4),
					new OracleParameter(":GDR", OracleType.VarChar,60),
					new OracleParameter(":AQZY", OracleType.VarChar,4000),
					new OracleParameter(":DQJD", OracleType.VarChar,3000),
					new OracleParameter(":BLKSRQ", OracleType.DateTime),
					new OracleParameter(":BLTS", OracleType.Number,4),
					new OracleParameter(":DQRQ", OracleType.DateTime),
					new OracleParameter(":BJRQ", OracleType.DateTime),
					new OracleParameter(":YJZT", OracleType.Char,1),
					new OracleParameter(":JYYJZT", OracleType.Char,1),
					new OracleParameter(":JYYJHCQXYRGS", OracleType.Number,4),
					new OracleParameter(":LCSLBH", OracleType.Char,2),
					new OracleParameter(":FXDJ", OracleType.Char,1),
					new OracleParameter(":SFGZAJ", OracleType.Char,1),
					new OracleParameter(":FZ", OracleType.VarChar,4000),
					new OracleParameter(":YSYJ_DM", OracleType.Char,13),
					new OracleParameter(":YSYJ_MC", OracleType.VarChar,300),
					new OracleParameter(":SFJBAJ", OracleType.Char,1),
					new OracleParameter(":ZXHD_DM", OracleType.Char,13),
					new OracleParameter(":DQYJJD", OracleType.VarChar,300),
					new OracleParameter(":YASCSSJD_DM", OracleType.Char,13),
					new OracleParameter(":YASCSSJD_MC", OracleType.VarChar,300),
					new OracleParameter(":YSRJDH", OracleType.VarChar,300),
					new OracleParameter(":BMSAH", OracleType.VarChar,100),
                    new OracleParameter(":XYR", OracleType.VarChar,500),
					new OracleParameter(":SFZH", OracleType.VarChar,100),
					new OracleParameter(":TARYXX", OracleType.VarChar,2000),
                    new OracleParameter(":SHR", OracleType.VarChar,100),
                    new OracleParameter(":SHSJ", OracleType.DateTime),
                    new OracleParameter(":ZJS", OracleType.Number,18),
                    new OracleParameter(":DJJ", OracleType.VarChar,100),
                    new OracleParameter(":ZYS", OracleType.Number,18)};
                   
			parameters[0].Value = model.TYSAH ?? "";
			parameters[1].Value = model.SFSC ?? "";
			parameters[2].Value = model.SFYGXTSJ ?? "";
			parameters[3].Value = model.CBDW_BM ?? "";
			parameters[4].Value = model.CBDW_MC ?? "";
			parameters[5].Value = model.FQDWBM;
			parameters[6].Value = model.FQL ?? "";
			parameters[7].Value = model.CJSJ;
			parameters[8].Value = model.ZHXGSJ;
			parameters[9].Value = model.SLRQ;
			parameters[10].Value = model.AJMC ?? "";
			parameters[11].Value = model.AJLB_BM ?? "";
			parameters[12].Value = model.AJLB_MC ?? "";
			parameters[13].Value = model.ZCJG_DWDM ?? "";
			parameters[14].Value = model.ZCJG_DWMC ?? "";
			parameters[15].Value = model.YSDW_DWDM ?? "";
			parameters[16].Value = model.YSDW_DWMC ?? "";
			parameters[17].Value = model.YSWSWH ?? "";
			parameters[18].Value = model.YSAY_AYDM ?? "";
			parameters[19].Value = model.YSAY_AYMC ?? "";
			parameters[20].Value = model.YSQTAY_AYDMS ?? "";
			parameters[21].Value = model.YSQTAY_AYMCS ?? "";
			parameters[22].Value = model.CBRGH ?? "";
			parameters[23].Value = model.CBR ?? "";
			parameters[24].Value = model.CBBM_BM ?? "";
			parameters[25].Value = model.CBBM_MC ?? "";
			parameters[26].Value = model.AJZT ?? "";
			parameters[27].Value = model.SFSWAJ ?? "";
			parameters[28].Value = model.SFDBAJ ?? "";
			parameters[29].Value = model.ZXHD_MC ?? "";
			parameters[30].Value = model.WCRQ ?? Convert.ToDateTime("1900-01-01");
            parameters[31].Value = model.GDRQ ?? Convert.ToDateTime("1900-01-01");
			parameters[32].Value = model.GDRGH ?? "";
			parameters[33].Value = model.GDR ?? "";
			parameters[34].Value = model.AQZY ?? "";
			parameters[35].Value = model.DQJD ?? "";
            parameters[36].Value = model.BLKSRQ ?? Convert.ToDateTime("1900-01-01");
			parameters[37].Value = model.BLTS ?? 0;
            parameters[38].Value = model.DQRQ ?? Convert.ToDateTime("1900-01-01");
            parameters[39].Value = model.BJRQ ?? Convert.ToDateTime("1900-01-01");
			parameters[40].Value = model.YJZT ?? "";
			parameters[41].Value = model.JYYJZT ?? "";
			parameters[42].Value = model.JYYJHCQXYRGS ?? 0;
			parameters[43].Value = model.LCSLBH ?? "";
			parameters[44].Value = model.FXDJ ?? "";
			parameters[45].Value = model.SFGZAJ ?? "";
			parameters[46].Value = model.FZ ?? "";
			parameters[47].Value = model.YSYJ_DM ?? "";
			parameters[48].Value = model.YSYJ_MC ?? "";
			parameters[49].Value = model.SFJBAJ ?? "";
			parameters[50].Value = model.ZXHD_DM ?? "";
			parameters[51].Value = model.DQYJJD ?? "";
			parameters[52].Value = model.YASCSSJD_DM ?? "";
			parameters[53].Value = model.YASCSSJD_MC ?? "";
            parameters[54].Value = model.YSRJDH ?? "";
			parameters[55].Value = model.BMSAH;
            parameters[56].Value = model.XYR ?? "";
            parameters[57].Value = model.SFZH ?? "";
            parameters[58].Value = model.TARYXX ?? "";
            parameters[59].Value = model.SHR ?? "";
            parameters[60].Value = model.SHSJ ?? DateTime.Now;
            parameters[61].Value = model.ZJS;
            parameters[62].Value = model.DJJ ?? "";
            parameters[63].Value = model.ZYS;
            try
            {
                int rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
                if (rows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Update(EDRS.Model.TYYW_GG_AJJBXX model)", "EDRS.OracleDAL.TYYW_GG_AJJBXX", strSql.ToString(), parameters);
            }
            return false;
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string BMSAH)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from TYYW_GG_AJJBXX ");
			strSql.Append(" where BMSAH=:BMSAH ");
			OracleParameter[] parameters = {
					new OracleParameter(":BMSAH", OracleType.VarChar,100)			};
			parameters[0].Value = BMSAH;

            try
            {
                int rows = DbHelperOra.ExecuteSql(strSql.ToString(), parameters);
                if (rows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool Delete(string BMSAH)", "EDRS.OracleDAL.TYYW_GG_AJJBXX", strSql.ToString(), parameters);
            }
            return false;
		}
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string BMSAHlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from TYYW_GG_AJJBXX ");
			strSql.Append(" where BMSAH in ("+BMSAHlist + ")  ");
            try
            {
                int rows = DbHelperOra.ExecuteSql(strSql.ToString());
                if (rows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                System.Collections.Generic.Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
                parameters.Add("BMSAH", BMSAHlist);
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool DeleteList(string BMSAHlist )", "EDRS.OracleDAL.TYYW_GG_AJJBXX", strSql.ToString(), parameters);
            }
            return false;
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public EDRS.Model.TYYW_GG_AJJBXX GetModel(string BMSAH)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select AJBH,jz.WSBH,jz.WSMC,ZZZT,t.BMSAH,TYSAH,SFSC,SFYGXTSJ,CBDW_BM,CBDW_MC,FQDWBM,FQL,CJSJ,ZHXGSJ,SLRQ,AJMC,AJLB_BM,AJLB_MC,ZCJG_DWDM,ZCJG_DWMC,YSDW_DWDM,YSDW_DWMC,YSWSWH,YSAY_AYDM,YSAY_AYMC,YSQTAY_AYDMS,YSQTAY_AYMCS,CBRGH,CBR,CBBM_BM,CBBM_MC,AJZT,SFSWAJ,SFDBAJ,ZXHD_MC,WCRQ,GDRQ,GDRGH,GDR,AQZY,DQJD,BLKSRQ,BLTS,DQRQ,BJRQ,YJZT,JYYJZT,JYYJHCQXYRGS,LCSLBH,FXDJ,SFGZAJ,FZ,YSYJ_DM,YSYJ_MC,SFJBAJ,ZXHD_DM,DQYJJD,YASCSSJD_DM,YASCSSJD_MC,YSRJDH,XYR,SFZH,TARYXX,SHR,ZJS,DJJ,ZYS from TYYW_GG_AJJBXX  t left join (select BMSAH,AJBH,WSBH,WSMC,ZZZT,JZPZ from yx_dzjz_jzjbxx) jz on(t.Bmsah = jz.BMSAH)");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
            strSql.Append(" where t.BMSAH=:BMSAH ");
			OracleParameter[] parameters = {
					new OracleParameter(":BMSAH", OracleType.VarChar,100)			};
			parameters[0].Value = BMSAH;

			EDRS.Model.TYYW_GG_AJJBXX model=new EDRS.Model.TYYW_GG_AJJBXX();


            DataSet ds = null;
            try
            {
                ds = DbHelperOra.Query(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public EDRS.Model.TYYW_GG_AJJBXX GetModel(string BMSAH)", "EDRS.OracleDAL.TYYW_GG_AJJBXX", strSql.ToString(), parameters);
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
		public EDRS.Model.TYYW_GG_AJJBXX DataRowToModel(DataRow row)
		{
			EDRS.Model.TYYW_GG_AJJBXX model=new EDRS.Model.TYYW_GG_AJJBXX();
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
				if(row["SFSC"]!=null)
				{
					model.SFSC=row["SFSC"].ToString();
				}
				if(row["SFYGXTSJ"]!=null)
				{
					model.SFYGXTSJ=row["SFYGXTSJ"].ToString();
				}
				if(row["CBDW_BM"]!=null)
				{
					model.CBDW_BM=row["CBDW_BM"].ToString();
				}
				if(row["CBDW_MC"]!=null)
				{
					model.CBDW_MC=row["CBDW_MC"].ToString();
				}
				if(row["FQDWBM"]!=null && row["FQDWBM"].ToString()!="")
				{
					model.FQDWBM=decimal.Parse(row["FQDWBM"].ToString());
				}
				if(row["FQL"]!=null)
				{
					model.FQL=row["FQL"].ToString();
				}
				if(row["CJSJ"]!=null && row["CJSJ"].ToString()!="")
				{
					model.CJSJ=DateTime.Parse(row["CJSJ"].ToString());
				}
				if(row["ZHXGSJ"]!=null && row["ZHXGSJ"].ToString()!="")
				{
					model.ZHXGSJ=DateTime.Parse(row["ZHXGSJ"].ToString());
				}
				if(row["SLRQ"]!=null && row["SLRQ"].ToString()!="")
				{
					model.SLRQ=DateTime.Parse(row["SLRQ"].ToString());
				}
				if(row["AJMC"]!=null)
				{
					model.AJMC=row["AJMC"].ToString();
				}
				if(row["AJLB_BM"]!=null)
				{
					model.AJLB_BM=row["AJLB_BM"].ToString();
				}
				if(row["AJLB_MC"]!=null)
				{
					model.AJLB_MC=row["AJLB_MC"].ToString();
				}
				if(row["ZCJG_DWDM"]!=null)
				{
					model.ZCJG_DWDM=row["ZCJG_DWDM"].ToString();
				}
				if(row["ZCJG_DWMC"]!=null)
				{
					model.ZCJG_DWMC=row["ZCJG_DWMC"].ToString();
				}
				if(row["YSDW_DWDM"]!=null)
				{
					model.YSDW_DWDM=row["YSDW_DWDM"].ToString();
				}
				if(row["YSDW_DWMC"]!=null)
				{
					model.YSDW_DWMC=row["YSDW_DWMC"].ToString();
				}
				if(row["YSWSWH"]!=null)
				{
					model.YSWSWH=row["YSWSWH"].ToString();
				}
				if(row["YSAY_AYDM"]!=null)
				{
					model.YSAY_AYDM=row["YSAY_AYDM"].ToString();
				}
				if(row["YSAY_AYMC"]!=null)
				{
					model.YSAY_AYMC=row["YSAY_AYMC"].ToString();
				}
				if(row["YSQTAY_AYDMS"]!=null)
				{
					model.YSQTAY_AYDMS=row["YSQTAY_AYDMS"].ToString();
				}
				if(row["YSQTAY_AYMCS"]!=null)
				{
					model.YSQTAY_AYMCS=row["YSQTAY_AYMCS"].ToString();
				}
				if(row["CBRGH"]!=null)
				{
					model.CBRGH=row["CBRGH"].ToString();
				}
				if(row["CBR"]!=null)
				{
					model.CBR=row["CBR"].ToString();
				}
				if(row["CBBM_BM"]!=null)
				{
					model.CBBM_BM=row["CBBM_BM"].ToString();
				}
				if(row["CBBM_MC"]!=null)
				{
					model.CBBM_MC=row["CBBM_MC"].ToString();
				}
				if(row["AJZT"]!=null)
				{
					model.AJZT=row["AJZT"].ToString();
				}
				if(row["SFSWAJ"]!=null)
				{
					model.SFSWAJ=row["SFSWAJ"].ToString();
				}
				if(row["SFDBAJ"]!=null)
				{
					model.SFDBAJ=row["SFDBAJ"].ToString();
				}
				if(row["ZXHD_MC"]!=null)
				{
					model.ZXHD_MC=row["ZXHD_MC"].ToString();
				}
				if(row["WCRQ"]!=null && row["WCRQ"].ToString()!="")
				{
					model.WCRQ=DateTime.Parse(row["WCRQ"].ToString());
				}
				if(row["GDRQ"]!=null && row["GDRQ"].ToString()!="")
				{
					model.GDRQ=DateTime.Parse(row["GDRQ"].ToString());
				}
				if(row["GDRGH"]!=null)
				{
					model.GDRGH=row["GDRGH"].ToString();
				}
				if(row["GDR"]!=null)
				{
					model.GDR=row["GDR"].ToString();
				}
				if(row["AQZY"]!=null)
				{
					model.AQZY=row["AQZY"].ToString();
				}
				if(row["DQJD"]!=null)
				{
					model.DQJD=row["DQJD"].ToString();
				}
				if(row["BLKSRQ"]!=null && row["BLKSRQ"].ToString()!="")
				{
					model.BLKSRQ=DateTime.Parse(row["BLKSRQ"].ToString());
				}
				if(row["BLTS"]!=null && row["BLTS"].ToString()!="")
				{
					model.BLTS=decimal.Parse(row["BLTS"].ToString());
				}
				if(row["DQRQ"]!=null && row["DQRQ"].ToString()!="")
				{
					model.DQRQ=DateTime.Parse(row["DQRQ"].ToString());
				}
				if(row["BJRQ"]!=null && row["BJRQ"].ToString()!="")
				{
					model.BJRQ=DateTime.Parse(row["BJRQ"].ToString());
				}
				if(row["YJZT"]!=null)
				{
					model.YJZT=row["YJZT"].ToString();
				}
				if(row["JYYJZT"]!=null)
				{
					model.JYYJZT=row["JYYJZT"].ToString();
				}
				if(row["JYYJHCQXYRGS"]!=null && row["JYYJHCQXYRGS"].ToString()!="")
				{
					model.JYYJHCQXYRGS=decimal.Parse(row["JYYJHCQXYRGS"].ToString());
				}
				if(row["LCSLBH"]!=null)
				{
					model.LCSLBH=row["LCSLBH"].ToString();
				}
				if(row["FXDJ"]!=null)
				{
					model.FXDJ=row["FXDJ"].ToString();
				}
				if(row["SFGZAJ"]!=null)
				{
					model.SFGZAJ=row["SFGZAJ"].ToString();
				}
				if(row["FZ"]!=null)
				{
					model.FZ=row["FZ"].ToString();
				}
				if(row["YSYJ_DM"]!=null)
				{
					model.YSYJ_DM=row["YSYJ_DM"].ToString();
				}
				if(row["YSYJ_MC"]!=null)
				{
					model.YSYJ_MC=row["YSYJ_MC"].ToString();
				}
				if(row["SFJBAJ"]!=null)
				{
					model.SFJBAJ=row["SFJBAJ"].ToString();
				}
				if(row["ZXHD_DM"]!=null)
				{
					model.ZXHD_DM=row["ZXHD_DM"].ToString();
				}
				if(row["DQYJJD"]!=null)
				{
					model.DQYJJD=row["DQYJJD"].ToString();
				}
				if(row["YASCSSJD_DM"]!=null)
				{
					model.YASCSSJD_DM=row["YASCSSJD_DM"].ToString();
				}
				if(row["YASCSSJD_MC"]!=null)
				{
					model.YASCSSJD_MC=row["YASCSSJD_MC"].ToString();
				}
				if(row["YSRJDH"]!=null)
				{
					model.YSRJDH=row["YSRJDH"].ToString();
				}
                if(row["XYR"]!=null)
				{
					model.XYR=row["XYR"].ToString();
				}
                if(row["SFZH"]!=null)
				{
					model.SFZH=row["SFZH"].ToString();
				}
                if(row["TARYXX"]!=null)
				{
					model.TARYXX=row["TARYXX"].ToString();
				}

                if (row["SHR"] != null)
                {
                    model.SHR = row["SHR"].ToString();
                }
                if (row["ZJS"] != null && row["ZJS"].ToString() != "")
                {
                    model.ZJS = decimal.Parse(row["ZJS"].ToString());
                }
                if (row["DJJ"] != null)
                {
                    model.DJJ = row["DJJ"].ToString();
                }
                if (row["ZYS"] != null && row["ZYS"].ToString() != "")
                {
                    model.ZYS = decimal.Parse(row["ZYS"].ToString());
                }
                if (row["AJBH"] != null)
                {
                    model.AJBH = row["AJBH"].ToString();
                }
                if (row["WSBH"] != null)
                {
                    model.WSBH = row["WSBH"].ToString();
                }
                if (row["WSMC"] != null)
                {
                    model.WSMC = row["WSMC"].ToString();
                }
                if (row["ZZZT"] != null)
                {
                    model.ZZZT = row["ZZZT"].ToString();
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
            strSql.Append("select BMSAH,TYSAH,SFSC,SFYGXTSJ,CBDW_BM,CBDW_MC,FQDWBM,FQL,CJSJ,ZHXGSJ,SLRQ,AJMC,AJLB_BM,AJLB_MC,ZCJG_DWDM,ZCJG_DWMC,YSDW_DWDM,YSDW_DWMC,YSWSWH,YSAY_AYDM,YSAY_AYMC,YSQTAY_AYDMS,YSQTAY_AYMCS,CBRGH,CBR,CBBM_BM,CBBM_MC,AJZT,SFSWAJ,SFDBAJ,ZXHD_MC,WCRQ,GDRQ,GDRGH,GDR,AQZY,DQJD,BLKSRQ,BLTS,DQRQ,BJRQ,YJZT,JYYJZT,JYYJHCQXYRGS,LCSLBH,FXDJ,SFGZAJ,FZ,YSYJ_DM,YSYJ_MC,SFJBAJ,ZXHD_DM,DQYJJD,YASCSSJD_DM,YASCSSJD_MC,YSRJDH,XYR,SFZH,TARYXX,SHR,ZJS,DJJ,ZYS");
			strSql.Append(" FROM TYYW_GG_AJJBXX ");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetList(string strWhere, params object[] objValues)", "EDRS.OracleDAL.TYYW_GG_AJJBXX", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
        public int GetRecordCount(string strWhere, params object[] objValues)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM TYYW_GG_AJJBXX ");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public int GetRecordCount(string strWhere, params object[] objValues)", "EDRS.OracleDAL.TYYW_GG_AJJBXX", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
				strSql.Append("order by T.BMSAH desc");
			}
			strSql.AppendFormat(")AS Ro, T.*  from TYYW_GG_AJJBXX{0} T ",ConfigHelper.GetConfigString("OrcDBLinq"));
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)", "EDRS.OracleDAL.TYYW_GG_AJJBXX", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
			parameters[0].Value = "TYYW_GG_AJJBXX";
			parameters[1].Value = "BMSAH";
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

