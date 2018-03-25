using System;
using ATMMachine.BusinessLogic.CustomExceptions;
using ATMMachine.BusinessLogic.Shared;

namespace ATMMachine.BusinessLogic
{
    public abstract class WithdrawalScheme : IWithdrawal
    {
        protected AtmMoneyStore _moneyStore;

        public abstract Cash Withdraw(double amountToWithdraw);

        protected static double WithdrawInChosenDenomination(double amountLeftToWithdraw, Cash cash, DenominationType denomination)
        {
            if (amountLeftToWithdraw > 0)
            {
                int NumberOfItemsInChosenDenomination = (int)(amountLeftToWithdraw / ExtensionMethods.GetDenominationValuePerUnit(denomination));
                if (NumberOfItemsInChosenDenomination > 0)
                {
                    Denomination item = new Denomination { Type = denomination, Count = NumberOfItemsInChosenDenomination };
                    cash.CoinOrNotes.Add(item);
                    amountLeftToWithdraw = Math.Round(amountLeftToWithdraw - item.Value * item.Count, 2);
                }
            }

            return amountLeftToWithdraw;
        }

        protected void CheckIfAtmHasEnoughMoney(double amountToWithdraw)
        {
            if (amountToWithdraw > _moneyStore.GetBalance())
                throw new OutOfMoneyException("Sorry, the amount you chose to withdraw exceeds the cash balance in this Atm machine!");
        }
    }
}
