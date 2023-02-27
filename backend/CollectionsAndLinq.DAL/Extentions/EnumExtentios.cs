﻿using System.ComponentModel;
using System.Reflection;

namespace CollectionsAndLinq.DAL.Extentions
{
    public static class EnumExtentios
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo == null) return null;
            var attribute = (DescriptionAttribute)fieldInfo.GetCustomAttribute(typeof(DescriptionAttribute));
            return attribute.Description;
        }
    }
}
