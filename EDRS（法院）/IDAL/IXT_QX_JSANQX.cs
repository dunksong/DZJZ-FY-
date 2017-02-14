using System;
using System.Data;
namespace EDRS.IDAL
{
	/// <summary>
	/// 接口层角色按钮权限
	/// </summary>
    public interface IXT_QX_JSANQX : ILogBase
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(string QXBM);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(EDRS.Model.XT_QX_JSANQX model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(EDRS.Model.XT_QX_JSANQX model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(string QXBM);
		bool DeleteList(string QXBMlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		EDRS.Model.XT_QX_JSANQX GetModel(string QXBM);
		EDRS.Model.XT_QX_JSANQX DataRowToModel(DataRow row);
		/// <summary>
		/// 获得数据列表
		/// </summary>
		DataSet GetList(string strWhere);
		/// <summary>
		/// 根据分页获得数据列表
		/// </summary>
		//DataSet GetList(int PageSize,int PageIndex,string strWhere);
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        int GetRecordCount(string strWhere, params object[] objValues);
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues);
		#endregion  成员方法
		#region  MethodEx
        /// <summary>
        /// 根据单位部门角色编码获取按钮权限列表
        /// </summary>
        /// <param name="dwbm"></param>
        /// <param name="bmbm"></param>
        /// <param name="jsbms"></param>
        /// <returns></returns>
        DataSet GetAnQxListByUser(string dwbm, string bmbm, string jsbms);
		#endregion  MethodEx
	} 
}
