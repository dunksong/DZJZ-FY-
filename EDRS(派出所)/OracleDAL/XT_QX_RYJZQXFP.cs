using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using Maticsoft.DBUtility;
using EDRS.IDAL;

using EDRS.Common;
using System.Collections;
using System.Data.OracleClient;
using System.Data;//Please add references

namespace EDRS.OracleDAL
{
    public class XT_QX_RYJZQXFP : IXT_QX_RYJZQXFP
    {
        public HttpRequest context = null;//客户端对象，用于记录日志，客户端信息
        public void SetHttpContext(System.Web.HttpRequest _context)
        {
            this.context = _context;
        }
        public bool AddRyJzQxFpList(string dwbm, string gh, List<string> bmsahs)
        {
            //新增前先删除
            if (!DeleteRyJzQxFpList(dwbm, gh, bmsahs))
            {
                return false;
            }
            string qxbm = GetRyJzQxFpBM(dwbm, gh);
            Hashtable sqlList = new Hashtable();
            foreach (string bmsah in bmsahs)
            {
                string strSql = "INSERT INTO XT_QX_RYJZQXFP";
                strSql += "(RYJZQXBM,BMSAH,DWBM,GH)";
                strSql += "VALUES(:RYJZQXBM,:BMSAH,:DWBM,:GH)";

                OracleParameter[] parameters = new OracleParameter[] { 
                    new OracleParameter(":RYJZQXBM",qxbm),
                    new OracleParameter(":BMSAH",bmsah),
                    new OracleParameter(":DWBM",dwbm),
                    new OracleParameter(":gh",gh)
                };
                sqlList.Add(strSql, parameters);
            }
            try
            {
                //throw new Exception("测试事务提交错误日志记录");
                return DbHelperOra.ExecuteSqlTran(sqlList);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool AddRyJzQxFpList(string dwbm, string gh, List<string> bmsahs)", "EDRS.OracleDAL.XT_QX_RYJZQXFP", sqlList);
            }
            return false;
        }

        public bool DeleteRyJzQxFpList(string dwbm, string gh, List<string> bmsahs)
        {
            string qxbm = GetRyJzQxFpBM(dwbm, gh);
            Hashtable sqlList = new Hashtable();
            foreach (string bmsah in bmsahs)
            {
                string strSql = "DELETE XT_QX_RYJZQXFP";
                strSql += "WHERE RYJZQXBM=:RYJZQXBM AND BMSAH=:BMSAH AND DWBM=:DWBM AND GH=:GH";
                OracleParameter[] parameters = new OracleParameter[] { 
                    new OracleParameter(":RYJZQXBM",qxbm),
                    new OracleParameter(":BMSAH",bmsah),
                    new OracleParameter(":DWBM",dwbm),
                    new OracleParameter(":gh",gh)
                };
                sqlList.Add(strSql, parameters);
            }
            try
            {
                //throw new Exception("测试事务提交错误日志记录");
                return DbHelperOra.ExecuteSqlTran(sqlList);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public bool DeleteRyJzQxFpList(string dwbm, string gh, List<string> bmsahs)", "EDRS.OracleDAL.XT_QX_RYJZQXFP", sqlList);
            }
            return false;
        }

        public List<string> GetRyJzQxFpList(string dwbm, string gh)
        {
            List<string> bmsahs = new List<string>();
            string sql = "select BMSAH from XT_QX_RYJZQXFP t";
            sql += " WHERE 1=1 AND DWBM=:DWBM AND GH=:GH";
            OracleParameter[] parameters = new OracleParameter[] { 
                new OracleParameter(":DWBM",dwbm),
                new OracleParameter(":GH",gh)
            };
            try
            {
                DataSet ds = DbHelperOra.Query(sql, parameters);
                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        bmsahs.Add(dr[0].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public List<string> GetRyJzQxFpList(string dwbm, string gh)", "EDRS.OracleDAL.XT_QX_RYJZQXFP", sql, parameters);
            }
            return bmsahs;
        }

        public DataTable GetRyJzQxFpTable(string dwbm, string gh)
        {
            List<string> bmsahs = new List<string>();
            string sql = "select BMSAH from XT_QX_RYJZQXFP t";
            sql += " WHERE 1=1 AND DWBM=:DWBM AND GH=:GH";
            OracleParameter[] parameters = new OracleParameter[] { 
                new OracleParameter(":DWBM",dwbm),
                new OracleParameter(":GH",gh)
            };
            try
            {
                DataSet ds = DbHelperOra.Query(sql, parameters);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public DataTable GetRyJzQxFpList(string dwbm, string gh)", "EDRS.OracleDAL.XT_QX_RYJZQXFP", sql, parameters);
            }
            return null;
        }

        public string GetRyJzQxFpBM(string dwbm, string gh)
        {
            string ryJzQxbm = string.Empty;
            string sql = "select Max(RYJZQXBM) from XT_QX_RYJZQXFP t";
            object returnValue = null;
            try {
                returnValue = DbHelperOra.GetSingle(sql);
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.context, "Exception", ex.Message, "public string GetRyJzQxFpBM(string dwbm, string gh)", "EDRS.OracleDAL.XT_QX_RYJZQXFP");
            }
            if (returnValue != null)
            {
                ryJzQxbm = (Convert.ToInt32(returnValue) + 1).ToString();
            }
            ryJzQxbm = ryJzQxbm.PadLeft(10, '0');
            return ryJzQxbm;
        }
    }
}
