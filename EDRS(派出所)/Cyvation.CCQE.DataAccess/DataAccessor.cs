using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Cyvation.CCQE.DataAccess
{
    public class DataAccessor : IDisposable
    {
        private ConcurrentDictionary<string, Database> cache;
        /// <summary>
        /// VPD开关，默认关闭。
        /// </summary>
        public static bool UseVpd
        {
            get;
            set;
        }

        private static readonly DataAccessor DataAccessorInstance;
        private static readonly object SyncRoot = new object();

        static DataAccessor()
        {
            UseVpd = true;
            DataAccessorInstance = new DataAccessor();
        }

        private DataAccessor()
        {
            cache = new ConcurrentDictionary<string, Database>();
        }

        /// <summary>
        /// 根据自己的配置信息创建Database的实例
        /// </summary>
        /// <param name="name">唯一名称。可用于缓存，提高性能。</param>
        /// <param name="connectionString">不带用户名和密码的连接字符串</param>
        /// <param name="providerName">提供者名称。例如System.Data.OracleClient</param>
        /// <param name="userId">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public static Database CreateDatabase(string keyname,string conName)
        {

            if (string.IsNullOrEmpty(keyname))
            {
                throw new ArgumentNullException("name");
            }
            Database dbExist = null;
            if (DataAccessorInstance.cache.TryGetValue(keyname, out dbExist))
            {
                if (UseVpd)
                {
                    return dbExist;
                }
            }
            var dbNew = DatabaseFactory.CreateDatabase(conName);
            DataAccessorInstance.cache.AddOrUpdate(keyname, dbNew, (key, existDb) => dbNew);
            return dbNew;
        }

  
        /// <summary>
        /// 创建默认连接的Database实例
        /// </summary>
        /// <returns></returns>
        public static Database CreateDatabase()
        {
            Database db = CreateDatabase("CCQE", "ConnectionString");
            return db;
        }

        /// <summary>
        /// 创建默认连接的Database实例
        /// </summary>
        /// <returns></returns>
        public static Database CreateDatabase(string keyName)
        {
            Database db = CreateDatabase(keyName, "ConnectionString");
            return db;
        }

        /// <summary>
        /// 创建数据库操作对象Database的实例
        /// </summary>
        /// <param name="name">单位编码</param>
        /// <param name="configFile">配置文件的位置</param>
        /// <returns>数据库操作对象Database</returns>
        //[Obsolete("该方法已过时，请使用无参数的CreateDatabase方法。", true)]
        //public static Database CreateDatabase(string name, string configFile)
        //{
        //    if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");
        //    if (string.IsNullOrEmpty(configFile)) throw new ArgumentNullException("configFile");
        //    Database db = null;

        //    name = CheckoutUseVpdConfig(name);
        //    if (DataAccessorInstance.cache.TryGetValue(name, out db))
        //    {
        //        return db;
        //    }
        //    return CreateAdd(name, new FileConfigurationSource(configFile));
        //}

       

        public void Dispose()
        {
            if (cache != null)
            {
                cache.Clear();
                cache = null;
            }
        }
    }
}

