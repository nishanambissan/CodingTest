using ATMMachine.BusinessLogic.Shared;

namespace ATMMachine.BusinessLogic
{
    public class Denomination
    {
        public DenominationType Type { get; set; }

        public double Value => ExtensionMethods.GetDenominationValuePerUnit(Type);

        public int Count { get; set; }
    }
}
