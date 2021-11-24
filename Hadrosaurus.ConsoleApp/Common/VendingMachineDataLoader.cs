using Hadrosaurus.Core.Interfaces.Services;
using Hadrosaurus.Core.Models;

namespace Hadrosaurus.ConsoleApp.Common
{
    internal class VendingMachineDataLoader
    {
        private readonly IVendingMachineService vendingMachineService;

        public VendingMachineDataLoader(IVendingMachineService vendingMachineService)
        {
            ArgumentNullException.ThrowIfNull(vendingMachineService);

            this.vendingMachineService = vendingMachineService;
        }

        /// <summary>
        /// Loads items and coins into VendingMachine.
        /// 
        /// Simulates items and coins loading at the start of the day (usually).
        /// Values are hard-coded for now.
        /// </summary>
        public void LoadData()
        {
            var items = new Dictionary<int, VendingMachineItem>
            {
                { 1, new VendingMachineItem("Lemonade", 0.5M, 5) },
                { 5,  new VendingMachineItem("\"Snickers\" bar", 0.75M, 3) },
                { 10, new VendingMachineItem("Mineral water 0.5L", 0.8M, 1) },
                { 11, new VendingMachineItem("\"Bounty\" bar", 0.45M, 2) },
                { 88,  new VendingMachineItem("Hazelnuts", 0.25M, 2) }
            };

            vendingMachineService.LoadItems(items);

            var coins = new CoinCollection(new Dictionary<int, int>
            {
                { 1, 100 }, // 100 x 1ct
                { 5, 50 }, // 50 x 5ct
                { 10, 50 } // 50 x 10ct
            });

            vendingMachineService.LoadCoins(coins);
        }
    }
}