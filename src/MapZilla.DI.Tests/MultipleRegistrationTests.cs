using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace MapZilla.Extensions.Microsoft.DependencyInjection.Tests
{
    public class MultipleRegistrationTests
    {
        [Fact]
        public void Can_register_multiple_times()
        {
            var services = new ServiceCollection();

            services.AddMapZilla(cfg => { });
            services.AddMapZilla(cfg => { });
            services.AddMapZilla(cfg => { });

            var serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetService<IMapper>().ShouldNotBeNull();
        }

        [Fact]
        public void Can_register_assembly_multiple_times()
        {
            var services = new ServiceCollection();

            services.AddMapZilla(typeof(MultipleRegistrationTests));
            services.AddMapZilla(typeof(MultipleRegistrationTests));
            services.AddMapZilla(typeof(MultipleRegistrationTests));
            services.AddTransient<ISomeService, MutableService>();

            var serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetService<IMapper>().ShouldNotBeNull();
            serviceProvider.GetService<DependencyValueConverter>().ShouldNotBeNull();
            serviceProvider.GetServices<DependencyValueConverter>().Count().ShouldBe(1);
        }
    }
}