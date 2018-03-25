using System;
using ATMMachine.BusinessLogic;
using ATMMachine.BusinessLogic.CustomExceptions;
using System.Collections.Generic;

namespace ATMMachine
{
    class Program
    {
        static AtmMoneyStore moneyStore;
        static IWithdrawal withdrawalScheme;

        static void Main(string[] args)
        {
            SetupMachineFirstTime();
            SetupWithdrawalScheme(WithdrawalType.PreferredDenominationRules);

            while (true)
            {
                Console.WriteLine("*******************************************************\n");
                Console.WriteLine("Please enter an amount to withdraw (e.g. 120.00 or 5.30");
                var input = Console.ReadLine();

                if (Double.TryParse(input, out double amountToWithdraw))
                {
                    ProcessInput(amountToWithdraw);

                    Console.WriteLine("Press any key to continue X to quit the program");
                    var keyEntered = Console.ReadKey();
                    if (keyEntered.Key == ConsoleKey.X)
                        break;
                }
                else
                    Console.WriteLine("Could not recognize that as a valid amount.");
            }
        }

        private static void SetupMachineFirstTime()
        {
            Console.WriteLine("Initialising money store for the first time...");
            moneyStore = new AtmMoneyStore();
            Console.WriteLine($"Available balance is : {moneyStore.GetBalance()}");
        }

        private static void ProcessInput(double amountToWithdraw)
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

        private static void SetupWithdrawalScheme(WithdrawalType type)
        {
            //TODO : this is poor man's injection. For now, since its a basic application we can live without have a container and DI logic
            //to inject the scheme. Open to extension though.

            switch (type)
            {
                case WithdrawalType.PreferredDenominationRules : 
                    DenominationPreferenceRules rules = new DenominationPreferenceRules(new List<DenominationType> { DenominationType.TenPound });
                    withdrawalScheme = new WithdrawalByPreferedDenomination(moneyStore, rules);
                    break;

                case WithdrawalType.LeastNumberOfItems :
                    withdrawalScheme = new WithdrawalByLeastNumberOfItems(moneyStore);
                    break;

                default: throw new Exception("This schema is not supported yet");
            }
        }

        private static void DisplayCashDispensedToUser(Cash cash)
        {
            Console.WriteLine(cash);
        }
    }
}
