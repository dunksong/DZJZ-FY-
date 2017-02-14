
using System;
using System.Data;
using System.Collections.Generic;
using EDRS.Common;
using EDRS.Model;
using EDRS.DALFactory;
using EDRS.IDAL;
using System.Text;

namespace EDRS.BLL
{
	/// <summary>
	/// 案件基本信息表
	/// </summary>
	public partial class TYYW_GG_AJJBXXKZ
	{
        private readonly ITYYW_GG_AJJBXXKZ dal = DataAccess.CreateTYYW_GG_AJJBXXKZ();
        public TYYW_GG_AJJBXXKZ(System.Web.HttpRequest context)
		{
            dal.SetHttpContext(context);
        }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string AJKZXH)
        {
            return dal.Exists(AJKZXH);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(EDRS.Model.TYYW_GG_AJJBXXKZ model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(EDRS.Model.TYYW_GG_AJJBXXKZ model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string AJKZXH)
        {

            return dal.Delete(AJKZXH);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string AJKZXHlist)
        {
            return dal.DeleteList(EDRS.Common.PageValidate.SafeLongFilter(AJKZXHlist, 0));
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public EDRS.Model.TYYW_GG_AJJBXXKZ GetModel(string AJKZXH)
        {

            return dal.GetModel(AJKZXH);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public EDRS.Model.TYYW_GG_AJJBXXKZ GetModelByCache(string AJKZXH)
        {

            string CacheKey = "TYYW_GG_AJJBXXKZModel-" + AJKZXH;
            object objModel = EDRS.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(AJKZXH);
                    if (objModel != null)
                    {
                        int ModelCache = EDRS.Common.ConfigHelper.GetConfigInt("ModelCache");
                        EDRS.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (EDRS.Model.TYYW_GG_AJJBXXKZ)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<EDRS.Model.TYYW_GG_AJJBXXKZ> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<EDRS.Model.TYYW_GG_AJJBXXKZ> DataTableToList(DataTable dt)
        {
            List<EDRS.Model.TYYW_GG_AJJBXXKZ> modelList = new List<EDRS.Model.TYYW_GG_AJJBXXKZ>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                EDRS.Model.TYYW_GG_AJJBXXKZ model;
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
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  BasicMethod
    }
}

