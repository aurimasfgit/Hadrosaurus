using Hadrosaurus.Core.Interfaces.Services;
using System.ComponentModel.DataAnnotations;

namespace Hadrosaurus.ConsoleApp.Common
{
    internal class VendingMachineMenu
    {
        private readonly IVendingMachineService vendingMachineService;

        public VendingMachineMenu(IVendingMachineService vendingMachineService)
        {
            ArgumentNullException.ThrowIfNull(vendingMachineService);

            this.vendingMachineService = vendingMachineService;
        }

        public void Show()
        {
            var items = vendingMachineService.GetItems();

            while (true)
            {
                Console.WriteLine("Select item to purchase:");

                Console.WriteLine();

                foreach (var item in items)
                    Console.WriteLine($"[{item.Key}] {item.Value.Name} - {item.Value.Price:n2} Eur.");

                Console.WriteLine();

                Console.WriteLine("[A] Insert coin");
                Console.WriteLine("[C] Cancel transaction");
                Console.WriteLine("[Q] Quit");

                var input = Console.ReadLine();

                if (input?.ToUpper() == "A")
                {
                    // for now insertion of coin is hard-coded. Every time user choose to insert coin, one coin of 20 ct is inserted
                    var denominationOfCoin = 20;

                    Console.WriteLine($"Inserting coin: 1 x {denominationOfCoin} ct.");

                    // TODO: add submenu where user can choose which denomination of coin he want to insert

                    // for now insertion of coin is hard-coded
                    vendingMachineService.InsertCoin(denominationOfCoin);
                }
                else if (input?.ToUpper() == "C")
                {
                    var returnedCoins = vendingMachineService.CancelTransaction();

                    Console.Write("Transaction canceled. Returning coins: ");

                    if (returnedCoins.Count == 0)
                        Console.WriteLine("0");

                    foreach (var coin in returnedCoins)
                        Console.WriteLine($"{coin.Value} x {coin.Key} ct. ");
                }
                else if (input?.ToUpper() == "Q")
                {
                    break;
                }
                else
                {
                    if (!int.TryParse(input, out int itemCode))
                        Console.WriteLine("Incorrect input. Try again...");
                    else if (!items.ContainsKey(itemCode))
                        Console.WriteLine("Item with specified code does not exist");
                    else
                    {
                        // TODO: global exception handler and logging
                        try
                        {
                            var changeCoinCollection = vendingMachineService.Purchase(itemCode);

                            Console.Write("Your change is: ");

                            if (changeCoinCollection.Count == 0)
                                Console.WriteLine("0");

                            foreach (var coin in changeCoinCollection)
                                Console.WriteLine($"{coin.Value} x {coin.Key} ct.");
                        }
                        catch (ValidationException exc)
                        {
                            Console.WriteLine(exc.Message);
                        }
                    }
                }

                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}