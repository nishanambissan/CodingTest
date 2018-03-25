using System;
using System.Linq;
using ATMMachine.BusinessLogic.CustomExceptions;
using ATMMachine.BusinessLogic.Shared;

namespace ATMMachine.BusinessLogic
{
    public class WithdrawalByPreferedDenomination : WithdrawalScheme
    {
        readonly DenominationPreferenceRules rules;

        public WithdrawalByPreferedDenomination(AtmMoneyStore moneyStore, DenominationPreferenceRules rules)
        {
            this.rules = rules;
            _moneyStore = moneyStore;
        }

        public override Cash Withdraw(double amountToWithdraw)
        {
            CheckIfAtmHasEnoughMoney(amountToWithdraw);

            Cash cash = new Cash();
            var allAvailableDenominations = _moneyStore.AvailableCash.CoinOrNotes.OrderByDescending(c => c.Value);

            foreach (var preferedDenomination in rules.PreferedDenominationTypes)
            {
                amountToWithdraw = WithdrawInChosenDenomination(amountToWithdraw, cash, preferedDenomination);
            }

            foreach (var denomination in allAvailableDenominations)
            {
                amountToWithdraw = WithdrawInChosenDenomination(amountToWithdraw, cash, denomination.Type);
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
