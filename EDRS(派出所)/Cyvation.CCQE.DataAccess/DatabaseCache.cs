using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cyvation.CCQE.DataAccess
{
    
    static class DatabaseCreator
    {
       // private static ConcurrentDictionary<string, Database> _cache;
        private static IConfigurationSource configurationSource;      

        //static DatabaseCache()
        //{         
           // _cache = new ConcurrentDictionary<string, Database>();
        //}

        public static Database CreateDatabase()
        {
            //return _cache.GetOrAdd(string.Empty, Create);
            return Create(string.Empty);
        }

        public static Database CreateDatabase(string orgCode)
        {
            //return _cache.GetOrAdd(orgCode, Create); 
            return Create(orgCode);
        }
         
        public static Database CreateDatabase(string orgCode, IConfigurationSource configSource)
        {
            configurationSource = configSource;      
            //return _cache.GetOrAdd(orgCode, Create);
            return Create(orgCode);
        }

        static Database Create(string orgCode)
        {
            DatabaseProviderFactory factory = CreateDatabaseProviderFactory();
            try
            {
                return string.IsNullOrEmpty(orgCode) ? factory.CreateDefault() : factory.Create(orgCode);
            }
            catch (Microsoft.Practices.ServiceLocation.ActivationException ex)
            {
                if (ex.InnerException!=null && ex.InnerException.GetType() == typeof(Microsoft.Practices.Unity.ResolutionFailedException))
                    throw ex.InnerException;
                else throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        static DatabaseProviderFactory CreateDatabaseProviderFactory()
        {
            return configurationSource== null ? new DatabaseProviderFactory() : new DatabaseProviderFactory(configurationSource);
        }
    }
}
