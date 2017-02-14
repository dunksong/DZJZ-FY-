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

namespace WebUI.Pages.Print
{
    public partial class PrintFrontCover1 : BasePage
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
                where += " and BT like :BT ";
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
            YX_DZJZ_FMDY bll = new YX_DZJZ_FMDY(this.Request);
           
            EDRS.Model.YX_DZJZ_FMDY model = new EDRS.Model.YX_DZJZ_FMDY();
            model.BT = Request.Form.Get("txt_bt");
            model.FBT = Request.Form.Get("txt_fbt");

            model.AJMC = Request.Form.Get("txt_ajmc");
            model.AJBH = Request.Form.Get("txt_ajbh");
            model.FZXYR = Request.Form.Get("txt_fzxyr");
            if (!string.IsNullOrEmpty(Request.Form.Get("txt_lasj")))
                model.LASJ = Convert.ToDateTime(Request.Form.Get("txt_lasj"));
         
            if (!string.IsNullOrEmpty(Request.Form.Get("txt_jasj")))
                model.JASJ = Convert.ToDateTime(Request.Form.Get("txt_jasj"));
            model.LJDW = Request.Form.Get("txt_ljdw");
            model.LJR = Request.Form.Get("txt_ljr");
            model.SHR = Request.Form.Get("txt_shr");
            model.BAGJ = string.IsNullOrEmpty(Request.Form.Get("txt_bagj")) ? 0 : int.Parse(Request.Form.Get("txt_bagj"));
            model.DJJ = Request.Form.Get("txt_djj");
            model.GJY = string.IsNullOrEmpty(Request.Form.Get("txt_gjy")) ? 0 : int.Parse(Request.Form.Get("txt_gjy"));
            model.CZRGH = UserInfo.GH;
            model.CZR = UserInfo.MC;
            model.CZSJ = DateTime.Now;
            model.CZIP = Request.ServerVariables["REMOTE_ADDR"];
            model.CZLX = "";

            if (bll.Add(model))
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "打印成功","", UserInfo, UserRole, this.Request);
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

            YX_DZJZ_FMDY bll = new YX_DZJZ_FMDY(this.Request);
            EDRS.Model.YX_DZJZ_FMDY model = bll.GetModel(pzbm);
            if (model != null)
            {
                model.BT = Request.Form.Get("txt_bt");
                model.FBT = Request.Form.Get("txt_fbt");
                model.AJMC = Request.Form.Get("txt_ajmc");
                model.AJBH = Request.Form.Get("txt_ajbh");
                model.FZXYR = Request.Form.Get("txt_fzxyr");
                if (!string.IsNullOrEmpty(Request.Form.Get("txt_lasj")))
                    model.LASJ = Convert.ToDateTime(Request.Form.Get("txt_lasj"));
                if (!string.IsNullOrEmpty(Request.Form.Get("txt_jasj")))
                    model.JASJ = Convert.ToDateTime(Request.Form.Get("txt_jasj"));
                model.LJDW = Request.Form.Get("txt_ljdw");
                model.LJR = Request.Form.Get("txt_ljr");
                model.SHR = Request.Form.Get("txt_shr");
                model.BAGJ = string.IsNullOrEmpty(Request.Form.Get("txt_bagj")) ? 0 : int.Parse(Request.Form.Get("txt_bagj"));
                model.DJJ = Request.Form.Get("txt_djj");
                model.GJY = string.IsNullOrEmpty(Request.Form.Get("txt_gjy")) ? 0 : int.Parse(Request.Form.Get("txt_gjy"));
                model.CZRGH = UserInfo.GH;
                model.CZR = UserInfo.MC;
                model.CZSJ = DateTime.Now;
                model.CZIP = Request.ServerVariables["REMOTE_ADDR"];
                model.CZLX = "";
                if (bll.Update(model))
                {
                    //数据日志
                    OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "修改方面打印成功","", UserInfo, UserRole, this.Request);
                    return ReturnString.JsonToString(Prompt.win, "保存成功", null);
                }
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "修改方面打印失败","", UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.error, "保存失败", null);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "修改方面打印未找到信息", Request.Form.Get("txt_cs"), UserInfo, UserRole, this.Request);
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
            YX_DZJZ_FMDY bll = new YX_DZJZ_FMDY(this.Request);

            if (bll.DeleteList(ids))
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "删除方面打印成功", Request.Form["cs"], UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.win, "删除数据成功", null);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "删除方面打印失败", Request.Form["cs"], UserInfo, UserRole, this.Request);
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
                OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "根据编号获取方面打印信息成功", "", UserInfo, UserRole, this.Request);
                return JsonHelper.JsonString(model);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "根据编号获取方面打印信息参数失败", Request["cs"], UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "获取数据失败", null);
        }
        #endregion

        #region 表单验证
        /// <summary>
        /// 表单验证
        /// </summary>
        private bool ProvingFrom(ref string msg)
        {
            string bt = Request.Form.Get("txt_bt");
            string fbt = Request.Form.Get("txt_fbt");
            if (string.IsNullOrEmpty(bt))
            {
                msg = "请输入标题！";
                return false;
            }
            if (string.IsNullOrEmpty(fbt))
            {
                msg = "请输入副标题！";
                return false;
            }
            return true;
        }
        #endregion

      


    }
}