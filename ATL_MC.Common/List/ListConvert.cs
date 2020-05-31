using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PVCommon.List
{
   public class ListConvert
    {

        /// <summary>
        /// datatable转list
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> DtToList<T>(DataTable dt) where T : class, new()
        {
            List<T> ts = new List<T>();// 定义集合
            Type type = typeof(T); // 获得此模型的类型
            string tempName = "";
            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                PropertyInfo[] propertys = t.GetType().GetProperties();// 获得此模型的公共属性
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;
                    if (dt.Columns.Contains(tempName))
                    {
                        if (!pi.CanWrite) continue;
                        object value = dr[tempName];
                        if (value != DBNull.Value)
                            pi.SetValue(t, value, null);
                    }
                }
                ts.Add(t);
            }
            return ts;
        }


        /// <summary>
        /// List转Dt,已考虑Null值和可空类型的问题
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static DataTable ListToDt<T>(IEnumerable<T> collection) where T : class, new()
        {
            //创建属性的集合
            List<PropertyInfo> pList = new List<PropertyInfo>();
            Type type = typeof(T);
            DataTable dt = new DataTable();
            Array.ForEach<PropertyInfo>(type.GetProperties(), p =>
            {
                pList.Add(p);
                if (p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    dt.Columns.Add(p.Name, p.PropertyType.GetGenericArguments()[0]);
                }
                else
                {
                    dt.Columns.Add(p.Name, p.PropertyType);
                }
            });
            foreach (var item in collection)
            {
                //创建一个DataRow实例
                DataRow row = dt.NewRow();
                pList.ForEach(p =>
                {

                    var value = p.GetValue(item, null);
                    if (value == null)
                    {
                        value = DBNull.Value;
                    }
                    row[p.Name] = value;
                });
                dt.Rows.Add(row);
            }
            return dt;
        }



    }
}
