using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace AlphaTest.Extensions
{
    public static class EnumExtensions
    {
        public static System.Web.Mvc.SelectList ToSelectList<TEnum>(this TEnum obj)
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {

            return new SelectList(Enum.GetValues(typeof(TEnum)).OfType<Enum>()
                .Select(x =>
                    new SelectListItem
                    {
                        Text = x.DisplayName(),
                        Value = (Convert.ToInt32(x)).ToString()
                    }), "Value", "Text");
        }

        public static System.Web.Mvc.SelectList ToSelectList<TEnum>(this TEnum obj, object selectedValue)
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            return new SelectList(Enum.GetValues(typeof(TEnum)).OfType<Enum>()
                .Select(x =>
                    new SelectListItem
                    {
                        Text = x.DisplayName(),
                        Value = (Convert.ToInt32(x)).ToString()
                    }), "Value", "Text", selectedValue);
        }

        public static string DisplayName(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            DisplayAttribute attribute
                = Attribute.GetCustomAttribute(field, typeof(DisplayAttribute))
                    as DisplayAttribute;

            return attribute == null ? value.ToString() : attribute.Name;
        }
    }
}