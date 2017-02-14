using System;
using System.Data;
namespace EDRS.IDAL
{
	/// <summary>
	/// 接口层电子卷宗封面打印
	/// </summary>
    public interface IYX_DZJZ_FMDY : ILogBase
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(string BM);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(EDRS.Model.YX_DZJZ_FMDY model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(EDRS.Model.YX_DZJZ_FMDY model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(string BM);
		bool DeleteList(string BMlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		EDRS.Model.YX_DZJZ_FMDY GetModel(string BM);
		EDRS.Model.YX_DZJZ_FMDY DataRowToModel(DataRow row);
		/// <summary>
		/// 获得数据列表
		/// </summary>
        DataSet GetList(string strWhere, params object[] objValues);

        int GetRecordCount(string strWhere, params object[] objValues);

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
