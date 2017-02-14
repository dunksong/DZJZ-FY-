using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Cyvation.CCQE.Common
{
    public static class JsonHelper
    {
        public static string ToTreeJson(this DataTable data, string idCol, string pidCol, string nameCol, string rootId = "")
        {
            return ToTreeJsonData(data, idCol, pidCol, nameCol, null, rootId);
        }
        public static string ToTreeJson(this DataTable data, string idCol, string pidCol, string nameCol,Dictionary<int,string> icons, string rootId = "")
        {
            return ToTreeJsonData(data, idCol, pidCol, nameCol, icons, rootId);
        }
        private static string ToTreeJsonData(this DataTable data, string idCol, string pidCol, string nameCol, Dictionary<int,string> icons,string rootId = "")
        {
            if (data == null) return null;
            if (string.IsNullOrEmpty(idCol) || string.IsNullOrEmpty(pidCol) || string.IsNullOrEmpty(nameCol)) return "";

            StringBuilder sbJson = new StringBuilder();
            DataRow[] drs = null;
            if (string.IsNullOrEmpty(rootId))
            {
                drs = data.Select(pidCol + " is null or " + pidCol + " = '' ");
            }
            else
            {
                drs = data.Select(pidCol + " = '" + rootId + "'");
            }
            sbJson.Append("[");
            foreach (DataRow dr in drs)
            {
                sbJson.Append("{");
                sbJson.AppendFormat("\"id\":\"{0}\", \"text\":\"{1}\"", Convert.ToString(dr[idCol]), Convert.ToString(dr[nameCol]));
                if (icons != null)
                {
                    foreach (var icon in icons)
                    {
                        if (icon.Key == dr[idCol].ToString().Length)
                        {
                            sbJson.Append(",\"icon\":\"" + icon.Value + "\"");
                            break;
                        }
                    }
                }
                if (data.Select(pidCol + " = '" + Convert.ToString(dr[idCol]) + "'").Count() > 0)
                {
                    sbJson.AppendFormat(", \"state\":\"closed\",\"children\":{0}", data.ToTreeJsonData(idCol, pidCol, nameCol, icons, Convert.ToString(dr[idCol])));
                }
                sbJson.Append("},");
            }
            string json = sbJson.ToString().TrimEnd(',') + "]";
            return json;
        }

        public static string ToTreeJsonAll(this DataTable data, string idCol, string pidCol, string nameCol, string rootId = "")
        {
            if (data == null) return null;
            DataTable dt = data.Copy();
            if (string.IsNullOrEmpty(idCol) || string.IsNullOrEmpty(pidCol) || string.IsNullOrEmpty(nameCol)) return "";

            StringBuilder sbJson = new StringBuilder();
            DataRow[] drs = null;
            if (string.IsNullOrEmpty(rootId))
            {
                drs = data.Select(pidCol + " is null or " + pidCol + " = '' ");
            }
            else
            {
                drs = data.Select(pidCol + " = '" + rootId + "'");
            }
            sbJson.Append("[");
            foreach (DataRow dr in drs)
            {
                sbJson.Append("{");
                //sbJson.AppendFormat("\"id\":\"{0}\", \"text\":\"{1}\", \"test\":\"aaaa\"", Convert.ToString(dr[idCol]), Convert.ToString(dr[nameCol]));
                string text = "";
                foreach (string s in nameCol.Split(','))
                {
                    text += string.IsNullOrEmpty(text) ? Convert.ToString(dr[s]) : "、" + Convert.ToString(dr[s]);
                }
                sbJson.AppendFormat("\"id\":\"{0}\", \"text\":\"{1}\"", Convert.ToString(dr[idCol]), text);
                foreach (DataColumn item in dr.Table.Columns)
                {
                    if (item.ColumnName != idCol && item.ColumnName != nameCol)
                    {
                        sbJson.AppendFormat(", \"{0}\":\"{1}\"", item.ColumnName.ToLower(), Convert.ToString(dr[item.ColumnName]));
                    }
                }
                if (dt.Select(pidCol + " = '" + Convert.ToString(dr[idCol]) + "'").Count() > 0)
                {
                    sbJson.AppendFormat(", \"state\":\"closed\",\"children\":{0}", data.ToTreeJsonAll(idCol, pidCol, nameCol, Convert.ToString(dr[idCol])));
                }
                sbJson.Append("},");
            }
            string json = sbJson.ToString().TrimEnd(',') + "]";
            return json;
        }

        public static string ToTreeListJson(this DataTable data, string idCol, string pidCol, string nameCol, string rootId = "")
        {
            if (data == null) return null;
            if (string.IsNullOrEmpty(idCol) || string.IsNullOrEmpty(pidCol) || string.IsNullOrEmpty(nameCol)) return "";

            StringBuilder sbJson = new StringBuilder();
            DataRow[] drs = null;
            if (string.IsNullOrEmpty(rootId))
            {
                drs = data.Select(pidCol + " is null or " + pidCol + " = '' ");
            }
            else
            {
                drs = data.Select(pidCol + " = '" + rootId + "'");
            }
            sbJson.Append("[");
            foreach (DataRow dr in drs)
            {
                sbJson.Append("{");
                sbJson.AppendFormat("\"{0}\":\"{1}\", \"{2}\":\"{3}\"", idCol.ToLower(), Convert.ToString(dr[idCol]), nameCol.ToLower(), Convert.ToString(dr[nameCol]));

                foreach (DataColumn item in dr.Table.Columns)
                {
                    if (item.ColumnName != idCol && item.ColumnName != pidCol && item.ColumnName != nameCol)
                    {
                        sbJson.AppendFormat(", \"{0}\":\"{1}\"", item.ColumnName, Convert.ToString(dr[item.ColumnName]));
                    }
                }
                if (data.Select(pidCol + " = '" + Convert.ToString(dr[idCol]) + "'").Count() > 0)
                {
                    sbJson.AppendFormat(", \"state\":\"closed\",\"children\":{0}", data.ToTreeListJson(idCol, pidCol, nameCol, Convert.ToString(dr[idCol])));
                }
                sbJson.Append("},");
            }
            string json = sbJson.ToString().TrimEnd(',') + "]";
            return json;
        }

        public static string ToComboxJson(this DataTable dt)
        {
            StringBuilder strJson = new StringBuilder();
            int count = dt.Rows.Count;
            strJson.Append("[");
            for (int i = 0; i < count; i++)
            {
                strJson.Append("{");
                strJson.Append("\"id\"" + ":\"" + dt.Rows[i]["bm"] + "\",");
                strJson.Append("\"text\"" + ":" + "\"" + dt.Rows[i]["mc"] + "\"" + "}");
                if (i < count - 1)
                    strJson.Append(",");
            }
            strJson.Append("]");
            return strJson.ToString();
        }

        public static string ToDatagridJson(this DataTable data)
        {
            StringBuilder sbJson = new StringBuilder();
            sbJson.Append("[");
            foreach (DataRow dr in data.Rows)
            {
                sbJson.Append("{");
                StringBuilder sbRow = new StringBuilder();
                foreach (DataColumn c in data.Columns)
                {
                    sbRow.AppendFormat("\"{0}\":\"{1}\",", c.ColumnName.ToLower(), dr[c.ColumnName] == DBNull.Value ? "" : c.DataType == typeof(System.DateTime) ? Convert.ToDateTime(dr[c.ColumnName]).ToString("yyyy-MM-dd") : Convert.ToString(dr[c.ColumnName]).Replace("\n", "<br>"));
                }
                sbJson.Append(sbRow.ToString().TrimEnd(','));
                sbJson.Append("},");
            }
            string json = sbJson.ToString().TrimEnd(',') + "]";
            return json;
        }

        public static string ConvertToStrJson(this DataSet ds)
        {
            string json = string.Empty;
            StringBuilder sbJson = new StringBuilder();
            sbJson.Append("{");
            foreach (DataTable dt in ds.Tables)
            {
                sbJson.AppendFormat("\"{0}\":{1},", dt.TableName, dt.ToDatagridJson());
            }
            json = sbJson.ToString().TrimEnd(',') + "}";
            return json;
        }

        /// <summary>
        /// 转化有分组的下拉列表
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="isHasChild"></param>
        /// <returns></returns>
        public static string ToComboxJson(this DataTable dt, bool isHasGroup)
        {
            if (!isHasGroup)
            {
                return dt.ToComboxJson();
            }
            StringBuilder strJson = new StringBuilder();
            int count = dt.Rows.Count;
            strJson.Append("[");
            for (int i = 0; i < count; i++)
            {
                strJson.Append("{");
                strJson.Append("\"id\"" + ":\"" + dt.Rows[i]["bm"] + "\",");
                strJson.Append("\"text\"" + ":" + "\"" + dt.Rows[i]["mc"] + "\"" + ",");
                string FBM = dt.Rows[i]["fbm"].ToString();
                DataRow[] drs = dt.Select("bm" + " = '" + FBM + "'");
                if (drs.Length > 0)
                {
                    strJson.Append("\"group\"" + ":" + "\"" + drs[0]["mc"] + "\"");
                }
                else
                {
                    strJson.Append("\"group\"" + ":" + "\"" + dt.Rows[i]["mc"] + "\"");
                }
                strJson.Append("}");
                if (i < count - 1)
                    strJson.Append(",");
            }
            strJson.Append("]");
            return strJson.ToString();
        }
    }
}
