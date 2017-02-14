using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using EDRS.BLL;
using Cyvation.CCQE.Web;
using System.Data;
using EDRS.Common;

namespace WebUI.Handler.Common
{
    /// <summary>
    /// UnitCommonHandler 的摘要说明
    /// </summary>
    public class UnitCommonHandler : AshxBase
    {

        public override void ProcessRequest(HttpContext context)
        {
            
            base.ProcessRequest(context);
            if (UserInfo == null) 
                return;
            
            string type = context.Request["t"];

            if (!string.IsNullOrEmpty(type))
            {
                context.Response.ContentEncoding = Encoding.UTF8;
                context.Response.ContentType = "application/json";
                              
                if (type.Equals("GetTreeDW"))
                    context.Response.Write(ListBindDW(context));
                if (type.Equals("ListBindDWAll"))
                    context.Response.Write(ListBindDWAll(context));

                context.Response.End();
            }
        }
        
        #region 绑定数据列表
        /// <summary>
        /// 绑定数据列表
        /// </summary>
        /// <returns></returns>
        private string ListBindDW(HttpContext context)
        {
            //数据查询条件
            string where = string.Empty;
            object[] values = new object[3];
            where += " and SFSC=:SFSC";            
            values[0] = "N";
            string key = context.Request["key"];
            if (!string.IsNullOrEmpty(key))
            {
                where += " and (DWMC like :DWMC or DWJC like :DWJC)";
                values[1] = "%" + key + "%";
                values[2] = "%" + key + "%";
            }
            string levelNumber = "3";
            string level = context.Request["level"];
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
            string pid = context.Request["pid"];
            string parentid = "";
            XT_ZZJG_DWBM bll = new XT_ZZJG_DWBM(context.Request);
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
            string treeText = context.Request["treeText"];
            if (!string.IsNullOrEmpty(treeText))
            {
                withWhere = (string.IsNullOrEmpty(withWhere) ? " 1=1 " : withWhere)+ " and DWMC like '%" + treeText + "%'";
                direction = false;
                levelNum = "";
                isOpen = true;
            }

            if (context.Request["bmbm"] != null && context.Request["jsbm"] != null && !string.IsNullOrEmpty(context.Request["bmbm"]) && !string.IsNullOrEmpty(context.Request["jsbm"]))
            {
                where += " AND DWBM not IN (SELECT distinct qxbm FROM xt_dm_qx WHERE trim(JSBM) = '" + context.Request["jsbm"].Replace("a", "") + "' AND TRIM(DWBM)='" + base.UserInfo.DWBM + "' AND TRIM(BMBM)='" + context.Request["bmbm"].Replace("a", "") + "' and QXLX='0')";
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

        #region 绑定数据列表
        /// <summary>
        /// 绑定数据列表
        /// </summary>
        /// <returns></returns>
        private string ListBindDWAll(HttpContext context)
        {
            //数据查询条件
            string where = string.Empty;
            object[] values = new object[3];
            where += " and SFSC=:SFSC";
            values[0] = "N";
            string key = context.Request["key"];
            if (!string.IsNullOrEmpty(key))
            {
                where += " and (DWMC like :DWMC or DWJC like :DWJC)";
                values[1] = "%" + key + "%";
                values[2] = "%" + key + "%";
            }
            //树形循环条件
            bool direction = true;
            bool isOpen = false;
            string withWhere = " FDWBM is NULL ";
            string levelNum = " and level <= 3 ";
            bool isNameAll = false;
            string pid = context.Request["pid"];
            if (!string.IsNullOrEmpty(pid))
                withWhere = " FDWBM = '" + pid + "'";

            //根据搜索名称查询节点            
            string treeText = context.Request["treeText"];
            if (!string.IsNullOrEmpty(treeText))
            {
                withWhere = " DWMC like '%" + treeText + "%'";
                direction = false;
                levelNum = "";
                isOpen = true;
            }

            where += levelNum;
            try
            {
                XT_ZZJG_DWBM bll = new XT_ZZJG_DWBM(context.Request);
                DataSet ds = bll.GetTreeList(where, withWhere, direction, values);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    dt.Columns["DWBM"].ColumnName = "ID";
                    dt.Columns["FDWBM"].ColumnName = "PARENTID";
                    dt.Columns["DWMC"].ColumnName = "NAME";

                    return new TreeJson(dt, "ID", "NAME", "PARENTID", "ISLEAF", "", string.IsNullOrEmpty(pid) ? "" : pid, isOpen, isNameAll).ResultJson.ToString();
                }
                return ReturnString.JsonToString(Prompt.error, "未找到单位的数据", null);
            }
            catch (Exception ex)
            {
                return ReturnString.JsonToString(Prompt.error, ex.Message, null);
            }
        }
        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}