using System;
using System.ComponentModel;

namespace ATMMachine.BusinessLogic.Shared
{
    public static class ExtensionMethods
    {
        public static string GetDescription(Enum value)
        {
            System.Reflection.FieldInfo fi = value.GetType().GetField(value.ToString());

            var attributes = fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes?.Length > 0)
                return ((DescriptionAttribute)attributes[0]).Description;
            else
                return value.ToString();
        }
    }
}
