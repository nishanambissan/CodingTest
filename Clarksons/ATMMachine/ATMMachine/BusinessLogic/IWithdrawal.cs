namespace ATMMachine.BusinessLogic
{
    public interface IWithdrawal
    {
        Cash Withdraw(double amountToWithdraw);
    }
}
