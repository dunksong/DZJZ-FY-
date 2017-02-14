using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using System.Xml;
using System.Net;
using EDRS.Common;
using System.Data;
using System.Text.RegularExpressions;

namespace WebUI
{
    public partial class FullQuery : BasePage
    {
        protected string SetBeTime = DateTime.Now.ToString();

        protected void Page_Load(object sender, EventArgs e)
        {

            string type = Request["t"];

            if (!string.IsNullOrEmpty(type))
            {
                Response.ContentEncoding = Encoding.UTF8;
                Response.ContentType = "application/json";

                if (type.Equals("querydata"))
                    Response.Write(QueryData());


                Response.End();
            }
        }




        #region 查询方法
        /// <summary>
        /// 查询方法
        /// </summary>
        /// <returns></returns>
        private string QueryData()
        {
            string id = Request.Form["id"];

            string jsonParas = "";

            string txt = Request.Form["txt"];
            string bmsah = Request.Form["bmsah"];
            string ajmc = Request.Form["ajmc"];
            //string cbr = Request.Form["cbr"];
            string slrq1 = Request.Form["slrq1"];
            string slrq2 = Request.Form["slrq2"];

            string dwbmin = "";
            string ajlbin = "";

            EDRS.BLL.XT_DM_QX bll = new EDRS.BLL.XT_DM_QX(Request);
            //单位编码权限
            DataSet ds = bll.GetQxListByRole(this.UserInfo.DWBM, "",this.UserInfo.GH, 0, "");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (i == 0)
                    dwbmin += " AND (CBDW_BM:" + ds.Tables[0].Rows[i]["QXBM"];
                else if (i > 0)
                {
                    dwbmin += " OR CBDW_BM:" + ds.Tables[0].Rows[i]["QXBM"];
                }
                if (i == ds.Tables[0].Rows.Count - 1)
                    dwbmin += ")";
            }

            //案件类别权限
            DataSet ds2 = bll.GetQxListByRole(this.UserInfo.DWBM, "", this.UserInfo.GH, 1, "");
            for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
            {
                if (i == 0)
                    ajlbin += " AND (AJLB_BM:" + ds2.Tables[0].Rows[i]["QXBM"];
                else if (i > 0)
                {
                    ajlbin += " OR AJLB_BM:" + ds2.Tables[0].Rows[i]["QXBM"];
                }
                if (i == ds2.Tables[0].Rows.Count - 1)
                    ajlbin += ")";
            }

            //ID
            if (!string.IsNullOrEmpty(id))
            {
                jsonParas += " AND id:\"" + id + "\"";
            }
            //部门受案号
            if (!string.IsNullOrEmpty(bmsah))
            {
                jsonParas += " AND BMSAH:\"" + HttpUtility.UrlEncode(bmsah)+"\"";
            }
            //案件名称
            if (!string.IsNullOrEmpty(ajmc))
            {
                jsonParas += " AND AJMC:" + HttpUtility.UrlEncode(ajmc);
            }
            //承办人
            //if (!string.IsNullOrEmpty(cbr))
            //{
            //    jsonParas +=" AND CBR:" + HttpUtility.UrlEncode(cbr);
            //}
            //受理开始日期
            if (!string.IsNullOrEmpty(slrq1))
            {
                if (!string.IsNullOrEmpty(slrq1) && !string.IsNullOrEmpty(slrq2))
                    jsonParas += " AND CJSJ:[\"" + Convert.ToDateTime(slrq1).ToString("yyyy-MM-ddTHH:mm:ss" + "Z") + "\" TO \"" + Convert.ToDateTime(slrq2).AddDays(1).ToString("yyyy-MM-ddTHH:mm:ss" + "Z") + "\"]";
                else if(!string.IsNullOrEmpty(slrq1) && string.IsNullOrEmpty(slrq2))
                    jsonParas += " AND CJSJ:[\"" + Convert.ToDateTime(slrq1).ToString("yyyy-MM-ddTHH:mm:ss" + "Z") + "\" TO \"" + DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss" + "Z") + "\"]";
                //["2016-01-16T00:00:00Z" TO "2016-01-16T23:59:59Z"]             
            }
            if (!string.IsNullOrEmpty(txt))
            {
                jsonParas += " AND WSCFLJ:" + HttpUtility.UrlEncode(txt);
                
            }
            if (!string.IsNullOrEmpty(dwbmin))
            {
                jsonParas += dwbmin;
            }
            if (!string.IsNullOrEmpty(ajlbin))
            {
                jsonParas += ajlbin;
            }
            if (!string.IsNullOrEmpty(jsonParas))
            {
                jsonParas = Regex.Replace(jsonParas, @"(^\s(AND)\s" + (true ? "*" : "") + @"|\s(AND)\s" + (true ? "*" : "") + "$)", "");                

                //jsonParas = HttpUtility.UrlEncode(jsonParas); //+ " AND parent:VIEW_DZJZ"
              

                return Post(jsonParas);
            }
            return "[]";
        } 
        #endregion
      

