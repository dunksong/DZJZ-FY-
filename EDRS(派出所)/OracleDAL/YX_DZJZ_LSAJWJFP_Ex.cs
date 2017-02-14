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
    /// 数据访问类:YX_DZJZ_LSAJWJFP
	/// </summary>
	public partial class YX_DZJZ_LSAJWJFP
    {
        /// <summary>
        /// 增加多条数据
        /// </summary>
        public bool AddList(List<EDRS.Model.YX_DZJZ_LSAJWJFP> modelList, EDRS.Model.YX_DZJZ_LSAJBD lsajbdModel, string yjxh)
        {
            int count = 0;
            //Hashtable hash = new Hashtable();
            Dictionary<object, object> hash = new Dictionary<object, object>();
          

            //    hash = LSAJBD(lsajbdModel);

                StringBuilder strSql = new StringBuilder();
                strSql.Append(" update YX_DZJZ_LSAJWJFP set SFSC='Y' where YJXH=:YJXHUP ");
                OracleParameter[] parameters1 = {
                    new OracleParameter(":YJXHUP", OracleType.VarChar,50)
                };
                parameters1[0].Value = yjxh;
                hash.Add(strSql.ToString(), parameters1);
         

            foreach (EDRS.Model.YX_DZJZ_LSAJWJFP model in modelList)
            {
                strSql.Clear();          
                strSql.Append(" insert into YX_DZJZ_LSAJWJFP(");
                strSql.Append("YJXH,WJXH,SFSC)");
                strSql.Append(" values (");
                strSql.Append(":YJXH" + count + ",:WJXH" + count + ",:SFSC" + count + ")");
                OracleParameter[] parameters = {
                    new OracleParameter(":YJXH"+count, OracleType.VarChar,50),
                    new OracleParameter(":WJXH"+count, OracleType.VarChar,100),
                    new OracleParameter(":SFSC"+count, OracleType.Char,1)
                };
                parameters[0].Value = yjxh;
                parameters[1].Value = model.WJXH;
                parameters[2].Value = "N";

                hash.Add(strSql.ToString(), parameters);
                count++;
            }
            try
            {
                return DbHelperOra.ExecuteSqlTran(hash);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool AddList(List<EDRS.Model.YX_DZJZ_LSAJWJFP> modelList)", "EDRS.OracleDAL.YX_DZJZ_LSAJWJFP");
            }
            return false;
        }

        private Dictionary<object, object> LSAJBD(EDRS.Model.YX_DZJZ_LSAJBD model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into YX_DZJZ_LSAJBD(");
            strSql.Append("GH,BMSAH,MC,AJMC,AJLBBM,AJLBMC,YJKSSJ,YJJSSJ,YJZH,YJMM,JDSJ,JDR,JDRGH,JDBMBM,JDBMMC,JDDWBM,JDDWMC,SFSC,YJSQDH,DWBM)");
            strSql.Append(" values (");
            strSql.Append(":GH,:BMSAH,:MC,:AJMC,:AJLBBM,:AJLBMC,:YJKSSJ,:YJJSSJ,:YJZH,:YJMM,:JDSJ,:JDR,:JDRGH,:JDBMBM,:JDBMMC,:JDDWBM,:JDDWMC,:SFSC,:YJSQDH,:DWBM)");
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
                    new OracleParameter(":DWBM",OracleType.VarChar,50)};
            parameters[0].Value = model.GH;
            parameters[1].Value = model.BMSAH;
            parameters[2].Value = model.MC;
            parameters[3].Value = model.AJMC;
            parameters[4].Value = model.AJLBBM;
            parameters[5].Value = model.AJLBMC;
            parameters[6].Value = model.YJKSSJ;
            parameters[7].Value = model.YJJSSJ;
            parameters[8].Value = model.YJZH;
            parameters[9].Value = model.YJMM;
            parameters[10].Value = model.JDSJ;
            parameters[11].Value = model.JDR;
            parameters[12].Value = model.JDRGH;
            parameters[13].Value = model.JDBMBM;
            parameters[14].Value = model.JDBMMC;
            parameters[15].Value = model.JDDWBM;
            parameters[16].Value = model.JDDWMC;
            parameters[17].Value = model.SFSC;
            parameters[18].Value = model.YJSQDH;
            parameters[19].Value = model.DWBM;

            Dictionary<object, object> hash = new Dictionary<object, object>();
            hash.Add(strSql.ToString(), parameters);
            return hash;
        }
	}
}

