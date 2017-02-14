using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Text;
using System.IO;
using EDRS.Common;
using System.Data;

namespace WebUI.Pages.Business
{
    public partial class GetService : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string type = Request["type"];
                if (type == "aj")
                {
                    EDRS.Model.XY_DZJZ_XTPZ model = GetConfigById(EnumConfig.警综平台案件信息接口地址);
                    #region 模拟测试警钟系统数据
                    //string s = "{\"DATA\":{\"aJBH\":\"J440111540000201512000024\", \"aJLX\" : \"01\", \"aJSTATE\" : \"02\", \"aJMC\" : \"李四盗窃案\", \"aB \": \"020101\", \"sL_BJSLH\" : \"J440111540000201001000003\", \"sLFXRQ \": \"2015-12-11 17:28:00\", \"ASJCZ\" : \"2015-12-11 17:28:00\", \"fASJZZ\" : \"2015-12-11 17:28:00\", \"ADD\" : \"广东省广州市白云区永平街永泰解放庄一巷1号\", \"zARS\" : \"5\", \"zAGJ \": \"A0106,A0106\", \"sWRS\" : \"2\", \"sSRS\" : \"1\", \"sSJZ\":\"100.00\",\"zYAQ\" : \"主要案情举例\", \"sLJSDW \": \"440111540000\", \"rESERVATION01 \": \"王五\", \"zBDW \": \"440111540000\"},\"success\":\"1\"}";
                    //Response.Write(s);
                    //Response.End();
                    //return; 
                    #endregion
                    string dwbm = UserDwbm.DWBM;
                    string userid = UserInfo.GZZH;
                    string username = UserInfo.MC;
                    string ajbh = Request["ajbh"];
                    //if (string.IsNullOrEmpty(ajbh))
                    //    ajbh = "A4401115400002016020008";
                    string url = model.CONFIGVALUE;
                    string param = @"log={USER_ID : '" + userid + "', USER_NAME : '" + username + "', DEPARTMENTCODE : '" + dwbm + "'}&data={AJBH: '" + ajbh + "'}";
                    byte[] bs = Encoding.ASCII.GetBytes(param);
                    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                    req.Method = "post";
                    req.ContentType = "application/x-www-form-urlencoded";
                    req.ContentLength = bs.Length;
                    try
                    {
                        using (Stream reqStream = req.GetRequestStream())
                        {
                            reqStream.Write(bs, 0, bs.Length);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHelper.LogError(this.Request, "Exception", ex.Message, "", "");
                        Response.Write(ReturnString.JsonToString(Prompt.error, ex.Message, null));
                        Response.End();
                    }
                    using (WebResponse wr = req.GetResponse())
                    {
                        //在这里对接收到的页面内容进行处理
                        StreamReader sr = new StreamReader(wr.GetResponseStream(), Encoding.UTF8);
                        string strhtml = sr.ReadToEnd();
                        sr.Close();

                        // LogHelper.LogError(this.Request, "TestAJ", strhtml, "", "");
                        Response.Write(strhtml);
                        Response.End();
                    }
                }
                else if (type == "ws")
                {
                    EDRS.Model.XY_DZJZ_XTPZ model = GetConfigById(EnumConfig.警综平台文书信息接口地址);

                    #region 测试接口
                    /*
                    string s = "{\"success\":\"1\",\"Rows\":[{\"wRITID\":\"PCS4406201606210000000001140509\",\"wSMC\":\"呈请立案报告书\",\"zXDXJY\":null},{\"wRITID\":\"PCS4406201606210000000001140614\",\"wSMC\":\"呈请搜查报告书\",\"zXDXJY\":\"张三\"},{\"wRITID\":\"PCS4406201606210000000001140393\",\"wSMC\":\"受案登记表\",\"zXDXJY\":null},{\"wRITID\":\"PCS4406201606210000000001140416\",\"wSMC\":\"受案登记表\",\"zXDXJY\":null},{\"wRITID\":\"PCS4406201606210000000001140855\",\"wSMC\":\"呈请逮捕报告书\",\"zXDXJY\":\"张三等人\"},{\"wRITID\":\"PCS4406201606210000000001140866\",\"wSMC\":\"提请批准逮捕书\",\"zXDXJY\":\"张三等人\"},{\"wRITID\":\"PCS4406201606210000000001140973\",\"wSMC\":\"呈请终止侦查报告书\",\"zXDXJY\":\"张三等人\"},{\"wRITID\":\"PCS4406201606210000000001140647\",\"wSMC\":\"呈请搜查报告书\",\"zXDXJY\":\"李四\"},{\"wRITID\":\"PCS4406201606210000000001141105\",\"wSMC\":\"呈请破案报告书\",\"zXDXJY\":null},{\"wRITID\":\"PCS4406201606210000000001141245\",\"wSMC\":\"呈请案件侦查终结报告书\",\"zXDXJY\":null},{\"wRITID\":\"PCS4406201606280000000001189359\",\"wSMC\":\"起诉意见书\",\"zXDXJY\":\"张三\"}]}";

                    string row2 = JsonHelper.DeserializeObjectKey("[" + s + "]", "Rows");

                    string condition2 = Request.Form["condition"];
                    if (!string.IsNullOrEmpty(condition2))
                    {
                        //  string w = JsonHelper.DeserializeObjectKey(condition, "field");
                        string v = JsonHelper.DeserializeObjectKey(condition2, "value");
                        DataTable dt = JsonHelper.ToDataTable(row2);

                        DataView view = dt.DefaultView;
                        view.RowFilter = String.Format("wRITID like '%{0}%' or wSMC like '%{0}%' or zXDXJY like '%{0}%'", v);
                        DataTable dtNew = view.ToTable();

                        s = "{\"success\":\"1\",\"Rows\":" + JsonHelper.JsonString(dtNew) + "}";

                        Response.Write(s);
                        Response.End();
                    }


                    Response.Write(s);
                    Response.End();
                    return; 
                     * */
                    #endregion

                    string dwbm = UserDwbm.DWBM;
                    string userid = UserInfo.GZZH;
                    string username = UserInfo.MC;
                    string ajbh = Request["ajbh"];
                    //LogHelper.LogError(this.Request, "调用数据：" + ajbh, "WebUI.Pages.Business.GetService.Page_Load", "WS");
                    if (string.IsNullOrEmpty(ajbh))
                    {
                        return;
                    }
                    string url = model.CONFIGVALUE;
                    //string param = @"data={log:{USER_ID : '" + userid + "', USER_NAME : '" + username + "', DEPARTMENTCODE : '" + dwbm + "'}&condition={AJBH: '" + ajbh + "'}}";
                    string param = @"data={log:{USER_ID : '" + "sylmj" + "', USER_NAME : '" + "三元里民警" + "', DEPARTMENTCODE : '" + "440111540000" + "'},condition={AJBH: '" + ajbh + "'}}";
                    byte[] bs = Encoding.ASCII.GetBytes(param);
                    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                    req.Method = "post";
                    req.ContentType = "application/x-www-form-urlencoded";
                    req.ContentLength = bs.Length;
                    try
                    {
                        using (Stream reqStream = req.GetRequestStream())
                        {
                            reqStream.Write(bs, 0, bs.Length);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHelper.LogError(this.Request, "Exception", ex.Message, "", "");
                        Response.Write(ReturnString.JsonToString(Prompt.error, ex.Message, null));
                        Response.End();
                    }
                    using (WebResponse wr = req.GetResponse())
                    {
                        //在这里对接收到的页面内容进行处理
                        StreamReader sr = new StreamReader(wr.GetResponseStream(), Encoding.UTF8);
                        string strhtml = sr.ReadToEnd();
                        sr.Close();
                        strhtml = strhtml.Replace("WS_LIST", "Rows");
                        // LogHelper.LogError(this.Request, "TestWS原数据", strhtml, "", "");
                        string row = JsonHelper.DeserializeObjectKey("[" + strhtml + "]", "Rows");
                        DataTable dt = JsonHelper.ToDataTable(row);
                        DataView view = dt.DefaultView;
                        view.RowFilter = String.Format("wSMC = '{0}' or wSMC = '{1}' or wSMC = '{2}' or wSMC = '{3}' or wSMC = '{4}' or wSMC = '{5}' or wSMC = '{6}' or wSMC = '{7}'", "起诉意见书", "补充侦查报告书", "要求复议意见书", "提请复核意见书", "呈请延长侦查羁押期限报告书", "呈请重新计算侦查羁押期限报告书（逮捕）", "提请批准逮捕书", "呈请变更逮捕措施报告书");
                        DataTable dtNew = view.ToTable();
                        strhtml = "{\"Rows\":" + JsonHelper.JsonString(dtNew) + "}";
                        //起诉意见书
                        //补充侦查报告书
                        //要求复议意见书
                        //提请复核意见书
                        //呈请延长侦查羁押期限报告书
                        //呈请重新计算侦查羁押期限报告书（逮捕）
                        //提请批准逮捕书
                        //呈请变更逮捕措施报告书


                        //搜索参数
                        string condition = Request.Form["condition"];

                        // LogHelper.LogError(this.Request, "TestWS原数据后" + ajbh +"-dddd-"+ condition+"--"+view.Count, strhtml, "", "");

                        if (!string.IsNullOrEmpty(condition) && condition != "[]")
                        {
                            string v = JsonHelper.DeserializeObjectKey(condition, "value");
                            view.RowFilter = String.Format("wRITID like '%{0}%' or wSMC like '%{0}%' or zXDXJY like '%{0}%'", v);
                            dtNew = view.ToTable();
                            strhtml = "{\"Rows\":" + JsonHelper.JsonString(dtNew) + "}";
                            // LogHelper.LogError(this.Request, "TestWS处理后", strhtml, "", "");
                        }

                        Response.Write(strhtml);
                        //  LogHelper.LogError(this.Request, "Exception", strhtml, "", "");
                        //string s = "{\"Rows\":[{\"wRITID\":\"PCS4413201409250000000003016122\",\"wSMC\":\"立案决定书\",\"zXDXJY\":\"\"},{\"wRITID\":\"PCS4413201409222222222222222\",\"wSMC\":\"拘留证\",\"zXDXJY\":\"李五\"}]}";
                        //Response.Write(s);
                        Response.End();
                    }
                }

            }
        }

        #region 根据系统配置的配置编号获取对应的配置信息
        /// <summary>
        /// 根据系统配置的配置编号获取对应的配置信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EDRS.Model.XY_DZJZ_XTPZ GetConfigById(EnumConfig econfig)
        {
            string where = string.Empty;

            object[] values = new object[1];

            where = " and CONFIGID=:CONFIGID and SYSTEMMARK=1";
            values[0] = (int)econfig;


            EDRS.BLL.XY_DZJZ_XTPZ bll = new EDRS.BLL.XY_DZJZ_XTPZ(this.Request);

            List<EDRS.Model.XY_DZJZ_XTPZ> modelList = bll.GetModelList(where, values);

            if (modelList != null && modelList.Count > 0)
                return modelList[0];
            return null;
        }
        #endregion
    }
}