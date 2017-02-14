using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EDRS.Common
{
    public class TreeJson
    {
        public StringBuilder ResultJson { get; set; }
       
        /// <summary>
        /// 是否展开
        /// </summary>
        private bool IsOpen = false;
        /// <summary>
        /// 是否显示所有字段
        /// </summary>
        private bool IsNameAll = false;
        /// <summary>
        /// 父级编号值
        /// </summary>
        private string PidValue = "";
        /// <summary>
        /// 完整父级编码
        /// </summary>
        private string ParentId = "";
        /// <summary>
        /// 是否需要更具ISLEAF字段追加空节点
        /// </summary>
        private bool IsLeaf = true;
        /// <summary>
        /// 设置样式字段
        /// </summary>
        private string StyleCol = "";
        /// <summary>
        /// 设置样式字段对应的样式
        /// </summary>
        private Dictionary<string, string> StyleValues = null;

        /// <summary>
        /// 是否选中CheckBox
        /// </summary>
        private bool IsChecked = false;

        #region 树形结构数据构造
        /// <summary>
        /// 树形结构数据构造
        /// </summary>
        /// <param name="table">数据结构</param>
        /// <param name="idcol">ID列</param>
        /// <param name="txtCol">名称列</param>
        /// <param name="rela">父级列名称</param>
        /// <param name="leafCol">是否包含子节点列名</param>
        /// <param name="pid">父级编号</param>
        public TreeJson(DataTable table, string idcol, string txtCol, string rela, string leafCol, object pid, bool isOpen)
        {
            this.IsOpen = isOpen;
            this.PidValue = pid.ToString();
            GetTreeJsonByTable(table, idcol, txtCol, rela, "", leafCol, pid);
            ResultJson = result;

        } 
        #endregion

        #region 树形结构数据构造
        /// <summary>
        /// 树形结构数据构造
        /// </summary>
        /// <param name="table">数据结构</param>
        /// <param name="idcol">ID列</param>
        /// <param name="txtCol">名称列</param>
        /// <param name="rela">父级列名称</param>
        /// <param name="leafCol">是否包含子节点列名</param>
        /// <param name="parentId">完整父级编码</param>
        /// <param name="pid">父级编号</param>
        /// <param name="isOpen">是否全展开</param>
        /// <param name="isOpen">是否包含所有字段</param>
        public TreeJson(DataTable table, string idcol, string txtCol, string rela, string leafCol, string parentId, object pid, bool isOpen, bool isNameAll)
        {
            this.IsOpen = isOpen;
            this.IsNameAll = isNameAll;
            this.PidValue = pid.ToString();
            this.ParentId = parentId;
            GetTreeJson(table, idcol, txtCol, rela, "", leafCol, pid);

        } 
        #endregion

        #region 树形结构数据构造
        /// <summary>
        /// 树形结构数据构造
        /// </summary>
        /// <param name="table">数据结构</param>
        /// <param name="idcol">ID列</param>
        /// <param name="txtCol">名称列</param>
        /// <param name="rela">父级列名称</param>
        /// <param name="leafCol">是否包含子节点列名</param>
        /// <param name="parentId">完整父级编码</param>
        /// <param name="pid">父级编号</param>
        /// <param name="isOpen">是否全展开</param>
        /// <param name="isOpen">是否包含所有字段</param>
        public TreeJson(DataTable table, string idcol, string txtCol, string rela, string leafCol, string parentId, object pid, bool isOpen, bool isNameAll, string styleCol, Dictionary<string, string> styleValues, bool ischecked)
        {
            this.IsOpen = isOpen;
            this.IsNameAll = isNameAll;
            this.PidValue = pid.ToString();
            this.ParentId = parentId;
            this.StyleCol = styleCol;
            this.StyleValues = styleValues;
            this.IsChecked = ischecked;
            GetTreeJson(table, idcol, txtCol, rela, "", leafCol, pid);

        }
        #endregion

        #region 树形结构数据构造
        /// <summary>
        /// 树形结构数据构造
        /// </summary>
        /// <param name="table">数据结构</param>
        /// <param name="idcol">ID列</param>
        /// <param name="txtCol">名称列</param>
        /// <param name="rela">父级列名称</param>
        /// <param name="leafCol">是否包含子节点列名</param>
        /// <param name="parentId">完整父级编码</param>
        /// <param name="pid">父级编号</param>
        /// <param name="isOpen">是否全展开</param>
        /// <param name="isOpen">是否包含所有字段</param>
        /// <param name="isLeaf">是否判断增加子级空字段</param>
        public TreeJson(DataTable table, string idcol, string txtCol, string rela, string url, string leafCol, string parentId, object pid, bool isOpen, bool isNameAll, bool isLeaf)
        {
            this.IsLeaf = isLeaf;
            this.IsOpen = isOpen;
            this.IsNameAll = isNameAll;
            this.PidValue = pid.ToString();
            this.ParentId = parentId;
            GetTreeJson(table, idcol, txtCol, rela, url, leafCol, pid);

        } 
        #endregion

        #region 树形结构数据构造
        /// <summary>
        /// 树形结构数据构造
        /// </summary>
        /// <param name="table">数据结构</param>
        /// <param name="idcol">ID列</param>
        /// <param name="txtCol">名称列</param>
        /// <param name="rela">父级列名称</param>
        /// <param name="leafCol">是否包含子节点列名</param>
        /// <param name="parentId">完整父级编码</param>
        /// <param name="pid">父级编号</param>
        /// <param name="isOpen">是否全展开</param>
        /// <param name="isOpen">是否包含所有字段</param>       
        public TreeJson(DataTable table, string idcol, string txtCol, string rela, string url, string leafCol, string parentId, object pid, bool isOpen, bool isNameAll)
        {
            this.IsOpen = isOpen;
            this.IsNameAll = isNameAll;
            this.PidValue = pid.ToString();
            this.ParentId = parentId;
            GetTreeJson(table, idcol, txtCol, rela, url, leafCol, pid);

        } 
        #endregion

        #region 构造树形
        /// <summary>
        /// 构造树形
        /// </summary>
        /// <param name="table"></param>
        /// <param name="idcol"></param>
        /// <param name="txtCol"></param>
        /// <param name="rela"></param>
        /// <param name="url"></param>
        /// <param name="leafCol"></param>
        /// <param name="pid"></param>
        private void GetTreeJson(DataTable table, string idcol, string txtCol, string rela, string url, string leafCol, object pid)
        {
            table.Columns.Add("TbUseState");
            bactb = table.Clone();
            bactb.Clear();
            GetTreeJsonByTable(table, idcol, txtCol, rela, url, leafCol, pid);
            ResultJson = new StringBuilder();
            ResultJson.Append(result.ToString());

            DataRow[] dr = table.Select("TbUseState is null");
            foreach (DataRow row in dr)
                bactb.ImportRow(row);

            if (bactb != null && bactb.Rows.Count > 0)
            {
                DataView dv = bactb.DefaultView;
                bactb = dv.ToTable(true, new string[] { rela });
                foreach (DataRow row in bactb.Rows)
                {
                    StringBuilder temp = new StringBuilder();
                    result.Clear();
                    GetTreeJsonByTable(table, idcol, txtCol, rela, url, leafCol, row[rela]);
                    temp = result.Replace("[", ",").Remove(result.Length - 1, 1);
                    ResultJson.Insert(ResultJson.Length - 1, temp);
                }
            }
            if (table.Columns.Contains("TbUseState"))
                table.Columns.Remove("TbUseState");
        } 
        #endregion

        #region 构造树形结构
        

        private StringBuilder result = new StringBuilder();
        private StringBuilder sb = new StringBuilder();
        private DataTable bactb = new DataTable();
        /// <summary>
        /// 树形结构数据构造
        /// </summary>
        /// <param name="table">数据结构</param>
        /// <param name="idcol">ID列</param>
        /// <param name="txtCol">名称列</param>
        /// <param name="rela">父级列名称</param>
        /// <param name="leafCol">是否包含子节点列名</param>
        /// <param name="pid">父级编号</param>
        private void GetTreeJsonByTable(DataTable table, string idcol, string txtCol, string rela,string url, string leafCol, object pid)
        {
            result.Append(sb.ToString());
            sb.Clear();
            if (table.Rows.Count > 0)
            {
                sb.Append("[");
                string filer = string.Format("{0}='{1}'", rela, pid);
                if (string.IsNullOrEmpty(pid.ToString()))
                    filer = string.Format("{0} is NULL", rela);

                DataRow[] rows = table.Select(filer);
                if (rows.Length > 0)
                {
                    foreach (DataRow row in rows)
                    {
                        row["TbUseState"] = "1";
                        string name = row[txtCol].ToString();
                        sb.Append("{\"id\":\"" + row[idcol] + "\",\"text\":\"" + row[txtCol].ToString().Replace("\n", "") + "\"");
                        //判断是否设置名称样式
                        if (!string.IsNullOrEmpty(StyleCol))
                        {
                            sb.Append(",\"style\":\"" + SetStyle(row[StyleCol].ToString()) + "\"");
                        }
                        //判断是否选中CheckBox
                        if (IsChecked)
                        {
                            if (!table.Columns.Contains("ischecked"))
                                sb.Append(",\"ischecked\":true");
                            else
                                row["ischecked"] = true;
                        }
                        if (row[idcol].ToString().Length >= 6)
                        {
                            sb.Append(",\"iconCls\":\"tree_dw\"");//iconCls
                            sb.Append(",\"icon\":\"tree_dw\"");
                        }
                        else if (row[idcol].ToString().Length == 4)
                        {
                            sb.Append(",\"iconCls\":\"tree_bm\"");//iconCls
                            sb.Append(",\"icon\":\"tree_bm\"");
                        }
                        else if (row[idcol].ToString().Length == 3)
                        {
                            sb.Append(",\"iconCls\":\"tree_js\"");//iconCls
                            sb.Append(",\"icon\":\"tree_js\"");
                        }
                        if (!string.IsNullOrEmpty(url))
                            sb.Append(",\"url\":\"" + row[url].ToString() + "\"");
                        //判断是否构造table中的所有字段
                        if (IsNameAll)
                        {
                            //循环构造table中的字段
                            foreach (DataColumn com in table.Columns)
                            {
                                sb.AppendFormat(",\"{0}\":\"{1}\"", com.ColumnName, row[com.ColumnName].ToString().Replace("\\","\\\\"));
                            }
                        }
                        //判断当前编号是否存在下级目录
                        if (table.Select(string.Format("{0}='{1}'", rela, row[idcol])).Length > 0)
                        {
                            if (this.IsOpen || this.ParentId == row[rela].ToString() || row[leafCol].ToString() == "1")
                            {
                                sb.Append(",\"state\":\"open\"");
                                //sb.Append(",\"isExpand\":\"true\"");
                            }
                            else
                                sb.Append(",\"state\":\"closed\"");
                            sb.Append(",\"children\":");
                            GetTreeJsonByTable(table, idcol, txtCol, rela,url, leafCol, row[idcol]);
                            result.Append(sb.ToString());
                            sb.Clear();
                        }
                        else if (IsLeaf && table.Columns.Contains("ISLEAF") && int.Parse(row["ISLEAF"].ToString()) == 0)
                        {
                            sb.Append(",\"children\":[]");
                        }
                        else
                        {
                            if (!this.IsOpen && leafCol != "" && row[leafCol].ToString() == "0")
                                sb.Append(",\"state\":\"closed\"");
                        }
                        result.Append(sb.ToString());
                        sb.Clear();
                        sb.Append("},");

                    }
                    sb = sb.Remove(sb.Length - 1, 1);
                }
                sb.Append("]");
                result.Append(sb.ToString());
                sb.Clear();
            }
        } 
        #endregion

        #region 获取键对应的样式
        /// <summary>
        /// 获取键对应的样式
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private string SetStyle(string key)
        {
            foreach (KeyValuePair<string, string> item in StyleValues)
            {
                if (item.Key == key)
                {
                    return item.Value;
                }
            }
            return "";
        } 
        #endregion
    }
}
