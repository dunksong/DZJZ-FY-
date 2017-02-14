using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Cyvation.CCQE.Common
{
    /// <summary>
    /// ×Ö·û´®´¦Àíº¯Êý
    /// </summary>
    public class StringCommonHelper
    {
        private static byte[] Vals = {  
        0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,  
        0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,  
        0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,  
        0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,  
        0x3F,0x0A,0x0B,0x0C,0x0D,0x0E,0x0F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,  
        0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,  
        0x3F,0x0A,0x0B,0x0C,0x0D,0x0E,0x0F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,  
        0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,  
        0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,  
        0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,  
        0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,  
        0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,  
        0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,  
        0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,  
        0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,  
        0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F,0x3F  };

        /// <summary>
        /// ¼ÓÃÜ×Ö·û´®
        /// </summary>
        /// <param name="s">¼ÓÃÜ×Ö·û´®</param>
        /// <param name="key">±ÈÈç:jUVBLUvURwE=</param>
        /// <param name="iv">±ÈÈç:tL7ZXYbX21w=</param>
        /// <returns>¼ÓÃÜ×Ö·û´®</returns>
        public static string EncryptString(string s, string key, string iv)
        {
            #region

            SymmetricAlgorithm des = new DESCryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            ICryptoTransform ictf = des.CreateEncryptor(Convert.FromBase64String(key), Convert.FromBase64String(iv));
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, ictf, CryptoStreamMode.Write);
            cs.Write(bytes, 0, bytes.Length);
            cs.FlushFinalBlock();
            ms.Close();

            return Convert.ToBase64String(ms.ToArray());

            #endregion
        }

        /// <summary>
        /// ¼ÓÃÜ×Ö·û´®
        /// </summary>
        /// <param name="s">¼ÓÃÜ×Ö·û´®</param>
        /// <returns>¼ÓÃÜ×Ö·û´®</returns>
        public static string EncryptString(string s)
        {
            #region

            SymmetricAlgorithm des = new DESCryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            ICryptoTransform ictf = des.CreateEncryptor(Convert.FromBase64String("jUVBLUvURwE="), Convert.FromBase64String("tL7ZXYbX21w="));
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, ictf, CryptoStreamMode.Write);
            cs.Write(bytes, 0, bytes.Length);
            cs.FlushFinalBlock();
            ms.Close();

            return Convert.ToBase64String(ms.ToArray());

            #endregion
        }

        /// <summary>
        /// ½âÃÜ×Ö·û´®
        /// </summary>
        /// <param name="s">½âÃÜ×Ö·û´®</param>
        /// <param name="key">±ÈÈç:jUVBLUvURwE=</param>
        /// <param name="iv">±ÈÈç:tL7ZXYbX21w=</param>
        /// <returns>½âÃÜ×Ö·û´®</returns>
        public static string DecryptString(string s, string key, string iv)
        {
            #region

            SymmetricAlgorithm des = new DESCryptoServiceProvider();
            byte[] bytes = Convert.FromBase64String(s);
            MemoryStream ms = new MemoryStream(bytes, 0, bytes.Length);
            des.Key = Convert.FromBase64String(key);
            des.IV = Convert.FromBase64String(iv);
            ICryptoTransform ictf = des.CreateDecryptor();
            CryptoStream cs = new CryptoStream(ms, ictf, CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cs);

            return sr.ReadToEnd();

            #endregion
        }

        /// <summary>
        /// ½âÃÜ×Ö·û´®
        /// </summary>
        /// <param name="s">½âÃÜ×Ö·û´®</param>
        /// <returns>½âÃÜ×Ö·û´®</returns>
        public static string DecryptString(string s)
        {
            #region

            SymmetricAlgorithm des = new DESCryptoServiceProvider();
            byte[] bytes = Convert.FromBase64String(s);
            MemoryStream ms = new MemoryStream(bytes, 0, bytes.Length);
            des.Key = Convert.FromBase64String("jUVBLUvURwE=");
            des.IV = Convert.FromBase64String("tL7ZXYbX21w=");
            ICryptoTransform ictf = des.CreateDecryptor();
            CryptoStream cs = new CryptoStream(ms, ictf, CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cs);

            return sr.ReadToEnd();

            #endregion
        }

        /// <summary>
        /// ¹ýÂË×Ö·û´®
        /// </summary>
        /// <param name="s">×Ö·û´®</param>
        /// <returns>¹ýÂË×Ö·û´®</returns>
        public static string CleanString(string s)
        {
            #region

            StringBuilder builder = new StringBuilder();

            if (!string.IsNullOrEmpty(s))
            {
                s = s.Trim();

                foreach (char c in s)
                {
                    switch (c)
                    {
                        case '"':
                            builder.Append("&quot;");
                            break;
                        case '<':
                            builder.Append("&lt;");
                            break;
                        case '>':
                            builder.Append("&gt;");
                            break;
                        default:
                            builder.Append(c);
                            break;
                    }
                }

                builder.Replace("'", "¡¯");
            }

            return builder.ToString();

            #endregion
        }

        /// <summary>
        /// ÇåÀísqlÎÄ±¾
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string SqlTextClear(string s)
        {
            return string.IsNullOrEmpty(s) ? string.Empty : s.Replace(",", "").Replace("<", "").Replace(">", "").Replace("--", "").Replace("'", "").Replace("\"", "")
                .Replace("=", "").Replace("%", "").Replace(" ", "");
        }

        /// <summary>
        /// ÊµÏÖjavascriptµÄunescapeº¯Êý
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string Unescape(string s)
        {
            #region

            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            StringBuilder builder = new StringBuilder();
            int i = 0;
            int len = s.Length;

            while (i < len)
            {
                int ch = s[i];

                if (('A' <= ch) && (ch <= 'Z'))
                {
                    builder.Append((char)ch);
                }
                else if (('a' <= ch) && (ch <= 'z'))
                {
                    builder.Append((char)ch);
                }
                else if (('0' <= ch) && (ch <= '9'))
                {
                    builder.Append((char)ch);
                }
                else if ((ch == '-') || (ch == '_')
                  || (ch == '.') || (ch == '!')
                  || (ch == '~') || (ch == '*')
                  || (ch == '\'') || (ch == '(')
                  || (ch == ')'))
                {
                    builder.Append((char)ch);
                }
                else if (ch == '%')
                {
                    int cint = 0;

                    if ('u' != s[i + 1])
                    {
                        cint = (cint << 4) | Vals[s[i + 1]];
                        cint = (cint << 4) | Vals[s[i + 2]];
                        i += 2;
                    }
                    else
                    {
                        cint = (cint << 4) | Vals[s[i + 2]];
                        cint = (cint << 4) | Vals[s[i + 3]];
                        cint = (cint << 4) | Vals[s[i + 4]];
                        cint = (cint << 4) | Vals[s[i + 5]];
                        i += 5;
                    }

                    builder.Append((char)cint);
                }

                i++;
            }

            return builder.ToString();

            #endregion
        }

        public static string GuidString()
        {
            return Guid.NewGuid().ToString("n");
        }

        public static string FormatDate(string s)
        {
            return s == string.Empty ? string.Empty : Convert.ToDateTime(s).ToString("yyyy-MM-dd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
        }

        public static string FormatDateTime(string s)
        {
            return s == string.Empty ? string.Empty : Convert.ToDateTime(s).ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo);
        }

        public static string CleanJsonString(string s)
        {
            #region

            string ret = string.Empty;

            foreach (char c in s.ToCharArray())
            {

                if ((c == (char)10) || (c == (char)13))
                {
                    continue;
                }
                else if (c == (char)92)
                {
                    ret += "\\\\";
                }
                else
                {
                    ret += c.ToString();
                }
            }

            return ret;

            #endregion
        }

        public static string TagGuid(string tag)
        {
            return tag + DateTime.Now.ToString("yyyyMMddHHmmssffffff", System.Globalization.DateTimeFormatInfo.InvariantInfo);
        }
    }
}
