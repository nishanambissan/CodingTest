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

        public static double GetDenominationValuePerUnit(DenominationType value)
        {
            switch (value)
            {
                case DenominationType.OneP: return 0.01;
                case DenominationType.TwoP: return 0.02;
                case DenominationType.FiveP: return 0.05;
                case DenominationType.TenP: return 0.1;
                case DenominationType.TwentyP: return 0.2;
                case DenominationType.FiftyP: return 0.5;
                case DenominationType.OnePound: return 1;
                case DenominationType.TwoPound: return 2;
                case DenominationType.FivePound: return 5;
                case DenominationType.TenPound: return 10;
                case DenominationType.TwentyPound: return 20;
                case DenominationType.FiftyPound: return 50;
                default: return 0;
            }
        }
    }
}
