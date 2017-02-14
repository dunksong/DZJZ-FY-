using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Text.RegularExpressions;
using EDRS.Common;
using EDRS.BLL;
using System.Data;
using Cyvation.CCQE.Common;

namespace WebUI.Pages.Template
{
    public partial class TemplateDeploy_bak : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request["t"];
            
            if (!string.IsNullOrEmpty(type))
            {
                Response.ContentEncoding = Encoding.UTF8;
                Response.ContentType = "application/json";

                if (type.Equals("GetData"))
                    Response.Write(ListBind());
                if (type.Equals("AddData"))
                    Response.Write(AddData());
                if (type.Equals("UpDate1"))
                    Response.Write(UpData1());
                if (type.Equals("UpData"))
                    Response.Write(UpData());
                if (type.Equals("DelData"))
                    Response.Write(DelData());
                if (type.Equals("GetModelPList"))
                    Response.Write(GetModelOrParentList());
                if (type.Equals("GetAJType"))
                    Response.Write(GetAJType());
                if (type.Equals("GetSSLB"))
                    Response.Write(GetSSLB());
                Response.End();
            }
        }
        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="dwbm"></param>
        /// <returns></returns>
        private string UpData1()
        {
            string msg = string.Empty;
            if (!ProvingFrom(ref msg))
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.卷宗模板配置Web, "修改模板配置验证失败" + msg, Request.Form.Get("txt_name_up"), UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.error, msg, null);
            }

            XY_DZJZ_MBPZB bll = new XY_DZJZ_MBPZB(this.Request);
            EDRS.Model.XY_DZJZ_MBPZB model = bll.GetModel(Request.Form.Get("tempID"));
            if (model != null)
            {
                model.DossierTypeValueMember = Request.Form.Get("tempID");
                model.CaseInfoTypeID = Request.Form.Get("tree_ajtype_val");
                model.CaseInfoTypeName = Request.Form.Get("tree_ajtype") ?? "";
                model.DossierTypeDisplayMember = Request.Form.Get("txt_name");
                model.SSLBBM = Request.Form.Get("tree_sslb_val") ?? "";
                model.SSLBMC = Request.Form.Get("tree_sslb") ?? "";
                if (string.IsNullOrEmpty(Request.Form.Get("txt_rank")))
                    model.SortIndex = 1;
                else
                    model.SortIndex = int.Parse(Request.Form.Get("txt_rank"));
                if (bool.Parse(Request.Form.Get("chk_autoFound_ar")))
                    model.Auto = "Y";
                else
                    model.Auto = "N";
                if (bll.Update(model))
                {
                    //数据日志
                    OperateLog.AddLog(OperateLog.LogType.卷宗模板配置Web, "修改模板配置成功", Request.Form.Get("txt_name_up"), UserInfo, UserRole, this.Request);
                    return ReturnString.JsonToString(Prompt.win, "保存成功", null);
                }
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.卷宗模板配置Web, "修改模板配置失败" + msg, Request.Form.Get("txt_name_up"), UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.error, "保存失败", null);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.卷宗模板配置Web, "修改模板配置未找到修改信息" + msg, Request.Form.Get("txt_name_up"), UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "未找到需要修改信息", null);
        }
        #endregion
        #region 添加单位数据
        /// <summary>
        /// 添加单位数据
        /// </summary>
        /// <returns></returns>
        private string AddData()
        {
            string msg = string.Empty;
            if (!ProvingFrom(ref msg))
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.卷宗模板配置Web, "添加模板配置验证失败" + msg, Request.Form.Get("txt_name"), UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.error, msg, null);
            }
            string number = "00000001";

            XY_DZJZ_MBPZB bll = new XY_DZJZ_MBPZB(this.Request);
            DataSet ds = bll.GetListByPage("", "DOSSIERTYPEVALUEMEMBER desc", 0, 1);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                number = ds.Tables[0].Rows[0]["DOSSIERTYPEVALUEMEMBER"].ToString();
                number = (int.Parse(number) + 1).ToString().PadLeft(8, '0');
            }

            EDRS.Model.XY_DZJZ_MBPZB model = new EDRS.Model.XY_DZJZ_MBPZB();
            model.DossierTypeValueMember = number;
            model.CaseInfoTypeID = Request.Form.Get("tree_ajtype_val") ?? ""; 
            model.CaseInfoTypeName = Request.Form.Get("tree_ajtype");
            model.DossierParentMember = Request.Form.Get("key_parent") ?? "";
            model.UnitID = UserInfo.DWBM;
            model.SSLBBM = Request.Form.Get("tree_sslb_val") ?? "";
            model.SSLBMC = Request.Form.Get("tree_sslb") ?? "";
            if (string.IsNullOrEmpty(Request.Form.Get("txt_rank")))
                model.SortIndex = 1;
            else
                model.SortIndex = int.Parse(Request.Form.Get("txt_rank"));
            model.DossierTypeDisplayMember = Request.Form.Get("txt_name");
            model.DossierEvidenceValueMember = "";
            model.Category = Request.Form.Get("hidd_Category");
            if (bool.Parse(Request.Form.Get("chk_autoFound_ar")))
                model.Auto = "Y";
            else
                model.Auto = "N";
            if (bll.Add(model))
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.卷宗模板配置Web, "添加模板配置成功", Request.Form.Get("txt_name"), UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.win, "保存成功", number);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.卷宗模板配置Web, "添加模板配置失败", Request.Form.Get("txt_name"), UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "保存失败", null);
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="dwbm"></param>
        /// <returns></returns>
        private string UpData()
        {
            string dwbm = Request.Form.Get("key_hidd_up");
            if (string.IsNullOrEmpty(dwbm) || !Regex.IsMatch(dwbm, @"^[A-Za-z0-9]{1,8}$"))
                return ReturnString.JsonToString(Prompt.error, "未找到需要修改信息", null);
            string msg = string.Empty;
            if (!ProvingUpFrom(ref msg))
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.卷宗模板配置Web, "修改模板配置验证失败" + msg, Request.Form.Get("txt_name_up"), UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.error, msg, null);
            }

            XY_DZJZ_MBPZB bll = new XY_DZJZ_MBPZB(this.Request);
            EDRS.Model.XY_DZJZ_MBPZB model = bll.GetModel(dwbm);
            if (model != null)
            {
                model.CaseInfoTypeID = Request.Form.Get("tree_ajtype_up_val");
                model.CaseInfoTypeName = Request.Form.Get("tree_ajtype_up") ?? "";
                model.DossierTypeDisplayMember = Request.Form.Get("txt_name_up");
                model.SSLBBM = Request.Form.Get("tree_sslb_up_val") ?? "";
                model.SSLBMC = Request.Form.Get("tree_sslb_up") ?? "";
                if (string.IsNullOrEmpty(Request.Form.Get("txt_rank_up")))
                    model.SortIndex = 1;
                else
                    model.SortIndex = int.Parse(Request.Form.Get("txt_rank_up"));
                if (bool.Parse(Request.Form.Get("chk_autoFound_up_ar")))
                    model.Auto = "Y";
                else
                    model.Auto = "N";
                if (bll.Update(model))
                {
                    //数据日志
                    OperateLog.AddLog(OperateLog.LogType.卷宗模板配置Web, "修改模板配置成功", Request.Form.Get("txt_name_up"), UserInfo, UserRole, this.Request);
                    return ReturnString.JsonToString(Prompt.win, "保存成功", null);
                }
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.卷宗模板配置Web, "修改模板配置失败" + msg, Request.Form.Get("txt_name_up"), UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.error, "保存失败", null);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.卷宗模板配置Web, "修改模板配置未找到修改信息" + msg, Request.Form.Get("txt_name_up"), UserInfo, UserRole, this.Request);
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
            //未启用多选
            string ids = Request.Form["DossierTypeValueMember"];
            XY_DZJZ_MBPZB bll = new XY_DZJZ_MBPZB(this.Request);
            //if (!bll.ExistsChildren(ids))
            //{
            if (bll.DeleteNode(ids))
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.卷宗模板配置Web, "删除模板配置成功", Request.Form["name"], UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.win, "删除数据成功", null);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.卷宗模板配置Web, "删除模板配置失败", Request.Form["name"], UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "删除数据失败", null);
            //}
            //return ReturnString.JsonToString(Prompt.error, "该卷（目录）下包含目录（文件），请先选择删除该卷（目录）下的目录（文件）后再删除！", null);
            //启用多选
            //string[] id = ids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            //ids = "";
            //for (int i = 0; i < id.Length; i++)
            //{
            //    ids += "'" + id[i].Trim() + "'";
            //    if (i < id.Length - 1)
            //        ids += ",";
            //}

            //if (bll.GetRecordCount(" and FDWBM in (" + ids + ")") == 0)
            //{
            //    if (bll.Delete(ids))
            //        return ReturnString.JsonToString(Prompt.win, "删除数据成功", null);
            //    return ReturnString.JsonToString(Prompt.error, "删除数据失败", null);
            //}
            //else
            //    return ReturnString.JsonToString(Prompt.error, "该单位包含下级单位，请先选择删除下级单位！", null);
        }
        #endregion

        #region 绑定案件类别
        /// <summary>
        /// 绑定案件类别
        /// </summary>
        /// <returns></returns>
        private string GetAJType()
        {
            XT_DM_AJLBBM bll = new XT_DM_AJLBBM(this.Request);
            string where = " and SFSC=:SCSC";
            object[] objValues = new object[2];
            objValues[0] = "N";  
            if (Request["key"] != null && !string.IsNullOrEmpty(Request["key"]))
            {
                where += " and AJLBMC like :AJLBMC ";
                objValues[1] = "%" + Request["key"].Trim() + "%";
            }
            where += " order by xh desc ";
            DataSet ds = bll.GetList(where, objValues);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                dt.Columns["AJLBBM"].ColumnName = "id";
                dt.Columns["AJLBMC"].ColumnName = "text";
                return EDRS.Common.JsonHelper.JsonString(dt);
            }
            return ReturnString.JsonToString(Prompt.error, "未找到"+((VersionName)0).ToString()+"类别", null);
        } 
        #endregion

        #region 绑定所属分类
        /// <summary>
        /// 绑定案件类别
        /// </summary>
        /// <returns></returns>
        private string GetSSLB()
        {
            XT_DM_AJLBBM bll = new XT_DM_AJLBBM(this.Request);
            string where = " ";
            List<object> objValues = new List<object>();
            if (Request["key"] != null && !string.IsNullOrEmpty(Request["key"]))
            {
                where += " AND SSLBMC like :SSLBMC ";
                objValues.Add("%" + Request["key"].Trim() + "%");
            }
            if (Request["LBLX"] != null && !string.IsNullOrEmpty(Request["LBLX"]))
            {
                where += " AND FSSLBBM = :FSSLBBM";
                objValues.Add(Request["LBLX"]);
            }
            else
            {
                where = " AND FSSLBBM IS NULL";
                objValues.Clear();
            }
            where += "";
            DataSet ds = bll.GetSSLBList(where, objValues.ToArray());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                dt.Columns["SSLBBM"].ColumnName = "id";
                dt.Columns["SSLBMC"].ColumnName = "text";
                return EDRS.Common.JsonHelper.JsonString(dt);
            }
            return ReturnString.JsonToString(Prompt.error, "未找到"+((VersionName)0).ToString()+"类别", null);
        }
        #endregion

        #region 绑定数据列表
        /// <summary>
        /// 绑定数据列表
        /// </summary>
        /// <returns></returns>
        private string ListBind()
        {
            //int pageNumber = 1; //int.Parse(Request["page"]);
            //int pageSize = int.MaxValue;// int.Parse(Request["rows"]);
            
            string where = string.Empty;

            //树形循环条件
            bool direction = true;
            bool isOpen = false;
            string withWhere = string.Empty;
            string levelNum = "";// " and level < 10 ";
            string isLeaf = "ISLEAF";
            XY_DZJZ_MBPZB bll = new XY_DZJZ_MBPZB(this.Request);
            object[] values = new object[1];

            if (UserInfo != null)
            {
                where = " and UNITID = :UNITID";
                values[0] = UserInfo.DWBM;
            }

            //关键字搜索
            string key = Request["key"];
            //获取父级节点
            string pid = "";
            pid = Request["pid"];
            if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(pid))
            {                
                withWhere = " and (DossierTypeDisplayMember like '%" + key + "%') and DossierParentMember = '" + pid + "'";
            }
            else if (!string.IsNullOrEmpty(key))
            {
                withWhere = " and (DossierTypeDisplayMember like '%" + key + "%')";
                direction = false;
                levelNum = "";
                isOpen = true;
                isLeaf = "";
            }
            else if (!string.IsNullOrEmpty(pid))
            {
                withWhere = " and DossierParentMember = '" + pid.ToString() + "'";
            }

            if (string.IsNullOrEmpty(withWhere))
                withWhere = " and DossierParentMember is NULL ";
            where += levelNum;


            DataSet ds = bll.GetTreeList(where, withWhere, direction, values);
            string datetype = Request["datetype"];
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (!string.IsNullOrEmpty(datetype) && datetype == "tree")
                {
                    string resultJson = dt.ToTreeJsonAll("DossierTypeValueMember", "DossierParentMember", "DossierTypeDisplayMember", "");
                    return resultJson;
                }
                dt.Columns["ICONCLS"].ColumnName = "icon";
             if(Request["type"] != null && !string.IsNullOrEmpty(Request["type"]) && Request["type"] == "t")
                 return new TreeJson(dt, "DossierTypeValueMember", "DossierTypeDisplayMember", "DossierParentMember", isLeaf, "", string.IsNullOrEmpty(pid) ? "" : pid, isOpen, true).ResultJson.ToString();
             else                
                return "{\"Rows\":" + new TreeJson(dt, "DossierTypeValueMember", "DossierTypeDisplayMember", "DossierParentMember", isLeaf, "", string.IsNullOrEmpty(pid) ? "" : pid, isOpen, true).ResultJson.ToString() + "}";
            }
            return "";
        }
        #endregion

        #region 根据编号获取数据
        /// <summary>
        /// 根据编号获取数据
        /// </summary>
        /// <returns></returns>
        private string GetModelOrParentList()
        {
            string DossierTypeValueMember = Request["id"];
            if (string.IsNullOrEmpty(DossierTypeValueMember))
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.卷宗模板配置Web, "根据编号获取模板配置参数失败", Request.Form.Get("name"), UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.error, "参数错误", null);
            }
            XY_DZJZ_MBPZB bll = new XY_DZJZ_MBPZB(this.Request);

            EDRS.Model.XY_DZJZ_MBPZB model = bll.GetModel(DossierTypeValueMember);
            if (model != null)
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.卷宗模板配置Web, "根据编号获取模板配置成功", Request.Form.Get("name"), UserInfo, UserRole, this.Request);
                return EDRS.Common.JsonHelper.JsonString(model);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.卷宗模板配置Web, "根据编号获取模板配置失败", Request.Form.Get("name"), UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "获取数据失败", null);
        }
        #endregion

        #region 表单验证
        /// <summary>
        /// 表单验证
        /// </summary>
        private bool ProvingFrom(ref string msg)
        {
            string mc = Request.Form.Get("txt_name").Trim();
            string ajlb = Request.Form.Get("tree_ajtype_val").Trim(); //案件类别
            string sslb = Request.Form.Get("tree_sslb_val").Trim(); //案件类别
            string ajlbmc = Request.Form.Get("tree_ajtype").Trim(); //案件类别名称
            string pbm = Request.Form.Get("key_parent").Trim(); //父级编码
            string dwbm = Request.Form.Get("tree_dwbm").Trim(); //单位编码
            string pd = Request.Form.Get("txt_rank");//排序编码


            if (string.IsNullOrEmpty(ajlb))
            {
                msg = ((VersionName)0).ToString()+ "类别不允许为空！";
                return false;
            }
            if (string.IsNullOrEmpty(sslb))
            {
                msg = "所属类别不允许为空！";
                return false;
            }
            if (string.IsNullOrEmpty(mc) || mc.Length > 150)
            {
                msg = "名称最多输入150英文字母或汉子。";
                return false;
            }
          
            return true;
        }
        #endregion 
       
        #region 表单验证
        /// <summary>
        /// 表单验证
        /// </summary>
        private bool ProvingUpFrom(ref string msg)
        {
            string mc = Request.Form.Get("txt_name_up").Trim();
            string ajlb = Request.Form.Get("tree_ajtype_up_val").Trim(); //案件类别
            string sslb = Request.Form.Get("tree_ajtype_up_val").Trim(); //所属类别

            if (string.IsNullOrEmpty(sslb))
            {
                msg = "所属类别不允许为空！";
                return false;
            }
            if (string.IsNullOrEmpty(ajlb))
            {
                msg = ((VersionName)0).ToString()+ "类别不允许为空！";
                return false;
            }
            if (string.IsNullOrEmpty(mc) || mc.Length > 150)
            {
                msg = "名称最多输入150英文字母或汉子。";
                return false;
            }
            return true;
        }
        #endregion 
    }
}