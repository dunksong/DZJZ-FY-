using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDRS.BLL;
using EDRS.Common;

namespace WebUI.Pages.RYGL
{
    public partial class AddPerson : BasePage
    {
        public static HttpRequest request;
        protected void Page_Load(object sender, EventArgs e)
        {
            request = Request;
            if (!Page.IsPostBack)
            {
                //得到调用方法，是新增还是修改
                var method = Request.QueryString["method"] != null
                    ? Request.QueryString["method"].ToString()
                    : "";
                if (method.Equals("add"))
                {
                    
                    _method.Value = method;
                }
                else if (method.Equals("edit"))
                {

                    var gh = Request.QueryString["gh"] != null ? Request.QueryString["gh"].ToString() : "";
                    var dwbm = Request.QueryString["dwbm"] != null ? Request.QueryString["dwbm"].ToString() : "";
                    if (string.IsNullOrWhiteSpace(gh) || string.IsNullOrWhiteSpace(dwbm))
                    {

                    }
                    else
                    {
                        _method.Value = method;
                        EditLoad(gh,dwbm);
                    }
                }
            }
        }

        [WebMethod]
        public static string Add(string data)
        {

            
            var bllRybm = new XT_ZZJG_RYBM(request);

            var rybm = JsonHelper.ParseFormJson<EDRS.Model.XT_ZZJG_RYBM>(data);
            //验证工号是否已存在
            bool exist = bllRybm.Exists(rybm.GZZH);
            if (exist)
            {
                return ReturnString.JsonToString(Prompt.error, "工号已存在", null);
            }

            //TODO: 单位编码读取
            rybm.DWBM = "0001";
            
            rybm.GH = rybm.GZZH;

            bool b =  bllRybm.Add(rybm);

            return b
                ? ReturnString.JsonToString(Prompt.win, "添加成功", null)
                : ReturnString.JsonToString(Prompt.error, "添加失败", null);
        }

        /// <summary>
        /// 根据工号和单位编码得到要修改的数据
        /// </summary>
        /// <param name="gh">工号</param>
        /// <param name="dwbm">单位编码</param>
        private void EditLoad(string gh,string dwbm)
        {
            var bll = new EDRS.BLL.XT_ZZJG_RYBM(request);
            var sbwhere = new StringBuilder();
            sbwhere.Append(" and GH=:GH ");
            sbwhere.Append(" and DWBM =:DWBM ");
            
            var objectValues = new object[2] {gh,dwbm};
            var modelList = bll.GetModelList(sbwhere.ToString(), objectValues);
            if (modelList.Count>0)
            {
                var model = modelList[0];
                _dlbm.Value = model.DLBM;
                _dwbm.Value = model.DWBM;
                _dzyj.Value = model.DZYJ;
                _gh.Value = model.GH;
                _gzzh.Value = model.GZZH;
                _gzzh.Value = model.GZZH;
                _kl.Value = model.KL;
                _mc.Value = model.MC;

                _sfsc.Value = model.SFSC;
                //是否停职
                //_sftz.Attributes.Add("checked", model.SFTZ != "N" ? "true" : "false");
                _sftz.Value = model.SFTZ;
                //是否临时工
                //_sflsry.Attributes.Add("checked", model.SFLSRY != "N" ? "true" : "false");
                _sflsry.Value = model.SFLSRY;
                _xb.Value = model.XB;//性别
                _yddhhm.Value = model.YDDHHM;
                _ydwbm.Value = model.YDWBM;
                _ydwmc.Value = model.YDWMC;

                _gzzh.Attributes.Add("readonly", "readonly");
                _gh.Attributes.Add("readonly", "readonly");
                _dwmc.Attributes.Add("readonly", "readonly");
                _dwmc.Attributes.Add("readonly", "readonly");

                _zp.Value = " ";
                
            }
            else
            {
                
            }
        }

        /// <summary>
        /// 处理修改请求
        /// </summary>
        /// <param name="data">要修改的数据</param>
        /// <returns></returns>
        [WebMethod]
        public static string Edit(string data)
        {
            var editmodel = JsonHelper.ParseFormJson<EDRS.Model.XT_ZZJG_RYBM>(data);
            try
            {
                var bll = new EDRS.BLL.XT_ZZJG_RYBM(request);
                bll.Update(editmodel);
                
                return ReturnString.JsonToString(Prompt.win, "修改成功", null);
            }
            catch (Exception)
            {
                return ReturnString.JsonToString(Prompt.win, "修改失败", null);
            }
            
        }
    }
}