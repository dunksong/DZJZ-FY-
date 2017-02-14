using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Text.RegularExpressions;
using EDRS.Common;
using EDRS.BLL;
using System.Data;
using Cyvation.CCQE.Common;
using System.Web.Script.Serialization;
using ZC57s.CaseInfoServ.DigitalDossier.ICInterface;

namespace WebUI.Pages.Template
{
    public class TemplateModel
    {
        public string id;
        public string text;
        public string parentId;
        public string SortIndex;
        public string auto;
    }
    public partial class TemplateDeploy : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            string type = Request["t"];
            
            if (!string.IsNullOrEmpty(type))
            {
                string outMsg="";
                Response.ContentEncoding = Encoding.UTF8;
                Response.ContentType = "application/json";

                if (type.Equals("GetData"))
                    Response.Write(ListBind());
                if(type.Equals("GetLoaclData"))
                {
                    Response.Write(GetLoaclData());
                }
                if (type.Equals("GetDwAjData"))
                {
                    string json = GetDwAjData();
                    Response.Write(json);
                }
                if (type.Equals("AddTemp"))
                {
                    Response.Write(AddTemp());
                }
                if (type.Equals("AddData"))
                {
                    Response.Write(AddData());
                }
                if(type.Equals("UpDate2"))
                {
                    Response.Write(UpDate2());
                }
                if (type.Equals("UpDate1"))
                    Response.Write(UpData1());
                if (type.Equals("UpData"))
                    Response.Write(UpData());
                if (type.Equals("DelData"))
                    Response.Write(DelData());
                if (type.Equals("GetModelPList"))
                    Response.Write(GetModelOrParentList());
                if (type.Equals("GetAJType"))
                    Response.Write(GetAJType());
                if (type.Equals("GetSSLB"))
                    Response.Write(GetSSLB());
                if (type.Equals("GetYpzmb"))
                {
                    Response.Write(GetYpzmb());
                }
                if (type.Equals("GetYWLXList"))
                {
                    Response.Write(GetYWLXList());
                }
                Response.End();
            }
        }

        private string GetYWLXList()
        {
            EDRS.BLL.XT_DM_YWBM bll = new EDRS.BLL.XT_DM_YWBM(this.Request);
            DataSet ds = bll.GetAllList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                dt.Columns["YWBM"].ColumnName = "id";
                dt.Columns["YWMC"].ColumnName = "text";
                return EDRS.Common.JsonHelper.JsonString(dt);
            }
            else
            {
                return EDRS.Common.ReturnString.JsonToString(Prompt.error, "未找到相关业务类别", null);
            }
        }

        private string UpDate2()
        {

            string dwbm = Request["dwbm"];
            string sslbbm = Request["sslbbm"];
            string ajlbbm = Request["ajlbbm"];
            List<object> param = new List<object>();
            string strWhere = " and unitID=:dwbm";

            param.Add(dwbm);
            strWhere += " and CASEINFOTYPEID=:CASEINFOTYPEID";
            param.Add(ajlbbm);
            strWhere += " and sslbbm=:sslbbm";
            param.Add(sslbbm);
            EDRS.BLL.XY_DZJZ_MBPZB bll = new XY_DZJZ_MBPZB(Request);
            DataSet ds = bll.GetList(strWhere, param.ToArray());
           
            EDRS.Model.XY_DZJZ_MBPZB model = new EDRS.Model.XY_DZJZ_MBPZB();
            string msg = string.Empty;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                model = bll.GetModel(dr["DOSSIERTYPEVALUEMEMBER"].ToString());
                if (model != null)
                {
                    if (string.IsNullOrEmpty(Request.Form.Get("txt_rank")))
                        model.SortIndex = 1;
                    else
                        model.SortIndex = int.Parse(Request.Form.Get("txt_rank"));
                    if (bool.Parse(Request.Form.Get("chk_autoFound_ar")))
                        model.Auto = "Y";
                    else
                        model.Auto = "N";
                    if (bll.Update(model))
                    {
                        //数据日志
                        OperateLog.AddLog(OperateLog.LogType.卷宗模板配置Web, "修改模板配置成功", Request.Form.Get("txt_name_up"), UserInfo, UserRole, this.Request);
                        return ReturnString.JsonToString(Prompt.win, "保存成功", null);
                    }
                    //数据日志
                    OperateLog.AddLog(OperateLog.LogType.卷宗模板配置Web, "修改模板配置失败" + msg, Request.Form.Get("txt_name_up"), UserInfo, UserRole, this.Request);
                    return ReturnString.JsonToString(Prompt.error, "保存失败", null);
                }
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.卷宗模板配置Web, "修改模板配置未找到修改信息" + msg, Request.Form.Get("txt_name_up"), UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "未找到需要修改信息", null);
        }

        private string GetLoaclData()
        {
            string where = string.Empty;

            //树形循环条件
            bool direction = true;
            bool isOpen = false;
            string withWhere = string.Empty;
            string levelNum = "";// " and level < 10 ";
            string isLeaf = "ISLEAF";
            XY_DZJZ_MBPZB bll = new XY_DZJZ_MBPZB(this.Request);
            object[] values = new object[2];
            string dwbm = Request["dwbm"].Trim();
            string ajlbbm = Request["ajlbbm"].Trim();
            where = " and trim(UNITID) = :UNITID";
            values[0] = dwbm;
            where += " and trim(caseInfoTypeID) = :caseInfoTypeID";
            values[1] = ajlbbm;
            DataSet ds = bll.GetTreeList(where, withWhere, direction, values);
            string treeJson =  ConvertToLocalSSLBJosn(ds);
            return treeJson;
        }

        private string AddTemp()
        {
            Cyvation.CCQE.Web.ZZJGHandler.JsonResult resultJson = new Cyvation.CCQE.Web.ZZJGHandler.JsonResult();

            try
            {
                string jsonText = Request.Params["jsonText"];//保存模板的json集合

                string _sslbbm = Request.Params["sslbbm"];//所属类别
                string _sslbmc = Request.Params["sslbmc"];//所属类别
                string _ajlbbm = Request.Params["ajlbbm"];//案件类别
                string _ajlbmc = Request.Params["ajlbmc"];//案件类别
                EDRS.Model.XT_DM_QX model = new EDRS.Model.XT_DM_QX();
                EDRS.BLL.XY_DZJZ_MBPZB bll = new EDRS.BLL.XY_DZJZ_MBPZB(Request);
                JavaScriptSerializer jsonH = new JavaScriptSerializer();
                List<TemplateModel> list = jsonH.Deserialize(jsonText, typeof(List<TemplateModel>)) as List<TemplateModel>;
                //将list 转换为 list<dictionary>
                List<Dictionary<string, string>> tempList = new List<Dictionary<string, string>>();
                foreach (TemplateModel tmodel in list)
                {
                    Dictionary<string, string> tempModel = new Dictionary<string, string>();
                    tempModel["id"] = tmodel.id;
                    tempModel["text"] = tmodel.text;
                    tempModel["parentId"] = tmodel.parentId;
                    tempModel["SortIndex"] = tmodel.SortIndex;
                    tempModel["auto"] = tmodel.auto;
                    tempList.Add(tempModel);
                }
                bool result = bll.AddList(tempList,UserInfo.DWBM,_ajlbbm,_ajlbmc,_sslbbm,_sslbmc);// bll.AddDW(list, _jsbm, _dwbm, _bmbm);
                resultJson.isTrue = true;
                resultJson.errorMsg = resultJson.isTrue ? "操作成功！" : "操作失败，请稍后重试！";
                if (result)
                {
                    OperateLog.AddLog(OperateLog.LogType.角色权限管理Web, "新增模板配置成功！",_sslbbm, UserInfo, UserRole, Request);
                }
                else
                {
                    OperateLog.AddLog(OperateLog.LogType.角色权限管理Web, "新增模板配置失败！", _sslbbm, UserInfo, UserRole, Request);
                }
            }
            catch (Exception ex)
            {
                EDRS.Common.LogHelper.LogError(null, "", ex.Message, "ADDDWQX", "ZZJGHandler");
            }
            return resultJson.ToJsonString();
        }

        private string GetYpzmb()
        {
            string dwbm = Request["dwbm"];
            string sslbbm = Request["sslbbm"];
            string ajlbbm = Request["ajlbbm"];
            List<object> param = new List<object>();
            string strWhere = " and unitID=:dwbm";
           
            param.Add(dwbm);
            strWhere += " and CASEINFOTYPEID=:CASEINFOTYPEID";
            param.Add(ajlbbm);
            strWhere += " and sslbbm=:sslbbm";
            param.Add(sslbbm);
            EDRS.BLL.XY_DZJZ_MBPZB bll = new XY_DZJZ_MBPZB(Request);
            DataSet ds = bll.GetList(strWhere, param.ToArray());
            string jsonResult = "";
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                jsonResult = ds.Tables[0].Rows[0]["AUTO"] + "," + ds.Tables[0].Rows[0]["SORTINDEX"];
            }
            return ReturnString.JsonToString(Prompt.win, "", jsonResult);
        }

        private string GetDwAjData()
        {
            EDRS.BLL.XY_DZJZ_MBPZB bll = new XY_DZJZ_MBPZB(Request);
            DataSet ds = null;
            string json = "{\"Rows\":" + "[]" + ",\"Total\":" + 0 + "}";
            string orderBy = Request["sortName"] + " " + Request["sortOrder"];
            int page = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["pagesize"]);
            string strWhere = "";
            string dwbm = Request["dwbm"];
            string caseajlb = Request["ajlb"];
            strWhere += " and trim(qxbm) in(select distinct trim(QXBM) from xt_dm_qx a join XT_QX_RYJSFP b on trim(a.jsbm) = b.jsbm and trim(a.bmbm)=b.bmbm and trim(a.dwbm)=b.dwbm and gh=:GH1 and b.dwbm=:DWBM1 and a.qxlx=0)";
            //strWhere += " and trim(ajlbbm) in ( select distinct trim(QXBM) from xt_dm_qx a join XT_QX_RYJSFP b on trim(a.jsbm) = b.jsbm and trim(a.bmbm)=b.bmbm and trim(a.dwbm)=b.dwbm and gh=:GH2 and b.dwbm=:DWBM2 and a.qxlx=1) ";
            object[] param = new object[] { 
                UserInfo.GH,
                UserInfo.DWBM
                //,
                //UserInfo.GH,
                //UserInfo.DWBM
            };
            //strWhere += " and ajlbbm in (select qxbm from xt_dm_qx where DWBM = '" + UserInfo.DWBM + "' AND JSBM in (" + Jsbms + ") AND BMBM in (" + Bmbms + ") AND QXLX = 1)";
            if (!string.IsNullOrEmpty(dwbm))
            {
                string dwbms = dwbm.Replace(";", ",");
                strWhere += " and trim(qxbm) in (" + StringPlus.ReplaceSingle(dwbms) + ")";
            }
            if (!string.IsNullOrEmpty(caseajlb))
            {
                string caseajlbs = caseajlb.Replace(";", ",");
                strWhere += " and trim(ajlbbm) in (" + caseajlbs + ")";
            }
            if(string.IsNullOrEmpty(orderBy.Trim()))
            {
                orderBy  = "dwbm";
            }
                
            try
            {
                int count = 0;
                ds = bll.GetDwAjList(out count, strWhere, orderBy, pageSize * (page - 1), pageSize * page, param);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    json = "{\"Rows\":" + ds.Tables[0].ToDatagridJson() + ",\"Total\":" + count + "}";
                }
            }
            catch (Exception ex)
            { 
                
            }
            return json;
        }
        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="dwbm"></param>
        /// <returns></returns>
        private string UpData1()
        {
            string msg = string.Empty;

            XY_DZJZ_MBPZB bll = new XY_DZJZ_MBPZB(this.Request);
            EDRS.Model.XY_DZJZ_MBPZB model = bll.GetModel(Request.Form.Get("tempID"));
            if (model != null)
            {
                model.DossierTypeValueMember = Request.Form.Get("tempID");
                if (string.IsNullOrEmpty(Request.Form.Get("txt_rank")))
                    model.SortIndex = 1;
                else
                    model.SortIndex = int.Parse(Request.Form.Get("txt_rank"));
                if (bool.Parse(Request.Form.Get("chk_autoFound_ar")))
                    model.Auto = "Y";
                else
                    model.Auto = "N";
                if (bll.Update(model))
                {
                    //数据日志
                    OperateLog.AddLog(OperateLog.LogType.卷宗模板配置Web, "修改模板配置成功", Request.Form.Get("txt_name_up"), UserInfo, UserRole, this.Request);
                    return ReturnString.JsonToString(Prompt.win, "保存成功", null);
                }
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.卷宗模板配置Web, "修改模板配置失败" + msg, Request.Form.Get("txt_name_up"), UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.error, "保存失败", null);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.卷宗模板配置Web, "修改模板配置未找到修改信息" + msg, Request.Form.Get("txt_name_up"), UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "未找到需要修改信息", null);
        }
        #endregion
        #region 添加单位数据
        /// <summary>
        /// 添加单位数据
        /// </summary>
        /// <returns></returns>
        private string AddData()
        {
            string msg = string.Empty;
            string number = "00000001";

            XY_DZJZ_MBPZB bll = new XY_DZJZ_MBPZB(this.Request);
            DataSet ds = bll.GetListByPage("", "DOSSIERTYPEVALUEMEMBER desc", 0, 1);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                number = ds.Tables[0].Rows[0]["DOSSIERTYPEVALUEMEMBER"].ToString();
                number = (int.Parse(number) + 1).ToString().PadLeft(8, '0');
            }
            EDRS.Model.XY_DZJZ_MBPZB model = new EDRS.Model.XY_DZJZ_MBPZB();
            model.DossierTypeValueMember = number;
            model.CaseInfoTypeID = Request.Form.Get("ajlbbm") ?? ""; 
            model.CaseInfoTypeName = Request.Form.Get("ajlbmc");
            model.DossierParentMember = Request.Form.Get("key_parent") ?? "";
            model.UnitID = Request.Form.Get("dwbm") ?? "";
            model.SSLBBM = Request.Form.Get("sslbbm") ?? "";
            model.SSLBMC = Request.Form.Get("sslbmc") ?? "";
            model.DossierTypeDisplayMember = Request.Form.Get("sslbmc") ?? "";
            if (string.IsNullOrEmpty(Request.Form.Get("txt_rank")))
                model.SortIndex = 1;
            else
                model.SortIndex = int.Parse(Request.Form.Get("txt_rank"));
            model.DossierEvidenceValueMember = "";
            model.Category = "";
            if (bool.Parse(Request.Form.Get("chk_autoFound_ar")))
                model.Auto = "Y";
            else
                model.Auto = "N";
            if (bll.Add(model))
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.卷宗模板配置Web, "添加模板配置成功", Request.Form.Get("txt_name"), UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.win, "保存成功", model.DossierTypeValueMember);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.卷宗模板配置Web, "添加模板配置失败", Request.Form.Get("txt_name"), UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "保存失败", null);
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="dwbm"></param>
        /// <returns></returns>
        private string UpData()
        {
            string dwbm = Request.Form.Get("key_hidd_up");
            if (string.IsNullOrEmpty(dwbm) || !Regex.IsMatch(dwbm, @"^[A-Za-z0-9]{1,8}$"))
                return ReturnString.JsonToString(Prompt.error, "未找到需要修改信息", null);
            string msg = string.Empty;
            if (!ProvingUpFrom(ref msg))
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.卷宗模板配置Web, "修改模板配置验证失败" + msg, Request.Form.Get("txt_name_up"), UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.error, msg, null);
            }

            XY_DZJZ_MBPZB bll = new XY_DZJZ_MBPZB(this.Request);
            EDRS.Model.XY_DZJZ_MBPZB model = bll.GetModel(dwbm);
            if (model != null)
            {
                model.CaseInfoTypeID = Request.Form.Get("tree_ajtype_up_val");
                model.CaseInfoTypeName = Request.Form.Get("tree_ajtype_up") ?? "";
                model.DossierTypeDisplayMember = Request.Form.Get("txt_name_up");
                model.SSLBBM = Request.Form.Get("tree_sslb_up_val") ?? "";
                model.SSLBMC = Request.Form.Get("tree_sslb_up") ?? "";
                if (string.IsNullOrEmpty(Request.Form.Get("txt_rank_up")))
                    model.SortIndex = 1;
                else
                    model.SortIndex = int.Parse(Request.Form.Get("txt_rank_up"));
                if (bool.Parse(Request.Form.Get("chk_autoFound_up_ar")))
                    model.Auto = "Y";
                else
                    model.Auto = "N";
                if (bll.Update(model))
                {
                    //数据日志
                    OperateLog.AddLog(OperateLog.LogType.卷宗模板配置Web, "修改模板配置成功", Request.Form.Get("txt_name_up"), UserInfo, UserRole, this.Request);
                    return ReturnString.JsonToString(Prompt.win, "保存成功", null);
                }
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.卷宗模板配置Web, "修改模板配置失败" + msg, Request.Form.Get("txt_name_up"), UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.error, "保存失败", null);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.卷宗模板配置Web, "修改模板配置未找到修改信息" + msg, Request.Form.Get("txt_name_up"), UserInfo, UserRole, this.Request);
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
            //未启用多选
            string sslbbm = Request.Form["sslbbm"];
            string ajlbbm = Request.Form["ajlbbm"];
            string dwbm = Request.Form["dwbm"];
            XY_DZJZ_MBPZB bll = new XY_DZJZ_MBPZB(this.Request);
            //if (!bll.ExistsChildren(ids))
            //{
            if (bll.DeleteNodeAndChild(dwbm, ajlbbm, sslbbm))
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.卷宗模板配置Web, "删除模板配置成功", Request.Form["name"], UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.win, "删除数据成功", null);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.卷宗模板配置Web, "删除模板配置失败", Request.Form["name"], UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "删除数据失败", null);
            //}
            //return ReturnString.JsonToString(Prompt.error, "该卷（目录）下包含目录（文件），请先选择删除该卷（目录）下的目录（文件）后再删除！", null);
            //启用多选
            //string[] id = ids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            //ids = "";
            //for (int i = 0; i < id.Length; i++)
            //{
            //    ids += "'" + id[i].Trim() + "'";
            //    if (i < id.Length - 1)
            //        ids += ",";
            //}

            //if (bll.GetRecordCount(" and FDWBM in (" + ids + ")") == 0)
            //{
            //    if (bll.Delete(ids))
            //        return ReturnString.JsonToString(Prompt.win, "删除数据成功", null);
            //    return ReturnString.JsonToString(Prompt.error, "删除数据失败", null);
            //}
            //else
            //    return ReturnString.JsonToString(Prompt.error, "该单位包含下级单位，请先选择删除下级单位！", null);
        }
        #endregion

        #region 绑定案件类别
        /// <summary>
        /// 绑定案件类别
        /// </summary>
        /// <returns></returns>
        private string GetAJType()
        {
            XT_DM_AJLBBM bll = new XT_DM_AJLBBM(this.Request);
            string where = " and SFSC=:SCSC";
            object[] objValues = new object[2];
            objValues[0] = "N";  
            if (Request["key"] != null && !string.IsNullOrEmpty(Request["key"]))
            {
                where += " and AJLBMC like :AJLBMC ";
                objValues[1] = "%" + Request["key"].Trim() + "%";
            }
            where += " order by xh desc ";
            DataSet ds = bll.GetList(where, objValues);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                dt.Columns["AJLBBM"].ColumnName = "id";
                dt.Columns["AJLBMC"].ColumnName = "text";
                return EDRS.Common.JsonHelper.JsonString(dt);
            }
            return ReturnString.JsonToString(Prompt.error, "未找到"+((VersionName)0).ToString()+"类别", null);
        } 
        #endregion

        #region 绑定所属分类
        /// <summary>
        /// 绑定案件类别
        /// </summary>
        /// <returns></returns>
        private string GetSSLB()
        {
            XT_DM_AJLBBM bll = new XT_DM_AJLBBM(this.Request);
            string where = " ";
            List<object> objValues = new List<object>();
            if (Request["key"] != null && !string.IsNullOrEmpty(Request["key"]))
            {
                where += " AND SSLBMC like :SSLBMC ";
                objValues.Add("%" + Request["key"].Trim() + "%");
            }
            if (Request["LBLX"] != null && !string.IsNullOrEmpty(Request["LBLX"]))
            {
                where += " AND FSSLBBM = :FSSLBBM";
                objValues.Add(Request["LBLX"]);
            }
            else
            {
                where = " AND FSSLBBM IS NULL";
                objValues.Clear();
            }
            where += "";
            DataSet ds = bll.GetSSLBList(where, objValues.ToArray());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                dt.Columns["SSLBBM"].ColumnName = "id";
                dt.Columns["SSLBMC"].ColumnName = "text";
                return EDRS.Common.JsonHelper.JsonString(dt);
            }
            return ReturnString.JsonToString(Prompt.error, "未找到"+((VersionName)0).ToString()+"类别", null);
        }
        #endregion

        #region 绑定数据列表
        /// <summary>
        /// 绑定数据列表
        /// </summary>
        /// <returns></returns>
        private string ListBind()
        {
            TYYW_GG_AJJBXX bll = new TYYW_GG_AJJBXX(Request);
            // and fsslbbm is null
            DataTable ds = bll.GetSSLBInfos("", null);
            
            if (ds != null)
            {
                List<ClsSSLB> list = new List<ClsSSLB>();
                ClsSSLB model;
                foreach (DataRow dr in ds.Rows)
                {
                    model = new ClsSSLB();
                    model.DossierTypeDisplayMember = dr["SSLBMC"].ToString();
                    model.DossierTypeParentMember = dr["FSSLBBM"].ToString();
                    model.DossierTypeValueMember = dr["SSLBBM"].ToString();
                    model.SortIndex = int.Parse(dr["SSLBSX"].ToString());
                    list.Add(model);
                }
                
                //DataTable dscop = ds.Copy();
                //dscop.Columns["SSLBBM"].ColumnName = "id";
                //dscop.Columns["SSLBMC"].ColumnName = "text";
                


                //string jsonString = EDRS.Common.JsonHelper.JsonString(dscop);
                //string outMsg = "";
                //var list = bll.GetSSLBInfos(out outMsg,UserInfo.DWBM,UserInfo.GH);
                string jsonString = ConvertToSSLBJosn(list);
                return jsonString;
            }
            
            return "";
        }

        private string ConvertToLocalSSLBJosn(DataSet ds, string parent = "")
        {
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                return "";
            string jsonData = "";
            DataRow[] drs = null;
            if (parent != "")
            {
                drs = ds.Tables[0].Select("DossierParentMember='" + parent + "'");
            }
            else
            {
                drs = ds.Tables[0].Select("DossierParentMember is null");
            }
            foreach (DataRow dr in drs)
            {
                    if (jsonData == "")
                    {
                        jsonData = "[";
                    }
                    else
                    {
                        jsonData += ",";
                    }
                    string children = ConvertToLocalSSLBJosn(ds, dr["DossierTypeValueMember"].ToString());
                    string node = "{ \"id\": \"" + dr["SSLBBM"] + "\",\"text\":\"" + dr["SSLBMC"] + "\",\"parentId\":\"" + dr["DossierParentMember"] + "\",\"SortIndex\":\"" + dr["SortIndex"] + "\",\"Auto\":\"" + dr["Auto"] + "\",\"icon\":\"" + dr["icon"] + "\"";
                    if (!string.IsNullOrEmpty(children))
                    {
                        node += ", \"children\": " + children;
                    }
                    node += "}";
                    jsonData += node;
            }
            if (jsonData != "")
            {
                jsonData += "]";
            }
            return jsonData;
        }
        private string ConvertToSSLBJosn(List<ZC57s.CaseInfoServ.DigitalDossier.ICInterface.ClsSSLB> list,string parent = "")
        {
            string jsonData = "";
            foreach (ZC57s.CaseInfoServ.DigitalDossier.ICInterface.ClsSSLB model in list)
            {
                if (model.DossierTypeParentMember == parent)
                {
                    if (jsonData == "")
                    {
                        jsonData = "[";
                    }
                    else
                    {
                        jsonData += ",";
                    }
                    string children = ConvertToSSLBJosn(list, model.DossierTypeValueMember);
                    string node = "{ \"id\": \"" + model.DossierTypeValueMember + "\",\"text\":\"" + model.DossierTypeDisplayMember + "\",\"parentId\":\"" + model.DossierTypeParentMember + "\",\"SortIndex\":\"" + model.SortIndex + "\"";
                    if (!string.IsNullOrEmpty(children))
                    {
                        node += ", \"children\": " + children;
                    }
                    node += "}";
                    jsonData += node;
                }
            }
            if (jsonData != "")
            {
                jsonData += "]";
            }
            return jsonData;
        }
        #endregion

        #region 根据编号获取数据
        /// <summary>
        /// 根据编号获取数据
        /// </summary>
        /// <returns></returns>
        private string GetModelOrParentList()
        {
            string DossierTypeValueMember = Request["id"];
            if (string.IsNullOrEmpty(DossierTypeValueMember))
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.卷宗模板配置Web, "根据编号获取模板配置参数失败", Request.Form.Get("name"), UserInfo, UserRole, this.Request);
                return ReturnString.JsonToString(Prompt.error, "参数错误", null);
            }
            XY_DZJZ_MBPZB bll = new XY_DZJZ_MBPZB(this.Request);

            EDRS.Model.XY_DZJZ_MBPZB model = bll.GetModel(DossierTypeValueMember);
            if (model != null)
            {
                //数据日志
                OperateLog.AddLog(OperateLog.LogType.卷宗模板配置Web, "根据编号获取模板配置成功", Request.Form.Get("name"), UserInfo, UserRole, this.Request);
                return EDRS.Common.JsonHelper.JsonString(model);
            }
            //数据日志
            OperateLog.AddLog(OperateLog.LogType.卷宗模板配置Web, "根据编号获取模板配置失败", Request.Form.Get("name"), UserInfo, UserRole, this.Request);
            return ReturnString.JsonToString(Prompt.error, "获取数据失败", null);
        }
        #endregion

        #region 表单验证
        /// <summary>
        /// 表单验证
        /// </summary>
        private bool ProvingFrom(ref string msg)
        {
            string mc = Request.Form.Get("txt_name").Trim();
            string ajlb = Request.Form.Get("tree_ajtype_val").Trim(); //案件类别
            string sslb = Request.Form.Get("tree_sslb_val").Trim(); //案件类别
            string ajlbmc = Request.Form.Get("tree_ajtype").Trim(); //案件类别名称
            string pbm = Request.Form.Get("key_parent").Trim(); //父级编码
            string dwbm = Request.Form.Get("tree_dwbm").Trim(); //单位编码
            string pd = Request.Form.Get("txt_rank");//排序编码


            if (string.IsNullOrEmpty(ajlb))
            {
                msg = ((VersionName)0).ToString()+"类别不允许为空！";
                return false;
            }
            if (string.IsNullOrEmpty(sslb))
            {
                msg = "所属类别不允许为空！";
                return false;
            }
            if (string.IsNullOrEmpty(mc) || mc.Length > 150)
            {
                msg = "名称最多输入150英文字母或汉子。";
                return false;
            }
          
            return true;
        }
        #endregion 
       
        #region 表单验证
        /// <summary>
        /// 表单验证
        /// </summary>
        private bool ProvingUpFrom(ref string msg)
        {
            string mc = Request.Form.Get("txt_name_up").Trim();
            string ajlb = Request.Form.Get("tree_ajtype_up_val").Trim(); //案件类别
            string sslb = Request.Form.Get("tree_ajtype_up_val").Trim(); //所属类别

            if (string.IsNullOrEmpty(sslb))
            {
                msg = "所属类别不允许为空！";
                return false;
            }
            if (string.IsNullOrEmpty(ajlb))
            {
                msg = ((VersionName)0).ToString()+ "类别不允许为空！";
                return false;
            }
            if (string.IsNullOrEmpty(mc) || mc.Length > 150)
            {
                msg = "名称最多输入150英文字母或汉子。";
                return false;
            }
            return true;
        }
        #endregion 
    }
}