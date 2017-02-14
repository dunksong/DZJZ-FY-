using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Data;
using Cyvation.CCQE.Web;
using System.Collections;

namespace WebUI.Handler.ZZJG
{
    /// <summary>
    /// LigerUIDate 的摘要说明
    /// </summary>
    public class LigerUIDate : AshxBase 
    {
        /// <summary>
        /// 记录被使用的row
        /// </summary>
        private List<string> rowsList;

        public override void ProcessRequest (HttpContext context) {
            base.ProcessRequest(context);
            if (base.UserInfo == null)
                return;
            if (context.Request["action"] == "GetAllDw")
            {
                GetAllDwBm(context);
            }
            else
            {
                GetDwBmByDw(context);
            }
        }

        private void GetAllDwBm(HttpContext context)
        {
            //数据查询条件
            string where = string.Empty;
            string pdwbm = base.UserInfo.DWBM;
            object[] values;
            values = new object[1];
            where += " AND SFSC=:SFSC ";
            values[0] = "N";
            try
            {
                EDRS.BLL.XT_DM_QX bll = new EDRS.BLL.XT_DM_QX(context.Request);
                DataSet ds = bll.GetTreeList(where, "", values);
                List<object> value = new List<object>();

                DataRow[] drs = ds.Tables[0].Select("FDWBM is null");//顶级
                //遍历父级节点
                foreach (DataRow dr in drs)
                {
                    var d = new Dictionary<string, object>();
                    d["text"] = dr["dwmc"].ToString();
                    d["id"] = dr["dwbm"].ToString();
                    d["ischecked"] = true;
                    if (dr["dwbm"].ToString().Length == 6)
                    {
                        d["icon"] = "tree_dw";
                    }
                    else if (dr["dwbm"].ToString().Length == 4)
                    {
                        d["icon"] = "tree_bm";
                    }
                    else if (dr["dwbm"].ToString().Length == 3)
                    {
                        d["icon"] = "tree_js";
                    }
                    d["children"] = GetChild(dr["dwbm"].ToString(), ds);
                    value.Add(d);
                }
                context.Response.Write(new JavaScriptSerializer().Serialize(value));
            }
            catch (Exception ex)
            {

            }
        }
        private void GetDwBmByDw(HttpContext context)
        {
            string jsbm = context.Request.Params["jsbm"];
            string bmbm = context.Request.Params["bmbm"];
            string _dwbm = context.Request.Params["_dwbm"];//角色对应的单位编码
            string dwbm = context.Request.Params["dwbm"];//角色所有的单位的权限
            string key = context.Request.Params["key"];
            
            string type = context.Request.Params["type"];
            if (_dwbm != null)
            {
                _dwbm = _dwbm.Replace("a", "");
                bmbm = bmbm.Replace("a", "");
                jsbm = jsbm.Replace("a", "");
            }
            //数据查询条件
            string where = string.Empty;
            string pdwbm = base.UserInfo.DWBM;
            string fdwbm = base.UserDwbm.FDWBM;
            if (type == "all")
            {
                pdwbm = base.UserDwbm.FDWBM;
            }
            List<object> values = new List<object>();
            string strQxWhere = "";
            strQxWhere += " and trim(JSBM) = :JSBM AND TRIM(DWBM)=:DWBM1 AND TRIM(BMBM)=:BMBM";
            values.Add(jsbm);
            values.Add(_dwbm);
            values.Add(bmbm);
            where += " AND t1.SFSC=:SFSC ";
            if (string.IsNullOrEmpty(key))
            {
                where += " AND ( t1.DWBM=:DWBM or t1.FDWBM= :FDWBM)";
                values.Add("N");
                values.Add(pdwbm);
                values.Add(fdwbm);
            }
            else
            {
                where += " AND t1.DWMC like :DWMC";
                where += " AND (t1.DWBM='" + pdwbm + "' OR t1.FDWBM = '" + pdwbm + "' or t2.dwbm = '" + pdwbm + "' or t2.fdwbm = '" + pdwbm + "' or t1.fdwbm ='" + fdwbm + "' or t2.fdwbm='" + fdwbm + "')";
                values.Add("N");
                values.Add("%" + key + "%");
                //values[2] = pdwbm;
            }
            try
            {
                EDRS.BLL.XT_DM_QX bll = new EDRS.BLL.XT_DM_QX(context.Request);
                DataSet ds = bll.GetTreeList(where, strQxWhere, values.ToArray());
                rowsList = new List<string>();
                List<object> value = new List<object>();
                
                //DataRow[] drs = ds.Tables[0].Select("DWBM = " + base.UserInfo.DWBM);//顶级
                //遍历父级节点
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    var d = new Dictionary<string, object>();
                    d["text"] = dr["dwmc"].ToString();
                    d["id"] = dr["dwbm"].ToString();
                    d["ischecked"] = Convert.ToInt32(dr["qx"]) > 0 ? true : false;
                    if (dr["dwbm"].ToString().Length >= 6)
                    {
                        d["icon"] = "tree_dw";
                    }
                    else if (dr["dwbm"].ToString().Length == 4)
                    {
                        d["icon"] = "tree_bm";
                    }
                    else if (dr["dwbm"].ToString().Length == 3)
                    {
                        d["icon"] = "tree_js";
                    }
                    if (string.IsNullOrEmpty(key) || true)//使用条件查询时，是否显示子级单位信息,当前默认显示
                    {
                        if (!string.IsNullOrEmpty(key))
                        {
                            
                            DataRow[] drs = ds.Tables[0].Select("FDWBM='" + dr["dwbm"].ToString() + "'");
                            DataRow[] dsp = ds.Tables[0].Select("DWBM='" + dr["fdwbm"].ToString() + "'");
                            if (drs.Length > 0) //存在子级
                            {
                                //DataSet dstemp = new DataSet();
                                //dstemp.Tables.Add(drs.CopyToDataTable());
                                d["children"] = GetChild(dr["dwbm"].ToString(), ds);
                                //value.Add(d);
                            }
                            //else if (drs.Length == 0 && dsp.Length == 0) //不存在子级和父级
                            //{

                            if (rowsList.Where(p=>p ==  dr["dwbm"].ToString()).ToArray().Length == 0)
                                value.Add(d);
                            rowsList.Add(dr["dwbm"].ToString());
                            //}
                        }
                        else
                            d["children"] = GetChild(dr["dwbm"].ToString(), context);
                    }
                    if (string.IsNullOrEmpty(key))
                        value.Add(d);
                }
                context.Response.Write(new JavaScriptSerializer().Serialize(value));

                OperateLog.AddLog(OperateLog.LogType.角色权限管理Web, "查询角色未分配的单位权限成功！", UserInfo, UserRole, context.Request);
            }
            catch (Exception ex)
            {
                OperateLog.AddLog(OperateLog.LogType.角色权限管理Web, "查询角色未分配的单位权限失败：" + ex.Message, UserInfo, UserRole, context.Request);
            }
        }
        private object GetChild(string pid, DataSet ds)
        {
            DataRow[] drs = ds.Tables[0].Select("FDWBM = " + pid);//顶级
            if (drs == null || drs.Length == 0)
                return null;
           
