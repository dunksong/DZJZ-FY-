using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;

namespace WebUI.AddFile.server
{
    /// <summary>
    /// fileupload 的摘要说明
    /// </summary>
    public class fileupload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //指定字符集
            context.Response.ContentEncoding = Encoding.UTF8;
            if (context.Request["REQUEST_METHOD"] == "OPTIONS")
            {
                context.Response.End();
            }
            SaveFile(context);
        }
        /// <summary></summary>
        /// 文件保存操作
        /// 
        /// <param name="basepath"></param name="basepath">
        private void SaveFile(HttpContext context)
        {
            //EDRS.BLL.YX_DZJZ_LSZZWJ bll = new  EDRS.BLL.YX_DZJZ_LSZZWJ(context.Request);
            //int index = (int)EDRS.Common.EnumConfig.律师资质文件存储路径;
            string basePath = "/Upload/LSZZImages/";
            string strPath = basePath;
            //basePath = bll.GetModel(index).CONFIGVALUE;
            basePath = System.Web.HttpContext.Current.Server.MapPath(basePath);
            var name = string.Empty;
            if (!basePath.EndsWith("\\"))
            {
                basePath += "\\";
            }
            //创建临时文件路径，在保存到数据库中时删除临时文件
            //basePath += "_temp\\";
            HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            //如果目录不存在，则创建目录
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }

            var suffix = files[0].ContentType.Split('/');
            //获取文件格式
            var _suffix = suffix[1].Equals("jpeg", StringComparison.CurrentCultureIgnoreCase) ? "" : suffix[1];
            var _temp = System.Web.HttpContext.Current.Request["name"];
            //如果不修改文件名，则创建随机文件名
            if (!string.IsNullOrEmpty(_temp))
            {
                name = _temp;
                name = Guid.NewGuid().ToString() + _temp.Substring(_temp.LastIndexOf('.'));
            }
            else
            {
                Random rand = new Random(24 * (int)DateTime.Now.Ticks);
                name = rand.Next() + "." + _suffix;
            }
            //文件保存
            var full = basePath + name;
            files[0].SaveAs(full);
            var _result = "{\"jsonrpc\" : \"2.0\", \"result\" : null, \"id\" : \"" + strPath+name + "\"}";

            System.Web.HttpContext.Current.Response.Write(_result);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
     
}