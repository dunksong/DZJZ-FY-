using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDRS.Common;
using System.Text;
using System.Data;
using EDRS.OracleDAL;

namespace WebUI.Pages.LSYJ
{
    public partial class ReadingPage : BasePage
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
                    Response.Write(bll.GetMlTree(Request, false, true));
                }
                if (type.Equals("ListBind"))
                    Response.Write(ListBind());

                if (type.Equals("submitapply"))
                    Response.Write(submitapply());

                if (type.Equals("submitapplyall"))
                    Response.Write(submitapplyall());

                Response.End();
            }
        } 
        #endregion 

        #region  申请当前页
        private string submitapply()
        {
            EDRS.BLL.YX_DZJZ_JZMLWJ bll = new EDRS.BLL.YX_DZJZ_JZMLWJ(Request);
            EDRS.BLL.YX_DZJZ_WJSQDY BLLSQ = new EDRS.BLL.YX_DZJZ_WJSQDY(Request);
            string id = Request["id"];
            string wjxh = Request.Form["yjxh"];
            string sq = string.Empty;
            if (!string.IsNullOrEmpty(id))
            {
                sq = " and JZWJBH=:JZWJBH";
            }

            DataSet d = BLLSQ.GetList(sq, new[] { id });
            DataTable dt=d.Tables[0];
            //判断该案件是否已在申请中.
            if (dt != null && dt.Rows.Count==0)
            {
              
                string str = string.Empty;
                str = " and wjxh=:wjxh";
                DataSet ds = bll.GetList(str, new[] { id });
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //string bmsah = ds.Tables[0].Rows[0]["BMSAH"].ToString();
                    EDRS.Model.YX_DZJZ_WJSQDY model = new EDRS.Model.YX_DZJZ_WJSQDY();
                    model.BMSAH = ds.Tables[0].Rows[0]["BMSAH"].ToString();
                    model.DYBMBM = "";  //打印部门编码
                    model.DYBMMC = "";
                    model.DYDWBM = "";
                    model.DYDWMC = "";
                    model.DYFS = null;  //打印份数
                    model.DYFY = null;  //打印费用
                    model.DYR = "";     //打印人
                    model.DYRGH = "";   //打印人工号
                    model.DYSJ = null;  //打印时间
                    model.LSZH = UserInfo.GH;  //律师证号
                    model.DYSQDH = DateTime.Now.ToString("yyyyMMddHHmmssfff") + model.LSZH;
                    model.JZWJBH = id;  //文件编号                  
                    model.SFSC = "N";
                    model.SQFS = 1;     //申请份数
                    model.SQSJ = DateTime.Now; //申请时间
                    model.YJXH = wjxh;  //阅卷序号

                    if (BLLSQ.Add(model))
                    {
                        return ReturnString.JsonToString(Prompt.win, "申请成功", null);
                    }
                    else
                    {
                        return ReturnString.JsonToString(Prompt.error, "申请失败", null);
                    }
                }
            }
            else
                {
                    return ReturnString.JsonToString(Prompt.error, "该文件已申请", null);
                }
            return ReturnString.JsonToString(Prompt.error, "未获取文件信息", null);
           }
            
        #endregion

        #region 申请多页
        public string submitapplyall()
        {
            EDRS.BLL.YX_DZJZ_JZMLWJ bll = new EDRS.BLL.YX_DZJZ_JZMLWJ(Request);
            EDRS.BLL.YX_DZJZ_WJSQDY BLLSQ = new EDRS.BLL.YX_DZJZ_WJSQDY(Request);
            string idstr = Request.Form["idstr[]"];
            string yjxh = Request.Form["yjxh"];
            
            if (!string.IsNullOrEmpty(idstr))
            {
                List<EDRS.Model.YX_DZJZ_WJSQDY> wlist = new List<EDRS.Model.YX_DZJZ_WJSQDY>();
                string[] idarr = idstr.Split(',');

                for (int i = 0; i < idarr.Length; i++)
                {
                    string sq = string.Empty;
                    sq = " and JZWJBH=:JZWJBH";
                    DataSet d = BLLSQ.GetList(sq, new[] { idarr[i] });
                    DataTable dt = d.Tables[0];
                    //判断是否存在该申请记录
                    if (dt != null && dt.Rows.Count == 0)
                    {
                        string lszh = UserInfo.GH;
                        string str = string.Empty;
                        str = " and wjxh=:wjxh";
                        DataSet ds = bll.GetList(str, new[] { idarr[i] });
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            EDRS.Model.YX_DZJZ_WJSQDY model = new EDRS.Model.YX_DZJZ_WJSQDY();
                            model.BMSAH = ds.Tables[0].Rows[0]["BMSAH"].ToString();
                            model.DYBMBM = "";  //打印部门编码
                            model.DYBMMC = "";
                            model.DYDWBM = "";
                            model.DYDWMC = "";
                            model.DYFS = null;  //打印份数
                            model.DYFY = null;  //打印费用
                            model.DYR = "";     //打印人
                            model.DYRGH = "";   //打印人工号
                            model.DYSJ = null;  //打印时间
                            model.LSZH = UserInfo.GH;  //律师证号
                            model.DYSQDH = DateTime.Now.ToString("yyyyMMddHHmmssfff") + model.LSZH;
                            model.JZWJBH = idarr[i];  //文件编号                  
                            model.SFSC = "N";
                            model.SQFS = 1;     //申请份数
                            model.SQSJ = DateTime.Now; //申请时间
                            model.YJXH = yjxh;  //阅卷序号
                            //添加到集合
                            wlist.Add(model);
                        }
                    }
                }
                if (BLLSQ.AddList(wlist))
                {
                    return ReturnString.JsonToString(Prompt.win, "提交申请成功", null);
                }
                else
                {
                    return ReturnString.JsonToString(Prompt.error, "提交申请失败", null);
                }
            }
            return ReturnString.JsonToString(Prompt.error, "未获取文件信息", null); 
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
            where += " and GH='" + UserInfo.GH + "'";
            where += " and DWBM='" + UserInfo.DWBM + "'";
            where += " and YJKSSJ <=to_date('" + DateTime.Now + "','yyyy-mm-dd hh24:mi:ss')  ";
            where += " and YJJSSJ >=to_date('" + DateTime.Now + "','yyyy-mm-dd hh24:mi:ss')  ";
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

            int pageNumber = int.Parse(page);
            int pageSize = int.Parse(rows);
            string where = " and T.SFSC='N'";

            object[] values = new object[1];
            if (!string.IsNullOrEmpty(key))
            {
                where += " and YJSQDH like :YJSQDH";
                values[0] = "%" + key + "%";
            }
           // where += " and T.GH='" + UserInfo.GH + "' and T.DWBM='" + UserInfo.DWBM + "'";

            where += " and LSZH='" + UserInfo.GH + "'";

            EDRS.BLL.YX_DZJZ_WJSQDY bll = new EDRS.BLL.YX_DZJZ_WJSQDY(this.Request);

            DataSet ds = bll.GetListByPageEx(where, "SQSJ desc,SQDZT desc", (pageSize * pageNumber) - pageSize + 1, (pageSize * pageNumber), values);
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