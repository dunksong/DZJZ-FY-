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
	/// 角色功能授权表
	/// </summary>
	public partial class XT_QX_JSGNFP
	{
		private readonly IXT_QX_JSGNFP dal=DataAccess.CreateXT_QX_JSGNFP();
        public XT_QX_JSGNFP(System.Web.HttpRequest _context)
        {
            dal.SetHttpContext(_context);
        }
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string DWBM,string JSBM,string GNBM)
		{
			return dal.Exists(DWBM,JSBM,GNBM);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EDRS.Model.XT_QX_JSGNFP model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(EDRS.Model.XT_QX_JSGNFP model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string DWBM,string JSBM,string GNBM)
		{
			
			return dal.Delete(DWBM,JSBM,GNBM);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public EDRS.Model.XT_QX_JSGNFP GetModel(string DWBM,string JSBM,string GNBM)
		{
			
			return dal.GetModel(DWBM,JSBM,GNBM);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public EDRS.Model.XT_QX_JSGNFP GetModelByCache(string DWBM,string JSBM,string GNBM)
		{
			
			string CacheKey = "XT_QX_JSGNFPModel-" + DWBM+JSBM+GNBM;
			object objModel = EDRS.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(DWBM,JSBM,GNBM);
					if (objModel != null)
					{
						int ModelCache = EDRS.Common.ConfigHelper.GetConfigInt("ModelCache");
						EDRS.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (EDRS.Model.XT_QX_JSGNFP)objModel;
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
        public List<EDRS.Model.XT_QX_JSGNFP> GetModelList(string strWhere, params object[] objValues)
		{
            DataSet ds = dal.GetList(strWhere, objValues);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<EDRS.Model.XT_QX_JSGNFP> DataTableToList(DataTable dt)
		{
			List<EDRS.Model.XT_QX_JSGNFP> modelList = new List<EDRS.Model.XT_QX_JSGNFP>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				EDRS.Model.XT_QX_JSGNFP model;
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
			return GetList("",null);
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
        /// 根据单位编码和工号查询角色功能
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="objValues"></param>
        /// <returns></returns>
        public DataSet GetJSGNFPByGh(string dwbm,string gh, string strWhere, params object[] objValues)
        {
            string CacheKey = "XT_QX_JSGNFPList-" + dwbm + gh;
            object objModel = EDRS.Common.DataCache.GetHttpCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetJSGNFPByGh(strWhere, objValues);
                    if (objModel != null)
                    {
                        int ModelCache = EDRS.Common.ConfigHelper.GetConfigInt("ModelCache");
                        EDRS.Common.DataCache.SetHttpCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (DataSet)objModel;
        }
		#endregion  ExtensionMethod
	}
}

