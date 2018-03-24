using System;
using ATMMachine.BusinessLogic;

namespace ATMMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            AtmMoneyStore moneyStore = new AtmMoneyStore();
            Console.WriteLine($"Available balance is : {moneyStore.GetBalance()}");
            while (true)
            {
                Console.WriteLine("*******************************************************\n");
                Console.WriteLine("Please enter an amount to withdraw (e.g. 120.00 or 5.30");
                var amount = Console.ReadLine();

                if (Double.TryParse(amount, out double result))
                {
                    ProcessInput(result);

                    Console.WriteLine("Press any key to continue X to quit the program");
                    var input = Console.ReadKey();
                    if (input.Key == ConsoleKey.X)
                        break;
                }
                else
                    Console.WriteLine("Could not recognize that as a valid amount.");
                
            }
        }

        private static void ProcessInput(double amountToWithdraw)
        {
            Console.WriteLine("\nTest output\n");
        }
    }
}
