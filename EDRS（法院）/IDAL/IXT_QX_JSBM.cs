using System;
using System.Data;
namespace EDRS.IDAL
{
	/// <summary>
	/// 接口层角色编码
	/// </summary>
    public interface IXT_QX_JSBM : ILogBase
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
        bool Exists(string JSBM, string DWBM, string BMBM);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(EDRS.Model.XT_QX_JSBM model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(EDRS.Model.XT_QX_JSBM model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
        bool Delete(string JSBM, string DWBM, string BMBM);
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        EDRS.Model.XT_QX_JSBM GetModel(string JSBM, string DWBM, string BMBM);
		EDRS.Model.XT_QX_JSBM DataRowToModel(DataRow row);
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
		/// 分页获取数据列表
		/// </summary>
        DataSet GetListByPageAlly(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues);

        /// <summary>
        /// 获得数据列表排序
        /// </summary>
        DataSet GetListOrBCount(string strWhere, string order, params object[] objValues);
        /// <summary>
        /// 根据单位编码和工号获取角色
        /// </summary>
        /// <param name="dwbm"></param>
        /// <param name="gh"></param>
        /// <returns></returns>
        DataSet GetJSBMbyUser(string dwbm, string gh);
		#endregion  MethodEx
	} 
}
