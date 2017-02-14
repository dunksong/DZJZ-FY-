using System;
using System.Collections.Generic;
using Cyvation.CCQE.Common;
using Cyvation.CCQE.Model;
using Cyvation.CCQE.BLL;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections;
using LitJson;
using System.Drawing;
using EDRS.Common;
using System.Web.Script.Serialization;
using System.Web;
using Cyvation.CCQE.Web;
using EDRS.BLL;

namespace WebUI.Handler.ZZJG
{
    /// <summary>
    /// DZJZ_LSYJ 的摘要说明
    /// </summary>
    public class DZJZ_LSYJ : AshxBase
    {

        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);
            if (UserInfo == null)
                return;
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.ContentType = "application/json";
            string action = context.Request.Params["action"];
            switch (action.Replace(",", ""))
            {
                case "getMlList":
                    getMlList(context);
                    break;
                case "ListBind":
                    ListBind(context);
                    break;
                case "ExistLSZH":
                    ExitsLSZH(context);
                    break;
                case "GetModelPList":
                    GetModelPList(context);
                    break;
                case "AddData":
                    AddData(context);
                    break;
                case "UpData":
                    UpData(context);
                    break;
                case "DelData":
                    DelData(context);
                    break;
                case "DelImg":
                    DelImg(context);
                    break;
                case "GetImgList":
                    GetImgList(context);
                    break;
            }
        }

        private void DelImg(HttpContext context)
        {
            string lszh = context.Request["lszh"];
            string imgName = context.Request["imgName"];
            List<string> delList = new List<string>();
            delList.Add(imgName);
            EDRS.BLL.YX_DZJZ_LSZZWJ BLL = new YX_DZJZ_LSZZWJ(context.Request);
            if (BLL.DelList(lszh,delList))
            {
                context.Response.Write(ReturnString.JsonToString(Prompt.win, "删除成功", null));
            }
            else
            {
                context.Response.Write(ReturnString.JsonToString(Prompt.error, "删除失败", null));
            }
        }

        private void GetImgList(HttpContext context)
        {
            string lszh = context.Request.Form.Get("lszh");
            EDRS.BLL.YX_DZJZ_LSZZWJ BLL = new YX_DZJZ_LSZZWJ(context.Request);
            List<string> imgList = BLL.GetList(lszh);
            context.Response.Write(new JavaScriptSerializer().Serialize(imgList));
        }

        private void DelData(HttpContext context)
        {
            string lszh = context.Request["lszh"];
            EDRS.BLL.YX_DZJZ_LSGL BLL = new YX_DZJZ_LSGL(context.Request);
            if (BLL.Delete(lszh))
            {
                context.Response.Write(ReturnString.JsonToString(Prompt.win, "删除成功", null));
            }
            else
            {
                context.Response.Write(ReturnString.JsonToString(Prompt.error, "删除失败", null));
            }
        }


        public void AddData(HttpContext context)
        {
            EDRS.Model.YX_DZJZ_LSGL model = new EDRS.Model.YX_DZJZ_LSGL();
            model.LSZH = context.Request["LSZH"];
            model.LSXM = context.Request["LSXM"];
            model.LSDW = context.Request["LSDW"];
            model.LSDWDZ = context.Request["LSDWDZ"];
            model.LSDWYZHM = context.Request["LSDWYZHM"];
            model.LSLXDH = context.Request["LSLXDH"];
            model.LSSJ = context.Request["LSSJ"];
            model.DELXR = context.Request["DELXR"];
            model.DELXRDH = context.Request["DELXRDH"];
            model.LSZGYXSJ = Convert.ToDateTime(context.Request["LSZGYXSJ"]);
            model.SFDXZG = "N";//this.SFDXZG.Checked ? "Y" : "N";
            model.LSXXBZ = context.Request["LSXXBZ"];
            model.LSZZWJ1 = "";
            model.LSZZWJ2 = "";
            model.LSZZWJ3 = "";
            model.LSZZWJ4 = "";
            string[] LSZZWJ = context.Request["LSZZWJ"].Split(',');
            model.CJSJ = DateTime.Now;
            EDRS.BLL.YX_DZJZ_LSGL bll = new YX_DZJZ_LSGL(context.Request);
            if (bll.Add(model))
            {
                //成功后再保存资质文件关联
                List<string> fileList = new List<string>();
                foreach (string file in LSZZWJ)
                {
                    if (file != "")
                    {
                        fileList.Add(file);
                    }
                }
                EDRS.BLL.YX_DZJZ_LSZZWJ WJBLL = new YX_DZJZ_LSZZWJ(context.Request);
                if (fileList.Count == 0 || WJBLL.AddList(model.LSZH, fileList))
                {
                    context.Response.Write(ReturnString.JsonToString(Prompt.win, "保存成功", null));
                    //true
                }
                else
                {
                    context.Response.Write(ReturnString.JsonToString(Prompt.error, "保存失败", null));
                    //false
                }
            }
            else
            {
                context.Response.Write(ReturnString.JsonToString(Prompt.error, "保存失败", null));
                //false
            }
        }
        private void UpData(HttpContext context)
        {
            EDRS.Model.YX_DZJZ_LSGL model = new EDRS.Model.YX_DZJZ_LSGL();
            model.LSZH = context.Request["LSZH"];
            model.LSXM = context.Request["LSXM"];
            model.LSDW = context.Request["LSDW"];
            model.LSDWDZ = context.Request["LSDWDZ"];
            model.LSDWYZHM = context.Request["LSDWYZHM"];
            model.LSLXDH = context.Request["LSLXDH"];
            model.LSSJ = context.Request["LSSJ"];
            model.DELXR = context.Request["DELXR"];
            model.DELXRDH = context.Request["DELXRDH"];
            model.LSZGYXSJ = Convert.ToDateTime(context.Request["LSZGYXSJ"]);
            model.SFDXZG = "N";// this.SFDXZG.Checked ? "Y" : "N";
            model.LSXXBZ = context.Request["LSXXBZ"];
            string[] LSZZWJ = context.Request["LSZZWJ"].Split(',');
            model.CJSJ = DateTime.Now;
            EDRS.BLL.YX_DZJZ_LSGL bll = new YX_DZJZ_LSGL(context.Request);
            if (bll.Update(model))
            {
                //成功后再保存资质文件关联
                List<string> fileList = new List<string>();
                foreach (string file in LSZZWJ)
                {
                    if (file == "")
                    {
                        continue;
                    }
                    fileList.Add(file);
                }
                EDRS.BLL.YX_DZJZ_LSZZWJ WJBLL = new YX_DZJZ_LSZZWJ(context.Request);
                if (fileList.Count == 0 || WJBLL.AddList(model.LSZH, fileList))
                {
                    context.Response.Write(ReturnString.JsonToString(Prompt.win, "保存成功", null));
                    //true
                }
                else
                {
                    context.Response.Write(ReturnString.JsonToString(Prompt.error, "保存失败", null));
                    //false
                }
            }
            else
            {
                context.Response.Write(ReturnString.JsonToString(Prompt.error, "保存失败", null));
                //false
            }
        }
        private void ExitsLSZH(HttpContext context)
        {
            EDRS.BLL.YX_DZJZ_LSGL BLL = new YX_DZJZ_LSGL(context.Request);
            context.Response.Write(BLL.Exists(context.Request["LSZH"]));
        }

        private void GetModelPList(HttpContext context)
        {
            EDRS.BLL.YX_DZJZ_LSGL bll = new YX_DZJZ_LSGL(context.Request);
            EDRS.Model.YX_DZJZ_LSGL model = new EDRS.Model.YX_DZJZ_LSGL();
            string LSZH = context.Request["LSZH"];

            if (string.IsNullOrEmpty(LSZH))
                context.Response.Write(ReturnString.JsonToString(Prompt.error, "参数错误", null));
            model = bll.GetModel(LSZH);
            if (model != null)
            {
                context.Response.Write(EDRS.Common.JsonHelper.JsonString(model));
            }
            else
            {
                context.Response.Write(ReturnString.JsonToString(Prompt.error, "获取数据失败", null));
            }

        }
        private void ListBind(HttpContext context)
        {
            string dwbm = context.Request["dkey"];
            if (string.IsNullOrEmpty(dwbm))
            {
                dwbm = UserInfo.DWBM;
            }
            string where = "";
            List<object> param = new List<object>();
            string lszh = context.Request.Form["txt_id"];
            string lsxm = context.Request.Form["txt_name"];
            string txt_time_begin = context.Request.Form["txt_time_begin"];
            string txt_time_end = context.Request.Form["txt_time_end"];
            if (!string.IsNullOrEmpty(dwbm))
            {
                where += " AND LSDW = :LSDW";
                param.Add(dwbm);
            }
            if (!string.IsNullOrEmpty(lszh))
            {
                where += " AND LSZH LIKE :LSZH";
                param.Add("%" + lszh + "%");
            }
            if (!string.IsNullOrEmpty(lsxm))
            {
                where += " AND LSXM LIKE :LSXM";
                param.Add("%" + lsxm + "%");
            }

            if (!string.IsNullOrEmpty(txt_time_begin))
            {
                where += " AND CJSJ >= :CJSJ1";
                param.Add(Convert.ToDateTime(txt_time_begin));
            }
            if (!string.IsNullOrEmpty(txt_time_end))
            {
                where += " AND CJSJ <= :CJSJ2";
                param.Add(Convert.ToDateTime(txt_time_end).AddDays(1));
            }

            EDRS.BLL.YX_DZJZ_LSGL BLL = new YX_DZJZ_LSGL(context.Request);

            DataSet ds = BLL.GetList(where, param.ToArray());
            string json = "{\"Rows\":" + ds.Tables[0].ToDatagridJson() + "}";
            context.Response.Write(json);
        }

        private void getMlList(HttpContext context)
        {
            string errmsg = string.Empty;
            YX_DZJZ_JZML bll = new YX_DZJZ_JZML(context.Request);
            string where = " AND BMSAH = :BMSAH";
            List<object> list = new List<object>();
            list.Add("济南市院不捕核受[2015]37010000001号");
            DataSet ds = bll.GetList(where, list.ToArray());
            if (ds != null && string.IsNullOrEmpty(errmsg))
            {
                DataTable dt = ds.Tables[0];
                string json = dt.ToDatagridJson();
                context.Response.Write(json);
                //OperateLog.AddLog(OperateLog.LogType.角色权限管理Web, "查询目录列表成功！" + errmsg, UserInfo, UserRole, context.Request);
            }
            else
            {
                //OperateLog.AddLog(OperateLog.LogType.角色权限管理Web, "查询目录列表失败：" + errmsg, UserInfo, UserRole, context.Request);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}