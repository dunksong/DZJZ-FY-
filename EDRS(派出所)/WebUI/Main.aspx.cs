using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EDRS.BLL;
using EDRS.Common;
using System.Data;
using EDRS.Common.DEncrypt;
using System.Text.RegularExpressions;
using System.Collections;

namespace WebUI
{
    public partial class Main : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["exit"] == "1")
            {
                //System.Web.Caching.Cache objcache = HttpContext.Current.Cache;
                //objcache.Remove("IXT_QX_GNDYList-" + UserInfo.DWBM + UserInfo.GH);
                Response.Clear();
                Response.Buffer = true;
                Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
                Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
                Response.Expires = 0;
                Response.CacheControl = "no-cache";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetNoStore();

                //Session["user"] = null;
                Session.Clear();
                Session.Abandon();

                

                Response.Redirect("Login.aspx");
            }

            string type = Request["t"];
            if (!string.IsNullOrEmpty(type))
            {

               
                Response.ContentEncoding = Encoding.UTF8;
                Response.ContentType = "application/json";
                if (type.Equals("BindingMenu"))
                    Response.Write(base.GetBindingMenu());
                if (type.Equals("AlterPwd"))
                    Response.Write(AlterPwd());
                Response.End();
            }
        }

        #region 密码修改
        /// <summary>
        /// 密码修改
        /// </summary>
        /// <returns></returns>
        private string AlterPwd()
        {
            string before = Request.Form.Get("pwd_before");
            string news = Request.Form.Get("pwd_news");
            string newsto = Request.Form.Get("pwd_newsTo");
            //验证旧密码
            if (before == null || string.IsNullOrEmpty(before))
                return ReturnString.JsonToString(Prompt.error, "旧密码不能为空", "pwd_before");
            if (!Regex.IsMatch(before, @"^(\w){6,20}$"))
                return ReturnString.JsonToString(Prompt.error, "旧密码输入不正确", "pwd_before");
            //验证新密码
            if (news == null || string.IsNullOrEmpty(news))
                return ReturnString.JsonToString(Prompt.error, "新密码不能为空", "pwd_news");
            if (!Regex.IsMatch(news, @"^(\w){6,20}$"))
                return ReturnString.JsonToString(Prompt.error, "新密码输入不正确", "pwd_news");
            //验证新密码与旧密码相同
            if (!news.Equals(newsto))
                return ReturnString.JsonToString(Prompt.error, "新密码与确认密码不一致！", "pwd_newsTo");
            //判断新密码与旧密码是否相同
            if (before.Equals(news))
                return ReturnString.JsonToString(Prompt.error, "旧密码与新密码相同无须修改！", "pwd_news");

            EDRS.BLL.XT_ZZJG_RYBM bll = new EDRS.BLL.XT_ZZJG_RYBM(this.Request);
            string msg = string.Empty;
            List<EDRS.Model.XT_QX_JSBM> jsbmList;
            EDRS.Model.XT_ZZJG_RYBM rybm = bll.UserLogin(UserInfo.DWBM, UserInfo.DLBM, before, out jsbmList, out msg);
            if (rybm != null)
            {
                rybm.KL = MD5Encrypt.Encrypt(news).ToLower();
                if (bll.Update(rybm))
                    return ReturnString.JsonToString(Prompt.win, "密码修改成功，重新登录生效！", null);
                return ReturnString.JsonToString(Prompt.error, "密码修改失败！", null);
            }
            else
                return ReturnString.JsonToString(Prompt.error, msg, null);
        } 
        #endregion

        #region 测试数据保存

        //        private void test() 
        //        {           

        //            TYYW_GG_AJJBXX jzajxx = new TYYW_GG_AJJBXX(this.Request);
        //            EDRS.Model.TYYW_GG_AJJBXX model = new EDRS.Model.TYYW_GG_AJJBXX();   

        //            model.BMSAH = "密检起诉受[2016]37078500026号";
        //            model.TYSAH = "37078520150084600";
        //            model.SFSC = "N";
        //            model.SFYGXTSJ = "N";
        //            model.CBDW_BM = "370785";
        //            model.CBDW_MC = "高密市院";
        //            model.FQDWBM = 3707;
        //            model.FQL = "2016";
        //            model.CJSJ = DateTime.Now;
        //            model.ZHXGSJ = DateTime.Now;
        //            model.SLRQ = DateTime.Now;
        //            model.AJMC = "邱玉波等三人贩卖毒品、李金辉等五人容留他人吸毒案";
        //            model.AJLB_BM = "0301";
        //            model.AJLB_MC = "一审公诉案件";
        //            model.ZCJG_DWDM = "0001213370785";
        //            model.ZCJG_DWMC = "高密市公安局";
        //            model.YSDW_DWDM = "0001213370785";
        //            model.YSDW_DWMC = "高密市公安局";
        //            model.YSWSWH = "高公(刑)诉字[2016]1003号";
        //            model.YSAY_AYDM = "9903106070100";
        //            model.YSAY_AYMC = "走私、贩卖、运输、制造毒品罪";
        //            model.YSQTAY_AYDMS = "9903106071100";
        //            model.YSQTAY_AYMCS = "容留他人吸毒罪";
        //            model.CBRGH = "0023";
        //            model.CBR = "李世乐";
        //            model.CBBM_BM = "0003";
        //            model.CBBM_MC = "公诉科";
        //            model.AJZT = "1";
        //            model.SFSWAJ = "N";
        //            model.SFDBAJ = "N";
        //            model.ZXHD_MC = "";
        //            model.WCRQ = DateTime.Now;
        //            model.GDRQ = DateTime.Now;
        //            model.GDRGH = "";
        //            model.GDR = "";
        //            model.AQZY = @"一、邱玉波、王武杰涉嫌贩卖毒品的犯罪事实
        //犯罪嫌疑人邱玉波、王武杰于2015年5月份相识，后邱玉波拉拢王武杰帮助其贩卖冰毒，王武杰在明知邱玉波贩卖冰毒的情况下多次以介绍购买冰毒人员、运送冰毒的方式帮助邱玉波贩卖冰毒。
        //1、2015年7月份的一天下午，犯罪嫌疑人王晨（另案已取保）通过犯罪嫌疑人王武杰的介绍，在高密市长丰街博然客房东侧邱玉波暂住房内交给犯罪嫌疑人邱玉波1000元钱购买冰毒。当天晚上，犯罪嫌疑人王武杰在犯罪嫌疑人邱玉波的指使下，帮助犯罪嫌疑人邱玉波将2.7克冰毒送至高密市毛家庄犯罪嫌疑人王晨租房处西侧空场交给犯罪嫌疑人王晨。
        //2、2015年7月底的一天晚上，犯罪嫌疑人王刚（另案已刑拘）联系犯罪嫌疑人邱玉波购买200元的冰毒，后犯罪嫌疑人邱玉波指使犯罪嫌疑人王武杰与犯罪嫌疑人王刚联系，并指使犯罪嫌疑人王武杰将0.5克冰毒送至高密市毛家庄向群超市处交给犯罪嫌疑人王刚。
        //二、犯罪嫌疑人李金辉涉嫌贩卖毒品、容留他人吸毒的犯罪事实
        //贩卖毒品部分：
        //2013年11月的一天，犯罪嫌疑人李金辉为获取非法利益，在高密市谭上村其家中西间卧室内以300元的价格向任玉亮（已强戒）出售毒品冰毒0.6克。
        //容留他人吸毒部分
        //1、2013年10月的一天，犯罪嫌疑人李金辉在高密市创业街东头其经营的青岛啤酒店二楼容留任玉亮吸食毒品冰毒一次。
        //2、2014年9月24日，犯罪嫌疑人李金辉在高密市刘新村村牌子北侧其表哥的厂房西间卧室内，容留秦浩（在逃）、任玉亮吸食毒品冰毒一次。
        //3、2014年10月的一天，犯罪嫌疑人李金辉在高密市刘新村村牌子北侧其表哥的厂房西间卧室内，容留任玉亮、犯罪嫌疑人王海鑫（另案已移诉）吸食毒品冰毒一次。
        //4、2014年11月的一天，犯罪嫌疑人李金辉在高密市刘新村村牌子北侧其表哥的厂房西间卧内，室容留任玉亮吸食毒品冰毒一次。
        //5、2015年6、7月的一天，犯罪嫌疑人李金辉在高密市醴泉大街想唱就唱KTV门口路边其停放的车内，容留任玉亮吸食毒品冰毒一次。
        //6、2015年7月份的一天，犯罪嫌疑人李金辉在高密市刘新村村牌子北侧其表哥的厂房西间卧室内，容留犯罪嫌疑人邱玉波吸食毒品冰毒一次。
        //7、2015年7月份的一天，犯罪嫌疑人李金辉在高密市刘新村村牌子北侧其表哥的厂房西间卧室内，再次容留犯罪嫌疑人邱玉波吸食毒品冰毒一次。
        //8、2015年7、8月份的一天，犯罪嫌疑人李金辉在高密市杨戈庄张涛家胡同东侧其停放的车内，容留犯罪嫌疑人张涛（已刑拘）吸食毒品冰毒一次。
        //9、2015年9月份的一天，犯罪嫌疑人李金辉在高密市刘新村村牌子北侧其表哥的厂房大门南侧的小屋内，容留任玉亮、犯罪嫌疑人单鹏程（已取保）吸食毒品冰毒一次。
        //10、2015年10月14日，犯罪嫌疑人李金辉在高密市刘新村村牌子北侧其表哥的厂房西间卧室内，容留犯罪嫌疑人张涛吸食毒品冰毒一次。
        //犯罪嫌疑人李金辉共贩卖毒品1次，容留他人吸食毒品冰毒10次共13人次。";
        //            model.DQJD = "二次退回补充侦查";
        //            model.BLKSRQ = DateTime.Now;
        //            model.BLTS = 0;
        //            model.DQRQ = DateTime.Now;
        //            model.BJRQ = DateTime.Now;
        //            model.YJZT = "0";
        //            model.JYYJZT = "0";
        //            model.JYYJHCQXYRGS = 0;
        //            model.LCSLBH = "01";
        //            model.FXDJ = "";
        //            model.SFGZAJ = "N";
        //            model.FZ = "邱玉波1999年1月12日因犯盗窃罪被高密市人民法院判处有期徒刑六个月，缓刑一年，并处罚金人民币二千元；2014年2月12日因吸毒被高密市公安局行政拘留十日并处罚款二千元；2014年7月8日因犯寻衅滋事罪被高密市人民法院判处有期徒刑一年二个月。王武杰2009年2月27日因犯故意伤害罪被高密市人民法院判处有期徒刑一年，缓刑一年；2010年5月10日因寻衅滋事被高密市公安局行政拘留十日；2015年9月18日因吸毒被高密市公安局行政拘留十日并处罚款二千元；2015年10月9日因吸毒被高密市公安局行政拘留十四日并处罚款二千元。";
        //            model.YSYJ_DM = "9930108100001";
        //            model.YSYJ_MC = "移送起诉";
        //            model.SFJBAJ = "N";
        //            model.ZXHD_DM = "";
        //            model.DQYJJD = "一次退查重报";
        //            model.YASCSSJD_DM = "";
        //            model.YASCSSJD_MC = "";
        //            model.YSRJDH = "高峰";
        //            model.XYR = "";
        //            model.SFZH = "";
        //            model.TARYXX = "";
        //            jzajxx.Add(model);



        //        }
        
        #endregion
            
    }
}