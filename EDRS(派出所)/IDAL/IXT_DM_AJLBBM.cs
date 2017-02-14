using System;
using System.Data;
namespace EDRS.IDAL
{
	/// <summary>
	/// 接口层扫描设备登记表
	/// </summary>
    public interface IXT_DM_AJLBBM : ILogBase
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(string AJLBBM);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(EDRS.Model.XT_DM_AJLBBM model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(EDRS.Model.XT_DM_AJLBBM model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(string AJLBBM);
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		EDRS.Model.XT_DM_AJLBBM GetModel(string AJLBBM);
		EDRS.Model.XT_DM_AJLBBM DataRowToModel(DataRow row);
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
        DataSet GetSSLBList(string strWhere, params object[] objValues);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(EDRS.Model.XT_DM_AJLBBM model, string ajlbbm);

        /// <summary>
        /// 删除多条数据
        /// </summary>
        bool DeleteList(string ajlbbmList);
        
		#endregion  MethodEx
	} 
}
