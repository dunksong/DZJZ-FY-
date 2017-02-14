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
	/// 数据访问类:YX_DZJZ_LSAJBD
	/// </summary>
    public partial class YX_DZJZ_LSAJBD
    {
        /// <summary>
        /// 增加多条数据
        /// </summary>
        public bool AddList(EDRS.Model.YX_DZJZ_LSYJSQ lsyjsqModel, EDRS.Model.YX_DZJZ_LSAJBD lsajbdModel, List<EDRS.Model.YX_DZJZ_LSAJWJFP> modelList, EDRS.Model.YX_DZJZ_LSGL lsglModel)
        {
            int count = 0;
            Dictionary<object, object> hash = new Dictionary<object, object>();
           
            StringBuilder strSql = new StringBuilder();
           
            strSql.Append(" update YX_DZJZ_LSAJWJFP set SFSC='Y' where YJXH=:YJXHUP ");
            OracleParameter[] parameters1 = {
                    new OracleParameter(":YJXHUP", OracleType.VarChar,50)
                };
            parameters1[0].Value = lsajbdModel.YJXH;
            hash.Add(strSql.ToString(), parameters1);


            #region 阅卷人信息
            strSql.Clear();
            strSql.Append("insert into YX_DZJZ_LSGL(");
            strSql.Append("LSZH,LSXM,LSDW,LSDWDZ,LSDWYZHM,LSLXDH,LSSJ,DELXR,DELXRDH,LSZGYXSJ,SFDXZG,LSXXBZ,CJSJ,ZHYCYJSJ,SFSC)");
            strSql.Append(" values (");
            strSql.Append(":LSZH,:LSXM,:LSDW,:LSDWDZ,:LSDWYZHM,:LSLXDH,:LSSJ,:DELXR,:DELXRDH,:LSZGYXSJ,:SFDXZG,:LSXXBZ,:CJSJ,:ZHYCYJSJ,:SFSC)");
            OracleParameter[] parameters4 = {
					new OracleParameter(":LSZH", OracleType.VarChar,100),
					new OracleParameter(":LSXM", OracleType.VarChar,60),
					new OracleParameter(":LSDW", OracleType.VarChar,300),
					new OracleParameter(":LSDWDZ", OracleType.VarChar,300),
					new OracleParameter(":LSDWYZHM", OracleType.Char,6),
					new OracleParameter(":LSLXDH", OracleType.VarChar,50),
					new OracleParameter(":LSSJ", OracleType.VarChar,50),
					new OracleParameter(":DELXR", OracleType.VarChar,60),
					new OracleParameter(":DELXRDH", OracleType.VarChar,50),
					new OracleParameter(":LSZGYXSJ", OracleType.DateTime),
					new OracleParameter(":SFDXZG", OracleType.Char,1),
					new OracleParameter(":LSXXBZ", OracleType.VarChar,300),
					new OracleParameter(":CJSJ", OracleType.DateTime),
                    new OracleParameter(":ZHYCYJSJ", OracleType.DateTime),
					new OracleParameter(":SFSC", OracleType.Char,1)};
            parameters4[0].Value = lsglModel.LSZH;
            parameters4[1].Value = lsglModel.LSXM;
            parameters4[2].Value = lsglModel.LSDW;
            parameters4[3].Value = lsglModel.LSDWDZ;
            parameters4[4].Value = lsglModel.LSDWYZHM;
            parameters4[5].Value = lsglModel.LSLXDH;
            parameters4[6].Value = lsglModel.LSSJ;
            parameters4[7].Value = lsglModel.DELXR;
            parameters4[8].Value = lsglModel.DELXRDH;
            parameters4[9].Value = lsglModel.LSZGYXSJ ?? (object)DBNull.Value;
            parameters4[10].Value = lsglModel.SFDXZG;
            parameters4[11].Value = lsglModel.LSXXBZ;
            parameters4[12].Value = lsglModel.CJSJ;
            parameters4[13].Value = lsglModel.ZHYCYJSJ ?? (object)DBNull.Value;
            parameters4[14].Value = lsglModel.SFSC;
            hash.Add(strSql.ToString(), parameters4);
            #endregion

            #region 律师申请
            strSql.Clear();
            strSql.Append("insert into YX_DZJZ_LSYJSQ(");
            strSql.Append("LSZH,SQSJ,SQSM,SFSC,SHRGH,SHR,SHSM,SHSJ,YJSQDM,SQDZT,YJSQDH)");
            strSql.Append(" values (");
            strSql.Append(":LSZH,:SQSJ,:SQSM,:SFSC,:SHRGH,:SHR,:SHSM,:SHSJ,:YJSQDM,:SQDZT,:YJSQDH)");
            OracleParameter[] parameters2 = {
					new OracleParameter(":LSZH", OracleType.VarChar,100),					
					new OracleParameter(":SQSJ", OracleType.DateTime),
					new OracleParameter(":SQSM", OracleType.VarChar,300),
					new OracleParameter(":SFSC", OracleType.Char,1),
					new OracleParameter(":SHRGH", OracleType.Char,4),
					new OracleParameter(":SHR", OracleType.VarChar,60),
					new OracleParameter(":SHSM", OracleType.VarChar,300),
					new OracleParameter(":SHSJ", OracleType.DateTime),
					new OracleParameter(":YJSQDM", OracleType.VarChar,300),
					new OracleParameter(":SQDZT", OracleType.Char,5),
					new OracleParameter(":YJSQDH", OracleType.VarChar,50)};
            parameters2[0].Value = (object)lsyjsqModel.LSZH ?? DBNull.Value;
            parameters2[1].Value = (object)lsyjsqModel.SQSJ ?? DBNull.Value;
            parameters2[2].Value = (object)lsyjsqModel.SQSM ?? DBNull.Value;
            parameters2[3].Value = (object)lsyjsqModel.SFSC ?? DBNull.Value;
            parameters2[4].Value = (object)lsyjsqModel.SHRGH ?? DBNull.Value;
            parameters2[5].Value = (object)lsyjsqModel.SHR ?? DBNull.Value;
            parameters2[6].Value = (object)lsyjsqModel.SHSM ?? DBNull.Value;
            parameters2[7].Value = (object)lsyjsqModel.SHSJ ?? DBNull.Value;
            parameters2[8].Value = (object)lsyjsqModel.YJSQDM ?? DBNull.Value;
            parameters2[9].Value = (object)lsyjsqModel.SQDZT ?? DBNull.Value;
            parameters2[10].Value = lsyjsqModel.YJSQDH;
            hash.Add(strSql.ToString(), parameters2);

            #endregion

            #region 律师阅卷绑定
            strSql.Clear();
            strSql.Append("insert into YX_DZJZ_LSAJBD(");
            strSql.Append("GH,BMSAH,MC,AJMC,AJLBBM,AJLBMC,YJKSSJ,YJJSSJ,YJZH,YJMM,JDSJ,JDR,JDRGH,JDBMBM,JDBMMC,JDDWBM,JDDWMC,SFSC,YJSQDH,YJXH,DWBM)");
            strSql.Append(" values (");
            strSql.Append(":GH,:BMSAH,:MC,:AJMC,:AJLBBM,:AJLBMC,:YJKSSJ,:YJJSSJ,:YJZH,:YJMM,:JDSJ,:JDR,:JDRGH,:JDBMBM,:JDBMMC,:JDDWBM,:JDDWMC,:SFSC,:YJSQDH,:YJXH,:DWBM)");
            OracleParameter[] parameters3 = {
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
            parameters3[0].Value = lsajbdModel.GH ?? "";
            parameters3[1].Value = lsajbdModel.BMSAH ?? "";
            parameters3[2].Value = lsajbdModel.MC ?? "";
            parameters3[3].Value = lsajbdModel.AJMC ?? "";
            parameters3[4].Value = lsajbdModel.AJLBBM ?? "";
            parameters3[5].Value = lsajbdModel.AJLBMC ?? "";
            parameters3[6].Value = lsajbdModel.YJKSSJ ?? (object)DBNull.Value;
            parameters3[7].Value = lsajbdModel.YJJSSJ ?? (object)DBNull.Value;
            parameters3[8].Value = lsajbdModel.YJZH ?? "";
            parameters3[9].Value = lsajbdModel.YJMM ?? "";
            parameters3[10].Value = lsajbdModel.JDSJ;
            parameters3[11].Value = lsajbdModel.JDR ?? "";
            parameters3[12].Value = lsajbdModel.JDRGH ?? "";
            parameters3[13].Value = lsajbdModel.JDBMBM ?? "";
            parameters3[14].Value = lsajbdModel.JDBMMC ?? "";
            parameters3[15].Value = lsajbdModel.JDDWBM ?? "";
            parameters3[16].Value = lsajbdModel.JDDWMC ?? "";
            parameters3[17].Value = lsajbdModel.SFSC ?? "";
            parameters3[18].Value = lsajbdModel.YJSQDH ?? "";
            parameters3[19].Value = lsajbdModel.YJXH ?? "";
            parameters3[20].Value = lsajbdModel.DWBM ?? "";
            hash.Add(strSql.ToString(), parameters3);
            #endregion

            #region 律师阅卷文件绑定
            foreach (EDRS.Model.YX_DZJZ_LSAJWJFP model in modelList)
            {
                strSql.Clear();
                strSql.Append(" insert into YX_DZJZ_LSAJWJFP(");
                strSql.Append("YJXH,WJXH,SFSC)");
                strSql.Append(" values (");
                strSql.AppendFormat(":YJXH{0},:WJXH{0},:SFSC{0})", count);
                OracleParameter[] parameters = {
                    new OracleParameter(":YJXH"+count, OracleType.VarChar,50),
                    new OracleParameter(":WJXH"+count, OracleType.VarChar,100),
                    new OracleParameter(":SFSC"+count, OracleType.Char,1)
                };
                parameters[0].Value = lsajbdModel.YJXH;
                parameters[1].Value = model.WJXH;
                parameters[2].Value = "N";

                hash.Add(strSql.ToString(), parameters);
                count++;
            }
            #endregion

            try
            {
                return DbHelperOra.ExecuteSqlTran(hash);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool AddList(EDRS.Model.YX_DZJZ_LSYJSQ lsyjsqModel, EDRS.Model.YX_DZJZ_LSAJBD lsajbdModel, List<EDRS.Model.YX_DZJZ_LSAJWJFP> modelList)", "EDRS.OracleDAL.YX_DZJZ_LSAJBD");
            }
            return false;
        }
        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool DeleteList(string ids)
        {
            string sqlStr = "update YX_DZJZ_LSAJBD set SFSC='Y' where YJXH in (" + ids + ")";
            int rows = 0;
            try
            {
                rows = DbHelperOra.ExecuteSql(sqlStr.ToString());
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool DeleteList(string ids)", "EDRS.OracleDAL.YX_DZJZ_LSAJBD", sqlStr.ToString());
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
        /// 律师阅卷登录
        /// </summary>
        /// <param name="yjzh"></param>
        /// <param name="yjmm"></param>
        /// <returns></returns>
        public DataSet GetModelByZH(string yjzh,string yjmm)
        {
           // string sqlstr = "select GH,BMSAH,YJXH,MC,AJMC,AJLBBM,AJLBMC,YJKSSJ,YJJSSJ,YJZH,YJMM,JDSJ,JDR,JDRGH,JDBMBM,JDBMMC,JDDWBM,JDDWMC,SFSC,YJSQDH,DWBM from  YX_DZJZ_LSAJBD where YJZH=:YJZH and YJMM=:YJMM ";
            string sqlstr = "select b.*,j.ajbh,j.wsbh,j.wsmc from  YX_DZJZ_LSAJBD b left join yx_dzjz_jzjbxx j on b.BMSAH=j.BMSAH where b.MC=:YJZH and b.YJMM=:YJMM";
            OracleParameter[] parameters = {
					                        new OracleParameter(":YJZH", OracleType.VarChar,100),
					                        new OracleParameter(":YJMM", OracleType.Char,8)
                                           };
            parameters[0].Value = yjzh ?? "";
            parameters[1].Value = yjmm ?? "";
            DataSet ds= DbHelperOra.Query(sqlstr,parameters);
            return ds;
        }

        /// <summary>
        /// 律师阅卷登录
        /// </summary>
        /// <param name="yjzh"></param>
        /// <param name="yjmm"></param>
        /// <returns></returns>
        public DataSet GetYZYJZH(string strWhere, params object[] objValues)
        {
            string sqlstr = "select * from YX_DZJZ_LSAJBD a left join YX_DZJZ_LSYJSQ b on a.gh = b.lszh and b.SFSC='N'";
            if (strWhere.Trim() != "")
            {
                sqlstr += " where 1=1 " + strWhere;
            }
            try
            {
                return DbHelperOra.Query(sqlstr, ParameterHelp.ParameterReset(strWhere, objValues));
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataSet GetYZYJZH(string strWhere, params object[] objValues)", "EDRS.OracleDAL.YX_DZJZ_LSAJBD", sqlstr, ParameterHelp.ParameterReset(strWhere, objValues));
            }
            return null;
        }

        /// <summary>
        /// 判断是否审核通过(用过则可打印)
        /// </summary>
        /// <param name="yjxh"></param>
        /// <returns></returns>
        //public DataSet GetModelIsYes(string yjxh)
        //{
        //    string sql = "";
        //}
    }
}

