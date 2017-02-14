
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
    public partial class TYYW_GG_AJJBXX
    {

        /// <summary>
        /// 扩展分页获取数据列表
        /// </summary>
        public DataSet GetListByPageUnite(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)
        {
            string tableName = string.Format("(select JZ.JZSCRXM,JZ.CJSJ,d.CJSJ GGCJSJ,JZ.ZZZT,JZ.LAZZZT,d.BMSAH,AJLB_MC,CBDW_MC,CBBM_MC,d.CBR,DQJD, SLRQ,AJZT,DQRQ,BJRQ,WCRQ,dk.GDRQ,AJLB_BM,CBDW_BM,d.SFSC,SHR,ZJS,DJJ,ZYS,JZPZ,JZSHRBH,JZSHR,JZSHSJ,JZBH ,SMAJLA,SMAJCD");
            tableName += ",dk.ay AJMC,dk.sajg,dk.yg,dk.bg,dk.yyr,dk.sqzxr,dk.bzxr,dk.sarq,dk.jarq,dk.cjjg,dk.zxbd,dk.sljg,dk.zxjg,dk.jafs,dk.gldaxlh,dk.hytcy,dk.sjy,dk.zcs ,dk.djc,dk.ys,dk.ywt,dk.bgqx ";
            tableName += string.Format(" from TYYW_GG_AJJBXX d JOIN tyyw_gg_ajjbxxkz dk on d.bmsah = dk.ahmc LEFT JOIN yx_dzjz_jzjbxx JZ ON (d.bmsah = JZ.BMSAH) where d.SFSC='N' and JZ.SFSC = 'N')");

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.BMSAH desc");
            }
            strSql.AppendFormat(")AS Ro,T.* from {0} T ", tableName);
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetListByPageUnite(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)", "EDRS.OracleDAL.TYYW_GG_AJJBXX_Ex", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
        }



        /// <summary>
        /// 扩展获取记录总数
        /// </summary>
        public int GetRecordCountUnite(string strWhere, params object[] objValues)
        {
            //string tableName = "(select JZ.JZSCRXM,JZ.CJSJ,JZ.AJBH,JZ.WSBH,JZ.WSMC,JZ.ZZZT,AJMC,d.BMSAH,AJLB_MC,CBDW_MC,CBBM_MC,DQJD,SLRQ,AJZT,DQRQ,BJRQ,WCRQ,AJLB_BM,CBDW_BM,d.SFSC,XYR,SFZH,TARYXX,SHR,ZJS,DJJ,ZYS,JZPZ,JZSHRBH,JZSHR,JZSHSJ,JZBH  from TYYW_GG_AJJBXX d  JOIN tyyw_gg_ajjbxxkz dk on d.bmsah = dk.ahmc LEFT JOIN yx_dzjz_jzjbxx JZ ON (d.bmsah = JZ.BMSAH) where d.SFSC='N' and JZ.SFSC = 'N') T ";
            string tableName = string.Format("(select JZ.JZSCRXM,JZ.CJSJ,d.CJSJ GGCJSJ,JZ.ZZZT,JZ.LAZZZT,d.BMSAH,AJLB_MC,CBDW_MC,CBBM_MC,d.CBR,DQJD, SLRQ,AJZT,DQRQ,BJRQ,WCRQ,dk.GDRQ,AJLB_BM,CBDW_BM,d.SFSC,SHR,ZJS,DJJ,ZYS,JZPZ,JZSHRBH,JZSHR,JZSHSJ,JZBH ,SMAJLA,SMAJCD");
            tableName += ",dk.ay AJMC,dk.sajg,dk.yg,dk.bg,dk.yyr,dk.sqzxr,dk.bzxr,dk.sarq,dk.jarq,dk.cjjg,dk.zxbd,dk.sljg,dk.zxjg,dk.jafs,dk.gldaxlh,dk.hytcy,dk.sjy,dk.zcs ,dk.djc,dk.ys,dk.ywt,dk.bgqx ";
            tableName += string.Format(" from TYYW_GG_AJJBXX d JOIN tyyw_gg_ajjbxxkz dk on d.bmsah = dk.ahmc LEFT JOIN yx_dzjz_jzjbxx JZ ON (d.bmsah = JZ.BMSAH) where d.SFSC='N' and JZ.SFSC = 'N')");
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select count(1) FROM {0} ", tableName);
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where 1=1 " + strWhere);
            }
            object obj = null;
            try
            {
                obj = DbHelperOra.GetSingle(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public int GetRecordCountUnite(string strWhere, params object[] objValues)", "EDRS.OracleDAL.TYYW_GG_AJJBXX_Ex", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
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
        /// 增加一条数据
        /// </summary>
        public bool AddList(EDRS.Model.TYYW_GG_AJJBXX model,EDRS.Model.TYYW_GG_AJJBXXKZ modelkz,EDRS.Model.YX_DZJZ_JZJBXX modeljbxx)
        {
            System.Collections.Hashtable hash = new System.Collections.Hashtable();
            StringBuilder strSql = new StringBuilder();
            if (model != null)
            {
                #region 案件基本信息

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
                hash.Add(strSql.ToString(), parameters);
                #endregion
            }
            if (modelkz != null)
            {
                #region 案件基本信息扩展
                strSql.Clear();
                strSql.Append("insert into TYYW_GG_AJJBXXKZ(");
                strSql.Append("AHMC,AY,SAJG,YG,BG,YYR,SQZXR,BZXR,SARQ,JARQ,CJJG,ZXBD,SLJG,ZXJG,JAFS,GLDAXLH,HYTCY,CBR,SJY,ZCS,DJC,YS,GDRQ,YWT,BGQX,AJLBBM,H,NF,YWLBBM,YWLBMC,DWBM,DWMC,DWJC,AJLBMC,QAH,MLH,JH,CZRGH,CZRMC,CZSJ)");
                strSql.Append(" values (");
                strSql.Append(":AHMC,:AY,:SAJG,:YG,:BG,:YYR,:SQZXR,:BZXR,:SARQ,:JARQ,:CJJG,:ZXBD,:SLJG,:ZXJG,:JAFS,:GLDAXLH,:HYTCY,:CBR,:SJY,:ZCS,:DJC,:YS,:GDRQ,:YWT,:BGQX,:AJLBBM,:H,:NF,:YWLBBM,:YWLBMC,:DWBM,:DWMC,:DWJC,:AJLBMC,:QAH,:MLH,:JH,:CZRGH,:CZRMC,:CZSJ)");
                OracleParameter[] parameterskz = {
					new OracleParameter(":AHMC", OracleType.VarChar,200),
					new OracleParameter(":AY", OracleType.VarChar,500),
					new OracleParameter(":SAJG", OracleType.VarChar,200),
					new OracleParameter(":YG", OracleType.VarChar,200),
					new OracleParameter(":BG", OracleType.VarChar,200),
					new OracleParameter(":YYR", OracleType.VarChar,200),
					new OracleParameter(":SQZXR", OracleType.VarChar,50),
					new OracleParameter(":BZXR", OracleType.VarChar,50),
					new OracleParameter(":SARQ", OracleType.DateTime),
					new OracleParameter(":JARQ", OracleType.DateTime),
					new OracleParameter(":CJJG", OracleType.VarChar,200),
					new OracleParameter(":ZXBD", OracleType.VarChar,200),
					new OracleParameter(":SLJG", OracleType.VarChar,4000),
					new OracleParameter(":ZXJG", OracleType.VarChar,4000),
					new OracleParameter(":JAFS", OracleType.VarChar,200),
					new OracleParameter(":GLDAXLH", OracleType.VarChar,200),
					new OracleParameter(":HYTCY", OracleType.VarChar,200),
					new OracleParameter(":CBR", OracleType.VarChar,50),
					new OracleParameter(":SJY", OracleType.VarChar,50),
					new OracleParameter(":ZCS", OracleType.Number),
					new OracleParameter(":DJC", OracleType.Number),
					new OracleParameter(":YS", OracleType.Number),
					new OracleParameter(":GDRQ", OracleType.DateTime),
					new OracleParameter(":YWT", OracleType.VarChar,200),
					new OracleParameter(":BGQX", OracleType.VarChar,20),
					new OracleParameter(":AJLBBM", OracleType.VarChar,20),
					new OracleParameter(":H", OracleType.Number),
					new OracleParameter(":NF", OracleType.VarChar,4),
					new OracleParameter(":YWLBBM", OracleType.VarChar,20),
					new OracleParameter(":YWLBMC", OracleType.VarChar,50),
					new OracleParameter(":DWBM", OracleType.VarChar,50),
					new OracleParameter(":DWMC", OracleType.VarChar,500),
					new OracleParameter(":DWJC", OracleType.VarChar,200),			
					new OracleParameter(":AJLBMC", OracleType.VarChar,200),
					new OracleParameter(":QAH", OracleType.VarChar,20),
					new OracleParameter(":MLH", OracleType.VarChar,20),
					new OracleParameter(":JH", OracleType.VarChar,20),
					new OracleParameter(":CZRGH", OracleType.VarChar,20),
					new OracleParameter(":CZRMC", OracleType.VarChar,100),
					new OracleParameter(":CZSJ", OracleType.DateTime)
                                                 };
                parameterskz[0].Value = modelkz.AHMC ?? "";
                parameterskz[1].Value = modelkz.AY ?? "";
                parameterskz[2].Value = modelkz.SAJG ?? "";
                parameterskz[3].Value = modelkz.YG ?? "";
                parameterskz[4].Value = modelkz.BG ?? "";
                parameterskz[5].Value = modelkz.YYR ?? "";
                parameterskz[6].Value = modelkz.SQZXR ?? "";
                parameterskz[7].Value = modelkz.BZXR ?? "";
                parameterskz[8].Value = modelkz.SARQ;
                parameterskz[9].Value = modelkz.JARQ;
                parameterskz[10].Value = modelkz.CJJG ?? "";
                parameterskz[11].Value = modelkz.ZXBD ?? "";
                parameterskz[12].Value = modelkz.SLJG ?? "";
                parameterskz[13].Value = modelkz.ZXJG ?? "";
                parameterskz[14].Value = modelkz.JAFS ?? "";
                parameterskz[15].Value = modelkz.GLDAXLH ?? "";
                parameterskz[16].Value = modelkz.HYTCY ?? "";
                parameterskz[17].Value = modelkz.CBR ?? "";
                parameterskz[18].Value = modelkz.SJY ?? "";
                parameterskz[19].Value = modelkz.ZCS;
                parameterskz[20].Value = modelkz.DJC;
                parameterskz[21].Value = modelkz.YS;
                parameterskz[22].Value = modelkz.GDRQ;
                parameterskz[23].Value = modelkz.YWT ?? "";
                parameterskz[24].Value = modelkz.BGQX ?? "";
                parameterskz[25].Value = modelkz.AJLBBM ?? "";
                parameterskz[26].Value = modelkz.H;
                parameterskz[27].Value = modelkz.NF;
                parameterskz[28].Value = modelkz.YWLBBM ?? "";
                parameterskz[29].Value = modelkz.YWLBMC ?? "";
                parameterskz[30].Value = modelkz.DWBM ?? "";
                parameterskz[31].Value = modelkz.DWMC ?? "";
                parameterskz[32].Value = modelkz.DWJC ?? "";
                parameterskz[33].Value = modelkz.AJLBMC ?? "";
                parameterskz[34].Value = modelkz.QAH ?? "";
                parameterskz[35].Value = modelkz.MLH ?? "";
                parameterskz[36].Value = modelkz.JH ?? "";
                parameterskz[37].Value = modelkz.CZRGH ?? "";
                parameterskz[38].Value = modelkz.CZRMC ?? "";
                parameterskz[39].Value = modelkz.CZSJ;
            
                hash.Add(strSql.ToString(), parameterskz);
                #endregion
            }
            if (modeljbxx != null)
            {
                #region 卷宗基本信息
                strSql.Clear();
                strSql.Append("insert into YX_DZJZ_JZJBXX(");
                strSql.Append("JZBH,SFSC,CJSJ,ZHXGSJ,FQDWBM,FQL,DWBM,BMSAH,TYSAH,JZMC,JZLJ,JZSCSJ,JZSCRGH,JZSCRXM,JZMS,JZXGH,SFKYGX,GXYWBMJH,MRSFGKPZ, ACCOMPLICES, AJMB_BM, AJMB_MC, IDNUMBER, ISRECORD, SUSPECTNAME,WSBH,AJBH,ZZZT,JZPZ,JZSHRBH,JZSHR,JZSHSJ,WSMC,SMAJLA,SMAJCD,LAZZZT)");
                strSql.Append(" values (");
                strSql.Append(":JZBH,:SFSC,:CJSJ,:ZHXGSJ,:FQDWBM,:FQL,:DWBM,:BMSAH,:TYSAH,:JZMC,:JZLJ,:JZSCSJ,:JZSCRGH,:JZSCRXM,:JZMS,:JZXGH,:SFKYGX,:GXYWBMJH,:MRSFGKPZ, :ACCOMPLICES, :AJMB_BM, :AJMB_MC, :IDNUMBER, :ISRECORD, :SUSPECTNAME, :WSBH, :AJBH, :ZZZT,:JZPZ,:JZSHRBH,:JZSHR,:JZSHSJ,:WSMC,:SMAJLA,:SMAJCD,:LAZZZT)");
                OracleParameter[] parametersjbxx = {
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
                    new OracleParameter(":WSMC",OracleType.VarChar),
                    new OracleParameter(":SMAJLA",OracleType.VarChar),
                    new OracleParameter(":SMAJCD",OracleType.VarChar),
                    new OracleParameter(":LAZZZT", OracleType.VarChar,10)
                                           };
                parametersjbxx[0].Value = modeljbxx.JZBH ?? "";
                parametersjbxx[1].Value = modeljbxx.SFSC;
                parametersjbxx[2].Value = modeljbxx.CJSJ;
                parametersjbxx[3].Value = modeljbxx.ZHXGSJ;
                parametersjbxx[4].Value = modeljbxx.FQDWBM;
                parametersjbxx[5].Value = modeljbxx.FQL ?? "";
                parametersjbxx[6].Value = modeljbxx.DWBM ?? "";
                parametersjbxx[7].Value = modeljbxx.BMSAH ?? "";
                parametersjbxx[8].Value = modeljbxx.TYSAH ?? "";
                parametersjbxx[9].Value = modeljbxx.JZMC ?? "";
                parametersjbxx[10].Value = modeljbxx.JZLJ ?? "";
                parametersjbxx[11].Value = (object)modeljbxx.JZSCSJ ?? DBNull.Value;
                parametersjbxx[12].Value = modeljbxx.JZSCRGH ?? "";
                parametersjbxx[13].Value = modeljbxx.JZSCRXM ?? "";
                parametersjbxx[14].Value = modeljbxx.JZMS ?? "";
                parametersjbxx[15].Value = modeljbxx.JZXGH;
                parametersjbxx[16].Value = modeljbxx.SFKYGX;
                parametersjbxx[17].Value = modeljbxx.GXYWBMJH ?? "";
                parametersjbxx[18].Value = modeljbxx.MRSFGKPZ;
                parametersjbxx[19].Value = modeljbxx.Accomplices ?? "";
                parametersjbxx[20].Value = modeljbxx.Ajmb_bm ?? "";
                parametersjbxx[21].Value = modeljbxx.Ajmb_mc ?? "";
                parametersjbxx[22].Value = modeljbxx.Idnumber ?? "";
                parametersjbxx[23].Value = modeljbxx.Isrecord ?? "";
                parametersjbxx[24].Value = modeljbxx.Suspectname ?? "";
                parametersjbxx[25].Value = modeljbxx.WSBH ?? "";
                parametersjbxx[26].Value = modeljbxx.AJBH ?? "";
                parametersjbxx[27].Value = modeljbxx.ZZZT ?? "";
                parametersjbxx[28].Value = modeljbxx.JZPZ ?? "";
                parametersjbxx[29].Value = modeljbxx.JZSHRBH ?? "";
                parametersjbxx[30].Value = modeljbxx.JZSHR ?? "";
                parametersjbxx[31].Value = (object)modeljbxx.JZSHSJ ?? DBNull.Value;
                parametersjbxx[32].Value = modeljbxx.WSMC ?? "";
                parametersjbxx[33].Value = modeljbxx.SMAJLA ?? "";
                parametersjbxx[34].Value = modeljbxx.SMAJCD ?? "";
                parametersjbxx[35].Value = modeljbxx.LAZZZT ?? "";

                hash.Add(strSql.ToString(), parametersjbxx);
                #endregion
            }
            try
            {
                return DbHelperOra.ExecuteSqlTran(hash);            
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool AddList(EDRS.Model.TYYW_GG_AJJBXX model,EDRS.Model.TYYW_GG_AJJBXXKZ modelkz,EDRS.Model.YX_DZJZ_JZJBXX modeljbxx)", "EDRS.OracleDAL.TYYW_GG_AJJBXX");
            }
            return false;
        }


        /// <summary>
        /// 修改一条数据
        /// </summary>
        public bool UpdateList(EDRS.Model.TYYW_GG_AJJBXX model, EDRS.Model.TYYW_GG_AJJBXXKZ modelkz, EDRS.Model.YX_DZJZ_JZJBXX modeljbxx)
        {
            System.Collections.Hashtable hash = new System.Collections.Hashtable();
            StringBuilder strSql = new StringBuilder();
            if (model != null)
            {
                #region 案件基本信息

                strSql.Append("update TYYW_GG_AJJBXX set ");
                strSql.Append("ZHXGSJ=:ZHXGSJ,SLRQ=:SLRQ,AJMC=:AJMC,AJLB_BM=:AJLB_BM,AJLB_MC=:AJLB_MC,CBRGH=:CBRGH,CBR=:CBR,BJRQ=:BJRQ where BMSAH=:BMSAH ");
                OracleParameter[] parameters = {
					new OracleParameter(":BMSAH", OracleType.VarChar,100),				
					new OracleParameter(":ZHXGSJ", OracleType.DateTime),
					new OracleParameter(":SLRQ", OracleType.DateTime),
					new OracleParameter(":AJMC", OracleType.VarChar,300),
					new OracleParameter(":AJLB_BM", OracleType.VarChar,50),
					new OracleParameter(":AJLB_MC", OracleType.VarChar,300),					
					new OracleParameter(":CBRGH", OracleType.Char,4),
					new OracleParameter(":CBR", OracleType.VarChar,60),			
					new OracleParameter(":BJRQ", OracleType.DateTime)};
                parameters[0].Value = model.BMSAH ?? "";             
                parameters[1].Value = model.ZHXGSJ;
                parameters[2].Value = model.SLRQ;
                parameters[3].Value = model.AJMC ?? "";
                parameters[4].Value = model.AJLB_BM ?? "";
                parameters[5].Value = model.AJLB_MC ?? "";
                parameters[6].Value = model.CBRGH ?? "";
                parameters[7].Value = model.CBR ?? "";
                parameters[8].Value = (object)model.BJRQ ?? DBNull.Value;
                hash.Add(strSql.ToString(), parameters);
                #endregion
            }
            if (modelkz != null)
            {
                #region 案件基本信息扩展
                strSql.Clear();
                strSql.Append("update TYYW_GG_AJJBXXKZ set ");
                strSql.Append("AY=:AY,SAJG=:SAJG,YG=:YG,BG=:BG,YYR=:YYR,SQZXR=:SQZXR,BZXR=:BZXR,SARQ=:SARQ,JARQ=:JARQ,CJJG=:CJJG,ZXBD=:ZXBD,SLJG=:SLJG,ZXJG=:ZXJG,JAFS=:JAFS,GLDAXLH=:GLDAXLH,HYTCY=:HYTCY,CBR=:CBR,SJY=:SJY,ZCS=:ZCS,DJC=:DJC,YS=:YS,GDRQ=:GDRQ,YWT=:YWT,BGQX=:BGQX,AJLBBM=:AJLBBM,H=:H,NF=:NF,YWLBBM=:YWLBBM,YWLBMC=:YWLBMC,DWBM=:DWBM,DWMC=:DWMC,DWJC=:DWJC,AJLBMC=:AJLBMC,QAH=:QAH,MLH=:MLH,JH=:JH where AHMC=:AHMC");               
                OracleParameter[] parameterskz = {
					new OracleParameter(":AHMC", OracleType.VarChar,200),
					new OracleParameter(":AY", OracleType.VarChar,500),
					new OracleParameter(":SAJG", OracleType.VarChar,200),
					new OracleParameter(":YG", OracleType.VarChar,200),
					new OracleParameter(":BG", OracleType.VarChar,200),
					new OracleParameter(":YYR", OracleType.VarChar,200),
					new OracleParameter(":SQZXR", OracleType.VarChar,50),
					new OracleParameter(":BZXR", OracleType.VarChar,50),
					new OracleParameter(":SARQ", OracleType.DateTime),
					new OracleParameter(":JARQ", OracleType.DateTime),
					new OracleParameter(":CJJG", OracleType.VarChar,200),
					new OracleParameter(":ZXBD", OracleType.VarChar,200),
					new OracleParameter(":SLJG", OracleType.VarChar,4000),
					new OracleParameter(":ZXJG", OracleType.VarChar,4000),
					new OracleParameter(":JAFS", OracleType.VarChar,200),
					new OracleParameter(":GLDAXLH", OracleType.VarChar,200),
					new OracleParameter(":HYTCY", OracleType.VarChar,200),
					new OracleParameter(":CBR", OracleType.VarChar,50),
					new OracleParameter(":SJY", OracleType.VarChar,50),
					new OracleParameter(":ZCS", OracleType.Number),
					new OracleParameter(":DJC", OracleType.Number),
					new OracleParameter(":YS", OracleType.Number),
					new OracleParameter(":GDRQ", OracleType.DateTime),
					new OracleParameter(":YWT", OracleType.VarChar,200),
					new OracleParameter(":BGQX", OracleType.VarChar,20),
					new OracleParameter(":AJLBBM", OracleType.VarChar,20),
					new OracleParameter(":H", OracleType.Number),
					new OracleParameter(":NF", OracleType.VarChar,4),
					new OracleParameter(":YWLBBM", OracleType.VarChar,20),
					new OracleParameter(":YWLBMC", OracleType.VarChar,50),
					new OracleParameter(":DWBM", OracleType.VarChar,50),
					new OracleParameter(":DWMC", OracleType.VarChar,500),
					new OracleParameter(":DWJC", OracleType.VarChar,200),			
					new OracleParameter(":AJLBMC", OracleType.VarChar,200),
					new OracleParameter(":QAH", OracleType.VarChar,20),
					new OracleParameter(":MLH", OracleType.VarChar,20),
					new OracleParameter(":JH", OracleType.VarChar,20)};
                parameterskz[0].Value = modelkz.AHMC ?? "";
                parameterskz[1].Value = modelkz.AY ?? "";
                parameterskz[2].Value = modelkz.SAJG ?? "";
                parameterskz[3].Value = modelkz.YG ?? "";
                parameterskz[4].Value = modelkz.BG ?? "";
                parameterskz[5].Value = modelkz.YYR ?? "";
                parameterskz[6].Value = modelkz.SQZXR ?? "";
                parameterskz[7].Value = modelkz.BZXR ?? "";
                parameterskz[8].Value = modelkz.SARQ;
                parameterskz[9].Value = modelkz.JARQ;
                parameterskz[10].Value = modelkz.CJJG ?? "";
                parameterskz[11].Value = modelkz.ZXBD ?? "";
                parameterskz[12].Value = modelkz.SLJG ?? "";
                parameterskz[13].Value = modelkz.ZXJG ?? "";
                parameterskz[14].Value = modelkz.JAFS ?? "";
                parameterskz[15].Value = modelkz.GLDAXLH ?? "";
                parameterskz[16].Value = modelkz.HYTCY ?? "";
                parameterskz[17].Value = modelkz.CBR ?? "";
                parameterskz[18].Value = modelkz.SJY ?? "";
                parameterskz[19].Value = modelkz.ZCS;
                parameterskz[20].Value = modelkz.DJC;
                parameterskz[21].Value = modelkz.YS;
                parameterskz[22].Value = modelkz.GDRQ;
                parameterskz[23].Value = modelkz.YWT ?? "";
                parameterskz[24].Value = modelkz.BGQX ?? "";
                parameterskz[25].Value = modelkz.AJLBBM ?? "";
                parameterskz[26].Value = modelkz.H;
                parameterskz[27].Value = modelkz.NF;
                parameterskz[28].Value = modelkz.YWLBBM ?? "";
                parameterskz[29].Value = modelkz.YWLBMC ?? "";
                parameterskz[30].Value = modelkz.DWBM ?? "";
                parameterskz[31].Value = modelkz.DWMC ?? "";
                parameterskz[32].Value = modelkz.DWJC ?? "";
                parameterskz[33].Value = modelkz.AJLBMC ?? "";
                parameterskz[34].Value = modelkz.QAH ?? "";
                parameterskz[35].Value = modelkz.MLH ?? "";
                parameterskz[36].Value = modelkz.JH ?? "";

                hash.Add(strSql.ToString(), parameterskz);
                #endregion
            }
            if (modeljbxx != null)
            {
                #region 卷宗基本信息
                strSql.Clear();
                strSql.Append("update YX_DZJZ_JZJBXX set ");

                strSql.AppendFormat("ZHXGSJ=:ZHXGSJ,JZMC=:JZMC,AJMB_BM= :AJMB_BM, AJMB_MC=:AJMB_MC {0}{1} where BMSAH=:BMSAH", (string.IsNullOrEmpty(modeljbxx.SMAJLA) ? "" : ",SMAJLA=:SMAJLA"), (string.IsNullOrEmpty(modeljbxx.SMAJCD) ? "" : ",SMAJCD=:SMAJCD"));

                System.Collections.Generic.List<OracleParameter> parametersjbxx = new System.Collections.Generic.List<OracleParameter>();
                parametersjbxx.Add(new OracleParameter(":ZHXGSJ", modeljbxx.ZHXGSJ));
                parametersjbxx.Add(new OracleParameter(":AJMB_BM", modeljbxx.Ajmb_bm ?? ""));
                parametersjbxx.Add(new OracleParameter(":AJMB_MC", modeljbxx.Ajmb_mc ?? ""));
                parametersjbxx.Add(new OracleParameter(":BMSAH", modeljbxx.BMSAH ?? ""));
                parametersjbxx.Add(new OracleParameter(":JZMC", modeljbxx.JZMC ?? ""));
                if (!string.IsNullOrEmpty(modeljbxx.SMAJLA))
                    parametersjbxx.Add(new OracleParameter(":SMAJLA", modeljbxx.SMAJLA ?? ""));
                if (!string.IsNullOrEmpty(modeljbxx.SMAJCD))
                    parametersjbxx.Add(new OracleParameter(":SMAJCD", modeljbxx.SMAJCD ?? ""));
                

                //OracleParameter[] parametersjbxx = {					
                //    new OracleParameter(":ZHXGSJ", OracleType.DateTime),			
                //    new OracleParameter(":AJMB_BM", OracleType.VarChar,500),
                //    new OracleParameter(":AJMB_MC", OracleType.VarChar,500),
                //    new OracleParameter(":BMSAH", OracleType.VarChar,100),
                //    new OracleParameter(":JZMC", OracleType.VarChar,300),       
                //    new OracleParameter(":SMAJLA", OracleType.VarChar),
                //    new OracleParameter(":SMAJCD", OracleType.VarChar),
                //                                   };
               
                //parametersjbxx[0].Value = modeljbxx.ZHXGSJ;
                //parametersjbxx[1].Value = modeljbxx.Ajmb_bm ?? "";
                //parametersjbxx[2].Value = modeljbxx.Ajmb_mc ?? "";
                //parametersjbxx[3].Value = modeljbxx.BMSAH ?? "";
                //parametersjbxx[4].Value = modeljbxx.JZMC ?? "";
                //parametersjbxx[5].Value = modeljbxx.SMAJLA ?? "";
                //parametersjbxx[6].Value = modeljbxx.SMAJCD ?? "";          
          
                hash.Add(strSql.ToString(), parametersjbxx.ToArray());
                #endregion
            }
            try
            {
                return DbHelperOra.ExecuteSqlTran(hash);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool AddList(EDRS.Model.TYYW_GG_AJJBXX model,EDRS.Model.TYYW_GG_AJJBXXKZ modelkz,EDRS.Model.YX_DZJZ_JZJBXX modeljbxx)", "EDRS.OracleDAL.TYYW_GG_AJJBXX");
            }
            return false;
        }
    }
}

