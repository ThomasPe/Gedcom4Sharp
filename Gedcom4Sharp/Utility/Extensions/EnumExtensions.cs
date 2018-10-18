using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Gedcom4Sharp.Utility.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets the Description Attribute for an enum value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Desc<T>(this T value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                    typeof(DescriptionAttribute),
                    false
            );

            if (attributes != null &&
                attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }

        public static T ParseDescriptionToEnum<T>(string description)
        {
            Array array = Enum.GetValues(typeof(T));
            var list = new List<T>(array.Length);
            for (int i = 0; i < array.Length; i++)
            {
                list.Add((T)array.GetValue(i));
            }

            var dict = list.Select(v => new {
                Value = v,
                Description = Desc(v)
            }).ToDictionary(x => x.Description, x => x.Value);
            return dict[description];
        }

        public static bool TryParseDescriptionToEnum<T>(string description, out T obj)
        {
            var success = true;
            try
            {
                obj = ParseDescriptionToEnum<T>(description);
            }
            catch (Exception ex)
            {
                success = false;
                obj = default(T);
            }
            return success;
        }
    }
}
