using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDRS.Common;

namespace WebUI.Pages.GNGL
{
    public partial class AddFunct : BasePage
    {
        public static HttpRequest request;
        protected void Page_Load(object sender, EventArgs e)
        {
            request = Request;
            //得到调用方法，是新增还是修改
            string method = Request.QueryString["method"] != null
                ? Request.QueryString["method"].ToString()
                : "";
            _method.Value = method;
            if (method.Equals("add"))
            {
                
                //得到功能的所属分类，由URL参数得到
                var fflbm = Request.QueryString["fflbm"] != null ? Request.QueryString["fflbm"].ToString() : "";
                var fflmc = Request.QueryString["fflmc"] != null ? Request.QueryString["fflmc"].ToString() : "";
                if (string.IsNullOrWhiteSpace(fflbm) || string.IsNullOrWhiteSpace(fflmc))
                {
                    _flbm.Value = "";
                    _flmc.Value = "顶级分类";
                }
                else
                {

                    _flbm.Value = fflbm;
                    _flmc.Value = fflmc;
                }
            }else if (method.Equals("edit"))
            {
                var gnbm = Request.QueryString["gnbm"] != null ? Request.QueryString["gnbm"].ToString() : "";
                var dwbm = Request.QueryString["dwbm"] != null ? Request.QueryString["dwbm"].ToString() : "";
                EditLoad(gnbm, dwbm);
            }
        }

        /// <summary>
        /// 添加功能
        /// </summary>
        /// <param name="data">功能表单数据</param>
        /// <returns>是否添加成功</returns>
        [WebMethod]
        public static string Add(string data)
        {
            //TODO:权限验证

            //表单验证
            var gnfl = JsonHelper.ParseFormJson<EDRS.Model.XT_QX_GNDY>(data);
            var bll = new EDRS.BLL.XT_QX_GNDY(request);
            //编码是否已经存在
            if (bll.Exists(gnfl.GNBM, gnfl.DWBM))
            {
                return ReturnString.JsonToString(Prompt.error, "编码已存在!", null);
            }
            try
            {
                //添加到数据库
                bll.Add(gnfl);
                return ReturnString.JsonToString(Prompt.win, "添加成功!", null);
            }
            catch (Exception)
            {
                return ReturnString.JsonToString(Prompt.error, "添加失败!", null);
            }
        }


        /// <summary>
        /// 修改功能时页面加载
        /// </summary>
        /// <param name="gnbm">功能编码</param>
        /// <param name="dwbm">单位编码</param>
        private void EditLoad(string gnbm,string dwbm)
        {
            //TODO: 权限验证

            var strgnbm = gnbm;
            var strdwbm = dwbm;

            //读取功能信息
            var bll = new EDRS.BLL.XT_QX_GNDY(this.Request);
            var model = bll.GetModel(gnbm, dwbm);
            try
            {
                this._cscs.Value = model.CSCS;
                this._dwbm.Value = model.DWBM;
                this._dwmc.Value = new EDRS.BLL.XT_ZZJG_DWBM(this.Request).GetModel(model.DWBM).DWMC;
                this._flbm.Value = model.FLBM;
                this._flmc.Value = new EDRS.BLL.XT_QX_GNFL(this.Request).GetModel(model.FLBM).FLMC;
                this._gnbm.Value = model.GNBM;
                this._gncs.Value = model.GNCS;
                this._gnct.Value = model.GNCT;
                this._gncxj.Value = model.GNCXJ;
                this._gnmc.Value = model.GNMC;
                this._gnsm.Value = model.GNSM;
                this._gnxh.Value = model.GNXH.ToString();
                this._gnxsmc.Value = model.GNXSMC;
                this._sfmtck.Value = model.SFMTCK;
                this._sfsc.Value = model.SFSC;
                this._gnbm.Attributes.Add("readonly", "readonly");
            }
            catch (Exception)
            {
                
                
            }
           
        }

        /// <summary>
        /// 提交修改功能
        /// </summary>
        /// <param name="data">修改的表单数据</param>
        /// <returns>返回结果</returns>
        [WebMethod]
        public static string Edit(string data)
        {
            //TODO: 添加权限验证
            //表单验证
            var gnfl = JsonHelper.ParseFormJson<EDRS.Model.XT_QX_GNDY>(data);
            var bll = new EDRS.BLL.XT_QX_GNDY(request);
            
            try
            {
                //添加到数据库
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