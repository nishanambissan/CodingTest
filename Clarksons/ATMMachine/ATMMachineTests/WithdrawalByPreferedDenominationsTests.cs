using System;
using ATMMachine.BusinessLogic;
using Xunit;
using System.Collections.Generic;
using System.Linq;

namespace ATMMachineTests
{
    public class WithdrawalByPreferedDenominationsTests
    {
        [Theory]
        [InlineData(125.52, 4512.48, DenominationType.TwentyPound, 6, DenominationType.FivePound, 1, 
                    DenominationType.FiftyP, 1, DenominationType.TwoP, 1)]
        [InlineData(120.00, 4518, DenominationType.TwentyPound, 6)]
        public void ShouldDispenseMostTwentyPoundNotes(double amountToWithdraw, double balance, params object[] denominations)
        {
            var rules = new DenominationPreferenceRules(new List<DenominationType> { DenominationType.TwentyPound });
            AtmMoneyStore moneyStore = new AtmMoneyStore();
            IWithdrawal withdrawal = new WithdrawalByPreferedDenominationRules(moneyStore, rules);

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

        [Theory]
        [InlineData(120.00, 4518)]
        public void ShouldDispenseMostTenPoundNotes(double amountToWithdraw, double balance)
        {
            var rules = new DenominationPreferenceRules(new List<DenominationType> { DenominationType.TenPound });
            AtmMoneyStore moneyStore = new AtmMoneyStore();
            IWithdrawal withdrawal = new WithdrawalByPreferedDenominationRules(moneyStore, rules);

            Cash cash = withdrawal.Withdraw(amountToWithdraw);

            Assert.Equal(DenominationType.TenPound, cash.CoinOrNotes.SingleOrDefault().Type);
            Assert.Equal(12, cash.CoinOrNotes.FindAll(c => c.Type == DenominationType.TenPound).Count);
            Assert.Equal(balance, moneyStore.GetBalance());
        }
    }
}
