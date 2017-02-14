using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;


namespace WebUI.Pages.LSYJ
{
    public partial class LSYJLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["method"] != null)
            {

                Response.ContentEncoding = Encoding.UTF8;
                Response.ContentType = "application/json";
                switch (Request.QueryString["method"])
                {
                    case "yjlslogin":
                        yjlslogin();
                        break;
                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// 律师阅卷登录
        /// </summary>
        public void yjlslogin()
        {
            //获取值
            string yjzh = Request.Form["yjzh"];
            string yjmm = Request.Form["yjmm"];
            EDRS.BLL.YX_DZJZ_LSAJBD bll = new EDRS.BLL.YX_DZJZ_LSAJBD(Request);
            DataSet ds = bll.GetModelByZH(yjzh,yjmm);
            if (ds != null&&ds.Tables.Count>0&&ds.Tables[0].Rows.Count>0)
            {
                DateTime nowtime=DateTime.Now;
                if (Convert.ToDateTime(ds.Tables[0].Rows[0]["YJKSSJ"]) > nowtime)
                {
                    Response.Write(2);
                    Response.End();
                }
                if (Convert.ToDateTime(ds.Tables[0].Rows[0]["YJJSSJ"]) < nowtime)
                {
                    Response.Write(1);
                    Response.End();
                }
                List<EDRS.Model.YX_DZJZ_LSAJBD> listmodel = bll.DataTableToList(ds.Tables[0]);
                Session["YjData"] = listmodel[0];
                //判断律师是否登录
                Session["lszh"] = ds.Tables[0].Rows[0]["GH"];
                Response.Write("{\"yjxh\":\"" + ds.Tables[0].Rows[0]["YJXH"] + "\",\"bmsah\":\"" + ds.Tables[0].Rows[0]["BMSAH"] + "\",\"ajmc\":\"" + ds.Tables[0].Rows[0]["AJMC"] + "\",\"ajbh\":\"" + ds.Tables[0].Rows[0]["AJBH"] + "\",\"lszh\":\"" + ds.Tables[0].Rows[0]["GH"] + "\",\"yjxh\":\"" + ds.Tables[0].Rows[0]["YJXH"] + "\"}");
                Response.End();
            }
            Response.Write(0);
            Response.End();
        }
    }
}