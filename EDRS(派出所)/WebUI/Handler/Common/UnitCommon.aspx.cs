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
using System.Diagnostics.Contracts;

namespace WebUI
{
    public partial class UnitCommon : BasePage
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
                if (type.Equals("GetTree"))
                    Response.Write(GetTree());
                if (type.Equals("GetTreeChil"))
                    Response.Write(GetTreeChildren());
                if (type.Equals("AddData"))
                    Response.Write(AddData());
                if (type.Equals("UpData"))
                    Response.Write(UpData());
                if (type.Equals("DelData"))
                    Response.Write(DelData());
                if (type.Equals("GetModelPList"))
                    Response.Write(GetModelOrParentList());

                if (type.Equals("GetTreeDW"))
                    Response.Write(ListBindDW());
                Response.End();
            }
            
        }
        #region 单位管理


        #region 绑定数据列表
        /// <summary>
        /// 绑定数据列表
        /// </summary>
        /// <returns></returns>
        private string ListBindDW()
        {
            //数据查询条件
            string where = string.Empty;
            object[] values = new object[3];
            where += " and SFSC=:SFSC";            
            values[0] = "N";
            string key = Request["key"];
            if (!string.IsNullOrEmpty(key))
            {
                where += " and (DWMC like :DWMC or DWJC like :DWJC)";
                values[1] = "%" + key + "%";
                values[2] = "%" + key + "%";
            }
            string levelNumber = "3";
            string level = Request["level"];
            if (level != null && !string.IsNullOrEmpty(level) && level != "0")
            {
                levelNumber = level;                
            }
            

            //树形循环条件
            bool direction = true;
            bool isOpen = false;
            string withWhere = string.Empty;
            string levelNum = " and level < " + levelNumber;
            bool isNameAll = false;
            string pid = Request["pid"];
            string parentid = "";
            XT_ZZJG_DWBM bll = new XT_ZZJG_DWBM(this.Request);
            if (!string.IsNullOrEmpty(pid))
                withWhere = (string.IsNullOrEmpty(withWhere) ? " 1=1 " : withWhere) + " and FDWBM = '" + pid + "'";
            else if (base.UserInfo != null) //判断根据登录用户进行筛选数据            
            {
                withWhere = (string.IsNullOrEmpty(withWhere) ? " 1=1 " : withWhere) + " and DWBM='" + base.UserInfo.DWBM + "' ";
                if (string.IsNullOrEmpty(pid))
                {
                    EDRS.Model.XT_ZZJG_DWBM model = bll.GetModel(base.UserInfo.DWBM);
                    if (model != null)
                    {
                        pid = model.FDWBM;
                        parentid = pid;
                    }
                    else
                        pid = base.UserInfo.DWBM;
                }
            }
            //根据搜索名称查询节点            
            string treeText = Request["treeText"];
            if (!string.IsNullOrEmpty(treeText))
            {
                withWhere = (string.IsNullOrEmpty(withWhere) ? " 1=1 " : withWhere)+ " and DWMC like '%" + treeText + "%'";
                direction = false;
                levelNum = "";
                isOpen = true;
            }

            if (Request["bmbm"] != null && Request["jsbm"] != null && !string.IsNullOrEmpty(Request["bmbm"]) && !string.IsNullOrEmpty(Request["jsbm"]))
            {
                where += " AND DWBM not IN (SELECT distinct qxbm FROM xt_dm_qx WHERE trim(JSBM) = '" + Request["jsbm"].Replace("a", "") + "' AND TRIM(DWBM)='" + base.UserInfo.DWBM + "' AND TRIM(BMBM)='" + Request["bmbm"].Replace("a", "") + "' and QXLX='0')";
            }


            if (string.IsNullOrEmpty(withWhere))
                withWhere = " FDWBM is NULL ";


            where += levelNum;
            try
            {
                DataSet ds = bll.GetTreeList(where, withWhere, direction, values);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    dt.Columns["DWBM"].ColumnName = "ID";
                    dt.Columns["FDWBM"].ColumnName = "PARENTID";
                    dt.Columns["DWMC"].ColumnName = "NAME";

                    return new TreeJson(dt, "ID", "NAME", "PARENTID", "ISLEAF", parentid, string.IsNullOrEmpty(pid) ? "" : pid, isOpen, isNameAll).ResultJson.ToString();
                }
                return ReturnString.JsonToString(Prompt.error, "未找到单位的数据", null);
            }
            catch (Exception ex)
            {
                return ReturnString.JsonToString(Prompt.error, ex.Message, null);
            }
            
        }
        #endregion



        #region 添加角色数据
        /// <summary>
        /// 添加角色数据
        /// </summary>
        /// <returns></returns>
        private string AddData()
        {
            string msg = string.Empty;
            if (!ProvingFrom(ref msg))
                return ReturnString.JsonToString(Prompt.error, msg, null);
            string number = "001";
            
            XT_ZZJG_BMBM bllBM = new XT_ZZJG_BMBM(this.Request);
            EDRS.Model.XT_ZZJG_BMBM modelBM = bllBM.GetModel("",Request.Form.Get("hidd_unitNumber").Trim());
            if (modelBM == null)
                return ReturnString.JsonToString(Prompt.error, "选择部门不是有效部门，请重新选择", null);

            XT_QX_JSBM bll = new XT_QX_JSBM(this.Request);
            //获取角色数据编号中最大的一个编号
            EDRS.Model.XT_QX_JSBM modelJS = bll.GetListOrderModel(" and DWBM='" + modelBM.DWBM + "' and BMBM='" + modelBM.BMBM + "' and rownum < 2","JSBM desc",null);
            if (modelJS != null)
            {
                //将数据中最大编号加1表示自增防止编号重复
                number = (int.Parse(modelJS.JSBM.ToString()) + 1).ToString();
                int len = 3 - number.Length;
                //字符长度未到3在数字前面自动加0
                for (int i = 0; i < len; i++)
                    number = number.Insert(0, "0");
            }

            EDRS.Model.XT_QX_JSBM model = new EDRS.Model.XT_QX_JSBM();
            model.DWBM = modelBM.DWBM;
            model.BMBM = modelBM.BMBM;
            model.JSBM = number;            
            model.JSMC = Request.Form.Get("txt_name").Trim();
            model.JSXH = Convert.ToDecimal(Request.Form.Get("txt_number").Trim());
            model.SPJSBM = "";
            if (bll.Add(model))
                return ReturnString.JsonToString(Prompt.win, "保存成功", null);
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

            string JSBM = Request.Form.Get("key_hidd");
            string BMBM = Request.Form.Get("key_hidd_dw");
            string DWBM = Request.Form.Get("hidd_unitNumber");
            if (string.IsNullOrEmpty(JSBM) || string.IsNullOrEmpty(BMBM) || string.IsNullOrEmpty(DWBM))
                return ReturnString.JsonToString(Prompt.error, "参数错误", null);
                       
            string msg = string.Empty;
            //if (!ProvingFrom(ref msg))
            //    return ReturnString.JsonToString(Prompt.error, msg, null);

            XT_QX_JSBM bll = new XT_QX_JSBM(this.Request);
            EDRS.Model.XT_QX_JSBM model = bll.GetModel(JSBM, BMBM, DWBM);
            if (model != null)
            {
                model.JSMC = Request.Form.Get("txt_name").Trim();
                model.JSXH = Convert.ToDecimal(Request.Form.Get("txt_number").Trim());                
                if (bll.Update(model))
                    return ReturnString.JsonToString(Prompt.win, "保存成功", null);
                return ReturnString.JsonToString(Prompt.error, "保存失败", null);
            }
            else
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

            XT_QX_JSBM bll = new XT_QX_JSBM(this.Request);
           
                //if (bll.Delete(ids))
                //    return ReturnString.JsonToString(Prompt.win, "删除数据成功", null);
                //return ReturnString.JsonToString(Prompt.error, "删除数据失败", null);
            return "";
        }
        #endregion

        #region 绑定数据列表
        /// <summary>
        /// 绑定数据列表
        /// </summary>
        /// <returns></returns>
        private string ListBind()
        {
            int pageNumber = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]); 
            string where = string.Empty;
            object[] values = new object[2];
           
            string key = Request["key"];
            if (!string.IsNullOrEmpty(key))
            {
                where += " and JSMC like :JSMC";
                values[0] = "%" + key + "%";
            }
            if (!string.IsNullOrEmpty(Request["dkey"]))
            {
                where += " and BMBM in (SELECT distinct ID FROM view_dw_bm START WITH trim(ID) = :ID CONNECT BY nocycle Parentid = PRIOR ID)";
                values[1] = Request["dkey"].ToString();
            }
            XT_QX_JSBM bll = new XT_QX_JSBM(this.Request);

            DataSet ds = bll.GetListByPageAlly(where, "", (pageSize * pageNumber) - pageSize + 1, (pageSize * pageNumber), values);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                int count = bll.GetRecordCount(where, values);
                return "{\"total\":" + count + ",\"rows\":" + JsonHelper.JsonString(ds.Tables[0]) + "}";
            }
            return ReturnString.JsonToString(Prompt.error, "未设置角色", null); ;
        }
        #endregion

        #region 绑定树形结构为单位部门
        /// <summary>
        /// 绑定树形结构为单位部门
        /// </summary>
        /// <returns></returns>
        private string GetTree()
        {
            string bm = " and lx='dw'";
            string where = string.Empty;
            object[] values = new object[2];           
            string key = Request["key"];
            if (key != null && !string.IsNullOrEmpty(key))
            {
                where += " and NAME like :NAME";
                values[0] = "%" + key + "%";
            }
            string id = Request["id"];
            if (id!= null && !string.IsNullOrEmpty("id"))
            {
                bm = "";
                where += " and PARENTID=:PARENTID";
                values[1] = id;
            }
            //if (!string.IsNullOrEmpty(Request["dkey"]))
            //    where += " and DWBM in (" + Request["dkey"].ToString() + ")";
            XT_ZZJG_BMBM bll = new XT_ZZJG_BMBM(this.Request);

            DataSet ds = bll.GetOrganization(bm + where, values);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                //DataTable dt = ds.Tables[0];
                

                //GetTreeJsonByTable(dt, "ID", "NAME", "PARENTID", "");
                //string j = result.ToString();
                //return j.Replace("[", "").Remove(j.Length-1,1);
                return "{\"total\":0,\"rows\":" + JsonHelper.JsonString(ds.Tables[0]) + "}";
            }
            return ReturnString.JsonToString(Prompt.error, "该单位还未设置部门", null);
        }
        #endregion

        private string GetTreeChildren()
        {
            XT_ZZJG_BMBM bll = new XT_ZZJG_BMBM(this.Request);
            string where = string.Empty;
            object[] values = new object[1];
            string id = Request["id"];
            if (id != null && !string.IsNullOrEmpty("id"))
            {
                where += " and PARENTID=:PARENTID";
                values[0] = id;
            }
            DataSet ds = bll.GetTreeChildren(where, values);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                return "{\"total\":0,\"rows\":" + JsonHelper.JsonString(ds.Tables[0]) + "}";
            return ReturnString.JsonToString(Prompt.error, "该单位还未设置部门", null);
        }



        #region 根据编号获取数据
        /// <summary>
        /// 根据编号获取数据
        /// </summary>
        /// <returns></returns>
        private string GetModelOrParentList()
        {
            string JSBM = Request["id"];
            string BMBM = Request["bmid"];
            string DWBM = Request["dwid"];
            if (string.IsNullOrEmpty(JSBM) || string.IsNullOrEmpty(BMBM) || string.IsNullOrEmpty(DWBM))
                return ReturnString.JsonToString(Prompt.error, "参数错误", null);

            XT_QX_JSBM bll = new XT_QX_JSBM(this.Request);

            EDRS.Model.XT_QX_JSBM model = bll.GetModel(JSBM, DWBM, BMBM);
            if (model != null)
                return JsonHelper.JsonString(model);
            return ReturnString.JsonToString(Prompt.error, "获取数据失败", null);
        }
        #endregion

        #region 表单验证
        /// <summary>
        /// 表单验证
        /// </summary>
        private bool ProvingFrom(ref string msg)
        {
            string dwmc = Request.Form.Get("txt_name").Trim();
            
            if (string.IsNullOrEmpty(dwmc) || !Regex.IsMatch(dwmc, @"^\w{1,30}$"))
            {
                msg = "名称最多输入30英文字母或汉子。";
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