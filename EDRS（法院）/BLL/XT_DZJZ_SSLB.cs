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
	/// 所属类别
	/// </summary>
	public partial class XT_DZJZ_SSLB
	{
		private readonly IXT_DZJZ_SSLB dal=DataAccess.CreateXT_DZJZ_SSLB();
        public XT_DZJZ_SSLB(System.Web.HttpRequest _context)
		{}
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string SSLBBM)
		{
			return dal.Exists(SSLBBM);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EDRS.Model.XT_DZJZ_SSLB model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(EDRS.Model.XT_DZJZ_SSLB model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string SSLBBM)
		{
			
			return dal.Delete(SSLBBM);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string SSLBBMlist )
		{
			return dal.DeleteList(SSLBBMlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public EDRS.Model.XT_DZJZ_SSLB GetModel(string SSLBBM)
		{
			
			return dal.GetModel(SSLBBM);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public EDRS.Model.XT_DZJZ_SSLB GetModelByCache(string SSLBBM)
		{
			
			string CacheKey = "XT_DZJZ_SSLBModel-" + SSLBBM;
            object objModel = EDRS.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(SSLBBM);
					if (objModel != null)
					{
                        int ModelCache = EDRS.Common.ConfigHelper.GetConfigInt("ModelCache");
                        EDRS.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (EDRS.Model.XT_DZJZ_SSLB)objModel;
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
		public List<EDRS.Model.XT_DZJZ_SSLB> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<EDRS.Model.XT_DZJZ_SSLB> DataTableToList(DataTable dt)
		{
			List<EDRS.Model.XT_DZJZ_SSLB> modelList = new List<EDRS.Model.XT_DZJZ_SSLB>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				EDRS.Model.XT_DZJZ_SSLB model;
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
			return dal.GetRecordCount(strWhere);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)
		{
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}

        /// <summary>
        /// 获取最大顺序数
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="objValues"></param>
        /// <returns></returns>
        public object GetMaxSx(string strWhere, params object[] objValues)
        {
            return dal.GetMaxSx(strWhere, objValues);
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
		#endregion  ExtensionMethod
	}
}

