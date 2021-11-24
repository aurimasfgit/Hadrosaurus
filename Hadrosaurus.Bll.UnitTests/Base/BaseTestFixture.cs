using Hadrosaurus.Bll.UnitTests.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace Hadrosaurus.Bll.UnitTests.Base
{
    public class BaseTestFixture : TestBedFixture
    {
        protected override void AddServices(IServiceCollection services, IConfiguration configuration) => services.ConfigureServices();

        protected override ValueTask DisposeAsyncCore() => new();

        protected override string GetConfigurationFile() => string.Empty;
    }
}