using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDRS.Common;
using System.Text;
using System.Data;

namespace WebUI.Pages.LSYJ
{
    public partial class LSGL : BasePage
    {

        /// <summary>
        /// 获取当前时间
        /// </summary>
        protected DateTime nowTime = DateTime.Now;

        #region 页面加载
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["YJUser"] != null)
            //    YJUser = JsonHelper.JsonString(Session["YJUser"] as EDRS.Model.YX_DZJZ_LSAJBD);

            string type = Request["t"];

            if (!string.IsNullOrEmpty(type))
            {
                Response.ContentEncoding = Encoding.UTF8;
                Response.ContentType = "application/json";
                if (type.Equals("GetMlTree"))
                {
                    EDRS.BLL.YX_DZJZ_JZML bll = new EDRS.BLL.YX_DZJZ_JZML(Request);
                    Response.Write(bll.GetMlTree(Request, false, true));
                }
                if (type.Equals("ListBind"))
                    Response.Write(ListBind());
                if (type.Equals("AddData"))
                    Response.Write(AddData());
                if (type.Equals("DelData"))
                    Response.Write(DelData());
                if (type.Equals("UpData"))
                    Response.Write(UpData());
                if (type.Equals("GetModel"))
                    Response.Write(GetModel(""));
                if (type.Equals("GetLsFile"))
                    Response.Write(GetLsFile());

                Response.End();
            }
        } 
        #endregion

        

        #region 根据律师证号获取律师附件信息
        /// 根据律师证号获取律师附件信息
        /// </summary>
        /// <returns></returns>
        private string GetLsFile()
        {
            string lszh = Request.Form["lszh"];
            EDRS.BLL.YX_DZJZ_LSZZWJ bll = new EDRS.BLL.YX_DZJZ_LSZZWJ(this.Request);

            List<string> filestr = bll.GetList(lszh);
            if (filestr.Count > 0)
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.阅卷Web, "根据编号获取律师附件成功", "", UserInfo, UserRole, this.Request);
                return JsonHelper.JsonString(filestr);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.阅卷Web, "根据编号获取律师信息失败", "", UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "获取数据失败", null);
        }
        #endregion

        #region 添加律师
        /// <summary>
        /// 添加律师
        /// </summary>
        /// <returns></returns>
        private string AddData()
        {
            string msg = string.Empty;
            if (!ProvingFrom(ref msg))
            {
                return ReturnString.JsonToString(Prompt.error, msg, null);
            }
            EDRS.Model.YX_DZJZ_LSGL model = new EDRS.Model.YX_DZJZ_LSGL();
            model.LSZH = Request.Form.Get("txt_lszh");
            model.LSXM =  Request.Form.Get("txt_lsxm");
            model.LSDW = Request.Form.Get("txt_lsdw");
            model.LSDWDZ = Request.Form.Get("txt_lsdwdz");
            model.LSDWYZHM =  Request.Form.Get("txt_lsdwyzh");
            model.LSLXDH =Request.Form.Get("txt_lslxdh");
            model.LSSJ = Request.Form.Get("txt_lssj");
            model.DELXR =Request.Form.Get("txt_delxr");
            model.DELXRDH = Request.Form.Get("txt_delxrdh");
            model.LSZGYXSJ =Convert.ToDateTime(Request.Form.Get("txt_lszgyxsj"));
            model.SFDXZG = Request.Form.Get("txt_sfdxzg") == "0" ? "N" : "Y";
            model.LSXXBZ =  Request.Form.Get("txt_lsxxbz");
            model.CJSJ = DateTime.Now;
            model.ZHYCYJSJ = null;
            model.SFSC = "N";
            EDRS.BLL.YX_DZJZ_LSGL bll = new EDRS.BLL.YX_DZJZ_LSGL(this.Request);
            //添加资质文件
            EDRS.BLL.YX_DZJZ_LSZZWJ zzbll = new EDRS.BLL.YX_DZJZ_LSZZWJ(this.Request);
            string idstr = Request.Form.Get("key_arrlife");
            
            //判断律师证号是否重复
            if (bll.Exists(model.LSZH))
            {
                return ReturnString.JsonToString(Prompt.error, "该律师证号已被使用,请重置", null);
            }

            //添加律师
            if (bll.Add(model))
            {
                //判断是否添加附件
                if (!string.IsNullOrEmpty(idstr))
                {
                    string[] idarr = idstr.Split(',');
                    List<string> filestr = new List<string>();
                    for (int i = 0; i < idarr.Length; i++)
                    {
                        filestr.Add(idarr[i]);
                    }
                    //添加律师资质文件
                    if (zzbll.AddList(model.LSZH, filestr))
                    {
                        //数据日志
                        OperateLog.AddLog(OperateLog.LogType.参数配置Web, "添加律师成功", "", UserInfo, UserRole, this.Request);
                        return ReturnString.JsonToString(Prompt.win, "保存成功", null);
                    }
                }
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.参数配置Web, "添加律师成功", "", UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.win, "保存成功", null);
                
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.参数配置Web, "添加律师失败", "", UserInfo, UserRole, this.Request);
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
            string lszh = Request.Form["id"];
            EDRS.BLL.YX_DZJZ_LSGL bll = new EDRS.BLL.YX_DZJZ_LSGL(this.Request);
            EDRS.BLL.YX_DZJZ_LSZZWJ zzbll = new EDRS.BLL.YX_DZJZ_LSZZWJ(this.Request);

            if (bll.Delete(lszh))
            {

                if (zzbll.DelAll(lszh))
                {
                    //数据日志
                    OperateLog.AddLog(OperateLog.LogType.参数配置Web, "删除律师成功", Request.Form["cs"], UserInfo, UserRole, this.Request);
                    return ReturnString.JsonToString(Prompt.win, "删除律师成功", null);
                }
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.参数配置Web, "删除律师成功", Request.Form["cs"], UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.win, "删除律师成功", null);
                
               
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.参数配置Web, "删除律师失败", Request.Form["cs"], UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "删除律师失败", null);
        }
        #endregion

        #region 根据律师证号获取数据
        /// <summary>
        /// 根据律师证号获取数据
        /// </summary>
        /// <param name="DWBM"></param>
        /// <returns></returns>
        private string GetModel(string lszh)
        {
            if (string.IsNullOrEmpty(lszh))
            {
                lszh = Request.Form.Get("id");
                if (string.IsNullOrEmpty(lszh))
                {
                    return ReturnString.JsonToString(Prompt.error, "参数错误", null);
                }
            }
           
            EDRS.BLL.YX_DZJZ_LSGL bll = new EDRS.BLL.YX_DZJZ_LSGL(this.Request);
            DataSet ds = bll.GetListfile(" and YX_DZJZ_LSGL.LSZH='" + lszh + "'", null);

            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt != null && dt.Columns.Count > 0 && dt.Rows.Count > 0)
                {
                    //数据日志
                    OperateLog.AddLog(OperateLog.LogType.参数配置Web, "根据编号获取类型信息成功", "", UserInfo, UserRole, this.Request);
                    return JsonHelper.JsonString(dt);
                }
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.参数配置Web, "根据编号获取类型信息失败", "", UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "获取数据失败", null);
        }
        #endregion
        #region 修改律师信息
        /// <summary>
        /// 修改律师信息
        /// </summary>
        /// <returns></returns>
        private string UpData()
        {
            string lszh = Request.Form.Get("key_hidd").Trim();
            if (string.IsNullOrEmpty(lszh))
            {
                return ReturnString.JsonToString(Prompt.error, "未找到需要修改信息", null);
            }
            string msg = string.Empty;
            if (!ProvingFrom(ref msg))
            {
                return ReturnString.JsonToString(Prompt.error, msg, null);
            }

            EDRS.BLL.YX_DZJZ_LSGL bll = new EDRS.BLL.YX_DZJZ_LSGL(this.Request);
            EDRS.Model.YX_DZJZ_LSGL model = bll.GetModel(lszh);
            //添加资质文件
            EDRS.BLL.YX_DZJZ_LSZZWJ zzbll = new EDRS.BLL.YX_DZJZ_LSZZWJ(this.Request);
            string idstr = Request.Form.Get("key_arrlife");

            if (model != null)
            {
                model.LSXM = Request.Form.Get("txt_lsxm");
                model.LSDW = Request.Form.Get("txt_lsdw");
                model.LSDWDZ = Request.Form.Get("txt_lsdwdz");
                model.LSDWYZHM = Request.Form.Get("txt_lsdwyzh");
                model.LSLXDH = Request.Form.Get("txt_lslxdh");
                model.LSSJ = Request.Form.Get("txt_lssj");
                model.DELXR = Request.Form.Get("txt_delxr");
                model.DELXRDH = Request.Form.Get("txt_delxrdh");
                model.LSZGYXSJ = Convert.ToDateTime(Request.Form.Get("txt_lszgyxsj"));
                model.SFDXZG = Request.Form.Get("txt_sfdxzg") == "0" ? "N" : "Y";
                model.LSXXBZ = Request.Form.Get("txt_lsxxbz");
                model.CJSJ = DateTime.Now;
                model.ZHYCYJSJ = null;
                model.SFSC = "N";
                
                if (bll.Update(model))
                {

                    if (!string.IsNullOrEmpty(idstr))
                    {
                        string[] idarr = idstr.Split(',');
                        List<string> filestr = new List<string>();
                        for (int i = 0; i < idarr.Length; i++)
                        {
                            filestr.Add(idarr[i]);
                        }
                        //添加律师资质文件
                        if (zzbll.AddList(model.LSZH, filestr))
                        {
                            //数据日志
                            OperateLog.AddLog(OperateLog.LogType.参数配置Web, "修改律师信息成功", "", UserInfo, UserRole, this.Request);
                            return ReturnString.JsonToString(Prompt.win, "保存成功", null);
                        }
                    }
                    //数据日志 
                    OperateLog.AddLog(OperateLog.LogType.参数配置Web, "修改律师信息成功", "", UserInfo, UserRole, this.Request);
                    return ReturnString.JsonToString(Prompt.win, "保存成功", null);
                }
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.参数配置Web, "修改律师信息失败", "", UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.error, "保存失败", null);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.参数配置Web, "修改律师未找到信息", "", UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "未找到需要修改信息", null);
        }
        #endregion
        #region 表单验证
        /// <summary>
        /// 表单验证
        /// </summary>
        private bool ProvingFrom(ref string msg)
        {
            string LSZH = Request.Form.Get("txt_lszh");
            string LSXM = Request.Form.Get("txt_lsxm");
            string LSDW = Request.Form.Get("txt_lsdw");
            string LSDWDZ = Request.Form.Get("txt_lsdwdz");
            string LSDWYZHM = Request.Form.Get("txt_lsdwyzh");
            string LSLXDH = Request.Form.Get("txt_lslxdh");
            string LSSJ = Request.Form.Get("txt_lssj");
            string DELXR = Request.Form.Get("txt_delxr");
            string DELXRDH = Request.Form.Get("txt_delxrdh");
            string LSZGYXSJ =Request.Form.Get("txt_lszgyxsj");
            string LSXXBZ = Request.Form.Get("txt_lsxxbz");
            if (string.IsNullOrEmpty(LSZH))
            {
                msg = "请输入律师证号！";
                return false;
            }
            if (string.IsNullOrEmpty(LSXM))
            {
                msg = "请输入律师姓名！";
                return false;
            }
            if (string.IsNullOrEmpty(LSDW))
            {
                msg = "请输入律师单位！";
                return false;
            }

            if (string.IsNullOrEmpty(LSDWDZ))
            {
                msg = "请输入律师单位地址！";
                return false;
            }
            if (string.IsNullOrEmpty(LSDWYZHM))
            {
                msg = "请输入律师单位邮政号码！";
                return false;
            }
            if (string.IsNullOrEmpty(LSLXDH))
            {
                msg = "请输入律师联系电话！";
                return false;
            }
            if (LSLXDH.Length > 50)
            {
                msg = "律师联系电话不能超过50个字！";
                return false;
            }
            if (string.IsNullOrEmpty(LSSJ))
            {
                msg = "请输入律师手机！";
                return false;
            }
            if (LSSJ.Length > 50)
            {
                msg = "律师手机不能超过50个字！";
                return false;
            }
            if (string.IsNullOrEmpty(DELXR))
            {
                msg = "请输入律师第二联系人！";
                return false;
            }
            if (string.IsNullOrEmpty(DELXRDH))
            {
                msg = "请输入律师第二联系人电话！";
                return false;
            }
            if (string.IsNullOrEmpty(LSZGYXSJ))
            {
                msg = "请选择律师资格有效时间！";
                return false;
            }
            return true;
        }
        #endregion

        #region 绑定列表
        /// <summary>
        /// 绑定列表
        /// </summary>
        /// <returns></returns>
        public string ListBind()
        {
            string page = Request["page"];
            string rows = Request["pagesize"];
            string key = Request["key"];
            string lsxm = Request["lsxm"];

            int pageNumber = int.Parse(page);
            int pageSize = int.Parse(rows);
            string where = " and T.SFSC='N'";

            object[] values = new object[1];
            if (!string.IsNullOrEmpty(key))
            {
                where += " and LSZH like :LSZH";
                values[0] = "%" + key + "%";
            }

            if (!string.IsNullOrEmpty(lsxm))
            {
                where += " and LSXM like :LSXM";
                values[0] = "%" + lsxm + "%";
            }

            EDRS.BLL.YX_DZJZ_LSGL bll = new EDRS.BLL.YX_DZJZ_LSGL(this.Request);

            DataSet ds = bll.GetListByPageEx(where, "CJSJ desc", (pageSize * pageNumber) - pageSize + 1, (pageSize * pageNumber), values);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.阅卷Web, "获取阅卷绑定列表成功", UserInfo, UserRole, this.Request);
                int count = bll.GetRecordCount(where, values);
                return "{\"Total\":" + count + ",\"Rows\":" + JsonHelper.JsonString(ds.Tables[0]) + "}";
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.阅卷Web, "获取阅卷绑定列表-未找到信息", UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "未找到阅卷绑定信息", null);
        }
        #endregion

        


        

        
    }
}