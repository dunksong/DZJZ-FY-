using System.Configuration;

namespace Cyvation.CCQE.DataAccess.Configurations
{
    public class VpdConfigSectionHandler : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            var xmlElement = section["tableName"]; 
            return xmlElement != null ? xmlElement.Value : null;
        }
    }
}