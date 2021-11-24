This is solution for Ars Ingenii recruitment and selection process. 
Given code-name Hadrosaurus (meaning "bulky lizard"). Noticed out to be a pretty good association with the vending machine :)
Solution is not fully complete. There are comments in the source code that explain how the program works and why one or the other 
decision was made. There are also comments that look like // TODO: [something]. There's written about what should be implemented 
in the program and some thoughts for the future.

The initial data required for the application is loaded in class Hadrosaurus.ConsoleApp.Common.VendingMachineDataLoader,
method LoadData. This method simulates items (products) and coins loading at the start of the day (usually).

Another important location of source code is in class Hadrosaurus.ConsoleApp.Common.VendingMachineMenu, method Show. When we choose 
command "A" of vending machine, we're inserting coin into vending machine which we can use to buy item. For now there is hard-coded 
insertion of one 20 ct coin. We can change that value in source code or if we need more money we can choose to insert a coin multiple
times, e.g. if we select option "A" three times, 3 x 20 ct coins will be inserted.