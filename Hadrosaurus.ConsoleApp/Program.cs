using Hadrosaurus.ConsoleApp;
using Hadrosaurus.ConsoleApp.Common;
using Microsoft.Extensions.DependencyInjection;

var startup = new Startup();

var vendingMachineDataLoader = startup.ServiceProvider.GetRequiredService<VendingMachineDataLoader>();
var vendingMachineMenu = startup.ServiceProvider.GetRequiredService<VendingMachineMenu>();

ArgumentNullException.ThrowIfNull(vendingMachineDataLoader);
ArgumentNullException.ThrowIfNull(vendingMachineMenu);

vendingMachineDataLoader.LoadData(); // loads the items for sale and coins for the change into vending machine
vendingMachineMenu.Show(); // renders default menu of vending machine