using Hadrosaurus.Bll.UnitTests.Base;
using Hadrosaurus.Core;
using Hadrosaurus.Core.Interfaces.Services;
using Hadrosaurus.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;
using Xunit.Abstractions;

namespace Hadrosaurus.Bll.UnitTests
{
    public class CoinCollectionServiceTests : BaseTestCollection
    {
        public CoinCollectionServiceTests(ITestOutputHelper testOutputHelper, BaseTestFixture fixture)
          : base(testOutputHelper, fixture)
        {
        }

        private CoinCollection TransformValue(decimal value, IDictionary<int, int> coins)
        {
            var vendingMachineChangeService = _fixture.GetService<ICoinCollectionService>(_testOutputHelper);
            var availableCoins = new CoinCollection(coins);

            return vendingMachineChangeService.TransformValue(value, availableCoins);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void ShouldBeGreaterThanZero(decimal value)
        {
            try
            {
                TransformValue(value, new Dictionary<int, int>());
            }
            catch (ArgumentException exc)
            {
                Assert.Equal(ExceptionMessages.ShouldBeGreaterThanZero, exc.Message);
            }
        }

        [Fact]
        public void InsufficientAmount()
        {
            var availableCoins = new Dictionary<int, int>
            {
                { 50, 1 }, // 1 x 50 ct
                { 5, 2 } // 2 x 5 ct
            };

            try
            {
                TransformValue(0.70M, availableCoins);
            }
            catch (ValidationException exc)
            {
                Assert.Equal(ExceptionMessages.InsufficientAmount, exc.Message);
            }
        }

        [Fact]
        public void NotEnoughCoinsOfCertainDenomination()
        {
            var availableCoins = new Dictionary<int, int>
            {
                { 50, 1 }, // 1  x 50 ct
                { 5, 2 } // 2 x 5 ct
            };

            try
            {
                TransformValue(0.15M, availableCoins);
            }
            catch (ValidationException exc)
            {
                Assert.Equal(ExceptionMessages.NotEnoughCoinsOfCertainDenomination, exc.Message);
            }
        }

        [Theory]
        [InlineData(0.15)]
        [InlineData(2.23)]
        [InlineData(2.24)]
        public void ReturnsCorrectSumOfCoins(decimal value)
        {
            var availableCoins = new Dictionary<int, int>
            {
                { 50, 4 }, // 4 x 50 ct
                { 10, 3 }, // 3 x 10 ct
                { 5, 3 }, // 3 x 5 ct
                { 2, 1 }, // 2 x 2 ct
                { 1, 3 } // 3 x 1 ct
            };

            var coins = TransformValue(value, availableCoins);

            Assert.Equal(value, coins.Sum);
        }

        // TODO: write more tests
    }
}