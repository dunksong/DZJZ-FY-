using System;
using System.Data;
namespace EDRS.IDAL
{
	/// <summary>
	/// 接口层所属类别
	/// </summary>
	public interface IXT_DZJZ_SSLB
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(string SSLBBM);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(EDRS.Model.XT_DZJZ_SSLB model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(EDRS.Model.XT_DZJZ_SSLB model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(string SSLBBM);
		bool DeleteList(string SSLBBMlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		EDRS.Model.XT_DZJZ_SSLB GetModel(string SSLBBM);
		EDRS.Model.XT_DZJZ_SSLB DataRowToModel(DataRow row);
		/// <summary>
		/// 获得数据列表
		/// </summary>
		DataSet GetList(string strWhere);
		/// <summary>
		/// 根据分页获得数据列表
		/// </summary>
		//DataSet GetList(int PageSize,int PageIndex,string strWhere);
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        int GetRecordCount(string strWhere, params object[] objValues);
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues);

        /// <summary>
        /// 获取最大顺序数
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="objValues"></param>
        /// <returns></returns>
        object GetMaxSx(string strWhere, params object[] objValues);
		#endregion  成员方法
		#region  MethodEx
        /// <summary>
        /// 获取树形结构当前节点所有父节点
        /// </summary>
        /// <param name="strWhere">数据查询条件</param>
        /// <param name="withWhere">循环开始条件</param>
        /// <param name="direction">查询方向（true父级向子级查询，false子级向父级）</param>
        /// <param name="objValues">参数</param>
        /// <returns>DataSet</returns>
        DataSet GetTreeList(string strWhere, string withWhere, bool direction, params object[] objValues);
		#endregion  MethodEx
	} 
}
