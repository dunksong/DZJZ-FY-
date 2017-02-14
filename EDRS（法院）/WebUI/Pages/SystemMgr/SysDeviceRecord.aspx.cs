using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Newtonsoft.Json;

using EDRS.BLL;
using System.Text;
using System.Text.RegularExpressions;
using EDRS.Common;
using System.Data;
using System.ComponentModel;

namespace WebUI
{
    public partial class SysDeviceRecord : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] t = Request.PathInfo.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            string type = Request["t"];

            if (!string.IsNullOrEmpty(type))
            {
                Response.ContentEncoding = Encoding.UTF8;
                Response.ContentType = "application/json";
                if (type.Equals("ListBind"))
                    Response.Write(ListBind());
             
                Response.End();
            }
        }

        /// <summary>
        /// 绑定列表
        /// </summary>
        /// <returns></returns>
        public string ListBind()
        {
            string page = Request["page"];
            string rows = Request["pagesize"];
            string key = Request["key"];
            string dwbm = Request["dwbm"];
          
            int pageNumber = int.Parse(page);
            int pageSize = int.Parse(rows);
            string where = string.Empty;

            object[] values = new object[1];
            if (!string.IsNullOrEmpty(key))
            {
                //设备型号
                where += " and DEVTYPE like :DEVTYPE ";
                values[0] = "%" + key + "%";
            }
            if (!string.IsNullOrEmpty(dwbm))
            {
                dwbm = dwbm.Replace(";", ",");
                where += " and DWBM in (" + StringPlus.ReplaceSingle(dwbm) + ")";               
            }

            EDRS.BLL.XY_DZJZ_SBDJ bll = new EDRS.BLL.XY_DZJZ_SBDJ(this.Request);
            
            string jsbms=string.Empty;
            for (int i = 0; i < this.UserRole.Count; i++)
			{
                if(i>0)
                    jsbms+=",";
                jsbms+=this.UserRole[i].JSBM;
			}


            DataSet ds = bll.GetListByPage(this.UserInfo.DWBM,jsbms, where, "", (pageSize * pageNumber) - pageSize + 1, (pageSize * pageNumber), values);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.设备登记Web, "绑定设备登记列表成功", UserInfo, UserRole, this.Request);
                int count = bll.GetRecordCount(this.UserInfo.DWBM, jsbms, where, values);

                return "{\"Total\":" + count + ",\"Rows\":" + JsonHelper.JsonString(ds.Tables[0]) + "}";
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.设备登记Web, "获取设备登记列表无数据", UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.win, "未找到设备登记", null);
        }

      

        #region 表单验证
        /// <summary>
        /// 表单验证
        /// </summary>
        private bool ProvingFrom(ref string msg)
        {
            string type = Request.Form.Get("slct_type_val");
            string value = Request.Form["txt_value"];
            if (string.IsNullOrEmpty(type))
            {
                msg = "请选择配置类型！";
                return false; 
            }
            if (string.IsNullOrEmpty(value))
            {
                msg = "请输入对应配置的值！";
                return false;
            }
            return true;
        } 
        #endregion

     
    }
}