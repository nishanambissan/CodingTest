using ATMMachine.BusinessLogic;
using Xunit;

namespace ATMMachineTests
{
    public class WithdrawalByLeastNumberOfItemsTests
    {
        [Theory]
        [InlineData(120.00, 4518, DenominationType.FiftyPound, 2, DenominationType.TwentyPound, 1)]
        [InlineData(0.00, 4638)]
        [InlineData(60.00, 4578, DenominationType.FiftyPound, 1, DenominationType.TenPound, 1)]
        [InlineData(5.30, 4632.7, DenominationType.FivePound, 1, DenominationType.TwentyP, 1, DenominationType.TenP, 1)]
        [InlineData(6.83, 4631.17, DenominationType.FivePound, 1, DenominationType.OnePound, 1, DenominationType.FiftyP, 1, 
                    DenominationType.TwentyP, 1, DenominationType.TenP, 1, 
                    DenominationType.TwoP, 1, DenominationType.OneP, 1)]
        public void ShouldWithdrawReturningLeastNumberOfNotesOrCoins(double amountToWithdraw, double balance, params object[] denominations)
        {
            AtmMoneyStore moneyStore = new AtmMoneyStore();
            WithdrawalByLeastNumberOfItems withdrawal = new WithdrawalByLeastNumberOfItems(moneyStore);

            Cash cash = withdrawal.Withdraw(amountToWithdraw);
            Assert.Equal(denominations.Length / 2, cash.CoinOrNotes.Count);

            for (int i = 0; i < denominations.Length; i++)
            {
                if (i % 2 == 0)
                {
                    var type = (DenominationType)denominations[i];
                    var count = (int)denominations[i + 1];
                    Assert.Equal(count, cash.CoinOrNotes.Find(c => c.Type == type)?.Count);
                }
            }

            Assert.Equal(balance, moneyStore.GetBalance());
        }

        [Fact]
        public void ShouldNotAllowToWithdrawIfAtmOutOfMoney()
        {
            AtmMoneyStore moneyStore = new AtmMoneyStore();
            WithdrawalByLeastNumberOfItems withdrawal = new WithdrawalByLeastNumberOfItems(moneyStore);

            Assert.Throws<ATMMachine.BusinessLogic.CustomExceptions.OutOfMoneyException>(() => withdrawal.Withdraw(5000));
        }
    }
}
