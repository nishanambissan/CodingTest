using System;
using System.Linq;

namespace ATMMachine.BusinessLogic
{
    public class WithdrawalByLeastNumberOfItems : IWithdrawal
    {
        readonly AtmMoneyStore _moneyStore;

        public WithdrawalByLeastNumberOfItems(AtmMoneyStore moneyStore)
        {
            this._moneyStore = moneyStore;
        }

        public Cash Withdraw(double amountToWithdraw)
        {
            Cash cash = new Cash();
            var moneyStoreSortedByDenominationDescending = _moneyStore.AvailableCash.CoinOrNotes.OrderByDescending(c => c.Value);
            foreach(var coinOrNote in moneyStoreSortedByDenominationDescending)
            {
                int NumberOfCoinsOrNotes = (int)amountToWithdraw / (int)coinOrNote.Value;
                Console.WriteLine($"{NumberOfCoinsOrNotes} of value {coinOrNote.Type.ToString()}");
                Denomination item = new Denomination { Type = coinOrNote.Type, Count = NumberOfCoinsOrNotes };
                cash.CoinOrNotes.Add(item);
                amountToWithdraw = amountToWithdraw - item.Value * item.Count;
                if (amountToWithdraw.Equals(0))
                    break;
            }
            return cash;
        }
    }
}
