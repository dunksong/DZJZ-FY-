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
	/// 卷宗日志记录表
	/// </summary>
	public partial class YX_DZJZ_JZRZJL
	{
		private readonly IYX_DZJZ_JZRZJL dal=DataAccess.CreateYX_DZJZ_JZRZJL();
        public YX_DZJZ_JZRZJL(System.Web.HttpRequest _context)
        {
            dal.SetHttpContext(_context);
        }
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(decimal XH)
		{
			return dal.Exists(XH);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EDRS.Model.YX_DZJZ_JZRZJL model)
		{
			return dal.Add(model);
		}
        /// <summary>
		/// 增加一条数据
		/// </summary>
        public bool AddByModelList(List<EDRS.Model.YX_DZJZ_JZRZJL> modelList)
        {
            return dal.AddByModelList(modelList);
        }
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(EDRS.Model.YX_DZJZ_JZRZJL model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(decimal XH)
		{
			
			return dal.Delete(XH);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string XHlist )
		{
			return dal.DeleteList(XHlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public EDRS.Model.YX_DZJZ_JZRZJL GetModel(decimal XH)
		{
			
			return dal.GetModel(XH);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public EDRS.Model.YX_DZJZ_JZRZJL GetModelByCache(decimal XH)
		{
			
			string CacheKey = "YX_DZJZ_JZRZJLModel-" + XH;
			object objModel = EDRS.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(XH);
					if (objModel != null)
					{
						int ModelCache = EDRS.Common.ConfigHelper.GetConfigInt("ModelCache");
						EDRS.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (EDRS.Model.YX_DZJZ_JZRZJL)objModel;
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
        public List<EDRS.Model.YX_DZJZ_JZRZJL> GetModelList(string strWhere, params object[] objValues)
		{
            DataSet ds = dal.GetList(strWhere, objValues);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<EDRS.Model.YX_DZJZ_JZRZJL> DataTableToList(DataTable dt)
		{
			List<EDRS.Model.YX_DZJZ_JZRZJL> modelList = new List<EDRS.Model.YX_DZJZ_JZRZJL>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				EDRS.Model.YX_DZJZ_JZRZJL model;
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
        public DataSet GetListByPageProc(DateTime startTime, DateTime endTime, string strWhere, string orderby, int startIndex, int endIndex, ref int count)
        {
            return dal.GetListByPageProc(startTime, endTime,strWhere, orderby, startIndex, endIndex, ref count);
        }

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

