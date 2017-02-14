using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Text.RegularExpressions;
using EDRS.Common;
using Newtonsoft.Json;
using System.Data;
using System.Text;
using EDRS.BLL;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace WebUI
{
    public partial class Login : System.Web.UI.Page
    {
        public static HttpRequest request;
        public string vr = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            

            request = this.Request;
       
#if PSB
            vr = "PSB";// string.Format("版本：V{0}", version.FileVersion);
#endif
            string type = Request.Form["action"];
            System.DateTime.Now.Year.ToString();

            if (!string.IsNullOrEmpty(type))
            {
                Response.ContentEncoding = Encoding.UTF8;
                Response.ContentType = "application/json";
                if (type.Equals("GetDwbm"))
                    Response.Write(ListBindDW());
                else if (type.Equals("UserLogin"))
                    Response.Write(UserLogin());
                Response.End();
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>

        private string UserLogin()
        {

            //List<FromValue> list = JsonConvert.DeserializeObject<List<FromValue>>(data);
            //if (list == null || list.Count == 0)
            //    return ReturnString.JsonToString(Prompt.error, "参数错误", "");

            string type = "";
            string name = "";
            string value = "";

            if (string.IsNullOrEmpty(Request.Form.Get("tree_select_id")) && string.IsNullOrEmpty(Request.Form.Get("tree_select_hid")))
                return ReturnString.JsonToString(Prompt.error, "请先选择单位", "tree_select");

            if (string.IsNullOrEmpty(Request.Form.Get("txtUser")))
                return ReturnString.JsonToString(Prompt.error, "用户名不能为空", "txtUser");
            if (!Regex.IsMatch(Request.Form.Get("txtUser").ToString(), @"^(\w){1,20}$"))
                return ReturnString.JsonToString(Prompt.error, "用户名输入不正确", "txtUser");

            if (string.IsNullOrEmpty(Request.Form.Get("txtPwd")))
                return ReturnString.JsonToString(Prompt.error, "密码不能为空", "txtPwd");
            if (!Regex.IsMatch(Request.Form.Get("txtPwd").ToString(), @"^(\w){6,20}$"))
                return ReturnString.JsonToString(Prompt.error, "密码输入不正确", "txtPwd");

            if (string.IsNullOrEmpty(Request.Form.Get("tree_select_id")))
                type = Request.Form.Get("tree_select_hid");
            else
                type = Request.Form.Get("tree_select_id").ToString();
            name = Request.Form.Get("txtUser").ToString();
            value = Request.Form.Get("txtPwd").ToString();

            //else if (fv.Name == "txtVCode")
            //{
            //    if (string.IsNullOrEmpty(fv.Value.ToString().Trim()))
            //        return ReturnString.JsonToString(Prompt.error, "验证码不能为空", "txtVCode");
            //    if (HttpContext.Current.Session["ValidateCode"].ToString().ToLower() != fv.Value.ToString().ToLower())
            //        return ReturnString.JsonToString(Prompt.error, "验证码错误", "txtVCode");
            //}


            //}

            //1bbd886460827015e5d605ed44252251  8个1   [a-zA-Z]\w{1,3}

            EDRS.BLL.XT_ZZJG_RYBM bll = new EDRS.BLL.XT_ZZJG_RYBM(request);
            string msg = string.Empty;
            List<EDRS.Model.XT_QX_JSBM> jsbmList;
            try
            {
                EDRS.BLL.XT_ZZJG_DWBM dwbmBll = new EDRS.BLL.XT_ZZJG_DWBM(request);
                EDRS.Model.XT_ZZJG_DWBM dwbmmodel = dwbmBll.GetModel(type);
                if (dwbmmodel == null)
                    return ReturnString.JsonToString(Prompt.error, "选择单位不存在，请重新选择", null);
                EDRS.Model.XT_ZZJG_RYBM rybm = bll.UserLogin(type, name, value, out jsbmList, out msg);
                if (rybm != null)
                {
                    HttpContext.Current.Session["user"] = rybm;
                    HttpContext.Current.Session["userDwbm"] = dwbmmodel;
                    if (jsbmList != null)
                    {
                        HttpContext.Current.Session["userRole"] = jsbmList;
                    }

                    HttpCookie cookie = new HttpCookie("login");
                    //cookie.Values[EDRS.Common.DEncrypt.DESEncrypt.Encrypt("UnitOption", "UnitOption")] = EDRS.Common.DEncrypt.DESEncrypt.Encrypt(rybm.DWBM, "UnitOption");
                    //cookie.Values[EDRS.Common.DEncrypt.DESEncrypt.Encrypt("UserName", "UserName")] = EDRS.Common.DEncrypt.DESEncrypt.Encrypt(rybm.DLBM, "UserName");
                    cookie.Values["UnitOption"] = rybm.DWBM;
                    cookie.Values["UnitOptionName"] = HttpUtility.UrlEncode(rybm.DWMC, Encoding.UTF8);
                    cookie.Values["UserName"] = HttpUtility.UrlEncode(rybm.DLBM, Encoding.UTF8);
                    cookie.Expires = DateTime.MaxValue;
                    HttpContext.Current.Response.Cookies.Add(cookie);


                    OperateLog.AddLog(OperateLog.LogType.登录系统, msg, rybm, jsbmList, request);
                    return ReturnString.JsonToString(Prompt.win, msg, null);
                }
                else
                {
                    rybm = new EDRS.Model.XT_ZZJG_RYBM();
                    XT_ZZJG_DWBM dwbmbll = new XT_ZZJG_DWBM(request);
                    EDRS.Model.XT_ZZJG_DWBM dwbm = dwbmbll.GetModel(type);
                    if (dwbm != null)
                    {
                        rybm.MC = name;
                        rybm.DWBM = dwbm.DWBM;
                        rybm.DWMC = dwbm.DWMC;
                    }
                    OperateLog.AddLog(OperateLog.LogType.登录系统, msg, rybm, jsbmList, request);
                    return ReturnString.JsonToString(Prompt.error, msg, null);
                }

            }
            catch (Exception ex)
            {
                msg = Regex.Replace(ex.Message, "[\r\n\"]", "");
                return ReturnString.JsonToString(Prompt.error, msg, null);
            }
        }
        #region 绑定数据列表
        /// <summary>
        /// 绑定数据列表
        /// </summary>
        /// <returns></returns>
        private string ListBindDW()
        {
            //数据查询条件
            string where = string.Empty;
            string withWhere = " FDWBM is null ";
            string siftWhere = "";
            object[] values = new object[3];
            where += " and SFSC=:SFSC";
            values[0] = "N";
            string key = Request["key"];
            if (!string.IsNullOrEmpty(key))
            {
                where += " and (DWMC like :DWMC or DWJC like :DWJC)";
                values[1] = "%" + key + "%";
                values[2] = "%" + key + "%";
            }
            //树形循环条件
            bool direction = true;
            bool isOpen = false;

            string levelNum = " and level < 3 ";
            bool isNameAll = false;
            string openPid = ""; //展开父级编号
            string pid = Request["pid"];
            if (!string.IsNullOrEmpty(pid))
                withWhere = " FDWBM = '" + pid + "'";
            else if (!string.IsNullOrEmpty(ConfigHelper.GetConfigString("DWBM")))
            {
                pid = ConfigHelper.GetConfigString("DWBM");
                withWhere = " (FDWBM = '" + pid + "' or DWBM = '" + pid + "')";
            }

            //根据搜索名称查询节点            
            string treeText = Request["treeText"];
            if (!string.IsNullOrEmpty(treeText))
            {
                siftWhere = " DWMC like '%" + treeText + "%'";
                //direction = false;
                levelNum = "";
                isOpen = true;
            }

            where += levelNum;
            try
            {
                XT_ZZJG_DWBM bll = new XT_ZZJG_DWBM(this.Request);
                DataSet ds = bll.GetTreeListById(where, withWhere, siftWhere, direction, values);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0].Copy();

                    dt.Columns["DWBM"].ColumnName = "id";
                    dt.Columns["FDWBM"].ColumnName = "pid";
                    dt.Columns["DWMC"].ColumnName = "text";
                    //DataColumn col = new DataColumn();
                    //col.ColumnName = "icon";
                    //col.DefaultValue = "'/images/icons/3.png'";
                    //dt.Columns.Add(col);

                    string d = JsonHelper.JsonString(dt);
                    return d;
                }
                if(string.IsNullOrEmpty(key))
                    return ReturnString.JsonToString(Prompt.error, "未找到单位的数据", null);
                return "";
            }
            catch (Exception ex)
            {
                return ReturnString.JsonToString(Prompt.error, ex.Message, null);
            }
        }
        #endregion
        /*

        #region 绑定数据列表
        /// <summary>
        /// 绑定数据列表
        /// </summary>
        /// <returns></returns>
        private string ListBindDW()
        {
            //数据查询条件
            string where = string.Empty;
            string withWhere = " FDWBM is null ";
            string siftWhere = "";
            object[] values = new object[3];
            where += " and SFSC=:SFSC";          
            values[0] = "N";
            string key = Request["key"];
            if (!string.IsNullOrEmpty(key))
            {
                where += " and (DWMC like :DWMC or DWJC like :DWJC)";
                values[1] = "%" + key + "%";
                values[2] = "%" + key + "%";
            }
            //树形循环条件
            bool direction = true;
            bool isOpen = false;
         
            string levelNum = " and level < 3 ";
            bool isNameAll = false;
            string openPid = ""; //展开父级编号
            string pid = Request["pid"];
            if (!string.IsNullOrEmpty(pid))
                withWhere = " FDWBM = '" + pid + "'";
            else if (!string.IsNullOrEmpty(ConfigHelper.GetConfigString("DWBM")))
            {
                pid = ConfigHelper.GetConfigString("DWBM");
                withWhere = " (FDWBM = '" + pid + "' or DWBM = '" + pid + "')";
            }
           
            //根据搜索名称查询节点            
            string treeText = Request["treeText"];
            if (!string.IsNullOrEmpty(treeText))
            {
                siftWhere = " DWMC like '%" + treeText + "%'";
                //direction = false;
                levelNum = "";
                isOpen = true;
            }

            where += levelNum;
            try
            {
                XT_ZZJG_DWBM bll = new XT_ZZJG_DWBM(this.Request);
                DataSet ds = bll.GetTreeListById(where, withWhere, siftWhere, direction, values);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    dt.Columns["DWBM"].ColumnName = "ID";
                    dt.Columns["FDWBM"].ColumnName = "PARENTID";
                    dt.Columns["DWMC"].ColumnName = "NAME";
                    //获取当前节点中的父级节点
                    DataRow[] drs = dt.Select("ID='" + pid + "'");
                    if (!string.IsNullOrEmpty(ConfigHelper.GetConfigString("DWBM")) && drs.Length > 0)
                    {
                        pid = drs[0]["PARENTID"].ToString();
                        openPid = pid;
                    }
                    string str = new TreeJson(dt, "ID", "NAME", "PARENTID", "", "ISLEAF", openPid, string.IsNullOrEmpty(pid) ? "" : pid, isOpen, isNameAll, false).ResultJson.ToString();
                    return str;
                }
                return ReturnString.JsonToString(Prompt.error, "未找到单位的数据", null);
            }
            catch (Exception ex)
            {
                return ReturnString.JsonToString(Prompt.error, ex.Message, null);
            }
        }
        #endregion
         * 
         * 
         * */
    }
}