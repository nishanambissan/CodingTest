using System;
using ATMMachine.BusinessLogic;

namespace ATMMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            AtmMoneyStore moneyStore = SetupMachineFirstTime();

            while (true)
            {
                Console.WriteLine("*******************************************************\n");
                Console.WriteLine("Please enter an amount to withdraw (e.g. 120.00 or 5.30");
                var input = Console.ReadLine();

                if (Double.TryParse(input, out double amountToWithdraw))
                {
                    ProcessInput(moneyStore, amountToWithdraw);

                    Console.WriteLine("Press any key to continue X to quit the program");
                    var keyEntered = Console.ReadKey();
                    if (keyEntered.Key == ConsoleKey.X)
                        break;
                }
                else
                    Console.WriteLine("Could not recognize that as a valid amount.");
            }
        }

        private static AtmMoneyStore SetupMachineFirstTime()
        {
            Console.WriteLine("Initialising money store for the first time...");
            AtmMoneyStore moneyStore = new AtmMoneyStore();
            Console.WriteLine($"Available balance is : {moneyStore.GetBalance()}");
            return moneyStore;
        }

        private static void ProcessInput(AtmMoneyStore moneyStore, double amountToWithdraw)
        {
            WithdrawalByLeastNumberOfItems withdrawal = new WithdrawalByLeastNumberOfItems(moneyStore);
            Cash cash = withdrawal.Withdraw(amountToWithdraw);
            Console.WriteLine($"Balance left after withdrawal is : {moneyStore.GetBalance()}");
        }
    }
}
