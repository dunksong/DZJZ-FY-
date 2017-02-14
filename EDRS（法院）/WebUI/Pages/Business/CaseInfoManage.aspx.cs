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
                    //�����Զ���Ӱ���
                    //thisreq = Request;
                    //thread = new Thread(new ThreadStart(AddTest));
                    //thread.Start();



                    TYYW_GG_AJJBXX bll = new TYYW_GG_AJJBXX(Request);
                    string data = bll.ListBin(this.Request, UserInfo.DWBM, UserInfo.GH, Bmbms, Jsbms);
                    //OperateLog.AddLog(OperateLog.LogType.������������Web, "�������ݲ�ѯ", UserInfo, UserRole, this.Request);
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
                //if (type.Equals("Add"))
                //    Response.Write(Add());

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

        #region �ݲ�ʹ��
        /// <summary>
        /// ��ȡ�û�Ȩ���б� �ݲ�ʹ��
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


        #region ��������
        /// <summary>
        /// ��������
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
                        return ReturnString.JsonToString(Prompt.error, "��������", null);
                    }
                }
                string type = Request["type"];
                string interfaceType = Request["interfaceType"];
                //�ж��Ƿ�Ϊ����
                if (type == "btn_derive")
                    type = "1";
                else
                    type = "0";

                TYYW_GG_AJJBXX jzajxx = new TYYW_GG_AJJBXX(this.Request);
                EDRS.Model.TYYW_GG_AJJBXX model = jzajxx.GetModel(BMSAH);
                
                

                //��ȡ�����ļ��е�����
                int? isLocalAjxx = ConfigHelper.GetConfigInt("IsLocalAjxx");

                if (isLocalAjxx == 1)
                {
                    if (model == null)
                    {
                        //�������ݲ�����ִ����ӵ�����
                        model = jzajxx.GetIceByModel(BMSAH, jzajxx.GetAjTypeBm(Request, UserInfo.DWBM, "", UserInfo.GH), GetUserRoleList(UserInfo.DWBM, UserInfo.GH), dwbm, UserInfo.GH);
                        if (model == null || !jzajxx.Add(model))
                        {
                            OperateLog.AddLog(OperateLog.LogType.������������Web, ((VersionName)0).ToString() + "������ӱ�������ʧ��", BMSAH, UserInfo, UserRole, this.Request);
                            return ReturnString.JsonToString(Prompt.error, "��ȡ" + ((VersionName)0).ToString() + "ʧ��", null);
                        }
                    }
                    else
                    {
                        //�������ݴ���ִ�и��±�������
                        model = jzajxx.GetIceByModel(BMSAH, jzajxx.GetAjTypeBm(Request, UserInfo.DWBM, "", UserInfo.GH), GetUserRoleList(UserInfo.DWBM, UserInfo.GH), model.CBDW_BM, UserInfo.GH);
                        if (model == null || !jzajxx.Update(model))
                        {
                            OperateLog.AddLog(OperateLog.LogType.������������Web, ((VersionName)0).ToString() + "�����޸ı�������ʧ��", BMSAH, UserInfo, UserRole, this.Request);
                            return ReturnString.JsonToString(Prompt.error, "��ȡ" + ((VersionName)0).ToString() + "ʧ��", null);
                        }
                    }
                }
                if (model != null)
                {
                    EDRS.BLL.XY_DZJZ_XTPZ xtpzbll = new EDRS.BLL.XY_DZJZ_XTPZ(Request);
                    //0������1����
                    EDRS.Model.XY_DZJZ_XTPZ xtpz = xtpzbll.GetConfigById(EnumConfig.�����ļ��ϴ���ַ);
                    if (xtpz != null)
                    {
                        string bmbm = "";
                        string bmmc = "";
                        if (UserRole != null && UserRole.Count > 0)
                        {
                            bmbm = UserRole[0].BMBM;
                            bmmc = UserRole[0].BMMC;
                        }

                        //string strstring = model.BMSAH + "_" + type + "@" + base.UserInfo.DWBM + "@" + base.UserInfo.MC + "@" + GetConfigById(EnumConfig.�����ļ��ϴ���ַ).CONFIGVALUE + "@" + UserInfo.GH + "@" + UserInfo.DWMC + "@" + bmbm + "@" + bmmc;

                        //strstring = DESEncrypt.Encrypt(strstring);

                        //string d = DESEncrypt.Decrypt(strstring);

                        //OperateLog.AddLog(OperateLog.LogType.������������Web, "���������ɹ�", model.BMSAH, UserInfo, UserRole, this.Request);
                        XT_DZJZ_ZZCS zzcsbll = new XT_DZJZ_ZZCS(Request);

                        string[] zks = new string[] { "BMSAH", "SFDC", "DWBM", "MC", "GH", "DWMC", "BMBM", "BMMC", "LJLX", "DZ1", "DZ2", "AJBH", "WSBH","WSMC" };
                        string callBack = string.Empty;
                        string linkType = string.Empty;

                        List<EDRS.Model.XT_DZJZ_ZZCS> zzcsList = new List<EDRS.Model.XT_DZJZ_ZZCS>();
                        EDRS.Model.XT_DZJZ_ZZCS zzcsModel = null;
                        string guid = Guid.NewGuid().ToString();
                        foreach (string dr in zks)
                        {

                            //��������(�����ܰ��ţ��Ƿ񵼳�����λ���룬�û����ƣ���λ���ƣ����ű��룬�������ƣ��������ͣ��ļ��ϴ���ַ���ص���ַ)<add key="ZzCsKey" value="BMSAH,SFDC,DWBM,MC,GH,DWMC,BMBM,BMMC,LJLX,DZ1,DZ2"/>    
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
                                    object obj = xtpzbll.GetConfigById(EnumConfig.��������);
                                    if (obj != null)
                                        linkType = zzcsModel.CSVALUE = ((EDRS.Model.XY_DZJZ_XTPZ)obj).CONFIGVALUE;
                                    else
                                        zzcsModel.CSVALUE = "";
                                    break;
                                case "DZ1":
                                    zzcsModel.CSVALUE = xtpz.CONFIGVALUE;
                                    break;
                                case "DZ2":
                                    object obj2 = xtpzbll.GetConfigById(EnumConfig.·��ӳ���ַ);
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
                                default:
                                    zzcsModel.CSVALUE = "";
                                    break;
                            }

                            zzcsList.Add(zzcsModel);
                        }
                        if (zzcsbll.AddList(zzcsList))
                        {

                            //string strstring = Request.Url.Authority + "@" + guid;

                            //TH:2016-05-23
                            //string strstring = Request.Url.Authority + "@" + guid;
                            //�����������ݣ���������ֱ������·�ɣ�·��ӳ���ַ�����ݱ�ţ�ִ������0��������1���༭Ŀ¼��2�������ӡ��3���ļ���ӡ��4����Ǵ�ӡ,6
                            string strstring = xtpz.CONFIGVALUE + "@" + linkType + "@" + callBack + "@" + guid + "@" + interfaceType;
                            return "{\"parm\":\"" + DESEncrypt.Encrypt(strstring) + "\"}";

                        }
                    }
                    else
                    {
                        OperateLog.AddLog(OperateLog.LogType.������������Web, "δ���þ��ڷ����ַ�����ȵ���������������", BMSAH, UserInfo, UserRole, this.Request);
                        return ReturnString.JsonToString(Prompt.error, "δ���þ��ڷ����ַ�����ȵ���������������", null);
                    }
                }
                OperateLog.AddLog(OperateLog.LogType.������������Web, ((VersionName)0).ToString() + "����ʧ��", BMSAH, UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.error, "��ȡ" + ((VersionName)0).ToString() + "ʧ��", null);

            }
            catch (Exception ex)
            {
                OperateLog.AddLog(OperateLog.LogType.������������Web, ((VersionName)0).ToString() + "����ʧ���쳣" + ex.Message, BMSAH, UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.error, "��ȡ" + ((VersionName)0).ToString() + "ʧ��,����" + ex.Message.Replace("\"", ""), null);
            }
        }
        #endregion

        //#region �������
        ///// <summary>
        ///// �������
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
        //        return ReturnString.JsonToString(Prompt.win, "����ɹ�", null);
        //    return ReturnString.JsonToString(Prompt.error, "����ʧ��", null);
        //} 
        //#endregion

        //#region �༭����
        ///// <summary>
        ///// �༭����
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
        //            return ReturnString.JsonToString(Prompt.win, "�޸ĳɹ�", null);
        //    }
        //    return ReturnString.JsonToString(Prompt.error, "�޸�ʧ��", null);
        //} 
        //#endregion

        //#region ɾ��
        ///// <summary>
        ///// ɾ��
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
        //        return ReturnString.JsonToString(Prompt.win, "ɾ�����ݳɹ�", null);
        //    return ReturnString.JsonToString(Prompt.error, "ɾ������ʧ��", null);
        //} 
        //#endregion

        //#region ���ݱ�Ż�ȡ����
        ///// <summary>
        ///// ���ݱ�Ż�ȡ����
        ///// </summary>
        ///// <returns></returns>
        //public string GetModel(string BMSAH)
        //{
        //    if (string.IsNullOrEmpty(BMSAH))
        //    {
        //        BMSAH = Request["id"];
        //        if(string.IsNullOrEmpty(BMSAH))
        //            return ReturnString.JsonToString(Prompt.error, "��������", null);
        //    }
        //    YX_DZJZ_JZAJXX jzajxx = new YX_DZJZ_JZAJXX(this.Request);
        //    EDRS.Model.YX_DZJZ_JZAJXX model = jzajxx.GetModel(BMSAH);
        //    if (model != null)
        //        return JsonHelper.JsonString(model);
        //    return ReturnString.JsonToString(Prompt.error, "��ȡ����ʧ��", null);
        //} 
        //#endregion

        #region ������ڰ���������Ϣ����״̬
        /// <summary>
        /// ������ڰ���������Ϣ����״̬
        /// </summary>
        /// <returns></returns>
        private string RomIsLock()
        {
            string BMSAH = Request.Form["BMSAH"];
            if (BMSAH == null || string.IsNullOrEmpty(BMSAH))
            {
                return ReturnString.JsonToString(Prompt.error, "������������ȷ", null);
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
                    OperateLog.AddLog(OperateLog.LogType.������������Web, ((VersionName)0).ToString() + "�����ɹ�", BMSAH, UserInfo, UserRole, this.Request);
                    return ReturnString.JsonToString(Prompt.win, "�����ɹ�", null);
                }
            }
            OperateLog.AddLog(OperateLog.LogType.������������Web, ((VersionName)0).ToString() + "����ʧ��", BMSAH, UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "����ʧ��", null);
        }
        #endregion
        

        #region �༭����
        /// <summary>
        /// �༭����
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

            string ajbh = Request.Form.Get("txt_ajbh"); //�������
            string lasj = Request.Form.Get("txt_lasj"); //����ʱ��
            string jasj = Request.Form.Get("txt_jasj"); //�᰸ʱ��
            string ladw = Request.Form.Get("txt_ladw_val"); //����λ
            string ladw_mc = Request.Form.Get("txt_ladw"); //����λ����
            string ljr = Request.Form.Get("txt_lar_val"); //������
            string ljr_mc = Request.Form.Get("txt_lar"); //����������
            string shr = Request.Form.Get("txt_shr"); //�����
            string zjs = (string.IsNullOrEmpty(Request.Form.Get("txt_zjs")) ? "0" : Request.Form.Get("txt_zjs")); //��������
            string djj = Request.Form.Get("txt_djj"); //�ڼ���
            string djy = (string.IsNullOrEmpty(Request.Form.Get("txt_djy")) ? "0" : Request.Form.Get("txt_djy")); //�ڼ�ҳ

            string wsbh = Request.Form.Get("wsbh_hidd");
            string wsmc = Request.Form.Get("txt_wsbh"); //txt_wsbh

            //�������
            if (string.IsNullOrEmpty(ajbh))
            {
                return ReturnString.JsonToString(Prompt.error, "������д�������", null);
            }
            //�жϰ�������
            if (string.IsNullOrEmpty(ajmc))
            {
                return ReturnString.JsonToString(Prompt.error, "������д��������", null);
            }
            //�жϰ������
            if (string.IsNullOrEmpty(ajlb))
            {
                return ReturnString.JsonToString(Prompt.error, "������д�������", null);
            }
            //����ʱ��
            if (string.IsNullOrEmpty(lasj))
            {
                return ReturnString.JsonToString(Prompt.error, "����ѡ������ʱ��", null);
            }
            //����λ
            if (string.IsNullOrEmpty(ladw))
            {
                return ReturnString.JsonToString(Prompt.error, "����ѡ������λ", null);
            }

            if (string.IsNullOrEmpty(bmsah))
                return ReturnString.JsonToString(Prompt.error, "δָ���޸�" + ((VersionName)0).ToString() + "��������ѡ���޸�" + ((VersionName)0).ToString(), null);

            YX_DZJZ_JZJBXX bll = new YX_DZJZ_JZJBXX(Request);
            if (!string.IsNullOrEmpty(ajbh))
            {
              //  List<EDRS.Model.YX_DZJZ_JZJBXX> list = bll.GetModelList(" and ajbh=:ajbh and wsbh=:wsbh and SFSC='N' and bmsah<>:bmsah ", new object[] { ajbh, wsbh ,bmsah});

                int dataCount = bll.GetRecordCount(" and ajbh=:ajbh and wsbh=:wsbh and SFSC='N' and bmsah<>:bmsah ", new object[] { ajbh, wsbh, bmsah });
                if (dataCount > 0)
                {
                    return ReturnString.JsonToString(Prompt.error, "������Ϣ��" + ajbh + "���Ѵ��ڣ��벻Ҫ�ظ���ӣ�", null);
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
                    model.CBDW_BM = ladw;
                    model.CBDW_MC = ladw_mc;
                    model.CBR = ljr_mc;
                    model.CBRGH = ljr;

                    //2016-6-6 ���ֶ�
                    model.SHR = shr; //�����
                    model.SHSJ = DateTime.Now;//���ʱ��
                    model.ZJS = Convert.ToDecimal(zjs);//�ܾ���
                    model.DJJ = djj;//�ڼ���
                    model.ZYS = Convert.ToDecimal(djy);//��ҳ��

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
                            if (!bll.Update(list[0]))
                            {
                                OperateLog.AddLog(OperateLog.LogType.������������Web, "���ڻ�����Ϣ�޸�ʧ��", model.BMSAH, UserInfo, UserRole, this.Request);
                                return ReturnString.JsonToString(Prompt.error, "���ڻ�����Ϣ�޸�ʧ��", null);
                            }
                        }


                        OperateLog.AddLog(OperateLog.LogType.������������Web, "�޸�" + ((VersionName)0).ToString() + "�ɹ�", model.BMSAH, UserInfo, UserRole, this.Request);
                        return ReturnString.JsonToString(Prompt.win, "�޸�" + ((VersionName)0).ToString() + "�ɹ�", null);
                    }
                    OperateLog.AddLog(OperateLog.LogType.������������Web, "�޸�" + ((VersionName)0).ToString() + "ʧ��", model.BMSAH, UserInfo, UserRole, this.Request);
                    return ReturnString.JsonToString(Prompt.error, "�޸�" + ((VersionName)0).ToString() + "ʧ��", null);
                }
                return ReturnString.JsonToString(Prompt.error, "�޸�" + ((VersionName)0).ToString() + "������", null);
            }
            return ReturnString.JsonToString(Prompt.error, "�뽫" + ((VersionName)0).ToString() + "��Ϣ��д����", null);
        }
        #endregion

        #region ɾ��
        /// <summary>
        /// ɾ��
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
                OperateLog.AddLog(OperateLog.LogType.������������Web, "ɾ�����ݳɹ�", ids, UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.win, "ɾ�����ݳɹ�", null);
            }
            OperateLog.AddLog(OperateLog.LogType.������������Web, "ɾ������ʧ��", ids, UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "ɾ������ʧ��", null);
        }
        #endregion

        #region ���ÿͻ�����Ӱ�����Ϣ
        /// <summary>
        /// ���ÿͻ�����Ӱ�����Ϣ
        /// </summary>
        /// <returns></returns>
        private string IAddData()
        {
            try
            {
                EDRS.BLL.XY_DZJZ_XTPZ xtpzbll = new EDRS.BLL.XY_DZJZ_XTPZ(Request);
                string strstring = "abc_2@" + base.UserInfo.DWBM + "@" + base.UserInfo.MC + "@" + xtpzbll.GetConfigById(EnumConfig.�����ļ��ϴ���ַ).CONFIGVALUE + "@" + UserInfo.GH + "@" + UserInfo.DWMC;
                strstring = DESEncrypt.Encrypt(strstring);
                OperateLog.AddLog(OperateLog.LogType.������������Web, "�������" + ((VersionName)0).ToString() + "�ӿ�", "", UserInfo, UserRole, this.Request);
                return "{\"parm\":\"" + strstring + "\"}";

            }
            catch (Exception)
            {
                OperateLog.AddLog(OperateLog.LogType.������������Web, "�������" + ((VersionName)0).ToString() + "�ӿڳ��ִ���", "", UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.error, "���ýӿڳ��ִ���", null);
            }
        }
        #endregion

        #region ��Ӱ�����Ϣ
        /// <summary>
        /// ��Ӱ�����Ϣ
        /// </summary>
        /// <returns></returns>
        private string AddData()
        {

            string ywlx = Request.Form.Get("txt_ywlx");
            string year = Request.Form.Get("txt_year");
            string zi = Request.Form.Get("txt_zi");
            string hao = Request.Form.Get("txt_hao");
            string bmsah = "(" + year + ")" + UserDwbm.DWJC + ywlx + zi + "�ֵ�" + hao + "��";
            //(2016)���Ϸ����̳��ֵ�2��
            string ajmc = Request.Form.Get("txt_ajmc");
            string lbmc = Request.Form.Get("txt_lbmc");
            string xyr = Request.Form.Get("txt_xyr");
            string sfzh = (string.IsNullOrEmpty(Request.Form.Get("txt_sfzh")) ? "" : Request.Form.Get("txt_sfzh"));
            string taryxx = (string.IsNullOrEmpty(Request.Form.Get("txt_taryxx")) ? "" : Request.Form.Get("txt_taryxx"));
            string ajlb = (string.IsNullOrEmpty(Request.Form.Get("txt_ajlb")) ? "" : Request.Form.Get("txt_ajlb"));


            string ajbh = Request.Form.Get("txt_ajbh"); //�������
            string lasj = Request.Form.Get("txt_lasj"); //����ʱ��
            string jasj = Request.Form.Get("txt_jasj"); //�᰸ʱ��
            string ladw = Request.Form.Get("txt_ladw_val"); //����λ
            string ladw_mc = Request.Form.Get("txt_ladw"); //����λ����
            string ljr = Request.Form.Get("txt_lar_val"); //������
            string ljr_mc = Request.Form.Get("txt_lar"); //����������
            string shr = Request.Form.Get("txt_shr"); //�����
            string zjs = ( string.IsNullOrEmpty(Request.Form.Get("txt_zjs")) ? "0" : Request.Form.Get("txt_zjs")); //��������
            string djj = Request.Form.Get("txt_djj"); //�ڼ���
            string djy = (string.IsNullOrEmpty(Request.Form.Get("txt_djy")) ? "0" : Request.Form.Get("txt_djy")); //�ڼ�ҳ
            string wsbh = Request.Form.Get("wsbh_hidd");
            string wsmc = Request.Form.Get("txt_wsbh");
            //�������
            if (string.IsNullOrEmpty(ajbh))
            {
                return ReturnString.JsonToString(Prompt.error, "������д�������", null);
            }
            //�жϰ�������
            if (string.IsNullOrEmpty(ajmc))
            {
                return ReturnString.JsonToString(Prompt.error, "������д��������", null);
            }            
            //�жϰ������
            if (string.IsNullOrEmpty(ajlb))
            {
                return ReturnString.JsonToString(Prompt.error, "������д�������", null);
            }
            //����ʱ��
            if (string.IsNullOrEmpty(lasj))
            {
                return ReturnString.JsonToString(Prompt.error, "����ѡ������ʱ��", null);
            }
            //����λ
            if (string.IsNullOrEmpty(ladw))
            {
                return ReturnString.JsonToString(Prompt.error, "����ѡ������λ", null);
            }  

            EDRS.BLL.YX_DZJZ_JZJBXX jbxxbll = new YX_DZJZ_JZJBXX(Request);

            if (!string.IsNullOrEmpty(ajbh))
            {
                string where = string.Empty;
                object[] param = null;
                if (!string.IsNullOrEmpty(wsbh))
                { 
                   where = " and ajbh=:ajbh and wsbh=:wsbh and SFSC='N'";
                    param = new object[] { ajbh,wsbh };
                }
                else
                {
                   where = " and ajbh=:ajbh and SFSC='N'";
                    param = new object[] { ajbh };
                }
              //  List<EDRS.Model.YX_DZJZ_JZJBXX> list = jbxxbll.GetModelList(where, param);

                int dataCount = jbxxbll.GetRecordCount(where, param);

                if (dataCount > 0)
                {
                    return ReturnString.JsonToString(Prompt.error, "������" + ajbh + "���Ѵ��ڣ��벻Ҫ�ظ���ӣ�", null);
                }
            }

            //if (!string.IsNullOrEmpty(wsbh))
            //{
            //    List<EDRS.Model.YX_DZJZ_JZJBXX> list = jbxxbll.GetModelList(" and wsbh=:wsbh and SFSC='N'", new object[] { wsbh });

            //    if (list != null && list.Count > 0)
            //    {
            //        return ReturnString.JsonToString(Prompt.error, "�����š�" + wsbh + "���Ѵ��ڣ��벻Ҫ�ظ���ӣ�", null);
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
                string num = "00001��";
                TYYW_GG_AJJBXX jzajxx = new TYYW_GG_AJJBXX(this.Request);
                DataSet ds = jzajxx.GetListByPage(" and bmsah like '" + bmsahName + "%'", "bmsah desc", 1, 1, null);
                if (ds != null && ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 1)
                {
                    num = ds.Tables[0].Rows[0]["bmsah"].ToString().Replace(bmsahName, "").Replace("��", "");
                    num = (Convert.ToInt32(num) + 1).ToString().PadLeft(5, '0') + "��";
                }
            
                EDRS.Model.TYYW_GG_AJJBXX model = new EDRS.Model.TYYW_GG_AJJBXX();
                //ɽ��ʡԺ������[2015]37000000072��
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
                model.SLRQ = (string.IsNullOrEmpty(lasj)? DateTime.Now : Convert.ToDateTime(lasj));// DateTime.Now;
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

                //2016-6-6 ���ֶ�
                model.SHR = shr; //�����
                model.SHSJ = DateTime.Now;//���ʱ��
                model.ZJS = Convert.ToDecimal(zjs);//�ܾ���
                model.DJJ = djj;//�ڼ���
                model.ZYS =  Convert.ToDecimal(djy);//��ҳ��

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
                        OperateLog.AddLog(OperateLog.LogType.������������Web, "���" + ((VersionName)0).ToString() + "�ɹ�", model.BMSAH, UserInfo, UserRole, this.Request);
                        return ReturnString.JsonToString(Prompt.win, "���" + ((VersionName)0).ToString() + "�ɹ�", null);
                    }
                    else
                        jzajxx.Delete(model.BMSAH);
                }
                OperateLog.AddLog(OperateLog.LogType.������������Web, "���" + ((VersionName)0).ToString() + "ʧ��", model.BMSAH, UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.error, "���" + ((VersionName)0).ToString() + "ʧ��", null);
            }
            return ReturnString.JsonToString(Prompt.error, "�뽫" + ((VersionName)0).ToString() + "��Ϣ��д����", null);
        }
        #endregion

