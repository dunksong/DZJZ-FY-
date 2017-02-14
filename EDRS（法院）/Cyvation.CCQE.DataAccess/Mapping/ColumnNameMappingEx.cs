using System;
using System.Data;
using System.Globalization;
using System.Reflection;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Cyvation.CCQE.DataAccess.Mapping
{
    internal class ColumnNameMappingEx : PropertyMapping
    {

        public ColumnNameMappingEx(PropertyInfo property, string columnName)
            : base(property)
        {
            ColumnName = columnName;
        }

        /// <summary>
        /// Gets the name of the column that is used for mapping.
        /// </summary>
        public string ColumnName { get; private set; }

        /// <summary>
        /// Converts the value for the column in the <paramref name="row"/> with a name matching that of the 
        /// mapped property to the type of the property.
        /// </summary>
        /// <param name="row">The data record.</param>
        /// <returns>The value for the corresponding column converted to the type of the mapped property.</returns>
        public override object GetPropertyValue(IDataRecord row)
        {
            if (row == null) throw new ArgumentNullException("row");

            object value = null;
            try
            {              
                 value = row[ColumnName];                
            }
            catch (IndexOutOfRangeException)
            {
                //System.Diagnostics.Debug.WriteLine( string.Format("IDataRecord中不存在名为“{0}”的列，已设为该字段类型对应的默认值。", ColumnName));
                return ConvertNonNullableValue(value, Property.PropertyType);
            }
            
            object convertedValue = null;
            try
            {
                convertedValue = ConvertValue(value, Property.PropertyType);
            }
            catch (InvalidCastException castException)
            {
                string exceptionMessage =
                   string.Format(
                       CultureInfo.CurrentCulture,
                       Resources.ExceptionConvertionFailedWhenMappingPropertyToColumn,
                       ColumnName,
                       Property.Name,
                       Property.PropertyType);
                throw new InvalidCastException(exceptionMessage, castException);
            }
            catch (FormatException formatException)
            {
                string exceptionMessage =
                                string.Format(
                                    CultureInfo.CurrentCulture,
                                    Resources.ExceptionConvertionFailedWhenMappingPropertyToColumn,
                                    ColumnName,
                                    Property.Name,
                                    Property.PropertyType);
                throw new InvalidCastException(exceptionMessage, formatException);
            }
            return convertedValue;
        }     

        /// <summary>
        /// Converts the database value <paramref name="value"/> to <paramref name="conversionType"/>.
        /// Will throw an exception if <paramref name="conversionType"/> is a nullable value.
        /// </summary>
        /// <param name="value">Value from the database.</param>
        /// <param name="conversionType">Type to convert to.</param>
        /// <returns>The converted value.</returns>
        protected new static object ConvertNonNullableValue(object value, Type conversionType)
        {
            object convertedValue = null;

            if (value != DBNull.Value && value != null)
            {
                convertedValue = Convert.ChangeType(value, conversionType);
            }             
            else if (conversionType.IsValueType)
            {
                convertedValue = Activator.CreateInstance(conversionType);
            }

            return convertedValue;
        }
    }
}