namespace Hadrosaurus.Core.Models
{
    public class VendingMachine
    {
        /// <summary>
        /// Collection of items in VendingMachine. 
        /// KEY - item code; VALUE - information (name, price, etc.) about item;
        /// </summary>
        public IDictionary<int, VendingMachineItem> Items { get; set; } = new Dictionary<int, VendingMachineItem>();

        /// <summary>
        /// Collection of coins in VendingMachine. 
        /// </summary>
        public CoinCollection Coins { get; set; } = new CoinCollection();

        /// <summary>
        /// Collection of inserted coins in VendingMachine. These coins are inserted by buyer and used to purchase items.
        /// </summary>
        public CoinCollection InsertedCoins { get; set; } = new CoinCollection();
    }
}