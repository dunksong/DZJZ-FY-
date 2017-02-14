using System;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Cyvation.CCQE.Common;

namespace Cyvation.CCQE.DataAccess.Mapping
{
    internal class KeyValueItemParameterMapper : IParameterMapper
    {
        private readonly Database database;

        internal DbCommand Command { get; private set; }

        public KeyValueItemParameterMapper(Database database)
        {
            this.database = database;
        }

        public void AssignParameters(DbCommand command, object[] parameterValues)
        {
            Command = command;
            if (parameterValues.Length > 0)
            {
                GuardParameterDiscoverySupported();

                var keyValueItems = parameterValues as KeyValueItem[];
                if (keyValueItems!= null)
                {
                    database.DiscoverParameters(command);
                    foreach (DbParameter parameter in command.Parameters)
                    {
                        var keyValueItem = keyValueItems.SingleOrDefault(
                                                   p => parameter.ParameterName.ToUpper() == p.Key.ToUpper());
                        if (keyValueItem == null)
                        {
                            throw new ArgumentNullException(string.Format("参数“{0}”不存在，请检查传入的键值对参数集。", parameter.ParameterName));
                        }  
                        parameter.Value = keyValueItem.Value;
                    }
                }
            }
        }

        private void GuardParameterDiscoverySupported()
        {
            if (!database.SupportsParemeterDiscovery)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture,
                                  "The database type “{0}” does not support automatic parameter discovery. Use an IParameterMapper instead.",
                                  database.GetType().FullName));
            }
        }

    }
}