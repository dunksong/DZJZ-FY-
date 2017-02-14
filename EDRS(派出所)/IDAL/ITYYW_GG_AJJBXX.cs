
using System;
using System.Data;
namespace EDRS.IDAL
{
	/// <summary>
	/// 接口层案件基本信息表
	/// </summary>
	public interface ITYYW_GG_AJJBXX : ILogBase
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(string BMSAH);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(EDRS.Model.TYYW_GG_AJJBXX model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(EDRS.Model.TYYW_GG_AJJBXX model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(string BMSAH);
		bool DeleteList(string BMSAHlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		EDRS.Model.TYYW_GG_AJJBXX GetModel(string BMSAH);
		EDRS.Model.TYYW_GG_AJJBXX DataRowToModel(DataRow row);
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

		#region  扩展接口
        /// <summary>
        /// 扩展分页获取数据列表
        /// </summary>
        DataSet GetListByPageUnite(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues);
        /// <summary>
        /// 扩展获取记录总数
        /// </summary>
        int GetRecordCountUnite(string strWhere, params object[] objValues);
        
		#endregion  扩展接口
	} 
}
