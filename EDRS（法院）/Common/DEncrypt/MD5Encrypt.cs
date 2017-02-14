using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDRS.Common.DEncrypt
{
    public class MD5Encrypt
    {
        /// <summary>
        /// MD5 加密
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public static string Encrypt(string original)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(original, "MD5");
        }
    }
}
