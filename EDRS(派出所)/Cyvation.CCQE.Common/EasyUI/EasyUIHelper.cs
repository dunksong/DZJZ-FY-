using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;

namespace Cyvation.CCQE.Common
{
    /// <summary>
    /// JQuery.EasyUI辅助类
    /// </summary>
    public class EasyUIHelper
    {
        /// <summary>
        /// 构造DataGrid数据源
        /// </summary>
        /// <param name="total">记录数,如果传入null则取dt的记录数</param>
        /// <param name="dt"></param>
        /// <param name="format">格式键值集合</param>
        /// <returns>DataGrid数据源字符串</returns>
        public static string BuildDataGridDataSource(int? total, DataTable dt, Dictionary<string, string> format)
        {
            #region

            StringBuilder builder = new StringBuilder();
            builder.Append("{\"total\":" + (total.HasValue ? total.Value.ToString() :
                (((dt == null) || (dt.Rows.Count == 0)) ? "0" : dt.Rows.Count.ToString())) + ",\"rows\":[");

            if ((dt == null) || (dt.Rows.Count == 0))
            {
                return builder.ToString() + "]}";
            }

            string nameValuePairs, strValue;

            foreach (DataRow dr in dt.Rows)
            {
                builder.Append("{");
                nameValuePairs = string.Empty;

                foreach (DataColumn col in dt.Columns)
                {
                    if (Convert.IsDBNull(dr[col]) || (dr[col] == null))
                    {
                        strValue = string.Empty;
                    }
                    else if ((format == null) || !format.ContainsKey(col.ColumnName))
                    {
                        strValue = dr[col].ToString();
                    }
                    else
                    {
                        switch (col.DataType.Name.ToLower())
                        {
                            case "datetime":
                                strValue = Convert.ToDateTime(dr[col]).ToString(format[col.ColumnName], System.Globalization.DateTimeFormatInfo.InvariantInfo);
                                break;
                            case "decimal":
                                strValue = Convert.ToDecimal(dr[col]).ToString(format[col.ColumnName]);
                                break;
                            case "double":
                                strValue = Convert.ToDouble(dr[col]).ToString(format[col.ColumnName]);
                                break;
                            case "float":
                                strValue = Convert.ToSingle(dr[col]).ToString(format[col.ColumnName]);
                                break;
                            default:
                                strValue = dr[col].ToString();
                                break;
                        }
                    }

                    nameValuePairs += string.Format("\"{0}\":\"{1}\",", col.ColumnName, strValue);
                }

                builder.Append(nameValuePairs.Remove(nameValuePairs.Length - 1) + "},");
            }

            return builder.Remove(builder.Length - 1, 1).ToString() + "]}";

            #endregion
        }

        /// <summary>
        /// 构造DataGrid数据源
        /// </summary>
        /// <param name="total">记录数</param>
        /// <param name="dt"></param>
        /// <returns>DataGrid数据源字符串</returns>
        public static string BuildDataGridDataSource(int total, DataTable dt)
        {
            return BuildDataGridDataSource(total, dt, null);
        }

        /// <summary>
        /// 构造DataGrid数据源
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="format">格式键值集合</param>
        /// <returns>DataGrid数据源字符串</returns>
        public static string BuildDataGridDataSource(DataTable dt, Dictionary<string, string> format)
        {
            return BuildDataGridDataSource(null, dt, format);
        }

        /// <summary>
        /// 构造DataGrid数据源
        /// </summary>
        /// <param name="total">记录数,如果传入null则取list的记录数</param>
        /// <param name="displayFileds">显示字段</param>
        /// <param name="list"></param>
        /// <param name="format">格式键值集合</param>
        /// <returns>DataGrid数据源字符串</returns>
        public static string BuildDataGridDataSource(int? total, string displayFileds, IList<List<object>> list, Dictionary<int, string> format)
        {
            return BuildDataGridDataSource(total, displayFileds.Split(','), list, format);
        }

        /// <summary>
        /// 构造DataGrid数据源
        /// </summary>
        /// <param name="total">记录数,如果传入null则取list的记录数</param>
        /// <param name="displayFileds">显示字段</param>
        /// <param name="list"></param>
        /// <returns>DataGrid数据源字符串</returns>
        public static string BuildDataGridDataSource(int total, string displayFileds, IList<List<object>> list)
        {
            return BuildDataGridDataSource(total, displayFileds, list, null);
        }

