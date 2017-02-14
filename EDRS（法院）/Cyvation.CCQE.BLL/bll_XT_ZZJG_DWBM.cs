using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Cyvation.CCQE.DataAccess;
using System.Data;
using Cyvation.CCQE.Common;
using Cyvation.CCQE.Model;

namespace Cyvation.CCQE.BLL
{
    public class bll_XT_ZZJG_DWBM
    {
        Database dbases;
        private const string DisplayFields = "DWBM,DWMC,FDWBM,DWJB,SFSC,DWJC,DWSX";
        public bll_XT_ZZJG_DWBM()
        {
            dbases = DataAccessor.CreateDatabase();
        }

        public string GetListData(string name, int pageIndex, int pageSize, out int rowCount)
        {
            string errMsgStr = "";
            rowCount = 0;
            string querySql = "select * from xt_zzjg_dwbm ";
            if (name.Length > 0)
            {
                querySql += "where dwmc like '%" + name + "%' ";
            }
            List<KeyValueItem> items = new List<KeyValueItem>();
            items.Add(new KeyValueItem("p_srcsql", querySql));
            items.Add(new KeyValueItem("p_pageindex", pageIndex));
            items.Add(new KeyValueItem("p_pagesize", pageSize));
            items.Add(new KeyValueItem("p_rowcount", rowCount));
            items.Add(new KeyValueItem("p_errmsg", errMsgStr));
            DataSet ds = dbases.ExecuteDataSet<KeyValueItem>("PKG_ZZJG_manage.proc_xt_zzjg_dwbm_list", items.ToArray());
            int.TryParse(items[items.Count - 2].Value.ToString(), out rowCount);
            var errMsg = items[items.Count - 1];

            return EasyUIHelper.BuildDataGridDataSource(rowCount, ds.Tables[0]);
        }

        public string Delete(string ids)
        {
            string errMsgStr = "";
            List<KeyValueItem> items = new List<KeyValueItem>();
            items.Add(new KeyValueItem("p_dwbms", ids));
            items.Add(new KeyValueItem("p_errmsg", errMsgStr));
            dbases.ExecuteNonQuery<KeyValueItem>("PKG_ZZJG_manage.proc_xt_zzjg_dwbm_delete", items.ToArray());
            errMsgStr = items[items.Count - 1].Value.ToString();

            return errMsgStr;
        }

        public bool Insert(DwbmModel modelobj, out string validSummary)
        {
            string errMsgStr = "";
            List<KeyValueItem> items = new List<KeyValueItem>();
            items.Add(new KeyValueItem("p_DWBM", modelobj.DWBM));
            items.Add(new KeyValueItem("p_DWMC", modelobj.DWMC));
            items.Add(new KeyValueItem("p_DWJB", modelobj.DWJB));
            items.Add(new KeyValueItem("p_SFSC", modelobj.SFSC));
            items.Add(new KeyValueItem("p_DWJC", modelobj.DWJC));
            items.Add(new KeyValueItem("p_DWSX", modelobj.DWSX));
            items.Add(new KeyValueItem("p_errmsg", errMsgStr));
            DataSet ds = dbases.ExecuteDataSet<KeyValueItem>("PKG_ZZJG_manage.proc_xt_zzjg_dwbm_insert", items.ToArray());
            validSummary = items[items.Count - 1].Value.ToString();

            return string.IsNullOrEmpty(validSummary);
        }

        public bool Update(DwbmModel modelobj, out string validSummary)
        {
            string errMsgStr = "";
            List<KeyValueItem> items = new List<KeyValueItem>();
            items.Add(new KeyValueItem("p_DWBM", modelobj.DWBM));
            items.Add(new KeyValueItem("p_DWMC", modelobj.DWMC));
            items.Add(new KeyValueItem("p_DWJB", modelobj.DWJB));
            items.Add(new KeyValueItem("p_SFSC", modelobj.SFSC));
            items.Add(new KeyValueItem("p_DWJC", modelobj.DWJC));
            items.Add(new KeyValueItem("p_DWSX", modelobj.DWSX));
            items.Add(new KeyValueItem("p_errmsg", errMsgStr));
            DataSet ds = dbases.ExecuteDataSet<KeyValueItem>("PKG_ZZJG_manage.proc_xt_zzjg_dwbm_update", items.ToArray());
            validSummary = items[items.Count - 1].Value.ToString();

            return string.IsNullOrEmpty(validSummary);
        }
    }
}
