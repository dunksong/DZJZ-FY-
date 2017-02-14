using System;
using System.Data;
using System.Collections.Generic;
namespace EDRS.IDAL
{
	/// <summary>
	/// 接口层卷宗日志记录表
	/// </summary>
    public interface IYX_DZJZ_JZRZJL : ILogBase
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(decimal XH);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(EDRS.Model.YX_DZJZ_JZRZJL model);
        /// <summary>
		/// 增加一条数据
		/// </summary>
        bool AddByModelList(List<EDRS.Model.YX_DZJZ_JZRZJL> modelList);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(EDRS.Model.YX_DZJZ_JZRZJL model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(decimal XH);
		bool DeleteList(string XHlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		EDRS.Model.YX_DZJZ_JZRZJL GetModel(decimal XH);
		EDRS.Model.YX_DZJZ_JZRZJL DataRowToModel(DataRow row);
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
        /// 分页获取数据列表
        /// </summary>
        DataSet GetListByPageProc(DateTime startTime, DateTime endTime, string strWhere, string orderby, int startIndex, int endIndex, ref int count);
		/// <summary>
		/// 根据分页获得数据列表
		/// </summary>
		//DataSet GetList(int PageSize,int PageIndex,string strWhere);
		#endregion  成员方法
		#region  MethodEx

		#endregion  MethodEx
	} 
}