            List<object> value = new List<object>();
            foreach (DataRow dr in drs)
            {
                rowsList.Add(dr["dwbm"].ToString());
                var d = new Dictionary<string, object>();
                d["text"] = dr["dwmc"].ToString();
                d["id"] = dr["dwbm"].ToString();
                d["ischecked"] = Convert.ToInt32(dr["qx"]) > 0 ? true : false;
                if (dr["dwbm"].ToString().Length >= 6)
                {
                    d["icon"] = "tree_dw";
                }
                else if (dr["dwbm"].ToString().Length == 4)
                {
                    d["icon"] = "tree_bm";
                }
                else if (dr["dwbm"].ToString().Length == 3)
                {
                    d["icon"] = "tree_js";
                }
                object child = null;
                DataRow[] drc = ds.Tables[0].Select("FDWBM='" + dr["dwbm"].ToString() + "'");
                DataSet dstp = null;
                if (drc.Length > 0)
                {
                    dstp = new DataSet();
                    dstp.Tables.Add(drc.CopyToDataTable());
                    child = GetChild(dr["dwbm"].ToString(), dstp);
                }

                if (child != null)
                {
                    d["children"] = child;
                }
                value.Add(d);
            }
            return value;
        }

        private object GetChild(string pid, HttpContext context)
        {
            //string key = context.Request.Params["key"];
            DataSet ds = null;
            //if (!string.IsNullOrEmpty(key))
            //{
            //    ds = dsp;
            //}
            //else
            ds = GetChildData(pid, context);

            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return null;
            List<object> value = new List<object>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                var d = new Dictionary<string, object>();
                d["text"] = dr["dwmc"].ToString();
                d["id"] = dr["dwbm"].ToString();
                d["ischecked"] = Convert.ToInt32(dr["qx"]) > 0 ? true : false;
                if (dr["dwbm"].ToString().Length >= 6)
                {
                    d["icon"] = "tree_dw";
                }
                else if (dr["dwbm"].ToString().Length == 4)
                {
                    d["icon"] = "tree_bm";
                }
                else if (dr["dwbm"].ToString().Length == 3)
                {
                    d["icon"] = "tree_js";
                }

