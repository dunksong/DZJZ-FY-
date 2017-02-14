
using System;
using System.Data;
namespace EDRS.IDAL
{
	/// <summary>
	/// 接口层卷宗目录
	/// </summary>
    public interface IYX_DZJZ_JZML : ILogBase
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(string JZBH,string MLBH);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(EDRS.Model.YX_DZJZ_JZML model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(EDRS.Model.YX_DZJZ_JZML model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(string JZBH,string MLBH);
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		EDRS.Model.YX_DZJZ_JZML GetModel(string JZBH,string MLBH);
		EDRS.Model.YX_DZJZ_JZML DataRowToModel(DataRow row);
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
        /// 获取树形案件目录列表
        /// </summary>
        /// <param name="strMlWhere">目录表查询条件</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="withWhere">循环条件</param>
        /// <param name="direction">查询顺序</param>
        /// <param name="objValues"></param>
        /// <returns></returns>
        DataSet GetListByTree(string strMlWhere, string strWhere, string withWhere, bool direction, string yjxh, bool isAll, params object[] objValues);
        DataSet GetListByTreeEx(string strMlWhere, string strWhere, string withWhere, bool direction, string yjxh, bool isAll, params object[] objValues);
        /// <summary>
        /// 获取树形案件卷和文件
        /// </summary>
        /// <param name="strMlWhere">目录表查询条件</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="withWhere">循环条件</param>
        /// <param name="direction">查询顺序</param>
        /// <param name="yjxh">阅卷序号</param>
        /// <param name="isAll">是否只加载所有</param>
        /// <param name="objValues"></param>
        /// <returns></returns>
        DataSet GetListByTreeJaM(string strMlWhere, string strWhere, string withWhere, bool direction, string yjxh, bool isAll, params object[] objValues);

        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="strOrder"></param>
        /// <param name="objValues"></param>
        /// <returns></returns>
        DataSet GetListByWjmc(string strWhere, string strOrder, params object[] objValues);

        /// <summary>
        /// 卷宗文件获取
        /// </summary>
        /// <param name="DWBM">单位编码</param>
        /// <param name="BMSAH">部门受案号</param>
        /// <param name="JZBH">卷宗编号</param>
        /// <returns></returns>
        DataSet GetDossierFileInfo(string DWBM, string BMSAH, string JZBH);

        /// <summary>
        /// 卷宗信息获取
        /// </summary>
        /// <param name="BH">部门受案号</param>
        /// <param name="DWBM">单位编码</param>
        /// <returns></returns>
        DataSet GetDossierInfo(string BH, string DWBM);

        /// <summary>
        /// 卷宗页获取
        /// </summary>
        /// <param name="DWBM">单位编号</param>
        /// <param name="BH">部门受案号</param>
        /// <param name="JZBH">卷宗编号</param>
        /// <param name="JZWJBH">卷宗文件编号</param>
        DataSet GetDossierFilePageInfo(string DWBM, string BH, string JZBH, string JZWJBH);
		#endregion  MethodEx
	} 
}
