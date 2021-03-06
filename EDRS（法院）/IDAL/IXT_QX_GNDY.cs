﻿using System;
using System.Data;
namespace EDRS.IDAL
{
	/// <summary>
	/// 接口层XT_QX_GNDY
	/// </summary>
	public interface IXT_QX_GNDY : ILogBase
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(string GNBM,string DWBM);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(EDRS.Model.XT_QX_GNDY model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(EDRS.Model.XT_QX_GNDY model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(string GNBM,string DWBM);
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		EDRS.Model.XT_QX_GNDY GetModel(string GNBM,string DWBM);
		EDRS.Model.XT_QX_GNDY DataRowToModel(DataRow row);
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
        /// 获取功能菜单
        /// </summary>
        DataSet GetListByType(string dwbm, string strWhere, params object[] objValues);
        /// <summary>
        /// 获取角色对应功能
        /// </summary>
        DataSet GetListByBound(string dwbm,string gh, string strWhere, params object[] objValues);
		#endregion  MethodEx
	} 
}
