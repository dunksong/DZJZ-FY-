using System;
using System.Data;
namespace EDRS.IDAL
{
	/// <summary>
	/// 接口层律师管理表
	/// </summary>
    public interface IYX_DZJZ_LSGL : ILogBase
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(string LSZH);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(EDRS.Model.YX_DZJZ_LSGL model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(EDRS.Model.YX_DZJZ_LSGL model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(string LSZH);
		bool DeleteList(string LSZHlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		EDRS.Model.YX_DZJZ_LSGL GetModel(string LSZH);
		EDRS.Model.YX_DZJZ_LSGL DataRowToModel(DataRow row);
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
        /// 分页获取数据列表
        /// </summary>
        DataSet GetListByPageEx(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues);
        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetListfile(string strWhere, params object[] objValues);
		#endregion  成员方法
		#region  MethodEx
        /// <summary>
        /// 更新最后修改时间
        /// </summary>
        /// <param name="LSZH"></param>
        /// <param name="zhxgsj"></param>
        /// <returns></returns>
        bool UpdateZHXGSJ(string LSZH, DateTime zhxgsj);

		#endregion  MethodEx
	} 
}
