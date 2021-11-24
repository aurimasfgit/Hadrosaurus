using Hadrosaurus.Core;
using Hadrosaurus.Core.Interfaces.Repositories;
using Hadrosaurus.Core.Interfaces.Services;
using Hadrosaurus.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Hadrosaurus.Bll
{
    public class VendingMachineService : IVendingMachineService
    {
        private readonly IVendingMachineRepository vendingMachineRepository;
        private readonly ICoinCollectionService coinCollectionService;

        public VendingMachineService(IVendingMachineRepository vendingMachineRepository, ICoinCollectionService coinCollectionService)
        {
            ArgumentNullException.ThrowIfNull(vendingMachineRepository);
            ArgumentNullException.ThrowIfNull(coinCollectionService);

            this.vendingMachineRepository = vendingMachineRepository;
            this.coinCollectionService = coinCollectionService;
        }

        private VendingMachine GetVendingMachineWithCheck()
        {
            var vendingMachine = vendingMachineRepository.Get();

            ArgumentNullException.ThrowIfNull(vendingMachine);

            return vendingMachine;
        }

        public void LoadItems(IDictionary<int, VendingMachineItem> items)
        {
            var vendingMachine = GetVendingMachineWithCheck();

            // TODO: check there is enough space for items to be inserted
            // TODO: append items to collection, not replace
            vendingMachine.Items = items;

            vendingMachineRepository.Set(vendingMachine);
        }

        public void LoadCoins(CoinCollection coins)
        {
            var vendingMachine = GetVendingMachineWithCheck();

            // TODO: check there is available slots for all coins to be inserted
            // TODO: append coins to collection, not replace
            vendingMachine.Coins = coins;

            vendingMachineRepository.Set(vendingMachine);
        }

        public IDictionary<int, VendingMachineItem> GetItems()
        {
            var vendingMachine = GetVendingMachineWithCheck();

            return vendingMachine.Items;
        }

        public void InsertCoin(int denomination)
        {
            var vendingMachine = GetVendingMachineWithCheck();

            // TODO: check if it is possible to insert more coins to vending machine. Maybe there's some physical limit or something.

            vendingMachine.InsertedCoins.Add(denomination);

            vendingMachineRepository.Set(vendingMachine);
        }

        // TODO: possibility to pay in cash (not only coins)

        public CoinCollection Purchase(int code)
        {
            var vendingMachine = GetVendingMachineWithCheck();

            if (!vendingMachine.Items.ContainsKey(code))
                throw new KeyNotFoundException(ExceptionMessages.CannotFindItemByCode);

            var vendingMachineItem = vendingMachine.Items[code];

            if (vendingMachineItem.NumberOfItems == 0)
                throw new ValidationException(ExceptionMessages.ItemNotAvailable);

            if (vendingMachine.InsertedCoins.Sum < vendingMachineItem.Price)
                throw new ValidationException(ExceptionMessages.NotEnoughMoneyInserted);

            // TODO: check if there is enough available space to "save" all coins in vending machine

            var allCoins = vendingMachine.Coins.Copy();

            // merging vendingMachine.Coins and vendingMachine.InsertedCoins collections into one - allCoins. We could use
            // vendingMachine.Coins collection and insert/remove coins directly there, but in case of failure we don't want to change state of vendingMachine
            foreach (var insertedCoins in vendingMachine.InsertedCoins)
                allCoins.Add(insertedCoins.Key, insertedCoins.Value);

            // calculating change to return
            var change = vendingMachine.InsertedCoins.Sum - vendingMachineItem.Price;
            var changeInCoins = coinCollectionService.TransformValue(change, allCoins);

            // removing coins needed for change from all coins collection
            foreach (var coin in changeInCoins)
                allCoins.Remove(coin.Key, coin.Value);

            // TODO: implement unit of work or some kind of transaction to restore vending machine state when errors occur

            vendingMachine.Coins = allCoins;

            vendingMachineItem.RemoveOneItem();

            vendingMachine.InsertedCoins.Clear();

            vendingMachineRepository.Set(vendingMachine);

            return changeInCoins;
        }

        public CoinCollection CancelTransaction()
        {
            var vendingMachine = GetVendingMachineWithCheck();
            var insertedCoins = vendingMachine.InsertedCoins.Copy();

            vendingMachine.InsertedCoins.Clear();

            vendingMachineRepository.Set(vendingMachine);

            return insertedCoins;
        }
    }
}