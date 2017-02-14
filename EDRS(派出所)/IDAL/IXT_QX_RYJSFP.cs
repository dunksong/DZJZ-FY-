using System;
using System.Data;
namespace EDRS.IDAL
{
	/// <summary>
	/// 接口层人员角色分配
	/// </summary>
    public interface IXT_QX_RYJSFP : ILogBase
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(string DWBM,string BMBM,string JSBM,string GH);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(EDRS.Model.XT_QX_RYJSFP model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(EDRS.Model.XT_QX_RYJSFP model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(string DWBM,string BMBM,string JSBM,string GH);
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		EDRS.Model.XT_QX_RYJSFP GetModel(string DWBM,string BMBM,string JSBM,string GH);
		EDRS.Model.XT_QX_RYJSFP DataRowToModel(DataRow row);
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

        /// <summary>
		/// 获取所有下级部门的人员
		/// </summary>
        DataSet GetListChildrenBmAll(string strWhere, params object[] objValues);
		#endregion  成员方法
		#region  MethodEx

		#endregion  MethodEx
	} 
}
