using System.Collections.Generic;
using System.Text;
using ATMMachine.BusinessLogic.Shared;

namespace ATMMachine.BusinessLogic
{
    public class Cash
    {
        public Cash()
        {
            CoinOrNotes = new List<Denomination>();
        }

        public List<Denomination> CoinOrNotes { get; set; } 

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            foreach (var coinOrNote in CoinOrNotes)
            {
                stringBuilder.AppendFormat($"\n{ExtensionMethods.GetDescription(coinOrNote.Type)} X {coinOrNote.Count} ");
            }
            return stringBuilder.ToString();
        }
    }
}
