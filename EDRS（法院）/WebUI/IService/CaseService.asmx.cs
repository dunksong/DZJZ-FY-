using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using EDRS.BLL;
using System.Net;
using System.Web.Services.Description;
using System.CodeDom;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using EDRS.Common;

namespace WebUI.IService
{
#if ADVANCED || ADVANCED_ALONE
    /// <summary>
    /// CaseService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class CaseService : System.Web.Services.WebService
    {
        private string key = "tyknntd-#4kji2%(+^";
        [WebMethod(Description = "通知报送案件")]
        public void NoticeNeedSubmittedCase(string AJBH, string WSBH)
        {
            string Str = "";
            if (!Directory.Exists(Path.Combine(GetRootPath(), "NoticeFile")))
                Directory.CreateDirectory(Path.Combine(GetRootPath(), "NoticeFile"));
            try
            {
                string bmsahs = GetText();
                if (bmsahs.IndexOf(WSBH) > -1)
                {
                    Str = "{\"Stat\":1,\"Msg\":\"报送通知失败，上一次报送通知尚未处理，请不要重复通知\",\"Data\":\"\"}";
                    CaseLog("", AJBH, WSBH, "-1", "接收到报送通知失败：上一次报送通知尚未处理，不重复接收。[案件编号=" + AJBH + "，文书编号=" + WSBH + "]");
                }
                else
                {
                    if (!string.IsNullOrEmpty(bmsahs))
                        bmsahs += ",";
                    bmsahs += WSBH;
                    //验证
                    EDRS.BLL.YX_DZJZ_JZJBXX bll = new YX_DZJZ_JZJBXX(this.Context.Request);
                    DataSet ds = bll.GetList(" AND WSBH=:WSBH", new object[] { WSBH });
                    if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    {
                        Str = "{\"Stat\":1,\"Msg\":\"报送通知失败，无对应案件信息\",\"Data\":\"\"}";
                        CaseLog("", "", WSBH, "-1", "不接收的案件报送通知：不存在对应文书编号的案件。[案件编号=" + AJBH + "，文书编号=" + WSBH + "]");
                    }
                    else
                    {
                        string jzbh = ds.Tables[0].Rows[0]["JZBH"].ToString();
                        EDRS.Model.YX_DZJZ_JZJBXX jz = bll.GetModel(jzbh);
                        if (jz == null)
                        {
                            Str = "{\"Stat\":1,\"Msg\":\"报送通知失败，无对应案件信息\",\"Data\":\"\"}";
                            CaseLog("", "", WSBH, "-1", "不接收的案件报送通知：不存在对应文书编号的案件。[案件编号=" + AJBH + "，文书编号=" + WSBH + "]");
                        }
                        else
                        {
                            //审核通过或已上报允许报送
                            if (jz.ZZZT == "4" || jz.ZZZT == "5" || jz.ZZZT == "6")
                            {
                                SaveText(bmsahs);
                                Str = "{\"Stat\":0,\"Msg\":\"报送通知成功\"}";
                                CaseLog("", AJBH, WSBH, "-1", "接收到报送通知成功：[案件编号=" + AJBH + "，文书编号=" + WSBH + "]");
                            }
                            else
                            {
                                Str = "{\"Stat\":1,\"Msg\":\"报送通知失败，当前报送案件尚未审核通过\",\"Data\":\"\"}";
                                CaseLog("", AJBH, WSBH, "-1", "接收到报送通知成功：[案件编号=" + AJBH + "，文书编号=" + WSBH + "]");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Str = "{\"Stat\":1,\"Msg\":\"接收报送通知失败：错误信息请查看系统日志！\",\"Data\":\"\"}";
                CaseLog("", AJBH, WSBH, "-1", "接收到报送通知失败：处理报送数据时发生系统错误，详情查看日志。[案件编号=" + AJBH + "，文书编号=" + WSBH + "]");
                EDRS.Common.LogHelper.LogError(this.Context.Request, "Exception", ex.Message, "public void NoticeNeedSubmittedCase(string AJBH, string WSBH)", "WebUI.IService.CaseService", "");
            }
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("gbk");
            HttpContext.Current.Response.Write(Str);
            HttpContext.Current.Response.End();
        }

        [WebMethod(Description = "获取需要报送的案件编号")]
        public string GetNeedSubmittedCase()
        {
            try
            {
                if (!File.Exists(NoticeFilePath))
                    return "";
                string result = GetText();
                CaseLog("", "", "", "-1", "服务端获取所有待报送案件成功：" + result);
                File.Delete(NoticeFilePath);
                return result;
            }
            catch (Exception ex)
            {
                CaseLog("", "", "", "-1", "服务端获取所有待报送案件失败：错误信息请查看系统日志！");
                EDRS.Common.LogHelper.LogError(this.Context.Request, "Exception", ex.Message, "ppublic string GetNeedSubmittedCase()", "WebUI.IService.CaseService", "");
            }
            return "";
        }

        [WebMethod(Description = "通知打包结果，并反馈扫描材料接口")]
        public bool NoticeProcessResult(string wsbh, bool isTrue, string fileName, string msg)
        {
            string error = "";
            EDRS.BLL.YX_DZJZ_JZJBXX bll = new YX_DZJZ_JZJBXX(this.Context.Request);
            EDRS.Model.YX_DZJZ_JZJBXX jz = null;
            //更改数据库
            try
            {
                int scbj = 0;
                string ajbh = string.Empty;
                if (!File.Exists(NoticeLogFilePath))
                    File.Create(NoticeLogFilePath);
                EDRS.Model.XY_DZJZ_XTPZ model = GetConfigById(EnumConfig.警综平台上传反馈扫描材料接口地址);
                DataSet ds = bll.GetList(" AND WSBH=:WSBH", new object[] { wsbh });
                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    isTrue = false;
                    error = "不存在文书编号【" + wsbh + "】的案件";
                    isTrue = false;
                    throw new Exception(error);
                }
                else
                {
                    string jzbh = ds.Tables[0].Rows[0]["JZBH"].ToString();
                    jz = bll.GetModel(jzbh);
                    if (jz == null)
                    {
                        isTrue = false;
                        error = "不存在文书编号【" + wsbh + "】的案件";
                        isTrue = false;
                        //CaseLog("", "", wsbh, "-1", "通知反馈接口时失败：不存在对应文书编号的案件。[案件编号=" + "" + "，文书编号=" + wsbh + "]");
                        throw new Exception(error);
                    }
                    else
                    {
                        ajbh = jz.AJBH;
                        //调用警综平台
                        scbj = isTrue ? 1 : 0;
                        //object[] param = new object[] { list[0].AJBH, list[0].WSBH, fileName, "", scbj };
                        if (isTrue)
                        {
                            jz.ZZZT = "5";
                            if (!bll.Update(jz))
                            {
                                isTrue = false;
                                error = "更改卷宗状态失败";
                                //CaseLog("", "", wsbh, "-1", "通知反馈接口时失败：更改本地卷宗状态失败。[案件编号=" + "" + "，文书编号=" + wsbh + "]");
                                throw new Exception(error);
                            }
                        }
                    }
                }
                WebReference.jzfk _interface = new WebReference.jzfk();
                //fileName = string.Format("{0}.zip", fileName);
                string result = _interface.updateService(ajbh, fileName, "", scbj.ToString(), wsbh);
                string value = JsonHelper.DeserializeObjectKey("[" + result + "]", "success");
                if (value == "0")
                {
                    throw new Exception(result);
                }
                CaseLog("", "", wsbh, "-1", "通知反馈接口时完成：[案件编号=" + ajbh + "，文书编号=" + wsbh + "，上传文件：" + fileName + "]");
                error = result;
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.Context.Request, "Exception", ex.Message, "public bool NoticeProcessResult(string wsbh, string msg)", "WebUI.IService.CaseService", "");
                isTrue = false;
                CaseLog("", "", wsbh, "-1", "通知反馈接口失败：错误信息请查看系统日志！[案件编号=" + "" + "，文书编号=" + wsbh + "]");
                error = ex.Message;
                if (jz != null)
                {
                    jz.ZZZT = "6";//标记为报送失败，可手动进行上报操作
                    try
                    {
                        bll.Update(jz);
                    }
                    catch { }
                }
            }
            finally
            {
                try
                {
                    FileStream fs = new FileStream(NoticeLogFilePath, FileMode.Append);
                    StreamWriter sr = new StreamWriter(fs, System.Text.Encoding.Default);
                    sr.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "|" + isTrue + "|" + wsbh + "|" + fileName + "|" + msg + "," + error);
                    sr.Close();
                    fs.Close();
                }
                catch (Exception ex)
                { }
            }
            //InvokeWebService
            return isTrue;
        }

      
        /// <summary>
        /// OCR完成，通知待审核状态
        /// </summary>
        /// <param name="fileKey">文件编码</param>
         [WebMethod(Description = "OCR完成，通知待审核状态")]
        public bool NoticeWaitAuditStatus(string wjmc)
        {
            string ajbh = string.Empty;
            string wsbh = string.Empty;
            string msg = string.Empty;
            try
            {
                //根据文件编码获取卷宗基本信息
                EDRS.BLL.YX_DZJZ_JZMLWJ wjBLL = new YX_DZJZ_JZMLWJ(this.Context.Request);
                List<EDRS.Model.YX_DZJZ_JZMLWJ> listwj = wjBLL.GetModelList(" AND WJMC=:WJMC", new object[] { wjmc });
                if (listwj.Count > 0)
                {
                    EDRS.BLL.YX_DZJZ_JZJBXX bll = new YX_DZJZ_JZJBXX(this.Context.Request);
                    EDRS.Model.YX_DZJZ_JZJBXX model = bll.GetModelByBMSAH(listwj[0].BMSAH);
                    if (model != null)
                    {
                        ajbh = model.AJBH;
                        wsbh = model.WSBH;
                        WebReference.jzfk _interface = new WebReference.jzfk();
                        //修改案卷材料审批状态反馈接口，案卷材料审批状态(1：是，0：否)反馈结果修改为 0未扫描，2已扫描待审批，1已审批
                        string status = model.ZZZT == "2" ? "2" : "0";//待审核单据状态对应警综平台2
                        try
                        {
                            string result = _interface.updateStatus(model.AJBH, model.WSBH, status);
                            CaseLog(model.BMSAH, model.AJBH, model.WSBH, model.ZZZT, "通知警综平台已扫描待审批状态完成，ajbh=" + ajbh + "，wsbh=" + wsbh + "：" + result);
                            msg = "通知待审核状态成功：wjmc=" + wjmc + ",ajbh=" + ajbh + ",wsbh=" + ajbh + ",result=" + result + "";
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("调用警综平台通知接口失败，" + ex.Message);
                        }
                        return true;
                    }
                }
                msg = "通知待审核状态失败，本地数据库没有有效的数据：wjmc=" + wjmc + ",ajbh=" + ajbh + ",wsbh=" + ajbh + ",result=" + "";
                CaseLog("", "", "", "", "通知待审核状态失败，案件编号=" + ajbh + "，文书编号=" + wsbh + "：没有找到对应的卷宗信息！");
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(this.Context.Request, "Exception", "通知待审核状态失败，案件编号=" + ajbh + "，文书编号=" + wsbh + "：" + ex.Message, "public bool NoticeWaitAuditStatus(string wjmc)", "WebUI.IService.CaseService", "");
                CaseLog("", "", "", "", "通知警综平台已扫描待审批状态失败,对应文件名称为：" + wjmc);
                msg = "通知待审核状态失败：wjmc=" + wjmc + ",ajbh=" + ajbh + ",wsbh=" + ajbh + ",result=" + ex.Message + "";
            }
            finally
            {
                try
                {
                    FileStream fs = new FileStream(NoticeWaitAuditStatusFilePath, FileMode.Append);
                    StreamWriter sr = new StreamWriter(fs, System.Text.Encoding.Default);
                    sr.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "|" + msg);
                    sr.Close();
                    fs.Close();
                }
                catch (Exception ex)
                { }
            }
            return false;
        }



         private string NoticeWaitAuditStatusFilePath = Path.Combine(Path.Combine(GetRootPath(), "NoticeFile"), "NoticeWaitAuditStatus.txt");


        /// <summary>
        /// 记录OCR以及文件上报记录
        /// </summary>
        /// <param name="bmsah">部门受案号</param>
        /// <param name="ajbh">案件编码</param>
        /// <param name="wsbh">文书编码</param>
        /// <param name="status">卷宗状态：0：未制作，1：制作中，2：已上传，3：审核不通过，4：审核通过，5：已上报</param>
        /// <param name="msg">消息</param>
        [WebMethod(Description = "记录OCR以及文件上报记录")]
        public void CaseLog(string bmsah, string ajbh, string wsbh, string status, string msg)
        {
            EDRS.BLL.XT_ZZJG_RYBM bll = new XT_ZZJG_RYBM(this.Context.Request);
            DataSet ds = bll.GetListByPage("", "DWBM", 1, 1, null);
            EDRS.Model.XT_ZZJG_RYBM UserInfo = new EDRS.Model.XT_ZZJG_RYBM();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                UserInfo.DWBM = ds.Tables[0].Rows[0]["DWBM"].ToString();
                UserInfo.DWMC = "无";
                UserInfo.GH = "";
                UserInfo.MC = "OCR以打包";
            }
            //OcrAndNoticeLogPath
            OperateLog.AddLog(OperateLog.LogType.卷宗OCR及打包状态, msg, bmsah + "|" + ajbh + "|" + wsbh, UserInfo, null, this.Context.Request);
        }
        #region 提供接口
        /// <summary>
        /// 获取案件信息列表
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="Bmsah"></param>
        /// <param name="Ajmc"></param>
        /// <param name="Cbr"></param>
        /// <param name="Timebegin"></param>
        /// <param name="Timeend"></param>
        /// <param name="Dwbm"></param>
        /// <param name="Ajlbbm"></param>
        /// <param name="Sort"></param>
        //[WebMethod(Description = "获取案件信息列表")]
        public void GetCaseDataList(string PageIndex, string PageSize, string mc, string TimeBegin, string Timeend, string DWBM, string LB)
        {


            //  "{"Total":1,"Rows":[{"ISREGARD": 0,  "AJMC": "ffffff",  "BMSAH": "广元市公安局上会议题[2016]37000000001号",  "AJLB_MC": "上会议题",  "CBDW_MC": "广元市公安局",  "CBBM_MC": null,  "CBR": null,  "DQJD": null,  "SLRQ": "2016-04-05 11:15:36",  "AJZT": "0",  "DQRQ": "1900-01-01 00:00:00",  "BJRQ": "1900-01-01 00:00:00",  "WCRQ": "1900-01-01 00:00:00",  "GDRQ": "1900-01-01 00:00:00",  "AJLB_BM": "1101",  "CBDW_BM": "370000",  "XYR": "ff",  "SFZH": "444444444444444444",  "TARYXX": "asdfasdfasdfasd"}]}"

            string rt = "{\"Total\":1,\"MC\": \"ffffff\",  \"BH\": \"广元市公安局上会议题[2016]37000000001号\",  \"LBMC\": \"上会议题\",  \"DWMC\": \"广元市公安局\", \"CBR\": null, \"SLRQ\": \"2016-04-05 11:15:36\", \"LBBM\": \"1101\",  \"DWBM\": \"370000\"}";
            // Encoding.UTF8
            // HttpContext.Current.Response.Charset = "UTF8";//"gbk";
            // rt = "{\"error\":\"目录信息不存在\"}";
            // rt = EDRS.Common.DEncrypt.DEncrypt.Encrypt(rt, key);
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("gbk");
            HttpContext.Current.Response.Write(rt);
            HttpContext.Current.Response.End();
        }
        #endregion


