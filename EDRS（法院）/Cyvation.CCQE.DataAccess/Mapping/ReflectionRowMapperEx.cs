using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.Unity.Utility;

namespace Cyvation.CCQE.DataAccess.Mapping
{
    internal class ReflectionRowMapperEx<TResult> : IRowMapper<TResult>
        where TResult : new()
    {

         private static readonly MethodInfo ConvertValue =
            StaticReflection.GetMethodInfo<PropertyMapping>(pm => pm.GetPropertyValue(null));
        private static readonly NewExpression CreationExpression = Expression.New(typeof(TResult));

        private readonly Func<IDataRecord, TResult> mapping;


        public ReflectionRowMapperEx()
        {
            try
            {
                var parameter = Expression.Parameter(typeof(IDataRecord), "reader");
                var properties = GetProperties();
                var ageMemberBinding = properties.Select(p=>
                      (MemberBinding)Expression.Bind(p, Expression.Convert(Expression.Call(Expression.Constant(new ColumnNameMappingEx(p, p.Name)), ConvertValue, parameter), p.PropertyType)));
                var expr = Expression.Lambda<Func<IDataRecord, TResult>>(Expression.MemberInit(CreationExpression,ageMemberBinding), parameter);
                this.mapping = expr.Compile();

            }
            catch (Exception e)
            {
                throw new InvalidOperationException(
                     string.Format(
                         CultureInfo.CurrentCulture,
                         "映射出错",
                         typeof(TResult).Name),e);
            }
        }

        


        /// <summary>Given a record from a data reader, map the contents to a common language runtime object.</summary>
        /// <param name="row">The input data from the database.</param>
        /// <returns>The mapped object.</returns>
        public TResult MapRow(IDataRecord row)
        {
            return this.mapping(row);
        }

        public static IEnumerable<PropertyInfo> GetProperties()
        {
            var properties =
                from property in typeof(TResult).GetProperties(BindingFlags.Instance | BindingFlags.Public)
                where IsAutoMappableProperty(property)
                select property;
            return properties;
        }

        private static bool IsAutoMappableProperty(PropertyInfo property)
        {
            return property.CanWrite
                   && property.GetIndexParameters().Length == 0
                   && !IsCollectionType(property.PropertyType);
        }

        private static bool IsCollectionType(Type type)
        {
            // string implements IEnumerable, but for our purposes we don't consider it a collection.
            if (type == typeof(string)) return false;

            var interfaces = from inf in type.GetInterfaces()
                             where inf == typeof(IEnumerable) ||
                                   (inf.IsGenericType && inf.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                             select inf;
            return interfaces.Count() != 0;
        }
    }
}