using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EDRS.IDAL
{
    public interface IEDRS_Report : ILogBase
    {

        #region 卷宗统计报表  -- CaseReport
        /// <summary>
        /// 根据分类获取案件
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="objValues"></param>
        /// <returns></returns>
        DataTable GetListGroupByAjLx(string strWhere, string strWhere1, params object[] objValues);
        /// <summary>
        /// 根据案件信息赛选案件
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="objValues"></param>
        /// <returns></returns>
        DataTable GetListGroupByAj(string strWhere, params object[] objValues);
        /// <summary>
        /// 根据卷筛选案件
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="objValues"></param>
        /// <returns></returns>
        DataTable GetListGroupByJ(string strWhere, params object[] objValues);
        /// <summary>
        /// 根据文件赛选案件
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="objValues"></param>
        /// <returns></returns>
        DataTable GetListGroupByM(string strWhere, params object[] objValues);
        /// <summary>
        /// 获取文件列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="objValues"></param>
        /// <returns></returns>
        DataTable GetWList(string strWhere, params object[] objValues);
        /// <summary>
        /// 获取目录列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="objValues"></param>
        /// <returns></returns>
        DataTable GetMLList(string strWhere, params object[] objValues);
        #endregion

        #region 卷宗制作工作量查询 -- MakeCaseReport.aspx
        DataSet GetCaseByPerson(string strWhere, string roleDwbmWhere, int pageSize, int pageIndex, string orderby, out int count, params object[] objValues);
        DataSet GetCaseByPerson(string groupType, string strWhere, string roleDwbmWhere, int pageSize, int pageIndex, string orderby, out int count, params object[] objValues);
        #endregion

        #region 卷宗数量统计
        #endregion

        #region 卷宗月统计报图
        DataSet GetCaseGroupMouth(string where, string roleDwbmWhere, string orderby, params object[] objValues);
        #endregion

        #region 单位卷宗制作情况
        #endregion

        #region 卷宗业务情况
        #endregion

        #region 根据单位获取案件
        DataSet GetCaseGroupByUnit(string where, string dwbm, string gh, int pageSize, int pageIndex, string orderby, out int count, params object[] objValues);
        DataSet GetCaseGroupByUnitLb(string where, string dwbm, string gh, int pageSize, int pageIndex, string orderby, out int count, params object[] objValues);
        DataSet GetCaseGroupByUnitYw(string where, string dwbm, string gh, int pageSize, int pageIndex, string orderby, out int count, params object[] objValues);
        #endregion
        #region 卷宗业务情况
        /// <summary>
        /// 获取所有业务类型
        /// </summary>
        /// <returns></returns>
        DataSet GetAllBusinessType();
        //卷宗业务情况查询
        DataSet GetCaseBusiness(string where, string roleDwbmWhere, int pageSize, int pageIndex, string orderby, out int count, params object[] objValues);
        //卷宗业务情况汇总
        DataSet GetCaseBusinessReport(string where, string havingWhere, string roleDwbmWhere, int pageSize, int pageIndex, string orderby, out int count, params object[] objValues);
        #endregion

    }
}
