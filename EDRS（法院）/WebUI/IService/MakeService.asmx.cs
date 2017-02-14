using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;

namespace WebUI.IService
{
    /// <summary>
    /// MakeService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class MakeService : System.Web.Services.WebService
    {

        [WebMethod]
        public DataTable GetZzParam(string gid)
        {
            EDRS.BLL.XT_DZJZ_ZZCS bll = new EDRS.BLL.XT_DZJZ_ZZCS(HttpContext.Current.Request);
            DataSet ds = bll.GetList(" and FZBS=:FZBS",new object[]{gid});
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0];
            return null;
        }

        [WebMethod]
        public bool DelZzParam(string gid)
        {
            EDRS.BLL.XT_DZJZ_ZZCS bll = new EDRS.BLL.XT_DZJZ_ZZCS(HttpContext.Current.Request);            
            if (bll.Delete(gid))
                return true;
            return false;
        }

        [WebMethod]
        public void SetLog(string typeId, string dwbm, string dwmc, string gh, string mc, string bmbm, string bmmc, string msg, string billTag)
        {
            EDRS.Model.YX_DZJZ_JZRZJL entity = new EDRS.Model.YX_DZJZ_JZRZJL();
            EDRS.BLL.YX_DZJZ_JZRZJL rzBll = new EDRS.BLL.YX_DZJZ_JZRZJL(HttpContext.Current.Request);
            entity.CZLX = typeId;
            entity.DWBM = dwbm;
            entity.DWMC = dwmc;
            entity.CZRGH = gh;
            entity.CZR = mc;
            entity.BMBM = bmbm;
            entity.BMMC = bmmc;
            entity.RZNR = msg;
            entity.CZAJBMSAH = billTag == null ? "" : billTag;
            rzBll.Add(entity);
        }

        

    }
}
