using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EDRS.Common;
using System.Data;
using System.Text.RegularExpressions;

namespace WebUI.Pages.QXGL
{
    public partial class StaffAuthorityManager:BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request["t"];

            if (!string.IsNullOrEmpty(type))
            {
                Response.ContentEncoding = Encoding.UTF8;
                Response.ContentType = "application/json";
                if (type.Equals("GetTreeDW"))
                    Response.Write(ListBindDW());
                if (type.Equals("GetData"))
                    Response.Write(ListBind());
                if (type.Equals("GetDataGn"))
                    Response.Write(ListBindGn());
                if (type.Equals("AddData"))
                    Response.Write(AddData());
                if (type.Equals("DelData"))
                    Response.Write(DelData());
                Response.End();
            }

        }

        #region 绑定数据列表
        /// <summary>
        /// 绑定数据列表
        /// </summary>
        /// <returns></returns>
        private string ListBindDW()
        {
            string where = "";
            //switch (this.UserDwbm.DWJB)
            //{
            //    case "1":
            //        where = " and sfgjysy='Y'";
            //        break;
            //    case "2":
            //        where = " and sfsysy='Y'";
            //        break;
            //    case "3":
            //        where = " and sfsjysy='Y'";
            //        break;
            //    case "4":
            //        where = " and sfqysy='Y'";
            //        break;
            //}
            EDRS.BLL.XT_ZZJG_DWBM dmbll = new EDRS.BLL.XT_ZZJG_DWBM(Request);
            //EDRS.Model.XT_ZZJG_DWBM dmmodel = dmbll.GetModel(UserInfo.DWBM);

            DataSet ds2 = dmbll.GetTreeList("", " DWBM=" + UserInfo.DWBM, false, null);
            if(ds2 != null && ds2.Tables.Count>0 && ds2.Tables[0].Rows.Count > 1)
                where += " and sfdjy='N' ";
            //if (dmmodel != null && dmmodel.FDWBM != null && !string.IsNullOrEmpty(dmmodel.FDWBM))
            //    where += " and sfdjy='N' ";

            EDRS.BLL.XT_QX_GNDY bll = new EDRS.BLL.XT_QX_GNDY(Request);
            DataSet ds = bll.GetListByType(UserInfo.DWBM, where);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                dt.Columns["FLBM"].ColumnName = "ID";
                dt.Columns["FFLBM"].ColumnName = "PARENTID";
                dt.Columns["FLMC"].ColumnName = "NAME";
                dt.Columns.Add("icon");
                foreach (DataRow dr in dt.Rows)
                {
                    if (string.IsNullOrEmpty(dr["PARENTID"].ToString()))
                        dr["icon"] = "picon";
                    else
                        dr["icon"] = "chicon";
                }

                return new TreeJson(dt, "ID", "NAME", "PARENTID","","","",true,true).ResultJson.ToString();
            }
            return ReturnString.JsonToString(Prompt.error, "未找到功能", null);

        }
        #endregion

        #region 添加人员功能权限数据
        /// <summary>
        /// 添加人员功能权限数据
        /// </summary>
        /// <returns></returns>
        private string AddData()
        {
            string msg = string.Empty;

            string gnid = Request["gns"];
            string ghs = Request["ghs"];
            string[] arrghs = ghs.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string[] arrgns = gnid.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            List<EDRS.Model.XT_QX_RYGNFP> modelList = new List<EDRS.Model.XT_QX_RYGNFP>();

            string where = string.Empty;

            string wghs = string.Empty;
            string wgnbms = string.Empty;

            for (int j = 0; j < arrgns.Length; j++)
            {
                for (int i = 0; i < arrghs.Length; i++)
                {
                    EDRS.Model.XT_QX_RYGNFP model = new EDRS.Model.XT_QX_RYGNFP();
                    model.DWBM = UserInfo.DWBM;
                    model.GH = arrghs[i];
                    model.GNBM = arrgns[j];
                    model.BMBM = "0   ";
                    //model.GNCS = "";
                    model.BZ = "";
                    modelList.Add(model);
                    if (j == 0)
                    {
                        wghs += "'" + arrghs[i] + "'";
                        if (i < arrghs.Length - 1)
                        {
                            wghs += ",";
                        }
                    }
                }
                wgnbms += "'" + arrgns[j] + "'";
                if (j < arrgns.Length-1)
                {
                    wgnbms += ",";
                }
            }
            EDRS.BLL.XT_QX_RYGNFP bll = new EDRS.BLL.XT_QX_RYGNFP(this.Request);
            List<EDRS.Model.XT_QX_RYGNFP> modelList2 = bll.GetModelList(" and DWBM = '" + UserInfo.DWBM + "' and GH in (" + wghs + ") and GNBM in (" + wgnbms + ")");

            List<EDRS.Model.XT_QX_RYGNFP> modelList3 = modelList.Except(modelList2, new XT_QX_RYGNFPListEquality()).ToList();
           
            if (modelList3.Count == 0 || bll.AddList(modelList3) )
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.功能权限管理Web, "添加人员功能权限成功","工号："+ghs, UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.win, "保存成功", null);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.功能权限管理Web, "添加人员功能权限失败", "工号：" + ghs, UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "保存失败", null);
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        private string DelData()
        {
            string gh = Request.Form["gh"];
       
            EDRS.BLL.XT_QX_RYGNFP bll = new EDRS.BLL.XT_QX_RYGNFP(this.Request);

            if (!string.IsNullOrEmpty(gh) && bll.DeleteList(StringPlus.ReplaceSingle(gh)))
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.功能权限管理Web, "删除人员功能权限成功", Request.Form["bmmc"], UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.win, "删除人员功能权限成功", null);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.功能权限管理Web, "删除人员功能权限失败", Request.Form["bmmc"], UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "删除人员功能权限失败", null);
        }
        #endregion

        #region 绑定数据列表
        /// <summary>
        /// 绑定数据列表
        /// </summary>
        /// <returns></returns>
        private string ListBind()
        {
            string page = Request["page"];
            string rows = Request["pagesize"];
            string key = Request["key"];
            int pageNumber = int.Parse(page);
            int pageSize = int.Parse(rows);
            string where = string.Empty;
           
            object[] values = new object[6];
            where += " and SFSC=:SFSC";
            values[0] = "N";
            where += " and DWBM=:DWBM";
            values[1] = UserInfo.DWBM;

            if (!string.IsNullOrEmpty(key))
            {
                where += " and( MC like :MC or GH like :GH or DLBM like :DLBM or GZZH like :GZZH)";
                values[2] = "%" + key + "%";
                values[3] = "%" + key + "%";
                values[4] = "%" + key + "%";
                values[5] = "%" + key + "%";
            }
            where += " and SFTZ='N'";
            EDRS.BLL.XT_ZZJG_RYBM bll = new EDRS.BLL.XT_ZZJG_RYBM(this.Request);
            DataSet ds = bll.GetListByPage(where, "", (pageSize * pageNumber) - pageSize + 1, (pageSize * pageNumber), values);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.功能权限管理Web, "人员功能权限获取人员信息成功", UserInfo, UserRole, this.Request);
                int count = bll.GetRecordCount(where, values);
                return "{\"Total\":" + count + ",\"Rows\":" + JsonHelper.JsonString(ds.Tables[0]) + "}";
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.功能权限管理Web, "人员功能权限未找到相关人员信息", UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "未找到任何人员信息", null);
        }
        #endregion

        #region 绑定数据列表
        /// <summary>
        /// 绑定数据列表
        /// </summary>
        /// <returns></returns>
        private string ListBindGn()
        {
            string page = Request["page"];
            string rows = Request["pagesize"];
            string key = Request["key"];
            string gn = Request["gn"];
            int pageNumber = int.Parse(page);
            int pageSize = int.Parse(rows);
            string where = string.Empty;

            object[] values = new object[7];
            where += " and T.SFSC=:SFSC";
            values[0] = "N";
            where += " and b.DWBM=:DWBM";
            values[1] = UserInfo.DWBM;

            if (!string.IsNullOrEmpty(key))
            {
                where += " and( T.MC like :MC or T.GH like :GH or T.DLBM like :DLBM or T.GZZH like :GZZH)";
                values[2] = "%" + key + "%";
                values[3] = "%" + key + "%";
                values[4] = "%" + key + "%";
                values[5] = "%" + key + "%";
            }
            if (!string.IsNullOrEmpty(gn))
            {
                where += " and b.GNBM=:GNBM";
                values[6] = gn;
            }

            EDRS.BLL.XT_ZZJG_RYBM bll = new EDRS.BLL.XT_ZZJG_RYBM(this.Request);
            DataSet ds = bll.GetListByPageAndGn(where, "", (pageSize * pageNumber) - pageSize + 1, (pageSize * pageNumber), values);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.功能权限管理Web, "获取人员功能权限成功", UserInfo, UserRole, this.Request);
                int count = bll.GetRecordCountAndGn(where, values);
                return "{\"Total\":" + count + ",\"Rows\":" + JsonHelper.JsonString(ds.Tables[0]) + "}";
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.功能权限管理Web, "获取人员功能权限未找到数据", UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "", null);
        }
        #endregion


    }

    #region 去重对象
    /// <summary>
    /// 去重对象
    /// </summary>
    public class XT_QX_RYGNFPListEquality : IEqualityComparer<EDRS.Model.XT_QX_RYGNFP>
    {
        public bool Equals(EDRS.Model.XT_QX_RYGNFP x, EDRS.Model.XT_QX_RYGNFP y)
        {
            if (x.DWBM == y.DWBM && x.GNBM == y.GNBM && x.GH == y.GH)
                return true;
            return false;
        }

        public int GetHashCode(EDRS.Model.XT_QX_RYGNFP obj)
        {
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return obj.ToString().GetHashCode();
            }
        }
    }   
    #endregion
}