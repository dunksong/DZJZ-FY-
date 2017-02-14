
using System;
using System.Data;
using System.Collections.Generic;
namespace EDRS.IDAL
{
	/// <summary>
	/// 接口层卷宗目录模板主表
	/// </summary>
    public interface IXT_DZJZ_ZZCS : ILogBase
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(string MBJZBH);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(EDRS.Model.XT_DZJZ_ZZCS model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(EDRS.Model.XT_DZJZ_ZZCS model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(string MBJZBH);
		bool DeleteList(string MBJZBHlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		EDRS.Model.XT_DZJZ_ZZCS GetModel(string MBJZBH);
		EDRS.Model.XT_DZJZ_ZZCS DataRowToModel(DataRow row);
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
        /// 增加多条数据
        /// </summary>
        bool AddList(List<EDRS.Model.XT_DZJZ_ZZCS> modelList);
		#endregion  MethodEx
	} 
}
