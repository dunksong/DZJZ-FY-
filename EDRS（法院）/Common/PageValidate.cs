using System;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Globalization;

namespace EDRS.Common
{
    /// <summary>
    /// 页面数据校验类
    /// Copyright (C) Create 2015-2020
    /// </summary>
    public class PageValidate
    {
        private static string msg;
        /// <summary>
        /// 获取错误信息
        /// </summary>
        public static string Msg
        {
            get { return msg; }
        }

        private static Regex RegPhone = new Regex("^[0-9]+[-]?[0-9]+[-]?[0-9]$");
        private static Regex RegNumber = new Regex("^[0-9]+$");
        private static Regex RegNumberSign = new Regex("^[+-]?[0-9]+$");
        private static Regex RegDecimal = new Regex("^[0-9]+[.]?[0-9]+$");
        private static Regex RegDecimalSign = new Regex("^[+-]?[0-9]+[.]?[0-9]+$"); //等价于^[+-]?\d+[.]?\d+$
        private static Regex RegEmail = new Regex("^[\\w-]+@[\\w-]+\\.(com|net|org|edu|mil|tv|biz|info)$");//w 英文字母或数字的字符串，和 [a-zA-Z0-9] 语法一样 
        private static Regex RegCHZN = new Regex("[\u4e00-\u9fa5]");

        public PageValidate()
        {
        }


        #region 数字字符串检查
        public static bool IsPhone(string inputData)
        {
            Match m = RegPhone.Match(inputData);
            return m.Success;
        }
        /// <summary>
        /// 检查Request查询字符串的键值，是否是数字，最大长度限制
        /// </summary>
        /// <param name="req">Request</param>
        /// <param name="inputKey">Request的键值</param>
        /// <param name="maxLen">最大长度</param>
        /// <returns>返回Request查询字符串</returns>
        public static string FetchInputDigit(HttpRequest req, string inputKey, int maxLen)
        {
            string retVal = string.Empty;
            if (inputKey != null && inputKey != string.Empty)
            {
                retVal = req.QueryString[inputKey];
                if (null == retVal)
                    retVal = req.Form[inputKey];
                if (null != retVal)
                {
                    retVal = SqlText(retVal, maxLen);
                    if (!IsNumber(retVal))
                        retVal = string.Empty;
                }
            }
            if (retVal == null)
                retVal = string.Empty;
            return retVal;
        }
        /// <summary>
        /// 是否数字字符串
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsNumber(string inputData)
        {
            Match m = RegNumber.Match(inputData);
            if (m.Success)
                return true;
            msg = "数字不正确";
            return false;
        }

        /// <summary>
        /// 是否数字字符串 可带正负号
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsNumberSign(string inputData)
        {
            Match m = RegNumberSign.Match(inputData);
            return m.Success;
        }
        /// <summary>
        /// 是否是浮点数限制小数位数
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsDecimalDigit(string inputData, int len, int scale)
        {
            try
            {
                decimal.Parse(inputData);
                if (Regex.IsMatch(inputData, @"^[0-9]{1," + (len - scale) + "}[.]?[0-9]{" + scale + "}$", RegexOptions.IgnoreCase))
                {
                    return true;
                }
            }
            catch (Exception)
            {
            }
            msg = "小数个数必须为" + scale + "位，总长不能超过" + len + "位";
            return false;
        }
        /// <summary>
        /// 是否是浮点数
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsDecimal(string inputData)
        {
            Match m = RegDecimal.Match(inputData);
            if (m.Success)
                return true;
            msg = "数字不正确";
            return false;
        }
        /// <summary>
        /// 是否是浮点数 可带正负号
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsDecimalSign(string inputData)
        {
            Match m = RegDecimalSign.Match(inputData);
            if (m.Success)
                return true;
            msg = "数字不正确";
            return false;
        }

        #endregion

        #region 中文检测

        /// <summary>
        /// 检测是否有中文字符
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsHasCHZN(string inputData)
        {
            Match m = RegCHZN.Match(inputData);
            return m.Success;
        }

        #endregion

        #region 邮件地址
        /// <summary>
        /// 是否是浮点数 可带正负号
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        /*
        public static bool IsEmail(string inputData)
        {
            Match m = RegEmail.Match(inputData);
            return m.Success;
        }
*/
        #endregion

        #region 日期格式判断
        /// <summary>
        /// 日期格式字符串判断
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        /*
        public static bool IsDateTime(string str)
        {
            try
            {
                if (!string.IsNullOrEmpty(str))
                {
                    DateTime.Parse(str);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        */
        #endregion

