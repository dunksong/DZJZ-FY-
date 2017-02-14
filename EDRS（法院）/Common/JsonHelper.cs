using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Collections;
using System.Web.Script.Serialization;


namespace EDRS.Common
{
    /// <summary>
    /// Json数据帮助类
    /// 修改人：yangtb
    /// 修改时间:2015/5/21
    /// 修改原因：增加ParseFormJson方法
    /// </summary>
    public class JsonHelper
    {
        /// <summary>
        /// 对象转json字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string JsonString(object data)
        {
            IsoDateTimeConverter time = new IsoDateTimeConverter();
            time.DateTimeFormat="yyyy-MM-dd HH:mm:ss";
            return JsonConvert.SerializeObject(data, Formatting.Indented, time);           
        }

        public static string JsonDataString(object list) 
        {
            DataContractJsonSerializer dcj = new DataContractJsonSerializer(list.GetType());
            string szjson = "";
            using (MemoryStream stream = new MemoryStream()) {
                dcj.WriteObject(stream, list);
                szjson = Encoding.UTF8.GetString(stream.ToArray());
            }
            return szjson;
        }
        /// <summary>
        /// 获取json字符串中某个key对应的值
        /// </summary>
        /// <param name="json"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string DeserializeObjectKey(string json, string key)
        {
            JArray jsa = (JArray)JsonConvert.DeserializeObject(json);
            if (jsa.Count > 0)
            {
                JObject obj = (JObject)jsa[0];
                if (obj[key] != null)
                    return obj[key].ToString();
            }
            return null;
        }
        /// <summary>
        /// json字符串转换为实体
        /// </summary>
        /// <typeparam name="T">需要转换成的类</typeparam>
        /// <param name="szJson">Json字符串</param>
        /// <returns>返回实例</returns>
        public static T ParseFormJson<T>(string szJson)
        {
            T obj = Activator.CreateInstance<T>();
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(szJson)))
            {
                DataContractJsonSerializer dcj = new DataContractJsonSerializer(typeof(T));
                return (T)dcj.ReadObject(ms);
            }
        }
        /// <summary>
        /// json字符串转换实体对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static List<T> JsonToList<T>(string json)
        {
            if (json == "")
                return default(List<T>);
            var newJson = json.Replace("}]", "");
            newJson = newJson.Substring(newJson.IndexOf("[{") + 2);
            var regex = new Regex("},{");
            var jsons = regex.Split(newJson);
            var entitys = new List<T>();
            foreach (var item in jsons)
            {
                var temp = "{" + item + "}";
                entitys.Add(ParseFormJson<T>(temp));
            }
            return entitys;
        }
        /// <summary>
        /// json 转table
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static DataTable ToDataTable(string json)
        {
            DataTable dataTable = new DataTable();  //实例化
            DataTable result;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                javaScriptSerializer.MaxJsonLength = Int32.MaxValue; //取得最大数值
                ArrayList arrayList = javaScriptSerializer.Deserialize<ArrayList>(json);
                if (arrayList.Count > 0)
                {
                    foreach (Dictionary<string, object> dictionary in arrayList)
                    {
                        if (dictionary.Keys.Count<string>() == 0)
                        {
                            result = dataTable;
                            return result;
                        }
                        if (dataTable.Columns.Count == 0)
                        {
                            foreach (string current in dictionary.Keys)
                            {
                                if (dictionary[current] != null)
                                    dataTable.Columns.Add(current, dictionary[current].GetType());
                                else
                                    dataTable.Columns.Add(current, typeof(String));
                            }
                        }
                        DataRow dataRow = dataTable.NewRow();
                        foreach (string current in dictionary.Keys)
                        {
                            dataRow[current] = dictionary[current];
                        }

                        dataTable.Rows.Add(dataRow); //循环添加行到DataTable中
                    }
                }
            }
            catch
            {
            }
            result = dataTable;
            return result;
        } 
       
    }
}