        #region 请求方法
        /// <summary>
        /// 请求方法
        /// </summary>
        /// <param name="jsonParas"></param>
        /// <returns></returns>
        public string Post(string jsonParas)
        {
            int count = 10;
            string rows = Request.Form["rows"];
            if (string.IsNullOrEmpty(rows))
                rows = "0";
            else
            {
                rows = ((int.Parse(rows) - 1) * count).ToString();
            }

            string url = ConfigHelper.GetConfigString("FullQuery");//配置地址格式：http://10.1.1.161:8080/solr/search/select?

            // strURL += "&wt=json&start=" + rows + "&rows=" + count + "&sort=&hl=true&hl.simple.pre=<em>&hl.simple.post=<%2Fem>"
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("q",jsonParas);
            dic.Add("wt", "json");
            dic.Add("start", rows);
            dic.Add("rows", count.ToString());
            dic.Add("hl", "true");
            dic.Add("hl.simple.pre", HttpUtility.UrlEncode("<em>"));
            dic.Add("hl.simple.post", HttpUtility.UrlEncode("</em>"));
         
            EDRS.Common.WebClient uea = new EDRS.Common.WebClient();
            return uea.Post(url, dic, Encoding.UTF8);


            string strURL = url; 
            strURL += "q=" + jsonParas;
            strURL += "&wt=json&start=" + rows + "&rows=" + count + "&sort=&hl=true&hl.simple.pre=<em>&hl.simple.post=<%2Fem>";// +"/" + methodName;
            //创建一个HTTP请求 
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strURL);
            //Post请求方式 
            request.Method = "POST";
            //内容类型
            request.ContentType = "application/x-www-form-urlencoded";
            //request.ContentType = "application/json";
            //设置参数，并进行URL编码 
            //虽然我们需要传递给服务器端的实际参数是JsonParas(格式：[{\"UserID\":\"0206001\",\"UserName\":\"ceshi\"}])，
            //但是需要将该字符串参数构造成键值对的形式（注："paramaters=[{\"UserID\":\"0206001\",\"UserName\":\"ceshi\"}]"），
            //其中键paramaters为WebService接口函数的参数名，值为经过序列化的Json数据字符串
            //最后将字符串参数进行Url编码
            //string paraUrlCoded = System.Web.HttpUtility.UrlEncode("q");
            //paraUrlCoded += "=" + System.Web.HttpUtility.UrlEncode(jsonParas);




            //byte[] payload;
            ////将Json字符串转化为字节 
            //payload = System.Text.Encoding.UTF8.GetBytes(paraUrlCoded);
            ////设置请求的ContentLength 
            //request.ContentLength = payload.Length;
            ////发送请求，获得请求流 

            Stream writer;
            try
            {
                writer = request.GetRequestStream();//获取用于写入请求数据的Stream对象
            }
            catch (Exception ex)
            {
                writer = null;
                return ReturnString.JsonToString(Prompt.error, "连接服务器失败,请检查服务器是否正常", null);
            }
            ////将请求参数写入流
            //writer.Write(payload, 0, payload.Length);
            //writer.Close();//关闭请求流

            String strValue = "";//strValue为http响应所返回的字符流

            HttpWebResponse response;
            try
            {
                //获得响应流
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                response = ex.Response as HttpWebResponse;
                return ReturnString.JsonToString(Prompt.error, ex.Message, null);
            }

            Stream s = response.GetResponseStream();
            StreamReader respStreamReader = new StreamReader(s, Encoding.UTF8);
            strValue = respStreamReader.ReadToEnd();

            s.Close();

            return strValue;//返回Json数据
        } 
        #endregion
    }
}