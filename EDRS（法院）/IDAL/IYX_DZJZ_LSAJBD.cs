using System;
using System.Data;
using System.Collections.Generic;
namespace EDRS.IDAL
{
	/// <summary>
	/// 接口层律师阅卷绑定表(一个
	/// </summary>
    public interface IYX_DZJZ_LSAJBD : ILogBase
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(string YJXH);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(EDRS.Model.YX_DZJZ_LSAJBD model);
        /// <summary>
        /// 增加多条数据
        /// </summary>
        bool AddList(EDRS.Model.YX_DZJZ_LSYJSQ lsyjsqModel, EDRS.Model.YX_DZJZ_LSAJBD lsajbdModel, List<EDRS.Model.YX_DZJZ_LSAJWJFP> modelList, EDRS.Model.YX_DZJZ_LSGL lsglModel);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(EDRS.Model.YX_DZJZ_LSAJBD model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(string YJXH);
        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool DeleteList(string ids);

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		EDRS.Model.YX_DZJZ_LSAJBD GetModel(string YJXH);
		EDRS.Model.YX_DZJZ_LSAJBD DataRowToModel(DataRow row);
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
        /// <summary>
        /// 律师阅卷登录
        /// </summary>
        /// <param name="yjzh"></param>
        /// <param name="yjmm"></param>
        /// <returns></returns>
        DataSet GetModelByZH(string yjzh, string yjmm);
        /// <summary>
        /// 律师阅卷登录
        /// </summary>
        /// <param name="yjzh"></param>
        /// <param name="yjmm"></param>
        /// <returns></returns>
        DataSet GetYZYJZH(string strWhere, params object[] objValues);
        
		#endregion  成员方法
		#region  MethodEx

		#endregion  MethodEx
	} 
}