        #region 其他

        /// <summary>
        /// 检查字符串最大长度，返回指定长度的串
        /// </summary>
        /// <param name="sqlInput">输入字符串</param>
        /// <param name="maxLength">最大长度</param>
        /// <returns></returns>			
        public static string SqlText(string sqlInput, int maxLength)
        {
            if (sqlInput != null && sqlInput != string.Empty)
            {
                sqlInput = sqlInput.Trim();
                if (sqlInput.Length > maxLength)//按最大长度截取字符串
                    sqlInput = sqlInput.Substring(0, maxLength);
            }
            return sqlInput;
        }
        /// <summary>
        /// 字符串编码
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static string HtmlEncode(string inputData)
        {
            return HttpUtility.HtmlEncode(inputData);
        }
        /// <summary>
        /// 设置Label显示Encode的字符串
        /// </summary>
        /// <param name="lbl"></param>
        /// <param name="txtInput"></param>
        public static void SetLabel(Label lbl, string txtInput)
        {
            lbl.Text = HtmlEncode(txtInput);
        }
        public static void SetLabel(Label lbl, object inputObj)
        {
            SetLabel(lbl, inputObj.ToString());
        }
        //字符串清理
        public static string InputText(string inputString, int maxLength)
        {
            StringBuilder retVal = new StringBuilder();

            // 检查是否为空
            if ((inputString != null) && (inputString != String.Empty))
            {
                inputString = inputString.Trim();

                //检查长度
                if (inputString.Length > maxLength)
                    inputString = inputString.Substring(0, maxLength);

                //替换危险字符
                for (int i = 0; i < inputString.Length; i++)
                {
                    switch (inputString[i])
                    {
                        case '"':
                            retVal.Append("&quot;");
                            break;
                        case '<':
                            retVal.Append("&lt;");
                            break;
                        case '>':
                            retVal.Append("&gt;");
                            break;
                        default:
                            retVal.Append(inputString[i]);
                            break;
                    }
                }
                retVal.Replace("'", " ");// 替换单引号
            }
            return retVal.ToString();

        }
        /// <summary>
        /// 转换成 HTML code
        /// </summary>
        /// <param name="str">string</param>
        /// <returns>string</returns>
        public static string Encode(string str)
        {
            str = str.Replace("&", "&amp;");
            str = str.Replace("'", "''");
            str = str.Replace("\"", "&quot;");
            str = str.Replace(" ", "&nbsp;");
            str = str.Replace("<", "&lt;");
            str = str.Replace(">", "&gt;");
            str = str.Replace("\n", "<br>");
            return str;
        }
        /// <summary>
        ///解析html成 普通文本
        /// </summary>
        /// <param name="str">string</param>
        /// <returns>string</returns>
        public static string Decode(string str)
        {
            str = str.Replace("<br>", "\n");
            str = str.Replace("&gt;", ">");
            str = str.Replace("&lt;", "<");
            str = str.Replace("&nbsp;", " ");
            str = str.Replace("&quot;", "\"");
            return str;
        }

        public static string SqlTextClear(string sqlText)
        {
            if (sqlText == null)
            {
                return null;
            }
            if (sqlText == "")
            {
                return "";
            }
            sqlText = sqlText.Replace(",", "");//去除,
            sqlText = sqlText.Replace("<", "");//去除<
            sqlText = sqlText.Replace(">", "");//去除>
            sqlText = sqlText.Replace("--", "");//去除--
            sqlText = sqlText.Replace("'", "");//去除'
            sqlText = sqlText.Replace("\"", "");//去除"
            sqlText = sqlText.Replace("=", "");//去除=
            sqlText = sqlText.Replace("%", "");//去除%
            sqlText = sqlText.Replace(" ", "");//去除空格
            return sqlText;
        }
        #endregion

        #region 是否由特定字符组成
        public static bool isContainSameChar(string strInput)
        {
            string charInput = string.Empty;
            if (!string.IsNullOrEmpty(strInput))
            {
                charInput = strInput.Substring(0, 1);
            }
            return isContainSameChar(strInput, charInput, strInput.Length);
        }

        public static bool isContainSameChar(string strInput, string charInput, int lenInput)
        {
            if (string.IsNullOrEmpty(charInput))
            {
                return false;
            }
            else
            {
                Regex RegNumber = new Regex(string.Format("^([{0}])+$", charInput));
                //Regex RegNumber = new Regex(string.Format("^([{0}]{{1}})+$", charInput,lenInput));
                Match m = RegNumber.Match(strInput);
                return m.Success;
            }
        }
        #endregion

