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
	/// 单位编码
	/// </summary>
	public partial class XT_ZZJG_DWBM
	{
		private readonly IXT_ZZJG_DWBM dal=DataAccess.CreateXT_ZZJG_DWBM();
        public XT_ZZJG_DWBM(System.Web.HttpRequest _context)
        {
            dal.SetHttpContext(_context);
        }
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string DWBM)
		{
			return dal.Exists(DWBM);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EDRS.Model.XT_ZZJG_DWBM model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(EDRS.Model.XT_ZZJG_DWBM model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string DWBM)
		{
			
			return dal.Delete(DWBM);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string DWBMlist )
		{
			return dal.DeleteList(DWBMlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public EDRS.Model.XT_ZZJG_DWBM GetModel(string DWBM)
		{
			
			return dal.GetModel(DWBM);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public EDRS.Model.XT_ZZJG_DWBM GetModelByCache(string DWBM)
		{
			
			string CacheKey = "XT_ZZJG_DWBMModel-" + DWBM;
			object objModel = EDRS.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(DWBM);
					if (objModel != null)
					{
						int ModelCache = EDRS.Common.ConfigHelper.GetConfigInt("ModelCache");
						EDRS.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (EDRS.Model.XT_ZZJG_DWBM)objModel;
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
        public List<EDRS.Model.XT_ZZJG_DWBM> GetModelList(string strWhere, params object[] objValues)
		{
            DataSet ds = dal.GetList(strWhere, objValues);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<EDRS.Model.XT_ZZJG_DWBM> DataTableToList(DataTable dt)
		{
			List<EDRS.Model.XT_ZZJG_DWBM> modelList = new List<EDRS.Model.XT_ZZJG_DWBM>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				EDRS.Model.XT_ZZJG_DWBM model;
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
        /// 批量逻辑删除数据
        /// </summary>
        public bool DeleteListLogic(string DWBMlist)
        {
            return dal.DeleteListLogic(DWBMlist);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListOrBCount(string strWhere, params object[] objValues)
        {
            return dal.GetListOrBCount(strWhere, objValues);
        }
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

        public DataSet GetTreeListById(string strWhere, string withWhere, string siftWhere, bool direction, params object[] objValues)
        {
            return dal.GetTreeListById(strWhere, withWhere, siftWhere, direction, objValues);
        }

        /// <summary>
        /// 根据单位编码和工号获取对应有权限的单位编码
        /// </summary>
        /// <param name="dwbm"></param>
        /// <param name="gh"></param>
        /// <returns></returns>
        public DataSet GetDwbmListByGh(string dwbm, string gh)
        {
            return dal.GetDwbmListByGh(dwbm, gh);
        }

        /// <summary>
        /// 获取权限相关部门编码
        /// </summary>
        /// <returns></returns>
        public string GetDwbm(string dwbm, string gh)
        {
            DataSet ds = GetDwbmListByGh(dwbm, gh);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                dt.Columns["DWBM"].ColumnName = "id";
                dt.Columns["DWMC"].ColumnName = "text";
                return JsonHelper.JsonString(dt);
            }
            return ReturnString.JsonToString(Prompt.error, "未找到相关部门编码", null);
        }

        /// <summary>
        /// 获取单位编码的使用数量
        /// </summary>
        /// <param name="dwbm"></param>
        /// <returns></returns>
        public int GetDwbmCount(string dwbm)
        {
            return dal.GetDwbmCount(dwbm);
        }
	}
}

