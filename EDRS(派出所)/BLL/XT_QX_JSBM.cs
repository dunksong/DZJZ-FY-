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
	/// 角色编码
	/// </summary>
	public partial class XT_QX_JSBM
	{
		private readonly IXT_QX_JSBM dal=DataAccess.CreateXT_QX_JSBM();
        public XT_QX_JSBM(System.Web.HttpRequest _context)
        {
            dal.SetHttpContext(_context);
        }
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
        public bool Exists(string JSBM, string DWBM, string BMBM)
		{
            return dal.Exists(JSBM, DWBM, BMBM);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EDRS.Model.XT_QX_JSBM model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(EDRS.Model.XT_QX_JSBM model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
        public bool Delete(string JSBM, string DWBM, string BMBM)
		{

            return dal.Delete(JSBM, DWBM, BMBM);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public EDRS.Model.XT_QX_JSBM GetModel(string JSBM, string DWBM, string BMBM)
		{

            return dal.GetModel(JSBM, DWBM, BMBM);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
        public EDRS.Model.XT_QX_JSBM GetModelByCache(string JSBM, string DWBM, string BMBM)
		{

            string CacheKey = "XT_QX_JSBMModel-" + JSBM + DWBM + BMBM;
			object objModel = EDRS.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
                    objModel = dal.GetModel(JSBM, DWBM, BMBM);
					if (objModel != null)
					{
						int ModelCache = EDRS.Common.ConfigHelper.GetConfigInt("ModelCache");
						EDRS.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (EDRS.Model.XT_QX_JSBM)objModel;
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
        public List<EDRS.Model.XT_QX_JSBM> GetModelList(string strWhere, params object[] objValues)
		{
            DataSet ds = dal.GetList(strWhere, objValues);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<EDRS.Model.XT_QX_JSBM> DataTableToList(DataTable dt)
		{
			List<EDRS.Model.XT_QX_JSBM> modelList = new List<EDRS.Model.XT_QX_JSBM>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				EDRS.Model.XT_QX_JSBM model;
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

        /// <summary>
		/// 分页获取数据列表
		/// </summary>
        public DataSet GetListByPageAlly(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)
        {
            return dal.GetListByPageAlly(strWhere, orderby, startIndex, endIndex, objValues);
        }


        /// <summary>
        /// 获取最大编号对象
        /// </summary>
        public EDRS.Model.XT_QX_JSBM GetListOrderModel(string strWhere, string order, params object[] objValues)
        {
            DataSet ds = dal.GetListOrBCount(strWhere, order, objValues);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                List<EDRS.Model.XT_QX_JSBM> modelList = DataTableToList(ds.Tables[0]);
                if (modelList != null && modelList.Count > 0)
                    return modelList[0];
            }
            return null;
        }
		#endregion  BasicMethod
        /// <summary>
        /// 根据单位编码和工号获取角色
        /// </summary>
        /// <param name="dwbm"></param>
        /// <param name="gh"></param>
        /// <returns></returns>
        public DataSet GetJSBMbyUser(string dwbm, string gh)
        {
            return dal.GetJSBMbyUser(dwbm, gh);
        }
	}
}

