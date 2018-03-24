using System;
namespace ATMMachine.BusinessLogic
{
    public class Denomination
    {
        public DenominationType Type { get; set; }

        public double Value { 
            get {
                switch(Type)
                {
                    case DenominationType.OneP : return 0.01;
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
                    default:return 0;
                }
            }
        }

        public int Count { get; set; }
    }
}
