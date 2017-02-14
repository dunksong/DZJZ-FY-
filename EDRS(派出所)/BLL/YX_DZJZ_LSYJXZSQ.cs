using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using EDRS.Model;
using EDRS.DALFactory;
using EDRS.IDAL;
namespace EDRS.BLL
{
	/// <summary>
	/// 业务逻辑类YX_DZJZ_LSYJXZSQ 的摘要说明。
	/// </summary>
	public class YX_DZJZ_LSYJXZSQ
	{
		private readonly IYX_DZJZ_LSYJXZSQ dal=DataAccess.CreateYX_DZJZ_LSYJXZSQ();
		public YX_DZJZ_LSYJXZSQ()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string SQBM)
		{
			return dal.Exists(SQBM);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EDRS.Model.YX_DZJZ_LSYJXZSQ model)
		{
			return dal.Add(model);
		}
		/// <summary>
		/// 增加多条数据
		/// </summary>
		public bool AddList(System.Collections.Generic.List<EDRS.Model.YX_DZJZ_LSYJXZSQ> models)
		{
			return dal.AddList(models);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(EDRS.Model.YX_DZJZ_LSYJXZSQ model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string SQBM)
		{
			
			return dal.Delete(SQBM);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string SQBMlist)
		{
			
			return dal.DeleteList(SQBMlist);
		}
        /// <summary>
        /// 删除多条数据
        /// </summary>
        public bool DeleteListBySFSC(string SQBMlist)
        {
            return dal.DeleteListBySFSC(SQBMlist);
        }
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public EDRS.Model.YX_DZJZ_LSYJXZSQ GetModel(string SQBM)
		{
			
			return dal.GetModel(SQBM);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public EDRS.Model.YX_DZJZ_LSYJXZSQ GetModelByCache(string SQBM)
		{
			
			string CacheKey = "YX_DZJZ_LSYJXZSQModel-" + SQBM;
            object objModel = EDRS.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(SQBM);
					if (objModel != null)
					{
                        int ModelCache = EDRS.Common.ConfigHelper.GetConfigInt("ModelCache");
                        EDRS.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (EDRS.Model.YX_DZJZ_LSYJXZSQ)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere,params object[] objValues)
		{
			return dal.GetList(strWhere,objValues);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<EDRS.Model.YX_DZJZ_LSYJXZSQ> GetModelList(string strWhere,params object[] objValues)
		{
			DataSet ds = dal.GetList(strWhere,objValues);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<EDRS.Model.YX_DZJZ_LSYJXZSQ> DataTableToList(DataTable dt)
		{
			List<EDRS.Model.YX_DZJZ_LSYJXZSQ> modelList = new List<EDRS.Model.YX_DZJZ_LSYJXZSQ>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				EDRS.Model.YX_DZJZ_LSYJXZSQ model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new EDRS.Model.YX_DZJZ_LSYJXZSQ();
					model.SQBM=dt.Rows[n]["SQBM"].ToString();
					model.LSZH=dt.Rows[n]["LSZH"].ToString();
					model.LSXM=dt.Rows[n]["LSXM"].ToString();
					model.SSDW=dt.Rows[n]["SSDW"].ToString();
					model.AJMC=dt.Rows[n]["AJMC"].ToString();
					model.XYRMC=dt.Rows[n]["XYRMC"].ToString();
					model.SQRMC=dt.Rows[n]["SQRMC"].ToString();
					model.SQDW=dt.Rows[n]["SQDW"].ToString();
					model.XZDZ=dt.Rows[n]["XZDZ"].ToString();
					model.SQDZT=dt.Rows[n]["SQDZT"].ToString();
					if(dt.Rows[n]["SQSJ"].ToString()!="")
					{
						model.SQSJ=DateTime.Parse(dt.Rows[n]["SQSJ"].ToString());
					}
					model.BZ=dt.Rows[n]["BZ"].ToString();
					model.SFSC=dt.Rows[n]["SFSC"].ToString();
					model.SHRGH=dt.Rows[n]["SHRGH"].ToString();
					model.SHR=dt.Rows[n]["SHR"].ToString();
					if(dt.Rows[n]["SHSJ"].ToString()!="")
					{
						model.SHSJ=DateTime.Parse(dt.Rows[n]["SHSJ"].ToString());
					}
					modelList.Add(model);
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
		/// 获得数据列表
		/// </summary>
		public int GetRecordCount(string strWhere,params object[] objValues)
		{
			return dal.GetRecordCount(strWhere,objValues);
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex,params object[] objValues)
		{
			return dal.GetListByPage(strWhere, orderby, startIndex, endIndex,objValues);
		}

		#endregion  成员方法
	}
}

