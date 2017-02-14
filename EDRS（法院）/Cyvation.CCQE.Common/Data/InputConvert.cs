// **********************************************************************
//
// 名称: 取界面上输入值时处理
//
// 描述: 取界面上输入值时处理
//
// 版本:1.0              创建人: hsq               创建日期:2013-11-27
// **********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cyvation.CCQE.Common
{
    public class InputConvert
    {
        // 过滤一些特殊字符
        public static string ToSafeString(string input)
        {
            if (string.IsNullOrEmpty(input)) { return input; }
            string strResulr  = input.Replace("'","''");
            return strResulr;

        }
    }
}
