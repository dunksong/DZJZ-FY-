using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web.UI.WebControls;

namespace Cyvation.CCQE.Common
{
    /// <summary>
    /// 下拉列表辅助类
    /// </summary>
    public static class DropdownListHelper
    {
        /// <summary>
        /// 绑定Microsoft DropDwonList
        /// </summary>
        /// <param name="control">被绑定的控件</param>
        /// <param name="dt">数据源</param>
        /// <param name="dataValueField">值字段</param>
        /// <param name="dataTextField">文本字段</param>
        /// <param name="defaultText">默认选项文本内容</param>
        public static void BindDropDownList(this System.Web.UI.WebControls.DropDownList control, DataTable dt, string dataValueField, string dataTextField, string defaultText = "")
        {
            if (dt == null) return;
            DataRow row = dt.NewRow();
            if (!string.IsNullOrEmpty(defaultText))
            {
                row[dataTextField] = defaultText;
                row[dataValueField] = "";
                dt.Rows.InsertAt(row, 0);
            }
            if (dt.Rows.Count == 0)
                return;
            control.DataSource = dt;
            control.DataTextField = dataTextField;
            control.DataValueField = dataValueField;
            control.DataBind();
        }


        /// <summary>
        /// 绑定Microsoft DropDwonList
        /// </summary>
        /// <param name="control">被绑定的控件</param>
        /// <param name="source">数据源</param>
        /// <param name="dataValueField">值字段</param>
        /// <param name="dataTextField">文本字段</param>
        /// <param name="defaultText">默认选项文本内容</param>
        public static void BindDropDownList<T>(this System.Web.UI.WebControls.DropDownList control, IEnumerable<T> source, string dataValueField, string dataTextField, string defaultText = "")
        {
            if (source == null) return;
            control.DataSource = source;
            control.DataTextField = dataTextField;
            control.DataValueField = dataValueField;
            control.DataBind();
            if (!string.IsNullOrEmpty(defaultText))
            {
                System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem();
                item.Text = defaultText;
                item.Value = "";
                control.Items.Insert(0, item);
            }
        }
    }
}
