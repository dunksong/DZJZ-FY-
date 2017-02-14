using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LitJson;
using System.Web.SessionState;

namespace Cyvation.CCQE.Web
{
    /// <summary>
    /// ZZJGCommonHandler 的摘要说明
    /// </summary>
    public class ZZJGCommonHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
          
            string action = context.Request.Params["action"];
            switch (action)
            {
                case "Time":
                    EDRS.Model.YX_DZJZ_LSAJBD model = context.Session["YjData"] as EDRS.Model.YX_DZJZ_LSAJBD;
                    if (model != null && model.YJJSSJ != null && model.YJJSSJ.Value.CompareTo(DateTime.Now) > 0)
                    {
                        TimeSpan ts = model.YJJSSJ.Value.Subtract(DateTime.Now);
                        context.Response.Write((ts.Days > 0 ? ts.Days + "天 " : "") + ts.Hours.ToString().PadLeft(2, '0') + ":" + ts.Minutes.ToString().PadLeft(2, '0') + ":" + ts.Seconds.ToString().PadLeft(2, '0'));
                    }
                    else
                    {
                        context.Session["YjData"] = null;
                        context.Response.Write("N");
                    }
                    break;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        
    }
}