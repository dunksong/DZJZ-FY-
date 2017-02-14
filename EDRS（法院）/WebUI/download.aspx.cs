using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace WebUI
{
    public partial class download : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request["t"];
            if (!string.IsNullOrEmpty(type))
            {
                Response.ContentEncoding = Encoding.UTF8;
                Response.ContentType = "application/json";
                switch (type)
                {                    
                    case "getmodel":
                        Response.Write(GetModelById());
                        break;                   
                }
                Response.End();
            }
        }

        /// <summary>
        /// 读取文件下载
        /// </summary>
        /// <returns></returns>
        private string GetModelById() 
        {            
            string file_id = Request["fileid"];
            if (!string.IsNullOrEmpty(file_id) && this.UserInfo != null)
            {
                if (DownloadFile("", file_id))
                {
                    return "文件下载成功";
                }
                return "文件下载失败";
            }
            return "下载文件不存在";
        }

        

        /// <summary>
        /// WriteFile分块下载 
        /// </summary>
        private bool DownloadFile(string name, string path)
        {

            //WriteFile分块下载 
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmss");//客户端保存的文件名 
            string filePath = Server.MapPath(path);//文件下载路径 
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(filePath);
            if (fileInfo.Exists == true)
            {
                fileName += fileInfo.Extension;
                const long ChunkSize = 102400;//100K 每次读取文件，只读取100Ｋ，这样可以缓解服务器的压力 
                byte[] buffer = new byte[ChunkSize];
                Response.Clear();
                System.IO.FileStream iStream = System.IO.File.OpenRead(filePath);
                long dataLengthToRead = iStream.Length;//获取下载的文件总大小 
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileName));
                while (dataLengthToRead > 0 && Response.IsClientConnected)
                {
                    int lengthRead = iStream.Read(buffer, 0, Convert.ToInt32(ChunkSize));//读取的大小 
                    Response.OutputStream.Write(buffer, 0, lengthRead);
                    Response.Flush();
                    dataLengthToRead = dataLengthToRead - lengthRead;
                }
                Response.Close();
                return true;
            }
            return false;
        }
    }
}