using System;
using System.Data;
namespace EDRS.IDAL
{
	/// <summary>
	/// 接口层案件基本信息扩展表
	/// </summary>
    public interface ITYYW_GG_AJJBXXKZ : ILogBase
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(string AJKZXH);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(EDRS.Model.TYYW_GG_AJJBXXKZ model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(EDRS.Model.TYYW_GG_AJJBXXKZ model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(string AJKZXH);
		bool DeleteList(string AJKZXHlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		EDRS.Model.TYYW_GG_AJJBXXKZ GetModel(string AJKZXH);
		EDRS.Model.TYYW_GG_AJJBXXKZ DataRowToModel(DataRow row);
		/// <summary>
		/// 获得数据列表
		/// </summary>
		DataSet GetList(string strWhere);

        int GetRecordCount(string strWhere);
        
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
		/// <summary>
		/// 根据分页获得数据列表
		/// </summary>
		//DataSet GetList(int PageSize,int PageIndex,string strWhere);
		#endregion  成员方法
		#region  MethodEx

		#endregion  MethodEx
	} 
}