        /// <summary>
        /// 构造DataGrid数据源
        /// </summary>
        /// <param name="displayFileds">显示字段</param>
        /// <param name="list"></param>
        /// <param name="format">格式键值集合</param>
        /// <returns>DataGrid数据源字符串</returns>
        public static string BuildDataGridDataSource(string displayFileds, IList<List<object>> list, Dictionary<int, string> format)
        {
            return BuildDataGridDataSource(list.Count, displayFileds, list, format);
        }

        /// <summary>
        /// 构造DataGrid数据源
        /// </summary>
        /// <param name="displayFileds">显示字段</param>
        /// <param name="list"></param>
        /// <returns>DataGrid数据源字符串</returns>
        public static string BuildDataGridDataSource(string displayFileds, IList<List<object>> list)
        {
            return BuildDataGridDataSource(list.Count, displayFileds, list, null);
        }

        /// <summary>
        /// 构造DataGrid数据源
        /// </summary>
        /// <param name="total">记录数,如果传入null则取list的记录数</param>
        /// <param name="fileds">显示字段</param>
        /// <param name="list"></param>
        /// <param name="format">格式键值集合</param>
        /// <returns>DataGrid数据源字符串</returns>
        public static string BuildDataGridDataSource(int? total, string[] fileds, IList<List<object>> list, Dictionary<int, string> format)
        {
            #region

            StringBuilder builder = new StringBuilder();
            builder.Append("{\"total\":" + (total.HasValue ? total.Value.ToString() :
                (((list == null) || (list.Count == 0)) ? "0" : list.Count.ToString())) + ",\"rows\":[");

            if ((list == null) || (list.Count == 0))
            {
                return builder.ToString() + "]}";
            }

            string nameValuePairs, strValue;

            foreach (List<object> values in list)
            {
                builder.Append("{");
                nameValuePairs = string.Empty;

                for (int i = 0; i < fileds.Length; i++)
                {
                    if (Convert.IsDBNull(values[i]) || (values[i] == null))
                    {
                        strValue = string.Empty;
                    }
                    else if ((format == null) || !format.ContainsKey(i))
                    {
                        strValue = values[i].ToString();
                    }
                    else
                    {
                        switch (values[i].GetType().Name.ToLower())
                        {
                            case "datetime":
                                strValue = Convert.ToDateTime(values[i]).ToString(format[i], System.Globalization.DateTimeFormatInfo.InvariantInfo);
                                break;
                            case "decimal":
                                strValue = Convert.ToDecimal(values[i]).ToString(format[i]);
                                break;
                            case "double":
                                strValue = Convert.ToDouble(values[i]).ToString(format[i]);
                                break;
                            case "float":
                                strValue = Convert.ToSingle(values[i]).ToString(format[i]);
                                break;
                            default:
                                strValue = values[i].ToString();
                                break;
                        }
                    }

                    nameValuePairs += string.Format("\"{0}\":\"{1}\",", fileds[i], strValue);
                }

                builder.Append(nameValuePairs.Remove(nameValuePairs.Length - 1) + "},");
            }

            return builder.Remove(builder.Length - 1, 1).ToString() + "]}";

            #endregion
        }

        /// <summary>
        /// 构造DataGrid数据源
        /// </summary>
        /// <param name="total">记录数,如果传入null则取list的记录数</param>
        /// <param name="fileds">显示字段</param>
        /// <param name="list"></param>
        /// <returns>DataGrid数据源字符串</returns>
        public static string BuildDataGridDataSource(int total, string[] fileds, IList<List<object>> list)
        {
            return BuildDataGridDataSource(total, fileds, list, null);
        }

        /// <summary>
        /// 构造DataGrid数据源
        /// </summary>
        /// <param name="fileds">显示字段</param>
        /// <param name="list"></param>
        /// <param name="format">格式键值集合</param>
        /// <returns>DataGrid数据源字符串</returns>
        public static string BuildDataGridDataSource(string[] fileds, IList<List<object>> list, Dictionary<int, string> format)
        {
            return BuildDataGridDataSource(list.Count, fileds, list, format);
        }

