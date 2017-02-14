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
	/// 律师管理表
	/// </summary>
	public partial class YX_DZJZ_LSGL
	{
		private readonly IYX_DZJZ_LSGL dal=DataAccess.CreateYX_DZJZ_LSGL();
        public YX_DZJZ_LSGL(System.Web.HttpRequest _context)
        {
            dal.SetHttpContext(_context);
        }
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string LSZH)
		{
			return dal.Exists(LSZH);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EDRS.Model.YX_DZJZ_LSGL model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(EDRS.Model.YX_DZJZ_LSGL model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string LSZH)
		{
			
			return dal.Delete(LSZH);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string LSZHlist )
		{
			return dal.DeleteList(LSZHlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public EDRS.Model.YX_DZJZ_LSGL GetModel(string LSZH)
		{
			
			return dal.GetModel(LSZH);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public EDRS.Model.YX_DZJZ_LSGL GetModelByCache(string LSZH)
		{
			
			string CacheKey = "YX_DZJZ_LSGLModel-" + LSZH;
			object objModel = EDRS.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(LSZH);
					if (objModel != null)
					{
						int ModelCache = EDRS.Common.ConfigHelper.GetConfigInt("ModelCache");
						EDRS.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (EDRS.Model.YX_DZJZ_LSGL)objModel;
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
        public List<EDRS.Model.YX_DZJZ_LSGL> GetModelList(string strWhere, params object[] objValues)
		{
            DataSet ds = dal.GetList(strWhere, objValues);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<EDRS.Model.YX_DZJZ_LSGL> DataTableToList(DataTable dt)
		{
			List<EDRS.Model.YX_DZJZ_LSGL> modelList = new List<EDRS.Model.YX_DZJZ_LSGL>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				EDRS.Model.YX_DZJZ_LSGL model;
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

        
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPageEx(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)
        { 
            return dal.GetListByPageEx(strWhere, orderby, startIndex, endIndex, objValues);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListfile(string strWhere, params object[] objValues)
        {
            return dal.GetListfile(strWhere,objValues);
        }
		#endregion  BasicMethod
		#region  ExtensionMethod
        public bool UpdateZHXGSJ(string LSZH, DateTime zhxgsj)
        {
            return dal.UpdateZHXGSJ(LSZH, zhxgsj);
        }
		#endregion  ExtensionMethod
	}
}

