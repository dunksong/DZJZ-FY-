using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Newtonsoft.Json;

using EDRS.BLL;
using System.Text;
using System.Text.RegularExpressions;
using EDRS.Common;
using System.Data;
using System.ComponentModel;

namespace WebUI
{
    public partial class SysConfig : BasePage
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
                    Response.Write(ListBind());
                if (type.Equals("AddData"))
                    Response.Write(AddData());
                if (type.Equals("UpData"))
                    Response.Write(UpData());
                if (type.Equals("DelData"))
                    Response.Write(DelData());
                if (type.Equals("GetModel"))
                    Response.Write(GetModel(""));
                if (type.Equals("GetType"))
                    Response.Write(ConfigType());
                if (type.Equals("ConfigTypeValue"))
                    Response.Write(ConfigTypeValue());
                Response.End();
            }
        }

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

            EDRS.BLL.XY_DZJZ_XTPZ bll = new EDRS.BLL.XY_DZJZ_XTPZ(this.Request);
            
            DataSet ds = bll.GetListByPage(where, "", (pageSize * pageNumber) - pageSize + 1, (pageSize * pageNumber), values);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.参数配置Web, "绑定参数配置列表成功", UserInfo, UserRole, this.Request);
                int count = bll.GetRecordCount(where, values);
                DataTable dt = (ds.Tables[0]).Copy();

                foreach (DataRow dr in dt.Rows)
                {
                    dr["CONFIGNAME"] = Enum.Parse(typeof(EnumConfig), dr["CONFIGID"].ToString(), true).ToString();
                }
                return "{\"Total\":" + count + ",\"Rows\":" + JsonHelper.JsonString(dt) + "}";
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.参数配置Web, "绑定参数配置列表未找到信息", UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "未找到配置信息", null);
        }

        #region 添加配置数据
        /// <summary>
        /// 添加配置数据
        /// </summary>
        /// <returns></returns>
        private string AddData()
        {
            string msg = string.Empty;
            if (!ProvingFrom(ref msg))
            {
                return ReturnString.JsonToString(Prompt.error, msg, null);
            }
            XY_DZJZ_XTPZ bll = new XY_DZJZ_XTPZ(this.Request);
            DataSet ds = bll.GetList(" and SystemMark=1 and ConfigId=:ConfigId", new string[] { Request.Form["slct_type_val"] });
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                return ReturnString.JsonToString(Prompt.error, "选择的配置已被添加", null);
            EDRS.Model.XY_DZJZ_XTPZ model = new EDRS.Model.XY_DZJZ_XTPZ();
            model.CONFIGID = Convert.ToInt32(Request.Form["slct_type_val"]);
            model.CONFIGNAME = Enum.Parse(typeof(EnumConfig), Request.Form["slct_type_val"], true).ToString();
            if (!string.IsNullOrEmpty(Request.Form["txt_value_val"]))
                model.CONFIGVALUE = Request.Form["txt_value_val"];
            else
                model.CONFIGVALUE = Request.Form["txt_value"];
            model.SYSTEMMARK = "1";

            if (bll.Add(model))
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.参数配置Web, "添加参数配置成功", model.CONFIGNAME, UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.win, "保存成功，重新登录生效", null);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.参数配置Web, "添加参数配置失败", model.CONFIGNAME, UserInfo, UserRole, this.Request);
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

            XY_DZJZ_XTPZ bll = new XY_DZJZ_XTPZ(this.Request);
            EDRS.Model.XY_DZJZ_XTPZ model = bll.GetModel(pzbm);
            if (model != null)
            {
                model.CONFIGID = Convert.ToInt32(Request.Form["slct_type_val"]);
                model.CONFIGNAME = Enum.Parse(typeof(EnumConfig), Request.Form["slct_type_val"], true).ToString();
                if (!string.IsNullOrEmpty(Request.Form["txt_value_val"]))
                    model.CONFIGVALUE = Request.Form["txt_value_val"];
                else
                    model.CONFIGVALUE = Request.Form["txt_value"];
                if (bll.Update(model))
                {
                    //数据日志
                    OperateLog.AddLog(OperateLog.LogType.参数配置Web, "修改参数配置成功", model.CONFIGNAME, UserInfo, UserRole, this.Request);
                    return ReturnString.JsonToString(Prompt.win, "保存成功，重新登录生效", null);
                }
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.参数配置Web, "修改参数配置失败", model.CONFIGNAME, UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.error, "保存失败", null);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.参数配置Web, "修改参数配置未找到信息", Request.Form.Get("txt_cs"), UserInfo, UserRole, this.Request);
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
            XY_DZJZ_XTPZ bll = new XY_DZJZ_XTPZ(this.Request);

            if (bll.DeleteList(ids))
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.参数配置Web, "删除参数配置成功", Request.Form["cs"], UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.win, "删除数据成功，重新登录生效", null);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.参数配置Web, "删除参数配置失败",Request.Form["cs"], UserInfo, UserRole, this.Request);
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
            XY_DZJZ_XTPZ bll = new XY_DZJZ_XTPZ(this.Request);
            EDRS.Model.XY_DZJZ_XTPZ model = bll.GetModel(PZBM);
            if (model != null)
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.参数配置Web, "根据编号获取参数配置信息成功", model.CONFIGNAME, UserInfo, UserRole, this.Request);
                return JsonHelper.JsonString(model);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.参数配置Web, "根据编号获取参数配置信息参数失败", Request["cs"], UserInfo, UserRole, this.Request);
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

        #region 配置类型
        /// <summary>
        /// 配置类型
        /// </summary>
        /// <returns></returns>
        private string ConfigType()
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            string[] names = Enum.GetNames(typeof(EnumConfig));
            foreach (string name in names)
            {
                Dictionary<string, string> dicEnum = new Dictionary<string, string>();
                dicEnum.Add("id", ((int)Enum.Parse(typeof(EnumConfig), name, true)).ToString());
                dicEnum.Add("name", name);
                list.Add(dicEnum);
            }

            string js = JsonHelper.JsonString(list);
            return js;
        } 
        #endregion


        private string ConfigTypeValue()
        {
            string id = Request["id"];
            foreach (var item in Enum.GetValues(typeof(EnumConfig)))
            {
                object[] objAttrs = item.GetType().GetField(item.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), true);
                object[] objAmbients = item.GetType().GetField(item.ToString()).GetCustomAttributes(typeof(AmbientValueAttribute), true);
                if (objAttrs != null && objAttrs.Length > 0)
                {                    
                    if ((int)item == int.Parse(id))
                    {
                        DescriptionAttribute desc = objAttrs[0] as DescriptionAttribute;
                        AmbientValueAttribute am = objAmbients[0] as AmbientValueAttribute;
                        return ReturnString.JsonToString(Prompt.win, desc.Description, am.Value.ToString());
                    }
                }
            }
            return ReturnString.JsonToString(Prompt.error, "未知类型", null);
        }
    }
}