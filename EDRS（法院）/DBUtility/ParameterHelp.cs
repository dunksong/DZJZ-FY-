using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using System.Data.OleDb;
using System.Text.RegularExpressions;

namespace Maticsoft.DBUtility
{
    /// <summary>
    /// Sql语句条件参数化处理类
    /// </summary>
    public class ParameterHelp
    {
        /// <summary>
        /// Sql语句条件参数化处理
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public static OracleParameter[] ParameterReset(string parm, params object[] strValues)
        {
            var st = Regex.Matches(parm, ":[a-zA-Z0-9_]+");
            OracleParameter[] parameters = new OracleParameter[st.Count];
            if (strValues != null)
            {
                strValues = strValues.Where(p => p != null).ToArray();
                if (strValues.Length != st.Count)
                    return null;
                for (int i = 0; i < st.Count; i++)
                    parameters[i] = new OracleParameter(st[i].ToString(), strValues[i]);
            }
            return parameters;
        }
    }
}
