using System;
using System.Data;
namespace EDRS.IDAL
{
	/// <summary>
	/// 接口层部门编码
	/// </summary>
    public interface IXT_ZZJG_BMBM : ILogBase
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(string BMBM);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(EDRS.Model.XT_ZZJG_BMBM model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(EDRS.Model.XT_ZZJG_BMBM model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(string BMBM);
		bool DeleteList(string BMBMlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		EDRS.Model.XT_ZZJG_BMBM GetModel(string DWBM,string BMBM);
		EDRS.Model.XT_ZZJG_BMBM DataRowToModel(DataRow row);
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
        bool DeleteListLogic(string BMBMlist, string DWBMlist);
       
        /// <summary>
        /// 获取树形结构当前节点所有父节点
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="objValues"></param>
        /// <returns></returns>
        DataSet GetTreeList(string strWhere, string withWhere, params object[] objValues);
        /// <summary>
        /// 获取单位部门视图
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="objValues"></param>
        /// <returns></returns>
        DataSet GetOrganization(string strWhere, params object[] objValues);
        /// <summary>
        /// 获得数据列表排序
        /// </summary>
        DataSet GetListOrBCount(string strWhere, string order, params object[] objValues);
        /// <summary>
        /// 根据父级编号获取1级子集
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="objValues"></param>
        /// <returns></returns>
        DataSet GetTreeChildren(string strWhere, params object[] objValues);

        /// <summary>
        /// 获取部门编码的使用数量
        /// </summary>
        /// <param name="dwbm"></param>
        /// <returns></returns>
        int GetBmbmCount(string dwbm, string bmbm);
		#endregion  MethodEx
	} 
}
