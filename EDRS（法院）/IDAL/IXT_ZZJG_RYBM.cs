using System;
using System.Data;
namespace EDRS.IDAL
{
	/// <summary>
	/// 接口层人员编码
	/// </summary>
    public interface IXT_ZZJG_RYBM : ILogBase
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
        bool Exists(string GH);
        bool ExistsDlbm(string dwbm,string gh,string dlbm);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(EDRS.Model.XT_ZZJG_RYBM model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(EDRS.Model.XT_ZZJG_RYBM model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(string GH);
		bool DeleteList(string GHlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		EDRS.Model.XT_ZZJG_RYBM GetModel(string GH);
		EDRS.Model.XT_ZZJG_RYBM DataRowToModel(DataRow row);
		/// <summary>
		/// 获得数据列表
		/// </summary>
        DataSet GetList(string strWhere, params object[] objValues);
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        int GetRecordCount(string strWhere, params object[] objValues);
        /// <summary>
        /// 获取记录总数
        /// </summary>
        int GetRecordCountAndGn(string strWhere, params object[] objValues);
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues);
        /// <summary>
		/// 分页获取数据列表
		/// </summary>
        DataSet GetListByPageAndGn(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues);
		/// <summary>
		/// 根据分页获得数据列表
		/// </summary>
		//DataSet GetList(int PageSize,int PageIndex,string strWhere);
		#endregion  成员方法
		#region  MethodEx
        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetListAndDWBM(string strWhere, params object[] objValues);
        /// <summary>
        /// 获取人员列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="orderby"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <param name="objValues"></param>
        /// <returns></returns>
        DataSet GetListByBm(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues);
		#endregion  MethodEx
	} 
}
