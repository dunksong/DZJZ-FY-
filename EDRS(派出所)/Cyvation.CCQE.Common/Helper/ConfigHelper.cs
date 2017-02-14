using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Cyvation.CCQE.Common
{
    public static class ConfigHelper
    {
        /// <summary>
        /// 逗号
        /// </summary>
        public const char DH = ',';
        /// <summary>
        /// 冒号
        /// </summary>
        public const char MH = ':';

        private const string fileName = "App.xml";

        private static List<KeyValueItem> keyvalues = null;

        private static List<KeyValueItem> GetXml(out string strErro)
        {
            strErro = string.Empty;
            if (null == keyvalues)
            {
                try
                {
                    var configurationOperate = ConfigurationOperate.CreateInstance();
                    byte[] bytes = configurationOperate.GetConfigurationContent(fileName);
                    ISerializeObject iSerializeObject = SerializerCreator.CreateSerializer(SerializeType.XML, new Type[] { typeof(KeyValueItem) });
                    keyvalues = iSerializeObject.DeserializeObject<List<KeyValueItem>>(new MemoryStream(bytes));
                }
                catch (Exception e)
                {
                    strErro = "取配置文件出错:" + e.Message;
                    keyvalues = null;
                }
            }
            return keyvalues;
        }

        public static string GetValue(string strKey, out string strErro)
        {
            strErro = "";
            List<KeyValueItem> keyValues = GetXml(out strErro);
            if (!string.IsNullOrEmpty(strErro))
            {
                return "";
            }
            if (null == keyValues)
            {
                strErro = "配置文件" + fileName + "配置有误，请检查！";
                return "";
            }
            KeyValueItem itemPath = keyValues.Find(item => strKey.Equals(item.Key));
            if (null == itemPath)
            {
                strErro = "配置文件" + fileName + "中" + strKey + "不存在！";
                return "";
            }

            return itemPath.Value.ToString();
        }
    }
}