//        public string Add()
//        {
//            EDRS.BLL.TYYW_GG_AJJBXX jbxxbll = new TYYW_GG_AJJBXX(Request);
//            EDRS.Model.TYYW_GG_AJJBXX jbxxmodel = new EDRS.Model.TYYW_GG_AJJBXX();
//            jbxxmodel.BMSAH = "Ϋ������[2016]37070000019��";
//            jbxxmodel.TYSAH = "37078120160035300";
//            jbxxmodel.SFSC = "N";
//            //CBDW_BM   CBDW_MC
//            jbxxmodel.CBDW_BM = "370700";
//            jbxxmodel.CBDW_MC = "Ϋ����Ժ";
//            jbxxmodel.FQDWBM = 3707;
//            jbxxmodel.FQL = "2016";
//            jbxxmodel.CJSJ = Convert.ToDateTime("2016/8/16 10:00:23");
//            jbxxmodel.ZHXGSJ = Convert.ToDateTime("2016/8/17 16:27:56");
//            jbxxmodel.SLRQ=Convert.ToDateTime("2016/8/16 10:00:24");
//            jbxxmodel.AJMC = "��ͩ���������5�����ӿ���ĳ���Ѱ�����µȰ�";
//            jbxxmodel.AJLB_BM = "0209";
//            jbxxmodel.AJLB_MC = "������׼�ӳ��Ѻ���ް���";
//            jbxxmodel.ZCJG_DWDM = "9901190102001";
//            jbxxmodel.ZCJG_DWMC = "��������";
//            jbxxmodel.YSDW_DWDM = "0125237078100";
//            jbxxmodel.YSDW_DWMC = "Ϋ����������Ժ������Ժ";
//            jbxxmodel.YSWSWH = "";
//            jbxxmodel.YSAY_AYDM = "9903106013800";
//            jbxxmodel.YSAY_AYMC = "����ĳ���";
//            jbxxmodel.YSQTAY_AYDMS = "9903106012200,9903106012100,9903105100000,9903105120000";
//            jbxxmodel.YSQTAY_AYMCS = "Ѱ��������,���ڶ�Ź��,��թ������,�ƻ�������Ӫ��";
//            jbxxmodel.CBRGH = "0029";
//            jbxxmodel.CBR = "������";
//            jbxxmodel.CBBM_BM = "0002";
//            jbxxmodel.CBBM_MC = "���ල��";


