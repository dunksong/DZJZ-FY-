using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDRS.BLL;
using EDRS.Common;

namespace WebUI.Pages.RYGL
{
    public partial class List : BasePage
    {
        public static HttpRequest request = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            request = Request;
        }

        [WebMethod]
        public static string GetList(string page, string rows)
        {
            int pageNumber = int.Parse(page);
            int pageSize = int.Parse(rows);


            StringBuilder sbwhere = new StringBuilder();
            sbwhere.Append(" and SFSC = 'N'");

            XT_ZZJG_RYBM bllRybm = new XT_ZZJG_RYBM(request);


            var ds = bllRybm.GetListByPage(sbwhere.ToString(),"",(pageSize*pageNumber)-pageSize+1,(pageSize*pageNumber),null);
            
            int count = ds.Tables[0].Rows.Count;



            List<EDRS.Model.XT_ZZJG_RYBM> modeList = bllRybm.DataTableToList(ds.Tables[0]);
            
            var resJson =  JsonHelper.JsonString(modeList);

            string j = "{\"total\":" + count + ",\"rows\":" + resJson + "}";
            return j;
        }

        /// <summary>
        /// 删除人员
        /// 不是物理删除，将SFSC字段更新为'Y'
        /// </summary>
        /// <param name="ids">人员id集合</param>
        /// <returns></returns>
        [WebMethod]
        public static string Delete(string ids)
        {
            ids = ids.Replace("[", "").Replace("]", "").Replace("\"","");

            var idsArry = ids.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
            XT_ZZJG_RYBM bllRybm = new XT_ZZJG_RYBM(request);
            //每个更新
            try
            {
                foreach (var s in idsArry)
                {
                    var rybm = bllRybm.GetModel(s);
                    if (rybm == null) continue;
                    rybm.SFSC = "Y";
                    bllRybm.Update(rybm);
                }
            }
            catch (Exception)
            {
                return ReturnString.JsonToString(Prompt.error, "删除失败!", null);
            }
            return ReturnString.JsonToString(Prompt.win, "删除成功", null);
        }

        [WebMethod]
        public static string GetRoleTree()
        {
            //var bll = new EDRS.BLL.VIEW_ROLE();
            //var list = bll.GetModelList("");
            //if (list.Count>0)
            //{
            //    var str = JsonHelper.JsonString(list);
            //    return str;
            //}
            var json = DataTableToJson(GetDwNode(""));
            return json;
        }

