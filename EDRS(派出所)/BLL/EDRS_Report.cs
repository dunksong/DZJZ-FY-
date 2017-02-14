using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDRS.DALFactory;
using System.Data;

namespace EDRS.BLL
{
    public class EDRS_Report
    {
        EDRS.IDAL.IEDRS_Report dal = DataAccess.CreateEDRS_Report();
        public EDRS_Report(System.Web.HttpRequest _context)
        {
            dal.SetHttpContext(_context);
        }
        #region 卷宗统计报表  -- CaseReport
        public DataTable GetListGroupByAjLx(string strWhere, string strWhere1, params object[] objValues)
        {
            return dal.GetListGroupByAjLx(strWhere, strWhere1, objValues);
        }

        public DataTable GetListGroupByAj(string strWhere, params object[] objValues)
        {
            return dal.GetListGroupByAj(strWhere, objValues);
        }
        public DataTable GetListGroupByJ(string strWhere, params object[] objValues)
        {
            return dal.GetListGroupByJ(strWhere, objValues);
        }
        public DataTable GetListGroupByM(string strWhere, params object[] objValues)
        {
            return dal.GetListGroupByM(strWhere, objValues);
        }
        public DataTable GetWList(string strWhere, params object[] objValues)
        {
            return dal.GetWList(strWhere, objValues);
        }

        #endregion

        #region 卷宗制作工作量查询 -- MakeCaseReport.aspx
        public DataSet GetCaseByPerson(string strWhere, string roleDwbmWhere, int pageSize, int pageIndex, string orderby, out int count, params object[] objValues)
        {
            return dal.GetCaseByPerson(strWhere, roleDwbmWhere, pageSize, pageIndex, orderby, out count, objValues);
        }
        public DataSet GetCaseByPerson(string groupType,string strWhere, string roleDwbmWhere, int pageSize, int pageIndex, string orderby, out int count, params object[] objValues)
        {
            return dal.GetCaseByPerson(groupType, strWhere, roleDwbmWhere, pageSize, pageIndex, orderby, out count, objValues);
        }

        #endregion

        #region 根据单位对案件统计查询
        public DataSet GetCaseGroupByUnit(string where, string dwbm, string gh, int pageSize, int pageIndex, string orderby, out int count, params object[] objValues)
        {
            return dal.GetCaseGroupByUnit(where,dwbm,gh, pageSize, pageIndex, orderby,out count, objValues);
        }
        public DataSet GetCaseGroupByUnitLb(string where, string dwbm, string gh, int pageSize, int pageIndex, string orderby, out int count, params object[] objValues)
        {
            return dal.GetCaseGroupByUnitLb(where, dwbm, gh, pageSize, pageIndex, orderby, out count, objValues);
        }
        public DataSet GetCaseGroupByUnitYw(string where, string dwbm, string gh, int pageSize, int pageIndex, string orderby, out int count, params object[] objValues)
        {
            return dal.GetCaseGroupByUnitYw(where, dwbm, gh, pageSize, pageIndex, orderby, out count, objValues);
        }
        #endregion

        #region  月统计视图
        public DataSet GetCaseGroupMouth(string where, string roleDwbmWhere, string orderby, params object[] objValues)
        {
            return dal.GetCaseGroupMouth(where, roleDwbmWhere,orderby, objValues);
        }
        #endregion
        #region 卷宗业务情况
        /// <summary>
        /// 获取所有业务类型
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllBusinessType()
        {
            return dal.GetAllBusinessType();
        }
        //卷宗业务情况查询
        public DataSet GetCaseBusiness(string where, string roleDwbmWhere, int pageSize, int pageIndex, string orderby, out int count, params object[] objValues)
        {
            return dal.GetCaseBusiness(where, roleDwbmWhere, pageSize, pageIndex, orderby, out count, objValues);
        }
        //卷宗业务情况汇总
        public DataSet GetCaseBusinessReport(string where, string havingWhere, string roleDwbmWhere, int pageSize, int pageIndex, string orderby, out int count, params object[] objValues)
        {
            return dal.GetCaseBusinessReport(where, havingWhere, roleDwbmWhere, pageSize, pageIndex, orderby, out count, objValues);
        }
        #endregion
    }
}
