using System.IO;
using System.Reflection;
using System.Web;

namespace Cyvation.CCQE.Common
{
    public class ConfigurationOperate
    {
        private readonly string rootPath;
        private static readonly string currentRootPath = HttpContext.Current.Server.MapPath("\\App_Data");
        private ConfigurationOperate(string rootPath)
        {
            this.rootPath = rootPath;
        }

        public static ConfigurationOperate CreateInstance(string rootPath)
        {
            return new ConfigurationOperate(rootPath);
        }

        public static ConfigurationOperate CreateInstance()
        {
            return new ConfigurationOperate(currentRootPath);
        }

        public byte[] GetConfigurationContent(string fileName, FileMode fileMode = FileMode.Open, FileAccess fileAccess = FileAccess.Read)
        {
            var fullPath = Path.Combine(rootPath, fileName);
            if (!File.Exists(fullPath)) throw new FileNotFoundException("文件不存在", fileName);
            byte[] buffer = null;
            using (var fs = new FileStream(Path.Combine(rootPath, fileName), fileMode, fileAccess))
            {
                buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
            }
            return buffer;
        }
    }
}
