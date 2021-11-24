using Hadrosaurus.Core;
using Hadrosaurus.Core.Interfaces.Services;
using Hadrosaurus.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Hadrosaurus.Bll
{
    public class CoinCollectionService : ICoinCollectionService
    {
        public CoinCollection TransformValue(decimal value, CoinCollection availableCoins)
        {
            if (value <= 0)
                throw new ArgumentException(ExceptionMessages.ShouldBeGreaterThanZero);

            if (availableCoins.Sum < value)
                throw new ValidationException(ExceptionMessages.InsufficientAmount);

            var coinCollection = new CoinCollection();

            // converting value, which is, for example, 0.15 to value in cents, e.g. 15, because we will operate with coins
            var valueInCents = value * 100;

            // filtering (taking only those coins where denomination value is lower or equal to requested value in cents,
            // because it is not possible to make, for example, 10 cents from 20 or 50 cent coins)
            // all available coins and ordering by denomination value descending
            var suitableCoinCollection = availableCoins.Where(x => x.Key <= valueInCents).OrderByDescending(x => x.Key);

            // iterating through all available coins (their values) from highest to lowest and trying to "fill in" the amount
            foreach (var suitableCoin in suitableCoinCollection)
            {
                if (valueInCents <= 0)
                    break;

                // calculating number of needed coins of specific denomination
                int numberOfCoinsNeeded = (int)valueInCents / suitableCoin.Key;

                if (numberOfCoinsNeeded == 0)
                    continue;

                // if there's not enough coins of specified denomination, using all available coins of that denomination
                if (numberOfCoinsNeeded > suitableCoin.Value)
                    numberOfCoinsNeeded = suitableCoin.Value;

                valueInCents -= (numberOfCoinsNeeded * suitableCoin.Key);

                coinCollection.Add(suitableCoin.Key, numberOfCoinsNeeded);
            }

            // if at the end of loop value is not equal to zero, means that there's something wrong, usually not enough available coins
            if (valueInCents != 0)
                throw new ValidationException(ExceptionMessages.NotEnoughCoinsOfCertainDenomination);

            return coinCollection;
        }
    }
}