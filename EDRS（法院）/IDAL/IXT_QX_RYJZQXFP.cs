using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EDRS.IDAL
{
    public interface IXT_QX_RYJZQXFP : ILogBase
    {
        #region 成员方法
        /// <summary>
        /// 增加权限,增加时先删除，再增加所有
        /// </summary>
        /// <param name="dwbm"></param>
        /// <param name="gh"></param>
        /// <param name="bmsahs"></param>
        /// <returns></returns>
        bool AddRyJzQxFpList(string dwbm, string gh, List<string> bmsahs);
        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="dwbm"></param>
        /// <param name="gh"></param>
        /// <param name="bmsahs"></param>
        /// <returns></returns>
        bool DeleteRyJzQxFpList(string dwbm, string gh, List<string> bmsahs);
        /// <summary>
        /// 获取已有权限列表
        /// </summary>
        /// <param name="dwbm"></param>
        /// <param name="gh"></param>
        /// <returns></returns>
        List<string> GetRyJzQxFpList(string dwbm, string gh);
        /// <summary>
        /// 获取已有权限列表
        /// </summary>
        /// <param name="dwbm"></param>
        /// <param name="gh"></param>
        /// <returns></returns>
        DataTable GetRyJzQxFpTable(string dwbm, string gh);
        /// <summary>
        /// 生成编号
        /// </summary>
        /// <param name="dwbm"></param>
        /// <param name="gh"></param>
        /// <returns></returns>
        string GetRyJzQxFpBM(string dwbm, string gh);
        #endregion
    }
}