//            jbxxmodel.AJZT = "1";
//            jbxxmodel.SFSWAJ = "N";
//            jbxxmodel.SFDBAJ = "N";
//            jbxxmodel.ZXHD_MC = "";


//            jbxxmodel.WCRQ = Convert.ToDateTime("1900/1/1 0:00:00");
//            jbxxmodel.GDRQ = Convert.ToDateTime("1900/1/1 0:00:00");
//            jbxxmodel.GDRGH = "";
//            jbxxmodel.GDR = "";

//            jbxxmodel.AQZY = @"����ĳ��1��2016��4��21����22�գ���ͩ��ͬ����ǿ���ж�����ϴ������֯�ľ֣��ľ���֯���Σ���ͷ�ͷŴ�ӯ������3�����ҡ�2��2016��1�£���ͩ�ͺ������ʥˮҽԺ������ͩ����ס����֯�ľ֣��ľֳ�ͷ�Ŵ���������10��Ԫ��3��2016�괺�ڹ����������ͬ����ǿ����������֯�ľ֣������˽��ɺ������������䡢����ǿ�ϻﾭӪ�ĳ����ľֳ�ͷ�ͷŴ�����100��Ԫ���ҡ�
//      Ѱ�������1��2015��5��4�գ������������ì�ܣ���������ͩ��ѵ����⣬��ͩ����������ȼݳ���Ϋ���и�����������Ȿ��CRVԽҰ�����������ã�����̥���ƣ���ʧ��ֵ20000��Ԫ���º���������ͩ�����߿�7��Ԫ�� 2��2016��4�£��������Ѹշ���ì�ܣ�����������ͩ�����Ż��Է�������ͩ����������ȵ�������̷������Դ彫���Ѹո��ߡ���������Ρ����³ɵȶ����Ѹ�ʵʩŹ���õ��ӽ����Ѹ������ֱ����ˣ������ѸռҾ������һ��� 3��2014�궬�죬��ͩ�����ڰٶ���ҡ�ɺȾƣ����Ƽ�������ͩһ���˷����ڽǣ���ָͩʹ��������˳�ľ���ȶ��Ƽ�������Ź��4��2016��3��2��22ʱ���µ�ɭ�ƺ�����ͨ������������ִ���µ�ɭ����ͩ������ʰ��ͨ����������ͩ����������ȼݳ������ֳ������ͨ������ʵʩŹ��5��2015�����죬����������뼽���·�����ִ�������°Ѹ��¸���������������ϵ��ֳ�������־ǿ�ȶԺ�������Ź��6��2016��3��4�·ݵ�һ�����ϣ���ͩ��88�ưɺȾƣ���ͩ�޹��ò������Ӵ��Ϻ���ͷ������ͩ�־�����������˵����ֳ���7���������������Ů���������2015��7��һ���賿2ʱ��������ȵ����KTV��������鷳���ڶ�������ϣ��������ʮ���˳ֿ�����ľ���ٴε����KTV�ٴ��������鷳���������KTVΧס��
//      ���ڶ�Ź�1��2014���°��꣬������Ͷź���Լ���ڽ��彭�ϾƵ��ſڶ�Ź�������������־ǿ���˳ֿ����������ź�����������ί���������ֹ����ھƵ��ſڶ�Ź����Ź�����в���ƽ�ÿ���������ί���ˡ�2��2015��10�£��������ϵط�˽���˽�������ʱ����ǿ�����ڽǣ�˫����Լ�ں����ſڶ�Ź����Ź�������ｨ�ָܳ������ׯ����Ź��3��2014�����죬����ִ��ͩ������������Լ������������·ϴ���Ǹ�����Ź��
//      ��թ�����2015���ģ���ͩ���˾ƺ�ȥ�����д���ñKTV���裬ǿ�д�����䣬����������һ������ƽ���˲����˸���ڴ��������ˡ��������ñ����Ա�����򶷣�����һ��ͷ�����ƣ�����ƽ�첲���ˣ��º���ͩ���˲��������ñ����ҪǮ�ƣ�������Т�ո���ͩ����4.5��Ԫ�˽���¡�
//      �ƻ�������Ӫ�1��2015��1�£��ڷᵣ����˾������Խ����ծȨת������ѧ������ѧ��Ӷ��ͩ��������������Խ�����Ҫծ����ͩ�������󡢹�־ǿ���˵��Խ�����ţ�����Խ�����ʮ��ͷ��ţǣ�����أ�ǣţ�����У���ͩ���˽���ţ��Ա������һ��С�����Ź���Խ���СŮ���ξ��������Խ��鳥������ѧ��ʮ������ѧ��֪�Խ�����ţ�Ĳ���ص㣬�Խ����ҵ���ţ��ʮ��ͷ�����಻֪���䡣�ö�ʮ������ţ�̰��ǻ����ܼ������̣��Խ���������ʳţ�ļ۸���������ʧ��ֵ20����Ԫ����ͩ���˻�������Ա���Խ�����ţ���ſڶ��ţ�������ţ��������Ӫ��������ţ�����Ʋ�����ʧ��ֵ200��Ԫ���ҡ�2��2015��4��5�£���ͩ�����˵��Խ�����ţ���Ա���Ů�������ε����Ѵ����������������������ѣ������ʧ��ֵ������Ԫ��";
//            jbxxmodel.DQJD = "�ϱ���׼";

