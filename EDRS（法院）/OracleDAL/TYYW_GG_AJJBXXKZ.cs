using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data.OracleClient;
using EDRS.Common;
using Maticsoft.DBUtility;
using EDRS.IDAL;
using System.Data;
namespace EDRS.OracleDAL
{
    /// <summary>
    /// 数据访问类
    /// </summary>
    public partial class TYYW_GG_AJJBXXKZ : ITYYW_GG_AJJBXXKZ
    {
         public HttpRequest context = null;//客户端对象，用于记录日志，客户端信息
        public void SetHttpContext(System.Web.HttpRequest _context)
        {
            this.context = _context;
        }
        public TYYW_GG_AJJBXXKZ() { }

        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string AJKZXH)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from TYYW_GG_AJJBXXKZ");
            strSql.Append(" where AJKZXH=:AJKZXH ");
            OracleParameter[] parameters = {
					new OracleParameter(":AJKZXH", OracleType.VarChar,50)			};
            parameters[0].Value = AJKZXH;

            return DbHelperOra.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(EDRS.Model.TYYW_GG_AJJBXXKZ model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TYYW_GG_AJJBXXKZ(");
            strSql.Append("AJKZXH,DWBM,DWMC,DWJC,AHMC,AY,SAJG,YG,BG,YYR,SQZXR,BZXR,SARQ,JARQ,CJJG,ZXBD,SLJG,ZXJG,JAFS,GLDAXLH,HYTCY,CBR,SJY,ZCS,DJC,YS,GDRQ,YWT,BGQX,AJLBBM,AJLBMC,H,NF,YWLBBM,YWLBMC,QAH,MLH,JH)");
            strSql.Append(" values (");
            strSql.Append(":AJKZXH,:DWBM,:DWMC,:DWJC,:AHMC,:AY,:SAJG,:YG,:BG,:YYR,:SQZXR,:BZXR,:SARQ,:JARQ,:CJJG,:ZXBD,:SLJG,:ZXJG,:JAFS,:GLDAXLH,:HYTCY,:CBR,:SJY,:ZCS,:DJC,:YS,:GDRQ,:YWT,:BGQX,:AJLBBM,:AJLBMC,:H,:NF,:YWLBBM,:YWLBMC,:QAH,:MLH,:JH)");
            OracleParameter[] parameters = {
					new OracleParameter(":AJKZXH", OracleType.VarChar,50),
					new OracleParameter(":DWBM", OracleType.VarChar,50),
					new OracleParameter(":DWMC", OracleType.VarChar,500),
					new OracleParameter(":DWJC", OracleType.VarChar,200),
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
					new OracleParameter(":ZCS", OracleType.Number,18),
					new OracleParameter(":DJC", OracleType.Number,18),
					new OracleParameter(":YS", OracleType.Number,18),
					new OracleParameter(":GDRQ", OracleType.DateTime),
					new OracleParameter(":YWT", OracleType.VarChar,200),
					new OracleParameter(":BGQX", OracleType.VarChar,20),
					new OracleParameter(":AJLBBM", OracleType.VarChar,50),
					new OracleParameter(":AJLBMC", OracleType.VarChar,200),
					new OracleParameter(":H", OracleType.Number,18),
					new OracleParameter(":NF", OracleType.VarChar,10),
					new OracleParameter(":YWLBBM", OracleType.VarChar,50),
					new OracleParameter(":YWLBMC", OracleType.VarChar,50),
					new OracleParameter(":QAH", OracleType.VarChar,20),
					new OracleParameter(":MLH", OracleType.VarChar,20),
					new OracleParameter(":JH", OracleType.VarChar,20)};
            parameters[0].Value = model.AJKZXH;
            parameters[1].Value = model.DWBM;
            parameters[2].Value = model.DWMC;
            parameters[3].Value = model.DWJC;
            parameters[4].Value = model.AHMC;
            parameters[5].Value = model.AY;
            parameters[6].Value = model.SAJG;
            parameters[7].Value = model.YG;
            parameters[8].Value = model.BG;
            parameters[9].Value = model.YYR;
            parameters[10].Value = model.SQZXR;
            parameters[11].Value = model.BZXR;
            parameters[12].Value = model.SARQ;
            parameters[13].Value = model.JARQ;
            parameters[14].Value = model.CJJG;
            parameters[15].Value = model.ZXBD;
            parameters[16].Value = model.SLJG;
            parameters[17].Value = model.ZXJG;
            parameters[18].Value = model.JAFS;
            parameters[19].Value = model.GLDAXLH;
            parameters[20].Value = model.HYTCY;
            parameters[21].Value = model.CBR;
            parameters[22].Value = model.SJY;
            parameters[23].Value = model.ZCS;
            parameters[24].Value = model.DJC;
            parameters[25].Value = model.YS;
            parameters[26].Value = model.GDRQ;
            parameters[27].Value = model.YWT;
            parameters[28].Value = model.BGQX;
            parameters[29].Value = model.AJLBBM;
            parameters[30].Value = model.AJLBMC;
            parameters[31].Value = model.H;
            parameters[32].Value = model.NF;
            parameters[33].Value = model.YWLBBM;
            parameters[34].Value = model.YWLBMC;
            parameters[35].Value = model.QAH;
            parameters[36].Value = model.MLH;
            parameters[37].Value = model.JH;

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
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(EDRS.Model.TYYW_GG_AJJBXXKZ model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TYYW_GG_AJJBXXKZ set ");
            strSql.Append("DWBM=:DWBM,");
            strSql.Append("DWMC=:DWMC,");
            strSql.Append("DWJC=:DWJC,");
            strSql.Append("AHMC=:AHMC,");
            strSql.Append("AY=:AY,");
            strSql.Append("SAJG=:SAJG,");
            strSql.Append("YG=:YG,");
            strSql.Append("BG=:BG,");
            strSql.Append("YYR=:YYR,");
            strSql.Append("SQZXR=:SQZXR,");
            strSql.Append("BZXR=:BZXR,");
            strSql.Append("SARQ=:SARQ,");
            strSql.Append("JARQ=:JARQ,");
            strSql.Append("CJJG=:CJJG,");
            strSql.Append("ZXBD=:ZXBD,");
            strSql.Append("SLJG=:SLJG,");
            strSql.Append("ZXJG=:ZXJG,");
            strSql.Append("JAFS=:JAFS,");
            strSql.Append("GLDAXLH=:GLDAXLH,");
            strSql.Append("HYTCY=:HYTCY,");
            strSql.Append("CBR=:CBR,");
            strSql.Append("SJY=:SJY,");
            strSql.Append("ZCS=:ZCS,");
            strSql.Append("DJC=:DJC,");
            strSql.Append("YS=:YS,");
            strSql.Append("GDRQ=:GDRQ,");
            strSql.Append("YWT=:YWT,");
            strSql.Append("BGQX=:BGQX,");
            strSql.Append("AJLBBM=:AJLBBM,");
            strSql.Append("AJLBMC=:AJLBMC,");
            strSql.Append("H=:H,");
            strSql.Append("NF=:NF,");
            strSql.Append("YWLBBM=:YWLBBM,");
            strSql.Append("YWLBMC=:YWLBMC,");
            strSql.Append("QAH=:QAH,");
            strSql.Append("MLH=:MLH,");
            strSql.Append("JH=:JH");
            strSql.Append(" where AJKZXH=:AJKZXH ");
            OracleParameter[] parameters = {
					new OracleParameter(":DWBM", OracleType.VarChar,50),
					new OracleParameter(":DWMC", OracleType.VarChar,500),
					new OracleParameter(":DWJC", OracleType.VarChar,200),
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
					new OracleParameter(":ZCS", OracleType.Number,18),
					new OracleParameter(":DJC", OracleType.Number,18),
					new OracleParameter(":YS", OracleType.Number,18),
					new OracleParameter(":GDRQ", OracleType.DateTime),
					new OracleParameter(":YWT", OracleType.VarChar,200),
					new OracleParameter(":BGQX", OracleType.VarChar,20),
					new OracleParameter(":AJLBBM", OracleType.VarChar,50),
					new OracleParameter(":AJLBMC", OracleType.VarChar,200),
					new OracleParameter(":H", OracleType.Number,18),
					new OracleParameter(":NF", OracleType.VarChar,10),
					new OracleParameter(":YWLBBM", OracleType.VarChar,50),
					new OracleParameter(":YWLBMC", OracleType.VarChar,50),
					new OracleParameter(":QAH", OracleType.VarChar,20),
					new OracleParameter(":MLH", OracleType.VarChar,20),
					new OracleParameter(":JH", OracleType.VarChar,20),
					new OracleParameter(":AJKZXH", OracleType.VarChar,50)};
            parameters[0].Value = model.DWBM;
            parameters[1].Value = model.DWMC;
            parameters[2].Value = model.DWJC;
            parameters[3].Value = model.AHMC;
            parameters[4].Value = model.AY;
            parameters[5].Value = model.SAJG;
            parameters[6].Value = model.YG;
            parameters[7].Value = model.BG;
            parameters[8].Value = model.YYR;
            parameters[9].Value = model.SQZXR;
            parameters[10].Value = model.BZXR;
            parameters[11].Value = model.SARQ;
            parameters[12].Value = model.JARQ;
            parameters[13].Value = model.CJJG;
            parameters[14].Value = model.ZXBD;
            parameters[15].Value = model.SLJG;
            parameters[16].Value = model.ZXJG;
            parameters[17].Value = model.JAFS;
            parameters[18].Value = model.GLDAXLH;
            parameters[19].Value = model.HYTCY;
            parameters[20].Value = model.CBR;
            parameters[21].Value = model.SJY;
            parameters[22].Value = model.ZCS;
            parameters[23].Value = model.DJC;
            parameters[24].Value = model.YS;
            parameters[25].Value = model.GDRQ;
            parameters[26].Value = model.YWT;
            parameters[27].Value = model.BGQX;
            parameters[28].Value = model.AJLBBM;
            parameters[29].Value = model.AJLBMC;
            parameters[30].Value = model.H;
            parameters[31].Value = model.NF;
            parameters[32].Value = model.YWLBBM;
            parameters[33].Value = model.YWLBMC;
            parameters[34].Value = model.QAH;
            parameters[35].Value = model.MLH;
            parameters[36].Value = model.JH;
            parameters[37].Value = model.AJKZXH;

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

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string AJKZXH)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TYYW_GG_AJJBXXKZ ");
            strSql.Append(" where AJKZXH=:AJKZXH ");
            OracleParameter[] parameters = {
					new OracleParameter(":AJKZXH", OracleType.VarChar,50)			};
            parameters[0].Value = AJKZXH;

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
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string AJKZXHlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TYYW_GG_AJJBXXKZ ");
            strSql.Append(" where AJKZXH in (" + AJKZXHlist + ")  ");
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


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public EDRS.Model.TYYW_GG_AJJBXXKZ GetModel(string AJKZXH)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AJKZXH,DWBM,DWMC,DWJC,AHMC,AY,SAJG,YG,BG,YYR,SQZXR,BZXR,SARQ,JARQ,CJJG,ZXBD,SLJG,ZXJG,JAFS,GLDAXLH,HYTCY,CBR,SJY,ZCS,DJC,YS,GDRQ,YWT,BGQX,AJLBBM,AJLBMC,H,NF,YWLBBM,YWLBMC,QAH,MLH,JH from TYYW_GG_AJJBXXKZ ");
            strSql.Append(" where AHMC=:AHMC ");
            OracleParameter[] parameters = {
					new OracleParameter(":AHMC", OracleType.VarChar,200)			};
            parameters[0].Value = AJKZXH;

            EDRS.Model.TYYW_GG_AJJBXXKZ model = new EDRS.Model.TYYW_GG_AJJBXXKZ();
            DataSet ds = DbHelperOra.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
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
        public EDRS.Model.TYYW_GG_AJJBXXKZ DataRowToModel(DataRow row)
        {
            EDRS.Model.TYYW_GG_AJJBXXKZ model = new EDRS.Model.TYYW_GG_AJJBXXKZ();
            if (row != null)
            {
                if (row["AJKZXH"] != null)
                {
                    model.AJKZXH = row["AJKZXH"].ToString();
                }
                if (row["DWBM"] != null)
                {
                    model.DWBM = row["DWBM"].ToString();
                }
                if (row["DWMC"] != null)
                {
                    model.DWMC = row["DWMC"].ToString();
                }
                if (row["DWJC"] != null)
                {
                    model.DWJC = row["DWJC"].ToString();
                }
                if (row["AHMC"] != null)
                {
                    model.AHMC = row["AHMC"].ToString();
                }
                if (row["AY"] != null)
                {
                    model.AY = row["AY"].ToString();
                }
                if (row["SAJG"] != null)
                {
                    model.SAJG = row["SAJG"].ToString();
                }
                if (row["YG"] != null)
                {
                    model.YG = row["YG"].ToString();
                }
                if (row["BG"] != null)
                {
                    model.BG = row["BG"].ToString();
                }
                if (row["YYR"] != null)
                {
                    model.YYR = row["YYR"].ToString();
                }
                if (row["SQZXR"] != null)
                {
                    model.SQZXR = row["SQZXR"].ToString();
                }
                if (row["BZXR"] != null)
                {
                    model.BZXR = row["BZXR"].ToString();
                }
                if (row["SARQ"] != null && row["SARQ"].ToString() != "")
                {
                    model.SARQ = DateTime.Parse(row["SARQ"].ToString());
                }
                if (row["JARQ"] != null && row["JARQ"].ToString() != "")
                {
                    model.JARQ = DateTime.Parse(row["JARQ"].ToString());
                }
                if (row["CJJG"] != null)
                {
                    model.CJJG = row["CJJG"].ToString();
                }
                if (row["ZXBD"] != null)
                {
                    model.ZXBD = row["ZXBD"].ToString();
                }
                if (row["SLJG"] != null)
                {
                    model.SLJG = row["SLJG"].ToString();
                }
                if (row["ZXJG"] != null)
                {
                    model.ZXJG = row["ZXJG"].ToString();
                }
                if (row["JAFS"] != null)
                {
                    model.JAFS = row["JAFS"].ToString();
                }
                if (row["GLDAXLH"] != null)
                {
                    model.GLDAXLH = row["GLDAXLH"].ToString();
                }
                if (row["HYTCY"] != null)
                {
                    model.HYTCY = row["HYTCY"].ToString();
                }
                if (row["CBR"] != null)
                {
                    model.CBR = row["CBR"].ToString();
                }
                if (row["SJY"] != null)
                {
                    model.SJY = row["SJY"].ToString();
                }
                if (row["ZCS"] != null && row["ZCS"].ToString() != "")
                {
                    model.ZCS = decimal.Parse(row["ZCS"].ToString());
                }
                if (row["DJC"] != null && row["DJC"].ToString() != "")
                {
                    model.DJC = decimal.Parse(row["DJC"].ToString());
                }
                if (row["YS"] != null && row["YS"].ToString() != "")
                {
                    model.YS = decimal.Parse(row["YS"].ToString());
                }
                if (row["GDRQ"] != null && row["GDRQ"].ToString() != "")
                {
                    model.GDRQ = DateTime.Parse(row["GDRQ"].ToString());
                }
                if (row["YWT"] != null)
                {
                    model.YWT = row["YWT"].ToString();
                }
                if (row["BGQX"] != null)
                {
                    model.BGQX = row["BGQX"].ToString();
                }
                if (row["AJLBBM"] != null)
                {
                    model.AJLBBM = row["AJLBBM"].ToString();
                }
                if (row["AJLBMC"] != null)
                {
                    model.AJLBMC = row["AJLBMC"].ToString();
                }
                if (row["H"] != null && row["H"].ToString() != "")
                {
                    model.H = decimal.Parse(row["H"].ToString());
                }
                if (row["NF"] != null)
                {
                    model.NF = row["NF"].ToString();
                }
                if (row["YWLBBM"] != null)
                {
                    model.YWLBBM = row["YWLBBM"].ToString();
                }
                if (row["YWLBMC"] != null)
                {
                    model.YWLBMC = row["YWLBMC"].ToString();
                }
                if (row["QAH"] != null)
                {
                    model.QAH = row["QAH"].ToString();
                }
                if (row["MLH"] != null)
                {
                    model.MLH = row["MLH"].ToString();
                }
                if (row["JH"] != null)
                {
                    model.JH = row["JH"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AJKZXH,DWBM,DWMC,DWJC,AHMC,AY,SAJG,YG,BG,YYR,SQZXR,BZXR,SARQ,JARQ,CJJG,ZXBD,SLJG,ZXJG,JAFS,GLDAXLH,HYTCY,CBR,SJY,ZCS,DJC,YS,GDRQ,YWT,BGQX,AJLBBM,AJLBMC,H,NF,YWLBBM,YWLBMC,QAH,MLH,JH ");
            strSql.Append(" FROM TYYW_GG_AJJBXXKZ ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperOra.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM TYYW_GG_AJJBXXKZ ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
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
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
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
                strSql.Append("order by T.AJKZXH desc");
            }
            strSql.Append(")AS Ro, T.*  from TYYW_GG_AJJBXXKZ T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Ro between {0} and {1}", startIndex, endIndex);
            return DbHelperOra.Query(strSql.ToString());
        }
        #endregion  BasicMethod

    }
}
