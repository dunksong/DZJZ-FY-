using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EDRS.IDAL;
using EDRS.DALFactory;

namespace EDRS.BLL
{
    /// <summary>
    /// 统计类
    /// </summary>
    public partial class DataStatistics
    {
        private readonly IDataStatistics dal=DataAccess.CreateDataStatistics();
        public DataStatistics(System.Web.HttpRequest _context)
        {
            dal.SetHttpContext(_context);
        }

        #region 卷宗统计数量
        /// <summary>
        /// 卷宗数量统计
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="dwbm">单位编码</param>
        /// <param name="gh">工号</param>
        /// <param name="systemmark">系统编号</param>
        /// <param name="configid">配置类型编号</param>
        /// <param name="orderby">排序</param>       
        /// <param name="count">返回总数</param>
        /// <param name="objValues">参数值</param>
        /// <returns></returns>
        public DataSet GetJZNumberByReport(string strWhere, string dwbm, string gh, string systemmark, int configid, out Decimal count, params object[] objValues)
        {
            return dal.GetJZNumberByReport(strWhere, dwbm, gh, systemmark, configid, out count, objValues);
        } 
        #endregion

        #region 单位卷宗制作情况统计
        /// <summary>
        /// 单位卷宗制作情况统计
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="roleDwbmWhere">角色数据权限限制</param>
        /// <param name="orderby">排序</param>
        /// <param name="startIndex">分页数</param>
        /// <param name="endIndex">每页显示数</param>
        /// <param name="count">返回总数</param>
        /// <param name="objValues">参数值</param>
        /// <returns></returns>
        public DataSet GetDwJzZzStatistics(string strWhere, string strWhereDw, string dwbm, string gh,string jsbm,string bmbm, string orderby, int startIndex, int endIndex, out int count, params object[] objValues)
        {
            return dal.GetDwJzZzStatistics(strWhere, strWhereDw, dwbm, gh,jsbm,bmbm, orderby, startIndex, endIndex, out  count, objValues);
        } 
        #endregion


        #region 单位卷宗制作情况业务统计
        /// <summary>
        /// 单位卷宗制作情况业务统计
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="dwbm">单位编码</param>
        /// <param name="gh">工号</param>       
        /// <param name="objValues">参数值</param>
        /// <returns></returns>
        public DataSet GetDwJzZzStatisticsByYw(string strWhere, string strWhereDw, string dwbm, string gh, string jsbm, string bmbm, string orderby, params object[] objValues)
        {
            return dal.GetDwJzZzStatisticsByYw(strWhere, strWhereDw, dwbm, gh, jsbm, bmbm, orderby, objValues);
        } 
        #endregion

        #region 单位卷宗制作情况类别统计
        /// <summary>
        /// 单位卷宗制作情况类别统计
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="dwbm">单位编码</param>
        /// <param name="gh">工号</param>       
        /// <param name="objValues">参数值</param>
        /// <returns></returns>
        public DataSet GetDwJzZzStatisticsByLb(string strWhere, string strWhereDw, string dwbm, string gh, string jsbm, string bmbm, string orderby, params object[] objValues)
        {
            return dal.GetDwJzZzStatisticsByLb(strWhere, strWhereDw, dwbm, gh, jsbm, bmbm, orderby, objValues);
        }
        #endregion

        #region 单位卷宗制作查询
        /// <summary>
        /// 单位卷宗制作查询
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="dwbm">单位编码</param>
        /// <param name="gh">工号</param>
        /// <param name="orderby">排序</param>
        /// <param name="startIndex">分页数</param>
        /// <param name="endIndex">每页显示数</param>
        /// <param name="count">返回总数</param>
        /// <param name="objValues">参数值</param>
        /// <returns></returns>
        public DataSet GetDwJzZzQuery(string strWhere, string dwbm, string gh, string orderby, int startIndex, int endIndex, out int count, params object[] objValues)
        {
            return dal.GetDwJzZzQuery(strWhere, dwbm, gh, orderby, startIndex, endIndex, out  count, objValues);
        } 
        #endregion

        /// <summary>
        /// 单位案件文件大小统计
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="dwbm">单位编码</param>
        /// <param name="gh">工号</param>
        /// <param name="orderby">排序</param>
        /// <param name="startIndex">分页数</param>
        /// <param name="endIndex">每页显示数</param>
        /// <param name="count">返回总数</param>
        /// <param name="objValues">参数值</param>
        /// <returns></returns>
        public DataSet GetDwAjWjSum(string strWhere, string dwbm, string gh, string orderby, int startIndex, int endIndex, out int count, params object[] objValues)
        {
            return dal.GetDwAjWjSum(strWhere, dwbm, gh, orderby, startIndex, endIndex, out  count, objValues);
        }

        /// <summary>
        /// 业绩统计
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="orderby"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <param name="count"></param>
        /// <param name="objValues"></param>
        /// <returns></returns>
        public DataTable GetYJTJ(string strWhere,string strWhereAj,string strWhereRy, string orderby, int startIndex, int endIndex, out int count, params object[] objValues)
        {
            return dal.GetYJTJ(strWhere,strWhereAj,strWhereRy, orderby, startIndex, endIndex, out count, objValues);
        }
    }
}
