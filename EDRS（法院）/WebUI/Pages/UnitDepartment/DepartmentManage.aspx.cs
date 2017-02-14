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
    public partial class DepartmentManage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

            string[] t = Request.PathInfo.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

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
                    Response.Write(GetModelOrParentList("",""));
                Response.End();
            }
            
        }
        #region 单位管理

        #region 添加部门数据
        /// <summary>
        /// 添加部门数据
        /// </summary>
        /// <returns></returns>
        private string AddData()
        {
            string msg = string.Empty;
            //if (!ProvingFrom(ref msg))
            //    return ReturnString.JsonToString(Prompt.error, msg, null);
            string number = "0001";

            XT_ZZJG_BMBM bll = new XT_ZZJG_BMBM(this.Request);
            EDRS.Model.XT_ZZJG_BMBM modelBM = bll.GetListOrderModel(" and rownum < 2", "BMBM desc", null);
            if (modelBM!= null)
            {
                number = (int.Parse(modelBM.BMBM.ToString()) + 1).ToString();
                int len = 4- number.Length;
                for (int i = 0; i < len; i++)
                    number=number.Insert(0, "0");
            }
            EDRS.Model.XT_ZZJG_BMBM model = new EDRS.Model.XT_ZZJG_BMBM();
            model.DWBM = Request.Form.Get("hidd_unitNumber").Trim();
            model.BMBM = number;
            model.FBMBM = Request.Form.Get("hidd_superiorNumber").Trim();
            model.BMMC = Request.Form.Get("txt_name").Trim();
            model.BMJC = Request.Form.Get("txt_abbreviation").Trim();
            model.BMAHJC ="";
            model.BMWHJC ="";
            model.SFLSJG ="N";
            model.SFCBBM = "0";
            model.BMXH = 0;
            model.BZ = Request.Form.Get("txt_remark").Trim();
            model.BMYS = "";
            if (bll.Add(model))
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.部门管理Web, "添加部门成功", model.BMMC, UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.win, "保存成功", null);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.部门管理Web, "添加部门失败",model.BMMC, UserInfo, UserRole, this.Request);
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
            string id = Request.Form.Get("key_hidd");
            string uid = Request.Form.Get("hidd_unitNumber").Trim();
            if (string.IsNullOrEmpty(id) || !Regex.IsMatch(id, @"^[A-Za-z0-9]{1,8}$"))
                return ReturnString.JsonToString(Prompt.error, "未找到需要修改信息", null);
            if (string.IsNullOrEmpty(uid) || !Regex.IsMatch(id, @"^[A-Za-z0-9]{1,8}$"))
                return ReturnString.JsonToString(Prompt.error, "未找到需要修改信息", null);
            string msg = string.Empty;
            //if (!ProvingFrom(ref msg))
            //    return ReturnString.JsonToString(Prompt.error, msg, null);

            XT_ZZJG_BMBM bll = new XT_ZZJG_BMBM(this.Request);
            EDRS.Model.XT_ZZJG_BMBM model = bll.GetModel(uid, id);
            if (model != null)
            {
                model.BMMC = Request.Form.Get("txt_name").Trim();
                model.BMJC = Request.Form.Get("txt_abbreviation").Trim();
                //model.BMAHJC = Request.Form.Get("txt_abbreviationNum1").Trim();
                //model.BMWHJC = Request.Form.Get("txt_abbreviationNum2").Trim();
                //model.SFLSJG = Request.Form.Get("txt_temporary") == null ? "N" : "Y";
                //model.SFCBBM = Request.Form.Get("txt_undertake") == null ? "0" : "1";
                //model.BMXH = Convert.ToDecimal(Request.Form.Get("txt_number").Trim());
                model.BZ = Request.Form.Get("txt_remark").Trim();

                if (bll.Update(model))
                {
                    //数据日志
                    OperateLog.AddLog(OperateLog.LogType.部门管理Web, "修改部门成功",model.BMMC, UserInfo, UserRole, this.Request);
                    return ReturnString.JsonToString(Prompt.win, "保存成功", null);
                }
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.部门管理Web, "修改部门失败",model.BMMC, UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.error, "保存失败", null);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.部门管理Web, "未找到修改部门信息", Request.Form.Get("txt_name"), UserInfo, UserRole, this.Request);
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
            string ids = Request.Form["BMBM"];
            string dwbm = Request.Form["DWBM"];
            string[] id = ids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            ids = "";
            for (int i = 0; i < id.Length; i++)
            {
                ids += "'" + id[i].Trim() + "'";
                if (i < id.Length - 1)
                    ids += ",";
            }
            XT_ZZJG_BMBM bll = new XT_ZZJG_BMBM(this.Request);
            //string where = " and SFSC='N' and FBMBM in (" + ids + ")";
            //if (bll.GetRecordCount(" and SFSC='N' and FBMBM in (" + ids + ")") == 0)
            if(bll.GetBmbmCount(dwbm,ids)==0)
            {
                if (!string.IsNullOrEmpty(dwbm) && bll.DeleteListLogic(ids,"'"+dwbm+"'"))
                {
                    //数据日志
                    OperateLog.AddLog(OperateLog.LogType.部门管理Web, "删除部门成功", Request.Form["bmmc"], UserInfo, UserRole, this.Request);
                    return ReturnString.JsonToString(Prompt.win, "删除数据成功", null);
                }
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.部门管理Web, "删除部门失败", Request.Form["bmmc"], UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.error, "删除数据失败", null);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.部门管理Web, "未找到删除部门信息",Request.Form["bmmc"],UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "该部门正在被使用，无法删除！", null);
        }
        #endregion

        #region 绑定数据列表
        /// <summary>
        /// 绑定数据列表
        /// </summary>
        /// <returns></returns>
        private string ListBind()
        {
            string where = string.Empty;
            string withWhere = string.Empty;
            object[] values = new object[2];
            where += " and SFSC=:SFSC";
            values[0] = "N";
            string key = Request["key"];
            if (!string.IsNullOrEmpty(key))
            {
                withWhere = " (BMMC like '%" + StringPlus.ReplaceSingle(key) + "%' or BMJC like '%" + StringPlus.ReplaceSingle(key) + "%')";
            }
            else
                withWhere = " FBMBM is NULL";

            if (!string.IsNullOrEmpty(Request["dkey"]) || base.UserInfo != null)
            {
                where += " and DWBM = :DWBM ";
                if (Request["dkey"] != null && !string.IsNullOrEmpty(Request["dkey"]))
                    values[1] = Request["dkey"].ToString();
                else
                    values[1] = base.UserInfo.DWBM;
            }
            else
            {
                return ReturnString.JsonToString(Prompt.error, "查询条件不正确", null);
            }

            XT_ZZJG_BMBM bll = new XT_ZZJG_BMBM(this.Request);
            DataSet ds = bll.GetTreeList(where, withWhere, values);
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string sortname = Request.Params["sortname"];

                    string sortorder = Request.Params["sortorder"];

                    DataTable dt = ds.Tables[0].Copy();
                    DataView dv = dt.DefaultView;
                    if (!string.IsNullOrEmpty(sortname) && !string.IsNullOrEmpty(sortorder))
                        dv.Sort = sortname + " " + sortorder;
                    else
                        dv.Sort = "DWBM asc,BMBM asc";
                    dt = dv.ToTable();
                    //数据日志
                    OperateLog.AddLog(OperateLog.LogType.部门管理Web, "获取部门列表成功", UserInfo, UserRole, this.Request);
                    return "{\"Rows\":" + JsonHelper.JsonString(dt) + "}";
                }
                else
                    return "{\"Rows\":[]}";
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.部门管理Web, "获取部门列表失败", UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "获取部门列表出现错误", null);
        }
        #endregion

        #region 根据编号获取数据
        /// <summary>
        /// 根据编号获取数据
        /// </summary>
        /// <returns></returns>
        private string GetModelOrParentList(string DWBM, string BMBM)
        {
            if (string.IsNullOrEmpty(BMBM))
            {
                BMBM = Request["id"];
                if (string.IsNullOrEmpty(BMBM))
                    return ReturnString.JsonToString(Prompt.error, "参数错误", null);
                DWBM = Request["did"];
                if (string.IsNullOrEmpty(DWBM))
                    return ReturnString.JsonToString(Prompt.error, "参数错误", null);
            }

            XT_ZZJG_BMBM bll = new XT_ZZJG_BMBM(this.Request);

            EDRS.Model.XT_ZZJG_BMBM model = bll.GetModel(DWBM,BMBM);
            if (model != null)
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.部门管理Web, "根据部门编号获取部门成功",model.BMMC, UserInfo, UserRole, this.Request);
                return JsonHelper.JsonString(model);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.部门管理Web, "根据部门编号获取部门失败", Request["bmmc"], UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "获取数据失败", null);
        }
        #endregion

       
        #endregion

    }
}