using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using EDRS.BLL;
using EDRS.Common;
using System.Text;
using System.Reflection;
using System.Data;
using System.Net;
using System.IO;
using EDRS.Common.DEncrypt;
using System.Threading;

namespace WebUI
{

    public partial class CaseInfoManage : BasePage
    {
        Thread thread = null;

        System.Web.HttpRequest thisreq;

        protected void Page_Load(object sender, EventArgs e)
        {

            string[] t = Request.PathInfo.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            string type = Request["t"];

            if (!string.IsNullOrEmpty(type))
            {
               
                Response.ContentEncoding = Encoding.UTF8;
                Response.ContentType = "application/json";
                if (type.Equals("ListBind"))
                {
                    //测试自动添加案件
                    //thisreq = Request;
                    //thread = new Thread(new ThreadStart(AddTest));
                    //thread.Start();
                    
                 
                    //权限状态
                    TYYW_GG_AJJBXX bll = new TYYW_GG_AJJBXX(Request);
                    string data = bll.ListBin(this.Request, UserInfo.DWBM, UserInfo.GH, base.GetBmBm(), Jsbms,base.GetJsQxzt());
                    //OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "案件数据查询", UserInfo, UserRole, this.Request);
                    Response.Write(data);
                }
                if (type.Equals("UpData"))
                    Response.Write(UpData());
                if (type.Equals("DelData"))
                    Response.Write(DelData());
                if (type.Equals("GetData"))
                    Response.Write(GetModel(""));
                if (type.Equals("GetMake"))
                    Response.Write(GetMake(""));
                if (type.Equals("RomIsLock"))
                    Response.Write(RomIsLock());
                if (type.Equals("AddData"))
                    Response.Write(AddData());
                if (type.Equals("IAddData"))
                    Response.Write(IAddData());
                if (type.Equals("getajmb"))
                    Response.Write(GetMBList());
                if (type.Equals("getrybm"))
                    Response.Write(GetRybmList());

                Response.End();
            }

           
        }

        public void AddTest()
        {
            for (int i = 3000; i < 3050; i++)
            {
                AddDataTest(i, thisreq);

                Thread.Sleep(500);
            }
        }

        #region 暂不使用
        /// <summary>
        /// 获取用户权限列表 暂不使用
        /// </summary>
        /// <param name="dwbm"></param>
        /// <param name="gh"></param>
        /// <returns></returns>
        private List<string> GetUserRoleList(string dwbm, string gh)
        {
            EDRS.BLL.XT_QX_RYJZQXFP bll = new EDRS.BLL.XT_QX_RYJZQXFP(Request);
            return null;
            return bll.GetRyJzQxFpList(dwbm, gh);
        }
        #endregion


