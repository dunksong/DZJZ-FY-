
using System;
using System.Data;
namespace EDRS.IDAL
{
	/// <summary>
	/// 接口层卷宗目录文件
	/// </summary>
    public interface IYX_DZJZ_JZMLWJ : ILogBase
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(string JZBH,string MLBH,string WJXH);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(EDRS.Model.YX_DZJZ_JZMLWJ model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(EDRS.Model.YX_DZJZ_JZMLWJ model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(string JZBH,string MLBH,string WJXH);
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		EDRS.Model.YX_DZJZ_JZMLWJ GetModel(string JZBH,string MLBH,string WJXH);
		EDRS.Model.YX_DZJZ_JZMLWJ DataRowToModel(DataRow row);
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

		#endregion  MethodEx
	} 
}
