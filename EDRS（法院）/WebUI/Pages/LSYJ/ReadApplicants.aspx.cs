using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using EDRS.Common;

namespace WebUI.Pages.LSYJ
{
    public partial class ReadApplicants : BasePage
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request["t"];
            if (!string.IsNullOrEmpty(type))
            {
                Response.ContentEncoding = Encoding.UTF8;
                Response.ContentType = "application/json";
                switch (type)
                {
                    case "gridbind":
                        Response.Write(GridBind());
                        break;
                    case "add":
                        Response.Write(AddData());
                        break;
                    case "update":
                        Response.Write(Update());
                        break;
                    case "delete":
                        Response.Write(Delete());
                        break;
                    case "getmodel":
                        Response.Write(GetModelById());
                        break;
                }
                Response.End();
            }
        }


        /// <summary>
        /// 绑定列表
        /// </summary>
        /// <returns></returns>
        private string GridBind()
        {
            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["pagesize"]);
            string key = Request["key"];
            string where = string.Empty;
            object[] values = new object[1];
            if (!string.IsNullOrEmpty(key))
            {
                where += " and SQBM like :SQBM ";
                values[0] = "%" + key + "%";
            }
            where += " and SFSC='N'";
            EDRS.BLL.YX_DZJZ_LSYJXZSQ bll = new EDRS.BLL.YX_DZJZ_LSYJXZSQ();
            DataSet ds = bll.GetListByPage(where, "SQSJ desc", (pageSize * pageIndex) - pageSize + 1, (pageSize * pageIndex), values);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                int count = bll.GetRecordCount(where, values);
                return "{\"Total\":" + count + ",\"Rows\":" + JsonHelper.JsonString(ds.Tables[0]) + "}";
            }
            return ReturnString.JsonToString(Prompt.error, "未找到信息", null);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        private string AddData()
        {
            if (!PageValidate.IsLengthStr(Request.Form.Get("txt_lszh"), 1, 100))
                return ReturnString.JsonToString(Prompt.error, "律师证号" + PageValidate.Msg, "txt_lszh");
            if (!PageValidate.IsLengthStr(Request.Form.Get("txt_lsxm"), 1, 50))
                return ReturnString.JsonToString(Prompt.error, "律师姓名" + PageValidate.Msg, "txt_lsxm");
            if (!PageValidate.IsLengthStr(Request.Form.Get("txt_ssdw"), 1, 200))
                return ReturnString.JsonToString(Prompt.error, "所属单位" + PageValidate.Msg, "txt_ssdw");
            if (!PageValidate.IsLengthStr(Request.Form.Get("txt_ajmc"), 1, 200))
                return ReturnString.JsonToString(Prompt.error, "名称" + PageValidate.Msg, "txt_ajmc");
            if (!PageValidate.IsLengthStr(Request.Form.Get("txt_xyrmc"), 1, 50))
                return ReturnString.JsonToString(Prompt.error, "嫌疑人" + PageValidate.Msg, "txt_xyrmc");
            if (!PageValidate.IsLengthStr(Request.Form.Get("txt_sqrmc"), 1, 50))
                return ReturnString.JsonToString(Prompt.error, "申请人" + PageValidate.Msg, "txt_sqrmc");
            if (!PageValidate.IsLengthStr(Request.Form.Get("txt_sqdw"), 1, 200))
                return ReturnString.JsonToString(Prompt.error, "申请单位" + PageValidate.Msg, "txt_sqdw");
        
            EDRS.Model.YX_DZJZ_LSYJXZSQ model = new EDRS.Model.YX_DZJZ_LSYJXZSQ();
            model.LSZH = Request.Form.Get("txt_lszh");
            model.LSXM = Request.Form.Get("txt_lsxm");
            model.SSDW = Request.Form.Get("txt_ssdw");
            model.AJMC = Request.Form.Get("txt_ajmc");
            model.XYRMC = Request.Form.Get("txt_xyrmc");
            model.SQRMC = Request.Form.Get("txt_sqrmc");
            model.SQDW = Request.Form.Get("txt_sqdw");
            model.XZDZ = "";
            model.SQDZT = "D";
            model.SQSJ = DateTime.Now;
            model.BZ = "";
            model.SFSC = "N";
            model.SHRGH = "";
            model.SHR ="";
            //model.SHSJ = "";

            EDRS.BLL.YX_DZJZ_LSYJXZSQ bll = new EDRS.BLL.YX_DZJZ_LSYJXZSQ();
            if (bll.Add(model))
                return ReturnString.JsonToString(Prompt.win, "保存成功", null);
            return ReturnString.JsonToString(Prompt.error, "保存失败", null);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        private string Update()
        {
            string SQBM = Request["hidd_sqbm"];
            if (string.IsNullOrEmpty(SQBM))
            {
                return ReturnString.JsonToString(Prompt.error, "参数错误", null);
            }
            EDRS.BLL.YX_DZJZ_LSYJXZSQ bll = new EDRS.BLL.YX_DZJZ_LSYJXZSQ();
            EDRS.Model.YX_DZJZ_LSYJXZSQ model = bll.GetModel(SQBM);
            if (model != null)
            {
                if (!PageValidate.IsLengthStr(Request.Form.Get("txt_lszh"), 1, 100))
                    return ReturnString.JsonToString(Prompt.error, "律师证号" + PageValidate.Msg, "txt_lszh");
                if (!PageValidate.IsLengthStr(Request.Form.Get("txt_lsxm"), 1, 50))
                    return ReturnString.JsonToString(Prompt.error, "律师姓名" + PageValidate.Msg, "txt_lsxm");
                if (!PageValidate.IsLengthStr(Request.Form.Get("txt_ssdw"), 1, 200))
                    return ReturnString.JsonToString(Prompt.error, "所属单位" + PageValidate.Msg, "txt_ssdw");
                if (!PageValidate.IsLengthStr(Request.Form.Get("txt_ajmc"), 1, 200))
                    return ReturnString.JsonToString(Prompt.error, "名称" + PageValidate.Msg, "txt_ajmc");
                if (!PageValidate.IsLengthStr(Request.Form.Get("txt_xyrmc"), 1, 50))
                    return ReturnString.JsonToString(Prompt.error, "嫌疑人" + PageValidate.Msg, "txt_xyrmc");
                if (!PageValidate.IsLengthStr(Request.Form.Get("txt_sqrmc"), 1, 50))
                    return ReturnString.JsonToString(Prompt.error, "申请人" + PageValidate.Msg, "txt_sqrmc");
                if (!PageValidate.IsLengthStr(Request.Form.Get("txt_sqdw"), 1, 200))
                    return ReturnString.JsonToString(Prompt.error, "申请单位" + PageValidate.Msg, "txt_sqdw");
                model.LSZH = Request.Form.Get("txt_lszh");
                model.LSXM = Request.Form.Get("txt_lsxm");
                model.SSDW = Request.Form.Get("txt_ssdw");
                model.AJMC = Request.Form.Get("txt_ajmc");
                model.XYRMC = Request.Form.Get("txt_xyrmc");
                model.SQRMC = Request.Form.Get("txt_sqrmc");
                model.SQDW = Request.Form.Get("txt_sqdw");

                if (bll.Update(model))
                    return ReturnString.JsonToString(Prompt.win, "保存成功",null);
                return ReturnString.JsonToString(Prompt.error, "保存失败", null);
            }
            return ReturnString.JsonToString(Prompt.error, "修改信息不存在", null);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        private string Delete()
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
            EDRS.BLL.YX_DZJZ_LSYJXZSQ bll = new EDRS.BLL.YX_DZJZ_LSYJXZSQ();
            if (bll.DeleteList(ids))
            {
                return ReturnString.JsonToString(Prompt.win, "删除数据成功", null);
            }
            return ReturnString.JsonToString(Prompt.error, "删除数据失败", null);
        }
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <returns></returns>
        private string GetModelById()
        {
            string SQBM = Request["sqbm"];
            if (string.IsNullOrEmpty(SQBM))
            {
                return ReturnString.JsonToString(Prompt.error, "参数错误", null);
            }
            EDRS.BLL.YX_DZJZ_LSYJXZSQ bll = new EDRS.BLL.YX_DZJZ_LSYJXZSQ();
            EDRS.Model.YX_DZJZ_LSYJXZSQ model = bll.GetModel(SQBM);
            if (model != null)
            {
                return JsonHelper.JsonString(model);
            }
            return ReturnString.JsonToString(Prompt.error, "获取数据失败", null);
        }
    }
}