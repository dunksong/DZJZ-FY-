using System;
using System.Data;
using System.Collections.Generic;
namespace EDRS.IDAL
{
	/// <summary>
	/// 接口层XT_QX_RYGNFP
	/// </summary>
    public interface IXT_QX_RYGNFP : ILogBase
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(string DWBM,string GH,string GNBM);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(EDRS.Model.XT_QX_RYGNFP model);
        /// <summary>
		/// 增加多条数据
		/// </summary>
        bool AddList(List<EDRS.Model.XT_QX_RYGNFP> modelList);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(EDRS.Model.XT_QX_RYGNFP model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(string DWBM,string GH,string GNBM);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool DeleteList(string listId);
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		EDRS.Model.XT_QX_RYGNFP GetModel(string DWBM,string GH,string GNBM);
		EDRS.Model.XT_QX_RYGNFP DataRowToModel(DataRow row);		
		/// <summary>
		/// 根据分页获得数据列表
		/// </summary>
		//DataSet GetList(int PageSize,int PageIndex,string strWhere);
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
