// **********************************************************************
//
// 名称: 数据源绑定及获取帮助类
//
// 描述: 
//
// 版本:1.0              创建人: lxl               创建日期:2013年11月27日14:52:04
// **********************************************************************

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Cyvation.CCQE.Common
{
    public static class DataSourceHelper
    {
        /// <summary>
        /// cbo控件单个值选中
        /// </summary>
        /// <param name="strValue">Value.ToString()</param>
        /// <returns>字符串数组</returns>
        public static string[] CboSelect(string strValue)
        {
            string[] array = new string[] { string.IsNullOrEmpty(strValue) ? string.Empty : strValue };
            return array;
        }

        /// <summary>
        /// 数据字典绑定(没有0)
        /// </summary>
        /// <param name="strValue">ValueMember</param>
        /// <param name="strDisplay">DisplayMember</param>
        /// <param name="typeofEnum">typeof(Enum)</param>
        /// <returns>DataTable</returns>
        public static DataTable DictionaryBindOne(string strValue, string strDisplay, Type typeofEnum)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(strValue, typeof(String));
            dt.Columns.Add(strDisplay, typeof(String));
            foreach (var item in Enum.GetValues(typeofEnum))
            {
                DataRow dr = dt.NewRow();
                dr[strValue] = ((int)item).ToString();
                dr[strDisplay] = item;
                dt.Rows.Add(dr);
            }
            return dt;
        }

        /// <summary>
        /// 数据字典绑定(有0)
        /// </summary>
        /// <param name="strValue">ValueMember</param>
        /// <param name="strDisplay">DisplayMember</param>
        /// <param name="typeofEnum">typeof(Enum)</param>
        /// <returns>DataTable</returns>
        public static DataTable DictionaryBindTwo(string strValue, string strDisplay, Type typeofEnum)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(strValue, typeof(String));
            dt.Columns.Add(strDisplay, typeof(String));
            foreach (var item in Enum.GetValues(typeofEnum))
            {
                DataRow dr = dt.NewRow();
                dr[strValue] = ((int)item).ToString().PadLeft(2, '0');
                dr[strDisplay] = item;
                dt.Rows.Add(dr);
            }
            return dt;
        }

        /// <summary>
        /// DataTable静态方法，用于方便添加两个列
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="oneStr"></param>
        /// <param name="twoStr"></param>
        /// <param name="oneValue"></param>
        /// <param name="twoValue"></param>
        /// <returns></returns>
        public static DataTable AddTwo(this DataTable dt, string oneStr, object oneValue, string twoStr, object twoValue)
        {
            DataRow dr = dt.NewRow();
            dr[oneStr] = oneValue;
            dr[twoStr] = twoValue;
            dt.Rows.Add(dr);
            return dt;
        }

        /// <summary>
        ///  是否DataTable数据源（YNVALUE, YNDISPLAY）
        /// </summary>
        public static DataTable BooleanDataSource()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("YNVALUE", typeof(String));
            dt.Columns.Add("YNDISPLAY", typeof(String));
            dt.AddTwo("YNVALUE", "Y", "YNDISPLAY", "是");
            dt.AddTwo("YNVALUE", "N", "YNDISPLAY", "否");
            return dt;
        }

        #region 简便枚举转DataTable方法
        private static readonly string enumKey = "KEY";
        private static readonly string enumValue = "VALUE";
        /// <summary>
        /// 简便枚举转DataTable方法 -- 键名(只读)
        /// </summary>
        public static string EnumKey
        {
            get
            {
                return enumKey;
            }
        }
        /// <summary>
        /// 简便枚举转DataTable方法 -- 值名(只读)
        /// </summary>
        public static string EnumValue
        {
            get
            {
                return enumValue;
            }
        }
        /// <summary>
        /// 简便枚举转DataTable方法（绑定请用 DataSourceHelper.EnumKey、DataSourceHelper.EnumValue）
        /// </summary>
        /// <param name="typeofEnum">typeof(Enum)</param>
        /// <param name="length">键前补 0 总长度，默认不补</param>
        /// <returns>枚举数据源DataTable</returns>
        public static DataTable EnumToDtSource(Type typeofEnum, int length = 1)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(enumKey, typeof(String));
            dt.Columns.Add(enumValue, typeof(String));
            foreach (var item in System.Enum.GetValues(typeofEnum))
            {
                DataRow dr = dt.NewRow();
                if (length > 1)
                {
                    dr[enumKey] = ((int)item).ToString().PadLeft(length, '0');
                }
                else
                {
                    dr[enumKey] = ((int)item).ToString();
                }
                dr[enumValue] = item;
                dt.Rows.Add(dr);
            }
            return dt;
        }
        ///// <summary>
        ///// 简便枚举转DataTable方法 获取 exTreeList 选中的值 Value
        ///// </summary>
        ///// <param name="control">封装的exTreeList控件</param>
        ///// <returns>object（未选中则返回空）</returns>
        //public static object EnumGetValue(ExTreeListComboBoxEdit control)
        //{
        //    if (control.Value != null)
        //    {
        //        object obj = (control.Value as List<object>).First();
        //        DataRow dr = ((System.Data.DataRowView)(obj)).Row;
        //        int inx = dr.Table.Columns.IndexOf(enumKey);
        //        return dr.ItemArray[inx];
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}
        #endregion
        
        /// <summary>
        /// 获取Enum的值
        /// </summary>
        /// <param name="i">Key值</param>
        public static TEnum GetEnum<TEnum>(int i) where TEnum : struct
        {
            TEnum em;
            Enum.TryParse<TEnum>(i.ToString(), out em);
            return em;
        }
        
        /// <summary>
        /// 返回Cell的字符串空保护方法
        /// </summary>
        public static string CheckToString(this object obj)
        {
            return obj != null ? obj.ToString() : string.Empty;
        }
    }
}