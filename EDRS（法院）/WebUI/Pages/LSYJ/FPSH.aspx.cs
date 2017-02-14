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
    public partial class FPSH : BasePage
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
                if (type.Equals("ReadLogin"))
                    Response.Write(ReadLogin());
                if (type.Equals("GetMlTree"))
                {
                    EDRS.BLL.YX_DZJZ_JZML bll = new EDRS.BLL.YX_DZJZ_JZML(Request);
                    Response.Write(bll.GetMlTreeEx(Request, false, true));
                }
                if (type.Equals("applypass"))
                    Response.Write(applypass());
           
                Response.End();
            }
        }
        #region 登录账号阅卷
        /// <summary>
        /// 登录账号阅卷
        /// </summary>
        /// <returns></returns>
        private string ReadLogin()
        {

            string where = string.Empty;
            string yjxh = Request["yjxh"];
            where += " and YJXH='" + StringPlus.ReplaceSingle(yjxh) + "'";
            object[] values = new object[0];
            EDRS.BLL.YX_DZJZ_LSAJBD bll = new EDRS.BLL.YX_DZJZ_LSAJBD(Request);
            List<EDRS.Model.YX_DZJZ_LSAJBD> listmodel = bll.GetModelList(where, values);
            if (listmodel != null && listmodel.Count > 0)
            {
                return JsonHelper.JsonString(listmodel);
            }
            return ReturnString.JsonToString(Prompt.error, "请确认阅卷时间是否未开始或者已结束", null);
        }
        #endregion

        #region 审核通过
        public string applypass()
        {
            string state = Request.Form["state"];
            string yjsqd = Request.Form["yjsqd"];
            string shsm = Request.Form["shsm"];

            EDRS.BLL.YX_DZJZ_LSYJSQ bll = new EDRS.BLL.YX_DZJZ_LSYJSQ(Request);
            //判断非空
            if (!string.IsNullOrEmpty(yjsqd))
            {
                if (state == "Y")
                {
                    EDRS.Model.YX_DZJZ_LSYJSQ model = bll.GetModel(yjsqd);
                    model.SHRGH = UserInfo.GH;
                    model.SHR = UserInfo.MC;
                    model.SQDZT = "Y";
                    model.SHSJ = DateTime.Now;
                    model.SHSM = "";
                    if (bll.Update(model))
                    {
                        return ReturnString.JsonToString(Prompt.win, "审核成功", null);
                    }
                    else
                    {
                        return ReturnString.JsonToString(Prompt.error, "审核失败", null);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(shsm))
                    {
                        EDRS.Model.YX_DZJZ_LSYJSQ model = bll.GetModel(yjsqd);
                        model.SHRGH = UserInfo.GH;
                        model.SHR = UserInfo.MC;
                        model.SQDZT = "N";
                        model.SHSJ = DateTime.Now;
                        model.SHSM = shsm;
                        if (bll.Update(model))
                        {
                            return ReturnString.JsonToString(Prompt.win, "审核成功", null);
                        }
                        else
                        {
                            return ReturnString.JsonToString(Prompt.error, "审核失败", null);
                        }
                    }
                    else
                    {
                        return ReturnString.JsonToString(Prompt.error, "审核说明不能为空", null);
                    }
                }
            }
            return ReturnString.JsonToString(Prompt.error, "未获取文件信息", null);
        }
        #endregion


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
                where += " and t.BMSAH like :BMSAH";
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
            where += " and SHRGH='" + UserInfo.GH + "'";
            //where += " and JDRGH = :JDRGH";
            //where += " and JDDWBM=:JDDWBM";
            //values[4] = UserInfo.GH;
            //values[5] = UserInfo.DWBM;

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
    }
}