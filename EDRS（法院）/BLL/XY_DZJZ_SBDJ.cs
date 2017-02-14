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
	public partial class XY_DZJZ_SBDJ
	{
		private readonly IXY_DZJZ_SBDJ dal=DataAccess.CreateXY_DZJZ_SBDJ();
        public XY_DZJZ_SBDJ(System.Web.HttpRequest _context)
        {
            dal.SetHttpContext(_context);
        }
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string MAC,string DEVSN)
		{
			return dal.Exists(MAC,DEVSN);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EDRS.Model.XY_DZJZ_SBDJ model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(EDRS.Model.XY_DZJZ_SBDJ model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string MAC,string DEVSN)
		{
			
			return dal.Delete(MAC,DEVSN);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public EDRS.Model.XY_DZJZ_SBDJ GetModel(string MAC,string DEVSN)
		{
			
			return dal.GetModel(MAC,DEVSN);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public EDRS.Model.XY_DZJZ_SBDJ GetModelByCache(string MAC,string DEVSN)
		{
			
			string CacheKey = "XY_DZJZ_SBDJModel-" + MAC+DEVSN;
			object objModel = EDRS.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(MAC,DEVSN);
					if (objModel != null)
					{
						int ModelCache = EDRS.Common.ConfigHelper.GetConfigInt("ModelCache");
						EDRS.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (EDRS.Model.XY_DZJZ_SBDJ)objModel;
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
        public List<EDRS.Model.XY_DZJZ_SBDJ> GetModelList(string strWhere, params object[] objValues)
		{
            DataSet ds = dal.GetList(strWhere, objValues);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<EDRS.Model.XY_DZJZ_SBDJ> DataTableToList(DataTable dt)
		{
			List<EDRS.Model.XY_DZJZ_SBDJ> modelList = new List<EDRS.Model.XY_DZJZ_SBDJ>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				EDRS.Model.XY_DZJZ_SBDJ model;
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
        public int GetRecordCount(string dwbm,string jsbm, string strWhere, params object[] objValues)
        {
            return dal.GetRecordCount(dwbm,jsbm, strWhere, objValues);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string dwbm, string jsbm, string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)
        {
            return dal.GetListByPage(dwbm, jsbm, strWhere, orderby, startIndex, endIndex, objValues);
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

		#endregion  ExtensionMethod
	}
}

