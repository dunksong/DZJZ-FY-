using System;
using System.Data;
namespace EDRS.IDAL
{
	/// <summary>
	/// 接口层IYX_DZJZ_LSYJXZSQ 的摘要说明。
	/// </summary>
	public interface IYX_DZJZ_LSYJXZSQ
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(string SQBM);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(EDRS.Model.YX_DZJZ_LSYJXZSQ model);
		/// <summary>
		/// 增加多条数据
		/// </summary>
		bool AddList(System.Collections.Generic.List<EDRS.Model.YX_DZJZ_LSYJXZSQ> models);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(EDRS.Model.YX_DZJZ_LSYJXZSQ model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(string SQBM);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool DeleteList(string SQBMlist);
        /// <summary>
        /// 删除多条数据
        /// </summary>
        bool DeleteListBySFSC(string SQBMlist);
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		EDRS.Model.YX_DZJZ_LSYJXZSQ GetModel(string SQBM);
		/// <summary>
		/// 获得数据列表
		/// </summary>
		DataSet GetList(string strWhere,params object[] objValues);
		/// <summary>
		/// 获得数据列表
		/// </summary>
		int GetRecordCount(string strWhere,params object[] objValues);
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex,params object[] objValues);
		/// <summary>
		/// 根据分页获得数据列表
		/// </summary>
		//DataSet GetList(int PageSize,int PageIndex,string strWhere);
		#endregion  成员方法
	}
}