        private static string DataTableToJson(DataTable dt)
        {
            var reJson = new StringBuilder();
            reJson.Append("[");
            if (dt != null && dt.Rows.Count > 0)
            {
                var dwbmbll = new EDRS.BLL.XT_ZZJG_DWBM(request);
                var bmbmbll = new EDRS.BLL.XT_ZZJG_BMBM(request);
                var jsbmbll = new EDRS.BLL.XT_QX_JSBM(request);
                var dwMoldelist = dwbmbll.DataTableToList(dt);

                foreach (var xtZzjgDwbm in dwMoldelist)
                {
                    reJson.Append("{\"id\":\"" + xtZzjgDwbm.DWBM + "\",");
                    reJson.Append("\"text\":\"" + xtZzjgDwbm.DWMC + "\",");
                    reJson.Append("\"lx\":\"dw\"");
                    //获取当前单位下的子单位
                    dt = GetDwNode(xtZzjgDwbm.DWBM);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        reJson.Append(",\"state\":\"closed\",");
                        var childrenstr = DataTableToJson(dt);
                        reJson.Append("\"children\":" + childrenstr);
                    }

                    //获取当前单位下的部门
                    var bmdt = GetBmNodeBydw(xtZzjgDwbm.DWBM);
                    if (bmdt!=null&&bmdt.Rows.Count>0)
                    {
                        var strBmJsons = ",\"children\":" + DataTableToBmJson(bmdt);
                        reJson.Append(strBmJsons);   
                    }
                    reJson.Append("},");
                }
            }
            var strjson = reJson.ToString();
            strjson = strjson.Substring(0, strjson.Length - 1);
            strjson += "]";
            return strjson;
        }
        private static string DataTableToBmJson(DataTable dt)
        {
            var reJson = new StringBuilder();
            reJson.Append("[");
            if (dt != null && dt.Rows.Count > 0)
            {
                var bmbmbll = new EDRS.BLL.XT_ZZJG_BMBM(request);
                var jsbmbll = new EDRS.BLL.XT_QX_JSBM(request);
                var bmMoldelist = bmbmbll.DataTableToList(dt);

                foreach (var bmtemp in bmMoldelist)
                {
                    reJson.Append("{\"id\":\"" + bmtemp.DWBM + "\",");
                    reJson.Append("\"text\":\"" + bmtemp.BMMC + "\",");
                    reJson.Append("\"lx\":\"bm\"");
                    //获取当前单位下的子单位
                    dt =GetBmNodeBybmParentid(bmtemp.BMBM);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        reJson.Append(",\"state\":\"closed\",");
                        var childrenstr = DataTableToBmJson(dt);
                        reJson.Append("\"children\":" + childrenstr);
                    }

                    //获取当前单位下的角色
                    var jsdt = GetJsNodeBybm(bmtemp.BMBM);
                    if (jsdt != null && jsdt.Rows.Count > 0)
                    {
                        var strJsJsons = ",\"children\":" + DataTableToJsJson(jsdt);
                    }
                    reJson.Append("},");
                }
            }
            var strjson = reJson.ToString();

            strjson = strjson.Substring(0, strjson.Length - 1);

            strjson += "]";
            return strjson;
        }
        private static string DataTableToJsJson(DataTable dt)
        {
            var reJson = new StringBuilder();
            reJson.Append("[");
            if (dt != null && dt.Rows.Count > 0)
            {
                
                var jsbmbll = new EDRS.BLL.XT_QX_JSBM(request);
                var bmMoldelist = jsbmbll.DataTableToList(dt);

                foreach (var bmtemp in bmMoldelist)
                {
                    reJson.Append("{\"id\":\"" + bmtemp.JSBM + "\",");
                    reJson.Append("\"text\":\"" + bmtemp.JSMC + "\",");
                    reJson.Append("\"lx\":\"js\"");
                   
                    reJson.Append("},");
                }
            }
            var strjson = reJson.ToString();

            strjson = strjson.Substring(0, strjson.Length - 1);

            strjson += "]";
            return strjson;
        }

        /// <summary>
        /// 根据单位父节点获取父节点下的子节点集合
        /// </summary>
        /// <param name="parentid">父节点ID</param>
        /// <returns>子节点集合</returns>
        private static DataTable GetDwNode(string parentid)
        {
            var bll = new EDRS.BLL.XT_ZZJG_DWBM(request);

            var sbwhere = new StringBuilder();
            var objectValues = new object[1];
            if (string.IsNullOrWhiteSpace(parentid))
            {
                sbwhere.Append(" and FDWBM is null ");
            }
            else
            {
                sbwhere.Append(" and FDWBM=:FDWBM ");
                objectValues[0] = parentid;
            }
            var ds = bll.GetList(sbwhere.ToString(), objectValues);


            return ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
        }

        /// <summary>
        /// 根据单位ID 获取单位下部门集合
        /// </summary>
        /// <param name="dwid">单位id</param>
        /// <returns>部门集合</returns>
        private static DataTable GetBmNodeBydw(string dwid)
        {
            var bll = new EDRS.BLL.XT_ZZJG_BMBM(request);

            var sbwhere = new StringBuilder();
            var objectValues = new object[1];
            sbwhere.Append(" and FBMBM is null and SFSC = 'N' ");

            sbwhere.Append(" and DWBM=:DWBM ");
            objectValues[0] = dwid;
            
            var ds = bll.GetList(sbwhere.ToString(), objectValues);
            return ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
        }

        /// <summary>
        /// 根据部门ID 获取部门下角色集合
        /// </summary>
        /// <param name="bmid">部门ID</param>
        /// <returns>角色集合</returns>
        private static DataTable GetJsNodeBybm(string bmid)
        {
            var bll = new EDRS.BLL.XT_QX_JSBM(request);

            var sbwhere = new StringBuilder();
            var objectValues = new object[1];
            sbwhere.Append(" and BMBM=:BMBM ");
            objectValues[0] = bmid;

            var ds = bll.GetList(sbwhere.ToString(), objectValues);
            return ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
        }
        /// <summary>
        /// 根据部门父ID获取部门下子部门
        /// </summary>
        /// <param name="parentid">父部门ID</param>
        /// <returns>子部门集合</returns>
        private static DataTable GetBmNodeBybmParentid(string parentid)
        {
            var bll = new EDRS.BLL.XT_ZZJG_BMBM(request);

            var sbwhere = new StringBuilder();
            var objectValues = new object[1];
            if (!string.IsNullOrWhiteSpace(parentid))
            {
                sbwhere.Append(" and FBMBM=:FBMBM ");
                objectValues[0] = parentid;
            }
            sbwhere.Append(" and FBMBM is null and SFSC = 'N' ");

            var ds = bll.GetList(sbwhere.ToString(), objectValues);
            return ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
            return null;
        }
    }
}