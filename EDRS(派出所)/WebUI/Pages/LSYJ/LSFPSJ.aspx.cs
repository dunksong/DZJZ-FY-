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
    public partial class LSFPSJ : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["lszh"] == null)
            {
                Response.Write("<script>window.alert('账号未登录，请进入登录页面！'); location.href = '/Pages/LSYJ/LSYJLogin.aspx';</script>");
                Response.End();
            }
           
            string type = Request["t"];

            if (!string.IsNullOrEmpty(type))
            {
                Response.ContentEncoding = Encoding.UTF8;
                Response.ContentType = "application/json";

                if (type.Equals("GetMlTree"))
                    Response.Write(GetMlTree());
                if (type.Equals("submitapplyall"))
                    Response.Write(submitapplyall());
                if (type.Equals("submitapply"))
                    Response.Write(submitapply());
                if (type.Equals("IsSh"))
                    IsSh();
                Response.End();
            }
        }
        #region 获取案件目录
        /// <summary>
        /// 获取案件目录
        /// </summary>
        /// <returns></returns>
        private string GetMlTree()
        {          
            EDRS.BLL.YX_DZJZ_JZML bll = new EDRS.BLL.YX_DZJZ_JZML(Request);
            return bll.GetMlTree(Request, false, false);
        } 
        #endregion

        #region 申请打印
        public string submitapplyall()
        {
            EDRS.BLL.YX_DZJZ_JZMLWJ bll = new EDRS.BLL.YX_DZJZ_JZMLWJ(Request);
            EDRS.BLL.YX_DZJZ_WJSQDY BLLSQ = new EDRS.BLL.YX_DZJZ_WJSQDY(Request);
            string idstr = Request.Form["idstr[]"];
            string yjxh = Request.Form["yjxh"];
            if (!string.IsNullOrEmpty(idstr))
            {
                #region 申请打印

                EDRS.BLL.YX_DZJZ_LSAJBD dbbll = new EDRS.BLL.YX_DZJZ_LSAJBD(Request);
                //获取部门部门受案号
                EDRS.Model.YX_DZJZ_LSAJBD dbmodel = dbbll.GetModel(yjxh);             
                //判断该申请打印是否存在
                EDRS.Model.YX_DZJZ_WJSQDY model = BLLSQ.GetModelByYJXH(yjxh);
                if (model != null)
                {                    
                    model.SQSJ = DateTime.Now;
                    model.DYR = "";
                    model.DYRGH = "";
                    model.DYSJ = null;
                }
                else
                {
                    model = new EDRS.Model.YX_DZJZ_WJSQDY();
                    model.BMSAH = dbmodel.BMSAH;
                    model.DYBMBM = "";  //打印部门编码
                    model.DYBMMC = "";
                    model.DYDWBM = "";
                    model.DYDWMC = "";
                    model.DYFS = null;  //打印份数
                    model.DYFY = null;  //打印费用
                    model.DYR = "";     //打印人
                    model.DYRGH = "";   //打印人工号
                    model.DYSJ = null;  //打印时间
                    model.LSZH = Request.Form["lszh"];  //律师证号
                    model.DYSQDH = DateTime.Now.ToString("yyyyMMddHHmmssfff") + model.LSZH;
                    model.JZWJBH = "";  //文件编号                  
                    model.SFSC = "N";
                    model.SQFS = 1;     //申请份数
                    model.SQSJ = DateTime.Now; //申请时间
                    model.YJXH = yjxh;  //阅卷序号
                    model.XH = Guid.NewGuid().ToString();
                }
                #endregion

                #region 申请打印记录
                List<EDRS.Model.YX_DZJZ_WJSQDYJL> sqjlList = new List<EDRS.Model.YX_DZJZ_WJSQDYJL>();

                string[] idarr = idstr.Split(',');

                //根据序号获取打印记录
                DataSet ds = BLLSQ.GetListDYJL(" and XH ='" + model.XH + "'");           

                EDRS.Model.YX_DZJZ_WJSQDYJL sqjlModel = null;
                for (int i = 0; i < idarr.Length; i++)
                {
                    DataRow[] dr = null;
                    if (ds != null)
                        dr = ds.Tables[0].Select("WJXH='" + idarr[i] + "'");
                    if (dr==null || dr.Length == 0)
                    {
                        sqjlModel = new EDRS.Model.YX_DZJZ_WJSQDYJL();
                        sqjlModel.WJXH = idarr[i];
                        sqjlModel.XH = model.XH;
                        sqjlModel.YJXH = model.YJXH;
                        sqjlModel.ADDTIME = DateTime.Now;
                        sqjlModel.SFSC = "N";
                        sqjlList.Add(sqjlModel);
                    }
                }
                #endregion

                if (BLLSQ.AddListJL(sqjlList, model, model.XH))
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

        #region  申请当前页
        private string submitapply()
        {
            EDRS.BLL.YX_DZJZ_JZMLWJ bll = new EDRS.BLL.YX_DZJZ_JZMLWJ(Request);
            EDRS.BLL.YX_DZJZ_WJSQDY BLLSQ = new EDRS.BLL.YX_DZJZ_WJSQDY(Request);
            string id = Request["id"];
            string yjxh = Request.Form["yjxh"];
            string sq = string.Empty;
            if (!string.IsNullOrEmpty(id))
            {
                sq = " and JZWJBH=:JZWJBH";
            }

            DataSet d = BLLSQ.GetList(sq, new[] { id });
            DataTable dt = d.Tables[0];

            //判断该案件是否已在申请中.
            if (dt != null && dt.Rows.Count == 0)
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
                    model.LSZH = Request.Form["lszh"]; ;  //律师证号
                    model.DYSQDH = DateTime.Now.ToString("yyyyMMddHHmmssfff") + model.LSZH;
                    model.JZWJBH = id;  //文件编号                  
                    model.SFSC = "N";
                    model.SQFS = 1;     //申请份数
                    model.SQSJ = DateTime.Now; //申请时间
                    model.YJXH = yjxh;  //阅卷序号

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
                return ReturnString.JsonToString(Prompt.win, "申请审核中,请耐心等待....", null);
            }
            return ReturnString.JsonToString(Prompt.error, "未获取文件信息", null);
        }
        #endregion

        #region 判断是否已审核
        public void IsSh()
        {
            EDRS.BLL.YX_DZJZ_WJSQDY BLLSQ = new EDRS.BLL.YX_DZJZ_WJSQDY(Request);
            DataSet dsSh = BLLSQ.GetListIsSH(Request.Form["yjxh"]);
            //判断打印申请是否已审核
           // DataRow[] drs = dsSh.Tables[0].Select("YJSQDH is null");
            DataRow[] drs = dsSh.Tables[0].Select("SQDZT = 'Y'");
            if (drs.Length>0)
            {
                Response.Write("y");
                Response.End();
            }
            else
            {
                Response.Write("n");
                Response.End();
            }

        }
        #endregion

    }
}