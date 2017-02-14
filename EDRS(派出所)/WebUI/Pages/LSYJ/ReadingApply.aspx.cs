using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDRS.Common;
using System.Text;
using System.Data;

namespace WebUI.Pages.LSYJ
{
    public partial class ReadingApply : BasePage
    {

        /// <summary>
        /// 获取当前时间
        /// </summary>
        protected DateTime nowTime = DateTime.Now;

        #region 页面加载
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["YJUser"] != null)
            //    YJUser = JsonHelper.JsonString(Session["YJUser"] as EDRS.Model.YX_DZJZ_LSAJBD);

            string type = Request["t"];

            if (!string.IsNullOrEmpty(type))
            {
                Response.ContentEncoding = Encoding.UTF8;
                Response.ContentType = "application/json";

                if (type.Equals("ReadLogin"))
                    Response.Write(ReadLogin());
                if (type.Equals("GetMlTree"))
                {
                    EDRS.BLL.YX_DZJZ_JZML bll = new EDRS.BLL.YX_DZJZ_JZML(Request);
                    Response.Write(bll.GetMlTreeEx(Request, false, true));
                }
                if (type.Equals("ListBind"))
                    Response.Write(ListBind());

                if (type.Equals("applypass"))
                    Response.Write(applypass());

                if (type.Equals("applyno"))
                    Response.Write(applyno());
                if (type.Equals("PrintFile"))
                    Response.Write(PrintFileByXh());
                Response.End();
            }
        } 
        #endregion

        #region 登录账号阅卷
        /// <summary>
        /// 登录账号阅卷
        /// </summary>
        /// <returns></returns>
        private string ReadLogin()
        {

            string where = string.Empty;

            //string yjzh = Request.Form["txt_yjzh"].Trim();
            //string yjmm = Request.Form["txt_yjmm"].Trim();

            string yjxh = Request["yjxh"];

            //where += " and YJZH='" + StringPlus.ReplaceSingle(yjzh) + "' and YJMM='" + StringPlus.ReplaceSingle(yjmm) + "' ";
            where += " and YJXH='"+StringPlus.ReplaceSingle(yjxh)+"'";
            //where += " and GH='" + UserInfo.GH + "'";
            //where += " and DWBM='" + UserInfo.DWBM + "'";
            //where += " and YJKSSJ <=to_date('" + DateTime.Now + "','yyyy-mm-dd hh24:mi:ss')  ";
            //where += " and YJJSSJ >=to_date('" + DateTime.Now + "','yyyy-mm-dd hh24:mi:ss')  ";
            object[] values = new object[0];

            EDRS.BLL.YX_DZJZ_LSAJBD bll = new EDRS.BLL.YX_DZJZ_LSAJBD(Request);
            List<EDRS.Model.YX_DZJZ_LSAJBD> listmodel = bll.GetModelList(where, values);
            if (listmodel != null && listmodel.Count > 0)
            {
                Session["YjData"] = listmodel[0];
                return JsonHelper.JsonString(listmodel);
            }
            return ReturnString.JsonToString(Prompt.error, "请确认阅卷时间是否未开始或者已结束", null);
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

            int pageNumber = int.Parse(page);
            int pageSize = int.Parse(rows);
            string where = " and T.SFSC='N'";

            object[] values = new object[3];
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
                where += " and LSZH like :LSZH";
                values[2] = "%" + gh + "%";
            }
            
            EDRS.BLL.YX_DZJZ_WJSQDY bll = new EDRS.BLL.YX_DZJZ_WJSQDY(this.Request);

            DataSet ds = bll.GetListByPageEx(where, "SQSJ desc", (pageSize * pageNumber) - pageSize + 1, (pageSize * pageNumber), values);
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

        #region 审核通过
        public string applypass()
        {
            string lszh = Request["lszh"];
            string dysqdh = Request["dysqdh"];
            string sqsj = Request["sqsj"];
           // string shsm = Request["shsm"];
            //判断非空
            if (!string.IsNullOrEmpty(lszh) && !string.IsNullOrEmpty(sqsj) && !string.IsNullOrEmpty(dysqdh))
            {
                EDRS.BLL.YX_DZJZ_LSYJSQ bll = new EDRS.BLL.YX_DZJZ_LSYJSQ(Request);
                {
                    EDRS.Model.YX_DZJZ_LSYJSQ model = new EDRS.Model.YX_DZJZ_LSYJSQ();
                    model.LSZH = lszh;
                    // model.YJSQDH = dysqdh;
                    model.SQSJ = Convert.ToDateTime(sqsj);
                    model.SQSM = "";
                    model.SFSC = "N";
                    model.SHRGH = UserInfo.GH;
                    model.SHR = UserInfo.MC;
                    model.SHSM = "";
                    model.SHSJ = DateTime.Now;
                    model.YJSQDM = dysqdh;

                    model.SQDZT = "Y";

                    if (bll.Add(model))
                    {
                        return ReturnString.JsonToString(Prompt.win, "审核成功", null);
                    }
                    else
                    {
                        return ReturnString.JsonToString(Prompt.error, "审核失败", null);
                    }
                }

            }
            return ReturnString.JsonToString(Prompt.error, "未获取文件信息", null); 
        }
        #endregion

        #region 审核不通过
        public string applyno()
        {
            string lszh = Request["lszh"];
            string dysqdh = Request["dysqdh"];
            string sqsj = Request["sqsj"];
            string shsm = Request["shsm"];
            //判断非空
            if (!string.IsNullOrEmpty(lszh) && !string.IsNullOrEmpty(sqsj) && !string.IsNullOrEmpty(dysqdh))
            {
                EDRS.BLL.YX_DZJZ_LSYJSQ bll = new EDRS.BLL.YX_DZJZ_LSYJSQ(Request);

                EDRS.Model.YX_DZJZ_LSYJSQ model = new EDRS.Model.YX_DZJZ_LSYJSQ();
                model.LSZH = lszh;
                // model.YJSQDH = dysqdh;
                model.SQSJ = Convert.ToDateTime(sqsj);
                model.SQSM = "";
                model.SFSC = "N";
                model.SHRGH = UserInfo.GH;
                model.SHR = UserInfo.MC;
                model.SHSM = shsm;
                model.SHSJ = DateTime.Now;
                model.YJSQDM = dysqdh;
                model.SQDZT = "N";

                if (bll.Add(model))
                {
                    return ReturnString.JsonToString(Prompt.win, "审核成功", null);
                }
                else
                {
                    return ReturnString.JsonToString(Prompt.error, "审核失败", null);
                }
            }
            return ReturnString.JsonToString(Prompt.error, "未获取文件信息", null);
        }
        #endregion

        #region 打印
        /// <summary>
        /// 打印
        /// </summary>
        /// <returns></returns>
        private string PrintFileByXh()
        {
            //申请序号
            string xh = Request.Form.Get("xh");
            //选择文件文件序号
            string param = Request.Form.Get("param");

            if (string.IsNullOrEmpty(xh) || string.IsNullOrEmpty(param))
            {
                return ReturnString.JsonToString(Prompt.error, "打印未获取到参数", null);
            }
            string[] paramArr = param.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            if (paramArr.Length == 0)
            {
                return ReturnString.JsonToString(Prompt.error, "请选择需要打印的文件页", null);
            }
            else
            {
                //设置文件是需要打印的
                List<EDRS.Model.YX_DZJZ_WJSQDYJL> listModel = new List<EDRS.Model.YX_DZJZ_WJSQDYJL>();
                EDRS.Model.YX_DZJZ_WJSQDYJL model = null;
                //循环构造集合
                for (int i = 0; i < paramArr.Length; i++)
                {
                    model = new EDRS.Model.YX_DZJZ_WJSQDYJL();
                    model.WJXH = paramArr[i];
                    model.XH = xh;
                    model.SFXYDY = "Y";
                    listModel.Add(model);
                }
                EDRS.BLL.YX_DZJZ_WJSQDY bll = new EDRS.BLL.YX_DZJZ_WJSQDY(Request);
                if (bll.UpdateListJl(listModel))
                {
                    OperateLog.AddLog(OperateLog.LogType.阅卷Web, "打印律师申请文件页成功", UserInfo, UserRole, this.Request);

                    return ReturnString.JsonToString(Prompt.win, "打印成功", null);
                }
                OperateLog.AddLog(OperateLog.LogType.阅卷Web, "打印律师申请文件页失败", UserInfo, UserRole, this.Request);

                return ReturnString.JsonToString(Prompt.error, "打印失败", null);

            }

        } 
        #endregion

    }
}