
using System;
using System.Data;
using System.Collections.Generic;
using EDRS.Common;
using EDRS.Model;
using EDRS.DALFactory;
using EDRS.IDAL;
using System.Text;
using ZC57s.CaseInfoServ.DigitalDossier.ICInterface;
namespace EDRS.BLL
{
	/// <summary>
	/// 案件基本信息表
	/// </summary>
	public partial class TYYW_GG_AJJBXX
	{
		private readonly ITYYW_GG_AJJBXX dal=DataAccess.CreateTYYW_GG_AJJBXX();
        public TYYW_GG_AJJBXX(System.Web.HttpRequest context)
		{
            dal.SetHttpContext(context);
        }
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string BMSAH)
		{
			return dal.Exists(BMSAH);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(EDRS.Model.TYYW_GG_AJJBXX model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(EDRS.Model.TYYW_GG_AJJBXX model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string BMSAH)
		{
			
			return dal.Delete(BMSAH);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string BMSAHlist )
		{
			return dal.DeleteList(BMSAHlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public EDRS.Model.TYYW_GG_AJJBXX GetModel(string BMSAH)
		{
			
			return dal.GetModel(BMSAH);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public EDRS.Model.TYYW_GG_AJJBXX GetModelByCache(string BMSAH)
		{
			
			string CacheKey = "TYYW_GG_AJJBXXModel-" + BMSAH;
			object objModel = EDRS.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(BMSAH);
					if (objModel != null)
					{
                        int ModelCache = EDRS.Common.ConfigHelper.GetConfigInt("ModelCache");
                        EDRS.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (EDRS.Model.TYYW_GG_AJJBXX)objModel;
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
        public List<EDRS.Model.TYYW_GG_AJJBXX> GetModelList(string strWhere, params object[] objValues)
		{
            DataSet ds = dal.GetList(strWhere, objValues);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<EDRS.Model.TYYW_GG_AJJBXX> DataTableToList(DataTable dt)
		{
			List<EDRS.Model.TYYW_GG_AJJBXX> modelList = new List<EDRS.Model.TYYW_GG_AJJBXX>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				EDRS.Model.TYYW_GG_AJJBXX model;
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
        /// 扩展包含是否关联分页获取数据列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="orderby"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <param name="objValues"></param>
        /// <returns></returns>
        public DataSet GetListByPageUnite(string strWhere, string orderby, int startIndex, int endIndex, params object[] objValues)
        {
            return dal.GetListByPageUnite(strWhere, orderby, startIndex, endIndex, objValues);
        }

        /// <summary>
        /// 扩展获取记录总数
        /// </summary>
        public int GetRecordCountUnite(string strWhere, params object[] objValues)
        {
            return dal.GetRecordCountUnite(strWhere, objValues);
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

        #region 获取Ice对象转换本地对象
        /// <summary>
        /// 获取Ice对象转换本地对象
        /// </summary>
        /// <param name="bmsah"></param>
        /// <returns></returns>
        public EDRS.Model.TYYW_GG_AJJBXX GetIceByModel(string bmsah, List<string> ajlbbm, List<string> userRoleList, string dwbm,string gh)
        {
            int count;
            string msg;
            IceServicePrx icePrx = new IceServicePrx();
            msg = GetConfiguration(dwbm, gh, icePrx);
            if (!string.IsNullOrEmpty(msg))
                return null;
            List<ZC57s.CaseInfoServ.DigitalDossier.ICInterface.ClsAJJBXX> ajjbxxList = icePrx.GetAjjbxx(bmsah, ajlbbm, userRoleList, dwbm, "", null, null, "", "", 1, 1, out count, out msg);
            //List<ZC57s.CaseInfoServ.DigitalDossier.ICInterface.ClsAJJBXX> ajjbxxList = icePrx.GetAjjbxx(bmsah, ajlbbm, userRoleList, "", "", null, null, "", "", 1, 1, out count, out msg);
            if (ajjbxxList != null && ajjbxxList.Count > 0)
            {
                ZC57s.CaseInfoServ.DigitalDossier.ICInterface.ClsAJJBXX clsajjbxx = ajjbxxList[0];
                EDRS.Model.TYYW_GG_AJJBXX ajjbxx = new Model.TYYW_GG_AJJBXX();
                ajjbxx.BMSAH=clsajjbxx.BMSAH;
                ajjbxx.TYSAH=clsajjbxx.TYSAH;
                ajjbxx.SFSC = clsajjbxx.SFSC;
                ajjbxx.SFYGXTSJ = clsajjbxx.SFYGXTSJ;
                ajjbxx.CBDW_BM = clsajjbxx.AJ_DWBM;
                ajjbxx.CBDW_MC = clsajjbxx.AJ_DWMC;
                ajjbxx.FQDWBM = clsajjbxx.FQDWBM;
                ajjbxx.FQL = clsajjbxx.FQL;
                ajjbxx.CJSJ = clsajjbxx.CJSJ;
                ajjbxx.ZHXGSJ = clsajjbxx.ZHXGSJ;
                ajjbxx.SLRQ = clsajjbxx.SLRQ;
                ajjbxx.AJMC = clsajjbxx.AJMC;
                ajjbxx.AJLB_BM = clsajjbxx.AJLB_BM;
                ajjbxx.AJLB_MC = clsajjbxx.AJLB_MC;
                ajjbxx.ZCJG_DWDM = clsajjbxx.ZCJG_DWDM;
                ajjbxx.ZCJG_DWMC = clsajjbxx.ZCJG_DWMC;
                ajjbxx.YSDW_DWDM = clsajjbxx.YSDW_DWDM;
                ajjbxx.YSDW_DWMC = clsajjbxx.YSDW_DWMC;
                ajjbxx.YSWSWH = clsajjbxx.YSWSWH;
                ajjbxx.YSAY_AYDM = clsajjbxx.YSAY_AYDM;
                ajjbxx.YSAY_AYMC = clsajjbxx.YSAY_AYMC;
                ajjbxx.YSQTAY_AYDMS = clsajjbxx.YSQTAY_AYDMS;
                ajjbxx.YSQTAY_AYMCS = clsajjbxx.YSQTAY_AYMCS;
                ajjbxx.CBRGH = clsajjbxx.CBRGH;
                ajjbxx.CBR = clsajjbxx.CBR;
                ajjbxx.CBBM_BM = clsajjbxx.CBBM_BM;
                ajjbxx.CBBM_MC = clsajjbxx.CBBM_MC;
                ajjbxx.AJZT = clsajjbxx.AJZT;
                ajjbxx.SFSWAJ = clsajjbxx.SFSWAJ;
                ajjbxx.SFDBAJ = clsajjbxx.SFDBAJ;
                ajjbxx.ZXHD_MC = clsajjbxx.ZXHD_MC;
                ajjbxx.WCRQ = clsajjbxx.WCRQ;
                ajjbxx.GDRQ = clsajjbxx.GDRQ;
                ajjbxx.GDRGH = clsajjbxx.GDRGH;
                ajjbxx.GDR = clsajjbxx.GDR;
                ajjbxx.AQZY = clsajjbxx.AQZY;
                ajjbxx.DQJD = clsajjbxx.DQJD;
                ajjbxx.BLKSRQ = clsajjbxx.BLKSRQ;
                ajjbxx.BLTS = clsajjbxx.BLTS;
                ajjbxx.DQRQ = clsajjbxx.DQRQ;
                ajjbxx.BJRQ = clsajjbxx.BJRQ;
                ajjbxx.YJZT = clsajjbxx.YJZT;
                ajjbxx.JYYJZT = clsajjbxx.JYYJZT;
                ajjbxx.JYYJHCQXYRGS = clsajjbxx.JYYJHCQXYRGS;
                ajjbxx.LCSLBH = clsajjbxx.LCSLBH;
                ajjbxx.FXDJ = clsajjbxx.FXDJ;
                ajjbxx.SFGZAJ = clsajjbxx.SFGZAJ;
                ajjbxx.FZ = clsajjbxx.FZ;
                ajjbxx.YSYJ_DM = clsajjbxx.YSYJ_DM;
                ajjbxx.YSYJ_MC = clsajjbxx.YSYJ_MC;
                ajjbxx.SFJBAJ = clsajjbxx.SFJBAJ;
                ajjbxx.ZXHD_DM = clsajjbxx.ZXHD_DM;
                ajjbxx.DQYJJD = clsajjbxx.DQYJJD;
                ajjbxx.YASCSSJD_DM = clsajjbxx.YASCSSJD_DM;
                ajjbxx.YASCSSJD_MC = clsajjbxx.YASCSSJD_MC;
                ajjbxx.YSRJDH = clsajjbxx.YSRJDH;
                return ajjbxx;
            }
            return null;
        } 
        #endregion


        #region 绑定获取案件基本信息列表
        /// <summary>
        /// 绑定获取案件基本信息列表
        /// </summary>
        /// <param name="Request"></param>
        /// <param name="dwbm"></param>
        /// <param name="gh"></param>
        /// <returns></returns>
        public string ListBin(System.Web.HttpRequest Request, string dwbm, string gh,string bmbm,string jsbm)
        {
            int count = 0;
            string msg = "";
            //读取配置文件中的配置
            int? isLocalAjxx = ConfigHelper.GetConfigInt("IsLocalAjxx");
            if (isLocalAjxx == 1)
            {
                DataTable dt = ListBindIce(Request, dwbm, gh, ref count, ref msg);
                if (dt != null)
                    return "{\"Total\":" + count + ",\"Rows\":" + JsonHelper.JsonString(dt) + "}";
                else
                    return ReturnString.JsonToString(Prompt.error, msg, null);
            }
            else if (isLocalAjxx == 2) //读取接口数据
            {
                return GetWebService(Request,dwbm,gh,jsbm,bmbm);
            }
            else
            {
                DataTable dt = ListBind(Request, dwbm, gh, ref count);
                if (dt != null)
                    return "{\"Total\":" + count + ",\"Rows\":" + JsonHelper.JsonString(dt) + "}";
                else
                    return ReturnString.JsonToString(Prompt.error, "未找到信息", null);
            }
        }

       

        /// <summary>
        /// 绑定获取案件基本信息列表
        /// </summary>
        /// <param name="Request"></param>
        /// <param name="dwbm"></param>
        /// <param name="gh"></param>
        /// <returns></returns>
        public DataTable ListBinTable(System.Web.HttpRequest Request, string dwbm, string gh)
        {
            int count = 0;
            string msg = "";
            //读取配置文件中的配置
            int? isLocalAjxx = ConfigHelper.GetConfigInt("IsLocalAjxx");
            if (isLocalAjxx == 1)
            {
                return ListBindIce(Request, dwbm, gh, ref count, ref msg);

            }
            else
            {
                return ListBind(Request, dwbm, gh, ref count);

            }
        }
        #endregion

        #region 根据Web接口获取数据
        /// <summary>
        /// 根据Web接口获取数据
        /// </summary>
        /// <param name="Request"></param>
        /// <param name="dwbm"></param>
        /// <param name="gh"></param>
        /// <param name="jsbm"></param>
        /// <param name="bmbm"></param>
        /// <returns></returns>
        public string GetWebService(System.Web.HttpRequest Request, string dwbm, string gh, string jsbm, string bmbm)
        {


            string url = ConfigHelper.GetConfigString("AJXXDZ");
            string key = "tyknntd-#4kji2%(+^";

            if (string.IsNullOrEmpty(url))
                return ReturnString.JsonToString(Prompt.error, "您已配置为外部连接方式，请先设置配置文件连接地址", null);

            Dictionary<String, String> dicList = new Dictionary<String, String>();

            string page = Request["page"];
            string rows = Request["pagesize"];
            string bmsah = Request["key"];
            string ajmc = Request["casename"];
            string cbr = Request["dutyman"];
            string timebegin = Request["timebegin"];
            string timeend = Request["timeend"];
            //单位编码和案件类别
            string caseUnit = Request["caseUnit"];
            string caseAjlb = Request["caseajlb"];
            //排序
            string sortname = Request["sortname"];
            string sortorder = Request["sortorder"];
            if (string.IsNullOrEmpty(sortname))
            {
                sortname = "SLRQ DESC";
                sortorder = "";
            }
            if (sortname == "AJ_DWMC" || sortname == "CBDW_MC")
                sortname = "CBDW_BM";

            sortname += " " + sortorder;

            List<object> values = new List<object>();
            if (!string.IsNullOrEmpty(bmsah))
            {
                bmsah = "Bmsah=\"" + bmsah + "\"";
            }
            if (!string.IsNullOrEmpty(ajmc))
            {
                ajmc = "Ajmc=\"" + ajmc + "\"";
            }
            if (!string.IsNullOrEmpty(cbr))
            {
                cbr = "Cbr=\"" + cbr + "\"";
            }
            if (!string.IsNullOrEmpty(timebegin))
            {
                timebegin = "Timebegin=\"" + timebegin + "\"";
            }
            if (!string.IsNullOrEmpty(timeend))
            {
                timeend = "Timeend =\"" + timeend + "\"";
            }
            if (!string.IsNullOrEmpty(caseUnit))//单位编码
            {
                string[] dwbms = caseUnit.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                string dwbmParm = "";
                for (int i = 0; i < dwbms.Length; i++)
                {
                    dwbmParm += "{\"dwbm\":\"" + dwbms[i] + "\"}";
                    if (i < dwbms.Length - 1)
                        dwbmParm += ",";
                }
                caseUnit = "[" + dwbmParm + "]";
            }
            else
            {
                XT_DM_QX qx = new XT_DM_QX(Request);
                DataSet ds = qx.GetDwList(jsbm, dwbm, bmbm, "");
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0].Copy();
                    dt.Columns["QXBM"].ColumnName = "dwbm";
                    dt.Columns.Remove("QXMC");
                    caseUnit = JsonHelper.JsonString(dt);
                }
                else
                    caseUnit = "[{\"dwbm\":\"" + dwbm + "\"}]";

            }
            if (!string.IsNullOrEmpty(caseAjlb))//案件类别
            {
                string[] ajlb = caseAjlb.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                string ajlbParm = "";
                for (int i = 0; i < ajlb.Length; i++)
                {
                    ajlbParm += "{\"lb\":\"" + ajlb[i] + "\"}";
                    if (i < ajlb.Length - 1)
                        ajlbParm += ",";
                }
                caseAjlb = "[" + ajlbParm + "]";
            }
            else
            {
                XT_DM_QX qx = new XT_DM_QX(Request);
                DataSet ds = qx.GetLBList(jsbm, dwbm, bmbm, "");
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0].Copy();
                    dt.Columns["QXBM"].ColumnName = "lb";
                    dt.Columns.Remove("QXMC");
                    caseAjlb = JsonHelper.JsonString(dt);
                }
                //else
                //    caseAjlb = "[{\"ajlbbm\":\"" + dwbm + "\"}]";
            }

            dicList.Add("PageIndex", page);
            dicList.Add("PageSize", rows);
            dicList.Add("Bmsah", bmsah);
            dicList.Add("MC", ajmc);
            //dicList.Add("Cbr", cbr);
            dicList.Add("Timebegin", timebegin);
            dicList.Add("Timeend", timeend);
            dicList.Add("DWBM", caseUnit.Replace("\r\n", ""));
            dicList.Add("LB", caseAjlb.Replace("\r\n", ""));
            //dicList.Add("Sort", sortname);
            
            //dicList.Add("PageIndex", (string.IsNullOrEmpty(page) ? page : EDRS.Common.DEncrypt.DEncrypt.Encrypt(page, key)));
            //dicList.Add("PageSize", (string.IsNullOrEmpty(rows) ? rows : EDRS.Common.DEncrypt.DEncrypt.Encrypt(rows, key)));
            //dicList.Add("Bmsah", (string.IsNullOrEmpty(bmsah) ? bmsah : EDRS.Common.DEncrypt.DEncrypt.Encrypt(bmsah, key)));
            //dicList.Add("Ajmc", (string.IsNullOrEmpty(ajmc) ? ajmc : EDRS.Common.DEncrypt.DEncrypt.Encrypt(ajmc, key)));
            //dicList.Add("Cbr", (string.IsNullOrEmpty(cbr) ? cbr : EDRS.Common.DEncrypt.DEncrypt.Encrypt(cbr, key)));
            //dicList.Add("Timebegin", (string.IsNullOrEmpty(timebegin) ? timebegin : EDRS.Common.DEncrypt.DEncrypt.Encrypt(timebegin, key)));
            //dicList.Add("Timeend", (string.IsNullOrEmpty(timeend) ? timeend : EDRS.Common.DEncrypt.DEncrypt.Encrypt(timeend, key)));
            //dicList.Add("Dwbm", (string.IsNullOrEmpty(caseUnit) ? caseUnit : EDRS.Common.DEncrypt.DEncrypt.Encrypt(caseUnit, key)));
            //dicList.Add("Ajlbbm", (string.IsNullOrEmpty(caseAjlb) ? caseAjlb : EDRS.Common.DEncrypt.DEncrypt.Encrypt(caseAjlb, key)));
            //dicList.Add("Sort", (string.IsNullOrEmpty(sortname) ? sortname : EDRS.Common.DEncrypt.DEncrypt.Encrypt(sortname, key)));

            WebClient wc = new WebClient();
            string rt = wc.Post(url, dicList);

            if (!string.IsNullOrEmpty(rt))
            {
                string stat = JsonHelper.DeserializeObjectKey("[" + rt + "]", "Stat");
                if (stat == "0")
                {
                    string data = JsonHelper.DeserializeObjectKey("[" + rt + "]", "Data");
                    DataTable dtData = JsonHelper.ToDataTable(data);
                    if (dtData != null)
                    {
                        dtData.Columns["MC"].ColumnName = "AJMC";
                        dtData.Columns["BH"].ColumnName = "BMSAH";
                        dtData.Columns["LBMC"].ColumnName = "AJLB_MC";
                        dtData.Columns["LBBM"].ColumnName = "AJLB_BM";
                        dtData.Columns["DWMC"].ColumnName = "CBDW_MC";
                        dtData.Columns["DWBM"].ColumnName = "CBDW_BM";


                        if (dtData.Rows.Count > 0)
                        {
                            return "{\"Total\":" + dtData.Rows[0]["Total"].ToString() + ",\"Rows\":" + JsonHelper.JsonString(dtData) + "}";
                        }
                    }
                    //{
                    //Total:分页数据总数(int),
                    //MC:名称(string),
                    //BH:编号(string),
                    //LBMC:类别名称(string),
                    //LBBM:类别编码(string),
                    //DWMC:单位名称(string),
                    //DWBM:单位编码(string),
                    //CBR:承办人(string),
                    //SLRQ：受理日期（yyyy-MM-dd HH:mm:ss）
                    //}
                    //  "{"Total":1,"Rows":[{"ISREGARD": 0,  "AJMC": "ffffff",  "BMSAH": "广元市公安局上会议题[2016]37000000001号",  "AJLB_MC": "上会议题",  "CBDW_MC": "广元市公安局",  "CBBM_MC": null,  "CBR": null,  "DQJD": null,  "SLRQ": "2016-04-05 11:15:36",  "AJZT": "0",  "DQRQ": "1900-01-01 00:00:00",  "BJRQ": "1900-01-01 00:00:00",  "WCRQ": "1900-01-01 00:00:00",  "GDRQ": "1900-01-01 00:00:00",  "AJLB_BM": "1101",  "CBDW_BM": "370000",  "XYR": "ff",  "SFZH": "444444444444444444",  "TARYXX": "asdfasdfasdfasd"}]}"
                }
                else if (stat == "1")
                {
                    string msg = JsonHelper.DeserializeObjectKey("[" + rt + "]", "Msg");
                    return ReturnString.JsonToString(Prompt.error, msg, null);
                }
            }
            return ReturnString.JsonToString(Prompt.error, "未找到信息", null);
        }
        #endregion

        #region 获取本地案件基本信息
        /// <summary>
        /// 获取本地案件基本信息
        /// </summary>
        /// <param name="Request"></param>
        /// <param name="dwbm"></param>
        /// <param name="gh"></param>
        /// <returns></returns>
        public DataTable ListBind(System.Web.HttpRequest Request, string dwbm, string gh,ref int count)
        {
            string page = Request["page"];
            string rows = Request["pagesize"];
            string key = Request["key"];
            string wsbh = Request["wsbh"];
            string casename = Request["casename"];
            string dutyman = Request["dutyman"];
            string relevance = Request["relevance"];
            string relevance_la = Request["relevance_la"];
          
            string timebegin = Request["timebegin"];
            string timeend = Request["timeend"];
            //单位编码和案件类别
            string caseUnit = Request["caseUnit"];
            string caseAjlb = Request["caseajlb"];
            //排序
            string sortname = Request["sortname"];
            string sortorder = Request["sortorder"];

            string smr = Request.Form["smr"];
            string smsj_bg = Request.Form["smsj_bg"];
            string smsj_en = Request.Form["smsj_en"];
            string xyr = Request.Form["xyr"];
            //立案状态
            string smajla = Request.Form["smajla"];
            //归档状态
            string smajcd = Request.Form["smajcd"];


            if (string.IsNullOrEmpty(sortname))
            {
                sortname = "GGCJSJ DESC";
                sortorder = "";
            }
            if (sortname == "AJ_DWMC" || sortname == "CBDW_MC")
                sortname = "CBDW_BM";
            if (sortname == "SFZZ")
                sortname = "ISREGARD";
            sortname += " " + sortorder;

            int pageNumber = int.Parse(page);
            int pageSize = int.Parse(rows);
            string where = string.Empty;

            List<object> values = new List<object>();
            if (!string.IsNullOrEmpty(key))
            {
                where += " and BMSAH like :BMSAH";
                values.Add("%" + key + "%");
            }
            if (!string.IsNullOrEmpty(wsbh))
            {
                where += " and WSBH like :WSBH";
                values.Add("%" + wsbh + "%");
            }
            if (!string.IsNullOrEmpty(casename))
            {
                where += " and AJMC like :AJMC";
                values.Add("%" + casename + "%");
            }
            if (!string.IsNullOrEmpty(dutyman))
            {
                where += " and CBR like :CBR";
                values.Add("%" + dutyman + "%");
            }
            
            if (!string.IsNullOrEmpty(relevance) && relevance != "-2")
            {
                where += " and ZZZT in (" + relevance + ")";
                //values.Add(relevance);
            }
            if (!string.IsNullOrEmpty(relevance_la) && relevance_la != "-2")
            {
                where += " and LAZZZT in (" + relevance_la + ")";
                //values.Add(relevance);
            }
            if (!string.IsNullOrEmpty(timebegin))
            {
                where += " and GDRQ >= :GDRQ";
                values.Add(Convert.ToDateTime(timebegin));
            }
            if (!string.IsNullOrEmpty(timeend))
            {
                where += " and GDRQ <= :GDRQ";
                values.Add(Convert.ToDateTime(timeend).AddDays(1));
            }

            if (!string.IsNullOrEmpty(smr))
            {
                where += " and JZSCRXM like :JZSCRXM";
                values.Add("%" + smr + "%");
            }
            if (!string.IsNullOrEmpty(smsj_bg))
            {
                where += " and CJSJ >= :CJSJ";
                values.Add(Convert.ToDateTime(smsj_bg));
            }
            if (!string.IsNullOrEmpty(smsj_en))
            {
                where += " and CJSJ <= :CJSJ1";
                values.Add(Convert.ToDateTime(smsj_en).AddDays(1));
            }
            if (!string.IsNullOrEmpty(xyr))
            {
                where += " and XYR like :XYR";
                values.Add("%" + xyr + "%");
            }
            if (!string.IsNullOrEmpty(smajla))
            {
                where += " and SMAJLA = :SMAJLA";
                values.Add(smajla);
            }
            if (!string.IsNullOrEmpty(smajcd))
            {
                where += " and SMAJCD = :SMAJCD";
                values.Add(smajcd);
            }

            //if (!string.IsNullOrEmpty(base.UserInfo.DWBM))
            //{
            //    where += " and CBDW_BM = :CBDWBM";
            //    values[6] = base.UserInfo.DWBM;
            //}
            where += " and SFSC='N'";
            if (string.IsNullOrEmpty(caseUnit))
            {
                where += " and trim(CBDW_BM) in(select distinct trim(QXBM) from xt_dm_qx a join XT_QX_RYJSFP b on trim(a.jsbm) = b.jsbm and trim(a.bmbm)=b.bmbm and trim(a.dwbm)=b.dwbm and gh=:GH1 and b.dwbm=:DWBM1 and a.qxlx=0)";
                values.Add(gh);
                values.Add(dwbm);  
            }
            else
            {
                
                string[] dwbms =caseUnit.Split(new char[]{';'},StringSplitOptions.RemoveEmptyEntries);
                string dwbmParm = "";
                for (int i = 0; i < dwbms.Length; i++)
                {
                    dwbmParm += "'" + dwbms[i] + "'";
                    if (i < dwbms.Length - 1)
                        dwbmParm += ",";
                }
                where += " and trim(CBDW_BM) in (" + dwbmParm + ") ";
              //  values.Add(dwbmParm);
            }
           // StringBuilder strWhere = new StringBuilder();
            if (string.IsNullOrEmpty(caseAjlb))
            {
                where += "  and trim(AJLB_BM) in ( select distinct trim(QXBM) from xt_dm_qx a join XT_QX_RYJSFP b on trim(a.jsbm) = b.jsbm and trim(a.bmbm)=b.bmbm and trim(a.dwbm)=b.dwbm and gh=:GH2 and b.dwbm=:DWBM2 and a.qxlx=1) ";
                values.Add(gh);
                values.Add(dwbm);
            }
            else
            {
                
                string[] ajlbs = caseAjlb.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                string ajlbsParm = "";
                for (int i = 0; i < ajlbs.Length; i++)
                {
                    ajlbsParm += "'"+ajlbs[i]+"'";
                    if (i < ajlbs.Length - 1)
                        ajlbsParm += ",";
                }
                where += " and trim(AJLB_BM) in (" + ajlbsParm + ") ";
                //values.Add(ajlbsParm);
            }

            TYYW_GG_AJJBXX bll = new TYYW_GG_AJJBXX(Request);

            DataSet ds = bll.GetListByPageUnite(where, sortname, (pageSize * pageNumber) - pageSize + 1, (pageSize * pageNumber), values.ToArray());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                count = bll.GetRecordCountUnite(where, values.ToArray());
                return dt;
                //OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "案件数据查询", UserInfo, UserRole, this.Request);
                //return "{\"Total\":" + count + ",\"Rows\":" + JsonHelper.JsonString(dt) + "}";
            }
            return null;
            //OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "案件数据查询未找到案件", UserInfo, UserRole, this.Request);
            //return ReturnString.JsonToString(Prompt.error, "未找到案件信息", null);
        }
        public DataTable ListBind(System.Web.HttpRequest Request, string dwbm, string gh,string zzzt,string strWhere ,ref int count)
        {
            string page = Request.Form["page"];
            string rows = Request.Form["pagesize"];
            string zzztTo = Request.Form["zzzt"];
            string ajmc = Request.Form["ajmc"];
            string ajlb = Request.Form["ajlb"];
            string lar = Request.Form["lar"];
            string ajbh = Request.Form["ajbh"];
            string wsbh = Request.Form["wsbh"];
            string timebegin = Request.Form["timebegin"];
            string timeend = Request.Form["timeend"];


            //扫描人和时间
            string smr = Request.Form["zzr"];
            string smsj_bg = Request.Form["zztimebegin"];
            string smsj_en = Request.Form["zztimeend"];
    
            //排序
            //string sortname = Request["sortname"];
            //string sortorder = Request["sortorder"];
            //if (string.IsNullOrEmpty(sortname))
            //{
            //    sortname = "CJSJ DESC";
            //    sortorder = "";
            //}
            //if (sortname == "AJ_DWMC" || sortname == "CBDW_MC")
            //    sortname = "CBDW_BM";
            //if (sortname == "SFZZ")
            //    sortname = "ISREGARD";
            string sortname = " CJSJ DESC ";



            int pageNumber = int.Parse(page);
            int pageSize = int.Parse(rows);
            string where = strWhere;

            List<object> values = new List<object>();
            if (!string.IsNullOrEmpty(ajmc))
            {
                where += " and AJMC like :AJMC";
                values.Add("%" + ajmc + "%");
            }
            if (!string.IsNullOrEmpty(lar))
            {
                where += " and CBR like :CBR";
                values.Add("%" + lar + "%");
            }
            if (!string.IsNullOrEmpty(ajbh))
            {
                where += " and BMSAH like :BMSAH";
                values.Add("%" + ajbh + "%");
            } if (!string.IsNullOrEmpty(wsbh))
            {
                where += " and WSBH like :WSBH";
                values.Add("%" + wsbh + "%");
            }

            if (!string.IsNullOrEmpty(smr))
            {
                where += " and JZSCRXM like :JZSCRXM";
                values.Add("%" + smr + "%");
            }
            if (!string.IsNullOrEmpty(smsj_bg))
            {
                where += " and CJSJ >= :CJSJ";
                values.Add(Convert.ToDateTime(smsj_bg));
            }
            if (!string.IsNullOrEmpty(smsj_en))
            {
                where += " and CJSJ <= :CJSJ1";
                values.Add(Convert.ToDateTime(smsj_en).AddDays(1));
            }

            if (!string.IsNullOrEmpty(zzztTo) && zzztTo != "-1")
            {
                where += " and ZZZT = :ZZZT";
                values.Add(zzztTo);
            }
            else if (!string.IsNullOrEmpty(zzzt))
            {
               
                string zt = "";
                string[] zzzts = zzzt.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < zzzts.Length; i++)
                {
                    zt += zzzts[i];
                    if (i < zzzts.Length - 1)
                        zt += ",";
                }
                where += " and ZZZT in (" + zt + ")";
                //values.Add(zt);               
            }
            if (!string.IsNullOrEmpty(timebegin))
            {
                where += " and GDRQ >= :SLRQ";
                values.Add(Convert.ToDateTime(timebegin));
            }
            if (!string.IsNullOrEmpty(timeend))
            {
                where += " and GDRQ < :SLRQ1";
                values.Add(Convert.ToDateTime(timeend).AddDays(1));
            }
            //if (!string.IsNullOrEmpty(base.UserInfo.DWBM))
            //{
            //    where += " and CBDW_BM = :CBDWBM";
            //    values[6] = base.UserInfo.DWBM;
            //}
            where += " and SFSC='N'";

            where += " and trim(CBDW_BM) in(select distinct trim(QXBM) from xt_dm_qx a join XT_QX_RYJSFP b on trim(a.jsbm) = b.jsbm and trim(a.bmbm)=b.bmbm and trim(a.dwbm)=b.dwbm and gh=:GH1 and b.dwbm=:DWBM1 and a.qxlx=0)";
                values.Add(gh);
                values.Add(dwbm);
          
            // StringBuilder strWhere = new StringBuilder();
            if (string.IsNullOrEmpty(ajlb))
            {
                where += "  and trim(AJLB_BM) in ( select distinct trim(QXBM) from xt_dm_qx a join XT_QX_RYJSFP b on trim(a.jsbm) = b.jsbm and trim(a.bmbm)=b.bmbm and trim(a.dwbm)=b.dwbm and gh=:GH2 and b.dwbm=:DWBM2 and a.qxlx=1) ";
                values.Add(gh);
                values.Add(dwbm);
            }
            else
            {
                where += " and trim(AJLB_BM) in (:AJLB_BM ) ";
                string[] ajlbs = ajlb.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                string ajlbsParm = "";
                for (int i = 0; i < ajlbs.Length; i++)
                {
                    ajlbsParm += ajlbs[i];
                    if (i < ajlbs.Length - 1)
                        ajlbsParm += ",";
                }
                values.Add(ajlbsParm);
            }

            TYYW_GG_AJJBXX bll = new TYYW_GG_AJJBXX(Request);

            DataSet ds = bll.GetListByPageUnite(where, sortname, (pageSize * pageNumber) - pageSize + 1, (pageSize * pageNumber), values.ToArray());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                count = bll.GetRecordCountUnite(where, values.ToArray());
                return dt;
                //OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "案件数据查询", UserInfo, UserRole, this.Request);
                //return "{\"Total\":" + count + ",\"Rows\":" + JsonHelper.JsonString(dt) + "}";
            }
            return null;
            //OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "案件数据查询未找到案件", UserInfo, UserRole, this.Request);
            //return ReturnString.JsonToString(Prompt.error, "未找到案件信息", null);
        } 
        #endregion

        #region 根据ICE获取案件基本信息列表
        /// <summary>
        /// 根据ICE获取案件基本信息列表
        /// </summary>
        /// <returns></returns>
        private DataTable ListBindIce(System.Web.HttpRequest Request, string dwbm, string gh, ref int count, ref string msg)
        {
            string page = Request["page"];
            string rows = Request["pagesize"];
            string key = Request["key"];
            string casename = Request["casename"];
            string dutyman = Request["dutyman"];
            string relevance = Request["relevance"];
            if (relevance == "1")
                relevance = "Y";
            else if (relevance == "2")
                relevance = "N";
            else
                relevance = "";
            DateTime? timebegin = null;
            if (Request["timebegin"] != null && !string.IsNullOrEmpty(Request["timebegin"]))
                timebegin = Convert.ToDateTime(Request["timebegin"]);

            DateTime? timeend = null;
            if (Request["timeend"] != null && !string.IsNullOrEmpty(Request["timeend"]))
                timeend = Convert.ToDateTime(Request["timeend"]);

            //排序
            string sortname = Request["sortname"];
            string sortorder = Request["sortorder"];
            //if (string.IsNullOrEmpty(sortname))
            //    sortname = "AJZT";
            if (sortname == "ISREGARD")
                sortname = "SFZZ";
            sortname += " " + sortorder;


            EDRS.Common.IceServicePrx iceprx = new IceServicePrx();

            msg = GetConfiguration(dwbm, gh, iceprx);
            if (!string.IsNullOrEmpty(msg))
                return null;
            string caseUnit = Request["caseUnit"];
            string caseAjlb = Request["caseajlb"];

             List<string> ajlbList = new List<string>();
             List<string> dwList = new List<string>();
            if(string.IsNullOrEmpty(caseAjlb))
            {
                ajlbList = GetAjTypeBm(Request, dwbm, "", gh);
            }
            else
            {
                foreach(string _ajlb in caseAjlb.Split(';'))
                {
                    ajlbList.Add(_ajlb);
                }
            }
            if(string.IsNullOrEmpty(caseUnit))
            {
                dwList = GetDwBm(Request,dwbm, "", gh);
            }
            else
            {
                foreach(string _unit in caseUnit.Split(';'))
                {
                    dwList.Add(_unit);
                }
            }
      
            DataTable dt = iceprx.AJJBXXJson(key, ajlbList, null, dwList, casename, timebegin, timeend, dutyman, relevance, int.Parse(rows), int.Parse(page), out count, out msg);
            if (count > 0)
            {
                DataView dv = dt.DefaultView;
                if (!string.IsNullOrEmpty(sortname.Trim()))
                    dv.Sort = sortname;
                return dv.ToTable();
               // OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "案件数据查询", UserInfo, UserRole, this.Request);
                //return "{\"Total\":" + count + ",\"Rows\":" + JsonHelper.JsonString(dt) + "}";
            }
            //OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "案件数据查询未找到案件", UserInfo, UserRole, this.Request);
            if (string.IsNullOrEmpty(msg))
                msg = "未找到信息";
            //else
            //    return ReturnString.JsonToString(Prompt.error, msg, null);

            return null;
        }
        #endregion

        #region 获取ICE统一业务配置参数
        /// <summary>
        /// 获取ICE统一业务配置参数
        /// </summary>
        /// <param name="Request"></param>
        /// <param name="dwbm"></param>
        /// <param name="gh"></param>
        /// <returns></returns>
        public string GetConfiguration(string dwbm, string gh, IceServicePrx iceprx)
        {
            XY_DZJZ_XTPZ xtpzbll = new XY_DZJZ_XTPZ(null);
            DataSet ds = xtpzbll.GetAllList(dwbm, gh);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow[] dr = ds.Tables[0].Select("CONFIGID=" + (int)EnumConfig.访问统一业务ICE地址);
                if (dr.Length > 0 && dr[0]["CONFIGVALUE"] != null)
                    iceprx.DownPrx = dr[0]["CONFIGVALUE"].ToString();
                else
                    return ReturnString.JsonToString(Prompt.error, "请先在系统配置模块配置访问统一业务ICE地址", null);

                dr = ds.Tables[0].Select("CONFIGID=" + (int)EnumConfig.ICE消息包大小);
                if (dr.Length > 0 && dr[0]["CONFIGVALUE"] != null)
                    iceprx.SizeMax = Convert.ToDecimal(dr[0]["CONFIGVALUE"].ToString());
                else
                    return ReturnString.JsonToString(Prompt.error, "请先在系统配置模块配置ICE消息包大小", null);
            }
            else
                return ReturnString.JsonToString(Prompt.error, "请先在系统配置模块中进行配置", null);
            return "";
        } 
        #endregion

        #region 获取案件类型权限编码集合
        /// <summary>
        /// 获取案件类型权限编码集合
        /// </summary>
        /// <param name="dwbm"></param>
        /// <param name="bmbm"></param>
        /// <param name="jsbm"></param>
        /// <returns></returns>
        public List<string> GetAjTypeBm(System.Web.HttpRequest Request, string dwbm, string bmbm, string gh)
        {
            EDRS.BLL.XT_DM_QX bll = new EDRS.BLL.XT_DM_QX(Request);
            DataSet ds = bll.GetQxListByRole(dwbm, bmbm, gh, 1,"");
            List<string> list = new List<string>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow ro in ds.Tables[0].Rows)
                    list.Add(ro["QXBM"].ToString());
            }
            return list;
        }
        /// <summary>
        /// 获取单位权限
        /// </summary>
        /// <param name="dwbm"></param>
        /// <param name="bmbm"></param>
        /// <param name="jsbm"></param>
        /// <returns></returns>
        public List<string> GetDwBm(System.Web.HttpRequest Request, string dwbm, string bmbm, string gh)
        {
            EDRS.BLL.XT_DM_QX bll = new EDRS.BLL.XT_DM_QX(Request);
            DataSet ds = bll.GetQxListByRole(dwbm, bmbm, gh, 0,"");
            List<string> list = new List<string>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow ro in ds.Tables[0].Rows)
                    list.Add(ro["QXBM"].ToString());
            }
            return list;
        }
        #endregion

        #region 获取同一业务所属类别列表数据List<ClsSSLB>
        public DataTable GetSSLBInfos(string strWhere,System.Web.HttpRequest context, params object[] objValues)//GetSSLBInfos(out string strError, string dwbm, string gh, System.Web.HttpRequest context)
        {
            //strError = "";
            //EDRS.Common.IceServicePrx iceprx = new IceServicePrx();
            XT_DM_AJLBBM bll = new XT_DM_AJLBBM(context);
            DataSet ds = bll.GetSSLBList(strWhere,objValues);
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;

            //string msg = "";
            //msg = GetConfiguration(dwbm, gh, iceprx);
            //return iceprx.GetSSLBInfos(out strError);
        }
        #endregion

        #region 增加一条数据
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddList(EDRS.Model.TYYW_GG_AJJBXX model, EDRS.Model.TYYW_GG_AJJBXXKZ modelkz, EDRS.Model.YX_DZJZ_JZJBXX modeljbxx)
        {
            return dal.AddList(model, modelkz, modeljbxx);
        }
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="modelkz"></param>
        /// <param name="modeljbxx"></param>
        /// <returns></returns>
        public bool UpdateList(EDRS.Model.TYYW_GG_AJJBXX model, EDRS.Model.TYYW_GG_AJJBXXKZ modelkz, EDRS.Model.YX_DZJZ_JZJBXX modeljbxx) 
        {
            return dal.UpdateList(model, modelkz, modeljbxx);
        }
        #endregion
    }
}

