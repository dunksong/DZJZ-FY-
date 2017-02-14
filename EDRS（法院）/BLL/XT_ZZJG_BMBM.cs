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
	/// 部门编码
	/// </summary>
	public partial class XT_ZZJG_BMBM
	{
		private readonly IXT_ZZJG_BMBM dal=DataAccess.CreateXT_ZZJG_BMBM();
        public XT_ZZJG_BMBM(System.Web.HttpRequest _context)
        {
            dal.SetHttpContext(_context);
        }
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string BMBM)
		{
			return dal.Exists(BMBM);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EDRS.Model.XT_ZZJG_BMBM model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(EDRS.Model.XT_ZZJG_BMBM model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string BMBM)
		{
			
			return dal.Delete(BMBM);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string BMBMlist )
		{
			return dal.DeleteList(BMBMlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public EDRS.Model.XT_ZZJG_BMBM GetModel(string DWBM,string BMBM)
		{

            return dal.GetModel(DWBM,BMBM);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public EDRS.Model.XT_ZZJG_BMBM GetModelByCache(string DWBM,string BMBM)
		{
			
			string CacheKey = "XT_ZZJG_BMBMModel-" + BMBM;
			object objModel = EDRS.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
                    objModel = dal.GetModel(DWBM,BMBM);
					if (objModel != null)
					{
						int ModelCache = EDRS.Common.ConfigHelper.GetConfigInt("ModelCache");
						EDRS.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (EDRS.Model.XT_ZZJG_BMBM)objModel;
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
        public List<EDRS.Model.XT_ZZJG_BMBM> GetModelList(string strWhere, params object[] objValues)
		{
            DataSet ds = dal.GetList(strWhere, objValues);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<EDRS.Model.XT_ZZJG_BMBM> DataTableToList(DataTable dt)
		{
			List<EDRS.Model.XT_ZZJG_BMBM> modelList = new List<EDRS.Model.XT_ZZJG_BMBM>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				EDRS.Model.XT_ZZJG_BMBM model;
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
        /// 批量逻辑删除数据
        /// </summary>
        public bool DeleteListLogic(string BMBMlist, string DWBMlist)
        {
            return dal.DeleteListLogic(BMBMlist, DWBMlist);
        }
       
        /// <summary>
        /// 获取树形结构当前节点所有父节点
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="objValues"></param>
        /// <returns></returns>
        public DataSet GetTreeList(string strWhere, string withWhere, params object[] objValues)
        {
            return dal.GetTreeList(strWhere, withWhere, objValues);
        }

        /// <summary>
        /// 获取单位部门视图
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="objValues"></param>
        /// <returns></returns>
        public DataSet GetOrganization(string strWhere, params object[] objValues)
        {
            return dal.GetOrganization(strWhere, objValues);
        }

        /// <summary>
        /// 获取最大编号对象
        /// </summary>
        public EDRS.Model.XT_ZZJG_BMBM GetListOrderModel(string strWhere, string order, params object[] objValues)
        {
            DataSet ds = dal.GetListOrBCount(strWhere, order, objValues);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                List<EDRS.Model.XT_ZZJG_BMBM> modelList = DataTableToList(ds.Tables[0]);
                if (modelList != null && modelList.Count > 0)
                    return modelList[0];
            }
            return null;
        }
        /// <summary>
        /// 根据父级编号获取1级子集
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="objValues"></param>
        /// <returns></returns>
        public DataSet GetTreeChildren(string strWhere, params object[] objValues)
        {
            return dal.GetTreeChildren(strWhere, objValues);
        }

        /// <summary>
        /// 获取部门编码的使用数量
        /// </summary>
        /// <param name="dwbm"></param>
        /// <returns></returns>
        public int GetBmbmCount(string dwbm, string bmbm)
        {
            return dal.GetBmbmCount(dwbm, bmbm);
        }
		#endregion  ExtensionMethod
	}
}

