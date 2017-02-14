/*
 * 功能说明：数据库访问扩展功能
 * 创建人：LDL
 * 创建时间：2012.12.26 
 * 
 * 版本：1.1
 * 修改人：LDL
 * 修改时间：2013.1.15
 * 修改说明：ExecuteSprocAccessor<TResult, KeyValueItem>方法，原本是不返回有输出参数的值的，现已将返回值输出到对应的KeyValueItem中；
 *          并删除了带IParameterMapper类型的接口，因为IParameterMapper已无效。
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Cyvation.CCQE.DataAccess.Mapping;
using Cyvation.CCQE.Common;

namespace Cyvation.CCQE.DataAccess
{
    public static class DatabaseExtensions
    {
        private static readonly ParameterCache cache = new ParameterCache();

        public static DataSet ExecuteDataSet<TParam>(this Database database,
                                                     string storedProcedureName,
                                                     params TParam[] parameterValues)
            where TParam : KeyValueItem
        {
            using (var command = GetDbCommand(database, storedProcedureName, parameterValues))
            {
                DataSet ds = database.ExecuteDataSet(command);

                if (command != null && parameterValues != null && parameterValues.Length > 0)
                {
                    FillOutputItemValue(database, command, parameterValues);
                }
                return ds;
            }
        }

        public static DataSet ExecuteDataSet<TParam>(this Database database,
                                                     DbTransaction transaction,
                                                     string storedProcedureName,
                                                     params TParam[] parameterValues)
            where TParam : KeyValueItem
        {
            using (var command = GetDbCommand(database, storedProcedureName, parameterValues))
            {
                DataSet ds = database.ExecuteDataSet(command, transaction);
                if (command != null && parameterValues != null && parameterValues.Length > 0)
                {
                    FillOutputItemValue(database, command, parameterValues);
                }
                return ds;
            }
        }

        public static int ExecuteNonQuery<TParam>(this Database database,
                                                  string storedProcedureName,
                                                  params TParam[] parameterValues)
            where TParam : KeyValueItem
        {
            using (DbCommand command = GetDbCommand(database, storedProcedureName, parameterValues))
            {
                int r = database.ExecuteNonQuery(command);

                if (command != null && parameterValues != null && parameterValues.Length > 0)
                {
                    FillOutputItemValue(database, command, parameterValues);
                }
                return r;
            }
        }

        public static int ExecuteNonQuery<TParam>(this Database database,
                                                  DbTransaction transaction,
                                                  string storedProcedureName,
                                                  params TParam[] parameterValues)
            where TParam : KeyValueItem
        {
            using (DbCommand command = GetDbCommand(database, storedProcedureName, parameterValues))
            {
                int r = database.ExecuteNonQuery(command, transaction);

                if (command != null && parameterValues != null && parameterValues.Length > 0)
                {
                    FillOutputItemValue(database, command, parameterValues);
                }
                return r;
            }
        }

        public static IDataReader ExecuteReader<TParam>(this Database database,
                                                        string storedProcedureName,
                                                        params TParam[] parameterValues)
            where TParam : KeyValueItem
        {
            using (DbCommand command = GetDbCommand(database, storedProcedureName, parameterValues))
            {
                IDataReader dr = database.ExecuteReader(command);
                if (command != null && parameterValues != null && parameterValues.Length > 0)
                {
                    FillOutputItemValue(database, command, parameterValues);
                }
                return dr;
            }
        }

        public static IDataReader ExecuteReader<TParam>(this Database database,
                                                        DbTransaction transaction,
                                                        string storedProcedureName,
                                                        params TParam[] parameterValues)
            where TParam : KeyValueItem
        {
            using (DbCommand command = GetDbCommand(database, storedProcedureName, parameterValues))
            {
                IDataReader dr = database.ExecuteReader(command, transaction);
                if (command != null && parameterValues != null && parameterValues.Length > 0)
                {
                    FillOutputItemValue(database, command, parameterValues);
                }
                return dr;
            }
        }

        public static object ExecuteScalar<TParam>(this Database database,
                                                   string storedProcedureName,
                                                   params TParam[] parameterValues)
            where TParam : KeyValueItem
        {
            using (DbCommand command = GetDbCommand(database, storedProcedureName, parameterValues))
            {
                object obj = database.ExecuteScalar(command);
                if (command != null && parameterValues != null && parameterValues.Length > 0)
                {
                    FillOutputItemValue(database, command, parameterValues);
                }
                return obj;
            }
        }

        public static object ExecuteScalar<TParam>(this Database database,
                                                   DbTransaction transaction,
                                                   string storedProcedureName,
                                                   params TParam[] parameterValues)
            where TParam : KeyValueItem
        {
            using (DbCommand command = GetDbCommand(database, storedProcedureName, parameterValues))
            {
                object obj = database.ExecuteScalar(command, transaction);
                if (command != null && parameterValues != null && parameterValues.Length > 0)
                {
                    FillOutputItemValue(database, command, parameterValues);
                }
                return obj;
            }
        }

        public static void LoadDataSet<TParam>(this Database database,
                                               string storedProcedureName,
                                               DataSet dataSet,
                                               string[] tableNames,
                                               params TParam[] parameterValues)
            where TParam : KeyValueItem
        {
            using (DbCommand command = GetDbCommand(database, storedProcedureName, parameterValues))
            {
                database.LoadDataSet(command, dataSet, tableNames);

                if (command != null && parameterValues != null && parameterValues.Length > 0)
                {
                    FillOutputItemValue(database, command, parameterValues);
                }
            }
        }

        public static void LoadDataSet<TParam>(this Database database,
                                               DbTransaction transaction,
                                               string storedProcedureName,
                                               DataSet dataSet,
                                               string[] tableNames,
                                               params TParam[] parameterValues)
            where TParam : KeyValueItem
        {
            using (DbCommand command = GetDbCommand(database, storedProcedureName, parameterValues))
            {
                database.LoadDataSet(command, dataSet, tableNames, transaction);

                if (command != null && parameterValues != null && parameterValues.Length > 0)
                {
                    FillOutputItemValue(database, command, parameterValues);
                }
            }
        }

        public static DbCommand GetStoredProcCommand<TParam>(this Database database,
                                                             string storedProcedureName,
                                                             params TParam[] parameterValues)
            where TParam : KeyValueItem
        {
            return GetDbCommand(database, storedProcedureName, parameterValues);
        }


        public static IEnumerable<TResult> ExecuteSprocAccessor<TResult, TParam>(this Database database,
                                                                                 string storedProcedureName,
                                                                                 params TParam[] parameterValues)
            where  TResult :new()  
            where TParam : KeyValueItem
        {
            return ExecuteSprocAccessor(database, storedProcedureName, new DefaultResultSetMapper<TResult>(new ReflectionRowMapperEx<TResult>()),
                                        parameterValues);
            //return database.CreateSprocAccessor<TResult>(storedProcedureName, new KeyValueItemParameterMapper(database),new ReflectionRowMapperEx<TResult>()).Execute(parameterValues);
        }

        //public static IEnumerable<TResult> ExecuteSprocAccessor<TResult, TParam>(this Database database,
        //                                                                         string storedProcedureName,
        //                                                                         IParameterMapper parameterMapper,
        //                                                                         params TParam[] parameterValues)
        //    where TResult : new()
        //    where TParam : KeyValueItem
        //{
        //    return database.CreateSprocAccessor<TResult>(storedProcedureName, parameterMapper, new ReflectionRowMapperEx<TResult>()).Execute(parameterValues);
        //}

        public static IEnumerable<TResult> ExecuteSprocAccessor<TResult, TParam>(this Database database,
                                                                                 string storedProcedureName,
                                                                                 IRowMapper<TResult> rowMapper,
                                                                                 params TParam[] parameterValues)
            where TResult : new()
            where TParam : KeyValueItem
        {
            return ExecuteSprocAccessor(database, storedProcedureName, new DefaultResultSetMapper<TResult>(rowMapper),
                                        parameterValues);
            //return ExecuteSprocAccessor<TResult, TParam>(database, storedProcedureName,
            //                                             new KeyValueItemParameterMapper(database), rowMapper);
        }

        //public static IEnumerable<TResult> ExecuteSprocAccessor<TResult, TParam>(this Database database,
        //                                                                         string storedProcedureName,
        //                                                                         IParameterMapper parameterMapper,
        //                                                                         IRowMapper<TResult> rowMapper,
        //                                                                         params TParam[] parameterValues)
        //    where TResult : new()
        //    where TParam : KeyValueItem
        //{
          //  return database.CreateSprocAccessor(storedProcedureName, parameterMapper, rowMapper).Execute(parameterValues);
        //}

        public static IEnumerable<TResult> ExecuteSprocAccessor<TResult, TParam>(this Database database,
                                                                                 string storedProcedureName,
                                                                                 IResultSetMapper<TResult>
                                                                                     resultSetMapper,
                                                                                 params TParam[] parameterValues)
            where TResult : new()
            where TParam : KeyValueItem
        {
            var reader = database.ExecuteReader<TParam>(storedProcedureName, parameterValues);
            return resultSetMapper.MapSet(reader); 

            //return ExecuteSprocAccessor<TResult, TParam>(database, storedProcedureName,
            //                                             new KeyValueItemParameterMapper(database), resultSetMapper);
        }

        //public static IEnumerable<TResult> ExecuteSprocAccessor<TResult, TParam>(this Database database,
        //                                                                         string storedProcedureName,
        //                                                                         IParameterMapper parameterMapper,
        //                                                                         IResultSetMapper<TResult>
        //                                                                             resultSetMapper,
        //                                                                         params TParam[] parameterValues)
        //    where TResult : new()
        //    where TParam : KeyValueItem
        //{
        //    return database.CreateSprocAccessor(storedProcedureName, parameterMapper, resultSetMapper).Execute(parameterValues);
        //}


        public static IEnumerable<TResult> ExecuteSqlStringAccessorEx<TResult>(this Database database, string sqlString)
           where TResult : new()
        {
            return CreateSqlStringAccessorEx<TResult>(database, sqlString).Execute();
        }

        public static DataAccessor<TResult> CreateSqlStringAccessorEx<TResult>(this Database database, string sqlString)
           where TResult : new()
        {
            return new SqlStringAccessor<TResult>(database, sqlString, new ReflectionRowMapperEx<TResult>());
        }

        public static DataAccessor<TResult> CreateSqlStringAccessorEx<TResult>(this Database database, string sqlString, IParameterMapper parameterMapper)
          where TResult : new()
        {
            return new SqlStringAccessor<TResult>(database, sqlString, parameterMapper, new ReflectionRowMapperEx<TResult>());
        }

        #region 内部方法

        private static DbCommand GetDbCommand(Database database, string storedProcedureName,
                                                                 params KeyValueItem[] parameterValues)
        {
            if (parameterValues == null) throw new ArgumentNullException("parameterValues");

            DbCommand cmd = database.GetStoredProcCommand(storedProcedureName);
            cache.SetParameters(cmd, database);
            for (int i = 0; i < cmd.Parameters.Count; i++)
            {
                bool flag = false;
                for (int j = 0; j < parameterValues.Length; j++)
                {
                    if (parameterValues[j].Key.ToUpper().Equals(cmd.Parameters[i].ParameterName.ToUpper()))
                    {
                        database.SetParameterValue(cmd, cmd.Parameters[i].ParameterName, parameterValues[j].Value);
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    database.SetParameterValue(cmd, cmd.Parameters[i].ParameterName, Convert.DBNull);
                }
            }
            return cmd;
        }

        private static object FillReturnItemValue(Database database, DbCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            return database.GetParameterValue(command, "RETURN_VALUE");
        }

        private static void FillOutputItemValue(Database database, DbCommand command, KeyValueItem[] parameterValues)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if (parameterValues == null)
            {
                throw new ArgumentNullException("parameterValues");
            }
            for (int i = 0; i < command.Parameters.Count; i++)
            {
                if (command.Parameters[i].Direction != ParameterDirection.Input)
                {
                    foreach (KeyValueItem item in parameterValues)
                    {
                        if (item == null) continue;
                        if (command.Parameters[i].ParameterName.ToUpper() == item.Key.ToUpper())
                        {
                            item.Value = database.GetParameterValue(command, command.Parameters[i].ParameterName);
                            break;
                        }
                    }
                }
            }
        }

        #endregion
    }
}
