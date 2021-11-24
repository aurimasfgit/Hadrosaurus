using Hadrosaurus.Core.Interfaces.Repositories;
using Hadrosaurus.Core.Models;

namespace Hadrosaurus.Dal
{
    /// <summary>
    /// A class that includes methods for vending machine state storing / retrieving from in-memory repository
    /// </summary>
    public class VendingMachineInMemoryRepository : IVendingMachineRepository
    {
        private VendingMachine vendingMachine = new();

        public VendingMachine Get()
        {
            return vendingMachine;
        }

        public void Set(VendingMachine vendingMachine)
        {
            this.vendingMachine = vendingMachine ?? throw new ArgumentNullException(nameof(vendingMachine));
        }
    }
}