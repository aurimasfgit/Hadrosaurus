using Hadrosaurus.Core.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Hadrosaurus.Bll.UnitTests.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<ICoinCollectionService, CoinCollectionService>();
        }
    }
}