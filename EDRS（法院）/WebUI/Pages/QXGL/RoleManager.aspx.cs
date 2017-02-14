using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebUI.Pages.QXGL
{
    public partial class RoleManager : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            jsbm.Value = Request.QueryString["_jsbm"];
            bmbm.Value = Request.QueryString["_bmbm"];
            dwbm.Value = Request.QueryString["_dwbm"];
            //_jsbm
            //_dwbm

        }
    }
}