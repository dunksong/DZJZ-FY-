using System;
using System.Data;
namespace EDRS.IDAL
{
	/// <summary>
	/// 接口层单位编码
	/// </summary>
    public interface IXT_ZZJG_DWBM : ILogBase
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(string DWBM);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(EDRS.Model.XT_ZZJG_DWBM model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(EDRS.Model.XT_ZZJG_DWBM model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(string DWBM);
		bool DeleteList(string DWBMlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		EDRS.Model.XT_ZZJG_DWBM GetModel(string DWBM);
		EDRS.Model.XT_ZZJG_DWBM DataRowToModel(DataRow row);
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
        /// 批量逻辑删除数据
        /// </summary>
        bool DeleteListLogic(string DWBMlist);
        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetListOrBCount(string strWhere, params object[] objValues);
        /// <summary>
        /// 获取树形结构当前节点所有父节点
        /// </summary>
        /// <param name="strWhere">数据查询条件</param>
        /// <param name="withWhere">循环开始条件</param>
        /// <param name="direction">查询方向（true父级向子级查询，false子级向父级）</param>
        /// <param name="objValues">参数</param>
        /// <returns>DataSet</returns>
        DataSet GetTreeList(string strWhere, string withWhere, bool direction, params object[] objValues);

        DataSet GetTreeListById(string strWhere, string withWhere, string siftWhere, bool direction, params object[] objValues);

        /// <summary>
        /// 根据单位编码和工号获取对应有权限的单位编码
        /// </summary>
        /// <param name="dwbm"></param>
        /// <param name="gh"></param>
        /// <returns></returns>
        DataSet GetDwbmListByGh(string dwbm, string gh);

        /// <summary>
        /// 获取单位编码的使用数量
        /// </summary>
        /// <param name="dwbm"></param>
        /// <returns></returns>
        int GetDwbmCount(string dwbm);
		#endregion  MethodEx
	} 
}
