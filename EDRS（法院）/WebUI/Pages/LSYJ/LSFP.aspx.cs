using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDRS.BLL;
using EDRS.Common;
using System.Data;
using System.Text;
using EDRS.Common.DEncrypt;

namespace WebUI.Pages.LSYJ
{
    public partial class LSFP : BasePage
    {
        /// <summary>
        /// 获取当前时间
        /// </summary>
        protected DateTime nowTime = DateTime.Now;

        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request["t"];

            if (!string.IsNullOrEmpty(type))
            {
                Response.ContentEncoding = Encoding.UTF8;
                Response.ContentType = "application/json";
                if (type.Equals("ListBind"))
                    Response.Write(ListBind());
                if (type.Equals("AddData"))
                    Response.Write(AddData());
                if (type.Equals("UpData"))
                    Response.Write(UpData());
                if (type.Equals("ExUpData"))
                    Response.Write(ExUpData());
                if (type.Equals("DelData"))
                    Response.Write(DelData());
                if (type.Equals("GetModel"))
                    Response.Write(GetModel(""));
                if (type.Equals("bindlsxm"))
                    Response.Write(bindlsxm());
                if (type.Equals("GetLS"))
                    Response.Write(GetLS());
                if (type.Equals("AddLS"))
                    Response.Write(AddLS());
                if (type.Equals("GetLsFile"))
                    Response.Write(GetLsFile());
                if (type.Equals("GetSHBM"))
                    Response.Write(GetSHBM());
                if (type.Equals("GetUserBybm"))
                    Response.Write(GetUserBybm());
                if (type.Equals("CopCard"))
                    Response.Write(CopCard());
                Response.End();
            }
        }
        #region 绑定律师姓名
        /// <summary>
        /// 绑定律师姓名
        /// </summary>
        /// <returns></returns>
        private string bindlsxm()
        {
            string key = Request.Form["key"];
            string where = "";
            if (!string.IsNullOrEmpty(key))
                where = " and LSXM like '%" + key + "%'";
            EDRS.BLL.YX_DZJZ_LSGL bll = new YX_DZJZ_LSGL(Request);
            DataSet ds = bll.GetListByPage(" and SFSC='N'" + where, "CJSJ desc", 1, int.MaxValue);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.阅卷Web, "律师绑定信息成功", "", UserInfo, UserRole, this.Request);
                return JsonHelper.JsonString(ds.Tables[0]);
            }
            else
            {
                OperateLog.AddLog(OperateLog.LogType.阅卷Web, "未找到律师绑定信息", "", UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.error, "未找到律师绑定信息", null);
            }
        }
        /// <summary>
        #endregion


        #region 根据律师证号获取律师附件信息
        /// 根据律师证号获取律师附件信息
        /// </summary>
        /// <returns></returns>
        private string GetLsFile()
        {
            string lszh = Request.Form["lszh"];
            EDRS.BLL.YX_DZJZ_LSZZWJ bll = new YX_DZJZ_LSZZWJ(this.Request);

            List<string> filestr = bll.GetList(lszh);
            if (filestr.Count > 0)
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.阅卷Web, "根据编号获取律师附件成功", "", UserInfo, UserRole, this.Request);
                return JsonHelper.JsonString(filestr);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.阅卷Web, "根据编号获取律师附件失败", "", UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "获取数据失败", null);
        }
        #endregion

        #region 根据律师证号获取律师详细信息
        /// 根据律师证号获取律师详细信息
        /// </summary>
        /// <returns></returns>
        private string GetLS()
        {
            string lszh = Request.Form["lszh"];
            EDRS.BLL.YX_DZJZ_LSGL bll = new YX_DZJZ_LSGL(this.Request);
            DataSet ds = bll.GetListfile(" and YX_DZJZ_LSGL.LSZH='" + lszh + "'", null);

            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt != null && dt.Columns.Count > 0 && dt.Rows.Count > 0)
                {
                    //数据日志
                    OperateLog.AddLog(OperateLog.LogType.阅卷Web, "根据编号获取律师信息成功", "", UserInfo, UserRole, this.Request);
                    return JsonHelper.JsonString(dt);

                }

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
        private string AddLS()
        {
            string msg = string.Empty;
            if (!ProvingFrom(ref msg))
            {
                return ReturnString.JsonToString(Prompt.error, msg, null);
            }

            EDRS.Model.YX_DZJZ_LSGL model = new EDRS.Model.YX_DZJZ_LSGL();
            model.LSZH = Request.Form["lszh"];
            model.LSXM = Request.Form["lsxm"];
            model.LSDW = Request.Form["lsdw"];
            model.LSDWDZ = Request.Form["lsdwdz"];
            model.LSDWYZHM = Request.Form["lsdwyzh"];
            model.LSLXDH = Request.Form["lslxdh"];
            model.LSSJ = Request.Form["lssj"];
            model.DELXR = Request.Form["delxr"];
            model.DELXRDH = Request.Form["delxrdh"];
            model.LSZGYXSJ = Convert.ToDateTime(Request.Form["lszgyxsj"]);
            model.SFDXZG = Request.Form["sfdxzg"] == "0" ? "N" : "Y";
            model.LSXXBZ = Request.Form["lsxxbz"];
            model.CJSJ = DateTime.Now;
            model.ZHYCYJSJ = null;
            model.SFSC = "N";
            EDRS.BLL.YX_DZJZ_LSGL bll = new EDRS.BLL.YX_DZJZ_LSGL(this.Request);

            //添加资质文件
            EDRS.BLL.YX_DZJZ_LSZZWJ zzbll = new EDRS.BLL.YX_DZJZ_LSZZWJ(this.Request);
            string idstr = Request.Form["filestr[]"];

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
            string ajmc = Request["ajmc"];
            string gh = Request["gh"];
            string mc = Request["mc"];

            int pageNumber = int.Parse(page);
            int pageSize = int.Parse(rows);
            string where = " and T.SFSC='N'";

            object[] values = new object[6];
            if (!string.IsNullOrEmpty(key))
            {
                where += " and t.BMSAH like :BMSAH";
                values[0] = "%" + key + "%";
            }
            if (!string.IsNullOrEmpty(ajmc))
            {
                where += " and AJMC like :AJMC";
                values[1] = "%" + ajmc + "%";
            }
            if (!string.IsNullOrEmpty(gh))
            {
                where += " and GH like :GH";
                values[2] = "%" + gh + "%";
            }
            if (!string.IsNullOrEmpty(mc))
            {
                where += " and MC like :MC";
                values[3] = "%" + mc + "%";
            }

            where += " and JDRGH = :JDRGH";
            where += " and JDDWBM=:JDDWBM";
            values[4] = UserInfo.GH;
            values[5] = UserInfo.DWBM;

            EDRS.BLL.YX_DZJZ_LSAJBD bll = new EDRS.BLL.YX_DZJZ_LSAJBD(this.Request);

            DataSet ds = bll.GetListByPage(where, "YJJSSJ desc,JDSJ desc", (pageSize * pageNumber) - pageSize + 1, (pageSize * pageNumber), values);
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

        #region 提交申请
        /// <summary>
        /// 提交申请
        /// </summary>
        /// <returns></returns>
        private string AddData()
        {
            string msg = string.Empty;
            if (!ProvingFrom(ref msg))
            {
                return ReturnString.JsonToString(Prompt.error, msg, null);
            }
            YX_DZJZ_LSYJSQ bll = new YX_DZJZ_LSYJSQ(this.Request);
            //DataSet ds = bll.GetList(" and SFSC='N' and SQDZT<>'N' and YJSQDH=:YJSQDH", new string[] { Request.Form["txt_yjsqdh"] });
            //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //    return ReturnString.JsonToString(Prompt.error, "该阅卷申请单号已被申请过", null);

            string zh = Request.Form["LSZH"];
            if (string.IsNullOrEmpty(zh))
            {
                return ReturnString.JsonToString(Prompt.error, "请选择律师姓名", null);
            }
            string dm = Request.Form["txt_yjsqdm"];
            if (string.IsNullOrEmpty(dm))
            {
                return ReturnString.JsonToString(Prompt.error, "申请单名不能为空", null);
            }
            Random random = new Random();
            EDRS.Model.YX_DZJZ_LSYJSQ model = new EDRS.Model.YX_DZJZ_LSYJSQ();
            model.LSZH = Request.Form["LSZH"];
            model.YJSQDH = random.Next(100000, 999999).ToString();
            model.SQSJ = DateTime.Now;
            model.SQSM = Request.Form["txt_sqsm"];
            model.SFSC = "N";
            model.SHRGH = UserInfo.GH;
            model.SHR = UserInfo.MC;
            model.SHSM = "";
            model.SHSJ = DateTime.Now;
            model.YJSQDM = dm;
            model.SQDZT = "T";
            if (bll.Add(model))
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.阅卷Web, "提交阅卷申请成功", model.YJSQDH, UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.win, "保存成功", null);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.阅卷Web, "提交阅卷申请失败", model.YJSQDH, UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "保存失败", null);
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        private string UpData()
        {
            string id = Request.Form.Get("key_hidd").Trim();
            if (string.IsNullOrEmpty(id))
            {
                return ReturnString.JsonToString(Prompt.error, "未找到需要修改信息", null);
            }
            string msg = string.Empty;
            if (!ProvingFrom(ref msg))
            {
                return ReturnString.JsonToString(Prompt.error, msg, null);
            }

            YX_DZJZ_LSYJSQ bll = new YX_DZJZ_LSYJSQ(this.Request);
            EDRS.Model.YX_DZJZ_LSYJSQ model = bll.GetModel(id);
            if (model != null)
            {
                model.LSZH = "";// Request.Form["txt_lszh"];
                // model.YJSQDH = Request.Form["txt_yjsqdh"];
                model.YJSQDM = Request.Form["txt_yjsqdm"];
                model.SQSJ = DateTime.Now;
                model.SQSM = Request.Form["txt_sqsm"];
                if (bll.Update(model))
                {
                    //数据日志
                    OperateLog.AddLog(OperateLog.LogType.阅卷Web, "修改阅卷申请成功", id, UserInfo, UserRole, this.Request);
                    return ReturnString.JsonToString(Prompt.win, "保存成功", null);
                }
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.阅卷Web, "修改阅卷申请失败", id, UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.error, "保存失败", null);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.阅卷Web, "修改阅卷申请-未找到修改信息", id, UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "未找到需要修改信息", null);
        }
        #endregion

        #region 审核
        /// <summary>
        /// 审核
        /// </summary>
        /// <returns></returns>
        private string ExUpData()
        {
            string id = Request.Form.Get("key_ex_hidd").Trim();
            if (string.IsNullOrEmpty(id))
            {
                return ReturnString.JsonToString(Prompt.error, "未找到需要修改信息", null);
            }
            string msg = string.Empty;
            if (!ProvingFrom(ref msg))
            {
                return ReturnString.JsonToString(Prompt.error, msg, null);
            }

            YX_DZJZ_LSYJSQ bll = new YX_DZJZ_LSYJSQ(this.Request);
            EDRS.Model.YX_DZJZ_LSYJSQ model = bll.GetModel(id);
            if (model != null)
            {
                model.SHSJ = DateTime.Now;
                model.SQDZT = Request.Form["rad_sqdzt"];
                model.SHSM = Request.Form["txt_shsm"];
                model.SHR = UserInfo.MC;
                model.SHRGH = UserInfo.GH;
                if (bll.Update(model))
                {
                    //数据日志
                    OperateLog.AddLog(OperateLog.LogType.阅卷Web, "修改参数配置成功", model.YJSQDH, UserInfo, UserRole, this.Request);
                    return ReturnString.JsonToString(Prompt.win, "保存成功", null);
                }
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.阅卷Web, "修改参数配置失败", model.YJSQDH, UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.error, "保存失败", null);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.阅卷Web, "修改参数配置未找到信息", id, UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "未找到需要修改信息", null);
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        private string DelData()
        {
            string ids = Request.Form["id"];
            string[] id = ids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            ids = "";
            for (int i = 0; i < id.Length; i++)
            {
                ids += "'" + id[i].Trim() + "'";
                if (i < id.Length - 1)
                    ids += ",";
            }

            YX_DZJZ_LSAJBD bll = new YX_DZJZ_LSAJBD(this.Request);

            if (bll.DeleteList(ids))
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.阅卷Web, "删除阅卷申请成功", "", UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.win, "删除数据成功", null);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.阅卷Web, "删除阅卷申请失败", "", UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "删除数据失败", null);
        }
        #endregion

        #region 根据单位获取配置数据
        /// <summary>
        /// 根据单位获取配置数据
        /// </summary>
        /// <param name="DWBM"></param>
        /// <returns></returns>
        private string GetModel(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                id = Request["id"];
                if (string.IsNullOrEmpty(id))
                {
                    return ReturnString.JsonToString(Prompt.error, "参数错误", null);
                }
            }
            YX_DZJZ_LSYJSQ bll = new YX_DZJZ_LSYJSQ(this.Request);
            EDRS.Model.YX_DZJZ_LSYJSQ model = bll.GetModel(id);
            if (model != null)
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.阅卷Web, "根据编号获取阅读申请成功", id, UserInfo, UserRole, this.Request);
                return JsonHelper.JsonString(model);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.阅卷Web, "根据编号获取阅读申请-未找到信息", id, UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "获取数据失败", null);
        }
        #endregion

        #region 表单验证
        /// <summary>
        /// 表单验证
        /// </summary>
        private bool ProvingFrom(ref string msg)
        {
            string LSZH = Request.Form["lszh"]; ;
            string LSXM = Request.Form["lsxm"];
            string LSDW = Request.Form["lsdw"];
            string LSDWDZ = Request.Form["lsdwdz"];
            string LSDWYZHM = Request.Form["lsdwyzh"];
            string LSLXDH = Request.Form["lslxdh"];
            string LSSJ = Request.Form["lssj"];
            string DELXR = Request.Form["delxr"];
            string DELXRDH = Request.Form["delxrdh"];
            string LSZGYXSJ = Request.Form["lszgyxsj"];
            string LSXXBZ = Request.Form["lsxxbz"];
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
            //if (string.IsNullOrEmpty(LSDW))
            //{
            //    msg = "请输入律师单位！";
            //    return false;
            //}

            //if (string.IsNullOrEmpty(LSDWDZ))
            //{
            //    msg = "请输入律师单位地址！";
            //    return false;
            //}
            //if (string.IsNullOrEmpty(LSDWYZHM))
            //{
            //    msg = "请输入律师单位邮政号码！";
            //    return false;
            //}
            //if (string.IsNullOrEmpty(LSLXDH))
            //{
            //    msg = "请输入律师联系电话！";
            //    return false;
            //}
            if (!string.IsNullOrEmpty(LSLXDH) && LSLXDH.Length > 50)
            {
                msg = "律师联系电话不能超过50个字！";
                return false;
            }
            //if (string.IsNullOrEmpty(LSSJ))
            //{
            //    msg = "请输入律师手机！";
            //    return false;
            //}
            if (!string.IsNullOrEmpty(LSSJ) && LSSJ.Length > 50)
            {
                msg = "律师手机不能超过50个字！";
                return false;
            }
            //if (string.IsNullOrEmpty(DELXR))
            //{
            //    msg = "请输入律师第二联系人！";
            //    return false;
            //}
            //if (string.IsNullOrEmpty(DELXRDH))
            //{
            //    msg = "请输入律师第二联系人电话！";
            //    return false;
            //}
            if (string.IsNullOrEmpty(LSZGYXSJ))
            {
                msg = "请选择律师资格有效时间！";
                return false;
            }
            return true;
        }
        #endregion

        #region 绑定部门数据
        /// <summary>
        /// 绑定部门数据
        /// </summary>
        /// <returns></returns>
        private string GetSHBM()
        {
            string where = string.Empty;
            string withWhere = string.Empty;
            object[] values = new object[2];
            where += " and SFSC=:SFSC";
            values[0] = "N";

            withWhere = " FBMBM is NULL";


            where += " and DWBM = :DWBM ";
            values[1] = base.UserInfo.DWBM;


            XT_ZZJG_BMBM bll = new XT_ZZJG_BMBM(this.Request);
            DataSet ds = bll.GetTreeList(where, withWhere, values);
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0].Copy();
                    DataView dv = dt.DefaultView;
                    dv.Sort = "DWBM asc,BMBM asc";
                    dt = dv.ToTable();                   
                    return JsonHelper.JsonString(dt);
                }
                else
                    return "[]";
            }
           
            return ReturnString.JsonToString(Prompt.error, "获取部门列表出现错误", null);
        } 
        #endregion

        #region 获取用户列表
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        private string GetUserBybm()
        {
            string bmbm = Request.Form["bmbm"];
            EDRS.BLL.XT_ZZJG_RYBM bll = new XT_ZZJG_RYBM(Request);
            DataSet ds = bll.GetListByBm(" and rj.dwbm=:dwbm and bmbm=:bmbm and SFTZ='N' and SFSC='N'", "GH ", 1, int.MaxValue, new object[] { UserInfo.DWBM, bmbm });
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return JsonHelper.JsonString(ds.Tables[0]);
            }
            return ReturnString.JsonToString(Prompt.error, "未设置相关人员信息，请先添加", null); 
        } 
        #endregion

        #region 获取写卡参数
        /// <summary>
        /// 获取写卡参数
        /// </summary>
        /// <returns></returns>
        private string CopCard()
        {
            string yjxh = Request.Form["yjxh"];
            if (string.IsNullOrEmpty(yjxh))
            {
                return ReturnString.JsonToString(Prompt.error, "写卡需要阅卷编号", null);
            }
            string strstring = DESEncrypt.Encrypt(yjxh + "@" + "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + "/IService/CallService.asmx");
            return "{\"parm\":\"" + strstring + "\"}";
        }
        #endregion
    }
}