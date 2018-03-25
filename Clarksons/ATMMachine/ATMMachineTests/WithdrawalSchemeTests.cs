using ATMMachine.BusinessLogic;
using Xunit;
using ATMMachine.BusinessLogic.CustomExceptions;

namespace ATMMachineTests
{
    public class WithdrawalSchemeTests
    {
        [Fact]
        public void ShouldNotAllowToWithdrawIfAtmOutOfMoney_SchemeLeastNumberOfItems()
        {
            AtmMoneyStore moneyStore = new AtmMoneyStore();
            WithdrawalScheme withdrawal = new WithdrawalByLeastNumberOfItems(moneyStore);

            Assert.Throws<OutOfMoneyException>(() => withdrawal.Withdraw(5000));
        }

        [Fact]
        public void ShouldNotAllowToWithdrawIfAtmOutOfMoney_SchemePreferedDenomination()
        {
            AtmMoneyStore moneyStore = new AtmMoneyStore();
            WithdrawalScheme withdrawal = new WithdrawalByPreferedDenomination(moneyStore, null);

            Assert.Throws<OutOfMoneyException>(() => withdrawal.Withdraw(5000));
        }
    }
}
