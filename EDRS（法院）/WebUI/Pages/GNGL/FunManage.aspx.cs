using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDRS.BLL;
using EDRS.Common;
using Microsoft.SqlServer.Server;

namespace WebUI.Pages.GNGL
{
    public partial class FunManage:BasePage
    {
        public static HttpRequest request;
        protected void Page_Load(object sender, EventArgs e)
        {
            request = this.Request;
            //if (!Page.IsPostBack)
            //{
            //    string[] t = Request.PathInfo.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            //    string type = Request["t"];

            //    if (!string.IsNullOrEmpty(type))
            //    {
            //        Response.ContentEncoding = Encoding.UTF8;
            //        Response.ContentType = "application/json";
            //        if (type.Equals("GetFunTypeAll"))
            //        {
            //            var parentid = Request["parentid"];
            //            Response.Write(GetFunTypeAll(parentid));
            //        }
            //        Response.End();
            //    }
            //}
        }

        /// <summary>
        /// 获取功能分类结合
        /// </summary>
        /// <returns>返回功能分类集合</returns>
        
        [WebMethod]
        public static  string GetFunTypeAll(string parentid)
        {
            //TODO: 添加权限验证

            var dt = GetFunTypeDataTable(parentid);

            

            var returnstring = DataTable2TreeJson(dt);
             returnstring = "[{\"id\":\"\",\"text\":\"顶级分类\",\"children\":" + returnstring + "}]";

            return returnstring;
        }

        /// <summary>
        /// DataTable 转树控件json
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns>转换的TreeJson</returns>
        private static string DataTable2TreeJson(DataTable dt)
        {
            //json格式字符串
            var sbjson = new StringBuilder();
            sbjson.Append("[");
            if (dt != null)
            {
                var bll = new XT_QX_GNFL(request);
                var list = bll.DataTableToList(dt);
                //循环获得的当前级分类节点
                foreach (EDRS.Model.XT_QX_GNFL xtQxGnfl in list)
                {
                    sbjson.Append("{\"id\":\"" + xtQxGnfl.FLBM + "\",");
                    sbjson.Append("\"text\":\"" + xtQxGnfl.FLMC + "\"");
                    //获取当前级节点下级节点
                    dt = GetFunTypeDataTable(xtQxGnfl.FLBM);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        sbjson.Append(",\"state\":\"closed\",");
                        //将下级节点转换成json
                        var childrenstr = DataTable2TreeJson(dt);
                        sbjson.Append("\"children\":" + childrenstr);
                    }
                    sbjson.Append("},");
                }

            }
            var strjson = sbjson.ToString();

            strjson = strjson.Substring(0, strjson.Length - 1);

            strjson += "]";
            return strjson;
        }

        /// <summary>
        /// 根据父分类ID，获取该父分类下的分类集合
        /// </summary>
        /// <param name="parentid">父分类ID</param>
        /// <returns>该父分类ID下的分类集合</returns>
        private static DataTable GetFunTypeDataTable(string parentid)
        {
            var gnfl = new XT_QX_GNFL(request);
            var sbwhere = new StringBuilder();
            var valuses = new object[1];

            sbwhere.Append(" and SFSC = 'N' ");

            if (!string.IsNullOrWhiteSpace(parentid))
            {
                sbwhere.Append(" and FFLBM =:FFLBM ");
                valuses[0] = parentid;
            }
            else
            {
                sbwhere.Append(" and FFLBM IS NULL ");
            }

            var ds = gnfl.GetList(sbwhere.ToString(), valuses);

            int count = (ds != null && ds.Tables.Count > 0) ? ds.Tables[0].Rows.Count : 0;

            return (ds != null && ds.Tables.Count > 0) ? ds.Tables[0] : new DataTable();
        }

        /// <summary>
        /// 获取分类下的功能集合
        /// </summary>
        /// <param name="funtypeid">功能分类</param>
        /// <returns>功能集合</returns>
        private static DataTable GetFunDataTable(string funtypeid)
        {
            var bllgndy = new XT_QX_GNDY(request);
            var sbwhere = new StringBuilder();
            var valueobject = new object[1];

            sbwhere.Append(" and SFSC = 'N' ");

            if (!string.IsNullOrWhiteSpace(funtypeid))
            {
                sbwhere.Append(" and FLBM=:FLBM ");
                valueobject[0] = funtypeid;
            }
            var ds = bllgndy.GetList(sbwhere.ToString(), valueobject);
            return (ds != null && ds.Tables.Count > 0) ? ds.Tables[0] : new DataTable();
        }

