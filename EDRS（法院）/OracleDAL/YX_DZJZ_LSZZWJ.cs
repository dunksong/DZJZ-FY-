using System;
using System.Collections.Generic;
using Maticsoft.DBUtility;
using EDRS.Common;//Please add references
using System.Text;
using EDRS.IDAL;
using System.Collections;
using System.Data.OracleClient;
using System.Data;

namespace EDRS.OracleDAL
{
    public partial class YX_DZJZ_LSZZWJ : IYX_DZJZ_LSZZWJ
    {
        public System.Web.HttpRequest context = null;
        public void SetHttpContext(System.Web.HttpRequest _context)
        {
            this.context = _context;
        }
        public YX_DZJZ_LSZZWJ()
        { }

        public bool AddList(string LSZH, List<string> fileList)
        {
            Hashtable sqlList = new Hashtable();
            foreach (string fileName in fileList)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into YX_DZJZ_LSZZWJ(");
                strSql.Append("LSZH,ZZWJ)");
                strSql.Append(" values (");
                strSql.Append(":LSZH,:ZZWJ)");
                OracleParameter[] parameters = {
                        new OracleParameter(":LSZH", OracleType.VarChar,100),
                        new OracleParameter(":ZZWJ", OracleType.VarChar,300),
                                               };
                parameters[0].Value = LSZH;
                parameters[1].Value = fileName;
                sqlList.Add(strSql, parameters);

            }
            try
            {
                return DbHelperOra.ExecuteSqlTran(sqlList);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool AddList(string LSZH, List<string> fileList)", "EDRS.OracleDAL.YX_DZJZ_LSZZWJ", sqlList);
            }
            return false;
        }
        public bool DelAll(string LSZH)
        {
            string sql = "UPDATE YX_DZJZ_LSZZWJ SET SFSC = 'Y' WHERE LSZH=:LSZH";
            OracleParameter[] param = new OracleParameter[]{
                new OracleParameter(":LSZH",LSZH)
            };
            try
            {
                int result = DbHelperOra.ExecuteSql(sql, param);
                return result > 0 ? true : false;
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool DelList(string LSZH, List<string> fileList)", "EDRS.OracleDAL.YX_DZJZ_LSZZWJ", sql);
            }
            return false;
        }
        public bool DelList(string LSZH, List<string> fileList)
        {
            string files = "";
            foreach (string filename in fileList)
            {
                files += string.IsNullOrEmpty(files) ? "" : ",";
                files += "'" + filename + "'";
            }
            string sql = "UPDATE YX_DZJZ_LSZZWJ SET SFSC = 'Y' WHERE LSZH=:LSZH AND ZZWJ IN (" + files + ")";
            OracleParameter[] param = new OracleParameter[] { 
                new OracleParameter(":LSZH",OracleType.VarChar,100)
            };
            param[0].Value = LSZH;
            try
            {
                int result = DbHelperOra.ExecuteSql(sql, param);
                return result > 0 ? true : false;
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool DelList(string LSZH, List<string> fileList)", "EDRS.OracleDAL.YX_DZJZ_LSZZWJ", sql);
            }
            return false;
        }

        public List<string> GetList(string LSZH)
        {
            string sql = "select LSZH,ZZWJ,SFSC from YX_DZJZ_LSZZWJ WHERE LSZH=:LSZH AND SFSC='N'";
            OracleParameter[] param = new OracleParameter[] { 
                new OracleParameter(":LSZH",LSZH)
            };
            try
            {
                DataSet ds = DbHelperOra.Query(sql, param);
                if (ds == null || ds.Tables.Count == 0)
                {
                    return null;
                }

                List<string> fileList = new List<string>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    fileList.Add(dr["ZZWJ"].ToString());
                }
                return fileList;
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool DelList(string LSZH, List<string> fileList)", "EDRS.OracleDAL.YX_DZJZ_LSZZWJ", sql);
            }
            return null;
        }

    }
}
