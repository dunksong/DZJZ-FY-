using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using EDRS.BLL;
using EDRS.Common;
using System.Text;
using System.Reflection;
using System.Data;
using System.Net;
using System.IO;
using EDRS.Common.DEncrypt;

namespace WebUI
{
    public partial class ArchiveManage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request["t"];

            if (!string.IsNullOrEmpty(type))
            {
                
                Response.ContentEncoding = Encoding.UTF8;
                Response.ContentType = "application/json";
                if (type.Equals("ListBind"))
                    Response.Write(ListBind());
                if (type.Equals("RomIsLock"))
                    Response.Write(RomIsLock());
                Response.End();
            }
            
        }

        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="key"></param>
        /// <param name="casename"></param>
        /// <param name="dutyman"></param>
        /// <param name="relevance"></param>
        /// <param name="timebegin"></param>
        /// <param name="timeend"></param>
        /// <returns></returns>
        //[WebMethod]
        public string ListBind()
        {
            string page = Request["page"];
            string rows = Request["pagesize"];
            string bmsah = Request["key"];
            string jzmc = Request["casename"];
            
            int pageNumber = int.Parse(page);
            int pageSize = int.Parse(rows);
            string where = string.Empty;

            object[] values = new object[7];
           
            string isLogck = "\"IsLock\":true";
            if (!string.IsNullOrEmpty(isLogck))
            {
                where += " and JZXGH like :JZXGH";
                values[0] = "%" + isLogck + "%";
            }            
            where += " and T.SFSC='N'";
            where += " and (d.DWBM in(select distinct trim(QXBM) from xt_dm_qx a join XT_QX_RYJSFP b on trim(a.jsbm) = b.jsbm and trim(a.bmbm)=b.bmbm and trim(a.dwbm)=b.dwbm and gh=:GH1 and b.dwbm=:DWBM1 and a.qxlx=0) and aj.AJLB_BM in ( select trim(QXBM) from xt_dm_qx a join XT_QX_RYJSFP b on trim(a.jsbm) = b.jsbm and trim(a.bmbm)=b.bmbm and trim(a.dwbm)=b.dwbm and gh=:GH2 and b.dwbm=:DWBM2 and a.qxlx=1)) ";
            values[1] = base.UserInfo.GH;
            values[2] = base.UserInfo.DWBM;
            values[3] = base.UserInfo.GH;
            values[4] = base.UserInfo.DWBM;

            if (!string.IsNullOrEmpty(jzmc))
            {
                where += " and JZMC like :JZMC";
                values[5] = "%" + jzmc + "%";
            }
            if (!string.IsNullOrEmpty(bmsah))
            {
                where += " and T.BMSAH like :BMSAH";
                values[6] = "%" + bmsah + "%";
            }

            YX_DZJZ_JZJBXX bll = new YX_DZJZ_JZJBXX(this.Request);

            DataSet ds = bll.GetListByPagePower(where, "", (pageSize * pageNumber) - pageSize + 1, (pageSize * pageNumber), values);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {                
                DataTable dt = ds.Tables[0];
                int count = bll.GetRecordCountPower(where, values);
                OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "卷宗解锁查询", UserInfo, UserRole, this.Request);
                return "{\"Total\":" + count + ",\"Rows\":" + JsonHelper.JsonString(dt) + "}";
            }
            OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "卷宗解锁查询未找到数据", UserInfo, UserRole, this.Request);
            return "{\"Total\":" + 0 + ",\"Rows\":[]}";
        } 
        #endregion


        #region 获取案件类型权限编码集合
        /// <summary>
        /// 获取案件类型权限编码集合
        /// </summary>
        /// <param name="dwbm"></param>
        /// <param name="bmbm"></param>
        /// <param name="jsbm"></param>
        /// <returns></returns>
        private List<string> GetAjTypeBm(string dwbm, string bmbm, string gh)
        {
            EDRS.BLL.XT_DM_QX bll = new EDRS.BLL.XT_DM_QX(Request);
            DataSet ds = bll.GetQxListByRole(dwbm, bmbm, gh, 1,"");
            List<string> list = new List<string>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow ro in ds.Tables[0].Rows)
                    list.Add(ro["QXBM"].ToString());
            }
            return list;
        }
        /// <summary>
        /// 获取单位权限
        /// </summary>
        /// <param name="dwbm"></param>
        /// <param name="bmbm"></param>
        /// <param name="jsbm"></param>
        /// <returns></returns>
        private List<string> GetDwBm(string dwbm, string bmbm, string gh)
        {
            EDRS.BLL.XT_DM_QX bll = new EDRS.BLL.XT_DM_QX(Request);
            DataSet ds = bll.GetQxListByRole(dwbm, bmbm, gh, 0,"");
            List<string> list = new List<string>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow ro in ds.Tables[0].Rows)
                    list.Add(ro["QXBM"].ToString());
            }
            return list;
        }
        #endregion


        #region 解除卷宗案件基本信息锁定状态
        /// <summary>
        /// 解除卷宗案件基本信息锁定状态
        /// </summary>
        /// <returns></returns>
        private string RomIsLock()
        {
            string bmsahs = Request.Form["bmsahs"];
            if (bmsahs == null || string.IsNullOrEmpty(bmsahs))
            {
                return ReturnString.JsonToString(Prompt.error, "解锁参数不正确", null);
            }
            
            string[] str = bmsahs.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries);

            List<EDRS.Model.YX_DZJZ_JZJBXX> modelList = new List<EDRS.Model.YX_DZJZ_JZJBXX>();
            for (int i = 0; i < str.Length; i++)
            {
                EDRS.Model.YX_DZJZ_JZJBXX model = new EDRS.Model.YX_DZJZ_JZJBXX();
                model.BMSAH = str[i];
                model.JZXGH = "";
                model.ZHXGSJ = DateTime.Now;
                model.ZZZT = "-1";
                modelList.Add(model);
            }
            YX_DZJZ_JZJBXX bll = new YX_DZJZ_JZJBXX(this.Request);
            if (bll.LockByModelList(modelList))
            {
                OperateLog.AddLogList(OperateLog.LogType.案件卷宗制作Web, "卷宗解锁成功", str, UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.win, "解锁成功", null);
            }
            OperateLog.AddLog(OperateLog.LogType.案件卷宗制作Web, "卷宗解锁失败", bmsahs, UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "解锁失败", null);
        } 
        #endregion

    }

}