using System;
using System.Data;
using System.Text;
using System.Data.OracleClient;
using EDRS.IDAL;
using Maticsoft.DBUtility;
using EDRS.Common;
using System.Collections.Generic;//Please add references
namespace EDRS.OracleDAL
{
    /// <summary>
    /// 数据访问类:YX_DZJZ_WJSQDY
    /// </summary>
    public partial class YX_DZJZ_WJSQDY
    {

      
        /// <summary>
        /// 增加多条数据
        /// </summary>
        public bool AddList(System.Collections.Generic.List<EDRS.Model.YX_DZJZ_WJSQDY> models)
        {
            System.Collections.Hashtable hash = new System.Collections.Hashtable();
            int count = 0;
            foreach (EDRS.Model.YX_DZJZ_WJSQDY model in models)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into YX_DZJZ_WJSQDY(");
                strSql.Append("LSZH,BMSAH,YJXH,JZWJBH,SQSJ,SQFS,DYSJ,DYFS,DYFY,DYR,DYRGH,DYBMBM,DYBMMC,DYDWBM,DYDWMC,SFSC,DYSQDH)");
                strSql.Append(" values (");
                strSql.Append(":LSZH" + count + ",:BMSAH" + count + ",:YJXH" + count + ",:JZWJBH" + count + ",:SQSJ" + count + ",:SQFS" + count + ",:DYSJ" + count + ",:DYFS" + count + ",:DYFY" + count + ",:DYR" + count + ",:DYRGH" + count + ",:DYBMBM" + count + ",:DYBMMC" + count + ",:DYDWBM" + count + ",:DYDWMC" + count + ",:SFSC" + count + ",:DYSQDH" + count + ")");
                OracleParameter[] parameters = {
					new OracleParameter(":LSZH"+count, OracleType.VarChar,100),
					new OracleParameter(":BMSAH"+count, OracleType.VarChar,100),
					new OracleParameter(":YJXH"+count, OracleType.VarChar,50),
					new OracleParameter(":JZWJBH"+count, OracleType.VarChar,50),
					new OracleParameter(":SQSJ"+count, OracleType.DateTime),
					new OracleParameter(":SQFS"+count, OracleType.Number,4),
					new OracleParameter(":DYSJ"+count, OracleType.DateTime),
					new OracleParameter(":DYFS"+count, OracleType.Number,4),
					new OracleParameter(":DYFY"+count, OracleType.Number,8),
					new OracleParameter(":DYR"+count, OracleType.VarChar,60),
					new OracleParameter(":DYRGH"+count, OracleType.Char,4),
					new OracleParameter(":DYBMBM"+count, OracleType.Char,4),
					new OracleParameter(":DYBMMC"+count, OracleType.VarChar,300),
					new OracleParameter(":DYDWBM"+count, OracleType.VarChar,50),
					new OracleParameter(":DYDWMC"+count, OracleType.VarChar,300),
					new OracleParameter(":SFSC"+count, OracleType.Char,1),
					new OracleParameter(":DYSQDH"+count, OracleType.VarChar,50)};
                parameters[0].Value = model.LSZH;
                parameters[1].Value = model.BMSAH;
                parameters[2].Value = model.YJXH;
                parameters[3].Value = model.JZWJBH;
                parameters[4].Value = model.SQSJ ?? (object)DBNull.Value;
                parameters[5].Value = model.SQFS;
                parameters[6].Value = model.DYSJ ?? (object)DBNull.Value;
                parameters[7].Value = model.DYFS ?? (object)DBNull.Value;
                parameters[8].Value = model.DYFY ?? (object)DBNull.Value;
                parameters[9].Value = model.DYR;
                parameters[10].Value = model.DYRGH;
                parameters[11].Value = model.DYBMBM;
                parameters[12].Value = model.DYBMMC;
                parameters[13].Value = model.DYDWBM;
                parameters[14].Value = model.DYDWMC;
                parameters[15].Value = model.SFSC;
                parameters[16].Value = model.DYSQDH;
                count++;

                hash.Add(strSql.ToString(), parameters);
            };
            if (DbHelperOra.ExecuteSqlTran(hash))
                return true;
            return false;
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPageEx(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)
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
                strSql.Append("order by T.XH desc");
            }
            strSql.AppendFormat(")AS Ro, T.*  from {0} T ", @"(select distinct a.lszh,a.bmsah,a.yjxh,a.sqsj,a.sqfs,a.sfsc,a.dysqdh,a.xh,aj.ajmc,
jz.ajbh,jz.wsbh,jz.wsmc,l.LSXM,l.LSDW,l.LSDWDZ,l.LSLXDH,l.DELXR from yx_dzjz_wjsqdy a 
left join tyyw_gg_ajjbxx aj on a.bmsah = aj.bmsah
left join yx_dzjz_jzjbxx jz on a.bmsah = jz.bmsah
left join YX_DZJZ_LSGL l on a.lszh = l.lszh)");
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
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)", "EDRS.OracleDAL.YX_DZJZ_WJSQDY", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
        }
        /// <summary>
        /// 判断申请是否审核
        /// </summary>
        /// <param name="yjxh"></param>
        /// <returns></returns>
        public DataSet GetListIsSH(string yjxh)
        {
            string sql = "select * from (select distinct yjxh,dysqdh from YX_DZJZ_WJSQDY ) sq left join YX_DZJZ_LSYJSQ sh on sq.dysqdh = sh.yjsqdm where sq.yjxh=:yjxh";
            OracleParameter[] parameters = {
                new OracleParameter(":YJXH", OracleType.VarChar,50)};
            parameters[0].Value = yjxh;
            return DbHelperOra.Query(sql, parameters);
        }


        /// <summary>
        /// 增加多条文件申请打印记录
        /// </summary>
        public bool AddListJL(List<EDRS.Model.YX_DZJZ_WJSQDYJL> sqdyjlList, EDRS.Model.YX_DZJZ_WJSQDY sqdyModel, string xh)
        {
            int count = 0;
           
            Dictionary<object, object> hash = new Dictionary<object, object>();

            StringBuilder strSql = new StringBuilder();
           // strSql.Append(" update YX_DZJZ_WJSQDYJL set SFSC='Y' where XH=:YJXH ");
            strSql.Append(" Delete YX_DZJZ_WJSQDY where XH=:YJXH ");
            OracleParameter[] parameters1 = {
                    new OracleParameter(":YJXH", OracleType.VarChar,50)
                };
            parameters1[0].Value = xh;
            hash.Add(strSql.ToString(), parameters1);


            #region 添加阅卷申请打印
            strSql.Clear();
            strSql.Append("insert into YX_DZJZ_WJSQDY(");
            strSql.Append("LSZH,BMSAH,YJXH,JZWJBH,SQSJ,SQFS,DYSJ,DYFS,DYFY,DYR,DYRGH,DYBMBM,DYBMMC,DYDWBM,DYDWMC,SFSC,DYSQDH,XH)");
            strSql.Append(" values (");
            strSql.Append(":LSZH,:BMSAH,:YJXH,:JZWJBH,:SQSJ,:SQFS,:DYSJ,:DYFS,:DYFY,:DYR,:DYRGH,:DYBMBM,:DYBMMC,:DYDWBM,:DYDWMC,:SFSC,:DYSQDH,:XH)");
            OracleParameter[] parameters = {
                    new OracleParameter(":LSZH", OracleType.VarChar,100),
                    new OracleParameter(":BMSAH", OracleType.VarChar,100),
                    new OracleParameter(":YJXH", OracleType.VarChar,50),
                    new OracleParameter(":JZWJBH", OracleType.VarChar,50),
                    new OracleParameter(":SQSJ", OracleType.DateTime),
                    new OracleParameter(":SQFS", OracleType.Number,4),
                    new OracleParameter(":DYSJ", OracleType.DateTime),
                    new OracleParameter(":DYFS", OracleType.Number,4),
                    new OracleParameter(":DYFY", OracleType.Number,8),
                    new OracleParameter(":DYR", OracleType.VarChar,60),
                    new OracleParameter(":DYRGH", OracleType.Char,4),
                    new OracleParameter(":DYBMBM", OracleType.Char,4),
                    new OracleParameter(":DYBMMC", OracleType.VarChar,300),
                    new OracleParameter(":DYDWBM", OracleType.VarChar,50),
                    new OracleParameter(":DYDWMC", OracleType.VarChar,300),
                    new OracleParameter(":SFSC", OracleType.Char,1),
                    new OracleParameter(":DYSQDH", OracleType.VarChar,50),
                    new OracleParameter(":XH", OracleType.VarChar,50)};
            parameters[0].Value = sqdyModel.LSZH ?? "";
            parameters[1].Value = sqdyModel.BMSAH ?? "";
            parameters[2].Value = sqdyModel.YJXH ?? "";
            parameters[3].Value = sqdyModel.JZWJBH ?? "";
            parameters[4].Value = sqdyModel.SQSJ ?? (object)DBNull.Value;
            parameters[5].Value = sqdyModel.SQFS ?? (object)DBNull.Value;
            parameters[6].Value = sqdyModel.DYSJ ?? (object)DBNull.Value;
            parameters[7].Value = sqdyModel.DYFS ?? (object)DBNull.Value;
            parameters[8].Value = sqdyModel.DYFY ?? (object)DBNull.Value;
            parameters[9].Value = sqdyModel.DYR ?? "";
            parameters[10].Value = sqdyModel.DYRGH ?? "";
            parameters[11].Value = sqdyModel.DYBMBM ?? "";
            parameters[12].Value = sqdyModel.DYBMMC ?? "";
            parameters[13].Value = sqdyModel.DYDWBM ?? "";
            parameters[14].Value = sqdyModel.DYDWMC ?? "";
            parameters[15].Value = sqdyModel.SFSC ?? "";
            parameters[16].Value = sqdyModel.DYSQDH ?? "";
            parameters[17].Value = sqdyModel.XH ?? "";

            hash.Add(strSql.ToString(), parameters);
            #endregion

            #region 阅卷申请记录
            foreach (EDRS.Model.YX_DZJZ_WJSQDYJL model in sqdyjlList)
            {
                strSql.Clear();
                strSql.Append(" insert into YX_DZJZ_WJSQDYJL(");
                strSql.Append("XH,YJXH,WJXH,SFSC,ADDTIME,WJLJ)");
                strSql.Append(" values (");
                strSql.Append(":XH" + count + ",:YJXH" + count + ",:WJXH" + count + ",:SFSC" + count + ",:ADDTIME" + count + ",:WJLJ" + count + ")");
                OracleParameter[] parameters2 = {
                    new OracleParameter(":XH"+count, OracleType.VarChar,50),
                    new OracleParameter(":YJXH"+count, OracleType.VarChar,50),
                    new OracleParameter(":WJXH"+count, OracleType.VarChar,100),
                    new OracleParameter(":SFSC"+count, OracleType.Char,1),
                    new OracleParameter(":ADDTIME"+count, OracleType.DateTime),
                    new OracleParameter(":WJLJ"+count, OracleType.VarChar,1000)
                };
                parameters2[0].Value = xh;
                parameters2[1].Value = model.YJXH;
                parameters2[2].Value = model.WJXH;
                parameters2[3].Value = "N";
                parameters2[4].Value = model.ADDTIME;
                parameters2[5].Value = model.WJLJ;

                hash.Add(strSql.ToString(), parameters2);
                count++;
            }
            #endregion
            try
            {
                return DbHelperOra.ExecuteSqlTran(hash);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool AddListJL(List<EDRS.Model.YX_DZJZ_WJSQDYJL> sqdyjlList, EDRS.Model.YX_DZJZ_WJSQDY sqdyModel, string xh)", "EDRS.OracleDAL.YX_DZJZ_WJSQDYJL");
            }
            return false;
        }

        #region 修改文件打印申请记录
        /// <summary>
        /// 修改文件打印申请记录
        /// </summary>
        /// <param name="sqdyjlList"></param>
        /// <param name="sqdyModel"></param>
        /// <param name="xh"></param>
        /// <returns></returns>
        public bool UpdateListJl(List<EDRS.Model.YX_DZJZ_WJSQDYJL> sqdyjlList)
        {
            int count = 0;

            Dictionary<object, object> hash = new Dictionary<object, object>();
            StringBuilder strSql = new StringBuilder();
            #region 阅卷申请记录
            foreach (EDRS.Model.YX_DZJZ_WJSQDYJL model in sqdyjlList)
            {
                strSql.Clear();
                strSql.AppendFormat("update YX_DZJZ_WJSQDYJL set SFXYDY =:SFXYDY{0} where WJXH=:WJXH{0} and XH=:XH{0} ", count);

                OracleParameter[] parameters2 = {
                    new OracleParameter(":SFXYDY"+count, OracleType.Char,1),
                    new OracleParameter(":WJXH"+count, OracleType.VarChar,100),
                    new OracleParameter(":XH"+count, OracleType.VarChar,100)
                };
                parameters2[0].Value = model.SFXYDY;
                parameters2[1].Value = model.WJXH;
                parameters2[2].Value = model.XH;

                hash.Add(strSql.ToString(), parameters2);
                count++;
            }
            #endregion
            try
            {
                return DbHelperOra.ExecuteSqlTran(hash);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool  UpdateListJl(List<EDRS.Model.YX_DZJZ_WJSQDYJL> sqdyjlList)", "EDRS.OracleDAL.YX_DZJZ_WJSQDYJL");
            }
            return false;
        } 
        #endregion
        /// <summary>
        /// 根据阅卷序号得到一个对象实体
        /// </summary>
        public EDRS.Model.YX_DZJZ_WJSQDY GetModelByYJXH(string yjxh)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select LSZH,BMSAH,YJXH,JZWJBH,SQSJ,SQFS,DYSJ,DYFS,DYFY,DYR,DYRGH,DYBMBM,DYBMMC,DYDWBM,DYDWMC,SFSC,DYSQDH,XH from YX_DZJZ_WJSQDY ");
            strSql.Append(ConfigHelper.GetConfigString("OrcDBLinq"));
            strSql.Append(" where YJXH=:YJXH ");
            OracleParameter[] parameters = {
					new OracleParameter(":YJXH", OracleType.VarChar,50)			};
            parameters[0].Value = yjxh;

            EDRS.Model.YX_DZJZ_WJSQDY model = new EDRS.Model.YX_DZJZ_WJSQDY();
            DataSet ds = null;
            try
            {
                ds = DbHelperOra.Query(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "ppublic EDRS.Model.YX_DZJZ_WJSQDY GetModel(string yjxh)", "EDRS.OracleDAL.YX_DZJZ_WJSQDY", strSql.ToString(), parameters);
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
        /// 根据序号获得打印记录列表
        /// </summary>
        public DataSet GetListDYJL(string strWhere, params object[] objValues)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from YX_DZJZ_WJSQDYJL ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where 1=1 " + strWhere);
            }
            try
            {
                return DbHelperOra.Query(strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetListDYJL(string strWhere, params object[] objValues)", "EDRS.OracleDAL.YX_DZJZ_WJSQDY", strSql.ToString(), ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
        }




    }
}