        #region 检查输入的参数是不是某些定义好的特殊字符：这个方法目前用于密码输入的安全检查
        /// <summary>
        /// 检查输入的参数是不是某些定义好的特殊字符：这个方法目前用于密码输入的安全检查
        /// </summary>
        public static bool isContainSpecChar(string strInput)
        {
            string[] list = new string[] { "123456", "654321" };
            bool result = new bool();
            for (int i = 0; i < list.Length; i++)
            {
                if (strInput == list[i])
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
        #endregion


        public static string SafeLongFilter(string text, long defaultValue, char split = ',')
        {
            if (text.Trim().Length < 1)
                return defaultValue.ToString(CultureInfo.InvariantCulture);
            string[] tmpSplit = text.Split(new[] { split }, StringSplitOptions.RemoveEmptyEntries);
            if (tmpSplit.Length < 1)
                return defaultValue.ToString(CultureInfo.InvariantCulture);

            long tmp;
            for (int i = 0; i < tmpSplit.Length; i++)
            {
                if (long.TryParse(tmpSplit[i], out tmp))
                    tmpSplit[i] = tmp.ToString(CultureInfo.InvariantCulture);
                else
                    tmpSplit[i] = defaultValue.ToString(CultureInfo.InvariantCulture);
            }
            return string.Join(split.ToString(CultureInfo.InvariantCulture), tmpSplit);
        }






        #region 验证邮箱
        /**/
        /// <summary>
        /// 验证邮箱
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsEmail(string source)
        {
            if (Regex.IsMatch(source, @"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$",

RegexOptions.IgnoreCase))
                return true;
            msg = "邮箱格式不正确";
            return false;
        }
        public static bool HasEmail(string source)
        {
            return Regex.IsMatch(source, @"[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})",

RegexOptions.IgnoreCase);
        }
        #endregion

        #region 验证网址
        /**/
        /// <summary>
        /// 验证网址
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsUrl(string source)
        {
            if (Regex.IsMatch(source, @"^(((file|gopher|news|nntp|telnet|http|ftp|https|ftps|sftp)://)|(www\.))+(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]

{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(/[a-zA-Z0-9\&amp;%_\./-~-]*)?$", RegexOptions.IgnoreCase))
                return true;
            msg = "网址格式不正确";
            return false;
        }
        public static bool HasUrl(string source)
        {
            if (Regex.IsMatch(source, @"(((file|gopher|news|nntp|telnet|http|ftp|https|ftps|sftp)://)|(www\.))+(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]

{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(/[a-zA-Z0-9\&amp;%_\./-~-]*)?", RegexOptions.IgnoreCase))
                return true;
            msg = "网址格式不正确";
            return false;
        }
        #endregion

        #region 验证日期
        /**/
        /// <summary>
        /// 验证日期
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>        
        public static bool IsDateTime(string source)
        {
            try
            {
                DateTime time = Convert.ToDateTime(source);
                return true;
            }
            catch
            {
                msg = "日期格式不正确，如:2001-01-01 09:00:00";
                return false;
            }
        }

        #endregion

        #region 验证手机号
        /**/
        /// <summary>
        /// 验证手机号
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsMobile(string source)
        {
            if (Regex.IsMatch(source, @"^1[35]\d{9}$", RegexOptions.IgnoreCase))
                return true;
            msg = "手机号格式不正确";
            return false;
        }
        public static bool HasMobile(string source)
        {
            if (Regex.IsMatch(source, @"1[35]\d{9}", RegexOptions.IgnoreCase))
                return true;
            msg = "手机号格式不正确";
            return false;
        }
        #endregion

        #region 验证IP
        /**/
        /// <summary>
        /// 验证IP
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsIP(string source)
        {
            if (Regex.IsMatch(source, @"^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]

|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$",

           RegexOptions.IgnoreCase))
                return true;
            msg = "IP地址格式不正确";
            return false;
        }
        public static bool HasIP(string source)
        {
            if (Regex.IsMatch(source, @"(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]

|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])",

           RegexOptions.IgnoreCase))
                return true;
            msg = "IP地址格式不正确";
            return false;
        }
        #endregion

        #region 验证身份证是否有效
        /**/
        /// <summary>
        /// 验证身份证是否有效
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static bool IsIDCard(string Id)
        {
            if (Id.Length == 18)
            {
                if (IsIDCard18(Id))
                    return true;
            }
            else if (Id.Length == 15)
            {
                if (IsIDCard15(Id))
                    return true;
            }

            msg = "身份证格式不正确";
            return false;
        }

        public static bool IsIDCard18(string Id)
        {
            long n = 0;
            if (long.TryParse(Id.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;//省份验证
            }
            string birth = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = Id.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != Id.Substring(17, 1).ToLower())
            {
                return false;//校验码验证
            }
            return true;//符合GB11643-1999标准
        }

        public static bool IsIDCard15(string Id)
        {
            long n = 0;
            if (long.TryParse(Id, out n) == false || n < Math.Pow(10, 14))
            {
                return false;//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;//省份验证
            }
            string birth = Id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }
            return true;//符合15位身份证标准
        }
        #endregion

        #region 是不是Int型的
        /**/
        /// <summary>
        /// 是不是Int型的
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsInt(string source)
        {
            Regex regex = new Regex(@"^(-){0,1}\d+$");
            if (regex.Match(source).Success)
            {
                if ((long.Parse(source) > 0x7fffffffL) || (long.Parse(source) < -2147483648L))
                {
                    msg = "不是正确的数字";
                    return false;
                }
                return true;
            }
            msg = "不是正确的数字";
            return false;
        }
        #endregion

        #region 验证正数字类型
        /// <summary>
        /// 验证正数字类型
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsIntN(string source)
        {
            try
            {
                Regex regex = new Regex(@"^\d+$");
                if (regex.Match(source).Success)
                {
                    int.Parse(source);
                    return true;
                }
            }
            catch (Exception)
            {
            }
            msg = "必须为正数";
            return false;
        }
        #endregion

        #region 看字符串的长度是不是在限定数之间 一个中文为两个字符
        /**/
        /// <summary>
        /// 看字符串的长度是不是在限定数之间 一个中文为两个字符
        /// </summary>
        /// <param name="source">字符串</param>
        /// <param name="begin">大于等于</param>
        /// <param name="end">小于等于</param>
        /// <returns></returns>
        public static bool IsLengthStr(string source, int begin, int end)
        {
            int length = Regex.Replace(source, @"[^\x00-\xff]", "OK").Length;
            if ((length < begin) || (length > end))
            {
                msg = "字符串长度必须至少" + begin + "，最多" + end + "个字符";
                return false;
            }
            return true;
        }
        #endregion

        #region 是不是中国电话，格式010-85849685
        /**/
        /// <summary>
        /// 是不是中国电话，格式010-85849685
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsTel(string source)
        {
            if (Regex.IsMatch(source, @"^\d{3,4}-?\d{6,8}$", RegexOptions.IgnoreCase))
                return true;
            msg = "电话号码格式不正确，格式如：010-85849685";
            return false;
        }
        #endregion

        #region 邮政编码 6个数字
        /**/
        /// <summary>
        /// 邮政编码 6个数字
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsPostCode(string source)
        {
            if (Regex.IsMatch(source, @"^\d{6}$", RegexOptions.IgnoreCase))
                return true;
            msg = "邮政编码格式不正确";
            return false;
        }
        #endregion

        #region 中文
        /**/
        /// <summary>
        /// 中文
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsChinese(string source)
        {
            if (Regex.IsMatch(source, @"^[\u4e00-\u9fa5]+$", RegexOptions.IgnoreCase))
                return true;
            msg = "正文格式不正确";
            return false;
        }
        public static bool hasChinese(string source)
        {
            if (Regex.IsMatch(source, @"[\u4e00-\u9fa5]+", RegexOptions.IgnoreCase))
                return true;
            msg = "正文格式不正确";
            return false;
        }
        #endregion

        #region 验证是不是正常字符 字母，数字，下划线的组合
        /**/
        /// <summary>
        /// 验证是不是正常字符 字母，数字，下划线的组合
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsNormalChar(string source)
        {
            if (Regex.IsMatch(source, @"[\w\d_]+", RegexOptions.IgnoreCase))
                return true;
            msg = "格式不正确，只能为字母数字下划组合";
            return false;
        }
        #endregion

        #region 验证是否为bool值
        /// <summary>
        /// 验证是否为bool值
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsBool(string source)
        {
            try
            {
                bool.Parse(source);
                return true;
            }
            catch (Exception)
            {
                msg = "格式不正确";
                return false;
            }
        }
        #endregion


    }
}
