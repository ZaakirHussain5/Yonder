using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Yonder.Models
{
    public class DataConvertor
    {
        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        public static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                   if (pro.Name.ToLower() == column.ColumnName)
                    {
                        try
                        {
                            if (dr[column.ColumnName].GetType() == typeof(System.DBNull))
                                pro.SetValue(obj, dr[column.ColumnName].ToString(), null);
                            else
                                pro.SetValue(obj, dr[column.ColumnName], null);
                        }
                        catch
                        {
                            pro.SetValue(obj, null, null);
                        }
                    }
                    else
                        continue;
                }
            }
            return obj;
        } 
    }
}