                object child = null;
                //if (!string.IsNullOrEmpty(key))
                //{
                //    DataRow[] drs = dsp.Tables[0].Select("FDWBM='" + dr["dwbm"].ToString() + "'");
                //    DataSet dstp = null;
                //    if (drs.Length > 0)
                //    {
                //        dstp = new DataSet();
                //        dstp.Tables.Add(drs.CopyToDataTable());
                //        child = GetChild(dr["dwbm"].ToString(), context, dstp);
                //    }

                //}
                //else
                child = GetChild(dr["dwbm"].ToString(), context);
                if (child != null)
                {
                    d["children"] = child;
                }
                value.Add(d);
            }
            return value;
        }



        private DataSet GetChildData(string pid, HttpContext context)
        {
            string jsbm = context.Request.Params["jsbm"];
            string bmbm = context.Request.Params["bmbm"];
            string _dwbm = context.Request.Params["_dwbm"];//角色对应的单位编码
            string dwbm = context.Request.Params["dwbm"];//角色所有的单位的权限
            string key = context.Request.Params["key"];

            string type = context.Request.Params["type"];
            if (_dwbm != null)
            {
                _dwbm = _dwbm.Replace("a", "");
                bmbm = bmbm.Replace("a", "");
                jsbm = jsbm.Replace("a", "");
            }
            //数据查询条件
            string where = string.Empty;
            List<object> values = new List<object>();
            string strQxWhere = "";
            strQxWhere += " and trim(JSBM) = :JSBM AND TRIM(DWBM)=:DWBM1 AND TRIM(BMBM)=:BMBM";
            values.Add(jsbm);
            values.Add(_dwbm);
            values.Add(bmbm);
            EDRS.BLL.XT_DM_QX bll = new EDRS.BLL.XT_DM_QX(context.Request);
            //数据查询条件
            where += " AND FDWBM=:DWBM";
            where += " AND SFSC=:SFSC ";
            values.Add(pid);
            values.Add("N");
            try
            {
                DataSet ds = bll.GetTreeList(where, strQxWhere, values.ToArray());
                return ds;
            }
            catch
            { }
            return null;
        }
       

        
        public bool IsReusable {
            get {
                return false;
            }
        }
    }
}