using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebUI.Pages.LSYJ
{
    public partial class LSDetails : System.Web.UI.Page
    {
        public string action = "AddData";
        public string _LSZH = "";
        public string _LSDW = "";
        public string _LSDWMC = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                action = Request["action"];
                _LSZH = Request["LSZH"];
                _LSDW = Request["LSDW"];
                _LSDWMC = Request["LSDWMC"];
            }
        }
    }
}