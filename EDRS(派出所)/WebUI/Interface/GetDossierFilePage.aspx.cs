using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using EDRS.Common;
using iTextSharp.text.pdf;
using System.Data;

namespace WebUI.Interface
{
    public partial class GetDossierFilePage : System.Web.UI.Page
    {
        //?DWBM=&BH=&JZBH=&JZWJYBH=
        //页面加载类
        protected void Page_Load(object sender, EventArgs e)
        {
            GetFile();
        }

        /// <summary>
        /// 获取案件文件
        /// </summary>
        /// <returns></returns>
        private void GetFile()
        {
            string wjlj = "";
            string wjmc = "";
         

          //  string dwbm = HttpUtility.UrlDecode(HttpUtility.UrlDecode(Request["DWBM"].Trim()));
            //string bh = HttpUtility.UrlDecode(HttpUtility.UrlDecode(Request["BH"].Trim()));
            string jzbh = HttpUtility.UrlDecode(HttpUtility.UrlDecode(Request["JZBH"].Trim()));
            string jzwjybh = HttpUtility.UrlDecode(HttpUtility.UrlDecode(Request["JZWJYBH"].Trim()));
         
            EDRS.BLL.YX_DZJZ_JZMLWJ bll = new EDRS.BLL.YX_DZJZ_JZMLWJ(HttpContext.Current.Request);
            DataSet ds = bll.GetList(" and JZBH=:JZBH and WJXH=:WJXH", new object[] {  jzbh, jzwjybh });

            //判断是否查询出现异常
            if (ds != null && ds.Tables.Count > 0)
            {
                //判断查询没有数据
                if (ds.Tables[0].Rows.Count > 0)
                {
                    wjlj = ds.Tables[0].Rows[0]["WJLJ"].ToString();
                    wjmc = ds.Tables[0].Rows[0]["WJMC"].ToString();
                }
            }
            
            if (string.IsNullOrEmpty(wjlj) || string.IsNullOrEmpty(wjmc))
            {
                Response.Write("文路径或者文件名称不存在");
            }
            else
            {
                EDRS.BLL.XY_DZJZ_XTPZ bllto = new EDRS.BLL.XY_DZJZ_XTPZ(this.Request);
                EDRS.Model.XY_DZJZ_XTPZ model = bllto.GetModel((int)EnumConfig.卷宗文件下载地址);
                if (model != null)
                {
                    IceServicePrx isp = new IceServicePrx();
                    string msg = "";
                    byte[] bytes = new byte[] { };
                    if (isp.DownFile(model.CONFIGVALUE, wjlj, wjmc, "", ref bytes, ref msg))
                    {
                        string showType = ConfigHelper.GetConfigString("FileShowType");
                        if (showType == "1")
                        {
                            Response.Write("<img style=\"max-width:100%;\" alt=\"\" src=\"data:image/jpeg;base64," + Convert.ToBase64String(bytes) + "\" />");
                        }
                        else
                        {
                            byte[] info = DataEncryption.Decryption(bytes);
                            Response.ContentType = "application/pdf";
                            Response.AddHeader("content-disposition", "filename=pdf");
                            Response.AddHeader("content-length", info.Length.ToString());
                            Response.BinaryWrite(info);
                        }
                    }
                    else
                    {
                        Response.Write(msg);
                    }
                }
                else
                    Response.Write("请先配置" + EnumConfig.卷宗文件下载地址);
            }

        }

        
        /// <summary>
        /// 未使用方法
        /// </summary>
        /// <param name="fpath"></param>
        private void EditPDF(string fpath)
        {

            string path = fpath.Replace("\\", "/");
            PdfReader reader = new PdfReader(path);
            MemoryStream ms = new MemoryStream();
            PdfStamper stamper = new PdfStamper(reader, ms);
            stamper.Writer.ViewerPreferences = PdfWriter.HideWindowUI;
            stamper.Writer.SetEncryption(PdfWriter.STRENGTH128BITS, null, null, PdfWriter.AllowPrinting | PdfWriter.AllowFillIn);
            stamper.Writer.CloseStream = false;         

            string script = "<script type=\"text/javascript\">";
            script += "window.onload=function(){";
            script += "document.all[document.getElementById(\"PDFNotKnown\") ? \"IfNoAcrobat\" : \"showdiv\"].style.display = \"block\";";

            script += "if(document.getElementById(\"showdiv\").style.display==\"block\"){";
            script += "pdf.SetShowToolbar(false); }}";



            // 禁用右键功能 
            //function stop(){
            // return false;
            // }
            //document.oncontextmenu=stop; 

            //禁止F8按钮
            script += "function keypressed() {";
            script += "if(event.keyCode == 119) {";
            script += "event.keyCode = 0;";
            script += "return false;}}";
            script += "</script>";


            stamper.Writer.AddJavaScript(script.ToString(), false);

            //PdfContentByte cb = stamper.GetOverContent(1);
            //cb.Circle(250, 250, 50);
            //cb.SetColorFill(iTextSharp.Color.RED);
            //cb.SetColorStroke(iTextSharp.text.Color.WHITE);
            //cb.FillStroke();
            stamper.Close();

            ViewPdf(ms);


        }
        private void ViewPdf(Stream fs)
        {
            byte[] buffer = new byte[fs.Length];
            fs.Position = 0;
            fs.Read(buffer, 0, (int)fs.Length);
            Response.Clear();
            //Response.AddHeader("Content-Disposition", "attachment;FileName=out.pdf");
            Response.AddHeader("Content-Length", fs.Length.ToString());
            //Response.AddHeader("Content-Disposition", "inline;FileName=out.pdf");
            Response.ContentType = "application/pdf";
            fs.Close();

            Response.BinaryWrite(buffer);
            Response.OutputStream.Flush();
            Response.OutputStream.Close();
        }

    }
}