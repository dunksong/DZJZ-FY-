using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EDRS.BLL;

namespace WebUI.Pages.Template
{
    public partial class ImportTemp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> SSLBList = new Dictionary<string, string>();
            EDRS.BLL.XT_DM_AJLBBM bll2 = new XT_DM_AJLBBM(Request);
            DataSet dsSSLB = bll2.GetSSLBList("", null);
            foreach (DataRow dr in dsSSLB.Tables[0].Rows)
            {
                SSLBList.Add(dr["SSLBBM"].ToString(), dr["SSLBMC"].ToString());
            }
            if (FileUpload1.HasFile)
            {
                string path = FileUpload1.PostedFile.FileName;
                DataSet ds = new DataSet();
                ds.ReadXml(path);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string number = "00000001";
                    XY_DZJZ_MBPZB bll = new XY_DZJZ_MBPZB(this.Request);
                    DataSet ds1 = bll.GetListByPage("", "DOSSIERTYPEVALUEMEMBER desc", 0, 1);
                    if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                    {
                        number = ds1.Tables[0].Rows[0]["DOSSIERTYPEVALUEMEMBER"].ToString();
                        number = (int.Parse(number) + 1).ToString().PadLeft(8, '0');
                    }
                    EDRS.Model.XY_DZJZ_MBPZB model = new EDRS.Model.XY_DZJZ_MBPZB();
                    model.DossierTypeValueMember = number;
                    model.CaseInfoTypeID = "0301";
                    model.CaseInfoTypeName = "一审公诉案件";
                    model.UnitID = "370000";
                    model.DossierTypeDisplayMember = dr["value"].ToString();
                    model.DossierParentMember = "";
                    model.DossierEvidenceValueMember = "";
                    model.SortIndex = Convert.ToInt32(dr["index"]);
                    model.Category = "J";
                    model.SSLBBM = dr["key"].ToString();
                    model.SSLBMC = SSLBList[model.SSLBBM];
                    model.Auto = "N";
                    string pid = number;
                    EDRS.BLL.XY_DZJZ_MBPZB bll1 = new EDRS.BLL.XY_DZJZ_MBPZB(Request);
                    if (bll1.Add(model))
                    { 
                        string key = dr["key"].ToString();
                        key = key.Split('-')[1];
                        DataRow[] drs = ds.Tables[1].Select("key like '" + key + "-%'");
                        foreach (DataRow dr1 in drs)
                        {

                            model.CaseInfoTypeID = "0301";
                            model.CaseInfoTypeName = "一审公诉案件";
                            model.UnitID = "370000";
                            model.DossierTypeDisplayMember = dr1["value"].ToString();
                            model.DossierParentMember = pid;
                            model.DossierEvidenceValueMember = "";


                            ds1 = bll.GetListByPage("", "DOSSIERTYPEVALUEMEMBER desc", 0, 1);
                            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                            {
                                number = ds1.Tables[0].Rows[0]["DOSSIERTYPEVALUEMEMBER"].ToString();
                                number = (int.Parse(number) + 1).ToString().PadLeft(8, '0');
                            }
                            model.DossierTypeValueMember = number;
                            model.SortIndex = Convert.ToInt32(dr1["index"]); ;
                            model.Category = "W";
                            model.SSLBBM = dr1["key"].ToString();
                            model.SSLBMC = SSLBList[model.SSLBBM];
                            model.Auto = "N";
                            bll1.Add(model);
                        }
                    }
                }
            }
        }
    }
}