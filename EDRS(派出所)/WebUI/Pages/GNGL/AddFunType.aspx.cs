using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDRS.BLL;
using EDRS.Common;

namespace WebUI.Pages.GNGL
{
    /// <summary>
    /// 功能分类添加，编辑页面
    /// </summary>
    public partial class AddFunType : BasePage
    {
        public static HttpRequest request;
        protected void Page_Load(object sender, EventArgs e)
        {
            request = Request;
            if (!Page.IsPostBack)
            {
                //得到调用方法，是新增还是修改
                string method = Request.QueryString["method"] != null
                    ? Request.QueryString["method"].ToString()
                    : "";
                if (method.Equals("add"))
                {
                    _method.Value = method;
                    var fflbm = Request.QueryString["fflbm"] != null ? Request.QueryString["fflbm"].ToString() : "";
                    var fflmc = Request.QueryString["fflmc"] != null ? Request.QueryString["fflmc"].ToString() : "";
                    if (string.IsNullOrWhiteSpace(fflbm) || string.IsNullOrWhiteSpace(fflmc))
                    {
                        _fflbm.Value = "";
                        _fflmc.Value = "顶级分类";
                    }
                    else
                    {

                        _fflbm.Value = fflbm;
                        _fflmc.Value = fflmc;
                    }

                }else if (method.Equals("edit"))
                {
                    _method.Value = method;
                    var flbm = Request.QueryString["fflbm"] != null ? Request.QueryString["fflbm"].ToString() : "";
                    EditLoad(flbm);
                }
            }

        }

        /// <summary>
        /// 添加功能分类
        /// </summary>
        /// <param name="data">表单数据</param>
        /// <returns>是否添加成功</returns>
        [WebMethod]
        public static string Add(string data)
        {
            //TODO: 权限验证

            //表单验证
            var bll = new EDRS.BLL.XT_QX_GNFL(request);
            var gnfl = JsonHelper.ParseFormJson<EDRS.Model.XT_QX_GNFL>(data);
            //验证分类编码是否已经存在
            if (bll.Exists(gnfl.FLBM)) //如果存在就返回提示
            {
                return ReturnString.JsonToString(Prompt.error, "分类编码已存在!", null);
            }
            try
            {
                //提交数据库
                bll.Add(gnfl);
                return ReturnString.JsonToString(Prompt.win, "添加成功!", null);
            }
            catch (Exception)
            {
                return ReturnString.JsonToString(Prompt.error, "添加失败!", null);
            }

            return ReturnString.JsonToString(Prompt.win, "添加成功!", null);
        }
        /// <summary>
        /// 修改页面加载，加载需要修改的分类信息
        /// </summary>
        /// <param name="flbm">分类编码</param>
        private void EditLoad(string flbm)
        {
            //TODO:权限验证

            var bllGnfl = new XT_QX_GNFL(this.Request);
            //从数据库得到分类实体
            var model = bllGnfl.GetModel(flbm);

            if (model != null) //如果分类存在就加载数据
            {
                _flbm.Value = model.FLBM;
                _flmc.Value = model.FLMC;
                //如果数据库中分类编码为空，就默认为：顶级分类。
                if (!string.IsNullOrWhiteSpace(model.FFLBM))
                {
                    _fflbm.Value = model.FFLBM;
                    _fflmc.Value = bllGnfl.GetModel(model.FFLBM).FLMC;
                }
                else
                {
                    _fflbm.Value = "";
                    _fflmc.Value = "顶级分类";
                }
            }
            else //不存在就默认：顶级分类
            {
                _fflbm.Value = "";
                _fflmc.Value = "顶级分类";
            }
        }

        /// <summary>
        /// 提交修改
        /// </summary>
        /// <param name="data">表单数据</param>
        /// <returns>返回是否修改成功</returns>
        [WebMethod]
        public static string Edit(string data)
        {
            //TODO: 权限验证
            //将表单实例化
            var gnfl = JsonHelper.ParseFormJson<EDRS.Model.XT_QX_GNFL>(data);
            try
            {
                //更新数据库
                var bll = new EDRS.BLL.XT_QX_GNFL(request);
                bll.Update(gnfl);
                return ReturnString.JsonToString(Prompt.win, "修改成功!", null);
            }
            catch (Exception)
            {
                return ReturnString.JsonToString(Prompt.error, "修改失败!", null);
            }
        }
    }
}