using System;
using ATMMachine.BusinessLogic;
using Xunit;

namespace ATMMachineTests
{
    public class AtmMoneyStoreTests
    {
        [Fact]
        public void ShouldShowAvailableBalanceOnStarupCorrectly()
        {
            AtmMoneyStore moneyStore = new AtmMoneyStore();

            Assert.Equal(4638, moneyStore.GetBalance());
        }
    }
}
