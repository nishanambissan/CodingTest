using System;
using ATMMachine.BusinessLogic;
using ATMMachine.BusinessLogic.CustomExceptions;

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
            try
            {
                Cash cash = withdrawal.Withdraw(amountToWithdraw);
                DisplayCashDispensedToUser(cash);
                Console.WriteLine($"Balance left after withdrawal is : {moneyStore.GetBalance()}");
            }
            catch(OutOfMoneyException exc)
            {
                Console.WriteLine(exc.Message);
            }
        }

        private static void DisplayCashDispensedToUser(Cash cash)
        {
            Console.WriteLine(cash.ToString());
        }
    }
}
