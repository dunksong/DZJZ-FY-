using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EDRS.BLL;
using System.Text;
using Cyvation.CCQE.Common;
using System.Web.Script.Serialization;
using System.Collections;
using System.Data.OracleClient;

namespace WebUI.Pages.Template
{
    public partial class ManagerSortIndex : System.Web.UI.Page
    {
        public string resultJson = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string t = Request["t"];
                if (t!= null && t.ToLower() == "validate")
                {
                    Response.Write(ValidateData(Request["id"]));
                }
                else if (t == "submint")
                {
                    
                    JavaScriptSerializer js = new JavaScriptSerializer();

                    List<DataList> data = (List<DataList>)js.Deserialize(Request["TreeData"].ToString(), typeof(List<DataList>));
                    SaveTempSort(data);
                }
                else
                {
                    resultJson = ListBind();
                }
            }
        }

        private void SaveTempSort(List<DataList> data)
        {
            Hashtable sqlList = new Hashtable();
            int index = 0;
            foreach (DataList item in data)
            {
                index++;
                string sql = "UPDATE XY_DZJZ_MBPZB SET SORTINDEX = "+index+" WHERE DOSSIERTYPEVALUEMEMBER='"+item.id+"'";
                sqlList.Add(sql, null);
                if (item.children != null && item.children.Count > 0)
                {
                    int childIndex = 0;
                    foreach (DataList _item in item.children)
                    {
                        childIndex++;
                        sql = "UPDATE XY_DZJZ_MBPZB SET SORTINDEX = " + childIndex + " , DOSSIERPARENTMEMBER='" + item.id + "' WHERE DOSSIERTYPEVALUEMEMBER='" + _item.id + "'";
                        sqlList.Add(sql, null);
                    }
                }
            }
            EDRS.BLL.XY_DZJZ_MBPZB bll = new XY_DZJZ_MBPZB(Request);
            bool result = bll.Update(sqlList);

            if (result)
            {
                Response.Write("保存成功！");
            }
            else
            {
                Response.Write("保存失败！");
            }
        }
        public class DataList
        {
            public string id;
            public List<DataList> children = new List<DataList>();
            
        }
        public bool ValidateData(string id)
        {
            return false;
        }
        private string ListBind()
        {
            //int pageNumber = 1; //int.Parse(Request["page"]);
            //int pageSize = int.MaxValue;// int.Parse(Request["rows"]);

            string where = string.Empty;

            //树形循环条件
            bool direction = true;
            bool isOpen = false;
            string withWhere = string.Empty;
            string levelNum = "";// " and level < 10 ";
            string isLeaf = "ISLEAF";
            XY_DZJZ_MBPZB bll = new XY_DZJZ_MBPZB(this.Request);
            object[] values = new object[1];

            //if (UserInfo != null)
            //{
            where = " and UNITID = :UNITID";
            values[0] = "370000";//UserInfo.DWBM;
            //}


            if (string.IsNullOrEmpty(withWhere))
                withWhere = " and DossierParentMember is NULL ";

            DataSet ds = bll.GetTreeList(where, withWhere, direction, values);
            string datetype = Request["datetype"];
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                string resultJson = dt.ToTreeJsonAll("DossierTypeValueMember", "DossierParentMember", "DossierTypeValueMember,DossierTypeDisplayMember", "");
                return resultJson;
            }
            return "";
        }
    }
}