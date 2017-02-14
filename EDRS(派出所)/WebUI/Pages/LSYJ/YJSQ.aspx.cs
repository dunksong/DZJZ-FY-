using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDRS.BLL;
using EDRS.Common;
using System.Data;
using System.Text;

namespace WebUI.Pages.LSYJ
{
    public partial class YJSQ : BasePage
    {
        /// <summary>
        /// 获取当前时间
        /// </summary>
        protected DateTime nowTime = DateTime.Now;

        protected void Page_Load(object sender, EventArgs e)
        {
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
                if (type.Equals("ExUpData"))
                    Response.Write(ExUpData());
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
            string page = Request["page"];
            string rows = Request["pagesize"];
            string key = Request["key"];
            string ajmc = Request["ajmc"];
            string gh = Request["gh"];
            string mc = Request["mc"];

            int pageNumber = int.Parse(page);
            int pageSize = int.Parse(rows);
            string where = " and T.SFSC='N'";

            object[] values = new object[6];
            if (!string.IsNullOrEmpty(key))
            {
                where += " and AJBH like :AJBH";
                values[0] = "%" + key + "%";
            }
            if (!string.IsNullOrEmpty(ajmc))
            {
                where += " and AJMC like :AJMC";
                values[1] = "%" + ajmc + "%";
            }
            if (!string.IsNullOrEmpty(gh))
            {
                where += " and GH like :GH";
                values[2] = "%" + gh + "%";
            }
            if (!string.IsNullOrEmpty(mc))
            {
                where += " and MC like :MC";
                values[3] = "%" + mc + "%";
            }

            where += " and JDRGH = :JDRGH";
            where += " and JDDWBM=:JDDWBM";
            values[4] = UserInfo.GH;
            values[5] = UserInfo.DWBM;

            EDRS.BLL.YX_DZJZ_LSAJBD bll = new EDRS.BLL.YX_DZJZ_LSAJBD(this.Request);

            DataSet ds = bll.GetListByPage(where, "YJJSSJ desc,JDSJ desc", (pageSize * pageNumber) - pageSize + 1, (pageSize * pageNumber), values);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.阅卷Web, "获取阅卷绑定列表成功", UserInfo, UserRole, this.Request);
                int count = bll.GetRecordCount(where, values);
                return "{\"Total\":" + count + ",\"Rows\":" + JsonHelper.JsonString(ds.Tables[0]) + "}";
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.阅卷Web, "获取阅卷绑定列表-未找到信息", UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "未找到阅卷绑定信息", null);
        } 
        #endregion

        #region 提交申请
        /// <summary>
        /// 提交申请
        /// </summary>
        /// <returns></returns>
        private string AddData()
        {
            string msg = string.Empty;
            if (!ProvingFrom(ref msg))
            {
                return ReturnString.JsonToString(Prompt.error, msg, null);
            }
            YX_DZJZ_LSYJSQ bll = new YX_DZJZ_LSYJSQ(this.Request);
            //DataSet ds = bll.GetList(" and SFSC='N' and SQDZT<>'N' and YJSQDH=:YJSQDH", new string[] { Request.Form["txt_yjsqdh"] });
            //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //    return ReturnString.JsonToString(Prompt.error, "该阅卷申请单号已被申请过", null);
            EDRS.Model.YX_DZJZ_LSYJSQ model = new EDRS.Model.YX_DZJZ_LSYJSQ();
            model.LSZH = "";// Request.Form["txt_lszh"];
            //model.YJSQDH = Request.Form["txt_yjsqdh"];
            model.SQSJ = DateTime.Now;
            model.SQSM = Request.Form["txt_sqsm"];
            model.SFSC = "N";
            model.SHRGH = UserInfo.GH;
            model.SHR = UserInfo.MC;
            model.SHSM = "";
            model.SHSJ = DateTime.Now;
            model.YJSQDM = Request.Form["txt_yjsqdm"];           
            model.SQDZT = "Y";

            if (bll.Add(model))
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.阅卷Web, "提交阅卷申请成功", model.YJSQDH, UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.win, "保存成功", null);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.阅卷Web, "提交阅卷申请失败", model.YJSQDH, UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "保存失败", null);
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        private string UpData()
        {
            string id = Request.Form.Get("key_hidd").Trim();
            if (string.IsNullOrEmpty(id))
            {
                return ReturnString.JsonToString(Prompt.error, "未找到需要修改信息", null);
            }
            string msg = string.Empty;
            if (!ProvingFrom(ref msg))
            {
                return ReturnString.JsonToString(Prompt.error, msg, null);
            }

            YX_DZJZ_LSYJSQ bll = new YX_DZJZ_LSYJSQ(this.Request);
            EDRS.Model.YX_DZJZ_LSYJSQ model = bll.GetModel(id);
            if (model != null)
            {
                model.LSZH = "";// Request.Form["txt_lszh"];
               // model.YJSQDH = Request.Form["txt_yjsqdh"];
                model.YJSQDM = Request.Form["txt_yjsqdm"];
                model.SQSJ = DateTime.Now;
                model.SQSM = Request.Form["txt_sqsm"];
                if (bll.Update(model))
                {
                    //数据日志
                    OperateLog.AddLog(OperateLog.LogType.阅卷Web, "修改阅卷申请成功", id, UserInfo, UserRole, this.Request);
                    return ReturnString.JsonToString(Prompt.win, "保存成功", null);
                }
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.阅卷Web, "修改阅卷申请失败", id, UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.error, "保存失败", null);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.阅卷Web, "修改阅卷申请-未找到修改信息", id, UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "未找到需要修改信息", null);
        }
        #endregion

        #region 审核
        /// <summary>
        /// 审核
        /// </summary>
        /// <returns></returns>
        private string ExUpData()
        {
            string id = Request.Form.Get("key_ex_hidd").Trim();
            if (string.IsNullOrEmpty(id))
            {
                return ReturnString.JsonToString(Prompt.error, "未找到需要修改信息", null);
            }
            string msg = string.Empty;
            if (!ProvingFrom(ref msg))
            {
                return ReturnString.JsonToString(Prompt.error, msg, null);
            }

            YX_DZJZ_LSYJSQ bll = new YX_DZJZ_LSYJSQ(this.Request);
            EDRS.Model.YX_DZJZ_LSYJSQ model = bll.GetModel(id);
            if (model != null)
            {
                model.SHSJ = DateTime.Now;
                model.SQDZT = Request.Form["rad_sqdzt"];
                model.SHSM = Request.Form["txt_shsm"];
                model.SHR = UserInfo.MC;
                model.SHRGH = UserInfo.GH;
                if (bll.Update(model))
                {
                    //数据日志
                    OperateLog.AddLog(OperateLog.LogType.阅卷Web, "修改参数配置成功", model.YJSQDH, UserInfo, UserRole, this.Request);
                    return ReturnString.JsonToString(Prompt.win, "保存成功", null);
                }
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.阅卷Web, "修改参数配置失败", model.YJSQDH, UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.error, "保存失败", null);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.阅卷Web, "修改参数配置未找到信息", id, UserInfo, UserRole, this.Request);
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

            YX_DZJZ_LSAJBD bll = new YX_DZJZ_LSAJBD(this.Request);

            if (bll.DeleteList(ids))
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.阅卷Web, "删除阅卷申请成功","", UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.win, "删除数据成功", null);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.阅卷Web, "删除阅卷申请失败","", UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "删除数据失败", null);
        }
        #endregion

        #region 根据单位获取配置数据
        /// <summary>
        /// 根据单位获取配置数据
        /// </summary>
        /// <param name="DWBM"></param>
        /// <returns></returns>
        private string GetModel(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                id = Request["id"];
                if (string.IsNullOrEmpty(id))
                {
                    return ReturnString.JsonToString(Prompt.error, "参数错误", null);
                }
            }
            YX_DZJZ_LSYJSQ bll = new YX_DZJZ_LSYJSQ(this.Request);
            EDRS.Model.YX_DZJZ_LSYJSQ model = bll.GetModel(id);
            if (model != null)
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.阅卷Web, "根据编号获取阅读申请成功", id, UserInfo, UserRole, this.Request);
                return JsonHelper.JsonString(model);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.阅卷Web, "根据编号获取阅读申请-未找到信息", id, UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "获取数据失败", null);
        }
        #endregion

        #region 表单验证
        /// <summary>
        /// 表单验证
        /// </summary>
        private bool ProvingFrom(ref string msg)
        {
            //string type = Request.Form.Get("slct_type_val");
            //string value = Request.Form["txt_value"];
            //if (string.IsNullOrEmpty(type))
            //{
            //    msg = "请选择配置类型！";
            //    return false;
            //}
            //if (string.IsNullOrEmpty(value))
            //{
            //    msg = "请输入对应配置的值！";
            //    return false;
            //}
            return true;
        }
        #endregion

    }
}