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
    public partial class TestJZ : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
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

        #region 获取警钟数据
        private void GetData()
        {
            EDRS.Model.XY_DZJZ_XTPZ model = GetConfigById(EnumConfig.警综平台文书信息接口地址);

            string ajbh = this.TextBox1.Text.Trim();
            //LogHelper.LogError(this.Request, "调用数据：" + ajbh, "WebUI.Pages.Business.GetService.Page_Load", "WS");
            if (string.IsNullOrEmpty(ajbh))
            {
                this.TextBox1.Text = "请输入案件编号";
                return;
            }
            string url = model.CONFIGVALUE;
            //string param = @"data={log:{USER_ID : '" + userid + "', USER_NAME : '" + username + "', DEPARTMENTCODE : '" + dwbm + "'}&condition={AJBH: '" + ajbh + "'}}";
            string param = @"data={log:{USER_ID : '" + "sylmj" + "', USER_NAME : '" + "三元里民警" + "', DEPARTMENTCODE : '" + "440111540000" + "'},condition={AJBH: '" + ajbh + "'}}";
            byte[] bs = Encoding.ASCII.GetBytes(param);
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "post";
            req.Timeout = 8000;
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
                this.TextBox2.Text = ex.Message;
                //LogHelper.LogError(this.Request, "Exception", ex.Message, "", "");
                //Response.Write(ReturnString.JsonToString(Prompt.error, ex.Message, null));
                //Response.End();
                return;
            }
            using (WebResponse wr = req.GetResponse())
            {
                //在这里对接收到的页面内容进行处理
                StreamReader sr = new StreamReader(wr.GetResponseStream(), Encoding.UTF8);
                string strhtml = sr.ReadToEnd();
                sr.Close();
                strhtml = strhtml.Replace("WS_LIST", "Rows");
                this.TextBox2.Text = strhtml;               
            }
        } 
        #endregion

        protected void Button1_Click(object sender, EventArgs e)
        {
            GetData();
        }
    
    }
}