        #region 卷宗制作
        /// <summary>
        /// 卷宗制作
        /// </summary>
        /// <param name="BMSAH"></param>
        /// <returns></returns>
        private string GetMake(string BMSAH)
        {
            try
            {

                string dwbm = "";
                string ajbh = "";
                string wsbh = "";
                string wsmc="";
                if (string.IsNullOrEmpty(BMSAH))
                {

                    BMSAH = Request["id"];
                    dwbm = Request["dwbm"];
                    ajbh = Request["ajbh"];
                    wsbh = Request["wsbh"];
                    wsmc=Request.Form["wsmc"];
                    if (string.IsNullOrEmpty(BMSAH))
                    {
                        return ReturnString.JsonToString(Prompt.error, "参数错误", null);
                    }
                }
                string type = Request["type"];
                string interfaceType = Request["interfaceType"];

                #region 原件导出和pdf导出控制按钮权限
                if (interfaceType == "5")
                {
                    if (!base.IsAnRole("caseinfomanage_yjdc"))
                        return ReturnString.JsonToString(Prompt.error, "没有权限，请先设置权限", null);
                }
                if (interfaceType == "8")
                {
                    if (!base.IsAnRole("caseinfomanage_pdfdc"))
                        return ReturnString.JsonToString(Prompt.error, "没有权限，请先设置权限", null);
                } 
                #endregion

                //判断是否为导出
                if (type == "btn_derive")
                    type = "1";
                else
                    type = "0";

                TYYW_GG_AJJBXX jzajxx = new TYYW_GG_AJJBXX(this.Request);
                EDRS.Model.TYYW_GG_AJJBXX model = jzajxx.GetModel(BMSAH);
                
                

                //读取配置文件中的配置
                int? isLocalAjxx = ConfigHelper.GetConfigInt("IsLocalAjxx");

                if (isLocalAjxx == 1)
                {
                    if (model == null)
                    {
                        //本地数据不存在执行添加到本地
                        model = jzajxx.GetIceByModel(BMSAH, jzajxx.GetAjTypeBm(Request, UserInfo.DWBM, "", UserInfo.GH), GetUserRoleList(UserInfo.DWBM, UserInfo.GH), dwbm, UserInfo.GH);
                        if (model == null || !jzajxx.Add(model))
                        {
                            OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, ((VersionName)0).ToString() + "制作添加本地数据失败", BMSAH, UserInfo, UserRole, this.Request);
                            return ReturnString.JsonToString(Prompt.error, "获取" + ((VersionName)0).ToString() + "失败", null);
                        }
                    }
                    else
                    {
                        //本地数据存在执行更新本地数据
                        model = jzajxx.GetIceByModel(BMSAH, jzajxx.GetAjTypeBm(Request, UserInfo.DWBM, "", UserInfo.GH), GetUserRoleList(UserInfo.DWBM, UserInfo.GH), model.CBDW_BM, UserInfo.GH);
                        if (model == null || !jzajxx.Update(model))
                        {
                            OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, ((VersionName)0).ToString() + "制作修改本地数据失败", BMSAH, UserInfo, UserRole, this.Request);
                            return ReturnString.JsonToString(Prompt.error, "获取" + ((VersionName)0).ToString() + "失败", null);
                        }
                    }
                }
                if (model != null)
                {
                    //0制作，1导出
                    EDRS.Model.XY_DZJZ_XTPZ xtpz = GetConfigById(EnumConfig.卷宗文件上传地址);
                    if (xtpz != null)
                    {
                        string bmbm = "";
                        string bmmc = "";
                        if (UserRole != null && UserRole.Count > 0)
                        {
                            bmbm = UserRole[0].BMBM;
                            bmmc = UserRole[0].BMMC;
                        }

                        //string strstring = model.BMSAH + "_" + type + "@" + base.UserInfo.DWBM + "@" + base.UserInfo.MC + "@" + GetConfigById(EnumConfig.卷宗文件上传地址).CONFIGVALUE + "@" + UserInfo.GH + "@" + UserInfo.DWMC + "@" + bmbm + "@" + bmmc;

                        //strstring = DESEncrypt.Encrypt(strstring);

                        //string d = DESEncrypt.Decrypt(strstring);

                        //OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "案件制作成功", model.BMSAH, UserInfo, UserRole, this.Request);
                        XT_DZJZ_ZZCS zzcsbll = new XT_DZJZ_ZZCS(Request);

                        string[] zks = new string[] { "BMSAH", "SFDC", "DWBM", "MC", "GH", "DWMC", "BMBM", "BMMC", "LJLX", "DZ1", "DZ2", "AJBH", "WSBH","WSMC" };
                        string callBack = string.Empty;
                        string linkType = string.Empty;

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
                                    object obj = GetConfigById(EnumConfig.连接类型);
                                    if (obj != null)
                                        linkType = zzcsModel.CSVALUE = ((EDRS.Model.XY_DZJZ_XTPZ)obj).CONFIGVALUE;
                                    else
                                        zzcsModel.CSVALUE = "";
                                    break;
                                case "DZ1":
                                    zzcsModel.CSVALUE = xtpz.CONFIGVALUE;
                                    break;
                                case "DZ2":
                                    object obj2 = GetConfigById(EnumConfig.路由映射地址);
                                    if (obj2 != null && obj2 != DBNull.Value)
                                    {
                                        callBack = zzcsModel.CSVALUE = ((EDRS.Model.XY_DZJZ_XTPZ)obj2).CONFIGVALUE;
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
                            }
                            zzcsList.Add(zzcsModel);
                        }
                        if (zzcsbll.AddList(zzcsList))
                        {

                            //string strstring = Request.Url.Authority + "@" + guid;

                            //TH:2016-05-23
                            //string strstring = Request.Url.Authority + "@" + guid;
                            string strstring = xtpz.CONFIGVALUE + "@" + linkType + "@" + callBack + "@" + guid + "@" + interfaceType;
                            return "{\"parm\":\"" + DESEncrypt.Encrypt(strstring) + "\"}";

                        }
                    }
                    else
                    {
                        OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "未配置卷宗服务地址，请先到参数配置中配置", BMSAH, UserInfo, UserRole, this.Request);
                        return ReturnString.JsonToString(Prompt.error, "未配置卷宗服务地址，请先到参数配置中配置", null);
                    }
                }
                OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, ((VersionName)0).ToString() + "制作失败", BMSAH, UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.error, "获取" + ((VersionName)0).ToString() + "失败", null);

            }
            catch (Exception ex)
            {
                OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, ((VersionName)0).ToString() + "制作失败异常" + ex.Message, BMSAH, UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.error, "获取" + ((VersionName)0).ToString() + "失败,错误：" + ex.Message.Replace("\"", ""), null);
            }
        }
        #endregion

        //#region 添加数据
        ///// <summary>
        ///// 添加数据
        ///// </summary>
        ///// <returns></returns>
        //public string AddData()
        //{
        //    EDRS.Model.YX_DZJZ_JZAJXX model = new EDRS.Model.YX_DZJZ_JZAJXX();
        //    model.BMSAH = Request.Form.Get("txt_BMSAH");
        //    model.AJMC = Request.Form.Get("txt_AJMC");
        //    model.AJLB_BM = Request.Form.Get("sct_AJLB_BM");
        //    model.AJLB_BM = Request.Form.Get("sct_CBDWBM");
        //    model.FQL = "4";
        //    YX_DZJZ_JZAJXX jzajxx = new YX_DZJZ_JZAJXX(this.Request);
        //    if (jzajxx.Add(model))
        //        return ReturnString.JsonToString(Prompt.win, "保存成功", null);
        //    return ReturnString.JsonToString(Prompt.error, "保存失败", null);
        //} 
        //#endregion

        //#region 编辑数据
        ///// <summary>
        ///// 编辑数据
        ///// </summary>
        ///// <returns></returns>
        //public string UpData()
        //{
        //    YX_DZJZ_JZAJXX jzajxx = new YX_DZJZ_JZAJXX(this.Request);
        //    EDRS.Model.YX_DZJZ_JZAJXX model = jzajxx.GetModel(Request.Form.Get("key_hidd"));
        //    if (model != null)
        //    {
        //        model.BMSAH = Request.Form.Get("txt_BMSAH");
        //        model.AJMC = Request.Form.Get("txt_AJMC");
        //        model.AJLB_BM = Request.Form.Get("sct_AJLB_BM");
        //        model.AJLB_BM = Request.Form.Get("sct_CBDWBM");
        //        model.FQL = "4";
        //        if (jzajxx.Update(model))
        //            return ReturnString.JsonToString(Prompt.win, "修改成功", null);
        //    }
        //    return ReturnString.JsonToString(Prompt.error, "修改失败", null);
        //} 
        //#endregion

        //#region 删除
        ///// <summary>
        ///// 删除
        ///// </summary>
        ///// <returns></returns>
        //private string DelData()
        //{
        //    string ids = Request.Form["BMSAH"];
        //    string[] id = ids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        //    ids="";
        //    for (int i = 0; i < id.Length; i++)
        //    {
        //        ids += "'" + id[i] + "'";
        //        if (i < id.Length - 1)
        //            ids += ",";
        //    }
        //    YX_DZJZ_JZAJXX jzajxx = new YX_DZJZ_JZAJXX(this.Request);
        //    if (jzajxx.DeleteList(ids))
        //        return ReturnString.JsonToString(Prompt.win, "删除数据成功", null);
        //    return ReturnString.JsonToString(Prompt.error, "删除数据失败", null);
        //} 
        //#endregion

        //#region 根据编号获取数据
        ///// <summary>
        ///// 根据编号获取数据
        ///// </summary>
        ///// <returns></returns>
        //public string GetModel(string BMSAH)
        //{
        //    if (string.IsNullOrEmpty(BMSAH))
        //    {
        //        BMSAH = Request["id"];
        //        if(string.IsNullOrEmpty(BMSAH))
        //            return ReturnString.JsonToString(Prompt.error, "参数错误", null);
        //    }
        //    YX_DZJZ_JZAJXX jzajxx = new YX_DZJZ_JZAJXX(this.Request);
        //    EDRS.Model.YX_DZJZ_JZAJXX model = jzajxx.GetModel(BMSAH);
        //    if (model != null)
        //        return JsonHelper.JsonString(model);
        //    return ReturnString.JsonToString(Prompt.error, "获取数据失败", null);
        //} 
        //#endregion

        #region 解除卷宗案件基本信息锁定状态
        /// <summary>
        /// 解除卷宗案件基本信息锁定状态
        /// </summary>
        /// <returns></returns>
        private string RomIsLock()
        {
            string BMSAH = Request.Form["BMSAH"];
            if (BMSAH == null || string.IsNullOrEmpty(BMSAH))
            {
                return ReturnString.JsonToString(Prompt.error, "解锁参数不正确", null);
            }

            YX_DZJZ_JZJBXX jzjbxx = new YX_DZJZ_JZJBXX(this.Request);
            List<EDRS.Model.YX_DZJZ_JZJBXX> jblist = jzjbxx.GetModelList(" and BMSAH=:BMSAH and SFSC='N'", new object[] { BMSAH });

            if (jblist != null && jblist.Count > 0)
            {
                EDRS.Model.YX_DZJZ_JZJBXX model = jblist[0];
                model.JZXGH = " ";
                model.ZHXGSJ = DateTime.Now;
                if (jzjbxx.Update(model))
                {
                    OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, ((VersionName)0).ToString() + "解锁成功", BMSAH, UserInfo, UserRole, this.Request);
                    return ReturnString.JsonToString(Prompt.win, "解锁成功", null);
                }
            }
            OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, ((VersionName)0).ToString() + "解锁失败", BMSAH, UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "解锁失败", null);
        }
        #endregion

        #region 根据系统配置的配置编号获取对应的配置信息
        /// <summary>
        /// 根据系统配置的配置编号获取对应的配置信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EDRS.Model.XY_DZJZ_XTPZ GetConfigById(EnumConfig econfig)
        {
            string where = string.Empty;

            object[] values = new object[1];

            where = " and CONFIGID=:CONFIGID and SYSTEMMARK=1";
            values[0] = (int)econfig;


            EDRS.BLL.XY_DZJZ_XTPZ bll = new EDRS.BLL.XY_DZJZ_XTPZ(this.Request);

            List<EDRS.Model.XY_DZJZ_XTPZ> modelList = bll.GetModelList(where, values);

            if (modelList != null && modelList.Count > 0)
                return modelList[0];
            return null;
        }
        #endregion

        #region 编辑数据
        /// <summary>
        /// 编辑数据
        /// </summary>
        /// <returns></returns>
        public string UpData()
        {
            string ajmc = Request.Form.Get("txt_ajmc");
            string lbmc = Request.Form.Get("txt_lbmc");
            string xyr = Request.Form.Get("txt_xyr");
            string sfzh = Request.Form.Get("txt_sfzh");
            string taryxx = Request.Form.Get("txt_taryxx");
            string ajlb = Request.Form.Get("txt_ajlb");
            string bmsah = Request.Form.Get("hidd_bmsah");

            string ajbh = Request.Form.Get("txt_ajbh"); //案件编号
            string lasj = Request.Form.Get("txt_lasj"); //立案时间
            string jasj = Request.Form.Get("txt_jasj"); //结案时间
            string ladw = Request.Form.Get("txt_ladw_val"); //立卷单位
            string ladw_mc = Request.Form.Get("txt_ladw"); //立卷单位名称
            string ljr = Request.Form.Get("txt_lar_val"); //立卷人
            string ljr_mc = Request.Form.Get("txt_lar"); //立卷人名称
            string shr = Request.Form.Get("txt_shr"); //审核人
            string zjs = (string.IsNullOrEmpty(Request.Form.Get("txt_zjs")) ? "0" : Request.Form.Get("txt_zjs")); //本案共卷
            string djj = Request.Form.Get("txt_djj"); //第几卷
            string djy = (string.IsNullOrEmpty(Request.Form.Get("txt_djy")) ? "0" : Request.Form.Get("txt_djy")); //第几页

            string wsbh = Request.Form.Get("wsbh_hidd");
            string wsmc = Request.Form.Get("txt_wsbh"); //txt_wsbh

            //案件编号
            if (string.IsNullOrEmpty(ajbh))
            {
                return ReturnString.JsonToString(Prompt.error, "必须填写案件编号", null);
            }
            //判断案件名称
            if (string.IsNullOrEmpty(ajmc))
            {
                return ReturnString.JsonToString(Prompt.error, "必须填写案件名称", null);
            }
            //判断案件类别
            if (string.IsNullOrEmpty(ajlb))
            {
                return ReturnString.JsonToString(Prompt.error, "必须填写案件类别", null);
            }
            //立案时间
            if (string.IsNullOrEmpty(lasj))
            {
                return ReturnString.JsonToString(Prompt.error, "必须选择立案时间", null);
            }
            //立卷单位
            if (string.IsNullOrEmpty(ladw))
            {
                return ReturnString.JsonToString(Prompt.error, "必须选择立卷单位", null);
            }

            if (string.IsNullOrEmpty(bmsah))
                return ReturnString.JsonToString(Prompt.error, "未指定修改" + ((VersionName)0).ToString() + "，请重新选择修改" + ((VersionName)0).ToString(), null);

            YX_DZJZ_JZJBXX bll = new YX_DZJZ_JZJBXX(Request);
            if (!string.IsNullOrEmpty(ajbh))
            {
              //  List<EDRS.Model.YX_DZJZ_JZJBXX> list = bll.GetModelList(" and ajbh=:ajbh and wsbh=:wsbh and SFSC='N' and bmsah<>:bmsah ", new object[] { ajbh, wsbh ,bmsah});

                int dataCount = bll.GetRecordCount(" and ajbh=:ajbh and wsbh=:wsbh and SFSC='N' and bmsah<>:bmsah ", new object[] { ajbh, wsbh, bmsah });
                if (dataCount > 0)
                {
                    return ReturnString.JsonToString(Prompt.error, "案件信息【" + ajbh + "】已存在，请不要重复添加！", null);
                }
            }
            if (!string.IsNullOrEmpty(ajmc) && !string.IsNullOrEmpty(ajlb))
            {
                TYYW_GG_AJJBXX jzajxx = new TYYW_GG_AJJBXX(this.Request);
                EDRS.Model.TYYW_GG_AJJBXX model = jzajxx.GetModel(bmsah);
                if (model != null)
                {
                    model.AJMC = ajmc;
                    model.AJLB_BM = ajlb;
                    model.AJLB_MC = lbmc;
                    model.XYR = xyr;
                    model.SFZH = sfzh;
                    model.TARYXX = taryxx;
                    model.SLRQ = (string.IsNullOrEmpty(lasj) ? DateTime.Now : Convert.ToDateTime(lasj));// DateTime.Now;
                    if (!string.IsNullOrEmpty(jasj))
                        model.BJRQ = Convert.ToDateTime(jasj);
                    if (!string.IsNullOrEmpty(ladw) && ladw != "null")
                    {
                        model.CBDW_BM = ladw;
                        model.CBDW_MC = ladw_mc;
                    }
                    model.CBR = ljr_mc;
                    model.CBRGH = ljr;

                    //2016-6-6 增字段
                    model.SHR = shr; //审核人
                    model.SHSJ = DateTime.Now;//审核时间
                    model.ZJS = Convert.ToDecimal(zjs);//总卷数
                    model.DJJ = djj;//第几卷
                    model.ZYS = Convert.ToDecimal(djy);//总页数

                    if (jzajxx.Update(model))
                    {
                        List<EDRS.Model.YX_DZJZ_JZJBXX> list = bll.GetModelList(" and BMSAH ='" + bmsah + "'", null);
                        if (list != null && list.Count > 0)
                        {
                            EDRS.Model.YX_DZJZ_JZJBXX jbxxModel = list[0];
                            jbxxModel.JZMC = ajmc;
                            jbxxModel.Ajmb_bm = ajlb;
                            jbxxModel.Ajmb_mc = lbmc;
                            jbxxModel.WSBH = wsbh;
                            jbxxModel.WSMC = wsmc;
                            jbxxModel.ZHXGSJ = DateTime.Now;
                            jbxxModel.DWBM = model.CBDW_BM;
                            if (!bll.Update(jbxxModel))
                            {
                                OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "卷宗基本信息修改失败", model.BMSAH, UserInfo, UserRole, this.Request);
                                return ReturnString.JsonToString(Prompt.error, "卷宗基本信息修改失败", null);
                            }
                        }


                        OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "修改" + ((VersionName)0).ToString() + "成功", model.BMSAH, UserInfo, UserRole, this.Request);
                        return ReturnString.JsonToString(Prompt.win, "修改" + ((VersionName)0).ToString() + "成功", null);
                    }
                    OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "修改" + ((VersionName)0).ToString() + "失败", model.BMSAH, UserInfo, UserRole, this.Request);
                    return ReturnString.JsonToString(Prompt.error, "修改" + ((VersionName)0).ToString() + "失败", null);
                }
                return ReturnString.JsonToString(Prompt.error, "修改" + ((VersionName)0).ToString() + "不存在", null);
            }
            return ReturnString.JsonToString(Prompt.error, "请将" + ((VersionName)0).ToString() + "信息填写完整", null);
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
                ids += "'" + id[i] + "'";
                if (i < id.Length - 1)
                    ids += ",";
            }
            TYYW_GG_AJJBXX jzajxx = new TYYW_GG_AJJBXX(this.Request);
            if (jzajxx.DeleteList(ids))
            {
                OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "删除数据成功", ids, UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.win, "删除数据成功", null);
            }
            OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "删除数据失败", ids, UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "删除数据失败", null);
        }
        #endregion

        #region 调用客户端添加案件信息
        /// <summary>
        /// 调用客户端添加案件信息
        /// </summary>
        /// <returns></returns>
        private string IAddData()
        {
            try
            {
                string strstring = "abc_2@" + base.UserInfo.DWBM + "@" + base.UserInfo.MC + "@" + GetConfigById(EnumConfig.卷宗文件上传地址).CONFIGVALUE + "@" + UserInfo.GH + "@" + UserInfo.DWMC;
                strstring = DESEncrypt.Encrypt(strstring);
                OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "调用添加" + ((VersionName)0).ToString() + "接口", "", UserInfo, UserRole, this.Request);
                return "{\"parm\":\"" + strstring + "\"}";

            }
            catch (Exception)
            {
                OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "调用添加" + ((VersionName)0).ToString() + "接口出现错误", "", UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.error, "调用接口出现错误", null);
            }
        }
        #endregion

        #region 添加案件信息
        /// <summary>
        /// 添加案件信息
        /// </summary>
        /// <returns></returns>
        private string AddData()
        {
            string ajmc = Request.Form.Get("txt_ajmc");
            string lbmc = Request.Form.Get("txt_lbmc");
            string xyr = Request.Form.Get("txt_xyr");
            string sfzh = (string.IsNullOrEmpty(Request.Form.Get("txt_sfzh")) ? "" : Request.Form.Get("txt_sfzh"));
            string taryxx = (string.IsNullOrEmpty(Request.Form.Get("txt_taryxx")) ? "" : Request.Form.Get("txt_taryxx"));
            string ajlb = (string.IsNullOrEmpty(Request.Form.Get("txt_ajlb")) ? "" : Request.Form.Get("txt_ajlb"));

            string ajbh = Request.Form.Get("txt_ajbh"); //案件编号
            string lasj = Request.Form.Get("txt_lasj"); //立案时间
            string jasj = Request.Form.Get("txt_jasj"); //结案时间
            string ladw = Request.Form.Get("txt_ladw_val"); //立卷单位
            string ladw_mc = Request.Form.Get("txt_ladw"); //立卷单位名称
            string ljr = Request.Form.Get("txt_lar_val"); //立卷人
            string ljr_mc = Request.Form.Get("txt_lar"); //立卷人名称
            string shr = Request.Form.Get("txt_shr"); //审核人
            string zjs = ( string.IsNullOrEmpty(Request.Form.Get("txt_zjs")) ? "0" : Request.Form.Get("txt_zjs")); //本案共卷
            string djj = Request.Form.Get("txt_djj"); //第几卷
            string djy = (string.IsNullOrEmpty(Request.Form.Get("txt_djy")) ? "0" : Request.Form.Get("txt_djy")); //第几页
            string wsbh = Request.Form.Get("wsbh_hidd");
            string wsmc = Request.Form.Get("txt_wsbh");
            //案件编号
            if (string.IsNullOrEmpty(ajbh))
            {
                return ReturnString.JsonToString(Prompt.error, "必须填写案件编号", null);
            }
            //判断案件名称
            if (string.IsNullOrEmpty(ajmc))
            {
                return ReturnString.JsonToString(Prompt.error, "必须填写案件名称", null);
            }            
            //判断案件类别
            if (string.IsNullOrEmpty(ajlb))
            {
                return ReturnString.JsonToString(Prompt.error, "必须填写案件类别", null);
            }
            //立案时间
            if (string.IsNullOrEmpty(lasj))
            {
                return ReturnString.JsonToString(Prompt.error, "必须选择立案时间", null);
            }
            //立卷单位
            if (string.IsNullOrEmpty(ladw))
            {
                return ReturnString.JsonToString(Prompt.error, "必须选择立卷单位", null);
            }  

            EDRS.BLL.YX_DZJZ_JZJBXX jbxxbll = new YX_DZJZ_JZJBXX(Request);

            if (!string.IsNullOrEmpty(ajbh))
            {
                string where = string.Empty;
                object[] param = null;
                if (string.IsNullOrEmpty(wsbh))
                {
                    where = " and ajbh = :ajbh and (wsbh is null or wsbh='') and SFSC='N'";
                    param = new object[] { ajbh};
                }else
                {
                    where = " and ajbh = :ajbh and wsbh=:wsbh and SFSC='N'";
                   param = new object[] { ajbh, wsbh };
                }

                //if (!string.IsNullOrEmpty(wsbh))
                //{ 
                   //where = " and ajbh like :ajbh and wsbh=:wsbh and SFSC='N'";
                   //param = new object[] { ajbh + "%", wsbh };
                //}
                //else
                //{
                //   where = " and ajbh like :ajbh and SFSC='N'";
                //   param = new object[] { ajbh + "%" };
                //}
              //  List<EDRS.Model.YX_DZJZ_JZJBXX> list = jbxxbll.GetModelList(where, param);

              //  int dataCount = jbxxbll.GetRecordCount(where, param);
                //if (dataCount > 0)
                //{
                //    return ReturnString.JsonToString(Prompt.error, "案件【" + ajbh + "】已存在，请不要重复添加！", null);
                //}

                DataSet ds = jbxxbll.GetList(where,param);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    //{
                    //    if (ds.Tables[0].Rows[i]["DWBM"].ToString() == UserInfo.DWBM)
                    //    {
                            return ReturnString.JsonToString(Prompt.error, "案件【" + ajbh + "】已存在，请不要重复添加，或者选择其它文书号！", null);
                    //    }
                    //}
                    //ajbh += "_"+UserInfo.DWBM;
                }
               
            }

            //if (!string.IsNullOrEmpty(wsbh))
            //{
            //    List<EDRS.Model.YX_DZJZ_JZJBXX> list = jbxxbll.GetModelList(" and wsbh=:wsbh and SFSC='N'", new object[] { wsbh });

            //    if (list != null && list.Count > 0)
            //    {
            //        return ReturnString.JsonToString(Prompt.error, "文书编号【" + wsbh + "】已存在，请不要重复添加！", null);
            //    }
            //}


            if (!string.IsNullOrEmpty(ajmc) && !string.IsNullOrEmpty(ajlb))//&& !string.IsNullOrEmpty(ajbh)
            {
                //EDRS.BLL.XT_DM_AJLBBM ajlbbmBll = new XT_DM_AJLBBM(Request);
                //EDRS.Model.XT_DM_AJLBBM ajlbModel = ajlbbmBll.GetModel(ajlb);

                //if (ajlbModel != null)
                //    lbjc = ajlbModel.AJSLJC;
                //if (string.IsNullOrEmpty(lbjc))
                //    lbjc = lbmc;
                string bmsahName = ajlb + "[" + DateTime.Now.Year + "]" + UserInfo.DWBM;
                string num = "00001号";
                TYYW_GG_AJJBXX jzajxx = new TYYW_GG_AJJBXX(this.Request);
                DataSet ds = jzajxx.GetListByPage(" and bmsah like '" + bmsahName + "%'", "bmsah desc", 1, 1, null);
                if (ds != null && ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 1)
                {
                    num = ds.Tables[0].Rows[0]["bmsah"].ToString().Replace(bmsahName, "").Replace("号", "");
                    num = (Convert.ToInt32(num) + 1).ToString().PadLeft(5, '0') + "号";
                }
            
                EDRS.Model.TYYW_GG_AJJBXX model = new EDRS.Model.TYYW_GG_AJJBXX();
                //山东省院起诉受[2015]37000000072号
                model.BMSAH = bmsahName + num;
                model.TYSAH = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                model.SFSC = "N";
                model.SFYGXTSJ = "N";
                
                model.FQDWBM = Convert.ToDecimal(UserInfo.DWBM.Substring(0, 4));
                model.FQL = DateTime.Now.Year.ToString();
                model.CJSJ = DateTime.Now;
                model.ZHXGSJ = DateTime.Now;
                model.SLRQ = (string.IsNullOrEmpty(lasj)? DateTime.Now : Convert.ToDateTime(lasj));// DateTime.Now;
                model.AJMC = ajmc;
                model.AJLB_BM = ajlb;
                model.AJLB_MC = lbmc;
                model.XYR = xyr;
                model.SFZH = sfzh;
                model.TARYXX = taryxx;                

                if (!string.IsNullOrEmpty(jasj))
                    model.BJRQ = Convert.ToDateTime(jasj);
                if (!string.IsNullOrEmpty(ladw) && ladw != "null")
                {
                    model.CBDW_BM = ladw;
                    model.CBDW_MC = ladw_mc;
                }
                else
                {
                    model.CBDW_BM = UserInfo.DWBM;
                    model.CBDW_MC = UserInfo.DWMC;
                }
                model.CBR = ljr_mc;
                model.CBRGH = ljr;

                //2016-6-6 增字段
                model.SHR = shr; //审核人
                model.SHSJ = DateTime.Now;//审核时间
                model.ZJS = Convert.ToDecimal(zjs);//总卷数
                model.DJJ = djj;//第几卷
                model.ZYS =  Convert.ToDecimal(djy);//总页数

                if (jzajxx.Add(model))
                {
                    string count = "0";

                    count = jbxxbll.GetRecordCount("", null).ToString();
                    count = count.PadLeft(5, '0');

                    EDRS.Model.YX_DZJZ_JZJBXX jbxxmodel = new EDRS.Model.YX_DZJZ_JZJBXX();
                    jbxxmodel.JZBH = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    jbxxmodel.SFSC = "N";
                    jbxxmodel.CJSJ = DateTime.Now;
                    jbxxmodel.ZHXGSJ = DateTime.Now;
                    jbxxmodel.FQDWBM = 0;
                    jbxxmodel.FQL = "";                    
                    jbxxmodel.DWBM = model.CBDW_BM;
                    jbxxmodel.BMSAH = model.BMSAH;
                    jbxxmodel.TYSAH = UserInfo.DWBM + DateTime.Now.ToString("yyyyMM") + jbxxbll.GetRecordCount("", null) + count;
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
                    jbxxmodel.ZZZT = "-1";
                                    
                    if (jbxxbll.Add(jbxxmodel))
                    {
                        OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "添加" + ((VersionName)0).ToString() + "成功", model.BMSAH, UserInfo, UserRole, this.Request);
                        return ReturnString.JsonToString(Prompt.win, "添加" + ((VersionName)0).ToString() + "成功", null);
                    }
                    else
                        jzajxx.Delete(model.BMSAH);
                }
                OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "添加" + ((VersionName)0).ToString() + "失败", model.BMSAH, UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.error, "添加" + ((VersionName)0).ToString() + "失败", null);
            }
            return ReturnString.JsonToString(Prompt.error, "请将" + ((VersionName)0).ToString() + "信息填写完整", null);
        }
        #endregion

        #region 根据编号获取数据
        /// <summary>
        /// 根据编号获取数据
        /// </summary>
        /// <returns></returns>
        public string GetModel(string BMSAH)
        {
            if (string.IsNullOrEmpty(BMSAH))
            {
                BMSAH = Request["id"];
                if (string.IsNullOrEmpty(BMSAH))
                    return ReturnString.JsonToString(Prompt.error, "参数错误", null);
            }
            TYYW_GG_AJJBXX jzajxx = new TYYW_GG_AJJBXX(this.Request);
            EDRS.Model.TYYW_GG_AJJBXX model = jzajxx.GetModel(BMSAH);
            if (model != null)
                return JsonHelper.JsonString(model);
            return ReturnString.JsonToString(Prompt.error, "获取数据失败", null);
        }
        #endregion

        #region 获取模板
        /// <summary>
        /// 获取模板
        /// </summary>
        /// <returns></returns>
        private string GetMBList()
        {
            string ajlb = Request.Form.Get("ajlb");
            string strWhere = "";
            if (string.IsNullOrEmpty(ajlb))
            {
                if (string.IsNullOrEmpty(ajlb))
                    return ReturnString.JsonToString(Prompt.error, "参数错误", null);
            }
            else
                strWhere += " and ajlbbm='" + StringPlus.ReplaceSingle(ajlb) + "'";

            strWhere += " AND DWBM = '" + UserInfo.DWBM + "' AND JSBM in (" + Jsbms + ") AND BMBM in (" + Bmbms + ") AND QXLX = 0";

            XY_DZJZ_MBPZB bll = new XY_DZJZ_MBPZB(this.Request);
            int count;
            DataSet ds = bll.GetDwAjList(out count, strWhere, "dwbm", 0, int.MaxValue, null);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0].Copy();
                dt.Columns["DOSSIERTYPEVALUEMEMBER"].ColumnName = "id";
                dt.Columns["DOSSIERTYPEDISPLAYMEMBER"].ColumnName = "text";
                return JsonHelper.JsonString(dt);
            }

            return ReturnString.JsonToString(Prompt.error, "获取数据失败", null);
        }
        #endregion

        #region 获取人员列表
        /// <summary>
        /// 获取人员列表
        /// </summary>
        /// <returns></returns>
        private string GetRybmList()
        {
            string err = string.Empty;
            int count = 0;
            string xm = "";
            #region 获取参数
            if (!string.IsNullOrEmpty(Request.Params["condition"]))
            {
                xm = Request.Params["condition"].Trim();
                xm = JsonHelper.DeserializeObjectKey(xm, "value");
            }

            int pageindex = Convert.ToInt32(Request.Params["page"]);
            int rows = Convert.ToInt32(Request.Params["pagesize"]);
            #endregion

            StringBuilder strWhere = new StringBuilder();

            if (!string.IsNullOrEmpty(xm))
                strWhere.AppendFormat(" and mc like '%{0}%'", xm);


            DataTable dt = Cyvation.CCQE.BLL.ZzjgManage.GetRyList(this.UserInfo.DWBM, this.UserInfo.GH, strWhere.ToString(), rows, pageindex, out count, out err);

            string retValue = string.Empty;

            if (!string.IsNullOrEmpty(err))
            {
                OperateLog.AddLog(OperateLog.LogType.人员管理Web, "查询人员列表数据失败：" + err, UserInfo, UserRole, Request);
                retValue = err;
            }
            else
            {
                OperateLog.AddLog(OperateLog.LogType.人员管理Web, "查询人员列表数据成功！" + err, UserInfo, UserRole, Request);
                retValue = "{\"Total\":" + count + ",\"Rows\":" + JsonHelper.JsonString(dt) + "}";
                //retValue = "{\"Rows\":" + JsonHelper.JsonString(dt) + "}";
            }
            return retValue;

        } 
        #endregion





        #region (测试)添加案件信息
        /// <summary>
        /// (测试)添加案件信息
        /// </summary>
        /// <returns></returns>
        private void AddDataTest(int c,  System.Web.HttpRequest req)
        {
            string ajmc = "测试案件名称"+c;
            string ajlb = "020911";            
            string lbmc = "盗窃枪支、弹药案";//020911 //3A1802   冒用他人边境管理区通行证
            if (c % 4 == 0)
            {
                ajlb = "3A1802";
                lbmc = "冒用他人边境管理区通行证";
            }

            string xyr = "张某"+c;
            string sfzh = "";
            string taryxx = "";


            string ajbh = "ABCDE1"+c; //案件编号
            string lasj = DateTime.Now.ToString(); //立案时间
            string jasj = DateTime.Now.ToString(); //结案时间
            string ladw = UserInfo.DWBM; //立卷单位
            string ladw_mc = UserInfo.DWMC; //立卷单位名称
            string ljr = UserInfo.GH; //立卷人
            string ljr_mc = UserInfo.MC; //立卷人名称
            string shr = ""; //审核人
            string zjs = "10"; //本案共卷
            string djj = ""; //第几卷
            string djy ="0"; //第几页
            string wsbh = "XXXX"+c;
            string wsmc = "XXXXX文书名称"+c;
          

            EDRS.BLL.YX_DZJZ_JZJBXX jbxxbll = new YX_DZJZ_JZJBXX(req);


            if (!string.IsNullOrEmpty(ajmc) && !string.IsNullOrEmpty(ajlb))//&& !string.IsNullOrEmpty(ajbh)
            {             
                string bmsahName = ajlb + "[" + DateTime.Now.Year + "]" + UserInfo.DWBM;
                string num = "00001号";
                TYYW_GG_AJJBXX jzajxx = new TYYW_GG_AJJBXX(req);
                DataSet ds = jzajxx.GetListByPage(" and bmsah like '" + bmsahName + "%'", "bmsah desc", 1, 1, null);
                if (ds != null && ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 1)
                {
                    num = ds.Tables[0].Rows[0]["bmsah"].ToString().Replace(bmsahName, "").Replace("号", "");
                    num = (Convert.ToInt32(num) + 1).ToString().PadLeft(5, '0') + "号";
                }

                EDRS.Model.TYYW_GG_AJJBXX model = new EDRS.Model.TYYW_GG_AJJBXX();
                //山东省院起诉受[2015]37000000072号
                model.BMSAH = bmsahName + num;
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

                if (jzajxx.Add(model))
                {
                    string count = "0";

                    count = jbxxbll.GetRecordCount("", null).ToString();
                    count = count.PadLeft(5, '0');

                    EDRS.Model.YX_DZJZ_JZJBXX jbxxmodel = new EDRS.Model.YX_DZJZ_JZJBXX();
                    jbxxmodel.JZBH = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    jbxxmodel.SFSC = "N";
                    jbxxmodel.CJSJ = DateTime.Now;
                    jbxxmodel.ZHXGSJ = DateTime.Now;
                    jbxxmodel.FQDWBM = 0;
                    jbxxmodel.FQL = "";
                    jbxxmodel.DWBM = UserInfo.DWBM;
                    jbxxmodel.BMSAH = model.BMSAH;
                    jbxxmodel.TYSAH = UserInfo.DWBM + DateTime.Now.ToString("yyyyMM") + jbxxbll.GetRecordCount("", null) + count;
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
                    jbxxmodel.ZZZT = "-1";

                    if (jbxxbll.Add(jbxxmodel))
                    {
                        return;
                    }
                    else
                        jzajxx.Delete(model.BMSAH);
                }              
                return;
            }
            return;
        }
        #endregion


    }

}