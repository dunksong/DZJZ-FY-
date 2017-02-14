using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using EDRS.Common;
using EDRS.BLL;
using EDRS.Common.DEncrypt;

namespace WebUI.Pages.Print
{
    public partial class AddCase : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           // string[] t = Request.PathInfo.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            string type = Request["t"];

            if (!string.IsNullOrEmpty(type))
            {
                Response.ContentEncoding = Encoding.UTF8;
                Response.ContentType = "application/json";
                if (type.Equals("ListBind"))
                    Response.Write(ListBind());
                if (type.Equals("AddData"))
                    Response.Write(AddData());
                if (type.Equals("UpData"))
                    Response.Write(UpData());
                if (type.Equals("DelData"))
                    Response.Write(DelData());
                if (type.Equals("GetModel"))
                    Response.Write(GetModel(""));
                if (type.Equals("GetBmsah"))
                    Response.Write(GetBmsah());
              
                Response.End();
            }
        }

        #region 绑定列表
        /// <summary>
        /// 绑定列表
        /// </summary>
        /// <returns></returns>
        public string ListBind()
        {
            string page = Request["page"];
            string rows = Request["pagesize"];
            string key = Request["key"];

            int pageNumber = int.Parse(page);
            int pageSize = int.Parse(rows);
            string where = string.Empty;

            object[] values = new object[1];
            if (!string.IsNullOrEmpty(key))
            {
                where += " and BT like :BT ";
                values[0] = "%" + key + "%";
            }

            EDRS.BLL.YX_DZJZ_FMDY bll = new EDRS.BLL.YX_DZJZ_FMDY(this.Request);

            DataSet ds = bll.GetListByPage(where, "", (pageSize * pageNumber) - pageSize + 1, (pageSize * pageNumber), values);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "绑定打印封面数据成功", UserInfo, UserRole, this.Request);
                int count = bll.GetRecordCount(where, values);

                return "{\"Total\":" + count + ",\"Rows\":" + JsonHelper.JsonString(ds.Tables[0]) + "}";
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "绑定打印封面数据未找到信息", UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "未找到信息", null);
        } 
        #endregion

        #region 添加配置数据
        /// <summary>
        /// 添加配置数据
        /// </summary>
        /// <returns></returns>
        private string AddData()
        {
            string type = Request.Form.Get("txt_type"); //打印类型

            string ywlx = Request.Form.Get("txt_ywlx"); //业务名称
            string ywbm = Request.Form.Get("txt_ywlx_val"); //业务名称

            string nf = Request.Form.Get("txt_nf"); //年份
            string z = Request.Form.Get("txt_z"); //字
            string h = Request.Form.Get("txt_h"); //号 
            string bmsah = "(" + nf + ")" + UserDwbm.DWJC + z + "字第" + h + "号"; //(2016)深南法少刑初字第2号
            string ajmc = Request.Form.Get("txt_fbt"); //案由
            string lbmc = z; //类别名称            
            string ajlb = (string.IsNullOrEmpty(Request.Form.Get("txt_z_val")) ? "" : Request.Form.Get("txt_z_val")); //案件类别编码

            string lasj = Request.Form.Get("txt_sasj"); //收案日期
            string jasj = Request.Form.Get("txt_jasj"); //结案时间
            string ladw = UserDwbm.DWBM;//Request.Form.Get("txt_ladw_val"); //立卷单位
            string ladw_mc = UserDwbm.DWMC;// Request.Form.Get("txt_ladw"); //立卷单位名称
            string ljr = "";//Request.Form.Get("txt_lar_val"); //立卷人
            string ljr_mc = string.IsNullOrEmpty(Request.Form.Get("txt_cbr")) ? "" : Request.Form.Get("txt_cbr"); //承办人
            string shr = string.IsNullOrEmpty(Request.Form.Get("txt_shr")) ? "" : Request.Form.Get("txt_shr"); //审核人
            string zjs = (string.IsNullOrEmpty(Request.Form.Get("txt_bjgjc")) ? "0" : Request.Form.Get("txt_bjgjc")); //本案共卷
            string djj = string.IsNullOrEmpty(Request.Form.Get("txt_sdjc")) ? "0" : Request.Form.Get("txt_sdjc"); //第几卷
            string djy = (string.IsNullOrEmpty(Request.Form.Get("txt_ngjy")) ? "0" : Request.Form.Get("txt_ngjy")); //第几页

            string xyr = ""; //嫌疑人
            string sfzh = "";//身份证号
            string taryxx = "";//同人信息
            string ajbh = "";// Request.Form.Get("txt_ajbh"); //案件编号
            string wsbh = "";//Request.Form.Get("wsbh_hidd");
            string wsmc = "";//Request.Form.Get("txt_wsbh");

            //判断案件名称
            //if (string.IsNullOrEmpty(ajmc))
            //{
            //    return ReturnString.JsonToString(Prompt.error, "必须填写案由", null);
            //}
            //判断年份
            if (string.IsNullOrEmpty(nf))
            {
                return ReturnString.JsonToString(Prompt.error, "请选择年份", null);
            }
            //判断字
            if (string.IsNullOrEmpty(z))
            {
                return ReturnString.JsonToString(Prompt.error, "请选择字", null);
            }
            //判断号
            if (string.IsNullOrEmpty(h))
            {
                return ReturnString.JsonToString(Prompt.error, "必须填写号", null);
            }
            //案号名称
            if (string.IsNullOrEmpty(bmsah))
            {
                return ReturnString.JsonToString(Prompt.error, "必须填写案号名称", null);
            }
            //案由
            if (string.IsNullOrEmpty(ajmc))
            {
                return ReturnString.JsonToString(Prompt.error, "必须填写案由", null);
            }


            //判断案件类别
            if (string.IsNullOrEmpty(ajlb))
            {
                return ReturnString.JsonToString(Prompt.error, "必须填写案件类别", null);
            }


            //  EDRS.BLL.YX_DZJZ_JZJBXX jbxxbll = new YX_DZJZ_JZJBXX(Request);



            if (!string.IsNullOrEmpty(ajmc) && !string.IsNullOrEmpty(ajlb))//&& !string.IsNullOrEmpty(ajbh)
            {
                EDRS.Model.TYYW_GG_AJJBXX model = new EDRS.Model.TYYW_GG_AJJBXX();
                EDRS.Model.YX_DZJZ_JZJBXX jbxxmodel = new EDRS.Model.YX_DZJZ_JZJBXX();
                EDRS.Model.TYYW_GG_AJJBXXKZ modelkz = new EDRS.Model.TYYW_GG_AJJBXXKZ();


                //EDRS.BLL.XT_DM_AJLBBM ajlbbmBll = new XT_DM_AJLBBM(Request);
                //EDRS.Model.XT_DM_AJLBBM ajlbModel = ajlbbmBll.GetModel(ajlb);

                //if (ajlbModel != null)
                //    lbjc = ajlbModel.AJSLJC;
                //if (string.IsNullOrEmpty(lbjc))
                //    lbjc = lbmc;
                #region 公共案件基本信息
                //string bmsahName = ajlb + "[" + DateTime.Now.Year + "]" + UserInfo.DWBM;
                //string num = "00001号";
                //TYYW_GG_AJJBXX jzajxx = new TYYW_GG_AJJBXX(this.Request);
                //DataSet ds = jzajxx.GetListByPage(" and bmsah like '" + bmsahName + "%'", "bmsah desc", 1, 1, null);
                //if (ds != null && ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 1)
                //{
                //    num = ds.Tables[0].Rows[0]["bmsah"].ToString().Replace(bmsahName, "").Replace("号", "");
                //    num = (Convert.ToInt32(num) + 1).ToString().PadLeft(5, '0') + "号";
                //}


                //山东省院起诉受[2015]37000000072号
                //model.BMSAH = bmsahName + num;
                model.BMSAH = bmsah;
                model.TYSAH = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                model.SFSC = "N";
                model.SFYGXTSJ = "N";
                model.CBDW_BM = UserInfo.DWBM;
                model.CBDW_MC = UserInfo.DWMC;
                model.FQDWBM = Convert.ToDecimal(UserInfo.DWBM.Substring(0, 4));
                model.FQL = DateTime.Now.Year.ToString();
                model.CJSJ = DateTime.Now;
                model.ZHXGSJ = DateTime.Now;
                model.SLRQ = (string.IsNullOrEmpty(lasj) ? DateTime.Now : Convert.ToDateTime(lasj));// DateTime.Now;
                model.AJMC = ajmc;
                model.AJLB_BM = ajlb;
                model.AJLB_MC = lbmc;
                model.XYR = xyr;
                model.SFZH = sfzh;
                model.TARYXX = taryxx;

                if (!string.IsNullOrEmpty(jasj))
                    model.BJRQ = Convert.ToDateTime(jasj);
                model.CBDW_BM = ladw;
                model.CBDW_MC = ladw_mc;
                model.CBR = ljr_mc;
                model.CBRGH = ljr;

                //2016-6-6 增字段
                model.SHR = shr; //审核人
                model.SHSJ = DateTime.Now;//审核时间
                model.ZJS = Convert.ToDecimal(zjs);//总卷数
                model.DJJ = djj;//第几卷
                model.ZYS = Convert.ToDecimal(djy);//总页数 
                #endregion

                //if (jzajxx.Add(model))
                //{
                #region 卷宗基本信息
                //string count = "0";

                //count = jbxxbll.GetRecordCount("", null).ToString();
                //count = count.PadLeft(5, '0');


                jbxxmodel.JZBH = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                jbxxmodel.SFSC = "N";
                jbxxmodel.CJSJ = DateTime.Now;
                jbxxmodel.ZHXGSJ = DateTime.Now;
                jbxxmodel.FQDWBM = 0;
                jbxxmodel.FQL = "";
                jbxxmodel.DWBM = UserInfo.DWBM;
                jbxxmodel.BMSAH = model.BMSAH;
                jbxxmodel.TYSAH = model.TYSAH;  //UserInfo.DWBM + DateTime.Now.ToString("yyyyMM") + jbxxbll.GetRecordCount("", null) + count;
                jbxxmodel.JZMC = ajmc;
                jbxxmodel.JZLJ = "";
                jbxxmodel.JZSCSJ = DateTime.Now;
                jbxxmodel.JZSCRGH = UserInfo.GH;
                jbxxmodel.JZSCRXM = UserInfo.MC;
                jbxxmodel.JZMS = "";
                jbxxmodel.JZXGH = "0";
                jbxxmodel.SFKYGX = "";
                jbxxmodel.GXYWBMJH = "";
                jbxxmodel.MRSFGKPZ = "";
                jbxxmodel.Accomplices = taryxx;
                jbxxmodel.Ajmb_bm = ajlb;
                jbxxmodel.Ajmb_mc = lbmc;
                jbxxmodel.Idnumber = sfzh;
                jbxxmodel.Isrecord = "0";
                jbxxmodel.Suspectname = xyr;
                jbxxmodel.WSBH = wsbh;
                jbxxmodel.AJBH = ajbh;
                jbxxmodel.WSMC = wsmc;
                //jbxxmodel.ZZZT = "-1";
                jbxxmodel.LAZZZT = "-1";
                jbxxmodel.SMAJLA = "1";
                jbxxmodel.SMAJCD = "";
                #endregion


                #region 案件基本信息扩展

                //modelkz.AJKZXH = ;
                modelkz.AHMC = bmsah;
                modelkz.AY = Request.Form.Get("txt_fbt");
                modelkz.SAJG = Request.Form.Get("txt_ajmc");
                modelkz.YG = Request.Form.Get("txt_ajbh");
                modelkz.BG = Request.Form.Get("txt_fzxyr");
                modelkz.YYR = string.IsNullOrEmpty(Request.Form.Get("txt_yyr")) ? "" : Request.Form.Get("txt_yyr");
                modelkz.SQZXR = string.IsNullOrEmpty(Request.Form.Get("txt_sqzxr")) ? "" : Request.Form.Get("txt_sqzxr");
                modelkz.BZXR = string.IsNullOrEmpty(Request.Form.Get("txt_bzxr")) ? "" : Request.Form.Get("txt_bzxr");
                modelkz.SARQ = Convert.ToDateTime(Request.Form.Get("txt_sasj"));
                modelkz.JARQ = Convert.ToDateTime(Request.Form.Get("txt_jasj"));
                modelkz.CJJG = string.IsNullOrEmpty(Request.Form.Get("txt_cjjg")) ? "" : Request.Form.Get("txt_cjjg");
                modelkz.ZXBD = string.IsNullOrEmpty(Request.Form.Get("txt_zxby")) ? "" : Request.Form.Get("txt_zxby");
                modelkz.SLJG = string.IsNullOrEmpty(Request.Form.Get("txt_shjg")) ? "" : Request.Form.Get("txt_shjg");
                modelkz.ZXJG = string.IsNullOrEmpty(Request.Form.Get("txt_zxjg")) ? "" : Request.Form.Get("txt_zxjg");
                modelkz.JAFS = string.IsNullOrEmpty(Request.Form.Get("txt_jafs")) ? "" : Request.Form.Get("txt_jafs");
                modelkz.GLDAXLH = string.IsNullOrEmpty(Request.Form.Get("txt_gldaxlh")) ? "" : Request.Form.Get("txt_gldaxlh");
                modelkz.HYTCY = string.IsNullOrEmpty(Request.Form.Get("txt_hytcy")) ? "" : Request.Form.Get("txt_hytcy");
                modelkz.CBR = string.IsNullOrEmpty(Request.Form.Get("txt_cbr")) ? "" : Request.Form.Get("txt_cbr");
                modelkz.SJY = string.IsNullOrEmpty(Request.Form.Get("txt_sjy")) ? "" : Request.Form.Get("txt_sjy");
                modelkz.ZCS = string.IsNullOrEmpty(Request.Form.Get("txt_bjgjc")) ? 0 : Convert.ToDecimal(Request.Form.Get("txt_bjgjc"));
                modelkz.DJC = string.IsNullOrEmpty(Request.Form.Get("txt_sdjc")) ? 0 : Convert.ToDecimal(Request.Form.Get("txt_sdjc"));
                modelkz.YS = string.IsNullOrEmpty(Request.Form.Get("txt_ngjy")) ? 0 : Convert.ToDecimal(Request.Form.Get("txt_ngjy"));
                modelkz.GDRQ = Convert.ToDateTime(Request.Form.Get("txt_gdrq"));
                modelkz.YWT = string.IsNullOrEmpty(Request.Form.Get("txt_ywt")) ? "" : Request.Form.Get("txt_ywt");
                modelkz.BGQX = string.IsNullOrEmpty(Request.Form.Get("txt_bgqx")) ? "" : Request.Form.Get("txt_bgqx");
                modelkz.AJLBBM = string.IsNullOrEmpty(Request.Form.Get("txt_z_val")) ? "" : Request.Form.Get("txt_z_val");
                modelkz.AJLBMC = string.IsNullOrEmpty(Request.Form.Get("txt_z")) ? "" : Request.Form.Get("txt_z");
                modelkz.H = Convert.ToDecimal(Request.Form.Get("txt_h"));
                modelkz.NF = string.IsNullOrEmpty(Request.Form.Get("txt_nf")) ? "" : Request.Form.Get("txt_nf");
                modelkz.YWLBBM = string.IsNullOrEmpty(ywbm) ? "" : ywbm;
                modelkz.YWLBMC = string.IsNullOrEmpty(ywlx) ? "" : ywlx;
                modelkz.DWBM = UserInfo.DWBM;
                modelkz.DWMC = UserInfo.DWMC;
                modelkz.DWJC = UserDwbm.DWJC;
                modelkz.QAH = "";
                modelkz.MLH = "G" + ywbm + "." + modelkz.AJLBBM;
                modelkz.JH = "";
                modelkz.CZRGH = UserInfo.GH;
                modelkz.CZRMC = UserInfo.MC;
                modelkz.CZSJ = DateTime.Now;

                #endregion

                #region 操作制作参数
                EDRS.BLL.XY_DZJZ_XTPZ xtpzbll = new EDRS.BLL.XY_DZJZ_XTPZ(Request);

                EDRS.Model.XY_DZJZ_XTPZ xtpz = xtpzbll.GetConfigById(EnumConfig.卷宗文件上传地址);
                if (xtpz == null)
                    return ReturnString.JsonToString(Prompt.error, "请先配置卷宗文件上传地址", null);
                string linkType = string.Empty; //链接类型
                string callBack = string.Empty; //路由链接模式地址              

                XT_DZJZ_ZZCS zzcsbll = new XT_DZJZ_ZZCS(Request);
                string bmbm = "";
                string bmmc = "";
                if (UserRole != null && UserRole.Count > 0)
                {
                    bmbm = UserRole[0].BMBM;
                    bmmc = UserRole[0].BMMC;
                }

                string strstring = string.Empty;
                string[] zks = new string[] { "BMSAH", "SFDC", "DWBM", "MC", "GH", "DWMC", "BMBM", "BMMC", "LJLX", "DZ1", "DZ2", "AJBH", "WSBH", "WSMC" };
                List<EDRS.Model.XT_DZJZ_ZZCS> zzcsList = new List<EDRS.Model.XT_DZJZ_ZZCS>();
                EDRS.Model.XT_DZJZ_ZZCS zzcsModel = null;
                string guid = Guid.NewGuid().ToString();
                foreach (string dr in zks)
                {

                    //制作参数(部门受案号，是否导出，单位编码，用户名称，单位名称，部门编码，部门名称，连接类型，文件上传地址，回调地址)<add key="ZzCsKey" value="BMSAH,SFDC,DWBM,MC,GH,DWMC,BMBM,BMMC,LJLX,DZ1,DZ2"/>    
                    zzcsModel = new EDRS.Model.XT_DZJZ_ZZCS();
                    zzcsModel.CSKEY = dr;
                    zzcsModel.FZBS = guid;
                    zzcsModel.DYSJ = DateTime.Now;

                    switch (zzcsModel.CSKEY)
                    {
                        case "BMSAH":
                            zzcsModel.CSVALUE = model.BMSAH;
                            break;
                        case "SFDC":
                            zzcsModel.CSVALUE = type;
                            break;
                        case "DWBM":
                            zzcsModel.CSVALUE = base.UserInfo.DWBM;
                            break;
                        case "MC":
                            zzcsModel.CSVALUE = base.UserInfo.MC;
                            break;
                        case "GH":
                            zzcsModel.CSVALUE = UserInfo.GH;
                            break;
                        case "DWMC":
                            zzcsModel.CSVALUE = UserInfo.DWMC;
                            break;
                        case "BMBM":
                            zzcsModel.CSVALUE = bmbm;
                            break;
                        case "BMMC":
                            zzcsModel.CSVALUE = bmmc;
                            break;
                        case "LJLX":
                            object obj3 = xtpzbll.GetConfigById(EnumConfig.连接类型);
                            if (obj3 != null)
                                linkType = zzcsModel.CSVALUE = ((EDRS.Model.XY_DZJZ_XTPZ)obj3).CONFIGVALUE;
                            else
                                zzcsModel.CSVALUE = "";
                            break;
                        case "DZ1":
                            zzcsModel.CSVALUE = xtpz.CONFIGVALUE;
                            break;
                        case "DZ2":
                            object obj4 = xtpzbll.GetConfigById(EnumConfig.路由映射地址);
                            if (obj4 != null && obj4 != DBNull.Value)
                            {
                                callBack = zzcsModel.CSVALUE = ((EDRS.Model.XY_DZJZ_XTPZ)obj4).CONFIGVALUE;
                            }
                            else
                                zzcsModel.CSVALUE = "";
                            break;
                        case "AJBH":
                            zzcsModel.CSVALUE = ajbh;
                            break;
                        case "WSBH":
                            zzcsModel.CSVALUE = wsbh;
                            break;
                        case "WSMC":
                            zzcsModel.CSVALUE = wsmc;
                            break;
                        default:
                            zzcsModel.CSVALUE = "";
                            break;
                    }

                    zzcsList.Add(zzcsModel);
                }
                if (zzcsbll.AddList(zzcsList))
                {
                    strstring = xtpz.CONFIGVALUE + "@" + linkType + "@" + callBack + "@" + guid + "@" + type;
                }
                #endregion

                EDRS.BLL.TYYW_GG_AJJBXX bll = new TYYW_GG_AJJBXX(Request);
                //判断存在调用修改
                if (bll.GetRecordCount(" and BMSAH=:BMSAH", new object[] { bmsah }) > 0)
                {
                    if (bll.UpdateList(model, modelkz, jbxxmodel))
                    {
                        OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "添加" + ((VersionName)0).ToString() + "成功", model.BMSAH, UserInfo, UserRole, this.Request);
                        return ReturnString.JsonToString(Prompt.win, DESEncrypt.Encrypt(strstring), null);
                    }
                    OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "保存数据失败", model.BMSAH, UserInfo, UserRole, this.Request);
                    return ReturnString.JsonToString(Prompt.error, "保存数据失败", null);
                }
                if (bll.AddList(model, modelkz, jbxxmodel))
                {
                    if (xtpz != null)
                    {
                        OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "添加" + ((VersionName)0).ToString() + "成功", model.BMSAH, UserInfo, UserRole, this.Request);
                        return ReturnString.JsonToString(Prompt.win, DESEncrypt.Encrypt(strstring), null);

                    }
                    return ReturnString.JsonToString(Prompt.win, "未设置服务链接地址", null);
                }

                OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "添加" + ((VersionName)0).ToString() + "失败", model.BMSAH, UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.error, "添加" + ((VersionName)0).ToString() + "失败", null);
            }
            return ReturnString.JsonToString(Prompt.error, "请将" + ((VersionName)0).ToString() + "信息填写完整", null);

        }
        #endregion

        #region 修改配置数据
        /// <summary>
        /// 修改配置数据
        /// </summary>
        /// <returns></returns>
        private string UpData()
        {
            string pzbm = Request.Form.Get("key_hidd").Trim();
            if (string.IsNullOrEmpty(pzbm))
            {
                return ReturnString.JsonToString(Prompt.error, "未找到需要修改信息", null);
            }
            string msg = string.Empty;
            if (!ProvingFrom(ref msg))
            {
                return ReturnString.JsonToString(Prompt.error, msg, null);
            }

            YX_DZJZ_FMDY bll = new YX_DZJZ_FMDY(this.Request);
            EDRS.Model.YX_DZJZ_FMDY model = bll.GetModel(pzbm);
            if (model != null)
            {
                model.BT = Request.Form.Get("txt_bt");
                model.FBT = Request.Form.Get("txt_fbt");
                model.AJMC = Request.Form.Get("txt_ajmc");
                model.AJBH = Request.Form.Get("txt_ajbh");
                model.FZXYR = Request.Form.Get("txt_fzxyr");
                if (!string.IsNullOrEmpty(Request.Form.Get("txt_lasj")))
                    model.LASJ = Convert.ToDateTime(Request.Form.Get("txt_lasj"));
                if (!string.IsNullOrEmpty(Request.Form.Get("txt_jasj")))
                    model.JASJ = Convert.ToDateTime(Request.Form.Get("txt_jasj"));
                model.LJDW = Request.Form.Get("txt_ljdw");
                model.LJR = Request.Form.Get("txt_ljr");
                model.SHR = Request.Form.Get("txt_shr");
                model.BAGJ = string.IsNullOrEmpty(Request.Form.Get("txt_bagj")) ? 0 : int.Parse(Request.Form.Get("txt_bagj"));
                model.DJJ = Request.Form.Get("txt_djj");
                model.GJY = string.IsNullOrEmpty(Request.Form.Get("txt_gjy")) ? 0 : int.Parse(Request.Form.Get("txt_gjy"));
                model.CZRGH = UserInfo.GH;
                model.CZR = UserInfo.MC;
                model.CZSJ = DateTime.Now;
                model.CZIP = Request.ServerVariables["REMOTE_ADDR"];
                model.CZLX = "";
                if (bll.Update(model))
                {
                    //数据日志
                    OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "修改封面打印成功","", UserInfo, UserRole, this.Request);
                    return ReturnString.JsonToString(Prompt.win, "保存成功", null);
                }
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "修改封面打印失败", "", UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.error, "保存失败", null);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "修改封面打印未找到信息", "", UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "未找到需要修改信息", null);
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        private string DelData()
        {
            string ids = Request.Form["id"];
            string[] id = ids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            ids = "";
            for (int i = 0; i < id.Length; i++)
            {
                ids += "'" + id[i].Trim() + "'";
                if (i < id.Length - 1)
                    ids += ",";
            }
            YX_DZJZ_FMDY bll = new YX_DZJZ_FMDY(this.Request);

            if (bll.DeleteList(ids))
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "删除封面打印成功", Request.Form["cs"], UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.win, "删除数据成功", null);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "删除封面打印失败", Request.Form["cs"], UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "删除数据失败", null);
        }
        #endregion

        #region 根据单位获取配置数据
        /// <summary>
        /// 根据单位获取配置数据
        /// </summary>
        /// <param name="DWBM"></param>
        /// <returns></returns>
        private string GetModel(string PZBM)
        {
            if (string.IsNullOrEmpty(PZBM))
            {
                PZBM = Request["id"];
                if (string.IsNullOrEmpty(PZBM))
                {
                    return ReturnString.JsonToString(Prompt.error, "参数错误", null);
                }
            }
            YX_DZJZ_FMDY bll = new YX_DZJZ_FMDY(this.Request);
            EDRS.Model.YX_DZJZ_FMDY model = bll.GetModel(PZBM);
            if (model != null)
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "根据编号获取封面打印信息成功", "", UserInfo, UserRole, this.Request);
                return JsonHelper.JsonString(model);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "根据编号获取封面打印信息参数失败", Request["cs"], UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "获取数据失败", null);
        }
        #endregion

        #region 表单验证
        /// <summary>
        /// 表单验证
        /// </summary>
        private bool ProvingFrom(ref string msg)
        {
            string bt = Request.Form.Get("txt_bt");
            string fbt = Request.Form.Get("txt_fbt");
            if (string.IsNullOrEmpty(bt))
            {
                msg = "请输入标题！";
                return false;
            }
            if (string.IsNullOrEmpty(fbt))
            {
                msg = "请输入副标题！";
                return false;
            }
            return true;
        }
        #endregion

        #region 获取部门受案号
        /// <summary>
        /// 获取部门受案号
        /// </summary>
        /// <returns></returns>
        private string GetBmsah()
        {
            string nf = Request.Form["nf"];
            string z = Request.Form["z"];
            string h = Request.Form["h"];
            string bmsah = string.Empty;
            if (string.IsNullOrEmpty(nf))
            return ReturnString.JsonToString(Prompt.error, "请选择年份", null);
            if (string.IsNullOrEmpty(z))
                return ReturnString.JsonToString(Prompt.error, "请选择字", null);
            if (string.IsNullOrEmpty(h))
                return ReturnString.JsonToString(Prompt.error, "请输入号", null);

            bmsah = string.Format("({0}){1}{2}字第{3}号", nf, UserDwbm.DWJC, z,h);
            //EDRS.BLL.TYYW_GG_AJJBXX bll = new TYYW_GG_AJJBXX(Request);
            
            //if (bll.GetModel(bmsah) == null)
            //{

            return GetModelByBmsah(bmsah);
            //    return ReturnString.JsonToString(Prompt.win, bmsah, null); 
            //}
            //return ReturnString.JsonToString(Prompt.win, "该案号名称名称已存在", null); 
 
        } 
        #endregion

        #region 根据部门受案号获取封面打印信息
        /// <summary>
        /// 根据部门受案号获取封面打印信息
        /// </summary>
        /// <returns></returns>
        private string GetModelByBmsah(string bmsah)
        {
            EDRS.BLL.TYYW_GG_AJJBXXKZ bll = new TYYW_GG_AJJBXXKZ(Request);
            EDRS.Model.TYYW_GG_AJJBXXKZ model = bll.GetModel(bmsah);
            if (model != null)
            {
                return JsonHelper.JsonString(model);
            }
            return ReturnString.JsonToString(Prompt.win, bmsah, null);
        } 
        #endregion

    }
}