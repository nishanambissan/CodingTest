using System;
using System.Linq;
using ATMMachine.BusinessLogic.CustomExceptions;

namespace ATMMachine.BusinessLogic
{
    public class WithdrawalByPreferedDenominationRules : IWithdrawal
    {
        readonly AtmMoneyStore _moneyStore;

        public WithdrawalByPreferedDenominationRules(AtmMoneyStore moneyStore)
        {
            this._moneyStore = moneyStore;
        }

        public Cash Withdraw(double amountToWithdraw, DenominationPreferenceRules rules)
        {
            Cash cash = new Cash();
            var moneyStoreSortedByDenominationDescending = _moneyStore.AvailableCash.CoinOrNotes.OrderByDescending(c => c.Value);
            if (amountToWithdraw > _moneyStore.GetBalance())
                throw new OutOfMoneyException("Sorry, the amount you chose to withdraw exceeds the cash balance in this Atm machine!");
            foreach(var coinOrNote in moneyStoreSortedByDenominationDescending)
            {
                if (amountToWithdraw.Equals(0))
                    break;
                int NumberOfCoinsOrNotes = (int)(amountToWithdraw / coinOrNote.Value);
                if (NumberOfCoinsOrNotes > 0)
                {
                    Denomination item = new Denomination { Type = coinOrNote.Type, Count = NumberOfCoinsOrNotes };
                    cash.CoinOrNotes.Add(item);
                    amountToWithdraw = Math.Round(amountToWithdraw - item.Value * item.Count, 2);
                }
            }
            UpdateCashBalanceInAtmMachine(cash);
            return cash;
        }

        public Cash Withdraw(double amountToWithdraw)
        {
            throw new NotSupportedException();
        }

        private void UpdateCashBalanceInAtmMachine(Cash cash)
        {
            foreach(var coinOrNote in cash.CoinOrNotes)
            {
                _moneyStore.AvailableCash.CoinOrNotes.First<Denomination>(c => c.Type == coinOrNote.Type).Count -= coinOrNote.Count;
            }
        }
    }
}
