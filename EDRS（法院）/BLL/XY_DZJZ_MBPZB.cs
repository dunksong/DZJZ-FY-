using System;
using System.Data;
using System.Collections.Generic;
using EDRS.Common;
using EDRS.Model;
using EDRS.DALFactory;
using EDRS.IDAL;
using System.Collections;
namespace EDRS.BLL
{
    /// <summary>
    /// 系统配置表
    /// </summary>
    public partial class XY_DZJZ_MBPZB
    {
        private readonly IXY_DZJZ_MBPZB dal = DataAccess.CreateXY_DZJZ_MBPZB();
        public XY_DZJZ_MBPZB(System.Web.HttpRequest _context)
        {
            dal.SetHttpContext(_context);
        }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string DossierTypeValueMember)
        {
            return dal.Exists(DossierTypeValueMember);
        }
        /// <summary>
        /// 子级是否存在该记录
        /// </summary>
        public bool ExistsChildren(string DossierTypeValueMember)
        {
            return dal.ExistsChildren(DossierTypeValueMember);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(EDRS.Model.XY_DZJZ_MBPZB model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(EDRS.Model.XY_DZJZ_MBPZB model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string DossierTypeValueMember)
        {

            return dal.Delete(DossierTypeValueMember);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string DossierTypeValueMemberlist)
        {
            return dal.DeleteList(DossierTypeValueMemberlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public EDRS.Model.XY_DZJZ_MBPZB GetModel(string DossierTypeValueMember)
        {
            return dal.GetModel(DossierTypeValueMember);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public EDRS.Model.XY_DZJZ_MBPZB GetModelByCache(string DossierTypeValueMember)
        {

            string CacheKey = "XY_DZJZ_MBPZBModel-" + DossierTypeValueMember;
            object objModel = EDRS.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(DossierTypeValueMember);
                    if (objModel != null)
                    {
                        int ModelCache = EDRS.Common.ConfigHelper.GetConfigInt("ModelCache");
                        EDRS.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (EDRS.Model.XY_DZJZ_MBPZB)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere, params object[] objValues)
        {
            return dal.GetList(strWhere, objValues);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<EDRS.Model.XY_DZJZ_MBPZB> GetModelList(string strWhere, params object[] objValues)
        {
            DataSet ds = dal.GetList(strWhere, objValues);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<EDRS.Model.XY_DZJZ_MBPZB> DataTableToList(DataTable dt)
        {
            List<EDRS.Model.XY_DZJZ_MBPZB> modelList = new List<EDRS.Model.XY_DZJZ_MBPZB>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                EDRS.Model.XY_DZJZ_MBPZB model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere, params object[] objValues)
        {
            return dal.GetRecordCount(strWhere, objValues);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex, objValues);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  BasicMethod
        #region  ExtensionMethod
        /// <summary>
        /// 获取树形结构当前节点所有父节点
        /// </summary>
        /// <param name="strWhere">数据查询条件</param>
        /// <param name="withWhere">循环开始条件</param>
        /// <param name="direction">查询方向（true父级向子级查询，false子级向父级）</param>
        /// <param name="objValues">参数</param>
        /// <returns>DataSet</returns>
        public DataSet GetTreeList(string strWhere, string withWhere, bool direction, params object[] objValues)
        {
            return dal.GetTreeList(strWhere, withWhere, direction, objValues);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteNode(string DossierTypeValueMember)
        {
            return dal.DeleteNode(DossierTypeValueMember);
        }
        /// <summary>
        /// 根据单位编码获取最近一个单位编码的模板
        /// </summary>
        /// <param name="dwbm">单位编码</param>
        /// <returns></returns>
        public DataSet GetListMinDwbm(string dwbm)
        {
            return dal.GetListMinDwbm(dwbm);
        }


        public bool Update(Hashtable sqlList)
        {
            return dal.Update(sqlList);
        }

        public DataSet GetDwAjList(out int count, string where, string orderBy, int startIndex, int endIndex, params object[] objValues)
        {
            return dal.GetDwAjList(out count,where, orderBy, startIndex, endIndex, objValues);
        }

        public bool AddList(List<Dictionary<string, string>> list, string dwbm, string ajlbbm,string ajlbmc, string sslbbm,string sslbmc)
        {
            return dal.AddList(list, dwbm, ajlbbm, ajlbmc, sslbbm, sslbmc);
        }

        public bool DeleteNodeAndChild(string dwbm, string ajlbbm, string sslbbm)
        {
            return dal.DeleteNodeAndChild(dwbm, ajlbbm, sslbbm);
        }
        #endregion  ExtensionMethod
    }
}

