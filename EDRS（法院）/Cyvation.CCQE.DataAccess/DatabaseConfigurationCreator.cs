using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Cyvation.CCQE.DataAccess
{
    class DatabaseConfigurationCreator
    {
        public IEnumerable<DbUserInfo> Users { get; private set; }
        public string ConnectionString { get; private set; }
        public IConfigurationSource ConfigurationSource { get; private set; }
        public string ProviderName { get; private set; }

        public DatabaseConfigurationCreator(string connectionString,string providerName, IEnumerable<DbUserInfo> usersInfo)
        {
            this.ConnectionString = connectionString;
            this.ProviderName = providerName;
            this.Users = usersInfo;
            this.ConfigurationSource = new DictionaryConfigurationSource();

            GenerateUsers();
        }

        void GenerateUsers()
        {
            var settings = new DatabaseSettings { DefaultDatabase = string.Empty };
            var section = new ConnectionStringsSection();

            ConfigurationSource.Add(DatabaseSettings.SectionName, settings);
            ConfigurationSource.Add("connectionStrings", section);

            foreach (var user in Users)
            {
                section.ConnectionStrings.Add(FormatConnectionString(user));
            }

            //string OdbcProviderName = ((DatabaseSettings)keyItem.ConfigurationSource.GetSection(DatabaseSettings.SectionName)).CurrentConfiguration.ConnectionStrings.ConnectionStrings["defaultConnString"].ProviderName;
             
        }

        ConnectionStringSettings FormatConnectionString(DbUserInfo user)
        {
            return new ConnectionStringSettings(user.OrgCode,
                string.Format(ConnectionString + "user id = {0}; password = {1}", user.Schema_Name, user.Password), ProviderName);
        }

        public bool ContainKey(string key)
        { 
            return Users!=null && Users.FirstOrDefault(p=>p.OrgCode==key) != null;
        }
    }
}
