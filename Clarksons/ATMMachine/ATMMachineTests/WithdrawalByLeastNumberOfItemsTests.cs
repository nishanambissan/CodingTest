using System;
using ATMMachine.BusinessLogic;
using Xunit;
using System.Collections.Generic;

namespace ATMMachineTests
{
    public class WithdrawalByLeastNumberOfItemsTests
    {
        [Theory]
        [InlineData(120.00, DenominationType.FiftyPound, 2, DenominationType.TwentyPound, 1)]
        [InlineData(60.00, DenominationType.FiftyPound, 1, DenominationType.TenPound, 1)]
        public void ShouldWithdrawReturningLeastNumberOfNotesOrCoins(double amountToWithdraw, params object[] denominations)
        {
            AtmMoneyStore moneyStore = new AtmMoneyStore();
            WithdrawalByLeastNumberOfItems withdrawal = new WithdrawalByLeastNumberOfItems(moneyStore);

            Cash cash = withdrawal.Withdraw(amountToWithdraw);

            for (int i = 0; i < denominations.Length; i++)
            {
                var type = (DenominationType)denominations[i];
                Console.WriteLine(type);
                var count = (int)denominations[i+1];
                Console.WriteLine(count);
                Assert.Equal(count, cash.CoinOrNotes.Find(c => c.Type == type)?.Count);
                i++;
            }
        }
    }
}
