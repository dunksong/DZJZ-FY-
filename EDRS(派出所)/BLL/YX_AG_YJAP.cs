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
	/// 阅卷安排
	/// </summary>
	public partial class YX_AG_YJAP
	{
		private readonly IYX_AG_YJAP dal=DataAccess.CreateYX_AG_YJAP();
        public YX_AG_YJAP(System.Web.HttpRequest _context)
        {
            dal.SetHttpContext(_context);
        }
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(decimal YJBH)
		{
			return dal.Exists(YJBH);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EDRS.Model.YX_AG_YJAP model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(EDRS.Model.YX_AG_YJAP model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(decimal YJBH)
		{
			
			return dal.Delete(YJBH);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string YJBHlist )
		{
			return dal.DeleteList(YJBHlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public EDRS.Model.YX_AG_YJAP GetModel(decimal YJBH)
		{
			
			return dal.GetModel(YJBH);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public EDRS.Model.YX_AG_YJAP GetModelByCache(decimal YJBH)
		{
			
			string CacheKey = "YX_AG_YJAPModel-" + YJBH;
			object objModel = EDRS.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(YJBH);
					if (objModel != null)
					{
						int ModelCache = EDRS.Common.ConfigHelper.GetConfigInt("ModelCache");
						EDRS.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (EDRS.Model.YX_AG_YJAP)objModel;
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
        public List<EDRS.Model.YX_AG_YJAP> GetModelList(string strWhere, params object[] objValues)
		{
            DataSet ds = dal.GetList(strWhere, objValues);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<EDRS.Model.YX_AG_YJAP> DataTableToList(DataTable dt)
		{
			List<EDRS.Model.YX_AG_YJAP> modelList = new List<EDRS.Model.YX_AG_YJAP>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				EDRS.Model.YX_AG_YJAP model;
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

		#endregion  ExtensionMethod
	}
}

