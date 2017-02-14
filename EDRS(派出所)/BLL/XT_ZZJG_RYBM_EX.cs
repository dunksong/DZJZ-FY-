using System;
using System.Data;
using System.Collections.Generic;
using EDRS.Common;
using EDRS.Model;
using EDRS.DALFactory;
using EDRS.IDAL;
using EDRS.Common.DEncrypt;
namespace EDRS.BLL
{
	/// <summary>
	/// 人员编码
	/// </summary>
	public partial class XT_ZZJG_RYBM
	{
        #region 根据用户名密码查询用户信息
        /// <summary>
        /// 根据用户名密码查询用户信息
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPwd"></param>
        /// <returns></returns>
        public Model.XT_ZZJG_RYBM UserLogin(string userType, string userName, string userPwd, out List<EDRS.Model.XT_QX_JSBM> jsbmList, out string msg)
        {
            msg = "";
            jsbmList = null;
            List<Model.XT_ZZJG_RYBM> listModel = GetModelListAndDwbm("and XT_ZZJG_RYBM.DWBM=:DWBM and DLBM=:DLBM and XT_ZZJG_RYBM.SFSC='N'", new object[] { userType, userName });
            if (listModel == null || listModel.Count == 0)
                msg = "登录账号不存在";            
            else
            {
                foreach (Model.XT_ZZJG_RYBM model in listModel)
                {
                    if (model.KL.Equals(MD5Encrypt.Encrypt(userPwd).ToLower()))
                    {
                        if (model.SFTZ == "Y")
                        {
                            msg = "该用户已被停职";
                            return null;
                        }
                        else
                        {
                            //获取用户相关角色
                            jsbmList = GetRoleByList(model.DWBM, model.GH);
                            System.Web.Caching.Cache objCache = System.Web.HttpContext.Current.Cache;                     
                            objCache.Remove("IXT_QX_GNDYList-" + model.DWBM + model.GH);
                            objCache.Remove("XY_DZJZ_XTPZModelList-" + model.DWBM + model.GH);
                            objCache.Remove("XT_DZJZ_JZMBModel-" + model.DWBM);
                            objCache.Remove("XT_QX_JSANQXModel-" + model.DWBM + model.GH);
                            msg = "登录成功";
                            model.KL = "";
                            return model;
                        }
                    }else
                        msg = "登录密码错误";
                }
                if (string.IsNullOrEmpty(msg))
                    msg = "登录账号不存在";
            }
            return null;
        } 
        #endregion

        #region 获取角色信息
        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="dwbm"></param>
        /// <param name="gh"></param>
        /// <returns></returns>
        private List<EDRS.Model.XT_QX_JSBM> GetRoleByList(string dwbm, string gh)
        {
            EDRS.BLL.XT_QX_JSBM bll = new EDRS.BLL.XT_QX_JSBM(null);
            DataSet ds = bll.GetJSBMbyUser(dwbm, gh);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                List<EDRS.Model.XT_QX_JSBM> list = bll.DataTableToList(ds.Tables[0]);
                if (list != null && list.Count > 0)
                    return list;
            }
            return null;
        }
        public bool ExistsDlbm(string dwbm, string gh, string dlbm)
        {
            return dal.ExistsDlbm(dwbm,gh, dlbm);
        }
        #endregion

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCountAndGn(string strWhere, params object[] objValues)
        {
            return dal.GetRecordCountAndGn(strWhere, objValues);
        }

        /// <summary>
		/// 分页获取数据列表
		/// </summary>
        public DataSet GetListByPageAndGn(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)
        {
            return dal.GetListByPageAndGn(strWhere, orderby, startIndex, endIndex, objValues);
        }
	}
}

