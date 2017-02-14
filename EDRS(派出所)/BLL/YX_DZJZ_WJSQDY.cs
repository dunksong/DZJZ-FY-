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
	/// 卷宗文件申请打印表(一个案件一个律师当前阅卷
	/// </summary>
	public partial class YX_DZJZ_WJSQDY
	{
		private readonly IYX_DZJZ_WJSQDY dal=DataAccess.CreateYX_DZJZ_WJSQDY();
        public YX_DZJZ_WJSQDY(System.Web.HttpRequest _context)
        {
            dal.SetHttpContext(_context);
        }
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string XH)
		{
			return dal.Exists(XH);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EDRS.Model.YX_DZJZ_WJSQDY model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(EDRS.Model.YX_DZJZ_WJSQDY model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string XH)
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
		public EDRS.Model.YX_DZJZ_WJSQDY GetModel(string XH)
		{
			
			return dal.GetModel(XH);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public EDRS.Model.YX_DZJZ_WJSQDY GetModelByCache(string XH)
		{
			
			string CacheKey = "YX_DZJZ_WJSQDYModel-" + XH;
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
			return (EDRS.Model.YX_DZJZ_WJSQDY)objModel;
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
        public List<EDRS.Model.YX_DZJZ_WJSQDY> GetModelList(string strWhere, params object[] objValues)
		{
            DataSet ds = dal.GetList(strWhere, objValues);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<EDRS.Model.YX_DZJZ_WJSQDY> DataTableToList(DataTable dt)
		{
			List<EDRS.Model.YX_DZJZ_WJSQDY> modelList = new List<EDRS.Model.YX_DZJZ_WJSQDY>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				EDRS.Model.YX_DZJZ_WJSQDY model;
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
        public DataSet GetListByPageEx(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)
        {
            return dal.GetListByPageEx(strWhere, orderby, startIndex, endIndex, objValues);
        }
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}
        /// <summary>
        /// 增加多条数据
        /// </summary>
        public bool AddList(System.Collections.Generic.List<EDRS.Model.YX_DZJZ_WJSQDY> models)
        {
            return dal.AddList(models);
        }
         /// <summary>
        /// 判断申请是否审核
        /// </summary>
        /// <param name="yjxh"></param>
        /// <returns></returns>
        public DataSet GetListIsSH(string yjxh)
        {
            return dal.GetListIsSH(yjxh);
        }
        /// <summary>
        /// 添加打印申请记录
        /// </summary>
        /// <param name="sqdyjlList"></param>
        /// <param name="sqdyModel"></param>
        /// <param name="xh"></param>
        /// <returns></returns>
        public bool AddListJL(List<EDRS.Model.YX_DZJZ_WJSQDYJL> sqdyjlList, EDRS.Model.YX_DZJZ_WJSQDY sqdyModel, string xh)
        {
            return dal.AddListJL(sqdyjlList,sqdyModel,xh);
        }

        /// <summary>
        /// 修改文件打印申请记录
        /// </summary>
        /// <param name="sqdyjlList"></param>
        /// <param name="sqdyModel"></param>
        /// <param name="xh"></param>
        /// <returns></returns>
        public bool UpdateListJl(List<EDRS.Model.YX_DZJZ_WJSQDYJL> sqdyjlList)
        {
            return dal.UpdateListJl(sqdyjlList);
        }
         /// <summary>
        /// 根据阅卷序号得到一个对象实体
        /// </summary>
        public EDRS.Model.YX_DZJZ_WJSQDY GetModelByYJXH(string yjxh)
        {
            return dal.GetModelByYJXH(yjxh);
        }
         /// <summary>
        /// 根据序号获得打印记录列表
        /// </summary>
        public DataSet GetListDYJL(string strWhere, params object[] objValues)
        {
            return dal.GetListDYJL(strWhere,objValues);
        }
		#endregion  BasicMethod
		
	}
}

