using System;
using System.Data;
using System.Collections.Generic;
namespace EDRS.IDAL
{
	/// <summary>
	/// 接口层系统配置表
	/// </summary>
    public interface IXY_DZJZ_XTPZ : ILogBase
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(string PZBM);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(EDRS.Model.XY_DZJZ_XTPZ model);
        /// <summary>
        /// 增加多条数据
        /// </summary>
        bool AddList(List<EDRS.Model.XY_DZJZ_XTPZ> modelList);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(EDRS.Model.XY_DZJZ_XTPZ model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(string PZBM);
		bool DeleteList(string PZBMlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		EDRS.Model.XY_DZJZ_XTPZ GetModel(string PZBM);
		EDRS.Model.XY_DZJZ_XTPZ DataRowToModel(DataRow row);
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
        /// 根据枚举值获取参数设置
        /// </summary>
        /// <param name="configID"></param>
        /// <returns></returns>
        EDRS.Model.XY_DZJZ_XTPZ GetModel(int configID);
		#endregion  MethodEx
	} 
}
