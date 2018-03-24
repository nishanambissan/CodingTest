using System;
using ATMMachine.BusinessLogic;
using Xunit;

namespace ATMMachineTests
{
    public class WithdrawalByLeastNumberOfItemsTests
    {
        [Fact]
        public void ShouldWithdrawReturningLeastNumberOfNotesOrCoins()
        {
            AtmMoneyStore moneyStore = new AtmMoneyStore();
            WithdrawalByLeastNumberOfItems withdrawal = new WithdrawalByLeastNumberOfItems(moneyStore);

            Cash cash = withdrawal.Withdraw(120.00);

            Assert.Equal(2, cash.CoinOrNotes.Find(c => c.Type == DenominationType.FiftyPound)?.Count);
            Assert.Equal(1, cash.CoinOrNotes.Find(c => c.Type == DenominationType.TwentyPound)?.Count);
        }
    }
}
