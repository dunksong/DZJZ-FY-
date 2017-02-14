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
	/// 系统配置表
	/// </summary>
	public partial class XY_DZJZ_XTPZ
	{
		private readonly IXY_DZJZ_XTPZ dal=DataAccess.CreateXY_DZJZ_XTPZ();
        public XY_DZJZ_XTPZ(System.Web.HttpRequest _context)
        {
            dal.SetHttpContext(_context);
        }
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string PZBM)
		{
			return dal.Exists(PZBM);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EDRS.Model.XY_DZJZ_XTPZ model)
		{
			return dal.Add(model);
		}

        /// <summary>
        /// 增加多条数据
        /// </summary>
        public bool AddList(List<EDRS.Model.XY_DZJZ_XTPZ> modelList)
        {
            return dal.AddList(modelList);
        }

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(EDRS.Model.XY_DZJZ_XTPZ model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string PZBM)
		{
			
			return dal.Delete(PZBM);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string PZBMlist )
		{
			return dal.DeleteList(PZBMlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public EDRS.Model.XY_DZJZ_XTPZ GetModel(string PZBM)
		{
			
			return dal.GetModel(PZBM);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public EDRS.Model.XY_DZJZ_XTPZ GetModelByCache(string PZBM)
		{
			
			string CacheKey = "XY_DZJZ_XTPZModel-" + PZBM;
			object objModel = EDRS.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(PZBM);
					if (objModel != null)
					{
						int ModelCache = EDRS.Common.ConfigHelper.GetConfigInt("ModelCache");
						EDRS.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (EDRS.Model.XY_DZJZ_XTPZ)objModel;
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
        public List<EDRS.Model.XY_DZJZ_XTPZ> GetModelList(string strWhere, params object[] objValues)
		{
            DataSet ds = dal.GetList(strWhere, objValues);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<EDRS.Model.XY_DZJZ_XTPZ> DataTableToList(DataTable dt)
		{
			List<EDRS.Model.XY_DZJZ_XTPZ> modelList = new List<EDRS.Model.XY_DZJZ_XTPZ>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				EDRS.Model.XY_DZJZ_XTPZ model;
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
        public EDRS.Model.XY_DZJZ_XTPZ GetModel(int configID)
        {
            return dal.GetModel(configID);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public  DataSet GetAllList(string dwbm,string gh)
        {
            string CacheKey = "XY_DZJZ_XTPZModelList-" + dwbm+gh;
            object objModel = EDRS.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetList("");
                    if (objModel != null)
                    {
                        int ModelCache = EDRS.Common.ConfigHelper.GetConfigInt("ModelCache");
                        EDRS.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (DataSet)objModel;
        }

		#endregion  ExtensionMethod
	}
}

