using System.Collections;

namespace Hadrosaurus.Core.Models
{
    /// <summary>
    /// Represents a collection of coins
    /// </summary>
    public class CoinCollection : IEnumerable<KeyValuePair<int, int>>
    {
        // 100 - e.g. 1 Eur, 1 USD, etc.
        // 200 - e.g. 2 Eur, 2 USD, etc.
        private readonly int[] availableDenominations = new[] { 1, 2, 5, 10, 20, 50, 100, 200 };

        private readonly IDictionary<int, int> coins = new Dictionary<int, int>();

        public CoinCollection()
        {
        }

        /// <summary>
        /// Creates collection of coins. Dictionary KEY - denomination of coin; VALUE - number of coins;
        /// </summary>
        /// <param name="coins"></param>
        public CoinCollection(IDictionary<int, int> coins)
        {
            ArgumentNullException.ThrowIfNull(coins);

            this.coins = coins;
        }

        #region IEnumerable<T> and IEnumerable members

        public IEnumerator<KeyValuePair<int, int>> GetEnumerator()
        {
            foreach (var coin in coins)
                yield return coin;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var coin in coins)
                yield return coin;
        }

        #endregion

        /// <summary>
        /// Copies coin dictionary object
        /// </summary>
        /// <returns>Copy of the coin dictionary</returns>
        public CoinCollection Copy()
        {
            var copiedDictionary = new Dictionary<int, int>(coins);
            var coinCollection = new CoinCollection(copiedDictionary);

            return coinCollection;
        }

        /// <summary>
        /// Removes all items from coin collection
        /// </summary>
        public void Clear()
        {
            coins.Clear();
        }

        /// <summary>
        /// Gets the number of elements contained in the coin collection
        /// </summary>
        public int Count => coins.Count;

        /// <summary>
        /// Sum of all coin values in collection
        /// </summary>
        public decimal Sum
        {
            get { return coins.Sum(x => x.Key * x.Value) / 100M; }
        }

        /// <summary>
        /// Adds specified number of coins to coin collection
        /// </summary>
        /// <param name="denomination">Denomination of coins to add to collection</param>
        /// <param name="numberOfCoins">Number of coins to add to collection. Default value is 1.</param>
        /// <exception cref="ArgumentException"></exception>
        public void Add(int denomination, int numberOfCoins = 1)
        {
            if (!availableDenominations.Contains(denomination))
                throw new ArgumentException(ExceptionMessages.IncorrectDenominationValue);

            if (numberOfCoins < 1)
                throw new ArgumentException(ExceptionMessages.NumberOfCoinsGreaterOrEqualToOne);

            if (coins.ContainsKey(denomination))
                coins[denomination] += numberOfCoins;
            else
                coins.Add(denomination, numberOfCoins);
        }

        /// <summary>
        /// Removes specified number of coins from coin collection
        /// </summary>
        /// <param name="denomination">Denomination of coins to remove from collection</param>
        /// <param name="numberOfCoins">Number of coins to remove from collection</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        public void Remove(int denomination, int numberOfCoins)
        {
            if (!availableDenominations.Contains(denomination))
                throw new ArgumentException(ExceptionMessages.IncorrectDenominationValue);

            if (numberOfCoins < 1)
                throw new ArgumentException(ExceptionMessages.NumberOfCoinsGreaterOrEqualToOne);

            if (coins.ContainsKey(denomination))
            {
                if (coins[denomination] > numberOfCoins)
                    coins[denomination] -= numberOfCoins;
                else if (coins[denomination] == numberOfCoins)
                    coins.Remove(denomination);
                else
                    throw new ArgumentException(ExceptionMessages.CannotPerformRemoveOperation);
            }
            else
                throw new KeyNotFoundException(ExceptionMessages.SpecifiedDenominationCoinsNotFound);
        }
    }
}