        /// <summary>
        /// 构造DataGrid数据源
        /// </summary>
        /// <param name="fileds">显示字段</param>
        /// <param name="list"></param>
        /// <returns>DataGrid数据源字符串</returns>
        public static string BuildDataGridDataSource(string[] fileds, IList<List<object>> list)
        {
            return BuildDataGridDataSource(list.Count, fileds, list, null);
        }

        /// <summary>
        /// 构造DataGrid数据源
        /// </summary>
        /// <param name="total">记录数,如果传入null则取instances的记录数</param>
        /// <param name="fileds">显示字段</param>
        /// <param name="instances">对象实例集合</param>
        /// <param name="format">格式键值集合</param>
        /// <returns>DataGrid数据源字符串</returns>
        public static string BuildDataGridDataSource(int? total, string[] fileds, IList<object> instances, Dictionary<string, string> format)
        {
            #region

            StringBuilder builder = new StringBuilder();
            builder.Append("{\"total\":" + (total.HasValue ? total.Value.ToString() :
                (((instances == null) || (instances.Count == 0)) ? "0" : instances.Count.ToString())) + ",\"rows\":[");

            if ((instances == null) || (instances.Count == 0))
            {
                return builder.ToString() + "]}";
            }

            PropertyInfo[] prpts;
            string strPrptType, strValue, nameValuePairs;

            foreach (object instance in instances)
            {
                if (instance != null)
                {
                    builder.Append("{");
                    prpts = instance.GetType().GetProperties();
                    nameValuePairs = string.Empty;

                    foreach (PropertyInfo prpt in prpts)
                    {
                        if ((fileds == null) || ((fileds != null)))// && fileds.Contains(prpt.Name)
                        {
                            strPrptType = prpt.PropertyType.Name.ToString().ToLower();

                            if (prpt.GetValue(instance, null) == null)
                            {
                                strValue = string.Empty;
                            }
                            else if ((format == null) || !format.ContainsKey(prpt.Name))
                            {
                                strValue = prpt.GetValue(instance, null).ToString();
                            }
                            else
                            {
                                switch (strPrptType)
                                {
                                    case "datetime":
                                        strValue = Convert.ToDateTime(prpt.GetValue(instance, null)).ToString(format[prpt.Name], System.Globalization.DateTimeFormatInfo.InvariantInfo);
                                        break;
                                    case "decimal":
                                        strValue = Convert.ToDecimal(prpt.GetValue(instance, null)).ToString(format[prpt.Name]);
                                        break;
                                    case "double":
                                        strValue = Convert.ToDouble(prpt.GetValue(instance, null)).ToString(format[prpt.Name]);
                                        break;
                                    case "float":
                                        strValue = Convert.ToSingle(prpt.GetValue(instance, null)).ToString(format[prpt.Name]);
                                        break;
                                    default:
                                        strValue = prpt.GetValue(instance, null).ToString();
                                        break;
                                }
                            }

                            nameValuePairs += string.Format("\"{0}\":\"{1}\",", prpt.Name, strValue);
                        }
                    }

                    builder.Append(nameValuePairs.Remove(nameValuePairs.Length - 1) + "},");
                }
            }

            return builder.Remove(builder.Length - 1, 1).ToString() + "]}";

            #endregion
        }

        /// <summary>
        /// 构造DataGrid数据源
        /// </summary>
        /// <param name="total">记录数,如果传入null则取instances的记录数</param>
        /// <param name="fileds">显示字段</param>
        /// <param name="instances">对象实例集合</param>
        /// <returns>DataGrid数据源字符串</returns>
        public static string BuildDataGridDataSource(int total, string[] fileds, IList<object> instances)
        {
            return BuildDataGridDataSource(total, fileds, instances, null);
        }

        /// <summary>
        /// 构造DataGrid数据源
        /// </summary>
        /// <param name="fileds">显示字段</param>
        /// <param name="instances"></param>
        /// <param name="format">格式键值集合</param>
        /// <returns>DataGrid数据源字符串</returns>
        public static string BuildDataGridDataSource(string[] fileds, IList<object> instances, Dictionary<string, string> format)
        {
            return BuildDataGridDataSource(instances.Count, fileds, instances, format);
        }

        /// <summary>
        /// 构造DataGrid数据源
        /// </summary>
        /// <param name="fileds">显示字段</param>
        /// <param name="instances"></param>
        /// <returns>DataGrid数据源字符串</returns>
        public static string BuildDataGridDataSource(string[] fileds, IList<object> instances)
        {
            return BuildDataGridDataSource(instances.Count, fileds, instances, null);
        }

