using Hadrosaurus.Core.Models;

namespace Hadrosaurus.Core.Interfaces.Repositories
{
    public interface IVendingMachineRepository
    {
        /// <summary>
        /// Gets current state of vending machine object
        /// </summary>
        /// <returns>VendingMachine object</returns>
        VendingMachine Get();

        /// <summary>
        /// Sets state of vending machine object
        /// </summary>
        /// <param name="vendingMachine">Object to be set</param>
        void Set(VendingMachine vendingMachine);
    }
}