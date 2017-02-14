using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EDRS.Common
{
    public class StatisticsDataHelper
    {
        /// <summary>
        /// 图形数据转换
        /// </summary>
        /// <param name="dt">数据</param>
        /// <param name="sLines">统计列名称数组</param>
        /// <param name="nameLine">统计显示列名</param>
        /// <returns></returns>
        public static string GetGraph(DataTable dt, String[] sLines, string nameLine)
        {
            string legData = "";
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < sLines.Length; i++)
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (i == 0)
                    {
                        if (j == dt.Rows.Count - 1)
                            legData += (j % 2 != 0 ? "\n" : "") + dt.Rows[j][nameLine].ToString().Replace(",", "，");
                        else
                            legData += (j % 2 != 0 ? "\n" : "") + dt.Rows[j][nameLine].ToString().Replace(",", "，") + ",";
                    }

                    if (j == dt.Rows.Count - 1)
                        sb.AppendFormat("{0}", dt.Rows[j][sLines[i]]);
                    else
                        sb.AppendFormat("{0},", dt.Rows[j][sLines[i]]);
                }
                if (i == sLines.Length-1)
                    sb.Append("");
                else
                    sb.Append("|");
            }
            return legData + "|" + sb.ToString();
        }
    }
}