        /// <summary>
        /// 构造DataGrid数据源
        /// </summary>
        /// <param name="instances"></param>
        /// <returns>DataGrid数据源字符串</returns>
        public static string BuildDataGridDataSource(IList<object> instances)
        {
            return BuildDataGridDataSource(instances.Count, null, instances, null);
        }

        /// <summary>
        /// 构造DataGrid更新记录
        /// </summary>
        /// <param name="rowIndex">行序号</param>
        /// <param name="dic"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string BuildUpdateRecord(int rowIndex, IDictionary<string, object> dic, Dictionary<string, string> format)
        {
            #region

            StringBuilder builder = new StringBuilder();
            builder.Append("{index:" + rowIndex.ToString() + ",row:{");

            if ((dic == null) || (dic.Count == 0))
            {
                return builder.ToString() + "}}";
            }

            string strValue;

            foreach (KeyValuePair<string, object> keyValue in dic)
            {
                if (Convert.IsDBNull(keyValue.Value) || (keyValue.Value == null))
                {
                    strValue = string.Empty;
                }
                else if ((format == null) || !format.ContainsKey(keyValue.Key))
                {
                    strValue = keyValue.Value.ToString();
                }
                else
                {
                    switch (keyValue.Value.GetType().Name.ToLower())
                    {
                        case "datetime":
                            strValue = Convert.ToDateTime(keyValue.Value).ToString(format[keyValue.Key], System.Globalization.DateTimeFormatInfo.InvariantInfo);
                            break;
                        case "decimal":
                            strValue = Convert.ToDecimal(keyValue.Value).ToString(format[keyValue.Key]);
                            break;
                        case "double":
                            strValue = Convert.ToDouble(keyValue.Value).ToString(format[keyValue.Key]);
                            break;
                        case "float":
                            strValue = Convert.ToSingle(keyValue.Value).ToString(format[keyValue.Key]);
                            break;
                        default:
                            strValue = keyValue.Value.ToString();
                            break;
                    }
                }

                builder.Append(string.Format("{0}:\"{1}\",", keyValue.Key, strValue));
            }

            return builder.Remove(builder.Length - 1, 1).ToString() + "}}";

            #endregion
        }

        /// <summary>
        /// 构造DataGrid更新记录
        /// </summary>
        /// <param name="rowIndex">行序号</param>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static string BuildUpdateRecord(int rowIndex, IDictionary<string, object> dic)
        {
            return BuildUpdateRecord(rowIndex, dic, null);
        }

        /// <summary>
        /// 构造DataGrid更新记录
        /// </summary>
        /// <param name="rowIndex">行序号</param>
        /// <param name="fileds"></param>
        /// <param name="values"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string BuildUpdateRecord(int rowIndex, string[] fileds, IList<object> values, Dictionary<int, string> format)
        {
            #region

            StringBuilder builder = new StringBuilder();
            builder.Append("{index:" + rowIndex.ToString() + ",row:{");

            if ((values == null) || (values.Count == 0))
            {
                return builder.ToString() + "}}";
            }

            string strValue;
            int index = 0;

            foreach (object value in values)
            {
                if (Convert.IsDBNull(value) || (value == null))
                {
                    strValue = string.Empty;
                }
                else if ((format == null) || !format.ContainsKey(index))
                {
                    strValue = value.ToString();
                }
                else
                {
                    switch (value.GetType().Name.ToLower())
                    {
                        case "datetime":
                            strValue = Convert.ToDateTime(value).ToString(format[index], System.Globalization.DateTimeFormatInfo.InvariantInfo);
                            break;
                        case "decimal":
                            strValue = Convert.ToDecimal(value).ToString(format[index]);
                            break;
                        case "double":
                            strValue = Convert.ToDouble(value).ToString(format[index]);
                            break;
                        case "float":
                            strValue = Convert.ToSingle(value).ToString(format[index]);
                            break;
                        default:
                            strValue = value.ToString();
                            break;
                    }
                }

                builder.Append(string.Format("{0}:\"{1}\",", fileds[index], strValue));
                index++;
            }

            return builder.Remove(builder.Length - 1, 1).ToString() + "}}";

            #endregion
        }

