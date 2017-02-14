
using System;
using System.Data;
using System.Collections.Generic;
using EDRS.Common;
using EDRS.Model;
using EDRS.DALFactory;
using EDRS.IDAL;
using System.Web;
namespace EDRS.BLL
{
	/// <summary>
	/// 卷宗目录
	/// </summary>
	public partial class YX_DZJZ_JZML
	{
		private readonly IYX_DZJZ_JZML dal=DataAccess.CreateYX_DZJZ_JZML();
        public YX_DZJZ_JZML(System.Web.HttpRequest _context)
        {
            dal.SetHttpContext(_context);
        }
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string JZBH,string MLBH)
		{
			return dal.Exists(JZBH,MLBH);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EDRS.Model.YX_DZJZ_JZML model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(EDRS.Model.YX_DZJZ_JZML model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string JZBH,string MLBH)
		{
			
			return dal.Delete(JZBH,MLBH);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public EDRS.Model.YX_DZJZ_JZML GetModel(string JZBH,string MLBH)
		{
			
			return dal.GetModel(JZBH,MLBH);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public EDRS.Model.YX_DZJZ_JZML GetModelByCache(string JZBH,string MLBH)
		{
			
			string CacheKey = "YX_DZJZ_JZMLModel-" + JZBH+MLBH;
			object objModel = EDRS.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(JZBH,MLBH);
					if (objModel != null)
					{
						int ModelCache = EDRS.Common.ConfigHelper.GetConfigInt("ModelCache");
						EDRS.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (EDRS.Model.YX_DZJZ_JZML)objModel;
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
        public List<EDRS.Model.YX_DZJZ_JZML> GetModelList(string strWhere, params object[] objValues)
		{
			DataSet ds = dal.GetList(strWhere,objValues);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<EDRS.Model.YX_DZJZ_JZML> DataTableToList(DataTable dt)
		{
			List<EDRS.Model.YX_DZJZ_JZML> modelList = new List<EDRS.Model.YX_DZJZ_JZML>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				EDRS.Model.YX_DZJZ_JZML model;
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
        /// 获取树形案件目录列表
        /// </summary>
        /// <param name="strMlWhere">目录表查询条件</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="withWhere">循环条件</param>
        /// <param name="direction">查询顺序</param>
        /// <param name="yjxh">阅卷序号</param>
        /// <param name="isAll">是否只加载所有</param>
        /// <param name="objValues"></param>
        /// <returns></returns>
        public DataSet GetListByTree(string strMlWhere, string strWhere, string withWhere, bool direction, string yjxh, bool isAll, params object[] objValues)
        {
            return dal.GetListByTree(strMlWhere, strWhere, withWhere, direction,yjxh,isAll, objValues);
        }
        /// <summary>
        /// 获取树形案件目录列表
        /// </summary>
        /// <param name="strMlWhere">目录表查询条件</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="withWhere">循环条件</param>
        /// <param name="direction">查询顺序</param>
        /// <param name="yjxh">阅卷序号</param>
        /// <param name="isAll">是否只加载所有</param>
        /// <param name="objValues"></param>
        /// <returns></returns>
        public DataSet GetListByTreeEx(string strMlWhere, string strWhere, string withWhere, bool direction, string yjxh, bool isAll, params object[] objValues)
        {
            return dal.GetListByTreeEx(strMlWhere, strWhere, withWhere, direction, yjxh, isAll, objValues);
        }

        #region 获取案件目录
        
        /// <summary>
        /// 获取案件目录
        /// </summary>
        /// <param name="Request"></param>
        /// <param name="isAll">是否获取所有目录，false为只有已分配的</param>
        /// <param name="isChecked">是否默认选中</param>
        /// <returns></returns>
        public string GetMlTree(System.Web.HttpRequest Request, bool isAll, bool isChecked)
        {
            string bmsah = HttpUtility.UrlDecode(HttpUtility.UrlDecode(Request.Form["bmsah"]));
            string parneid = Request.Form["pid"];
            string ischecked = Request.Form["ischecked"];
            string yjxh = Request.Form["yjxh"] ?? "";
            string sfsm = Request.Form["wjtype"];

            string strMlWhere = " and SFSC='N'";
            string where = " and level < 3";
            string withWhere = " and FMLBH is null";
            object[] values = new object[2];
            if (!string.IsNullOrEmpty(bmsah))
            {
                strMlWhere += " and BMSAH=:BMSAH";
                values[0] = bmsah;
            }
            //判断存在父级编码
            if (!string.IsNullOrEmpty(parneid))
            {
                withWhere = " and FMLBH =:FMLBH";
                values[1] = parneid;
            }
            if (!string.IsNullOrEmpty(sfsm) && sfsm == "N")
            {
                withWhere += " and SFSM='N'";
            }

            EDRS.BLL.YX_DZJZ_JZML bll = new EDRS.BLL.YX_DZJZ_JZML(Request);
            DataSet ds = bll.GetListByTree(strMlWhere, where, withWhere, true, StringPlus.ReplaceSingle(yjxh), isAll, values);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0].Copy();
                dt.Columns["MLBH"].ColumnName = "ID";
                dt.Columns["FMLBH"].ColumnName = "PARENTID";
                dt.Columns["MLXSMC"].ColumnName = "NAME";
                if (isChecked)
                {
                    dt.Columns["ISEXIST"].ColumnName = "ischecked";
                }
                dt.Columns.Add("icon");
                foreach (DataRow dr in dt.Rows)
                {
                    switch (int.Parse(dr["MLLX"].ToString()))
                    {
                        case 1:
                            dr["icon"] = "jicon";
                            break;
                        case 2:
                            dr["icon"] = "mlicon";
                            break;
                        case 3:
                            dr["icon"] = "yicon";
                            break;
                        case 4:
                            dr["icon"] = "wjicon";
                            break;
                    }
                }
              
                Dictionary<string, string> styleValues = new Dictionary<string, string>();
                styleValues.Add("Y", "color:blue;");
                string json = new TreeJson(dt, "ID", "NAME", "PARENTID", "", "", parneid ?? "", true, true, "SFSM", styleValues, (ischecked == "true" ? true : false)).ResultJson.ToString();

                return json;

            }
            return ReturnString.JsonToString(Prompt.error, "未找到相关目录！", null);
        }

        /// <summary>
        /// 获取案件目录
        /// </summary>
        /// <param name="Request"></param>
        /// <param name="isAll">是否获取所有目录，false为只有已分配的</param>
        /// <param name="isChecked">是否默认选中</param>
        /// <returns></returns>
        public string GetMlTreeEx(System.Web.HttpRequest Request, bool isAll, bool isChecked)
        {
            string bmsah = HttpUtility.UrlDecode(HttpUtility.UrlDecode(Request.Form["bmsah"]));
            string parneid = Request["pid"];
            string ischecked = Request["ischecked"];
            string yjxh = Request["yjxh"] ?? "";
            string sfsm = Request.Form["wjtype"];

            string strMlWhere = " and SFSC='N'";
            string where = " and level < 3";
            string withWhere = " and FMLBH is null";
            object[] values = new object[2];
            if (!string.IsNullOrEmpty(bmsah))
            {
                strMlWhere += " and BMSAH=:BMSAH";
                values[0] = bmsah;
            }

            //判断存在父级编码
            if (!string.IsNullOrEmpty(parneid))
            {
                withWhere = " and FMLBH =:FMLBH";
                values[1] = parneid;
            }
            if (!string.IsNullOrEmpty(sfsm) && sfsm == "N")
            {
                withWhere += " and SFSM='N'";
            }
            EDRS.BLL.YX_DZJZ_JZML bll = new EDRS.BLL.YX_DZJZ_JZML(Request);
            DataSet ds = bll.GetListByTreeEx(strMlWhere, where, withWhere, true, StringPlus.ReplaceSingle(yjxh), isAll, values);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0].Copy();
                dt.Columns["MLBH"].ColumnName = "ID";
                dt.Columns["FMLBH"].ColumnName = "PARENTID";
                dt.Columns["MLXSMC"].ColumnName = "NAME";
                if (isChecked)
                {
                    dt.Columns["ISEXIST"].ColumnName = "ischecked";
                }
                dt.Columns.Add("icon");
                foreach (DataRow dr in dt.Rows)
                {
                    switch (int.Parse(dr["MLLX"].ToString()))
                    {
                        case 1:
                            dr["icon"] = "jicon";
                            break;
                        case 2:
                            dr["icon"] = "mlicon";
                            break;
                        case 3:
                            dr["icon"] = "yicon";
                            break;
                        case 4:
                            dr["icon"] = "wjicon";
                            break;
                    }
                    if (dr["wjxhjl"] != null && dr["wjxhjl"].ToString() != "")
                        dr["ischecked"] = 1;
                    else
                        dr["ischecked"] = DBNull.Value;
                }

                Dictionary<string, string> styleValues = new Dictionary<string, string>();
                styleValues.Add("Y", "color:blue;");
                string json = new TreeJson(dt, "ID", "NAME", "PARENTID", "", "", parneid ?? "", true, true, "SFSM", styleValues, (ischecked == "true" ? true : false)).ResultJson.ToString();

                return json;

            }
            return ReturnString.JsonToString(Prompt.error, "未找到相关目录！", null);
        }
        #endregion

        /// <summary>
        /// 获取树形案件卷和文件
        /// </summary>
        /// <param name="strMlWhere">目录表查询条件</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="withWhere">循环条件</param>
        /// <param name="direction">查询顺序</param>
        /// <param name="yjxh">阅卷序号</param>
        /// <param name="isAll">是否只加载所有</param>
        /// <param name="objValues"></param>
        /// <returns></returns>
        public DataSet GetListByTreeJaM(string strMlWhere, string strWhere, string withWhere, bool direction, string yjxh, bool isAll, params object[] objValues)
        {
            return dal.GetListByTreeJaM(strMlWhere, strWhere, withWhere, direction, yjxh, isAll, objValues);
        }

        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="strOrder"></param>
        /// <param name="objValues"></param>
        /// <returns></returns>
        public DataSet GetListByWjmc(string strWhere, string strOrder, params object[] objValues)
        {
            return dal.GetListByWjmc(strWhere, strOrder, objValues);
        }

        /// <summary>
        /// 卷宗文件获取
        /// </summary>
        /// <param name="DWBM">单位编码</param>
        /// <param name="BMSAH">部门受案号</param>
        /// <param name="JZBH">卷宗编号</param>
        /// <returns></returns>
        public DataSet GetDossierFileInfo(string DWBM, string BMSAH, string JZBH)
        {
            return dal.GetDossierFileInfo(DWBM, BMSAH, JZBH);
        }

        /// <summary>
        /// 卷宗信息获取
        /// </summary>
        /// <param name="BH">部门受案号</param>
        /// <param name="DWBM">单位编码</param>
        /// <returns></returns>
        public DataSet GetDossierInfo(string BH, string DWBM)
        {
            return dal.GetDossierInfo(BH, DWBM);
        }

        /// <summary>
        /// 卷宗页获取
        /// </summary>
        /// <param name="DWBM">单位编号</param>
        /// <param name="BH">部门受案号</param>
        /// <param name="JZBH">卷宗编号</param>
        /// <param name="JZWJBH">卷宗文件编号</param>
        public DataSet GetDossierFilePageInfo(string DWBM, string BH, string JZBH, string JZWJBH)
        {
            return dal.GetDossierFilePageInfo(DWBM, BH, JZBH, JZWJBH);
        }

		#endregion  ExtensionMethod
	}
}

