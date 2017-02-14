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
using System.IO;
using System.Text.RegularExpressions;

namespace WebUI.Pages.Business
{
    public partial class MakeCheck : BasePage
    {
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
                    int count = 0;
                    TYYW_GG_AJJBXX bll = new TYYW_GG_AJJBXX(Request);
                    DataTable dt = bll.ListBind(this.Request, UserInfo.DWBM, UserInfo.GH, "2,3,4,6"," and WSBH is not null", ref count);
                    Response.Write("{\"Total\":" + count + ",\"Rows\":" + JsonHelper.JsonString(dt) + "}");
                }
                if (type.Equals("GetMlTree"))
                {
                    EDRS.BLL.YX_DZJZ_JZML bll = new EDRS.BLL.YX_DZJZ_JZML(Request);
                    Response.Write(bll.GetMlTree(Request, false, true));
                }
                if (type.Equals("Add"))
                {
                    Response.Write(UpData());
                }
                if (type.Equals("sdsb"))
                {
                    Response.Write(SetBs());
                }
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
                where += " and CONFIGNAME like :CONFIGNAME ";
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
            string msg = string.Empty;
            //if (!ProvingFrom(ref msg))
            //{
            //    return ReturnString.JsonToString(Prompt.error, msg, null);
            //}
            YX_DZJZ_FMDY bll = new YX_DZJZ_FMDY(this.Request);

            EDRS.Model.YX_DZJZ_FMDY model = new EDRS.Model.YX_DZJZ_FMDY();
            model.BT = Request.Form.Get("txt_bt");
            model.FBT = Request.Form.Get("txt_fbt");
            model.AJMC = Request.Form.Get("txt_ajmc");
            model.AJBH = Request.Form.Get("txt_ajbh");
            model.FZXYR = Request.Form.Get("txt_fzxyr");
            model.LASJ = Convert.ToDateTime(Request.Form.Get("txt_lasj"));
            model.JASJ = Convert.ToDateTime(Request.Form.Get("txt_jasj"));
            model.LJDW = Request.Form.Get("txt_ljdw");
            model.LJR = Request.Form.Get("txt_ljr");
            model.SHR = Request.Form.Get("txt_shr");
            model.BAGJ = int.Parse(Request.Form.Get("txt_bagj"));
            model.DJJ = Request.Form.Get("txt_djj");
            model.GJY = int.Parse(Request.Form.Get("txt_gjy"));
            model.CZRGH = UserInfo.GH;
            model.CZR = UserInfo.MC;
            model.CZSJ = DateTime.Now;
            model.CZIP = "";
            model.CZLX = "";