//            jbxxmodel.BLKSRQ = Convert.ToDateTime("1900/1/1 0:00:00");
//            jbxxmodel.BLTS = 0;
//            jbxxmodel.DQRQ = Convert.ToDateTime("1900/1/1 0:00:00");
//            jbxxmodel.BJRQ = Convert.ToDateTime("1900/1/1 0:00:00");
//            jbxxmodel.YJZT = "0";


//            jbxxmodel.JYYJZT = "0";
//            jbxxmodel.JYYJHCQXYRGS = 0;
//            jbxxmodel.LCSLBH = "0";
//            jbxxmodel.YJZT = "01";

//            jbxxmodel.FXDJ = "";
//            jbxxmodel.SFGZAJ = "N";
//            jbxxmodel.FZ = "";
//            jbxxmodel.YSYJ_DM = "";

//            jbxxmodel.YSYJ_MC = "";
//            jbxxmodel.SFJBAJ = "N";
//            jbxxmodel.ZXHD_DM = "";
//            jbxxmodel.DQYJJD = "";

//            jbxxmodel.YASCSSJD_DM = "";
//            jbxxmodel.YASCSSJD_MC = "";
//            jbxxmodel.YSRJDH = "�����£�3012153";
//            jbxxmodel.XYR = "";
//            jbxxmodel.SFZH = "";
//            jbxxmodel.TARYXX = "";
//            if (jbxxbll.Add(jbxxmodel))
//            {
//                OperateLog.AddLog(OperateLog.LogType.������������Web, "���" + ((VersionName)0).ToString() + "�ɹ�", jbxxmodel.BMSAH, UserInfo, UserRole, this.Request);
//                return ReturnString.JsonToString(Prompt.win, "���" + ((VersionName)0).ToString() + "�ɹ�", null);
            