        #region 老接口


        #region 获取案件信息
        /// <summary>
        /// 获取案件信息
        /// </summary>
        /// <param name="bmsah"></param>
        /// <param name="dwbm"></param>
        /// <param name="gh"></param>
        // [WebMethod(Description = "获取案件信息")]
        public void GetCaseDataByBmsah(string bmsah, string dwbm, string gh)
        {
            bool msg;
            string rt = string.Empty;
            try
            {
                bmsah = EDRS.Common.DEncrypt.DEncrypt.Decrypt(bmsah, key);
                dwbm = EDRS.Common.DEncrypt.DEncrypt.Decrypt(dwbm, key);
                gh = EDRS.Common.DEncrypt.DEncrypt.Decrypt(gh, key);
            }
            catch (Exception ex)
            {
                rt = "{\"error\":\"请确认参数是否正确\"}";
            }
            rt = GetAJJBXX(bmsah, dwbm, gh, out msg);
            rt = EDRS.Common.DEncrypt.DEncrypt.Encrypt(rt, key);
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("gbk");
            HttpContext.Current.Response.Write(rt);
            HttpContext.Current.Response.End();
        }
        #endregion

        #region 获取案件卷+文件+文件页列表
        /// <summary>
        /// 获取案件卷+文件+文件页列表
        /// </summary>
        /// <param name="bmsah"></param>
        /// <param name="dwbm"></param>
        /// <param name="gh"></param>
        // [WebMethod(Description = "获取案件卷+文件+文件页列表")]
        public void GetCaseDataListMlAll(string bmsah, string dwbm, string gh)
        {
            bool msg;
            string rt = string.Empty;
            try
            {
                bmsah = EDRS.Common.DEncrypt.DEncrypt.Decrypt(bmsah, key);
                dwbm = EDRS.Common.DEncrypt.DEncrypt.Decrypt(dwbm, key);
                gh = EDRS.Common.DEncrypt.DEncrypt.Decrypt(gh, key);
            }
            catch (Exception ex)
            {
                rt = "{\"error\":\"请确认参数是否正确\"}";
            }

            rt = GetAJJBXX(bmsah, dwbm, gh, out msg);
            if (msg)
            {

                string parneid = "";// Request["pid"];           
                string yjxh = "";// Request["yjxh"] ?? "";

                string strMlWhere = " and SFSC='N'";
                string where = " and level < 10";
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
                EDRS.BLL.YX_DZJZ_JZML bll = new EDRS.BLL.YX_DZJZ_JZML(HttpContext.Current.Request);
                DataSet ds = bll.GetListByTree(strMlWhere, where, withWhere, true, EDRS.Common.StringPlus.ReplaceSingle(yjxh), true, values);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    rt = EDRS.Common.JsonHelper.JsonString(ds.Tables[0]);
                }
                else
                    rt = "{\"error\":\"目录信息不存在\"}";
            }
            rt = EDRS.Common.DEncrypt.DEncrypt.Encrypt(rt, key);
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("gbk");
            HttpContext.Current.Response.Write(rt);
            HttpContext.Current.Response.End();
        }
        #endregion

        #region 获取案件卷+文件列表
        /// <summary>
        /// 获取案件卷+文件列表
        /// </summary>
        /// <param name="bmsah"></param>
        /// <param name="dwbm"></param>
        /// <param name="gh"></param>
        // [WebMethod(Description = "获取案件卷+文件列表")]
        public void GetCaseDataListMl(string bmsah, string dwbm, string gh)
        {
            bool msg;
            string rt = string.Empty;
            try
            {
                bmsah = EDRS.Common.DEncrypt.DEncrypt.Decrypt(bmsah, key);
                dwbm = EDRS.Common.DEncrypt.DEncrypt.Decrypt(dwbm, key);
                gh = EDRS.Common.DEncrypt.DEncrypt.Decrypt(gh, key);
            }
            catch (Exception ex)
            {
                rt = "{\"error\":\"请确认参数是否正确\"}";
            }
            rt = GetAJJBXX(bmsah, dwbm, gh, out msg);
            if (msg)
            {

                string parneid = "";// Request["pid"];           
                string yjxh = "";// Request["yjxh"] ?? "";

                string strMlWhere = " and SFSC='N'";
                string where = " and level < 10";
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
                EDRS.BLL.YX_DZJZ_JZML bll = new EDRS.BLL.YX_DZJZ_JZML(HttpContext.Current.Request);
                DataSet ds = bll.GetListByTreeJaM(strMlWhere, where, withWhere, true, EDRS.Common.StringPlus.ReplaceSingle(yjxh), true, values);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    rt = EDRS.Common.JsonHelper.JsonString(ds.Tables[0]);
                }
                else
                    rt = "{\"error\":\"目录信息不存在\"}";
            }
            rt = EDRS.Common.DEncrypt.DEncrypt.Encrypt(rt, key);
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("gbk");
            HttpContext.Current.Response.Write(rt);
            HttpContext.Current.Response.End();
        }
        #endregion

        #region 根据卷编号获取该卷+文件+文件页列表
        /// <summary>
        /// 根据卷编号获取该卷+文件+文件页列表
        /// </summary>
        /// <param name="bmsah"></param>
        /// <param name="dwbm"></param>
        /// <param name="gh"></param>
        /// <param name="mlbh"></param>
        //[WebMethod(Description = "根据卷编号获取该卷+文件+文件页列表")]
        public void GetCaseDataListMlAllByBh(string bmsah, string dwbm, string gh, string mlbh)
        {
            bool msg;
            string rt = string.Empty;
            try
            {
                bmsah = EDRS.Common.DEncrypt.DEncrypt.Decrypt(bmsah, key);
                dwbm = EDRS.Common.DEncrypt.DEncrypt.Decrypt(dwbm, key);
                mlbh = EDRS.Common.DEncrypt.DEncrypt.Decrypt(mlbh, key);
                gh = EDRS.Common.DEncrypt.DEncrypt.Decrypt(gh, key);
            }
            catch (Exception ex)
            {
                rt = "{\"error\":\"请确认参数是否正确\"}";
            }
            rt = GetAJJBXX(bmsah, dwbm, gh, out msg);
            if (msg)
            {

                //    string mlbh = "";// Request["pid"];           
                string yjxh = "";// Request["yjxh"] ?? "";

                string strMlWhere = " and SFSC='N'";
                string where = " and level < 10";
                string withWhere = " and FMLBH is null";
                object[] values = new object[2];
                if (!string.IsNullOrEmpty(bmsah))
                {
                    strMlWhere += " and BMSAH=:BMSAH";
                    values[0] = bmsah;
                }
                //判断存在父级编码
                if (!string.IsNullOrEmpty(mlbh))
                {
                    withWhere = " and MLBH =:MLBH";
                    values[1] = mlbh;
                }
                EDRS.BLL.YX_DZJZ_JZML bll = new EDRS.BLL.YX_DZJZ_JZML(HttpContext.Current.Request);
                DataSet ds = bll.GetListByTree(strMlWhere, where, withWhere, true, EDRS.Common.StringPlus.ReplaceSingle(yjxh), true, values);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    rt = EDRS.Common.JsonHelper.JsonString(ds.Tables[0]);
                }
                else
                    rt = "{\"error\":\"目录信息不存在\"}";
            }
            rt = EDRS.Common.DEncrypt.DEncrypt.Encrypt(rt, key);
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("gbk");
            HttpContext.Current.Response.Write(rt);
            HttpContext.Current.Response.End();
        }
        #endregion

        #region 根据卷编号获取该卷+文件列表
        /// <summary>
        /// 根据卷编号获取该卷+文件列表
        /// </summary>
        /// <param name="bmsah"></param>
        /// <param name="dwbm"></param>
        /// <param name="gh"></param>
        /// <param name="mlbh"></param>
        //[WebMethod(Description = "根据卷编号获取该卷+文件列表")]
        public void GetCaseDataListMlByBh(string bmsah, string dwbm, string gh, string mlbh)
        {
            bool msg;
            string rt = string.Empty;
            try
            {
                bmsah = EDRS.Common.DEncrypt.DEncrypt.Decrypt(bmsah, key);
                dwbm = EDRS.Common.DEncrypt.DEncrypt.Decrypt(dwbm, key);
                mlbh = EDRS.Common.DEncrypt.DEncrypt.Decrypt(mlbh, key);
                gh = EDRS.Common.DEncrypt.DEncrypt.Decrypt(gh, key);
            }
            catch (Exception ex)
            {
                rt = "{\"error\":\"请确认参数是否正确\"}";
            }
            rt = GetAJJBXX(bmsah, dwbm, gh, out msg);
            if (msg)
            {

                //string parneid = "";// Request["pid"];           
                string yjxh = "";// Request["yjxh"] ?? "";

                string strMlWhere = " and SFSC='N'";
                string where = " and level < 10";
                string withWhere = " and FMLBH is null";
                object[] values = new object[2];
                if (!string.IsNullOrEmpty(bmsah))
                {
                    strMlWhere += " and BMSAH=:BMSAH";
                    values[0] = bmsah;
                }
                //判断存在父级编码
                if (!string.IsNullOrEmpty(mlbh))
                {
                    withWhere = " and MLBH =:MLBH";
                    values[1] = mlbh;
                }
                EDRS.BLL.YX_DZJZ_JZML bll = new EDRS.BLL.YX_DZJZ_JZML(HttpContext.Current.Request);
                DataSet ds = bll.GetListByTreeJaM(strMlWhere, where, withWhere, true, EDRS.Common.StringPlus.ReplaceSingle(yjxh), true, values);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    rt = EDRS.Common.JsonHelper.JsonString(ds.Tables[0]);
                }
                else
                    rt = "{\"error\":\"目录信息不存在\"}";
            }
            rt = EDRS.Common.DEncrypt.DEncrypt.Encrypt(rt, key);
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("gbk");
            HttpContext.Current.Response.Write(rt);
            HttpContext.Current.Response.End();
        }
        #endregion

        #region 根据文件编号获取文件页列表
        /// <summary>
        /// 根据文件编号获取文件页列表
        /// </summary>
        /// <param name="bmsah"></param>
        /// <param name="dwbm"></param>
        /// <param name="gh"></param>
        /// <param name="mlbh"></param>
        //[WebMethod(Description = "根据文件编号获取文件页列表")]
        public void GetCaseDataListMlByWjbh(string bmsah, string dwbm, string gh, string mlbh)
        {
            bool msg;
            string rt = string.Empty;
            try
            {
                bmsah = EDRS.Common.DEncrypt.DEncrypt.Decrypt(bmsah, key);
                dwbm = EDRS.Common.DEncrypt.DEncrypt.Decrypt(dwbm, key);
                gh = EDRS.Common.DEncrypt.DEncrypt.Decrypt(gh, key);
                mlbh = EDRS.Common.DEncrypt.DEncrypt.Decrypt(mlbh, key);
            }
            catch (Exception ex)
            {
                rt = "{\"error\":\"请确认参数是否正确\"}";
            }
            rt = GetAJJBXX(bmsah, dwbm, gh, out msg);
            if (msg)
            {
                string strWhere = " and MLBH=:MLBH";
                strWhere += " and BMSAH=:BMSAH";
                EDRS.BLL.YX_DZJZ_JZML bll = new EDRS.BLL.YX_DZJZ_JZML(HttpContext.Current.Request);
                DataSet ds = bll.GetListByWjmc(strWhere, "WJSXH", new object[] { mlbh, bmsah });
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    rt = EDRS.Common.JsonHelper.JsonString(ds.Tables[0]);
                }
                else
                    rt = "{\"error\":\"文件页列表不存在\"}";
            }
            rt = EDRS.Common.DEncrypt.DEncrypt.Encrypt(rt, key);
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("gbk");
            HttpContext.Current.Response.Write(rt);
            HttpContext.Current.Response.End();
        }
        #endregion

        #region 根据文件序号获取文件
        /// <summary>
        /// 根据文件序号获取文件
        /// </summary>
        /// <param name="bmsah"></param>
        /// <param name="dwbm"></param>
        /// <param name="gh"></param>
        /// <param name="wjxh"></param>
        //[WebMethod(Description = "根据文件序号获取文件")]
        //public void GetCaseDataFile(string bmsah, string dwbm, string gh, string wjxh)
        //{
        //    bool msg;
        //    string rt = string.Empty;
        //    try
        //    {
        //        bmsah = EDRS.Common.DEncrypt.DEncrypt.Decrypt(bmsah, key);
        //        dwbm = EDRS.Common.DEncrypt.DEncrypt.Decrypt(dwbm, key);
        //        gh = EDRS.Common.DEncrypt.DEncrypt.Decrypt(gh, key);
        //        wjxh = EDRS.Common.DEncrypt.DEncrypt.Decrypt(wjxh, key);
        //    }
        //    catch (Exception ex)
        //    {
        //        rt = "{\"error\":\"请确认参数是否正确\"}";
        //    }
        //    rt = GetAJJBXX(bmsah, dwbm, gh, out msg);
        //    if (msg)
        //    {
        //        string strWhere = " and WJXH=:WJXH";
        //        strWhere += " and BMSAH=:BMSAH";
        //        EDRS.BLL.YX_DZJZ_JZML bll = new EDRS.BLL.YX_DZJZ_JZML(HttpContext.Current.Request);
        //        DataSet ds = bll.GetListByWjmc(strWhere, "WJSXH", new object[] { wjxh, bmsah });
        //        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //        {
        //            EDRS.BLL.XY_DZJZ_XTPZ bllXp = new EDRS.BLL.XY_DZJZ_XTPZ(HttpContext.Current.Request);
        //            EDRS.Model.XY_DZJZ_XTPZ model = bllXp.GetModel((int)EDRS.Common.EnumConfig.卷宗文件下载地址);
        //            if (model != null)
        //            {
        //                EDRS.Common.IceServicePrx isp = new EDRS.Common.IceServicePrx();
        //                string messg = "";
        //                byte[] bytes = new byte[] { };
        //                if (isp.DownFile(model.CONFIGVALUE, ds.Tables[0].Rows[0]["WJLJ"].ToString(), ds.Tables[0].Rows[0]["WJMC"].ToString(), "", ref bytes, ref messg))
        //                {
        //                    rt = "";
        //                    byte[] info = EDRS.Common.DataEncryption.Decryption(bytes);
        //                    HttpContext.Current.Response.ContentType = "application/pdf";
        //                    HttpContext.Current.Response.AddHeader("content-disposition", "filename=pdf");
        //                    HttpContext.Current.Response.AddHeader("content-length", info.Length.ToString());
        //                    HttpContext.Current.Response.BinaryWrite(info);
        //                }
        //                else
        //                    rt = "{\"error\":\"文件页列表不存在\"}";
        //            }
        //            else
        //                rt = "{\"error\":\"未设置文件下载地址\"}";
        //        }
        //        else
        //            rt = "{\"error\":\"文件页列表不存在\"}";
        //    }
        //    if (!string.IsNullOrEmpty(rt))
        //    {
        //        rt = EDRS.Common.DEncrypt.DEncrypt.Encrypt(rt, key);
        //        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("gbk");
        //        HttpContext.Current.Response.Write(rt);
        //        HttpContext.Current.Response.End();
        //    }
        //}
        #endregion


        #endregion

        #region 新接口

        #region 根据部门受案号查询案件卷列表
        /// <summary>
        /// 根据部门受案号查询案件卷列表
        /// </summary>
        /// <param name="bmsah"></param>
        // [WebMethod(Description = "根据部门受案号查询案件卷列表")]
        public void GetVolume(string bmsah)
        {
            //返回参数
            string rt = string.Empty;

            //try
            //{
            //    //参数解密
            //    bmsah = EDRS.Common.DEncrypt.DEncrypt.Decrypt(bmsah, key);
            //}
            //catch (Exception ex)
            //{
            //    rt = "{\"error\":\"请确认参数是否正确\"}";
            //}
            EDRS.BLL.YX_DZJZ_JZML bll = new EDRS.BLL.YX_DZJZ_JZML(HttpContext.Current.Request);
            DataSet ds = bll.GetList(" and BMSAH=:BMSAH and mllx = 1 and SFSC='N'", new object[] { bmsah });

            //判断是否查询出现异常
            if (ds != null && ds.Tables.Count > 0)
            {
                //判断查询没有数据
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0].Copy();
                    dt.Columns.Remove("JZBH");
                    dt.Columns.Remove("SFSC");
                    dt.Columns.Remove("CJSJ");
                    dt.Columns.Remove("ZHXGSJ");
                    dt.Columns.Remove("FQDWBM");
                    dt.Columns.Remove("FQL");
                    dt.Columns.Remove("DWBM");
                    dt.Columns.Remove("BMSAH");
                    dt.Columns.Remove("FMLBH");
                    dt.Columns.Remove("MLXX");
                    dt.Columns.Remove("MLSXH");
                    dt.Columns.Remove("MLBM");
                    dt.Columns.Remove("MLLX");
                    //成功
                    rt = EDRS.Common.JsonHelper.JsonString(dt);

                }
                else
                {
                    //该案件没有卷
                    rt = "{\"error\":\"该案件没有卷\"}";
                }
            }
            else
            {
                //查询失败
                rt = "{\"error\":\"查询失败\"}";
            }

            //返回结果加密
            //rt = EDRS.Common.DEncrypt.DEncrypt.Encrypt(rt, key);

            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("gbk");
            HttpContext.Current.Response.Write(rt);
            HttpContext.Current.Response.End();
        }
        #endregion
        #region 根据目录编号查询文件列表
        /// <summary>
        /// 根据目录编号查询文件列表
        /// </summary>
        /// <param name="bmsah"></param>
        //[WebMethod(Description = "根据目录编号查询文件列表")]
        public void GetFile(string mlbh)
        {
            string str = string.Empty;
            //try
            //{
            //    //参数解密
            //    mlbh = EDRS.Common.DEncrypt.DEncrypt.Decrypt(mlbh, key);
            //}
            //catch (Exception ex)
            //{
            //    str = "{\"error\":\"请确认参数是否正确\"}";
            //}
            EDRS.BLL.YX_DZJZ_JZML bll = new EDRS.BLL.YX_DZJZ_JZML(HttpContext.Current.Request);
            DataSet ds = bll.GetList(" and FMLBH=:FMLBH and mllx=3 and SFSC='N'", new object[] { mlbh });
            //判断是否异常
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0].Copy();
                    dt.Columns.Remove("JZBH");
                    dt.Columns.Remove("SFSC");
                    dt.Columns.Remove("CJSJ");
                    dt.Columns.Remove("ZHXGSJ");
                    dt.Columns.Remove("FQDWBM");
                    dt.Columns.Remove("FQL");
                    dt.Columns.Remove("DWBM");
                    dt.Columns.Remove("BMSAH");
                    dt.Columns.Remove("FMLBH");
                    dt.Columns.Remove("MLXX");
                    dt.Columns.Remove("MLSXH");
                    dt.Columns.Remove("MLBM");
                    dt.Columns.Remove("MLLX");
                    str = EDRS.Common.JsonHelper.JsonString(dt);
                }
                else
                {
                    str = "{\"error\":\"该卷没有文件\"}";
                }
            }
            else
            {
                str = "{\"error\":\"查询失败\"}";
            }
            //str = EDRS.Common.DEncrypt.DEncrypt.Encrypt(str, key);
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("gbk");
            HttpContext.Current.Response.Write(str);
            HttpContext.Current.Response.End();
        }
        #endregion
        #region 根据目录编号查询文件页列表
        /// <summary>
        /// 根据目录编号查询文件页列表
        /// </summary>
        /// <param name="bmsah"></param>
        //[WebMethod(Description = "根据目录编号查询文件页列表")]
        public void GetFilePages(string mlbh)
        {
            string str = string.Empty;
            //try
            //{
            //    //参数解密
            //    mlbh = EDRS.Common.DEncrypt.DEncrypt.Decrypt(mlbh, key);
            //}
            //catch (Exception ex)
            //{
            //    str = "{\"error\":\"请确认参数是否正确\"}";
            //}
            EDRS.BLL.YX_DZJZ_JZMLWJ bll = new EDRS.BLL.YX_DZJZ_JZMLWJ(HttpContext.Current.Request);
            DataSet ds = bll.GetList(" and MLBH=:MLBH and SFSC='N' order by WJSXH", new object[] { mlbh });
            //判断是否异常
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0].Copy();
                    dt.Columns.Remove("JZBH");
                    //dt.Columns.Remove("MLBH");
                    dt.Columns.Remove("WJXH");
                    dt.Columns.Remove("SFSC");
                    dt.Columns.Remove("ZHXGSJ");
                    dt.Columns.Remove("FQDWBM");
                    dt.Columns.Remove("FQL");
                    dt.Columns.Remove("DWBM");
                    dt.Columns.Remove("BMSAH");
                    //dt.Columns.Remove("WJLJ");
                    //dt.Columns.Remove("WJMC");
                    //dt.Columns.Remove("WJXSMC");
                    //dt.Columns.Remove("WJHZ");

                    dt.Columns.Remove("WJKSY");
                    dt.Columns.Remove("WJJSY");
                    dt.Columns.Remove("WJBZXX");

                    dt.Columns.Remove("WJYZBZ");
                    dt.Columns.Remove("WJSXH");
                    dt.Columns.Remove("WJZDY");

                    //dt.Columns.Remove("SSLBBM");
                    //dt.Columns.Remove("SSLBMC");
                    //dt.Columns.Remove("WJDX");
                    str = EDRS.Common.JsonHelper.JsonString(dt);
                }
                else
                {
                    str = "{\"error\":\"该文件没有文件页\"}";
                }
            }
            else
            {
                str = "{\"error\":\"查询失败\"}";
            }
            //str = EDRS.Common.DEncrypt.DEncrypt.Encrypt(str, key);
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("gbk");
            HttpContext.Current.Response.Write(str);
            HttpContext.Current.Response.End();
        }

        #endregion
        #region 根据文件序号获取文件
        /// <summary>
        /// 根据文件序号获取文件
        /// </summary>   
        /// <param name="wjxh"></param>
        //[WebMethod(Description = "根据文件序号获取文件")]
        public void GetCaseDataFile(string wjxh)
        {
            // bool msg;
            string rt = string.Empty;
            //try
            //{

            //    wjxh = EDRS.Common.DEncrypt.DEncrypt.Decrypt(wjxh, key);
            //}
            //catch (Exception ex)
            //{
            //    rt = "{\"error\":\"请确认参数是否正确\"}";
            //}
            //rt = GetAJJBXX(bmsah, dwbm, gh, out msg);
            //if (msg)
            //{
            string strWhere = " and WJXH=:WJXH";
            //   strWhere += " and BMSAH=:BMSAH";
            EDRS.BLL.YX_DZJZ_JZML bll = new EDRS.BLL.YX_DZJZ_JZML(HttpContext.Current.Request);
            DataSet ds = bll.GetListByWjmc(strWhere, "WJSXH", new object[] { wjxh });
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                EDRS.BLL.XY_DZJZ_XTPZ bllXp = new EDRS.BLL.XY_DZJZ_XTPZ(HttpContext.Current.Request);
                EDRS.Model.XY_DZJZ_XTPZ model = bllXp.GetModel((int)EDRS.Common.EnumConfig.卷宗文件下载地址);
                if (model != null)
                {
                    EDRS.Common.IceServicePrx isp = new EDRS.Common.IceServicePrx();
                    string messg = "";
                    byte[] bytes = new byte[] { };
                    if (isp.DownFile(model.CONFIGVALUE, ds.Tables[0].Rows[0]["WJLJ"].ToString(), ds.Tables[0].Rows[0]["WJMC"].ToString(), "", ref bytes, ref messg))
                    {
                        rt = "";
                        string showType = ConfigHelper.GetConfigString("FileShowType");
                        if (showType == "1")
                        {
                            HttpContext.Current.Response.Write("<img style=\"max-width:100%;\" alt=\"\" src=\"data:image/jpeg;base64," + Convert.ToBase64String(bytes) + "\" />");
                        }
                        else
                        {
                            byte[] info = EDRS.Common.DataEncryption.Decryption(bytes);
                            HttpContext.Current.Response.ContentType = "application/pdf";
                            HttpContext.Current.Response.AddHeader("content-disposition", "filename=pdf");
                            HttpContext.Current.Response.AddHeader("content-length", info.Length.ToString());
                            HttpContext.Current.Response.BinaryWrite(info);
                        }
                    }
                    else
                        rt = "{\"error\":\"文件页列表不存在\"}";
                }
                else
                    rt = "{\"error\":\"未设置文件下载地址\"}";
            }
            else
                rt = "{\"error\":\"文件页列表不存在\"}";
            //}
            if (!string.IsNullOrEmpty(rt))
            {
                //rt = EDRS.Common.DEncrypt.DEncrypt.Encrypt(rt, key);
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("gbk");
                HttpContext.Current.Response.Write(rt);
                HttpContext.Current.Response.End();
            }
        }
        #endregion

        #endregion

        #region 公共方法

        object _lock = new object();


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


            EDRS.BLL.XY_DZJZ_XTPZ bll = new EDRS.BLL.XY_DZJZ_XTPZ(this.Context.Request);

            List<EDRS.Model.XY_DZJZ_XTPZ> modelList = bll.GetModelList(where, values);

            if (modelList != null && modelList.Count > 0)
                return modelList[0];
            return null;
        }
        #endregion
        #region 根据部门受案号获取案件基本信息
        /// <summary>
        /// 根据部门受案号获取案件基本信息
        /// </summary>
        /// <param name="bmsah"></param>
        /// <param name="dwbm"></param>
        /// <param name="gh"></param>
        /// <returns></returns>
        private string GetAJJBXX(string bmsah, string dwbm, string gh, out bool msg)
        {
            msg = false;
            EDRS.BLL.TYYW_GG_AJJBXX jzajxx = new EDRS.BLL.TYYW_GG_AJJBXX(HttpContext.Current.Request);
            EDRS.Model.TYYW_GG_AJJBXX model = jzajxx.GetModel(bmsah);
            if (model != null)
            {
                EDRS.BLL.XT_DM_QX qxbll = new EDRS.BLL.XT_DM_QX(HttpContext.Current.Request);
                DataSet ds = qxbll.GetQxList(dwbm, gh);
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow[] drDwbm = ds.Tables[0].Select("qxbm=" + model.CBDW_BM);
                        DataRow[] drAjlb = ds.Tables[0].Select("qxbm=" + model.AJLB_BM);
                        if (drDwbm.Length > 0 && drAjlb.Length > 0)
                        {
                            msg = true;
                            return EDRS.Common.JsonHelper.JsonString(model);
                        }
                        else
                            return "{\"error\":\"没有权限访问" + ((VersionName)0).ToString() + "\"}";
                    }
                    else
                        return "{\"error\":\"账号未设置权限，无法获取" + ((VersionName)0).ToString() + "\"}";
                }
                else
                    return "{\"error\":\"账号权限获取失败，无法获取" + ((VersionName)0).ToString() + "\"}";
            }
            else
            {
                return "{\"error\":\"未找到" + ((VersionName)0).ToString() + "信息\"}";
            }
        }
        #endregion

        #region 根据部门受案号获取相关卷或者文件页
        /// <summary>
        /// 根据部门受案号获取相关卷或者文件页
        /// </summary>
        /// <param name="bmsah"></param>
        /// <param name="dwbm"></param>
        /// <param name="gh"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="mllx"></param>
        /// <returns></returns>
        private string GetJZML(string bmsah, string PageIndex, string PageSize, int mllx)
        {
            try
            {
                int.Parse(PageIndex);
                int.Parse(PageSize);
            }
            catch (Exception)
            {
                return "{\"error\":\"PageIndex和PageSize必须为数字\"}";
            }

            string msg = "";
            if (mllx == 1)
                msg = "卷";
            else
                msg = "文件页";
            string where = string.Format(" and SFSC='N' and mllx={0} and BMSAH=:BMSAH ", mllx);
            EDRS.BLL.YX_DZJZ_JZML mlbll = new EDRS.BLL.YX_DZJZ_JZML(HttpContext.Current.Request);
            DataSet dsMl = mlbll.GetListByPage(where, "mlsxh", int.Parse(PageIndex), int.Parse(PageSize), new object[] { bmsah });
            if (dsMl != null && dsMl.Tables.Count > 0 && dsMl.Tables[0].Rows.Count > 0)
                return EDRS.Common.JsonHelper.JsonString(dsMl.Tables[0]);
            else
                return "{\"error\":\"没有找到相关" + msg + "\"}";
        }
        #endregion
        #region WebService方法
        /// < summary>          
        /// 动态调用web服务 
        /// < /summary>          
        /// < param name="url">WSDL服务地址< /param>
        /// < param name="classname">类名< /param>  
        /// < param name="methodname">方法名< /param>  
        /// < param name="args">参数< /param> 
        /// < returns>< /returns>
        public object InvokeWebService(string url, string classname, string methodname, object[] args)
        {
            string @namespace = "EnterpriseServerBase.WebService.DynamicWebCalling";
            if ((classname == null) || (classname == ""))
            {
                classname = GetWsClassName(url);
            }
            try
            {                   //获取WSDL   
                System.Net.WebClient wc = new System.Net.WebClient();
                Stream stream = wc.OpenRead(url + "?WSDL");
                ServiceDescription sd = ServiceDescription.Read(stream);
                ServiceDescriptionImporter sdi = new ServiceDescriptionImporter();
                sdi.AddServiceDescription(sd, "", "");
                CodeNamespace cn = new CodeNamespace(@namespace);
                //生成客户端代理类代码          
                CodeCompileUnit ccu = new CodeCompileUnit();
                ccu.Namespaces.Add(cn);
                sdi.Import(cn, ccu);
                CSharpCodeProvider icc = new CSharpCodeProvider();
                //设定编译参数                 
                CompilerParameters cplist = new CompilerParameters();
                cplist.GenerateExecutable = false;
                cplist.GenerateInMemory = true;
                cplist.ReferencedAssemblies.Add("System.dll");
                cplist.ReferencedAssemblies.Add("System.XML.dll");
                cplist.ReferencedAssemblies.Add("System.Web.Services.dll");
                cplist.ReferencedAssemblies.Add("System.Data.dll");
                //编译代理类                 
                CompilerResults cr = icc.CompileAssemblyFromDom(cplist, ccu);
                if (true == cr.Errors.HasErrors)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    foreach (System.CodeDom.Compiler.CompilerError ce in cr.Errors)
                    {
                        sb.Append(ce.ToString());
                        sb.Append(System.Environment.NewLine);
                    }
                    throw new Exception(sb.ToString());
                }
                //生成代理实例，并调用方法   
                System.Reflection.Assembly assembly = cr.CompiledAssembly;
                Type t = assembly.GetType(@namespace + "." + classname, true, true);
                object obj = Activator.CreateInstance(t);
                System.Reflection.MethodInfo mi = t.GetMethod(methodname);
                return mi.Invoke(obj, args);
                // PropertyInfo propertyInfo = type.GetProperty(propertyname);     
                //return propertyInfo.GetValue(obj, null); 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message, new Exception(ex.InnerException.StackTrace));
            }
        }
        private string GetWsClassName(string wsUrl)
        {
            string[] parts = wsUrl.Split('/');
            string[] pps = parts[parts.Length - 1].Split('.');
            return pps[0];
        }
        #endregion
        #region 文本操作

        private string NoticeFilePath = Path.Combine(Path.Combine(GetRootPath(), "NoticeFile"), "NoticeFile.txt");
        private string NoticeLogFilePath = Path.Combine(Path.Combine(GetRootPath(), "NoticeFile"), "NoticeLogFile.txt");
        private string OcrAndNoticeLogPath = Path.Combine(Path.Combine(GetRootPath(), "NoticeFile"), "LogFile.txt");
        public string GetText()
        {
            try
            {
                lock (_lock)
                {
                    if (!File.Exists(NoticeFilePath))
                    {
                        return "";
                    }
                    else
                    {
                        StreamReader sr = new StreamReader(NoticeFilePath, System.Text.Encoding.Default);
                        String input = sr.ReadToEnd();
                        sr.Close();
                        return input.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public bool SaveText(string text)
        {
            try
            {
                lock (_lock)
                {
                    if (File.Exists(NoticeFilePath))
                    {
                        File.Delete(NoticeFilePath);
                    }
                    FileStream fs = new FileStream(NoticeFilePath, FileMode.CreateNew);
                    StreamWriter sr = new StreamWriter(fs, System.Text.Encoding.Default);
                    sr.Write(text);
                    sr.Close();
                    fs.Close();
                    return true;
                }
            }
            catch (Exception ex)
            { }

            return false;


        }
        /// <summary>
        /// 取得网站根目录的物理路径
        /// </summary>
        /// <returns></returns>
        public static string GetRootPath()
        {
            string AppPath = "";
            HttpContext HttpCurrent = HttpContext.Current;
            if (HttpCurrent != null)
            {
                AppPath = HttpCurrent.Server.MapPath("~");
            }
            else
            {
                AppPath = AppDomain.CurrentDomain.BaseDirectory;
                if (Regex.Match(AppPath, @"\\$", RegexOptions.Compiled).Success)
                    AppPath = AppPath.Substring(0, AppPath.Length - 1);
            }
            return AppPath;
        }
        #endregion

        #endregion

        #region 制作信息获取接口
        /// <summary>
        /// 制作信息获取接口
        /// </summary>
        /// <param name="DWBM">单位编码</param>
        /// <param name="BH">部门受案号</param>
        //[WebMethod(Description = "制作信息获取接口")]
        public void GetDossierDoInfo(string DWBM, string BH)
        {
            //返回参数
            string rt = string.Empty;
            EDRS.BLL.YX_DZJZ_JZJBXX bll = new EDRS.BLL.YX_DZJZ_JZJBXX(HttpContext.Current.Request);
            DataSet ds = bll.GetJzjbxxByBmsah(BH, DWBM);

            //判断是否查询出现异常
            if (ds != null && ds.Tables.Count > 0)
            {
                //判断查询没有数据
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0].Copy();

                    //成功
                    rt = "{\"Stat\":0,\"Msg\":\"查询成功\",\"Data\":" + EDRS.Common.JsonHelper.JsonString(dt) + "}";
                }
                else
                {
                    //该案件没有卷
                    rt = "{\"Stat\":1,\"Msg\":\"该案件没有制作信息\",\"Data\":\"\"}";
                }
            }
            else
            {
                //查询失败
                rt = "{\"Stat\":1,\"Msg\":\"查询失败\",\"Data\":\"\"}";
            }

            //返回结果加密
            //rt = EDRS.Common.DEncrypt.DEncrypt.Encrypt(rt, key);

            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("gbk");
            HttpContext.Current.Response.Write(rt);
            HttpContext.Current.Response.End();
        }
        #endregion

        #region 卷宗文件获取接口
        /// <summary>
        /// 卷宗文件获取接口
        /// </summary>
        /// <param name="DWBM"></param>
        /// <param name="BH"></param>
        /// <param name="JZBH"></param>
        //[WebMethod(Description = "卷宗文件获取接口")]
        public void GetDossierFileInfo(string DWBM, string BH, string JZBH)
        {
            //返回参数
            string rt = string.Empty;
            EDRS.BLL.YX_DZJZ_JZML bll = new EDRS.BLL.YX_DZJZ_JZML(HttpContext.Current.Request);
            DataSet ds = bll.GetDossierFileInfo(DWBM, BH, JZBH);

            //判断是否查询出现异常
            if (ds != null && ds.Tables.Count > 0)
            {
                //判断查询没有数据
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0].Copy();

                    //成功
                    rt = "{\"Stat\":0,\"Msg\":\"查询成功\",\"Data\":" + EDRS.Common.JsonHelper.JsonString(dt) + "}";
                }
                else
                {
                    //该案件没有卷
                    rt = "{\"Stat\":1,\"Msg\":\"该卷宗没有文件信息\",\"Data\":\"\"}";
                }
            }
            else
            {
                //查询失败
                rt = "{\"Stat\":1,\"Msg\":\"查询失败\",\"Data\":\"\"}";
            }

            //返回结果加密
            //rt = EDRS.Common.DEncrypt.DEncrypt.Encrypt(rt, key);

            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("gbk");
            HttpContext.Current.Response.Write(rt);
            HttpContext.Current.Response.End();
        }
        #endregion

        #region 卷宗信息获取
        /// <summary>
        /// 卷宗信息获取
        /// </summary>
        /// <param name="bmsah"></param>
        //[WebMethod(Description = "卷宗信息获取")]
        public void GetDossierInfo(string BH, string DWBM)
        {
            //返回参数
            string Str = string.Empty;
            EDRS.BLL.YX_DZJZ_JZML bll = new EDRS.BLL.YX_DZJZ_JZML(HttpContext.Current.Request);
            DataSet ds = bll.GetDossierInfo(BH, DWBM);
            //判断是否查询出现异常
            if (ds != null && ds.Tables.Count > 0)
            {
                //判断查询没有数据
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0].Copy();
                    dt.Columns.Remove("JZBH");
                    dt.Columns.Remove("JZMC");
                    //成功
                    Str = "{\"Stat\":0,\"Msg\":\"查询成功\",\"Data\":" + EDRS.Common.JsonHelper.JsonString(dt) + "}";

                }
                else
                {
                    //该案件没有卷
                    Str = "{\"Stat\":1,\"Msg\":\"该卷宗信息下没有文件\",\"Data\":\"\"}";

                }
            }
            else
            {
                //查询失败
                Str = "{\"Stat\":1,\"Msg\":\"查询失败\",\"Data\":\"\"}";

            }
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("gbk");
            HttpContext.Current.Response.Write(Str);
            HttpContext.Current.Response.End();
        }
        #endregion

        #region 卷宗页数据获取接口
        /// <summary>
        /// 卷宗页数据获取接口
        /// </summary>
        /// <param name="DWBM"></param>
        /// <param name="BH"></param>
        /// <param name="JZBH"></param>
        /// <param name="JZWJYBH"></param>
        //[WebMethod(Description = "卷宗页数据获取接口")]
        public void GetDossierFilePage(string DWBM, string BH, string JZBH, string JZWJYBH)
        {
            //返回参数
            string Str = string.Empty;
            EDRS.BLL.YX_DZJZ_JZMLWJ bll = new EDRS.BLL.YX_DZJZ_JZMLWJ(HttpContext.Current.Request);
            DataSet ds = bll.GetList(" and DWBM=:DWBM and BMSAH=:BMSAH and JZBH=:JZBH and WJXH=:WJXH", new object[] { DWBM, BH, JZBH, JZWJYBH });
            //判断是否查询出现异常
            if (ds != null && ds.Tables.Count > 0)
            {
                //判断查询没有数据
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //成功
                    Str = "{\"Stat\":0,\"Msg\":\"查询成功\",\"Data\":" + EDRS.Common.JsonHelper.JsonString(ds.Tables[0]) + "}";

                }
                else
                {
                    //该案件没有卷
                    Str = "{\"Stat\":1,\"Msg\":\"该卷宗页数据不存在\",\"Data\":\"\"}";

                }
            }
            else
            {
                //查询失败
                Str = "{\"Stat\":1,\"Msg\":\"查询失败\",\"Data\":\"\"}";

            }
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("gbk");
            HttpContext.Current.Response.Write(Str);
            HttpContext.Current.Response.End();
        }
        #endregion

        #region 卷宗页获取
        /// <summary>
        /// 卷宗页获取
        /// </summary>
        /// <param name="DWBM">单位编号</param>
        /// <param name="BH">部门受案号</param>
        /// <param name="JZBH">卷宗编号</param>
        /// <param name="JZWJBH">卷宗文件编号</param>
        //[WebMethod(Description = "卷宗信息获取")]
        public void GetDossierFilePageInfo(string DWBM, string BH, string JZBH, string JZWJBH)
        {
            //返回参数
            string Str = string.Empty;
            EDRS.BLL.YX_DZJZ_JZML bll = new EDRS.BLL.YX_DZJZ_JZML(HttpContext.Current.Request);
            DataSet ds = bll.GetDossierFilePageInfo(DWBM, BH, JZBH, JZWJBH);
            //判断是否查询出现异常
            if (ds != null && ds.Tables.Count > 0)
            {
                //判断查询没有数据
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //成功
                    Str = "{\"Stat\":0,\"Msg\":\"查询成功\",\"Data\":" + EDRS.Common.JsonHelper.JsonString(ds.Tables[0]) + "}";

                }
                else
                {
                    //该案件没有卷
                    Str = "{\"Stat\":1,\"Msg\":\"该文件信息下没有文件页\",\"Data\":\"\"}";

                }
            }
            else
            {
                //查询失败
                Str = "{\"Stat\":1,\"Msg\":\"查询失败\",\"Data\":\"\"}";

            }
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("gbk");
            HttpContext.Current.Response.Write(Str);
            HttpContext.Current.Response.End();
        }

        #endregion


    }
#endif
}