        /// <summary>
        /// 删除选中分类
        /// 业务规则：
        /// 1.删除为修改删除标记为“Y”，不能为真正的物理删除
        /// 2.删除时如果有下级不能删除
        /// </summary>
        /// <param name="id">分类ID</param>
        /// <returns>返回提示</returns>
        [WebMethod]
        public static string DeleteFunType(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return ReturnString.JsonToString(Prompt.error, "请选中一个选中项", null);
            }
            var bll = new EDRS.BLL.XT_QX_GNFL(request);
            try
            {
                //是否有下级
                var funType =  GetFunTypeDataTable(id);
                var fun = GetFunDataTable(id);
                if ((funType!=null&&funType.Rows.Count>0)||(fun!=null&&fun.Rows.Count>0))
                {
                    return ReturnString.JsonToString(Prompt.error, "删除失败当前功能下项存在功能定义", null);
                }
                //修改“是否删除”为“Y”
                var mode = bll.GetModel(id);
                mode.SFSC = "Y";
                bll.Update(mode);
            }
            catch (Exception)
            {
                return ReturnString.JsonToString(Prompt.error, "删除数据发生异常", null);
            }
            return ReturnString.JsonToString(Prompt.win, "删除成功", null);
        }

        /// <summary>
        /// 删除选中的功能
        /// 业务规则：
        /// 1.删除为标记是否删除为“Y”
        /// </summary>
        /// <param name="id">功能ID</param>
        /// <param name="dwbm">单位编码</param>
        /// <returns>是否删除成功</returns>
        [WebMethod]
        public static string DeleteFun(string id,string dwbm)
        {
            //检查参数是否有误
            if (string.IsNullOrWhiteSpace(id)||string.IsNullOrWhiteSpace(dwbm))
            {
                return ReturnString.JsonToString(Prompt.error, "请选中一个选中项", null);
            }

            
            var bll = new EDRS.BLL.XT_QX_GNDY(request);
            //删除
            try
            {
                //修改标记为："Y"
                var mode = bll.GetModel(id,dwbm);

                bll.Delete(mode.GNBM, mode.DWBM);
            }
            catch (Exception)
            {
                return ReturnString.JsonToString(Prompt.error, "删除数据发生异常", null);
            }
            return ReturnString.JsonToString(Prompt.win, "删除成功", null);
        }

        /// <summary>
        /// 根据查询条件查询功能
        /// </summary>
        /// <param name="page">当前页</param>
        /// <param name="rows">每页数量</param>
        /// <param name="flbm">分类编码</param>
        /// <param name="gnmc">功能名称</param>
        /// <param name="gnbm">功能编码</param>
        /// <returns></returns>
        [WebMethod]
        public static string GetFunData(string page, string rows, string flbm, string gnmc, string gnbm)
        {

            int pageNumber = int.Parse(page);
            int pageSize = int.Parse(rows);

            var sbwhere = new StringBuilder();
            sbwhere.Append(" and SFSC = 'N'");
            //添加分类编码查询条件
            var objectValues = new object[3];
            if (!string.IsNullOrWhiteSpace(flbm))
            {
                sbwhere.Append(" and FLBM=:FLBM");
                objectValues[0] = flbm;
            }
            //添加功能名称条件
            if (!string.IsNullOrWhiteSpace(gnmc))
            {
                sbwhere.Append(" and GNMC like :GNMC");
                objectValues[1] = "%" + gnmc + "%";
            }
            //添加功能编码条件
            if (!string.IsNullOrWhiteSpace(gnbm))
            {
                sbwhere.Append(" and GNBM like :GNBM ");
                objectValues[2] = "%" + gnbm + "%";
            }

            var bllGndy = new XT_QX_GNDY(request);

            var ds = bllGndy.GetListByPage(sbwhere.ToString(), "", (pageSize*pageNumber) - pageSize + 1,
                (pageSize*pageNumber), objectValues);

            int count = ds.Tables[0].Rows.Count;

            List<EDRS.Model.XT_QX_GNDY> modeList = bllGndy.DataTableToList(ds.Tables[0]);

            var resJson = JsonHelper.JsonString(modeList);

            string j = "{\"total\":" + count + ",\"rows\":" + resJson + "}";
            return j;
        }
    }
}