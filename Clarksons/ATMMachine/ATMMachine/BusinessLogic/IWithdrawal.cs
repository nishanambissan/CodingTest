namespace ATMMachine.BusinessLogic
{
    public enum WithdrawalType { LeastNumberOfItems, PreferredDenominationRules}
    public interface IWithdrawal
    {
        Cash Withdraw(double amountToWithdraw);
    }
}
