using System;
using System.Data;
using System.Collections.Generic;

namespace EDRS.IDAL
{
	/// <summary>
	/// 接口层XT_QX_GNDY
	/// </summary>
    public interface IXT_DM_QX : ILogBase
	{
		#region  成员方法
        DataTable GetAJLBList(string jsbm,string _dwbm,string _bmbm,string key);
        /// <summary>
        /// 根据角色编码获取所以权限,0:单位权限，1：案件类别权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        DataSet GetQXList(string jsbm, string qxType,string _dwbm,string _bmbm,string key);
        DataSet GetTreeList(string strWhere, string strQxWhere, params object[] objValues);
        /// <summary>
        /// 获取当前角色未分配的单位列表
        /// </summary>
        /// <param name="jsbm"></param>
        /// <returns></returns>
        DataSet GetDwList(string jsbm, string _dwbm, string _bmbm,string key);
        /// <summary>
        /// 获取当前角色未分配的类别列表
        /// </summary>
        /// <param name="jsbm"></param>
        /// <returns></returns>
        DataSet GetLBList(string jsbm, string _dwbm, string _bmbm,string key);

        /// <summary>
        /// 添加部门权限
        /// </summary>
        /// <param name="bmbm">单位编码</param>
        /// <param name="jsbm">角色编码</param>
        /// <returns>返回空时，成功</returns>
        bool AddDW(List<EDRS.Model.XT_DM_QX> list, string jsbm, string _dwbm, string _bmbm);
        /// <summary>
        /// 删除部门权限
        /// </summary>
        /// <param name="bmbm">部门编码</param>
        /// <param name="jsbm">角色编码</param>
        /// <returns>返回空时，成功</returns>
        bool DelDW(List<EDRS.Model.XT_DM_QX> list, string jsbm, string _dwbm, string _bmbm);
        /// <summary>
        /// 添加类别权限
        /// </summary>
        /// <param name="lbbm">案件类别编码</param>
        /// <param name="jsbm">角色编码</param>
        /// <returns>返回空时，成功</returns>
        bool AddLB(List<EDRS.Model.XT_DM_QX> list, string jsbm, string _dwbm, string _bmbm);
        /// <summary>
        /// 删除类别权限
        /// </summary>
        /// <param name="lbbm">案件类别编码</param>
        /// <param name="jsbm">角色编码</param>
        /// <returns>返回空时，成功</returns>
        bool DelLB(List<EDRS.Model.XT_DM_QX> list, string jsbm, string _dwbm, string _bmbm);

        /// <summary>
        /// 获取用户所有角色权限
        /// </summary>
        /// <param name="dwbm">单位编码</param>
        /// <param name="bmbm">部门编码</param>
        /// <param name="gh">工号</param>
        /// <param name="type">部门权限还是类型权限（0，1）</param>
        /// <returns></returns>
        DataSet GetQxListByRole(string dwbm, string bmbm, string gh, int type, string strWhere);
        DataSet GetQxListByRole(string strWhere, params object[] objValues);
        /// <summary>
        /// 获取单位权限编码，组装树形结构数据
        /// </summary>
        /// <param name="dwbm"></param>
        /// <param name="jsbms"></param>
        /// <returns></returns>
        DataSet GetDwQxList(string dwbm, string jsbms); 

        /// <summary>
        /// 获取权限列表，包含单位编码和案件类别编号
        /// </summary>
        /// <param name="dwbm"></param>
        /// <param name="gh"></param>
        /// <returns></returns>
        DataSet GetQxList(string dwbm, string gh);
		#endregion  成员方法
	} 
}
