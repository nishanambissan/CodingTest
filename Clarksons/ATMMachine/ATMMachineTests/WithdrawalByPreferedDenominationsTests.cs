using System;
using ATMMachine.BusinessLogic;
using Xunit;
using System.Collections.Generic;

namespace ATMMachineTests
{
    public class WithdrawalByPreferedDenominationsTests
    {
        [Theory]
        [InlineData(125.52, 4512.48, DenominationType.TwentyPound, 6, DenominationType.FivePound, 1, 
                    DenominationType.FiftyP, 1, DenominationType.TwoP, 1)]
        [InlineData(120.00, 4518, DenominationType.TwentyPound, 6)]
        public void ShouldWithdrawMostTwentyPoundNotes(double amountToWithdraw, double balance, params object[] denominations)
        {
            var rules = new DenominationPreferenceRules(new List<DenominationType> { DenominationType.TwentyPound });
            AtmMoneyStore moneyStore = new AtmMoneyStore(rules);
            IWithdrawal withdrawal = new WithdrawalByPreferedDenominationRules(moneyStore);

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
    }
}