//            }
//            OperateLog.AddLog(OperateLog.LogType.������������Web, "���" + ((VersionName)0).ToString() + "ʧ��", jbxxmodel.BMSAH, UserInfo, UserRole, this.Request);
//            return ReturnString.JsonToString(Prompt.error, "���" + ((VersionName)0).ToString() + "ʧ��", null);
            
//        
        #region ���ݱ�Ż�ȡ����
        /// <summary>
        /// ���ݱ�Ż�ȡ����
        /// </summary>
        /// <returns></returns>
        public string GetModel(string BMSAH)
        {
            if (string.IsNullOrEmpty(BMSAH))
            {
                BMSAH = Request["id"];
                if (string.IsNullOrEmpty(BMSAH))
                    return ReturnString.JsonToString(Prompt.error, "��������", null);
            }
            TYYW_GG_AJJBXX jzajxx = new TYYW_GG_AJJBXX(this.Request);
            EDRS.Model.TYYW_GG_AJJBXX model = jzajxx.GetModel(BMSAH);
            if (model != null)
                return JsonHelper.JsonString(model);
            return ReturnString.JsonToString(Prompt.error, "��ȡ����ʧ��", null);
        }
        #endregion

        #region ��ȡģ��
        /// <summary>
        /// ��ȡģ��
        /// </summary>
        /// <returns></returns>
        private string GetMBList()
        {
            string ajlb = Request.Form.Get("ajlb");
            string strWhere = "";
            if (string.IsNullOrEmpty(ajlb))
            {
                if (string.IsNullOrEmpty(ajlb))
                    return ReturnString.JsonToString(Prompt.error, "��������", null);
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

            return ReturnString.JsonToString(Prompt.error, "��ȡ����ʧ��", null);
        }
        #endregion

        #region ��ȡ��Ա�б�
        /// <summary>
        /// ��ȡ��Ա�б�
        /// </summary>
        /// <returns></returns>
        private string GetRybmList()
        {
            string err = string.Empty;
            int count = 0;
            string xm = "";
            #region ��ȡ����
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
                OperateLog.AddLog(OperateLog.LogType.��Ա����Web, "��ѯ��Ա�б�����ʧ�ܣ�" + err, UserInfo, UserRole, Request);
                retValue = err;
            }
            else
            {
                OperateLog.AddLog(OperateLog.LogType.��Ա����Web, "��ѯ��Ա�б����ݳɹ���" + err, UserInfo, UserRole, Request);
                retValue = "{\"Total\":" + count + ",\"Rows\":" + JsonHelper.JsonString(dt) + "}";
                //retValue = "{\"Rows\":" + JsonHelper.JsonString(dt) + "}";
            }
            return retValue;

        } 
        #endregion
               

        #region (����)��Ӱ�����Ϣ
        /// <summary>
        /// (����)��Ӱ�����Ϣ
        /// </summary>
        /// <returns></returns>
        private void AddDataTest(int c,  System.Web.HttpRequest req)
        {
            string ajmc = "���԰�������"+c;
            string ajlb = "020911";            
            string lbmc = "����ǹ֧����ҩ��";//020911 //3A1802   ð�����˱߾�������ͨ��֤
            if (c % 4 == 0)
            {
                ajlb = "3A1802";
                lbmc = "ð�����˱߾�������ͨ��֤";
            }

            string xyr = "��ĳ"+c;
            string sfzh = "";
            string taryxx = "";


            string ajbh = "ABCDE1"+c; //�������
            string lasj = DateTime.Now.ToString(); //����ʱ��
            string jasj = DateTime.Now.ToString(); //�᰸ʱ��
            string ladw = UserInfo.DWBM; //����λ
            string ladw_mc = UserInfo.DWMC; //����λ����
            string ljr = UserInfo.GH; //������
            string ljr_mc = UserInfo.MC; //����������
            string shr = ""; //�����
            string zjs = "10"; //��������
            string djj = ""; //�ڼ���
            string djy ="0"; //�ڼ�ҳ
            string wsbh = "XXXX"+c;
            string wsmc = "XXXXX��������"+c;
          

            EDRS.BLL.YX_DZJZ_JZJBXX jbxxbll = new YX_DZJZ_JZJBXX(req);


            if (!string.IsNullOrEmpty(ajmc) && !string.IsNullOrEmpty(ajlb))//&& !string.IsNullOrEmpty(ajbh)
            {             
                string bmsahName = ajlb + "[" + DateTime.Now.Year + "]" + UserInfo.DWBM;
                string num = "00001��";
                TYYW_GG_AJJBXX jzajxx = new TYYW_GG_AJJBXX(req);
                DataSet ds = jzajxx.GetListByPage(" and bmsah like '" + bmsahName + "%'", "bmsah desc", 1, 1, null);
                if (ds != null && ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 1)
                {
                    num = ds.Tables[0].Rows[0]["bmsah"].ToString().Replace(bmsahName, "").Replace("��", "");
                    num = (Convert.ToInt32(num) + 1).ToString().PadLeft(5, '0') + "��";
                }

                EDRS.Model.TYYW_GG_AJJBXX model = new EDRS.Model.TYYW_GG_AJJBXX();
                //ɽ��ʡԺ������[2015]37000000072��
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

                //2016-6-6 ���ֶ�
                model.SHR = shr; //�����
                model.SHSJ = DateTime.Now;//���ʱ��
                model.ZJS = Convert.ToDecimal(zjs);//�ܾ���
                model.DJJ = djj;//�ڼ���
                model.ZYS = Convert.ToDecimal(djy);//��ҳ��

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