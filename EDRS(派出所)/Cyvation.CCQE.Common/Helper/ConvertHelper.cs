using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cyvation.CCQE.Common
{
    public static class ConvertHelper
    {

        public static string TryConvertToString(this object obj)
        {
            if (obj is DBNull) return null;
            if (obj == null) return null;
            try
            {
                return obj.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        public static int TryConvertToInt32(this object obj)
        {
            try
            {
                return Convert.ToInt32(obj);
            }
            catch
            {
                return new int();
            }
        }

        public static double TryConvertToDouble(this object obj)
        {
            try
            {
                return Convert.ToDouble(obj);
            }
            catch
            {
                return new double();
            }
        }

        public static DateTime? TryConvertToDateTime(this object obj)
        {
            if (obj == null) return null;
            try
            {
                return Convert.ToDateTime(obj);
            }
            catch { }
            return null;
        }

        public static Decimal? TryConvertToDecimal(this object obj)
        {
            if (obj == null) return null;
            try
            {
                return Convert.ToDecimal(obj);
            }
            catch
            {
                return null;
            }
        }
    }
}
