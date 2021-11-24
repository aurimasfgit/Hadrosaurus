using Hadrosaurus.Bll;
using Hadrosaurus.ConsoleApp.Common;
using Hadrosaurus.Core.Interfaces.Repositories;
using Hadrosaurus.Core.Interfaces.Services;
using Hadrosaurus.Dal;
using Microsoft.Extensions.DependencyInjection;

namespace Hadrosaurus.ConsoleApp.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            // registration of repositories and services
            services.AddSingleton<IVendingMachineRepository, VendingMachineInMemoryRepository>();
            services.AddSingleton<IVendingMachineService, VendingMachineService>();
            services.AddSingleton<ICoinCollectionService, CoinCollectionService>();

            // registration of ConsoleApp objects 
            services.AddSingleton<VendingMachineDataLoader>();
            services.AddSingleton<VendingMachineMenu>();
        }
    }
}