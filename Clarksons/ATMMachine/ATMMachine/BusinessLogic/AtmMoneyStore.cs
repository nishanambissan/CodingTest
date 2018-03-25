using System;

namespace ATMMachine.BusinessLogic
{
    public class AtmMoneyStore
    {
        public AtmMoneyStore() { Setup(); }

        public void Setup()
        {
            AvailableCash = new Cash();
            AvailableCash.CoinOrNotes.Add(new Denomination { Type = DenominationType.OneP, Count = 100 });
            AvailableCash.CoinOrNotes.Add(new Denomination { Type = DenominationType.TwoP, Count = 100 });
            AvailableCash.CoinOrNotes.Add(new Denomination { Type = DenominationType.FiveP, Count = 100 });
            AvailableCash.CoinOrNotes.Add(new Denomination { Type = DenominationType.TenP, Count = 100 });
            AvailableCash.CoinOrNotes.Add(new Denomination { Type = DenominationType.TwentyP, Count = 100 });
            AvailableCash.CoinOrNotes.Add(new Denomination { Type = DenominationType.FiftyP, Count = 100 });
            AvailableCash.CoinOrNotes.Add(new Denomination { Type = DenominationType.OnePound, Count = 100 });
            AvailableCash.CoinOrNotes.Add(new Denomination { Type = DenominationType.TwoPound, Count = 100 });
            AvailableCash.CoinOrNotes.Add(new Denomination { Type = DenominationType.FivePound, Count = 50 });
            AvailableCash.CoinOrNotes.Add(new Denomination { Type = DenominationType.TenPound, Count = 50 });
            AvailableCash.CoinOrNotes.Add(new Denomination { Type = DenominationType.TwentyPound, Count = 50 });
            AvailableCash.CoinOrNotes.Add(new Denomination { Type = DenominationType.FiftyPound, Count = 50 });
        }
        public Cash AvailableCash { get; set; }

        public double GetBalance() 
        {
            double balance = 0;
            AvailableCash.CoinOrNotes.ForEach(c => balance += c.Value * c.Count);
            return Math.Round(balance,2);
        }
    }
}
