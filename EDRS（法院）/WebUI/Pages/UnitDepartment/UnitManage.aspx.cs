using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDRS.BLL;
using System.Data;
using EDRS.Common;
using System.Text;
using System.Text.RegularExpressions;

namespace WebUI
{
    public partial class UnitManage : BasePage
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
                if (type.Equals("UpData"))
                    Response.Write(UpData());
                if (type.Equals("DelData"))
                    Response.Write(DelData());
                if (type.Equals("GetModelPList"))
                    Response.Write(GetModelOrParentList(""));
                Response.End();
            }
            
        }
        #region 单位管理

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
                return ReturnString.JsonToString(Prompt.error, msg, null);
            }

            EDRS.Model.XT_ZZJG_DWBM model = new EDRS.Model.XT_ZZJG_DWBM();
            model.FDWBM = Request.Form.Get("hidd_superiorNumber").Trim();
            model.DWMC = Request.Form.Get("txt_name").Trim();
            model.DWJC = Request.Form.Get("txt_abbreviation").Trim();
            model.DWBM = Request.Form.Get("txt_number").Trim();
            model.DWJB = Request.Form.Get("txt_rank").Trim();

            XT_ZZJG_DWBM bll = new XT_ZZJG_DWBM(this.Request);
            if (bll.Add(model))
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.单位管理Web, "添加单位成功",model.DWMC , UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.win, "保存成功", null);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.单位管理Web, "添加单位失败",Request.Form.Get("txt_name"), UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "保存失败，请先确认单位编码是否已存在", null);

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
            string dwbm = Request.Form.Get("key_hidd");
            if (string.IsNullOrEmpty(dwbm) || !Regex.IsMatch(dwbm.Trim(), @"^[A-Za-z0-9]{1,50}$"))
                return ReturnString.JsonToString(Prompt.error, "未找到需要修改信息", null);
            string msg = string.Empty;
            if (!ProvingFrom(ref msg))
            {
                return ReturnString.JsonToString(Prompt.error, msg, null);
            }
            XT_ZZJG_DWBM bll = new XT_ZZJG_DWBM(this.Request);
            EDRS.Model.XT_ZZJG_DWBM model = bll.GetModel(dwbm.Trim());
            if (model != null)
            {
                model.FDWBM = Request.Form.Get("hidd_superiorNumber").Trim();
                model.DWMC = Request.Form.Get("txt_name").Trim();
                model.DWJC = Request.Form.Get("txt_abbreviation").Trim();
                //model.DWBM = Request.Form.Get("txt_number").Trim();
                model.DWJB = Request.Form.Get("txt_rank").Trim();

                if (bll.Update(model))
                {
                    //数据日志
                    OperateLog.AddLog(OperateLog.LogType.单位管理Web, "修改单位成功",model.DWMC, UserInfo, UserRole, this.Request);
                    return ReturnString.JsonToString(Prompt.win, "保存成功", null);
                }
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.单位管理Web, "修改单位失败", model.DWMC, UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.error, "保存失败", null);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.单位管理Web, "修改单位未找到修改信息", Request.Form.Get("txt_name"), UserInfo, UserRole, this.Request);
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
            string ids = Request.Form["DWBM"];
            string[] id = ids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            ids = "";
            for (int i = 0; i < id.Length; i++)
            {
                ids += "'" + id[i].Trim() + "'";
                if (i < id.Length - 1)
                    ids += ",";
            }
            XT_ZZJG_DWBM bll = new XT_ZZJG_DWBM(this.Request);
            //string where = " and SFSC='N' and FDWBM in (" + ids + ")";
            //if (bll.GetRecordCount(" and SFSC='N' and FDWBM in (" + ids + ")") == 0)
            if (bll.GetDwbmCount(ids) == 0)
            {
               // if (bll.DeleteListLogic(ids))
                if(bll.DeleteList(ids))
                {
                    //数据日志
                    OperateLog.AddLog(OperateLog.LogType.单位管理Web, "删除数据成功", Request.Form["DWMC"], UserInfo, UserRole, this.Request);
                    return ReturnString.JsonToString(Prompt.win, "删除数据成功", null);
                }
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.单位管理Web, "删除数据失败", Request.Form["DWMC"], UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.error, "删除数据失败", null);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.单位管理Web, "该单位已被使用，不能删除", Request.Form["DWMC"], UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "该单位已被使用，不能删除！", null);
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
            string levelNum = " and level < " + (Request["level"] == null ? 3 : int.Parse(Request["level"].ToString()));
            string isLeaf = "ISLEAF";
            string parentid = "";
            XT_ZZJG_DWBM bll = new XT_ZZJG_DWBM(this.Request);
            object[] values = new object[1];
            where += " and SFSC=:SFSC";
            values[0] = "N";
            //关键字搜索
            string key = Request["key"];
            //获取父级节点
            string pid = "";
            if (base.UserInfo != null) //判断根据登录用户进行筛选数据            
            {
                withWhere = " DWBM='" + UserInfo.DWBM + "' ";
                if (string.IsNullOrEmpty(pid))
                {
                    EDRS.Model.XT_ZZJG_DWBM model = bll.GetModel(UserInfo.DWBM);
                    if (model != null)
                    {
                        pid = model.FDWBM;
                        parentid = pid;
                    }
                    else
                        pid = UserInfo.DWBM;
                }
            }
            if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(Request["pid"]))
            {
                    pid = Request["pid"];
                    withWhere = " (DWMC like '%" + StringPlus.ReplaceSingle(key) + "%' or DWJC like '%" + StringPlus.ReplaceSingle(key) + "%') and FDWBM = '" + pid + "'";
            }
            else if (!string.IsNullOrEmpty(key))
            {
                pid = UserInfo.DWBM;
                withWhere = " (DWMC like '%" + StringPlus.ReplaceSingle(key) + "%' or DWJC like '%" + StringPlus.ReplaceSingle(key) + "%') and FDWBM = '" + pid + "'";
                direction = true;
                levelNum = "";
                isOpen = true;
                isLeaf = "";
            }
            else if (!string.IsNullOrEmpty(Request["pid"]))
            {
                pid = Request["pid"];
                withWhere = "  FDWBM = '" + StringPlus.ReplaceSingle(Request["pid"].ToString()) + "'";
            }

            if (string.IsNullOrEmpty(withWhere))
                withWhere = " FDWBM is NULL ";
            where += levelNum;

            
            DataSet ds = bll.GetTreeList(where, withWhere, direction, values);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
               
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.单位管理Web, "获取单位列表", UserInfo, UserRole, this.Request);
                return "{\"Rows\":" + new TreeJson(dt, "DWBM", "DWMC", "FDWBM", isLeaf, parentid, string.IsNullOrEmpty(pid) ? "" : pid, isOpen, true).ResultJson.ToString() + ",\"Total\":100}";
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.单位管理Web, "单位列表未找到数据", UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "未找到单位信息！", null);
        }
        #endregion

        #region 根据编号获取数据
        /// <summary>
        /// 根据编号获取数据
        /// </summary>
        /// <returns></returns>
        private string GetModelOrParentList(string DWBM)
        {
            if (string.IsNullOrEmpty(DWBM))
            {
                DWBM = Request["id"];
                if (string.IsNullOrEmpty(DWBM))
                {
                    return ReturnString.JsonToString(Prompt.error, "参数错误", null);
                }
            }
           
            XT_ZZJG_DWBM bll = new XT_ZZJG_DWBM(this.Request);
           
            EDRS.Model.XT_ZZJG_DWBM model = bll.GetModel(DWBM);
            if (model != null)
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.单位管理Web, "根据编号获取单位成功",Request["dwmc"], UserInfo, UserRole, this.Request);
                return JsonHelper.JsonString(model);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.单位管理Web, "根据编号获取单位失败", Request["dwmc"], UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "获取数据失败", null);
        }
        #endregion

        #region 表单验证
        /// <summary>
        /// 表单验证
        /// </summary>
        private bool ProvingFrom(ref string msg)
        {
            string fdwbm = Request.Form.Get("hidd_superiorNumber").Trim();
            string dwmc = Request.Form.Get("txt_name").Trim();
            string dwjc = Request.Form.Get("txt_abbreviation").Trim();
            string dwbm = Request.Form.Get("txt_number").Trim();
            string dwjb = Request.Form.Get("txt_rank").Trim();

            if (string.IsNullOrEmpty(dwbm) || !Regex.IsMatch(dwbm, @"^[A-Za-z0-9]{1,50}$"))
            {
                msg = "编号中只能包含1-50位英文字母或数字。";
                return false;
            }
            if (!string.IsNullOrEmpty(fdwbm) && !Regex.IsMatch(fdwbm, @"^[A-Za-z0-9]{1,50}$"))
            {
                msg = "父编号中只能包含1-50位英文字母或数字。";
                return false;
            }
            if (string.IsNullOrEmpty(dwmc) || !Regex.IsMatch(dwmc, @"^\w{1,150}$"))
            {
                msg = "名称最多输入150英文字母或汉子。";
                return false;
            }
            if (!string.IsNullOrEmpty(dwjc) && !Regex.IsMatch(dwjc, @"^\w{1,30}$"))
            {
                msg = "简称最多输入150英文字母或汉子。";
                return false;
            }
            return true;
        }
        #endregion 
        #endregion

        #region 添加部门数据
        /// <summary>
        /// 添加部门数据
        /// </summary>
        /// <returns></returns>
        private string AddDepartmentData()
        {
            string msg = string.Empty;
            if (!ProvingFrom(ref msg))
                return ReturnString.JsonToString(Prompt.error, msg, null);

            EDRS.Model.XT_ZZJG_BMBM model = new EDRS.Model.XT_ZZJG_BMBM();
            model.DWBM = Request.Form.Get("hidd_superiorNumber");
            model.BMBM = Request.Form.Get("txt_name");
            model.FBMBM = Request.Form.Get("txt_abbreviation");
            model.BMMC = Request.Form.Get("txt_number");
            model.BMJC = Request.Form.Get("txt_rank");
            model.BMAHJC = "";
            model.BMWHJC = "";
            model.SFLSJG = "";
            model.SFCBBM = "";
            model.BMXH = 0;
            model.BMYS = "";

            XT_ZZJG_BMBM bll = new XT_ZZJG_BMBM(this.Request);
            if (bll.Add(model))
                return ReturnString.JsonToString(Prompt.win, "保存成功", null);
            return ReturnString.JsonToString(Prompt.error, "保存失败", null);
        }
        #endregion
    }
}