namespace ATMMachine.BusinessLogic
{
    public interface IWithdrawal
    {
        Cash Withdraw(double amountToWithdraw);
        Cash Withdraw(double amountToWithdraw, DenominationPreferenceRules rules);
    }
}
