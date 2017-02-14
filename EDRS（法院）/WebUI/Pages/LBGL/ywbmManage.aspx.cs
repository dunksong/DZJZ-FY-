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

namespace WebUI.Pages.LBGL
{
    public partial class ywbmManage : BasePage
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
            string page = Request.Form["page"];
            string rows = Request.Form["pagesize"];
            string key = Request.Form["key"];           

            int pageNumber = int.Parse(page);
            int pageSize = int.Parse(rows);
            string where = string.Empty;

            List<object> values = new List<object>();

            if (!string.IsNullOrEmpty(key))
            {
                where += " and YWMC like :YWMC ";
                values.Add("%" + key + "%");
            }

            EDRS.BLL.XT_DM_YWBM bll = new EDRS.BLL.XT_DM_YWBM(this.Request);

            DataSet ds = bll.GetListByPage(where + " and SFSC='N' ", " YWBM asc", (pageSize * pageNumber) - pageSize + 1, (pageSize * pageNumber), values.ToArray());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.参数配置Web, "业务编码绑定成功", UserInfo, UserRole, this.Request);
                int count = bll.GetRecordCount(where + " and SFSC='N' ", values.ToArray());
                DataTable dt = (ds.Tables[0]).Copy();

                return "{\"Total\":" + count + ",\"Rows\":" + JsonHelper.JsonString(dt) + "}";
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.参数配置Web, "业务编码绑定列表未找到信息", UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "未找到类别", null);
        }
        #endregion

        #region 添加类别
        /// <summary>
        /// 添加类别
        /// </summary>
        /// <returns></returns>
        private string AddData()
        {
            string msg = string.Empty;
            if (!ProvingFrom(ref msg))
            {
                return ReturnString.JsonToString(Prompt.error, msg, null);
            }

            string ywbm = "01";

            XT_DM_YWBM bll = new XT_DM_YWBM(this.Request);
            DataSet ds = bll.GetListByPage("", "ywbm desc", 1, 1, null);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                int num = int.Parse(ds.Tables[0].Rows[0]["YWBM"].ToString());
                num++;
                ywbm = num.ToString().PadLeft(2, '0');
            }

            EDRS.Model.XT_DM_YWBM model = new EDRS.Model.XT_DM_YWBM();
            model.YWBM = ywbm;
            model.YWMC = Request.Form["txt_name"];
            model.YWJC = Request.Form["txt_namejc"];
            model.BZ = Request.Form["txt_bz"];           
            model.SFSC = "N";           

            if (bll.Add(model))
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.参数配置Web, "添加业务编码成功", "", UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.win, "保存成功", null);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.参数配置Web, "添加业务编码失败", "", UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "保存失败", null);
        }
        #endregion

        #region 修改类别
        /// <summary>
        /// 修改类别
        /// </summary>
        /// <returns></returns>
        private string UpData()
        {
            string ywbm = Request.Form.Get("key_hidd").Trim();
            if (string.IsNullOrEmpty(ywbm))
            {
                return ReturnString.JsonToString(Prompt.error, "未找到需要修改信息", null);
            }
            string msg = string.Empty;
            if (!ProvingFrom(ref msg))
            {
                return ReturnString.JsonToString(Prompt.error, msg, null);
            }

            XT_DM_YWBM bll = new XT_DM_YWBM(this.Request);
            EDRS.Model.XT_DM_YWBM model = bll.GetModel(ywbm);
            if (model != null)
            {

                model.YWMC = Request.Form["txt_name"];
                model.YWJC = Request.Form["txt_namejc"];
                model.BZ = Request.Form["txt_bz"];           
               
                if (bll.Update(model))
                {
                    //数据日志
                    OperateLog.AddLog(OperateLog.LogType.参数配置Web, "修改业务编码成功", "", UserInfo, UserRole, this.Request);
                    return ReturnString.JsonToString(Prompt.win, "保存成功", null);
                }
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.参数配置Web, "修改业务编码失败", "", UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.error, "保存失败", null);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.参数配置Web, "修改业务编码未找到信息", "", UserInfo, UserRole, this.Request);
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
            XT_DM_YWBM bll = new XT_DM_YWBM(this.Request);

            if (bll.DeleteList(ids))
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.参数配置Web, "删除业务编码成功", Request.Form["cs"], UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.win, "删除业务编码成功", null);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.参数配置Web, "删除业务编码失败", Request.Form["cs"], UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "删除业务编码失败", null);
        }
        #endregion

        #region 根据类别编号获取数据
        /// <summary>
        /// 根据类别编号获取数据
        /// </summary>
        /// <param name="DWBM"></param>
        /// <returns></returns>
        private string GetModel(string ywbm)
        {
            if (string.IsNullOrEmpty(ywbm))
            {
                ywbm = Request.Form.Get("id");
                if (string.IsNullOrEmpty(ywbm))
                {
                    return ReturnString.JsonToString(Prompt.error, "参数错误", null);
                }
            }
            XT_DM_YWBM bll = new XT_DM_YWBM(this.Request);
            EDRS.Model.XT_DM_YWBM model = bll.GetModel(ywbm);
            if (model != null)
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.参数配置Web, "根据编号获取业务编码信息成功", "", UserInfo, UserRole, this.Request);
                return JsonHelper.JsonString(model);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.参数配置Web, "根据编号获取业务编码信息失败", "", UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "获取数据失败", null);
        }
        #endregion

        #region 表单验证
        /// <summary>
        /// 表单验证
        /// </summary>
        private bool ProvingFrom(ref string msg)
        {
            string mc = Request.Form["txt_name"];
            string jc = Request.Form["txt_namejc"];
            string bz = Request.Form["txt_bz"];
         
            if (string.IsNullOrEmpty(mc))
            {
                msg = "请输入业务名称！";
                return false;
            }
            if (string.IsNullOrEmpty(jc))
            {
                msg = "请输入业务简称！";
                return false;
            }
            if (!string.IsNullOrEmpty(mc) && mc.Length > 30)
            {
                msg = "业务名称不能超过30个字！";
                return false;
            }
            if (!string.IsNullOrEmpty(jc) && jc.Length > 30)
            {
                msg = "业务简称不能超过30个字！";
                return false;
            }
            if (!string.IsNullOrEmpty(bz) && bz.Length > 400)
            {
                msg = "备注不能超过400个字！";
                return false;
            }
            return true;
        }
        #endregion

   

    }
}