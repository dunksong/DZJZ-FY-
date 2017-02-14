
using System;
using System.Data;
using System.Collections.Generic;
using EDRS.Common;
using EDRS.Model;
using EDRS.DALFactory;
using EDRS.IDAL;

namespace EDRS.BLL
{
	/// <summary>
	/// 卷宗目录模板主表
	/// </summary>
	public partial class XT_DM_QX
	{
        private readonly IXT_DM_QX dal = DataAccess.CreateXT_DM_QX();
        public XT_DM_QX(System.Web.HttpRequest _context)
        {
            dal.SetHttpContext(_context);
        }
		#region  BasicMethod
        public DataTable GetAJLBList(string jsbm, string _dwbm, string _bmbm,string key)
        {
            return dal.GetAJLBList(jsbm, _dwbm, _bmbm,key);
        }
        public DataSet GetQXList(string jsbm, string qxType, string _dwbm, string _bmbm,string key)
        {
            return dal.GetQXList(jsbm, qxType, _dwbm, _bmbm, key);
        }
        public DataSet GetDwList(string jsbm, string _dwbm, string _bmbm,string key)
        {
            return dal.GetDwList(jsbm, _dwbm, _bmbm,key);
        }
        public DataSet GetLBList(string jsbm, string _dwbm, string _bmbm,string key)
        {
            return dal.GetLBList(jsbm, _dwbm, _bmbm, key);
        }

        public bool AddDW(List<EDRS.Model.XT_DM_QX> list, string jsbm, string _dwbm, string _bmbm) 
        {
            return dal.AddDW(list, jsbm, _dwbm, _bmbm); 
        }
        public bool DelDW(List<EDRS.Model.XT_DM_QX> list, string jsbm, string _dwbm, string _bmbm) 
        {
            return dal.DelDW(list, jsbm, _dwbm, _bmbm); 
        }
        public bool AddLB(List<EDRS.Model.XT_DM_QX> list, string jsbm, string _dwbm, string _bmbm) 
        { 
            return dal.AddLB(list, jsbm, _dwbm, _bmbm); 
        }
        public bool DelLB(List<EDRS.Model.XT_DM_QX> list, string jsbm, string _dwbm, string _bmbm)
        {
            return dal.DelLB(list, jsbm, _dwbm, _bmbm);
        }
        public DataSet GetTreeList(string strWhere, string strQxWhere, params object[] objValues)
        {
            return dal.GetTreeList(strWhere,strQxWhere, objValues);
        }
        /// <summary>
        /// 获取用户所有角色权限
        /// </summary>
        /// <param name="dwbm">单位编码</param>
        /// <param name="bmbm">部门编码</param>
        /// <param name="gh">工号</param>
        /// <param name="type">部门权限还是类型权限（0，1）</param>
        /// <returns></returns>
        public DataSet GetQxListByRole(string dwbm, string bmbm, string gh, int type, string strWhere)
        {
            return dal.GetQxListByRole(dwbm, bmbm, gh, type, strWhere);
        }
        public DataSet GetQxListByRole(string strWhere, params object[] objValues)
        {
            return dal.GetQxListByRole(strWhere,objValues);
        }
        public DataSet GetDwQxList(string dwbm, string jsbms)
        {
            string CacheKey = "XT_DZJZ_JZMBModel-" + dwbm;
            object objModel = EDRS.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetDwQxList(dwbm, jsbms);
                    if (objModel != null)
                    {
                        int ModelCache = EDRS.Common.ConfigHelper.GetConfigInt("ModelCache");
                        EDRS.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (DataSet)objModel;


           // return dal.GetDwQxList(dwbm, jsbms);
        }
        /// <summary>
        /// 获取权限列表，包含单位编码和案件类别编号
        /// </summary>
        /// <param name="dwbm"></param>
        /// <param name="gh"></param>
        /// <returns></returns>
        public DataSet GetQxList(string dwbm, string gh)
        {
            return dal.GetQxList(dwbm,gh);
        }

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