            if (bll.Add(model))
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "打印成功", "", UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.win, "保存成功", null);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "打印失败", "", UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "保存失败", null);
        }
        #endregion

        #region 修改配置数据
        /// <summary>
        /// 修改配置数据
        /// </summary>
        /// <returns></returns>
        private string UpData()
        {
            string ids = Request.Form["bmsah"];
            string[] id = ids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            ids = "";
            for (int i = 0; i < id.Length; i++)
            {
                ids += "'" + id[i].Trim() + "'";
                if (i < id.Length - 1)
                    ids += ",";
            }
            if (string.IsNullOrEmpty(ids))
            {
                return ReturnString.JsonToString(Prompt.error, "未找到需要修改信息", null);
            }

            YX_DZJZ_JZJBXX bll = new YX_DZJZ_JZJBXX(this.Request);


            if (bll.UpdateByZZZTList(ids, UserInfo.MC, DateTime.Now, UserInfo.GH, Request.Form.Get("type"), Request.Form.Get("txt_pz")))
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "操作成功", "", UserInfo, UserRole, this.Request);
                foreach (string key in ids.Split(','))
                {
                    string bmsah = key.Replace("'", "");
                    EDRS.Model.YX_DZJZ_JZJBXX model = bll.GetModel(bmsah);
                    if (model != null && (model.ZZZT == "4" || model.ZZZT == "3"))
                    {
                        try
                        {
                            WebReference.jzfk _interface = new WebReference.jzfk();
                            string status = model.ZZZT == "4" ? "1" : "0";
                            string result = _interface.updateStatus(model.AJBH, model.WSBH, status);
                            string msg = "通知审核状态记录：" + result + "|本地状态：" + model.ZZZT + "|AJBH:" + model.AJBH + "|WSBH:" + model.WSBH + "|" + status;
                            try
                            {
                                string NoticeAuditStatusFilePath = Path.Combine(Path.Combine(GetRootPath(), "NoticeFile"), "NoticeWaitAuditStatus.txt");
                                FileStream fs = new FileStream(NoticeAuditStatusFilePath, FileMode.Append);
                                StreamWriter sr = new StreamWriter(fs, System.Text.Encoding.Default);
                                sr.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "|" + msg);
                                sr.Close();
                                fs.Close();
                            }
                            catch (Exception ex)
                            { }

                            OperateLog.AddLog(OperateLog.LogType.卷宗OCR及打包状态, "通知状态改变成功：[案件编号=" + model.AJBH + "，文书编号=" + model.WSBH + "]", model.BMSAH + "|" + model.AJBH + "|" + model.WSBH, null, null, this.Request);
                        }
                        catch (Exception ex)
                        {
                            OperateLog.AddLog(OperateLog.LogType.卷宗OCR及打包状态, "状态回写失败：" + ex.Message, "", UserInfo, UserRole, this.Request);
                        }
                    }
                }
                return ReturnString.JsonToString(Prompt.win, "操作成功", null);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "操作失败", "", UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "操作失败", null);
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
                OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "删除参数配置成功", Request.Form["cs"], UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.win, "删除数据成功，重新登录生效", null);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "删除参数配置失败", Request.Form["cs"], UserInfo, UserRole, this.Request);
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
                OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "根据编号获取参数配置信息成功", "", UserInfo, UserRole, this.Request);
                return JsonHelper.JsonString(model);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "根据编号获取参数配置信息参数失败", Request["cs"], UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "获取数据失败", null);
        }
        #endregion

        #region 表单验证
        /// <summary>
        /// 表单验证
        /// </summary>
        private bool ProvingFrom(ref string msg)
        {
            string type = Request.Form.Get("slct_type_val");
            string value = Request.Form["txt_value"];
            if (string.IsNullOrEmpty(type))
            {
                msg = "请选择配置类型！";
                return false;
            }
            if (string.IsNullOrEmpty(value))
            {
                msg = "请输入对应配置的值！";
                return false;
            }
            return true;
        }
        #endregion

        #region 手动报送
        /// <summary>
        /// 手动报送
        /// </summary>
        /// <returns></returns>
        private string SetBs()
        {
            string AJBH = Request.Form["AJBH"];
            string WSBH = Request.Form["WSBH"];
            string ZZZT = Request.Form["ZZZT"];
            string Str = "";
            if (string.IsNullOrEmpty(AJBH) || string.IsNullOrEmpty(WSBH) || ZZZT != "6")
            {
                return "{\"Stat\":1,\"Msg\":\"报送通知失败，手动报送只能针对报送失败的案件。\",\"Data\":\"\"}";
            }
            try
            {
                string bmsahs = GetText();
                if (bmsahs.IndexOf(WSBH) > -1)
                {
                    Str = "{\"Stat\":1,\"Msg\":\"报送通知失败，上一次报送通知尚未处理，请不要重复通知\",\"Data\":\"\"}";
                    OperateLog.AddLog(OperateLog.LogType.卷宗OCR及打包状态, "接收到报送通知失败：上一次报送通知尚未处理，不重复接收。[案件编号=" + AJBH + "，文书编号=" + WSBH + "]", "|" + AJBH + "|" + WSBH, null, null, this.Context.Request);
                   
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
                        
                        OperateLog.AddLog(OperateLog.LogType.卷宗OCR及打包状态, "不接收的案件报送通知：不存在对应文书编号的案件。[案件编号=" + AJBH + "，文书编号=" + WSBH + "]", "|" + AJBH + "|" + WSBH, null, null, this.Context.Request);
                    }
                    else
                    {
                        string jzbh = ds.Tables[0].Rows[0]["JZBH"].ToString();
                        EDRS.Model.YX_DZJZ_JZJBXX jz = bll.GetModel(jzbh);
                        if (jz == null)
                        {
                            Str = "{\"Stat\":1,\"Msg\":\"报送通知失败，无对应案件信息\",\"Data\":\"\"}";
                            OperateLog.AddLog(OperateLog.LogType.卷宗OCR及打包状态, "不接收的案件报送通知：不存在对应文书编号的案件。[案件编号=" + AJBH + "，文书编号=" + WSBH + "]", "|" + AJBH + "|" + WSBH, null, null, this.Context.Request);
                          
                        }
                        else
                        {
                            //审核通过或已上报允许报送
                            //if (jz.ZZZT == "4" || jz.ZZZT == "5")
                            //{
                                SaveText(bmsahs);
                                Str = "{\"Stat\":0,\"Msg\":\"报送通知成功\"}";
                                OperateLog.AddLog(OperateLog.LogType.卷宗OCR及打包状态, "接收到报送通知成功：[案件编号=" + AJBH + "，文书编号=" + WSBH + "]", "|" + AJBH + "|" + WSBH, null, null, this.Context.Request);
                                
                            //}
                            //else
                            //{
                            //    Str = "{\"Stat\":1,\"Msg\":\"报送通知失败，当前报送案件尚未审核通过\",\"Data\":\"\"}";
                            //    OperateLog.AddLog(OperateLog.LogType.卷宗OCR及打包状态, "接收到报送通知成功：[案件编号=" + AJBH + "，文书编号=" + WSBH + "]", "|" + AJBH + "|" + WSBH, null, null, this.Context.Request);
                              
                            //}
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Str = "{\"Stat\":1,\"Msg\":\"接收报送通知失败：错误信息请查看系统日志！\",\"Data\":\"\"}";
                OperateLog.AddLog(OperateLog.LogType.卷宗OCR及打包状态, "接收到报送通知失败：处理报送数据时发生系统错误，详情查看日志。[案件编号=" + AJBH + "，文书编号=" + WSBH + "]", "|" + AJBH + "|" + WSBH, null, null, this.Context.Request);
                EDRS.Common.LogHelper.LogError(this.Context.Request, "Exception", ex.Message, "public void SetBs(string AJBH, string WSBH)", "WebUI.MakeCheck", "");
            }
            return Str;
        }
        object _lock = new object();
        private string NoticeFilePath = Path.Combine(Path.Combine(GetRootPath(), "NoticeFile"), "NoticeFile.txt");
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
    }
}