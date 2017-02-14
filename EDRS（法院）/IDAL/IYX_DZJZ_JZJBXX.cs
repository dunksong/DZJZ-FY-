
using System;
using System.Data;
using System.Collections.Generic;
namespace EDRS.IDAL
{
	/// <summary>
	/// 接口层卷宗基本信息表
	/// </summary>
    public interface IYX_DZJZ_JZJBXX : ILogBase
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(string JZBH);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(EDRS.Model.YX_DZJZ_JZJBXX model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(EDRS.Model.YX_DZJZ_JZJBXX model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(string JZBH);
		bool DeleteList(string JZBHlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		EDRS.Model.YX_DZJZ_JZJBXX GetModel(string JZBH);
		EDRS.Model.YX_DZJZ_JZJBXX DataRowToModel(DataRow row);
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
        int GetRecordCountPower(string strWhere, params object[] objValues);
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        DataSet GetListByPagePower(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues);
        /// <summary>
        /// 解锁
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool LockByModelList(List<EDRS.Model.YX_DZJZ_JZJBXX> modelList);
		/// <summary>
		/// 根据分页获得数据列表
		/// </summary>
		//DataSet GetList(int PageSize,int PageIndex,string strWhere);
		#endregion  成员方法
		#region  MethodEx
        /// <summary>
        /// 获取卷宗基本信息
        /// </summary>
        /// <returns></returns>
        DataSet GetJzjbxxByBmsah(string bmsah, string dwbm);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        EDRS.Model.YX_DZJZ_JZJBXX GetModelByBMSAH(string BMSAH);

        /// <summary>
        /// 批量审核数据
        /// </summary>
        bool UpdateByZZZTList(string BMSAHlist, string SHR, DateTime SHSJ, string SHRGH, string ZZZT, string JZPZ);

        DataSet GetJzjbxxByBmsahList(string bmsahs, string dwbm);

		#endregion  MethodEx
	} 
}
