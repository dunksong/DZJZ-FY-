using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EDRS.IDAL
{
    public interface IDataStatistics : ILogBase
    {
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
        DataSet GetJZNumberByReport(string strWhere, string dwbm, string gh, string systemmark, int configid, out Decimal count, params object[] objValues);
        /// <summary>
        /// 单位卷宗制作情况统计
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
        DataSet GetDwJzZzStatistics(string strWhere, string strWhereDw, string dwbm, string gh, string jsbm,string bmbm,string orderby, int startIndex, int endIndex, out int count, params object[] objValues);

        /// <summary>
        /// 单位卷宗制作情况业务统计
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="dwbm">单位编码</param>
        /// <param name="gh">工号</param>       
        /// <param name="objValues">参数值</param>
        /// <returns></returns>
        DataSet GetDwJzZzStatisticsByYw(string strWhere, string strWhereDw, string dwbm, string gh, string jsbm, string bmbm, string orderby, params object[] objValues);

        /// <summary>
        /// 单位卷宗制作情况类别统计
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="dwbm">单位编码</param>
        /// <param name="gh">工号</param>       
        /// <param name="objValues">参数值</param>
        /// <returns></returns>
        DataSet GetDwJzZzStatisticsByLb(string strWhere, string strWhereDw, string dwbm, string gh, string jsbm, string bmbm, string orderby, params object[] objValues);

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
        DataSet GetDwJzZzQuery(string strWhere, string dwbm, string gh, string orderby, int startIndex, int endIndex, out int count, params object[] objValues);

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
        DataSet GetDwAjWjSum(string strWhere, string dwbm, string gh, string orderby, int startIndex, int endIndex, out int count, params object[] objValues);

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
        DataTable GetYJTJ(string strWhere, string strWhereAj, string strWhereRy, string orderby, int startIndex, int endIndex, out int count, params object[] objValues);
    }
}
