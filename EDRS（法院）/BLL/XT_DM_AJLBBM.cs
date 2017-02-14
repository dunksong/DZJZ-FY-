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
	/// 扫描设备登记表
	/// </summary>
	public partial class XT_DM_AJLBBM
	{
		private readonly IXT_DM_AJLBBM dal=DataAccess.CreateXT_DM_AJLBBM();
		public XT_DM_AJLBBM(System.Web.HttpRequest _context)
        {
            dal.SetHttpContext(_context);
        }
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string AJLBBM)
		{
			return dal.Exists(AJLBBM);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EDRS.Model.XT_DM_AJLBBM model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(EDRS.Model.XT_DM_AJLBBM model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string AJLBBM)
		{
			
			return dal.Delete(AJLBBM);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public EDRS.Model.XT_DM_AJLBBM GetModel(string AJLBBM)
		{
			
			return dal.GetModel(AJLBBM);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public EDRS.Model.XT_DM_AJLBBM GetModelByCache(string AJLBBM)
		{

            string CacheKey = "XT_DM_AJLBBMModel-" + AJLBBM;
			object objModel = EDRS.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(AJLBBM);
					if (objModel != null)
					{
						int ModelCache = EDRS.Common.ConfigHelper.GetConfigInt("ModelCache");
						EDRS.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (EDRS.Model.XT_DM_AJLBBM)objModel;
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
        public List<EDRS.Model.XT_DM_AJLBBM> GetModelList(string strWhere, params object[] objValues)
		{
            DataSet ds = dal.GetList(strWhere, objValues);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<EDRS.Model.XT_DM_AJLBBM> DataTableToList(DataTable dt)
		{
			List<EDRS.Model.XT_DM_AJLBBM> modelList = new List<EDRS.Model.XT_DM_AJLBBM>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				EDRS.Model.XT_DM_AJLBBM model;
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
        public DataSet GetSSLBList(string strWhere, params object[] objValues)
        {
            return dal.GetSSLBList(strWhere, objValues);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(EDRS.Model.XT_DM_AJLBBM model, string ajlbbm)
        {
            return dal.Update(model, ajlbbm);
        }

        /// <summary>
        /// 删除多条数据
        /// </summary>
        public bool DeleteList(string ajlbbmList)
        {
            return dal.DeleteList(ajlbbmList);
        }
		#endregion  ExtensionMethod
	}
}

