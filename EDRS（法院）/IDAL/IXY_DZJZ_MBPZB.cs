using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
namespace EDRS.IDAL
{
    /// <summary>
    /// 接口层卷宗模板配置表
    /// </summary>
    public interface IXY_DZJZ_MBPZB : ILogBase
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string DossierTypeValueMember);
        /// <summary>
        /// 子级是否存在该记录
        /// </summary>
        bool ExistsChildren(string DossierTypeValueMember);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        bool Add(EDRS.Model.XY_DZJZ_MBPZB model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(EDRS.Model.XY_DZJZ_MBPZB model);

        bool Update(Hashtable sqlList);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(string DossierTypeValueMember);
        bool DeleteList(string DossierTypeValueMemberlist);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        EDRS.Model.XY_DZJZ_MBPZB GetModel(string DossierTypeValueMember);
        EDRS.Model.XY_DZJZ_MBPZB DataRowToModel(DataRow row);
        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList(string strWhere, params object[] objValues);
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        int GetRecordCount(string strWhere, params object[] objValues);
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues);
        /// <summary>
        /// 根据分页获得数据列表
        /// </summary>
        //DataSet GetList(int PageSize,int PageIndex,string strWhere);
        #endregion  成员方法
        #region  MethodEx
        /// <summary>
        /// 获取树形结构当前节点所有父节点
        /// </summary>
        /// <param name="strWhere">数据查询条件</param>
        /// <param name="withWhere">循环开始条件</param>
        /// <param name="direction">查询方向（true父级向子级查询，false子级向父级）</param>
        /// <param name="objValues">参数</param>
        /// <returns>DataSet</returns>
        DataSet GetTreeList(string strWhere, string withWhere, bool direction, params object[] objValues);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool DeleteNode(string DossierTypeValueMember);

        /// <summary>
        /// 根据单位编码获取最近一个单位编码的模板
        /// </summary>
        /// <param name="dwbm">单位编码</param>
        /// <returns></returns>
        DataSet GetListMinDwbm(string dwbm);

        DataSet GetDwAjList(out int count, string where, string orderBy, int startIndex, int endIndex, params object[] objValues);
        /// <summary>
        /// 批量从ICE向本地数据库添加模板
        /// </summary>
        /// <param name="list"></param>
        /// <param name="dwbm"></param>
        /// <param name="ajlbbm"></param>
        /// <param name="sslbbm"></param>
        /// <returns></returns>
        bool AddList(List<Dictionary<string,string>> list,string dwbm,string ajlbbm,string ajlbmc,string sslbbm,string sslbmc);
        /// <summary>
        /// 删除选中的节点及下级文件
        /// </summary>
        /// <param name="dwbm"></param>
        /// <param name="ajlbbm"></param>
        /// <param name="sslbbm"></param>
        /// <returns></returns>
        bool DeleteNodeAndChild(string dwbm, string ajlbbm, string sslbbm);
        #endregion  MethodEx
    }
}
