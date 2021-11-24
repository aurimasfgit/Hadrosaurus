using Hadrosaurus.Core.Models;

namespace Hadrosaurus.Core.Interfaces.Services
{
    public interface ICoinCollectionService
    {
        /// <summary>
        /// Converts value to a collection of coins that can be returned, e.g.
        /// if value equals 0.15 Eur, it can return 1 x 10ct and 1 x 5ct in coin collection (if corresponding coins are in the availableCoins collection)
        /// </summary>
        /// <param name="value">Value to be returned in coins, e.g. 0.25 Eur</param>
        /// <param name="coins">A collection of coins from which coins are selected for return</param>
        /// <returns>The value which is transformed into a coin collection</returns>
        CoinCollection TransformValue(decimal value, CoinCollection availableCoins);
    }
}