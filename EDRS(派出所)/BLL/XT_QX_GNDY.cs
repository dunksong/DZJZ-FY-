using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using EDRS.Common;
using EDRS.Model;
using EDRS.DALFactory;
using EDRS.IDAL;
namespace EDRS.BLL
{
	/// <summary>
	/// 功能定义表
	/// </summary>
	public partial class XT_QX_GNDY
	{
		private readonly IXT_QX_GNDY dal=DataAccess.CreateXT_QX_GNDY();
        private System.Web.HttpRequest context = null;
        public XT_QX_GNDY(System.Web.HttpRequest _context)
        {
            this.context = _context;
            dal.SetHttpContext(_context);
        }
		#region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string GNBM, string DWBM)
        {
            return dal.Exists(GNBM, DWBM);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(EDRS.Model.XT_QX_GNDY model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(EDRS.Model.XT_QX_GNDY model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// 不是物理删除，标记删除
        /// 
        /// 删除时需要先删除该功能已经分配的权限（人员功能分配和角色功能分配）
        /// </summary>
        public bool Delete(string GNBM, string DWBM)
        {
            
            //删除角色功能分配表数据
            var mode = GetModel(GNBM, DWBM);
            mode.SFSC = "Y";
            
            
            var bllrygnfp = new BLL.XT_QX_RYGNFP(this.context);
            var blljsgnfp = new BLL.XT_QX_JSGNFP(this.context);
            //获取功能的功能分配集合
            var modellist = GetListByRygnfps(GNBM, DWBM);
            //删除人员功能分配表数据
            foreach (var rygnfp in modellist)
            {
                bllrygnfp.Delete(DWBM, rygnfp.GH, GNBM);
            }
            //获取角色分配集合
            var jsmodellist = GetListByJsgnfps(GNBM, DWBM);
            //删除角色分配集合
            foreach (var jsgnfp in jsmodellist)
            {
                blljsgnfp.Delete(DWBM, jsgnfp.JSBM, GNBM);
            }

            return dal.Update(mode);
        }
        /// <summary>
        /// 获取功能的人员分配集合
        /// </summary>
        /// <param name="gnbm">功能编码</param>
        /// <param name="dwbm">单位编码</param>
        /// <returns>分配的权限集合</returns>
	    private static IEnumerable<Model.XT_QX_RYGNFP> GetListByRygnfps(string gnbm,string dwbm)
	    {
            var bllrygnfp = new BLL.XT_QX_RYGNFP(null);
	        var sbwhere = new StringBuilder();
	        var objectValues = new Object[2];
	        sbwhere.Append(" and GNBM=:GNBM ");
	        objectValues[0] = gnbm;
	        sbwhere.Append(" and DWBM=:DWBM ");
	        objectValues[1] = dwbm;
	        return bllrygnfp.GetModelList(sbwhere.ToString(), objectValues);
	    }

        /// <summary>
        /// 获取功能的角色分配集合
        /// </summary>
        /// <param name="gnbm">功能编码</param>
        /// <param name="dwbm">单位编码</param>
        /// <returns>返回集合</returns>
	    private static IEnumerable<Model.XT_QX_JSGNFP> GetListByJsgnfps(string gnbm, string dwbm)
        {
            var blljsgnfp = new BLL.XT_QX_JSGNFP(null);
            var sbwhere = new StringBuilder();
            var objectValues = new object[2];
            sbwhere.Append(" and GNBM =:GNBM ");
            objectValues[0] = gnbm;
            sbwhere.Append(" and DWBM =:DWBM ");
            objectValues[1] = dwbm;
            return blljsgnfp.GetModelList(sbwhere.ToString(), objectValues);
        }

	    /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public EDRS.Model.XT_QX_GNDY GetModel(string GNBM, string DWBM)
        {

            return dal.GetModel(GNBM, DWBM);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public EDRS.Model.XT_QX_GNDY GetModelByCache(string GNBM, string DWBM)
        {

            string CacheKey = "XT_QX_GNDYModel-" + GNBM + DWBM;
            object objModel = EDRS.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(GNBM, DWBM);
                    if (objModel != null)
                    {
                        int ModelCache = EDRS.Common.ConfigHelper.GetConfigInt("ModelCache");
                        EDRS.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (EDRS.Model.XT_QX_GNDY)objModel;
        }


		/// <summary>
		/// 获得数据列表
		/// </summary>
        public DataSet GetList(string strWhere, params object[] objValues)
		{
			return dal.GetList(strWhere,objValues);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
        public List<EDRS.Model.XT_QX_GNDY> GetModelList(string strWhere, params object[] objValues)
		{
			DataSet ds = dal.GetList(strWhere,objValues);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<EDRS.Model.XT_QX_GNDY> DataTableToList(DataTable dt)
		{
			List<EDRS.Model.XT_QX_GNDY> modelList = new List<EDRS.Model.XT_QX_GNDY>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				EDRS.Model.XT_QX_GNDY model;
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
			return dal.GetRecordCount(strWhere,objValues);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)
		{
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex,objValues);
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
        /// 获取功能菜单
        /// </summary>
        public DataSet GetListByType(string dwbm, string strWhere, params object[] objValues)
        {
            return dal.GetListByType(dwbm,strWhere, objValues);
        }
        /// <summary>
        /// 获取角色对应功能
        /// </summary>
        public DataSet GetListByBound(string dwbm,string gh, string strWhere, params object[] objValues)
        {
            object objModel=null;
            string CacheKey = "IXT_QX_GNDYList-" +dwbm + gh;

            objModel = EDRS.Common.DataCache.GetHttpCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetListByBound(dwbm, gh, strWhere, objValues);
                    if (objModel != null)
                    {
                        int ModelCache = EDRS.Common.ConfigHelper.GetConfigInt("ModelCache");
                        EDRS.Common.DataCache.SetHttpCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (DataSet)objModel;
        }
		#endregion  ExtensionMethod
	}
}

