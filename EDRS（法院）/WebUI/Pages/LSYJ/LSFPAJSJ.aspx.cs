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
    public partial class LSFPAJSJ : BasePage
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
                    EDRS.BLL.TYYW_GG_AJJBXX bll = new EDRS.BLL.TYYW_GG_AJJBXX(Request);
                    string data = bll.ListBin(this.Request, UserInfo.DWBM, UserInfo.GH, Bmbms, Jsbms);
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
            return bll.GetMlTree(Request,true,true);
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
            string yjxh = Guid.NewGuid().ToString().Replace("-", "").ToUpper();  //"山东省院捕受[2015]37000000054号";

            string lssq = Request.Form["lssq"];
            string[] lssqs = null;
            if (string.IsNullOrEmpty(lssq))
            {
                return ReturnString.JsonToString(Prompt.error, "请先填写阅卷登记信息！", null);
            }
            lssqs = lssq.Split(new string[] { "|&|" }, StringSplitOptions.None);
            if (lssqs == null || lssqs.Length != 8)
            {
                return ReturnString.JsonToString(Prompt.error, "填写的阅卷登记信息不正确！", null);
            }

            string rybm = "";// Request.Form["lszh_hidd"];
            // string[] rybms = rybm.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries);
            //if(rybms.Length != 2)
            //    return ReturnString.JsonToString(Prompt.error, "请选择阅卷人！", null);
            /* 2016-9-6 取消设置阅卷时间
            if (Request.Form["txt_yjkssj"] == null || string.IsNullOrEmpty(Request.Form["txt_yjkssj"]))
                return ReturnString.JsonToString(Prompt.error, "请设置阅卷开始时间！", null);
            if (Request.Form["txt_yjjssj"] == null || string.IsNullOrEmpty(Request.Form["txt_yjjssj"]))
                return ReturnString.JsonToString(Prompt.error, "请设置阅卷结束时间！", null);
            if (Convert.ToDateTime(Request.Form["txt_yjkssj"]) > Convert.ToDateTime(Request.Form["txt_yjjssj"]))
                return ReturnString.JsonToString(Prompt.error, "结束时间不能小于开始时间！", null);
            */
            EDRS.BLL.TYYW_GG_AJJBXX ajbll = new EDRS.BLL.TYYW_GG_AJJBXX(Request);
            EDRS.Model.TYYW_GG_AJJBXX ajmodel = ajbll.GetModel(Request.Form["key_hidd"]);
            if (ajmodel == null)
                return ReturnString.JsonToString(Prompt.error, "分配失败，" + ((VersionName)0).ToString() + "不存在！", null);
            //json = json.Replace("id", "_wjxh").Replace("MLLX", "_fpbm").Replace("text", "_sfsc").Replace("PARENTID", "_yjxh");

            #region 阅卷人信息
            // localStorage['lssq'] = $("#txt_yjr").val() + "|&|" + zjlx + "|&|" + $("#txt_zjh").val() + "|&|" + yjrsf + "|&|" + cxyy + "|&|" + shr + "|&|" + shbm + "|&|" + $("#txt_shr").val();
            EDRS.Model.YX_DZJZ_LSGL lsglModel = new EDRS.Model.YX_DZJZ_LSGL();
            lsglModel.LSZH = Guid.NewGuid().ToString();
            lsglModel.LSXM = lssqs[0]; //阅卷人
            lsglModel.LSDW = lssqs[1];//证件类型
            lsglModel.LSDWDZ = lssqs[3];//阅卷人身份
            lsglModel.LSDWYZHM = "";
            lsglModel.LSLXDH = lssqs[2];//证件号
            lsglModel.LSSJ = "";
            lsglModel.DELXR = lssqs[4];//查询原因
            lsglModel.DELXRDH = "";
            // lsglModel.LSZGYXSJ = ;
            lsglModel.SFDXZG = "N";
            lsglModel.LSXXBZ = "";
            lsglModel.CJSJ = DateTime.Now;
            lsglModel.ZHYCYJSJ = null;
            lsglModel.SFSC = "N";

            #endregion

            #region 律师阅卷申请
            EDRS.Model.YX_DZJZ_LSYJSQ modelLsyjsq = new EDRS.Model.YX_DZJZ_LSYJSQ();
            modelLsyjsq.LSZH = lsglModel.LSZH;
            modelLsyjsq.YJSQDH = Guid.NewGuid().ToString();
            modelLsyjsq.SQSJ = DateTime.Now;
            modelLsyjsq.SQSM = lssqs[6];//审核部门
            modelLsyjsq.SFSC = "N";
            modelLsyjsq.SHRGH = lssqs[5]; //审核人工号
            modelLsyjsq.SHR = lssqs[7];//审核人
            modelLsyjsq.SHSM = "";
            modelLsyjsq.SHSJ = null;
            modelLsyjsq.YJSQDM = "";
            modelLsyjsq.SQDZT = "T";

            #endregion

            #region 律师阅卷绑定
            Random random = new Random();
            EDRS.Model.YX_DZJZ_LSAJBD model = new EDRS.Model.YX_DZJZ_LSAJBD();
            model.GH = lsglModel.LSZH;
            model.BMSAH = ajmodel.BMSAH;
            model.YJXH = yjxh;
            model.MC = lssqs[0];
            model.AJMC = ajmodel.AJMC;
            model.AJLBBM = ajmodel.AJLB_BM;
            model.AJLBMC = ajmodel.AJLB_MC;
            model.YJKSSJ = DateTime.Now;// Convert.ToDateTime(Request.Form["txt_yjkssj"]);
            model.YJJSSJ = null;// Convert.ToDateTime(Request.Form["txt_yjjssj"]);
            model.YJZH = lssqs[0];
            model.YJMM = random.Next(100000, 999999).ToString();
            model.JDSJ = DateTime.Now;
            model.JDR = UserInfo.MC;
            model.JDRGH = UserInfo.GH;
            model.JDBMBM = "";
            model.JDBMMC = "";
            model.JDDWBM = UserInfo.DWBM;
            model.JDDWMC = UserInfo.DWMC;
            model.SFSC = "N";
            model.YJSQDH = modelLsyjsq.YJSQDH;
            model.JZZTXS = "";
            model.DWBM = UserInfo.DWBM;
            #endregion

            #region 律师阅卷绑定文件
            List<EDRS.Model.YX_DZJZ_LSAJWJFP> modelList = JsonHelper.JsonToList<EDRS.Model.YX_DZJZ_LSAJWJFP>(json);
            #endregion

            EDRS.BLL.YX_DZJZ_LSAJBD bllBD = new EDRS.BLL.YX_DZJZ_LSAJBD(Request);
            try
            {
                if (bllBD.AddList(modelLsyjsq, model, modelList, lsglModel))
                {
                    OperateLog.AddLog(OperateLog.LogType.阅卷Web, "分配成功", "", UserInfo, UserRole, this.Request);
                    return JsonHelper.JsonString(model);
                }

                //return ReturnString.JsonToString(Prompt.win, "分配成功！", null);
            }
            catch (Exception ex)
            {
                return ReturnString.JsonToString(Prompt.error, StringPlus.String2Json(ex.Message), null);
            }
            OperateLog.AddLog(OperateLog.LogType.阅卷Web, "分配失败", "", UserInfo, UserRole, this.Request);
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