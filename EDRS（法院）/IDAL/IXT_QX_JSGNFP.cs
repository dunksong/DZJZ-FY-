using System;
using System.Data;
namespace EDRS.IDAL
{
	/// <summary>
	/// 接口层角色功能授权表
	/// </summary>
    public interface IXT_QX_JSGNFP : ILogBase
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(string DWBM,string JSBM,string GNBM);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(EDRS.Model.XT_QX_JSGNFP model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(EDRS.Model.XT_QX_JSGNFP model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(string DWBM,string JSBM,string GNBM);
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		EDRS.Model.XT_QX_JSGNFP GetModel(string DWBM,string JSBM,string GNBM);
		EDRS.Model.XT_QX_JSGNFP DataRowToModel(DataRow row);
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
        /// 根据单位编码和工号查询角色功能
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="objValues"></param>
        /// <returns></returns>
        DataSet GetJSGNFPByGh(string strWhere, params object[] objValues);
		#endregion  MethodEx
	} 
}
