using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using EDRS.Common;
using iTextSharp.text.pdf;
using System.Data;
using System.Text;

namespace WebUI.Interface
{

    public partial class GetDossierPage : System.Web.UI.Page
    {

        //?DWBM=&BH=&JZBH=&JZWJYBH=
        //页面加载类
        protected void Page_Load(object sender, EventArgs e)
        {
            string p = Request.Form["otherParam"];
            string type = Request.Form["type"];

            if (!string.IsNullOrEmpty(p))
            {
                Response.ContentEncoding = Encoding.UTF8;
                Response.ContentType = "application/json";

                if (p == "zTreeAsyncTest")
                {
                    if (type == "j")
                        Response.Write(GetDossierFileInfo());
                    else if (type == "w")
                        Response.Write(GetDossierFilePageInfo());
                    else if (type == "a")
                        Response.Write(GetDossierInfo());
                    else if (type == "pa")
                        Response.End();
                    else
                        Response.Write(GetDossierDoInfo());
                    Response.End();
                }
                //else if (p == "search_info")
                //{
                //    Response.Write(GetDossierDoInfo());
                //    Response.End();
                //}
                else if (p == "search_detailed")
                {
                    Response.Write(GetDossierFilePage());
                    Response.End();
                }
                else if (p == "setsh")
                {
                    Response.Write(SetSh());
                    Response.End();
                }
            }
            LogHelper.LogError(Request, "接口调用", "WebUI.Interface.GetDossierPage.Page_Load","InterFace");
        }



        #region 制作信息获取接口
        /// <summary>
        /// 制作信息获取接口
        /// </summary>
        /// <param name="DWBM">单位编码</param>
        /// <param name="BH">部门受案号</param>
        public string GetDossierDoInfo()
        {
          //  string dwbm = Request.Form["dwbm"];
            string bh = Request.Form["bh"];
            string wsbh = Request.Form["wsbh"];

            //if (string.IsNullOrEmpty(wsbh) || string.IsNullOrEmpty(bh))
            //    return ReturnString.JsonToString(Prompt.error, "查询参数错误", null);

           // dwbm = HttpUtility.UrlDecode(HttpUtility.UrlDecode(dwbm));
            bh = HttpUtility.UrlDecode(HttpUtility.UrlDecode(bh));
            wsbh = HttpUtility.UrlDecode(HttpUtility.UrlDecode(wsbh));

            EDRS.BLL.YX_DZJZ_JZJBXX bll = new EDRS.BLL.YX_DZJZ_JZJBXX(HttpContext.Current.Request);

          //  object[] param = new object[3];
            List<object> param = new List<object>();

            string where = "";//" and dwbm = :dwbm";
            // param[0] = dwbm;
            if (!string.IsNullOrEmpty(bh) && bh != "null")
            {
                where += " and ajbh=:ajbh";
                param.Add(bh);
            }
            if (!string.IsNullOrEmpty(wsbh) && wsbh != "null")
            {
                where += " and wsbh=:wsbh";
                param.Add(wsbh);
            }
            where += " and SFSC = 'N' and ZZZT >= 2 and WSBH is not null"; // and ZZZT=2
            
            List<EDRS.Model.YX_DZJZ_JZJBXX> list = bll.GetModelList(where, param.ToArray());
            if (list == null || list.Count == 0)
            {
                return ReturnString.JsonToString(Prompt.error, "未能查询到案件编号为【" + bh + "】的卷宗数据！", null);
            }
            //判断卷宗状态
            //if (Convert.ToInt32(list[0].ZZZT) != 2)
            //{
            //    return ReturnString.JsonToString(Prompt.error, "未能查询到案件编号为【" + bh + "】的卷宗数据！", null);
            //}
            string bmsah = "";
            for (int i = 0; i < list.Count; i++)
            {
                bmsah += "'" + list[i].BMSAH + "'";
                if (i < list.Count - 1)
                    bmsah += ",";
            }
            //bh = list[0].BMSAH;
            //返回参数           
          //  DataSet ds = bll.GetJzjbxxByBmsah(bh, dwbm);
            DataSet ds = bll.GetJzjbxxByBmsahList(bmsah, "");
            //判断是否查询出现异常
            if (ds != null && ds.Tables.Count > 0)
            {
                //判断查询没有数据
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dtData = ds.Tables[0].Copy();                   

                    dtData.Columns["JZBH"].ColumnName = "id";
                    dtData.Columns["WSBH"].ColumnName = "name";
                    dtData.Columns.Add("pId");
                    dtData.Columns.Add("title");
                    dtData.Columns.Add("isParent");
                    dtData.Columns.Add("type");
                    dtData.Columns.Add("open");
                    dtData.Columns.Add("icon");
                    dtData.Columns.Add("zzzt");

                    //DataColumn dc = new DataColumn();
                    //dc.ColumnName = "checked";
                    //dc.DefaultValue = true;
                    //dtData.Columns.Add(dc);

                   // dtData.Columns.Add("nocheck");

                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(dtData.Rows[i]["JZSL"]) > 0)
                            dtData.Rows[i]["isParent"] = true;
                        else
                            dtData.Rows[i]["isParent"] = false;

                        dtData.Rows[i]["pId"] = "printjzbh";
                        dtData.Rows[i]["title"] = dtData.Rows[i]["name"];
                        dtData.Rows[i]["type"] = "a";
                        dtData.Rows[i]["open"] = false;
                        dtData.Rows[i]["icon"] = "img/Product_16x16.png";
                        dtData.Rows[i]["zzzt"] = (list[0].ZZZT == "2" ? true : false);
                      
                       // dtData.Rows[i]["nocheck"] = "true";//nocheck:true
                    }

