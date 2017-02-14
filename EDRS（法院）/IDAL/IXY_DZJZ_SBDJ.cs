﻿using System;
using System.Data;
namespace EDRS.IDAL
{
	/// <summary>
	/// 接口层扫描设备登记表
	/// </summary>
    public interface IXY_DZJZ_SBDJ : ILogBase
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(string MAC,string DEVSN);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(EDRS.Model.XY_DZJZ_SBDJ model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(EDRS.Model.XY_DZJZ_SBDJ model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(string MAC,string DEVSN);
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		EDRS.Model.XY_DZJZ_SBDJ GetModel(string MAC,string DEVSN);
		EDRS.Model.XY_DZJZ_SBDJ DataRowToModel(DataRow row);
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
        int GetRecordCount(string dwbm, string jsbm, string strWhere, params object[] objValues);
        DataSet GetListByPage(string dwbm, string jsbm, string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues);
		/// <summary>
		/// 根据分页获得数据列表
		/// </summary>
		//DataSet GetList(int PageSize,int PageIndex,string strWhere);
		#endregion  成员方法
		#region  MethodEx

		#endregion  MethodEx
	} 
}
