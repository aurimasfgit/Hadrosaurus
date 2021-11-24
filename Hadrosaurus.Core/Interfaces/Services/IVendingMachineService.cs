using Hadrosaurus.Core.Models;

namespace Hadrosaurus.Core.Interfaces.Services
{
    public interface IVendingMachineService
    {
        /// <summary>
        /// Loads items into vending machine. Dictionary KEY - code of item; VALUE - vending machine item object;
        /// </summary>
        /// <param name="items">Items to load into vending machine</param>
        void LoadItems(IDictionary<int, VendingMachineItem> items);

        /// <summary>
        /// Loads coins into vending machine
        /// </summary>
        /// <param name="coins">Coins to load into vending machine</param>
        void LoadCoins(CoinCollection coins);

        /// <summary>
        /// Returns all available for sale items of vending machine
        /// </summary>
        /// <returns>Vending machine items dictionary</returns>
        IDictionary<int, VendingMachineItem> GetItems();

        /// <summary>
        /// Inserts (adds to InsertedCoins collection of VendingMachine) coin which will be used to purchase item from vending machine
        /// </summary>
        /// <param name="denomination">Denomination of coin to insert</param>
        void InsertCoin(int denomination);

        /// <summary>
        /// Checks if vending machine has enough inserted money, purchases item and calculates the change
        /// </summary>
        /// <param name="code">Code of item to purchase</param>
        /// <returns>The change to return</returns>
        CoinCollection Purchase(int code);

        /// <summary>
        /// Cancels transaction and returns all inserted money (cleans money collection of VendingMachine object)
        /// </summary>
        /// <returns>Inserted coins</returns>
        CoinCollection CancelTransaction();
    }
}