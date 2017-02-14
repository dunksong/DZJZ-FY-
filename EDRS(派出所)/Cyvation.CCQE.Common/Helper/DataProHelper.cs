using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Cyvation.CCQE.Common
{
    /// <summary>
    /// 用于处理easyui控件ID编码
    /// </summary>
    public class DataProHelper
    {
        /// <summary>
        /// 将数据库查出来的数据表的编码字段首部+1
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="bm">编码字段</param>
        /// <returns></returns>
        public static DataTable ProBMAddOne(DataTable dt,string bm)
        {
            if (dt == null && dt.Rows.Count == 0)
            {
                return dt;
            }
            int count = dt.Rows.Count;
            string strTmp = string.Empty;
            for (int i = 0; i < count; i++)
            {
                strTmp = dt.Rows[i][bm].ToString();
                strTmp = "1" + strTmp;
                dt.Rows[i][bm] = strTmp;
            }
            return dt;
        }

        /// <summary>
        /// 处理编码-1
        /// </summary>
        /// <param name="bm"></param>
        /// <returns></returns>
        public static string ProBmSubOne(string bm)
        {
            if (string.IsNullOrEmpty(bm))
            {
                return bm;
            }
            return bm.Substring(1);
        }
    }
}
