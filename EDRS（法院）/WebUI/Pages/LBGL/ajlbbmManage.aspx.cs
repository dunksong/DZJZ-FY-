using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using EDRS.Common;
using EDRS.BLL;

namespace WebUI.Pages.LBGL
{
    public partial class ajlbbmManage : BasePage
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
                if (type.Equals("AddData"))
                    Response.Write(AddData());
                if (type.Equals("UpData"))
                    Response.Write(UpData());
                if (type.Equals("DelData"))
                    Response.Write(DelData());
                if (type.Equals("GetModel"))
                    Response.Write(GetModel(""));
                if (type.Equals("GetType"))
                    Response.Write(GetYWBM());
           
                Response.End();
            }
        }

        #region 绑定列表
        /// <summary>
        /// 绑定列表
        /// </summary>
        /// <returns></returns>
        public string ListBind()
        {
            string page = Request.Form["page"];
            string rows = Request.Form["pagesize"];
            string key = Request.Form["key"];
            string ywbm = Request.Form["ywbm"];

            int pageNumber = int.Parse(page);
            int pageSize = int.Parse(rows);
            string where = string.Empty;

            List<object> values = new List<object>();
            
            if (!string.IsNullOrEmpty(key))
            {
                where += " and AJLBMC like :AJLBMC ";
                values.Add("%" + key + "%");
            }
            if (!string.IsNullOrEmpty(ywbm))
            {
                where += " and YWBM = :YWBM ";
                values.Add(ywbm);
            }

            EDRS.BLL.XT_DM_AJLBBM bll = new EDRS.BLL.XT_DM_AJLBBM(this.Request);

            DataSet ds = bll.GetListByPage(where+" and SFSC = 'N' ", " ajlbbm asc", (pageSize * pageNumber) - pageSize + 1, (pageSize * pageNumber), values.ToArray());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.参数配置Web, "类别绑定成功", UserInfo, UserRole, this.Request);
                int count = bll.GetRecordCount(where+" and SFSC='N' " , values.ToArray());
                DataTable dt = (ds.Tables[0]).Copy();

                return "{\"Total\":" + count + ",\"Rows\":" + JsonHelper.JsonString(dt) + "}";
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.参数配置Web, "类别绑定列表未找到信息", UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "未找到类别", null);
        } 
        #endregion

        #region 添加类别
        /// <summary>
        /// 添加类别
        /// </summary>
        /// <returns></returns>
        private string AddData()
        {
            string msg = string.Empty;
            if (!ProvingFrom(ref msg))
            {
                return ReturnString.JsonToString(Prompt.error, msg, null);
            }
            string ywbm = Request.Form.Get("slct_type_val");
            string ajlbbm = ywbm + "01";

            XT_DM_AJLBBM bll = new XT_DM_AJLBBM(this.Request);
            DataSet ds = bll.GetListByPage(" and ywbm=:ywbm ", "ajlbbm desc", 1, 1, new object[] { ywbm });
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string lb = ds.Tables[0].Rows[0]["AJLBBM"].ToString();
                lb = lb.Substring(ywbm.Length, lb.Length - ywbm.Length);
                int num = int.Parse(lb);
                num++;
                ajlbbm = ywbm + num.ToString().PadLeft(lb.Length, '0');
            }
         
            EDRS.Model.XT_DM_AJLBBM model = new EDRS.Model.XT_DM_AJLBBM();
            model.YWBM = ywbm;
            model.AJLBBM = ajlbbm;
            model.AJLBMC = Request.Form.Get("txt_lbname");
            model.AJSLJC = Request.Form.Get("txt_sljc");
            model.SFSC = "N";           
    
            if (bll.Add(model))
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.参数配置Web, "添加类别成功", "", UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.win, "保存成功", null);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.参数配置Web, "添加类别失败", "", UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "保存失败", null);
        }
        #endregion

        #region 修改类别
        /// <summary>
        /// 修改类别
        /// </summary>
        /// <returns></returns>
        private string UpData()
        {
            string ajlbbm = Request.Form.Get("key_hidd").Trim();
            if (string.IsNullOrEmpty(ajlbbm))
            {
                return ReturnString.JsonToString(Prompt.error, "未找到需要修改信息", null);
            }
            string msg = string.Empty;
            if (!ProvingFrom(ref msg))
            {
                return ReturnString.JsonToString(Prompt.error, msg, null);
            }

            XT_DM_AJLBBM bll = new XT_DM_AJLBBM(this.Request);
            EDRS.Model.XT_DM_AJLBBM model = bll.GetModel(ajlbbm);
            if (model != null)
            {
                string ywbm = Request.Form.Get("slct_type_val");

                string ajlbbmNew = ywbm + "01";
                //业务类型不同重新设置编号
                if (model.YWBM != ywbm)
                {
                    DataSet ds = bll.GetListByPage(" and ywbm=:ywbm ", "ajlbbm desc", 1, 1, new object[] { ywbm });
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        //int num = int.Parse(ds.Tables[0].Rows[0]["AJLBBM"].ToString().Substring(2, 2));
                        //num++;
                        //ajlbbmNew = Request.Form.Get("slct_type_val") + num.ToString().PadLeft(2, '0');

                        string lb = ds.Tables[0].Rows[0]["AJLBBM"].ToString();
                        lb = lb.Substring(ywbm.Length, lb.Length - ywbm.Length);
                        int num = int.Parse(lb);
                        num++;
                        ajlbbmNew = ywbm + num.ToString().PadLeft(lb.Length, '0');

                    }

                    model.AJLBBM = ajlbbmNew;
                }             
             
                model.YWBM = Request.Form.Get("slct_type_val");                
                model.AJLBMC = Request.Form.Get("txt_lbname");
                model.AJSLJC = Request.Form.Get("txt_sljc");

                if (bll.Update(model,ajlbbm))
                {
                    //数据日志
                    OperateLog.AddLog(OperateLog.LogType.参数配置Web, "修改类别成功", "", UserInfo, UserRole, this.Request);
                    return ReturnString.JsonToString(Prompt.win, "保存成功", null);
                }
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.参数配置Web, "修改类别失败", "", UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.error, "保存失败", null);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.参数配置Web, "修改类别未找到信息", "", UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "未找到需要修改信息", null);
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        private string DelData()
        {
            string ids = Request.Form["id"];
            string[] id = ids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            ids = "";
            for (int i = 0; i < id.Length; i++)
            {
                ids += "'" + id[i].Trim() + "'";
                if (i < id.Length - 1)
                    ids += ",";
            }
            XT_DM_AJLBBM bll = new XT_DM_AJLBBM(this.Request);

            if (bll.DeleteList(ids))
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.参数配置Web, "删除类别成功", Request.Form["cs"], UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.win, "删除类别成功", null);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.参数配置Web, "删除类别失败", Request.Form["cs"], UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "删除类别失败", null);
        }
        #endregion

        #region 根据类别编号获取数据
        /// <summary>
        /// 根据类别编号获取数据
        /// </summary>
        /// <param name="DWBM"></param>
        /// <returns></returns>
        private string GetModel(string ajlbbm)
        {
            if (string.IsNullOrEmpty(ajlbbm))
            {
                ajlbbm = Request.Form.Get("id");
                if (string.IsNullOrEmpty(ajlbbm))
                {
                    return ReturnString.JsonToString(Prompt.error, "参数错误", null);
                }
            }
            XT_DM_AJLBBM bll = new XT_DM_AJLBBM(this.Request);
            EDRS.Model.XT_DM_AJLBBM model = bll.GetModel(ajlbbm);
            if (model != null)
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.参数配置Web, "根据编号获取类型信息成功","", UserInfo, UserRole, this.Request);
                return JsonHelper.JsonString(model);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.参数配置Web, "根据编号获取类型信息失败", "", UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "获取数据失败", null);
        }
        #endregion

        #region 表单验证
        /// <summary>
        /// 表单验证
        /// </summary>
        private bool ProvingFrom(ref string msg)
        {
            string type = Request.Form.Get("slct_type_val");
            string lbmc = Request.Form["txt_lbname"];
            string sljc = Request.Form["txt_sljc"];
            if (string.IsNullOrEmpty(type))
            {
                msg = "请选择业务编码！";
                return false;
            }
            if (string.IsNullOrEmpty(lbmc))
            {
                msg = "请输入类别名称！";
                return false;
            }
            if (!string.IsNullOrEmpty(lbmc) && lbmc.Length > 30)
            {
                msg = "类别名称不能超过30个字！";
                return false;
            }
            if (!string.IsNullOrEmpty(sljc) && sljc.Length > 30)
            {
                msg = "受理简称不能超过30个字！";
                return false;
            }
            return true;
        }
        #endregion

        #region 业务编码
        /// <summary>
        /// 业务编码
        /// </summary>
        /// <returns></returns>
        private string GetYWBM()
        {
            XT_DM_YWBM bll = new XT_DM_YWBM(Request);
            DataSet ds = bll.GetList(" and SFSC='N' order by ywbm", null);
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0].Copy();
                    dt.Columns["YWBM"].ColumnName = "id";
                    dt.Columns["YWMC"].ColumnName = "name";                   
                    return JsonHelper.JsonString(dt);
                }
                return ReturnString.JsonToString(Prompt.error, "未设置类别，请先设置类别", null);
            }
            return ReturnString.JsonToString(Prompt.error, "未找到类别", null);
        }
        #endregion


    }
}