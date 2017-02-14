using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDRS.DALFactory;
using EDRS.IDAL;
using System.Data;

namespace EDRS.BLL
{
    public class XT_QX_RYJZQXFP
    {
        private readonly IXT_QX_RYJZQXFP dal = DataAccess.CreateXT_QX_RYJZQXFP();
        public XT_QX_RYJZQXFP(System.Web.HttpRequest _context)
        {
            dal.SetHttpContext(_context);
        }

        /// <summary>
        /// 增加权限,增加时先删除，再增加所有
        /// </summary>
        /// <param name="dwbm"></param>
        /// <param name="gh"></param>
        /// <param name="bmsahs"></param>
        /// <returns></returns>
        public bool AddRyJzQxFpList(string dwbm, string gh, List<string> bmsahs) {
            return dal.AddRyJzQxFpList(dwbm, gh, bmsahs);
        }
        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="dwbm"></param>
        /// <param name="gh"></param>
        /// <param name="bmsahs"></param>
        /// <returns></returns>
        public bool DeleteRyJzQxFpList(string dwbm, string gh, List<string> bmsahs)
        {
            return dal.DeleteRyJzQxFpList(dwbm, gh, bmsahs);
        }
        /// <summary>
        /// 获取已有权限列表
        /// </summary>
        /// <param name="dwbm"></param>
        /// <param name="gh"></param>
        /// <returns></returns>
        public List<string> GetRyJzQxFpList(string dwbm, string gh)
        {
            return dal.GetRyJzQxFpList(dwbm, gh);
        }
        public DataTable GetRyJzQxFpTable(string dwbm, string gh)
        {
            return dal.GetRyJzQxFpTable(dwbm, gh);
        }
    }
}
