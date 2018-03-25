using System;
using System.Linq;
using ATMMachine.BusinessLogic.CustomExceptions;
using ATMMachine.BusinessLogic.Shared;

namespace ATMMachine.BusinessLogic
{
    public class WithdrawalByPreferedDenominationRules : IWithdrawal
    {
        readonly AtmMoneyStore _moneyStore;

        public WithdrawalByPreferedDenominationRules(AtmMoneyStore moneyStore)
        {
            this._moneyStore = moneyStore;
        }

        public Cash Withdraw(double amountToWithdraw)
        {
            Cash cash = new Cash();
            var moneyStoreSortedByDenominationDescending = _moneyStore.AvailableCash.CoinOrNotes.OrderByDescending(c => c.Value);

            if (amountToWithdraw > _moneyStore.GetBalance())
                throw new OutOfMoneyException("Sorry, the amount you chose to withdraw exceeds the cash balance in this Atm machine!");
            
            foreach(var coinOrNote in moneyStoreSortedByDenominationDescending)
            {
                if (amountToWithdraw.Equals(0))
                    break;
                
                foreach (var denomination in _moneyStore.Rules.PreferedDenominationTypes)
                {
                    int NumberOfCoinsOrNotesInPreferedDenomination = (int)(amountToWithdraw / ExtensionMethods.GetDenominationValuePerUnit(denomination));
                    if (NumberOfCoinsOrNotesInPreferedDenomination > 0)
                    {
                        Denomination item = new Denomination { Type = denomination, Count = NumberOfCoinsOrNotesInPreferedDenomination };
                        cash.CoinOrNotes.Add(item);
                        amountToWithdraw = Math.Round(amountToWithdraw - item.Value * item.Count, 2);
                    }
                }
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

        private void UpdateCashBalanceInAtmMachine(Cash cash)
        {
            foreach(var coinOrNote in cash.CoinOrNotes)
            {
                _moneyStore.AvailableCash.CoinOrNotes.First<Denomination>(c => c.Type == coinOrNote.Type).Count -= coinOrNote.Count;
            }
        }
    }
}
