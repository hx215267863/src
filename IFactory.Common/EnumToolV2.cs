using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace IFactory.Common
{
    public static class EnumToolV2
    {
        public static string GetDescription(this Enum enumName)
        {
            string str = string.Empty;
            DescriptionAttribute[] descriptAttr = enumName.GetType().GetField(enumName.ToString()).GetDescriptAttr();
            return descriptAttr == null || descriptAttr.Length == 0 ? enumName.ToString() : descriptAttr[0].Description;
        }

        public static DescriptionAttribute[] GetDescriptAttr(this FieldInfo fieldInfo)
        {
            if (fieldInfo != null)
                return (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return null;
        }

        public static T GetEnumName<T>(string description)
        {
            foreach (FieldInfo field in typeof(T).GetFields())
            {
                DescriptionAttribute[] descriptAttr = field.GetDescriptAttr();
                if (descriptAttr != null && descriptAttr.Length != 0)
                {
                    if (descriptAttr[0].Description == description)
                        return (T)field.GetValue(null);
                }
                else if (field.Name == description)
                    return (T)field.GetValue(null);
            }
            throw new ArgumentException(string.Format("{0} 未能找到对应的枚举.", description), "Description");
        }

        public static ArrayList ToArrayList(this Enum en)
        {
            ArrayList arrayList = new ArrayList();
            foreach (Enum @enum in Enum.GetValues(en.GetType()))
                arrayList.Add(new KeyValuePair<Enum, string>(@enum, @enum.GetDescription()));
            return arrayList;
        }
    }
}
