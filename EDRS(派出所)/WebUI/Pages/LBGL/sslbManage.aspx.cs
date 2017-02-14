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
    public partial class sslbManage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] t = Request.PathInfo.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            string type = Request.Form["t"];

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
                if (type.Equals("GetPrntJ"))
                    Response.Write(GetPrntJ());
                if (type.Equals("GetMaxSx"))
                    Response.Write(GetMaxSx());
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
            string where = string.Empty;
            //树形循环条件
            bool direction = true;
            bool isOpen = false;
            string withWhere = string.Empty;
            string levelNum = " and level < " + (Request.Form["level"] == null ? 3 : int.Parse(Request.Form["level"].ToString()));
            string isLeaf = "ISLEAF";
            string parentid = "";
            XT_DZJZ_SSLB bll = new XT_DZJZ_SSLB(this.Request);
            List<object> values = new List<object>();
            where += "";
          
            //关键字搜索
            string key = Request.Form["key"];
            //获取父级节点
            string pid = "";
          
            if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(Request.Form["pid"]))
            {
                pid = Request.Form["pid"];
                withWhere = " SSLBMC like '%" + StringPlus.ReplaceSingle(key) + "%' and FSSLBBM = '" + pid + "'";
            }
            else if (!string.IsNullOrEmpty(key))
            {
                withWhere = " SSLBMC like '%" + StringPlus.ReplaceSingle(key) + "%' ";
                direction = false;
             //   levelNum = "";
               // isOpen = true;
               // isLeaf = "";
            }
            //else if (!string.IsNullOrEmpty(Request.Form["pid"]))
            //{
            //    pid = Request.Form["pid"];
            //    withWhere = "  FSSLBBM = '" + StringPlus.ReplaceSingle(Request.Form["pid"].ToString()) + "'";
            //}

            if (string.IsNullOrEmpty(withWhere))
                withWhere = " FSSLBBM is NULL ";
            where += levelNum;


            DataSet ds = bll.GetTreeList(where, withWhere, direction, values.ToArray());

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];

                //数据日志
                OperateLog.AddLog(OperateLog.LogType.参数配置Web, "获取所属类别列表", UserInfo, UserRole, this.Request);
                return "{\"Rows\":" + new TreeJson(dt, "SSLBBM", "SSLBMC", "FSSLBBM", isLeaf, parentid, string.IsNullOrEmpty(pid) ? "" : pid, isOpen, true).ResultJson.ToString() + ",\"Total\":100}";
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.参数配置Web, "所属类别未找到数据", UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "未找到单位信息！", null);
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

            string whereValue = String.Empty;

            string lx = Request.Form.Get("slct_type_val");

            if (lx == "1")
                whereValue = "000000-";
            else if (lx == "2")
                whereValue = "000001-";
            else if (lx == "3")
                whereValue = "000002-";
            else
                return ReturnString.JsonToString(Prompt.error, "类型选择错误，请刷新页面重试！", null);

            XT_DZJZ_SSLB bll = new XT_DZJZ_SSLB(this.Request);
            DataSet ds = bll.GetListByPage(" and sslbbm like '" + whereValue + "%' ", "sslbbm desc", 1, 1, null);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                //0101    000000-000000
                int num = int.Parse(ds.Tables[0].Rows[0]["SSLBBM"].ToString().Substring(7, 6));
                num++;
                whereValue += num.ToString().PadLeft(6, '0');             
            }
            else
                whereValue += "000000";
           
            EDRS.Model.XT_DZJZ_SSLB model = new EDRS.Model.XT_DZJZ_SSLB();
            model.SSLBBM = whereValue;
            model.SSLBLX = lx;
            model.SSLBMC = Request.Form["txt_name"];
            model.SSLBSX = Convert.ToDecimal(Request.Form["txt_sx"]);
            model.SSLBSM = Request.Form["txt_sm"];
            model.FSSLBBM = Request.Form["key_fhidd"];
            //LogHelper.LogError(Request, "id:" + model.SSLBBM + ",Fid:" + model.FSSLBBM, " 1", "测试1");
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

            XT_DZJZ_SSLB bll = new XT_DZJZ_SSLB(this.Request);
            EDRS.Model.XT_DZJZ_SSLB model = bll.GetModel(ajlbbm);
            string lbbm = string.Empty;
            if (model != null)
            {
                lbbm = model.SSLBBM;
                model.SSLBMC = Request.Form.Get("txt_name");
                model.SSLBSX = Convert.ToDecimal(Request.Form.Get("txt_sx"));
                model.SSLBSM = Request.Form.Get("txt_sm");
                if (bll.Update(model))
                {
                    System.Collections.Hashtable hssql = new System.Collections.Hashtable();

                    hssql.Add(string.Format("update xy_dzjz_mbpzb set sslbmc='{0}',DossierTypeDisplayMember='{0}' where sslbbm='{1}'", model.SSLBMC, lbbm), null);
                    XY_DZJZ_MBPZB bllmb = new XY_DZJZ_MBPZB(this.Request);
                    bllmb.Update(hssql);


                    //数据日志
                    OperateLog.AddLog(OperateLog.LogType.参数配置Web, "修改所属类别成功", "", UserInfo, UserRole, this.Request);
                    return ReturnString.JsonToString(Prompt.win, "保存成功", null);
                }
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.参数配置Web, "修改所属类别失败", "", UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.error, "保存失败", null);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.参数配置Web, "修改所属类别未找到信息", "", UserInfo, UserRole, this.Request);
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
            string id = Request.Form["id"];
            //string[] id = ids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            //ids = "";
            //for (int i = 0; i < id.Length; i++)
            //{
            //    ids += "'" + id[i].Trim() + "'";
            //    if (i < id.Length - 1)
            //        ids += ",";
            //}
            XT_DZJZ_SSLB bll = new XT_DZJZ_SSLB(this.Request);

            DataSet ds = bll.GetTreeList("", " FSSLBBM = '" + id + "'", true);
            string ids = string.Empty;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ids += "'" + ds.Tables[0].Rows[i]["SSLBBM"].ToString().Trim() + "',";                   
                }
            }
            ids += "'" + id + "'";
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
        private string GetModel(string sslbbm)
        {
            if (string.IsNullOrEmpty(sslbbm))
            {
                sslbbm = Request.Form.Get("id");
                if (string.IsNullOrEmpty(sslbbm))
                {
                    return ReturnString.JsonToString(Prompt.error, "参数错误", null);
                }
            }
            XT_DZJZ_SSLB bll = new XT_DZJZ_SSLB(this.Request);
            EDRS.Model.XT_DZJZ_SSLB model = bll.GetModel(sslbbm);
            if (model != null)
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.参数配置Web, "根据编号获取所属类别成功", "", UserInfo, UserRole, this.Request);
                return JsonHelper.JsonString(model);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.参数配置Web, "根据编号获取所属类别失败", "", UserInfo, UserRole, this.Request);
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
            string name = Request.Form["txt_name"];
            string sx = Request.Form["txt_sx"];
            string sm = Request.Form["txt_sm"];

            if (type == "3")
            {
                if (string.IsNullOrEmpty(Request.Form["key_fhidd"]))
                {
                    msg = "请选择卷！";
                    return false;
                }
            }
            if (string.IsNullOrEmpty(name))
            {
                msg = "请输入名称！";
                return false;
            }
            if (!string.IsNullOrEmpty(name) && name.Length > 150)
            {
                msg = "名称不能超过150个字！";
                return false;
            }
            if (!string.IsNullOrEmpty(sx) && !PageValidate.IsInt(sx))
            {
                msg = "顺序必须为正整数！";
                return false;
            }
            if (!string.IsNullOrEmpty(sm) && sm.Length > 2000)
            {
                msg = "说明不能超过2000个字！";
                return false;
            }
            return true;
        }
        #endregion

        #region 获取所有卷
        /// <summary>
        /// 获取所有卷
        /// </summary>
        /// <returns></returns>
        private string GetPrntJ()
        {
            XT_DZJZ_SSLB bll = new XT_DZJZ_SSLB(Request);
            DataSet ds = bll.GetList(" and sslblx=1 order by sslbsx asc");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                    return JsonHelper.JsonString(ds.Tables[0]);
                return ReturnString.JsonToString(Prompt.error, "未设置卷信息", null);
            }
            return ReturnString.JsonToString(Prompt.error, "未获取卷信息", null);
        } 
        #endregion

        #region 获取最大排序数
        /// <summary>
        /// 获取最大排序数
        /// </summary>
        /// <returns></returns>
        private string GetMaxSx()
        {
            string fid = Request.Form["fid"];
            List<object> objValues = new List<object>();
            string where = string.Empty;
            if (!string.IsNullOrEmpty(fid))
            {
                where += " and fsslbbm=:fsslbbm";
                objValues.Add(fid);
                where += " and sslblx=3";
            }
            else
                where += " and sslblx=1";

            XT_DZJZ_SSLB bll = new XT_DZJZ_SSLB(this.Request);
            object count = bll.GetMaxSx(where, objValues.ToArray());
            if (count != null)
                return  ReturnString.JsonToString(Prompt.win, (Convert.ToInt32(count)+1).ToString(), null);
            return  ReturnString.JsonToString(Prompt.win,"1", null);
        } 
        #endregion

    }
}