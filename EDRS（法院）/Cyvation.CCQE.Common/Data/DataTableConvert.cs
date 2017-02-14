using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Cyvation.CCQE.Common
{
    public static class DataTableConvert
    {

        public static string TryToString(this DataRow dr, string column)
        {
            if (string.IsNullOrEmpty(column)) return null;
            if (dr.Table.Columns.Contains(column))
            {
                try
                {
                    return Convert.ToString(dr[column]);
                }
                catch
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public static int? TryToInt32(this DataRow dr, string column)
        {
            if (string.IsNullOrEmpty(column)) return null;
            if (dr.Table.Columns.Contains(column))
            {
                try
                {
                    return Convert.ToInt32(dr[column]);
                }
                catch
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public static DateTime? TryToDateTime(this DataRow dr, string column)
        {
            if (string.IsNullOrEmpty(column)) return null;
            if (dr.Table.Columns.Contains(column))
            {
                try
                {
                    return Convert.ToDateTime(dr[column]);
                }
                catch
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