        /// <summary>
        /// 构造DataGrid更新记录
        /// </summary>
        /// <param name="rowIndex">行序号</param>
        /// <param name="fileds"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string BuildUpdateRecord(int rowIndex, string[] fileds, IList<object> values)
        {
            return BuildUpdateRecord(rowIndex, fileds, values, null);
        }

        /// <summary>
        /// 构造DataGrid更新记录
        /// </summary>
        /// <param name="rowIndex">行序号</param>
        /// <param name="displayFileds"></param>
        /// <param name="values"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string BuildUpdateRecord(int rowIndex, string displayFileds, IList<object> values, Dictionary<int, string> format)
        {
            return BuildUpdateRecord(rowIndex, displayFileds.Split(','), values, format);
        }

        /// <summary>
        /// 构造DataGrid更新记录
        /// </summary>
        /// <param name="rowIndex">行序号</param>
        /// <param name="displayFileds"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string BuildUpdateRecord(int rowIndex, string displayFileds, IList<object> values)
        {
            return BuildUpdateRecord(rowIndex, displayFileds.Split(','), values, null);
        }

        /// <summary>
        /// 构造DataGrid更新记录
        /// </summary>
        /// <param name="rowIndex">行序号</param>
        /// <param name="fileds"></param>
        /// <param name="instances"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string BuildUpdateRecord(int rowIndex, string[] fileds, object instance, Dictionary<string, string> format)
        {
            #region

            StringBuilder builder = new StringBuilder();
            builder.Append("{index:" + rowIndex.ToString() + ",row:{");

            if (instance == null)
            {
                return builder.ToString() + "}}";
            }

            string strPrptType, strValue;

            foreach (PropertyInfo prpt in instance.GetType().GetProperties())
            {
                if ((fileds == null) || ((fileds != null)))// && fileds.Contains(prpt.Name)
                {
                    strPrptType = prpt.PropertyType.Name.ToString().ToLower();

                    if (prpt.GetValue(instance, null) == null)
                    {
                        strValue = string.Empty;
                    }
                    else if ((format == null) || !format.ContainsKey(prpt.Name))
                    {
                        strValue = prpt.GetValue(instance, null).ToString();
                    }
                    else
                    {
                        switch (strPrptType)
                        {
                            case "datetime":
                                strValue = Convert.ToDateTime(prpt.GetValue(instance, null)).ToString(format[prpt.Name], System.Globalization.DateTimeFormatInfo.InvariantInfo);
                                break;
                            case "decimal":
                                strValue = Convert.ToDecimal(prpt.GetValue(instance, null)).ToString(format[prpt.Name]);
                                break;
                            case "double":
                                strValue = Convert.ToDouble(prpt.GetValue(instance, null)).ToString(format[prpt.Name]);
                                break;
                            case "float":
                                strValue = Convert.ToSingle(prpt.GetValue(instance, null)).ToString(format[prpt.Name]);
                                break;
                            default:
                                strValue = prpt.GetValue(instance, null).ToString();
                                break;
                        }
                    }

                    builder.Append(string.Format("{0}:\"{1}\",", prpt.Name, strValue));
                }
            }

            return builder.Remove(builder.Length - 1, 1).ToString() + "}}";

            #endregion
        }

        /// <summary>
        /// 构造DataGrid更新记录
        /// </summary>
        /// <param name="rowIndex">行序号</param>
        /// <param name="instances"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string BuildUpdateRecord(int rowIndex, object instance, Dictionary<string, string> format)
        {
            return BuildUpdateRecord(rowIndex, null, instance, format);
        }

        /// <summary>
        /// 构造DataGrid更新记录
        /// </summary>
        /// <param name="rowIndex">行序号</param>
        /// <param name="fileds"></param>
        /// <param name="instances"></param>
        /// <returns></returns>
        public static string BuildUpdateRecord(int rowIndex, string[] fileds, object instance)
        {
            return BuildUpdateRecord(rowIndex, fileds, instance, null);
        }

        /// <summary>
        /// 构造DataGrid更新记录
        /// </summary>
        /// <param name="rowIndex">行序号</param>
        /// <param name="instances"></param>
        /// <returns></returns>
        public static string BuildUpdateRecord(int rowIndex, object instance)
        {
            return BuildUpdateRecord(rowIndex, null, instance, null);
        }
    }
}
