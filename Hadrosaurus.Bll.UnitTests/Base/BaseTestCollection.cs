using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace Hadrosaurus.Bll.UnitTests.Base
{
    public class BaseTestCollection : TestBed<BaseTestFixture>
    {
        public BaseTestCollection(ITestOutputHelper testOutputHelper, BaseTestFixture fixture)
          : base(testOutputHelper, fixture)
        {
        }

        protected override void Clear()
        {
        }

        protected override ValueTask DisposeAsyncCore() => new();
    }
}