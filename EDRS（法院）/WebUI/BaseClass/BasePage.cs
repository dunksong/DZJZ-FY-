using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using EDRS.Common;
using System.Data;
using System.IO;
using System.Text;


public class BasePage : System.Web.UI.Page
{
    #region 属性

    /// <summary>
    /// 版本字段
    /// </summary>
    public string Version = string.Empty;
    public string VersionPage = string.Empty;
    public string Ver_Advanced_Alone = string.Empty;

    /// <summary>
    /// 获取一年前时间
    /// </summary>
    public string SetBeTime
    {
        get { return (DateTime.Now.Year - 1).ToString(); }
    }
    private EDRS.Model.XT_ZZJG_RYBM userInfo;
    /// <summary>
    /// 登录用户信息
    /// </summary>
    public EDRS.Model.XT_ZZJG_RYBM UserInfo
    {
        get { return userInfo; }
        set { userInfo = value; }
    }
    #endregion

    private EDRS.Model.XT_ZZJG_DWBM userDwbm;
    /// <summary>
    /// 用户登录单位编码
    /// </summary>
    public EDRS.Model.XT_ZZJG_DWBM UserDwbm
    {
        get { return userDwbm; }
        set { userDwbm = value; }
    }

    private List<EDRS.Model.XT_QX_JSBM> userRole;
    /// <summary>
    /// 用户角色
    /// </summary>
    public List<EDRS.Model.XT_QX_JSBM> UserRole
    {
        get { return userRole; }
        set { userRole = value; }
    }

    private string jsbms;
    /// <summary>
    /// 角色编码字符串
    /// </summary>
    public string Jsbms
    {
        get { return jsbms; }
        set { jsbms = value; }
    }
    private string bmbms;
    /// <summary>
    /// 部门编码字符串
    /// </summary>
    public string Bmbms
    {
        get { return bmbms; }
        set { bmbms = value; }
    }
    #region 方法

    /// <summary>
    /// 构造函数
    /// </summary>
    public BasePage()
    {
        //控制页面版本
#if PAGE
        VersionPage = "PAGE";
#endif
        //控制功能版本
#if PSB
        Version = "PSB";
#endif
        //高级版本独立
#if ADVANCED_ALONE
        Ver_Advanced_Alone = "ADVANCED_ALONE";
#endif


        //if (UserInfo == null)
        //    UserInfo = new EDRS.Model.XT_ZZJG_RYBM();
        //if (UserRole == null)
        //    UserRole = new List<EDRS.Model.XT_QX_JSBM>();
        this.Load += new EventHandler(Page_Load);

    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.Error += new System.EventHandler(BasePage_Error);
    }

