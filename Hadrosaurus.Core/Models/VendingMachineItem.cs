namespace Hadrosaurus.Core.Models
{
    public class VendingMachineItem
    {
        public string Name { get; }
        public decimal Price { get; }
        public int NumberOfItems { get; private set; }

        /// <summary>
        /// Creates VendingMachine item object
        /// </summary>
        /// <param name="name">Name of vending machine item</param>
        /// <param name="price">Price of vending machine item. Should be greater then zero.</param>
        /// <param name="numberOfItems">Number of specific item in vending machine. Should be greater or equal to zero.</param>
        public VendingMachineItem(string name, decimal price, int numberOfItems)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            if (price <= 0)
                throw new ArgumentException(ExceptionMessages.PriceGreaterThanZero);

            if (numberOfItems < 0)
                throw new ArgumentException(ExceptionMessages.NumberOfItemsGreaterOrEqualToZero);

            Name = name;
            Price = price;
            NumberOfItems = numberOfItems;
        }

        /// <summary>
        /// Reduces amount of NumberOfItems by one
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public void RemoveOneItem()
        {
            if (NumberOfItems <= 0)
                throw new ArgumentException(ExceptionMessages.NumberOfItemsMoreThanZero);

            NumberOfItems--;
        }
    }
}