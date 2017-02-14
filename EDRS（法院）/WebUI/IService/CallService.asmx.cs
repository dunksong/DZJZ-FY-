using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;

namespace WebUI.IService
{
    /// <summary>
    /// CallService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    //exe 程序调用接口
    public class CallService : System.Web.Services.WebService
    {
        /// <summary>
        /// 操作写卡
        /// </summary>
        /// <param name="kh"></param>
        /// <param name="yjxh"></param>
        /// <returns></returns>
        [WebMethod(Description = "写卡")]
        public object[] SetYjKh(string kh, string yjxh)
        {
            if (string.IsNullOrEmpty(kh) || string.IsNullOrEmpty(yjxh))
            {
                return new object[] { false, "卡号和阅卷序号不能为空" };
            }
            EDRS.BLL.YX_DZJZ_LSAJBD bll = new EDRS.BLL.YX_DZJZ_LSAJBD(HttpContext.Current.Request);
            try
            {
                DataSet ds = bll.GetYZYJZH(" and JDBMMC=:JDBMMC", new object[] { kh });           

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow[] dr = ds.Tables[0].Select("SQDZT='X'");
                        if (dr.Length > 0)
                            return new object[] { false, "该卡正在使用" };
                    }
                    EDRS.Model.YX_DZJZ_LSAJBD model = bll.GetModel(yjxh);
                    if (model != null)
                    {
                        model.JDBMMC = kh;
                        try
                        {
                            if (bll.Update(model))
                            {
                                EDRS.BLL.YX_DZJZ_LSYJSQ bllsq = new EDRS.BLL.YX_DZJZ_LSYJSQ(HttpContext.Current.Request);
                                List<EDRS.Model.YX_DZJZ_LSYJSQ> list = bllsq.GetModelList(" and LSZH=:LSZH",new object[]{model.GH});
                                if (list != null && list.Count > 0)
                                {
                                    list[0].SQDZT = "X";
                                    if (bllsq.Update(list[0]))
                                        return new object[] { true, "写卡成功" };
                                    else
                                    {
                                        model.JDBMMC = "";
                                        bll.Update(model);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            return new object[] { false, ex.Message };
                        }
                        return new object[] { false, "写卡失败" };
                    }
                    return new object[] { false, "阅卷信息不存在" };
                }
            }
            catch (Exception ex)
            {
                return new object[] { false, ex.Message };
            }
            return new object[] { false, "验证卡号失败" };
        }
             

        #region 阅卷用户验证
        /// <summary>
        /// 阅卷用户验证
        /// </summary>
        [WebMethod(Description = "阅卷用户验证")]
        public object[] YjUserVerification(string kh)
        {
            if (string.IsNullOrEmpty(kh))
            {
                return new object[] { false, "卡号不能为空" };
            }
            EDRS.BLL.YX_DZJZ_LSAJBD bll = new EDRS.BLL.YX_DZJZ_LSAJBD(HttpContext.Current.Request);
            DataSet ds = bll.GetYZYJZH(" and JDBMMC=:JDBMMC", new object[] { kh });
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];

//F:未提交
//T:已提交
//Y:通过审核
//N:未通过审核
//D:已阅卷

                switch (dt.Rows[0]["SQDZT"].ToString())
                {
                    case "Y":
                        return new object[] { false, "已审核还未写卡" };
                    case "N":
                        return new object[] { false, "阅卷未通过审核" };
                    case "X":
                        string url = "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + "/Pages/LSYJ/LSFPSJ.aspx?yjxh=" + dt.Rows[0]["YJXH"] + "&bmsah=" + dt.Rows[0]["BMSAH"] + "&ajmc=" + dt.Rows[0]["AJMC"] + "&ajbh=" + dt.Rows[0]["BMSAH"] + "&lszh=" + dt.Rows[0]["LSZH"] + "&yjxh=" + dt.Rows[0]["YJXH"];
                        return new object[] { true, url };
                    case "D":
                        return new object[] { false, "此卡已失效,请联系管理员" };
                    default:
                        return new object[] { false, "阅卷申请未审核" };
                }
              
            }
            return new object[] { false, "阅卷信息不存在" };
        } 
        #endregion
    }
}
