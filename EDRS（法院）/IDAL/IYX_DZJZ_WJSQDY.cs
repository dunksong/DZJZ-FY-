using System;
using System.Data;
using System.Collections.Generic;
namespace EDRS.IDAL
{
	/// <summary>
	/// 接口层卷宗文件申请打印表(一个案件一个律师当前阅卷
	/// </summary>
    public interface IYX_DZJZ_WJSQDY : ILogBase
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(string XH);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(EDRS.Model.YX_DZJZ_WJSQDY model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(EDRS.Model.YX_DZJZ_WJSQDY model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(string XH);
		bool DeleteList(string XHlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		EDRS.Model.YX_DZJZ_WJSQDY GetModel(string XH);
		EDRS.Model.YX_DZJZ_WJSQDY DataRowToModel(DataRow row);
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
        DataSet GetListByPageEx(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues);
		/// <summary>
		/// 根据分页获得数据列表
		/// </summary>
		//DataSet GetList(int PageSize,int PageIndex,string strWhere);

        /// <summary>
        /// 增加多条数据
        /// </summary>
        bool AddList(List<EDRS.Model.YX_DZJZ_WJSQDY> models);
         /// <summary>
        /// 判断申请是否审核
        /// </summary>
        /// <param name="yjxh"></param>
        /// <returns></returns>
        DataSet GetListIsSH(string yjxh);

        bool AddListJL(List<EDRS.Model.YX_DZJZ_WJSQDYJL> sqdyjlList, EDRS.Model.YX_DZJZ_WJSQDY sqdyModel, string xh);

        /// <summary>
        /// 修改文件打印申请记录
        /// </summary>
        /// <param name="sqdyjlList"></param>
        /// <param name="sqdyModel"></param>
        /// <param name="xh"></param>
        /// <returns></returns>
        bool UpdateListJl(List<EDRS.Model.YX_DZJZ_WJSQDYJL> sqdyjlList);
         /// <summary>
        /// 根据阅卷序号得到一个对象实体
        /// </summary>
         EDRS.Model.YX_DZJZ_WJSQDY GetModelByYJXH(string yjxh);
        
         /// <summary>
        /// 根据序号获得打印记录列表
        /// </summary>
         DataSet GetListDYJL(string strWhere, params object[] objValues);
        
		#endregion  成员方法
		#region  MethodEx

		#endregion  MethodEx
	} 
}