                    DataRow dr = dtData.NewRow();
                    dr["id"] = "printjzbh";
                    dr["name"] = dtData.Rows[0]["JZMC"];
                    dr["pId"] =-1;
                    dr["title"] = "";
                    dr["isParent"] = false;
                    dr["type"] = "pa";
                    dr["open"] = true;
                    dr["icon"] = "img/PackageProduct_16x16.png";
                    
                    dtData.Rows.Add(dr);
                
                    return JsonHelper.JsonString(dtData);
                    //成功
                    //  return EDRS.Common.JsonHelper.JsonString(dt);
                }
                else
                {
                    return ReturnString.JsonToString(Prompt.error, "该案件没有制作信息", null);
                }
            }
            else
            {
                return ReturnString.JsonToString(Prompt.error, "查询失败", null);
            }
        }
        #endregion

        #region 卷宗文件获取接口
        /// <summary>
        /// 卷宗文件获取接口
        /// </summary>
        /// <param name="DWBM"></param>
        /// <param name="BH"></param>
        /// <param name="JZBH"></param>      
        public string GetDossierFileInfo()
        {
           // string dwbm = Request.Form["dwbm"];
            string bh = Request.Form["id"];
            string jzbh = Request.Form["jzbh"];

            //if (string.IsNullOrEmpty(dwbm) || string.IsNullOrEmpty(bh) || string.IsNullOrEmpty(jzbh))
            //    return ReturnString.JsonToString(Prompt.error, "查询参数错误", null);
            if (string.IsNullOrEmpty(bh) || string.IsNullOrEmpty(jzbh))
                return ReturnString.JsonToString(Prompt.error, "查询参数错误", null);
      //      dwbm = HttpUtility.UrlDecode(HttpUtility.UrlDecode(dwbm));
            bh = HttpUtility.UrlDecode(HttpUtility.UrlDecode(bh));
            jzbh = HttpUtility.UrlDecode(HttpUtility.UrlDecode(jzbh));

            //返回参数
            EDRS.BLL.YX_DZJZ_JZML bll = new EDRS.BLL.YX_DZJZ_JZML(HttpContext.Current.Request);
            DataSet ds = bll.GetDossierFileInfo("", bh, jzbh);

            //判断是否查询出现异常
            if (ds != null && ds.Tables.Count > 0)
            {
                //判断查询没有数据
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dtData = ds.Tables[0].Copy();
                    dtData.Columns["JZWJBH"].ColumnName = "id";
                    dtData.Columns["JZWJMC"].ColumnName = "name";
                    dtData.Columns.Add("pId");
                    dtData.Columns.Add("title");
                    dtData.Columns.Add("isParent");
                    dtData.Columns.Add("type");
                    dtData.Columns.Add("icon");
                    DataColumn dc = new DataColumn();
                    dc.ColumnName = "nocheck";
                    dc.DefaultValue = true;
                    dtData.Columns.Add(dc);

                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(dtData.Rows[i]["JZWJYSL"]) > 0)
                            dtData.Rows[i]["isParent"] = true;
                        else
                            dtData.Rows[i]["isParent"] = false;

                        dtData.Rows[i]["pId"] = -1;
                        dtData.Rows[i]["title"] = dtData.Rows[i]["name"];
                        dtData.Rows[i]["type"] = "w";
                        dtData.Rows[i]["icon"] = "img/GroupHeader_16x16.png";
                    }
                    return JsonHelper.JsonString(dtData);
                }
                else
                {
                    return GetDossierFilePageInfo();
                    //该案件没有卷
                   // return ReturnString.JsonToString(Prompt.error, "该卷宗没有文件信息", null);
                }
            }
            else
            {
                //查询失败
                return ReturnString.JsonToString(Prompt.error, "查询失败", null);
            }

        }
        #endregion

        #region 卷宗信息获取
        /// <summary>
        /// 卷宗信息获取
        /// </summary>
        /// <param name="bmsah"></param>       
        public string GetDossierInfo()
        {
           // string dwbm = Request.Form["dwbm"];
            string bh = Request.Form["bh"];
            string wsbh=Request.Form["wsbh"];
            //if (string.IsNullOrEmpty(dwbm) || string.IsNullOrEmpty(bh))
            //    return ReturnString.JsonToString(Prompt.error, "查询参数错误", null);
            if (string.IsNullOrEmpty(bh))
                return ReturnString.JsonToString(Prompt.error, "查询参数错误", null);

           // dwbm = HttpUtility.UrlDecode(HttpUtility.UrlDecode(dwbm));
            bh = HttpUtility.UrlDecode(HttpUtility.UrlDecode(bh));

            EDRS.BLL.YX_DZJZ_JZJBXX bllJzjbxx = new EDRS.BLL.YX_DZJZ_JZJBXX(HttpContext.Current.Request);
            //string where = " and ajbh=:ajbh and dwbm = :dwbm";
            string where = "";// " and ajbh=:ajbh";

            List<object> param = new List<object>();

            if (!string.IsNullOrEmpty(bh) && bh != "null")
            {
                where += " and ajbh=:ajbh";
                param.Add(bh);
            }
            if (!string.IsNullOrEmpty(wsbh) && wsbh != "null")
            {
                where += " and wsbh=:wsbh";
                param.Add(wsbh);
            }
          
            List<EDRS.Model.YX_DZJZ_JZJBXX> list = bllJzjbxx.GetModelList(where, param.ToArray());
            if (list == null || list.Count == 0)
            {
                return ReturnString.JsonToString(Prompt.error, "未能查询到案件编号为【" + bh + "】的卷宗数据！", null);
            }
            bh = list[0].BMSAH;
            //返回参数           
            EDRS.BLL.YX_DZJZ_JZML bll = new EDRS.BLL.YX_DZJZ_JZML(HttpContext.Current.Request);
            DataSet ds = bll.GetDossierInfo(bh, "");
            //判断是否查询出现异常
            if (ds != null && ds.Tables.Count > 0)
            {
                //判断查询没有数据
                if (ds.Tables[0].Rows.Count > 0)
                {

                    DataTable dtData = ds.Tables[0].Copy();
                    dtData.Columns["BH"].ColumnName = "id";
                    dtData.Columns["JZLBMC"].ColumnName = "name";
                    dtData.Columns.Add("pId");
                    dtData.Columns.Add("title");
                    dtData.Columns.Add("isParent");
                    dtData.Columns.Add("type");
                    dtData.Columns.Add("icon");
                    DataColumn dc = new DataColumn();
                    dc.ColumnName = "nocheck";
                    dc.DefaultValue = true;
                    dtData.Columns.Add(dc);
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {

                        if (Convert.ToInt32(dtData.Rows[i]["JZWJSL"]) > 0)
                            dtData.Rows[i]["isParent"] = true;
                        else
                            dtData.Rows[i]["isParent"] = false;

                        dtData.Rows[i]["pId"] = -1;
                        dtData.Rows[i]["title"] = dtData.Rows[i]["name"];
                        dtData.Rows[i]["type"] = "j";
                        dtData.Rows[i]["icon"] = "img/Content_16x16.png";

                    }

                    return JsonHelper.JsonString(dtData);

                    //成功
                    //  return EDRS.Common.JsonHelper.JsonString(ds.Tables[0]);
                }
                else
                {
                    //该案件没有卷
                    return ReturnString.JsonToString(Prompt.error, "该卷宗信息下没有文件", null);
                }
            }
            else
            {
                //查询失败
                return ReturnString.JsonToString(Prompt.error, "查询失败", null);
            }
        }
        #endregion

        #region 卷宗页数据获取接口
        /// <summary>
        /// 卷宗页数据获取接口
        /// </summary>
        /// <param name="DWBM"></param>
        /// <param name="BH"></param>
        /// <param name="JZBH"></param>
        /// <param name="JZWJYBH"></param>
        public string GetDossierFilePage()
        {
            string dwbm = Request.Form["dwbm"];
            string bh = Request.Form["bh"];
            string jzbh = Request.Form["jzbh"];
            string jzwjybh = Request.Form["jzwjybh"];

            if (string.IsNullOrEmpty(dwbm) || string.IsNullOrEmpty(bh) || string.IsNullOrEmpty(jzbh) || string.IsNullOrEmpty(jzwjybh))
                return ReturnString.JsonToString(Prompt.error, "查询参数错误", null);

            dwbm = HttpUtility.UrlDecode(HttpUtility.UrlDecode(dwbm));
            bh = HttpUtility.UrlDecode(HttpUtility.UrlDecode(bh));
            jzbh = HttpUtility.UrlDecode(HttpUtility.UrlDecode(jzbh));
            jzwjybh = HttpUtility.UrlDecode(HttpUtility.UrlDecode(jzwjybh));

            //返回参数
            string Str = string.Empty;
            EDRS.BLL.YX_DZJZ_JZMLWJ bll = new EDRS.BLL.YX_DZJZ_JZMLWJ(HttpContext.Current.Request);
            DataSet ds = bll.GetList(" and DWBM=:DWBM and BMSAH=:BMSAH and JZBH=:JZBH and WJXH=:WJXH", new object[] { dwbm, bh, jzbh, jzwjybh });
            //判断是否查询出现异常
            if (ds != null && ds.Tables.Count > 0)
            {
                //判断查询没有数据
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //成功
                    return EDRS.Common.JsonHelper.JsonString(ds.Tables[0]);

                }
                else
                {
                    //该案件没有卷
                    return ReturnString.JsonToString(Prompt.error, "该卷宗页数据不存在", null);
                }
            }
            else
            {
                //查询失败
                return ReturnString.JsonToString(Prompt.error, "查询失败", null);
            }
        }
        #endregion

        #region 卷宗页获取
        /// <summary>
        /// 卷宗页获取
        /// </summary>
        /// <param name="DWBM">单位编号</param>
        /// <param name="BH">部门受案号</param>
        /// <param name="JZBH">卷宗编号</param>
        /// <param name="JZWJBH">卷宗文件编号</param>
        public string GetDossierFilePageInfo()
        {
           // string dwbm = Request.Form["dwbm"];
            string bh = Request.Form["bh"];
            string wsbh = Request.Form["wsbh"];
            string jzbh = Request.Form["jzbh"];
            string jzwjbh = Request.Form["id"];

            if (string.IsNullOrEmpty(bh) || string.IsNullOrEmpty(jzbh) || string.IsNullOrEmpty(jzwjbh) || string.IsNullOrEmpty(wsbh))
                return ReturnString.JsonToString(Prompt.error, "查询参数错误", null);

           // dwbm = HttpUtility.UrlDecode(HttpUtility.UrlDecode(dwbm));
            bh = HttpUtility.UrlDecode(HttpUtility.UrlDecode(bh));
            jzbh = HttpUtility.UrlDecode(HttpUtility.UrlDecode(jzbh));
            jzwjbh = HttpUtility.UrlDecode(HttpUtility.UrlDecode(jzwjbh));
            wsbh = HttpUtility.UrlDecode(HttpUtility.UrlDecode(wsbh));

            EDRS.BLL.YX_DZJZ_JZJBXX bllJzjbxx = new EDRS.BLL.YX_DZJZ_JZJBXX(HttpContext.Current.Request);
            string where = "";//" and ajbh=:ajbh";          

            List<object> param = new List<object>();

            if (!string.IsNullOrEmpty(bh) && bh != "null")
            {
                where += " and ajbh=:ajbh";
                param.Add(bh);
            }
            if (!string.IsNullOrEmpty(wsbh) && wsbh != "null")
            {
                where += " and wsbh=:wsbh";
                param.Add(wsbh);
            }


            List<EDRS.Model.YX_DZJZ_JZJBXX> list = bllJzjbxx.GetModelList(where, param.ToArray());
            if (list == null || list.Count == 0)
            {
                return ReturnString.JsonToString(Prompt.error, "未能查询到案件编号为【" + bh + "】的卷宗数据！", null);
            }
            bh = list[0].BMSAH;
            //返回参数
            string Str = string.Empty;
            EDRS.BLL.YX_DZJZ_JZML bll = new EDRS.BLL.YX_DZJZ_JZML(HttpContext.Current.Request);
            DataSet ds = bll.GetDossierFilePageInfo("", bh, jzbh, jzwjbh);
            //判断是否查询出现异常
            if (ds != null && ds.Tables.Count > 0)
            {
                //判断查询没有数据
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //成功
                    DataTable dtData = ds.Tables[0].Copy();
                    dtData.Columns["JZWJYBH"].ColumnName = "id";
                    dtData.Columns["JZWJYMC"].ColumnName = "name";
                    dtData.Columns.Add("pId");
                    dtData.Columns.Add("title");
                    dtData.Columns.Add("isParent");
                    dtData.Columns.Add("type");
                    dtData.Columns.Add("icon");
                    DataColumn dc = new DataColumn();
                    dc.ColumnName = "nocheck";
                    dc.DefaultValue = true;
                    dtData.Columns.Add(dc);
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        dtData.Rows[i]["isParent"] = false;
                        dtData.Rows[i]["pId"] = -1;
                        dtData.Rows[i]["title"] = dtData.Rows[i]["name"];
                        dtData.Rows[i]["type"] = "y";
                        dtData.Rows[i]["icon"] = "img/TextBox_16x16.png";

                    }

                    return JsonHelper.JsonString(dtData);
                }
                else
                {
                    //该案件没有卷
                    return ReturnString.JsonToString(Prompt.error, "该文件信息下没有文件页", null);
                }
            }
            else
            {
                //查询失败
                return ReturnString.JsonToString(Prompt.error, "查询失败", null);
            }
        }

        #endregion

        #region 审核
        /// <summary>
        /// 审核
        /// </summary>
        /// <returns></returns>
        public string SetSh()
        {
            string ids = Request.Form["bmsah"];
            string[] id = ids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            ids = "";
            for (int i = 0; i < id.Length; i++)
            {
                ids += "'" + id[i].Trim() + "'";
                if (i < id.Length - 1)
                    ids += ",";
            }
            if (string.IsNullOrEmpty(ids))
            {
                return ReturnString.JsonToString(Prompt.error, "未找到需要修改信息", null);
            }

            EDRS.BLL.YX_DZJZ_JZJBXX bll = new EDRS.BLL.YX_DZJZ_JZJBXX(this.Request);
            DataSet ds = bll.GetList(string.Format(" and JZBH in ({0})",ids));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["ZZZT"].ToString() != "2")
                    {
                        return ReturnString.JsonToString(Prompt.error, "该案件目前不能再进行审核", null);
                    }
                }
            }

            if (bll.UpdateByZZZTList(ids, "", DateTime.Now, "", Request.Form.Get("txt_type"), Request.Form.Get("txt_pz")))
            {
                //数据日志
                // OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "操作成功", "",UserInfo, UserRole, this.Request);
                foreach (string key in ids.Split(','))
                {
                    string bmsah = key.Replace("'", "");
                    EDRS.Model.YX_DZJZ_JZJBXX model = bll.GetModel(bmsah);
                    if (model != null && (model.ZZZT == "4" || model.ZZZT == "3"))
                    {
                        try
                        {
                            WebReference.jzfk _interface = new WebReference.jzfk();
                            string status = model.ZZZT == "4" ? "1" : "0";
                            string result = _interface.updateStatus(model.AJBH, model.WSBH, status);
                            LogHelper.LogError(Request, "Exception", "通知审核状态记录：" + result + "|本地状态：" + model.ZZZT + "|AJBH:" + model.AJBH + "|WSBH:" + model.WSBH + "|" + status, "", "");
                            OperateLog.AddLog(OperateLog.LogType.卷宗OCR及打包状态, "通知状态改变成功：[案件编号=" + model.AJBH + "，文书编号=" + model.WSBH + "]", model.BMSAH + "|" + model.AJBH + "|" + model.WSBH, null, null, this.Request);
                        }
                        catch (Exception ex)
                        {
                            OperateLog.AddLog(OperateLog.LogType.卷宗OCR及打包状态, "状态回写失败：" + ex.Message, "", null, null, this.Request);
                        }
                    }
                }
                return ReturnString.JsonToString(Prompt.win, "操作成功", null);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "操作失败", "", null, null, this.Request);
            return ReturnString.JsonToString(Prompt.error, "操作失败", null);
        } 
        #endregion

    }
}