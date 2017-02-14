using System.Configuration;

namespace Cyvation.CCQE.DataAccess.Configurations
{
    public class VpdConfigSection : ConfigurationSection
    {
         const string TableNameText = "tableName";
         const string ProviderNameText = "providerName";

        [ConfigurationProperty(TableNameText, DefaultValue = DefaultTableName, IsRequired = true)]
        public string TableName
        {
            get
            {
                return (string)this[TableNameText];
            }
            set
            {
                this[TableNameText] = value;
            }
        }

         [ConfigurationProperty(ProviderNameText, DefaultValue = DefaultProviderName, IsRequired = true)]
        public string ProviderName
        {
            get { return (string)this[ProviderNameText]; }
            set { this[ProviderNameText] = value; }
        }

        public  const string TableConfigSectionName = "vpdConfigSection";

        public const string DefaultTableName = "xt_vpd_dbuserconfig";
        public const string DefaultProviderName = "System.Data.OracleClient";

    }
}