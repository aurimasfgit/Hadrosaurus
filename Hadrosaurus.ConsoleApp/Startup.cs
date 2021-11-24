using Microsoft.Extensions.DependencyInjection;
using Hadrosaurus.ConsoleApp.Extensions;

namespace Hadrosaurus.ConsoleApp
{
    internal class Startup
    {
        private readonly IServiceProvider serviceProvider;

        public Startup()
        {
            var services = new ServiceCollection();

            services.ConfigureServices();

            serviceProvider = services.BuildServiceProvider();
        }

        public IServiceProvider ServiceProvider => serviceProvider;
    }
}