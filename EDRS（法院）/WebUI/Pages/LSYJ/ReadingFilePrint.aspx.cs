using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDRS.Common;
using iTextSharp.text.pdf;
using System.IO;
using System.Text;

namespace WebUI.Pages.LSYJ
{
    public partial class ReadingFilePrint : BasePage
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request["t"];

            if (!string.IsNullOrEmpty(type))
            {
                Response.ContentEncoding = Encoding.UTF8;
                Response.ContentType = "application/json";

                if (type.Equals("PrintFile"))
                {                 
                    Response.Write(GetFile());
                    Response.End();
                }
                else if (type.Equals("pf"))
                {
                    ShowFile();
                }               
            }



            //EditPDF(Server.MapPath("/images/pdf/00004.pdf"));
            //if (!IsPostBack)
            //{
            //    GetFile();
            //}



        }

        private void ShowFile()
        {
            string pf = Request.QueryString["n"];
            using (System.IO.FileStream fs = new System.IO.FileStream(Server.MapPath(string.Format("/PrintPdfFiles/{0}.pdf", pf)), System.IO.FileMode.Open))
            {
               // byte[] file = File.ReadAllBytes(Server.MapPath(string.Format("/PrintPdfFiles/{0}.pdf", pf)));
                byte[] file = new byte[fs.Length];
                fs.Read(file, 0, file.Length);
                if (file != null && file.Length > 0)
                {
                    // File.Delete(Server.MapPath(string.Format("/PrintPdfFiles/{0}.pdf", pf)));
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "filename=pdf");
                    Response.AddHeader("content-length", file.Length.ToString());
                    Response.BinaryWrite(file);
                    //Response.AddHeader("Accept-Language", "zh-tw");
                    //Response.ContentType = "application/octet-stream";
                    //Response.AppendHeader("Content-Length ", file.Length.ToString());

                    Response.Flush();
                    Response.End();
                }
                else
                {
                    Response.Write("文件不存在！");
                }
            }
        }


        /// <summary>
        /// 获取案件文件
        /// </summary>
        /// <returns></returns>
        private string GetFile()
        {
            string param = Request.Form["param"];

            if (string.IsNullOrEmpty(param))
            {
                return ReturnString.JsonToString(Prompt.error, "打印未获取到参数", null);             

            }

            string[] paramArr = param.Split(new char[]{'|'},StringSplitOptions.RemoveEmptyEntries);

            if (paramArr.Length == 0)
            {
                return ReturnString.JsonToString(Prompt.error, "访问参数错误", null);             
            }
            else
            {
                EDRS.BLL.XY_DZJZ_XTPZ bll = new EDRS.BLL.XY_DZJZ_XTPZ(this.Request);
                EDRS.Model.XY_DZJZ_XTPZ model = bll.GetModel((int)EnumConfig.卷宗文件下载地址);
                if (model != null)
                {
                    IceServicePrx isp = new IceServicePrx();

                    string msg = "";

                    byte[] bytes = new byte[] { };

                    List<byte[]> pdflist = new List<byte[]>();

                    byte[] info = null;

                    string[] paramIds = null;


                    for (int i = 0; i < paramArr.Length; i++)
                    {
                        paramIds = paramArr[i].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        if (isp.DownFile(model.CONFIGVALUE, paramIds[0], paramIds[1], "", ref bytes, ref msg))
                        {
                            info = DataEncryption.Decryption(bytes);
                            pdflist.Add(info);
                        }
                        if (!string.IsNullOrEmpty(msg))
                        {
                             return ReturnString.JsonToString(Prompt.error, StringPlus.String2Json(msg), null); 
                        }
                    }
                    string fileName =  DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    string fileParnt = string.Format("/PrintPdfFiles/{0}.pdf", fileName);
                    byte[] d = MergePDFFiles(pdflist, fileParnt);
                    //Response.ContentType = "application/pdf";
                    //Response.AddHeader("content-disposition", "filename=pdf");
                    //Response.AddHeader("content-length", d.Length.ToString());
                    //Response.BinaryWrite(d);
                    return ReturnString.JsonToString(Prompt.win, fileName, null);

                }
                else
                {
                     return ReturnString.JsonToString(Prompt.error, "请先配置" + EnumConfig.卷宗文件下载地址, null);
                }
            }

        }


        /// <summary> 合併PDF檔(集合) </summary>
        /// <param name="fileList">欲合併PDF檔之集合(一筆以上)</param>
        /// <param name="outMergeFile">合併後的檔名</param>
        private byte[] MergePDFFiles(List<byte[]> fileList, string outMergeFile)
        {
            
            outMergeFile = Server.MapPath(outMergeFile);
            PdfReader reader;
            iTextSharp.text.Document document = new iTextSharp.text.Document();
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(outMergeFile, FileMode.Create));
            document.Open();
            PdfContentByte cb = writer.DirectContent;
            PdfImportedPage newPage;
            for (int i = 0; i < fileList.Count; i++)
            {
                // reader = new PdfReader(Server.MapPath(fileList[i]));
                reader = new PdfReader(fileList[i]);

                int iPageNum = reader.NumberOfPages;

                for (int j = 1; j <= iPageNum; j++)
                {
                    document.NewPage();
                    newPage = writer.GetImportedPage(reader, j);
                    cb.AddTemplate(newPage, 0, 0);
                }
            }
            document.Close();

           byte[] file=  PdfWriter.GetISOBytes(outMergeFile);
           return file;
            
        }


        private void EditPDF(string fpath)
        {

            string path = fpath.Replace("\\", "/");
            PdfReader reader = new PdfReader(path);
            MemoryStream ms = new MemoryStream();
            PdfStamper stamper = new PdfStamper(reader, ms);
            stamper.Writer.ViewerPreferences = PdfWriter.HideWindowUI;
            stamper.Writer.SetEncryption(PdfWriter.STRENGTH128BITS, null, null, PdfWriter.AllowPrinting | PdfWriter.AllowFillIn);
            stamper.Writer.CloseStream = false;
            //直接弹出打印不用点击打印按钮
            //PdfAction.JavaScript("myOnMessage();", stamper.Writer);
            //stamper.Writer.AddJavaScript("this.print(true);function myOnMessage(aMessage) {app.alert('Test',2);} var msgHandlerObject = new Object();doc.onWillPrint = myOnMessage;this.hostContainer.messageHandler = msgHandlerObject;");

            //StringBuilder script = new StringBuilder();
            //script.Append("this.print({bUI: false,bSilent: true,bShrinkToFit: true});").Append("\r\nthis.closeDoc();");
            //script.Append("var pp = this.getPrintParams();pp.printerName = '\\\\fpserver\\hp LaserJet 1010'; this.print(pp);");
            //script.Append("this.print(flase);");

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