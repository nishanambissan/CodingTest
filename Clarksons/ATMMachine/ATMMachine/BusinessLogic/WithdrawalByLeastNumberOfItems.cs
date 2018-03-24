using System;
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
            return new Cash();
        }
    }
}
