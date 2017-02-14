using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDRS.Common;
using EDRS.Model;
using Newtonsoft.Json;

namespace WebUI.Pages.GNGL
{
    public partial class ConfigFun : BasePage
    {
        public static HttpRequest request;
        protected void Page_Load(object sender, EventArgs e)
        {
            request = Request;
            string[] t = Request.PathInfo.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            string type = Request["t"];
            // funTypeAll = FunTypeAll();
            if (!string.IsNullOrEmpty(type))
            {
                Response.ContentEncoding = Encoding.UTF8;
                Response.ContentType = "application/json";
                if (type.Equals("LoadTreeGrid"))
                {
                    Response.Write(FunTypeAll());
                }
                Response.End();
            }
        }


        private  string GetFunTreeGrid()
        {
            var bllgndy = new EDRS.BLL.XT_QX_GNDY(this.Request);
            var sbwhere = new StringBuilder();
            var objectValues = new object[1];

            sbwhere.Append(" and SFSC =:SFSC ");
            objectValues[0] = "N";
            

            var ds =  bllgndy.GetList(sbwhere.ToString(), objectValues);
            if (ds!=null&&ds.Tables.Count>0)
            {
                var count = bllgndy.GetRecordCount(sbwhere.ToString(), objectValues);

                var dt = ds.Tables[0];
                string j = "{\"total\":" + count + ",\"rows\":" + JsonHelper.JsonString(dt) + "}";
                return j;
            }
            return "";
        }
        /// <summary>
        /// 保存人员权限
        /// </summary>
        /// <param name="ryids">选择的人员</param>
        /// <param name="qxids">选择的权限</param>
        /// <returns>返回保存是否成功</returns>
        [WebMethod]
        public static string SaveRyAuth(string ryids,string qxids)
        {
            var bll = new EDRS.BLL.XT_QX_RYGNFP(request);

            var bllryjsfp = new EDRS.BLL.XT_QX_RYJSFP(request);

            var mode = new EDRS.Model.XT_QX_RYGNFP();
             
            //将ryids 转换成数组
           
            var listryid = JsonStringToList<EDRS.Model.XT_ZZJG_RYBM>(ryids);
            //将qxids 转换成数组
            var listqxid = JsonStringToList<TreeViewMode>(qxids);

            

            //循环添加人员功能
            try
            {
                foreach (var r in listryid)
                {
                    foreach (var q in listqxid.Where(q => !string.IsNullOrWhiteSpace(q.Gnbm)))
                    {
                        //添加前先删除原来的该人员权限
                        bll.Delete(r.DWBM, r.GH, q.Gnbm);

                        var sbryjsfp = new StringBuilder();
                        sbryjsfp.Append(" and GH=:GH ");
                        var objectValues = new object[1] {r.GH};
                        //读取人员部门编号
                        var ryjsfplist = bllryjsfp.GetList(sbryjsfp.ToString(), objectValues);
                        var bmbm = string.Empty;
                        if (ryjsfplist.Tables.Count>0&&ryjsfplist.Tables[0].Rows.Count>0)
                        {
                            bmbm = ryjsfplist.Tables[0].Rows[0]["GH"].ToString();
                        }
                       
                        //添加新的权限
                        bll.Add(new XT_QX_RYGNFP()
                        {
                            BMBM = "0001",
                            BZ = "",
                            DWBM = r.DWBM,
                            GH = r.GH,
                            GNBM = q.Gnbm,
                            GNCS = " "
                        });
                    }
                }
                return ReturnString.JsonToString(Prompt.win, "添加权限分配成功", null);
            }
            catch (Exception)
            {

                return ReturnString.JsonToString(Prompt.error, "添加权限分配失败", null);
            }
        }


        public static string FunTypeAll()
        {
            var gnfl = new EDRS.BLL.XT_QX_GNFL(request);
            var bllGn = new EDRS.BLL.XT_QX_GNDY(request);

            var sbwhere = new StringBuilder();
            
            sbwhere.Append(" and SFSC = 'N' ");
            //得到分类
            var flModeList = gnfl.GetModelList(sbwhere.ToString());

            var viewModeList = new List<TreeViewMode>();
            sbwhere.Append(" and FLBM=:FLBM ");
            //循环分类，得到分类下的功能
            foreach (var xtQxGnfl in flModeList)
            {
               
                var objectVelues = new object[1] {xtQxGnfl.FLBM};

                var ml = bllGn.GetModelList(sbwhere.ToString(), objectVelues);

                viewModeList.AddRange(ml.Select(xtQxGndy => new TreeViewMode()
                {
                    Fflbm = xtQxGndy.FLBM,
                    Flbm = xtQxGndy.GNBM, 
                    Fldz = xtQxGnfl.URLDZ, 
                    Flmc = xtQxGnfl.FLMC, 
                    Gnbm = xtQxGndy.GNBM,
                    Gndz = xtQxGndy.GNCXJ,
                    _parentId = xtQxGndy.FLBM,
                    Gnmc = xtQxGndy.GNMC
                }));
                viewModeList.Add(new TreeViewMode()
                {
                    Fflbm = xtQxGnfl.FFLBM,
                    _parentId = xtQxGnfl.FFLBM,
                    Flbm = xtQxGnfl.FLBM,
                    Flmc = xtQxGnfl.FLMC,
                    Fldz =  xtQxGnfl.URLDZ
                });
            }

            string j = "{\"total\":" + viewModeList.Count + ",\"rows\":" + JsonHelper.JsonString(viewModeList)+"}";
            return j;
        }


        /// <summary>
        /// 保存角色权限
        /// </summary>
        /// <param name="jsids">角色id集合</param>
        /// <param name="qxids">权限ID集合</param>
        /// <returns></returns>
        [WebMethod]
        public static string SaveJsAuth(string jsids, string qxids)
        {
            //
            return "";
        }
        #region  根据人员ID 获取权限
        /// <summary>
        /// 根据人员ID 获取权限
        /// </summary>
        /// <param name="ryid"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetQxByRyid(string ghid)
        {
            ghid = ghid.Replace("[", "").Replace("]", "").Replace("\"", "");


            var sbwhere = new StringBuilder();
            
            var ghids = ghid.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);

            if (ghids.Length==0)
            {
                return "";
            }
            var objectValues = new object[ghids.Length];
            var blljsgnfp = new EDRS.BLL.XT_QX_RYGNFP(request);

            for (var i=0;i<ghids.Length;i++)
            {
                if (i==0)
                {
                    sbwhere.Append(" and (GH=:GH ");
                }
                else
                {
                    sbwhere.Append("  GH=:GH"+i);
                }
                
                objectValues[i] = ghids[i];

                if (i<ghids.Length-1)
                {
                    sbwhere.Append(" or ");
                }
                if (i==ghids.Length-1)
                {
                    sbwhere.Append(" ) ");
                }
            }

            var list = blljsgnfp.GetModelList(sbwhere.ToString(), objectValues);

            var listgnid = list.Select(xtQxRygnfp => xtQxRygnfp.GNBM).ToList();


            var str = JsonHelper.JsonString(listgnid);
            return str;
        }
        #endregion

        private static List<T> JsonStringToList<T>(string jsonStr)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List < T > objects = serializer.Deserialize<List<T>>(jsonStr);
            return objects;
        }
       
    }
    
    public class TreeViewMode
    {
        public string Flbm { get; set; }
        public string Fflbm { get; set; }
        public string Gnbm { get; set; }
        public string Gnmc { get; set; }
        public string Gndz { get; set; }
        public string Flmc { get; set; }
        public string Fldz { get; set; }
        public string _parentId { get; set; }
    }
}