    //错误处理
    protected void BasePage_Error(object sender, System.EventArgs e)
    {
        string errMsg;
        try
        {
            Exception currentError = Server.GetLastError();
            errMsg = currentError.Message.ToString();

            string path = Server.MapPath("/LogInfo/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileName = "Err" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            FileStream fs = null;
            if (File.Exists(path + fileName))
                fs = new FileStream(path + fileName, FileMode.Append);
            else
                fs = new FileStream(path + fileName, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            StringBuilder sb = new StringBuilder();
            sb.Append("***********************\r\r[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + "]\r\r***********************\r\n\r\n");
            sb.Append("错误地址：" + Request.Url.ToString() + "\r\n");
            sb.Append("错误信息：" + currentError.Message.ToString() + "\r\n");
            sb.Append("Stack Trace：" + currentError.ToString() + "\r\n\r\n\r\n");
            Server.ClearError();
            sw.WriteLine(sb);
            sw.Close();
            fs.Close();
            sw.Dispose();
            fs.Dispose();
            //Response.Write(ReturnString.JsonToString(Prompt.error, errMsg, null));
            //Response.End();
        }
        catch (Exception ex)
        {

        }
    }

    /// <summary>
    /// 验证用户是否登录
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
        {
            System.Web.Security.FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            Response.Clear();

            if (Request.Form.Count == 0)//window.alert('账号未登录，请进入登录页面！');
                Response.Write("<script defer>window.alert('账号未登录，请进入登录页面！');parent.parent.location='/Login.aspx';</script>");
            else
                Response.Write(ReturnString.JsonToString(Prompt.error, "登录超时，请刷新页面重新登录fn=parent.skip", null));
            Response.End();

        }
        else
        {
            if (this.UserInfo == null)
                this.userInfo = Session["user"] as EDRS.Model.XT_ZZJG_RYBM;
            if (this.UserDwbm == null)
                this.userDwbm = Session["userDwbm"] as EDRS.Model.XT_ZZJG_DWBM;
            if (this.UserRole == null)
                this.userRole = Session["userRole"] as List<EDRS.Model.XT_QX_JSBM>;

            if (this.UserInfo != null)
            {
                if (!GetRole(userInfo.DWBM, userInfo.GH))
                {
                    Response.Write("您没有权限访问该功能！");
                    Response.End();
                    return;
                }
            }

            if (this.UserRole != null)
            {
                string jsbms = "";
                string bmbms = "";
                for (int i = 0; i < UserRole.Count; i++)
                {  
                    jsbms += "'" + UserRole[i].JSBM + "'";
                    bmbms += "'" + UserRole[i].BMBM + "'";

                    if (i < UserRole.Count - 1)
                    {
                        jsbms += ",";
                        bmbms += ",";
                    }
                }
                this.jsbms = jsbms;
                this.bmbms = bmbms;
            }
            //TODO: 页面权限判断
        }
    }
    #endregion


    #region 权限判断

    /// <summary>
    /// 根据单位编号和工号获取角色功能
    /// </summary>
    /// <param name="dwbm"></param>
    /// <param name="gh"></param>
    /// <returns></returns>
    private bool GetRole(string dwbm, string gh)
    {
        string path = this.Request.Path.ToLower().Remove(0, 1);
        if (path == "pages/business/getservice.aspx")
        {
            path = "pages/business/caseinfomanage.aspx";
        }
        if (path == "pages/report/reportcenter.aspx")
        {
            path = this.Request.UrlReferrer.AbsolutePath.ToLower().Remove(0, 1);
        }

        if (path == "pages/gngl/gnmanager.aspx" || path == "pages/qxgl/rolemanager.aspx")
        {
            path = "View/Zzjg/ZzjgMain.htm".ToLower();
        }
        if (path == "pages/template/sortindexmanager.aspx")
        {
            path = "pages/template/templatedeploy.aspx";
        }
        if (path == "pages/lsyj/readingdistribution.aspx")
        {
            path = "pages/lsyj/YJSQ.aspx";
        }
        //if (path == "pages/lsyj/lsfpsj.aspx")
        //{
        //    path = "pages/lsyj/lsfp.aspx";
        //}
        if (path == "pages/lsyj/lsfpajsj.aspx" || (this.Request.UrlReferrer != null && this.Request.UrlReferrer.AbsolutePath.ToLower().Remove(0, 1) == "pages/lsyj/lsfpajsj.aspx"))
        {
            path = "pages/lsyj/lsfp.aspx";
        }
        if (path == "pages/lsyj/readingfile.aspx" && this.Request.UrlReferrer.AbsolutePath.ToLower().Remove(0, 1) == "pages/lsyj/readingdistribution.aspx")
        {
            path = "pages/lsyj/YJSQ.aspx";
        }
        if (path == "pages/lsyj/readingfileprint.aspx")
        {
            path = "pages/lsyj/ReadingApply.aspx";
        }

        if (path == "pages/lsyj/readingfile.aspx")
        {
            path = this.Request.UrlReferrer.AbsolutePath.ToLower().Remove(0, 1);
        }
        if (path == "download.aspx")
        {
            path = this.Request.UrlReferrer.AbsolutePath.ToLower().Remove(0, 1);
        }
        path = path.Replace("templatedeploy_bak", "templatedeploy");
        if (path.Equals("main.aspx"))
            return true;
        else if (path != "main.aspx")
        {
            DataSet gnds = GetListByBound(dwbm, gh);
            DataRow[] drs = gnds.Tables[0].Select("GNCT='" + path + "'");
            if (drs.Length > 0)
                return true;
        }
        return false;
    }
    #endregion

    #region 根据工号和单位编码获取功能列表
    /// <summary>
    /// 根据工号和单位编码获取功能列表
    /// </summary>
    /// <param name="dwbm"></param>
    /// <param name="gh"></param>
    /// <returns></returns>
    private DataSet GetListByBound(string dwbm, string gh)
    {
        EDRS.BLL.XT_QX_GNDY bll = new EDRS.BLL.XT_QX_GNDY(this.Request);
        return bll.GetListByBound(dwbm, gh, "", null);
    }
    #endregion

    #region 根据单位编码和工号获取角色功能分配列表
    /// <summary>
    /// 根据单位编码和工号获取角色功能分配列表
    /// </summary>
    /// <param name="dwbm"></param>
    /// <param name="gh"></param>
    /// <returns></returns>
    private DataSet GetJSGNFPByGh(string dwbm, string gh)
    {
        string where = " and DWBM=:DWBM and GH=:GH ";
        object[] objValues = new object[] { dwbm, gh };
        EDRS.BLL.XT_QX_JSGNFP bll = new EDRS.BLL.XT_QX_JSGNFP(this.Request);
        return bll.GetJSGNFPByGh(dwbm, gh, where, objValues);
    }
    #endregion


    #region 绑定菜单
    /// <summary>
    /// 绑定菜单
    /// </summary>
    public string GetBindingMenu()
    {
        string dwbm = this.userInfo.DWBM;
        string gh = this.userInfo.GH;
        if (string.IsNullOrEmpty(dwbm) || string.IsNullOrEmpty(gh))
            return ReturnString.JsonToString(Prompt.error, "参数错误", null);
        string where = "";
        //switch (this.UserDwbm.DWJB) { 
        //    case "1":
        //        where = " and sfgjysy='Y'";
        //        break;
        //    case "2":
        //        where = " and sfsysy='Y'";
        //        break;
        //    case "3":
        //        where = " and sfsjysy='Y'";
        //        break;
        //    case "4":
        //        where = " and sfqysy='Y'";
        //        break;
        //}

        EDRS.BLL.XT_QX_GNDY bll = new EDRS.BLL.XT_QX_GNDY(this.Request);
        DataSet ds = bll.GetListByBound(dwbm, gh, where, null);

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            bool isOpen = true;

            return new TreeJson(ds.Tables[0], "flbm", "flmc", "fflbm", "gnct", "", "", "", isOpen, true).ResultJson.ToString();
        }
        return ReturnString.JsonToString(Prompt.error, "该账号未设置任何功能", null);
    }
    #endregion

    #region 按钮权限列表
    /// <summary>
    /// 按钮权限列表
    /// </summary>
    public bool IsAnRole(string code)
    {
        EDRS.BLL.XT_QX_JSANQX bll = new EDRS.BLL.XT_QX_JSANQX(Request);
        DataSet ds = bll.GetAnQxListByUser(UserDwbm.DWBM, Bmbms, Jsbms, UserInfo.GH);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            // DataRow[] drs = ds.Tables[0].Select("GNCT='" + path + "'");
            DataRow[] drs = ds.Tables[0].Select("ANBH='" + code + "' and QXBM is not null ");
            if (drs.Length > 0)
            {
                return true;
                //StringBuilder str = new StringBuilder();
                //for (int i = 0; i < drs.Length; i++)
                //{
                //    //str.AppendFormat("$('#{0}').remove();", drs[i]["ANBH"]);
                //}
                //$(document).ready(function () {" + str.ToString() + " alert(1); });
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "sgefegaddffee", "<script>alert('请正确输入！');</script>", true);
                //Response.Write("<script> var my = document.getElementById('btn_pdfdc'); my.parentNode.removeChild(my);</script>");
            }
        }
        return false;
    }
    #endregion

}