using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using EDRS.Common;
using System.Text.RegularExpressions;

namespace WebUI
{
    public partial class PageButManager : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            string[] t = Request.PathInfo.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            string type = Request.Form["t"];

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
                Response.End();
            }
        }

        /// <summary>
        /// 绑定列表
        /// </summary>
        /// <returns></returns>
        public string ListBind()
        {
            string page = Request.Form["page"];
            string rows = Request.Form["pagesize"];
            string key = Request.Form["key"];

            int pageNumber = int.Parse(page);
            int pageSize = int.Parse(rows);
            string where = string.Empty;
            string order = string.Empty;
            object[] values = new object[1];
            if (!string.IsNullOrEmpty(key))
            {
                where += " and ANMC=:ANMC";
                values[0] = string.Format("%{0}%", key);
            }
            //    where += " and DWBM like :DWBM ";
            //    values[0] = "%" + key + "%";
            //    order = "FLXH";
            //}
            // where += " and DWBM='" + UserInfo.DWBM + "'";
            order = "GNXH";
            EDRS.BLL.XT_QX_ANDY bll = new EDRS.BLL.XT_QX_ANDY(this.Request);

            DataSet ds = bll.GetListByPage(where, order, (pageSize * pageNumber) - pageSize + 1, (pageSize * pageNumber), values);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                //数据日志
                //OperateLog.AddLog(OperateLog.LogType.功能分类管理Web, "绑定功能分类列表成功", UserInfo, UserRole, this.Request);
                int count = bll.GetRecordCount(where, values);
                return "{\"Total\":" + count + ",\"Rows\":" + JsonHelper.JsonString(ds.Tables[0]) + "}";
            }
            //数据日志
            //OperateLog.AddLog(OperateLog.LogType.功能分类管理Web, "绑定功能分类列表未找到信息", UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "未找到功能分类信息", null);
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
            EDRS.BLL.XT_QX_ANDY bll = new EDRS.BLL.XT_QX_ANDY(this.Request);

            int count= bll.GetRecordCount(" and ANBH = :ANBH" ,new object[] { Request.Form.Get("txt_xh").Trim() });
            if (count > 0)
            {
                return ReturnString.JsonToString(Prompt.error, "按钮编号已存在", null);                
            }
            EDRS.Model.XT_QX_ANDY model = new EDRS.Model.XT_QX_ANDY();
            model.ANBH = Request.Form.Get("txt_xh").Trim();
            model.ANMC = Request.Form.Get("txt_mc").Trim();
            model.YMMC = Request.Form.Get("txt_ymmc_val").Trim();         

            if (bll.Add(model))
            {
                //数据日志
                //OperateLog.AddLog(OperateLog.LogType.功能分类管理Web, "添加功能分类成功", model.FLMC, UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.win, "保存成功", null);
            }
            //数据日志
            //OperateLog.AddLog(OperateLog.LogType.功能分类管理Web, "添加功能分类失败", model.FLMC, UserInfo, UserRole, this.Request);
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
            string anbh = Request.Form.Get("key_hidd").Trim();
            if (string.IsNullOrEmpty(anbh))
            {
                //数据日志
                //OperateLog.AddLog(OperateLog.LogType.功能分类管理Web, "修改功能分类的参数错误", UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.error, "未找到需要修改信息", null);
            }
            string msg = string.Empty;
            if (!ProvingFrom(ref msg))
            {
                //数据日志
                //OperateLog.AddLog(OperateLog.LogType.功能分类管理Web, "修改功能分类验证失败：" + msg, Request.Form.Get("txt_mc"), UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.error, msg, null);
            }

            EDRS.BLL.XT_QX_ANDY bll = new EDRS.BLL.XT_QX_ANDY(this.Request);

            int count = bll.GetRecordCount(" and ANBH = :ANBH", new object[] { Request.Form.Get("txt_xh").Trim() });
            if (count > 0)
            {
                return ReturnString.JsonToString(Prompt.error, "按钮编号已存在", null);
            }
           
            EDRS.Model.XT_QX_ANDY model = bll.GetModel(anbh);
            if (model != null)
            {
                model.ANBH = Request.Form.Get("txt_xh").Trim();
                model.ANMC = Request.Form.Get("txt_mc").Trim();
                model.YMMC = Request.Form.Get("txt_ymmc_val").Trim();
                if (bll.Update(model))
                {
                    //数据日志
                    //OperateLog.AddLog(OperateLog.LogType.功能分类管理Web, "修改功能分类成功", model.FLMC, UserInfo, UserRole, this.Request);
                    return ReturnString.JsonToString(Prompt.win, "保存成功", null);
                }
                //数据日志
                //OperateLog.AddLog(OperateLog.LogType.功能分类管理Web, "修改功能分类失败", model.FLMC, UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.error, "保存失败", null);
            }
            //数据日志
            //OperateLog.AddLog(OperateLog.LogType.功能分类管理Web, "修改功能分类未找到信息", Request.Form.Get("txt_mc"), UserInfo, UserRole, this.Request);
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
            EDRS.BLL.XT_QX_ANDY bll = new EDRS.BLL.XT_QX_ANDY(this.Request);

            if (bll.DeleteList(ids))
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.功能分类管理Web, "删除功能分类成功", Request.Form["mc"], UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.win, "删除数据成功", null);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.功能分类管理Web, "删除功能分类失败", Request.Form["mc"], UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "删除数据失败", null);
        }
        #endregion

        #region 根据单位获取配置数据
        /// <summary>
        /// 根据单位获取配置数据
        /// </summary>
        /// <param name="DWBM"></param>
        /// <returns></returns>
        private string GetModel(string anbm)
        {
            if (string.IsNullOrEmpty(anbm))
            {
                anbm = Request.Form["id"];
                if (string.IsNullOrEmpty(anbm))
                {
                    //数据日志
                    //OperateLog.AddLog(OperateLog.LogType.功能分类管理Web, "根据编号获取功能分类信息参数错误", UserInfo, UserRole, this.Request);
                    return ReturnString.JsonToString(Prompt.error, "参数错误", null);
                }
            }
            EDRS.BLL.XT_QX_ANDY bll = new EDRS.BLL.XT_QX_ANDY(this.Request);
            EDRS.Model.XT_QX_ANDY model = bll.GetModel(anbm);
            if (model != null)
            {
                //数据日志
                //OperateLog.AddLog(OperateLog.LogType.功能分类管理Web, "根据编号获取功能分类信息成功", model.FLMC, UserInfo, UserRole, this.Request);
                return JsonHelper.JsonString(model);
            }
            //数据日志
            //OperateLog.AddLog(OperateLog.LogType.功能分类管理Web, "根据编号获取功能分类信息参数失败", Request["mc"], UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "获取数据失败", null);
        }
        #endregion

        #region 表单验证
        /// <summary>
        /// 表单验证
        /// </summary>
        private bool ProvingFrom(ref string msg)
        {
            string mc = Request.Form.Get("txt_mc");//按钮名称
            string ymmc = Request.Form.Get("txt_ymmc_val").Trim();//页面名称
            string xh = Request.Form.Get("txt_xh");//按钮编号

            if (string.IsNullOrEmpty(mc) || !string.IsNullOrEmpty(mc) && mc.Trim().Length > 50)
            {
                msg = "按钮名称必须填写，最多50字符！";
                return false;
            }
            if (string.IsNullOrEmpty(xh) || !string.IsNullOrEmpty(xh) && xh.Trim().Length > 50)
            {
                msg = "按钮编号必须填写，最多50字符！";
                return false;
            }
            else if (!Regex.IsMatch(xh, @"^\w{1,50}$"))
            {
                msg = "按钮编号只能包含字母、数字和下划线！";
                return false;
            }
            if (string.IsNullOrEmpty(ymmc) || !string.IsNullOrEmpty(ymmc) && ymmc.Trim().Length > 100)
            {
                msg = "页面名称必须填写，最多100字符！";
                return false;
            }
            return true;
        }
        #endregion
    }
}