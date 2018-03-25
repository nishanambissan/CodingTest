using System;
using ATMMachine.BusinessLogic;
using ATMMachine.BusinessLogic.CustomExceptions;
using System.Collections.Generic;

namespace ATMMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            AtmMoneyStore moneyStore = SetupMachineFirstTime();

            IWithdrawal withdrawalScheme = SetupWithdrawalScheme(moneyStore, WithdrawalType.LeastNumberOfItems);

            while (true)
            {
                Console.WriteLine("*******************************************************\n");
                Console.WriteLine("Please enter an amount to withdraw (e.g. 120.00 or 5.30");
                var input = Console.ReadLine();

                if (Double.TryParse(input, out double amountToWithdraw))
                {
                    ProcessInput(moneyStore, amountToWithdraw, withdrawalScheme);

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

        private static void ProcessInput(AtmMoneyStore moneyStore, double amountToWithdraw, IWithdrawal withdrawalScheme)
        {
            try
            {
                Cash cash = withdrawalScheme.Withdraw(amountToWithdraw);
                DisplayCashDispensedToUser(cash);
                Console.WriteLine($"Balance left after withdrawal is : {moneyStore.GetBalance()}");
            }
            catch (OutOfMoneyException exc)
            {
                Console.WriteLine(exc.Message);
            }
        }

        private static IWithdrawal SetupWithdrawalScheme(AtmMoneyStore moneyStore, WithdrawalType type)
        {
            //TODO : this is poor man's injection. For now, since its a basic application we can live without have a container and DI logic
            //to inject the scheme. Open to extension though.

            switch (type)
            {
                case WithdrawalType.PreferredDenominationRules : 
                    DenominationPreferenceRules rules = new DenominationPreferenceRules(new List<DenominationType> { DenominationType.TwentyPound });
                    return new WithdrawalByPreferedDenominationRules(moneyStore, rules);

                case WithdrawalType.LeastNumberOfItems :
                    return new WithdrawalByLeastNumberOfItems(moneyStore);

                default: throw new Exception("This schema is not supported yet");
            }
        }

        private static void DisplayCashDispensedToUser(Cash cash)
        {
            Console.WriteLine(cash);
        }
    }
}
