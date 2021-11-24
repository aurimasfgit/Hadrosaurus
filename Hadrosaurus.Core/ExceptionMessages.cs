namespace Hadrosaurus.Core
{
    public class ExceptionMessages
    {
        public const string InsufficientAmount = "The amount of money in vending machine is insufficient";
        public const string NotEnoughCoinsOfCertainDenomination = "Coins of a certain denomination are not enough";
        public const string ItemNotAvailable = "The selected item is no longer available";
        public const string NotEnoughMoneyInserted = "Not enough money inserted";

        public const string ShouldBeGreaterThanZero = "Value should be greater than zero";
        public const string IncorrectDenominationValue = "Incorrect denomination value";
        public const string NumberOfCoinsGreaterOrEqualToOne = "Number of coins should be greater or equal to one";
        public const string PriceGreaterThanZero = "Price should be greater than zero";
        public const string NumberOfItemsGreaterOrEqualToZero = "Number of items should be greater or equal to zero";
        public const string NumberOfItemsMoreThanZero = "Number of items should be more than zero";
        public const string CannotPerformRemoveOperation = "Number of coins should be greater or equal to zero to perform remove operation";
        public const string SpecifiedDenominationCoinsNotFound = "Coins with specified denomination not found in coin collection";
        public const string CannotFindItemByCode = "Cannot find vending machine item by code";
    }
}