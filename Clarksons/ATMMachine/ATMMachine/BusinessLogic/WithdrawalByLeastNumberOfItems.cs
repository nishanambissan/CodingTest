using System.Linq;

namespace ATMMachine.BusinessLogic
{
    public class WithdrawalByLeastNumberOfItems : WithdrawalScheme
    {
        public WithdrawalByLeastNumberOfItems(AtmMoneyStore moneyStore)
        {
            _moneyStore = moneyStore;
        }

        public override Cash Withdraw(double amountToWithdraw)
        {
            CheckIfAtmHasEnoughMoney(amountToWithdraw);

            Cash cash = new Cash();
            var allAvailableDenominations = _moneyStore.AvailableCash.CoinOrNotes.OrderByDescending(c => c.Value);

            foreach(var denomination in allAvailableDenominations)
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
