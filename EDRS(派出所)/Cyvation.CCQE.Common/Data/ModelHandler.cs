using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;

namespace Cyvation.CCQE.Common
{
    /// <summary>
    /// 实体类处理者
    /// </summary>
    public class ModelHandler
    {
        public static List<T> FillModel<T>(DataTable dt)
        {
            List<T> list = new List<T>();
            T model = default(T);           

            foreach (DataRow dr in dt.Rows)
            {
                model = FillModel<T>(dr);                
                list.Add(model);
            }
            return list;
        }

        public static T FillModel<T>(DataRow dr)
        {
            T model = Activator.CreateInstance<T>();
            foreach (DataColumn dcol in dr.Table.Columns)
            {
                PropertyInfo pi = model.GetType().GetProperty(dcol.ColumnName);
                if (pi == null) continue;
                if (dr[dcol.ColumnName] != DBNull.Value)
                {                 
                    pi.SetValue(model,pi.PropertyType == dcol.DataType? dr[dcol.ColumnName]:Convert.ChangeType(dr[dcol.ColumnName] ,pi.PropertyType), null);
                }
                else
                    pi.SetValue(model, null, null);
            }
            return model;
        }

        /// <summary> 
        /// 将实体类转换成DataTable 
        /// </summary> 
        public static DataTable FillDataTable<T>(IList<T> objlist)
        {
            if (objlist == null || objlist.Count <= 0)
            {
                return null;
            }
            DataTable dt = new DataTable(typeof(T).Name);
            DataColumn column;
            DataRow row;
            System.Reflection.PropertyInfo[] myPropertyInfo = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (T t in objlist)
            {
                if (t == null)
                {
                    continue;
                }
                row = dt.NewRow();
                for (int i = 0, j = myPropertyInfo.Length; i < j; i++)
                {
                    System.Reflection.PropertyInfo pi = myPropertyInfo[i];
                    if (pi == null) continue;
                    string name = pi.Name;
                    if (dt.Columns[name] == null)
                    {
                        column = new DataColumn(name, pi.PropertyType);
                        dt.Columns.Add(column);
                    }
                    row[name] = pi.GetValue(t, null);
                }
                dt.Rows.Add(row);
            }
            return dt;
        }
    }
}
