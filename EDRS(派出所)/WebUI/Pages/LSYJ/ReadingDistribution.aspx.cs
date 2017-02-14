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
    public partial class ReadingDistribution :BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            string type = Request["t"];

            if (!string.IsNullOrEmpty(type))
            {
                Response.ContentEncoding = Encoding.UTF8;
                Response.ContentType = "application/json";

                if (type.Equals("GetMlTree"))
                    Response.Write(GetMlTree());
                if (type.Equals("AddData"))
                    Response.Write(AddData());
                if (type.Equals("ListBind"))
                {
                    //权限状态
                    EDRS.BLL.TYYW_GG_AJJBXX bll = new EDRS.BLL.TYYW_GG_AJJBXX(Request);
                    string data = bll.ListBin(this.Request, UserInfo.DWBM, UserInfo.GH, base.GetBmBm(), Jsbms, base.GetJsQxzt());
                    //OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "案件数据查询", UserInfo, UserRole, this.Request);
                    Response.Write(data);                   
                }
                if (type.Equals("getPeople"))
                    Response.Write(GetPeople());
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
            return bll.GetMlTree(Request, true, true);
        } 
        #endregion

        #region 保存分配案件文件
        /// <summary>
        /// 保存分配案件文件
        /// </summary>
        /// <returns></returns>
        private string AddData()
        {
            string json = Request.Form["json"];
            string yjxh = Guid.NewGuid().ToString().Replace("-","").ToUpper();  //"山东省院捕受[2015]37000000054号";
            string rybm = Request.Form["lszh_hidd"];
            string[] rybms = rybm.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries);
            if(rybms.Length != 2)
                return ReturnString.JsonToString(Prompt.error, "请选择阅卷人！", null);
            if(Request.Form["txt_yjkssj"] == null || string.IsNullOrEmpty(Request.Form["txt_yjkssj"]))
                return ReturnString.JsonToString(Prompt.error, "请设置阅卷开始时间！", null);
            if (Request.Form["txt_yjjssj"] == null || string.IsNullOrEmpty(Request.Form["txt_yjjssj"]))
                return ReturnString.JsonToString(Prompt.error, "请设置阅卷结束时间！", null);
            if(Convert.ToDateTime(Request.Form["txt_yjkssj"]) > Convert.ToDateTime(Request.Form["txt_yjjssj"]))
                return ReturnString.JsonToString(Prompt.error, "结束时间不能小于开始时间！", null);

            EDRS.BLL.TYYW_GG_AJJBXX ajbll = new EDRS.BLL.TYYW_GG_AJJBXX(Request);
            EDRS.Model.TYYW_GG_AJJBXX ajmodel = ajbll.GetModel(Request.Form["key_hidd"]);
            if(ajmodel==null)
                return ReturnString.JsonToString(Prompt.error, "分配失败，"+((VersionName)0).ToString()+"不存在！", null);
            //json = json.Replace("id", "_wjxh").Replace("MLLX", "_fpbm").Replace("text", "_sfsc").Replace("PARENTID", "_yjxh");
            Random random = new Random();
            EDRS.Model.YX_DZJZ_LSAJBD model = new EDRS.Model.YX_DZJZ_LSAJBD();
            model.GH = rybms[0];
            model.BMSAH = ajmodel.BMSAH;
            model.YJXH = yjxh;
            model.MC = Request.Form["txt_lsxm"];
            model.AJMC = ajmodel.AJMC;
            model.AJLBBM = ajmodel.AJLB_BM;
            model.AJLBMC = ajmodel.AJLB_MC;
            model.YJKSSJ = Convert.ToDateTime(Request.Form["txt_yjkssj"]);
            model.YJJSSJ = Convert.ToDateTime(Request.Form["txt_yjjssj"]);
            model.YJZH = random.Next(100000, 999999).ToString();
            model.YJMM = random.Next(100000, 999999).ToString();
            model.JDSJ = DateTime.Now;
            model.JDR = UserInfo.MC;
            model.JDRGH = UserInfo.GH;
            model.JDBMBM = "";
            model.JDBMMC = "";
            model.JDDWBM = UserInfo.DWBM;
            model.JDDWMC = UserInfo.DWMC;
            model.SFSC = "N";
            model.YJSQDH = "";//Request.Form["yjsqdh"];
            model.JZZTXS = "";
            model.DWBM = rybms[1];
            EDRS.BLL.YX_DZJZ_LSAJBD bllBD = new EDRS.BLL.YX_DZJZ_LSAJBD(Request);
            List<EDRS.Model.YX_DZJZ_LSAJBD> listModel = bllBD.GetModelList(" and YJXH=:YJXH", new object[] { Request.Form["yjxh"] });
            if (listModel == null || listModel.Count == 0)
            {
                if (!bllBD.Add(model))
                    return ReturnString.JsonToString(Prompt.error, "分配失败！", null);
            }
            else
            {
                yjxh = listModel[0].YJXH;
                listModel[0].BMSAH = ajmodel.BMSAH;
                listModel[0].GH = rybms[0];
                listModel[0].DWBM = rybms[1];
                listModel[0].MC = Request.Form["txt_lsxm"];
                listModel[0].AJMC = ajmodel.AJMC;
                listModel[0].AJLBBM = ajmodel.AJLB_BM;
                listModel[0].AJLBMC = ajmodel.AJLB_MC;
                listModel[0].YJKSSJ = Convert.ToDateTime(Request.Form["txt_yjkssj"]);
                listModel[0].YJJSSJ = Convert.ToDateTime(Request.Form["txt_yjjssj"]);
                if(!bllBD.Update(listModel[0]))
                    return ReturnString.JsonToString(Prompt.error, "修改分配失败！", null);
            }

            List<EDRS.Model.YX_DZJZ_LSAJWJFP> modelList = JsonHelper.JsonToList<EDRS.Model.YX_DZJZ_LSAJWJFP>(json);

            if (modelList != null && modelList.Count > 0)
            {
                EDRS.BLL.YX_DZJZ_LSAJWJFP bll = new EDRS.BLL.YX_DZJZ_LSAJWJFP(Request);
                if (bll.AddList(modelList, model, yjxh))
                    return ReturnString.JsonToString(Prompt.win, "分配成功！", null);
            }
            return ReturnString.JsonToString(Prompt.error, "分配失败！", null);
        }
        #endregion


        #region 绑定人员
        /// <summary>
        /// 绑定人员
        /// </summary>
        /// <returns></returns>
        private string GetPeople()
        {
            string page = Request["page"];
            string rows = Request["pagesize"];
            string key = Request["condition"];
            int pageNumber = int.Parse(page);
            int pageSize = int.Parse(rows);
            string where = string.Empty;

            object[] values = new object[6];
            where += " and SFSC=:SFSC";
            values[0] = "N";
            where += " and DWBM=:DWBM";
            values[1] = UserInfo.DWBM;

            if (!string.IsNullOrEmpty(key))
            {
                string value = JsonHelper.DeserializeObjectKey(key, "value");
                if (!string.IsNullOrEmpty(value))
                {
                    where += " and( MC like :MC or GH like :GH or DLBM like :DLBM or GZZH like :GZZH)";
                    values[2] = "%" + value + "%";
                    values[3] = "%" + value + "%";
                    values[4] = "%" + value + "%";
                    values[5] = "%" + value + "%";
                }
            }
            EDRS.BLL.XT_ZZJG_RYBM bll = new EDRS.BLL.XT_ZZJG_RYBM(this.Request);
            DataSet ds = bll.GetListByPage(where, "", (pageSize * pageNumber) - pageSize + 1, (pageSize * pageNumber), values);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.功能权限管理Web, "阅卷分配获取人员信息成功", UserInfo, UserRole, this.Request);
                int count = bll.GetRecordCount(where, values);
                return "{\"Total\":" + count + ",\"Rows\":" + JsonHelper.JsonString(ds.Tables[0]) + "}";
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.功能权限管理Web, "阅卷分配未找到相关人员信息", UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "未找到任何人员信息", null);
        }
        #endregion
    }
}