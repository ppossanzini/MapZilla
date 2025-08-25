using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace MapZilla.Extensions.Microsoft.DependencyInjection.Tests
{
	public class ServiceLifetimeTests
	{
		//Implicitly Transient
		[Fact]
		public void AddMapZillaExtensionDefaultWithAssemblySingleDelegateArgCollection()
		{
			//arrange
			var serviceCollection = new ServiceCollection();

			//act
			serviceCollection.AddMapZilla(cfg => { }, new List<Assembly>());
			var serviceDescriptor = serviceCollection.FirstOrDefault(sd => sd.ServiceType == typeof(IMapper));

			//assert
			serviceDescriptor.ShouldNotBeNull();
			serviceDescriptor.Lifetime.ShouldBe(ServiceLifetime.Transient);
		}

		[Fact]
		public void AddMapZillaExtensionDefaultWithAssemblyDoubleDelegateArgCollection()
		{
			//arrange
			var serviceCollection = new ServiceCollection();

			//act
			serviceCollection.AddMapZilla((sp, cfg) => { }, new List<Assembly>());
			var serviceDescriptor = serviceCollection.FirstOrDefault(sd => sd.ServiceType == typeof(IMapper));

			//assert
			serviceDescriptor.ShouldNotBeNull();
			serviceDescriptor.Lifetime.ShouldBe(ServiceLifetime.Transient);
		}

		[Fact]
		public void AddMapZillaExtensionDefaultWithAssemblyCollection()
		{
			//arrange
			var serviceCollection = new ServiceCollection();

			//act
			serviceCollection.AddMapZilla(new List<Assembly>());
			var serviceDescriptor = serviceCollection.FirstOrDefault(sd => sd.ServiceType == typeof(IMapper));

			//assert
			serviceDescriptor.ShouldNotBeNull();
			serviceDescriptor.Lifetime.ShouldBe(ServiceLifetime.Transient);
		}

		[Fact]
		public void AddMapZillaExtensionDefaultSingleDelegateWithProfileTypeCollection()
		{
			//arrange
			var serviceCollection = new ServiceCollection();

			//act
			serviceCollection.AddMapZilla(cfg => { },new[] {typeof(ServiceLifetimeTests)});
			var serviceDescriptor = serviceCollection.FirstOrDefault(sd => sd.ServiceType == typeof(IMapper));

			//assert
			serviceDescriptor.ShouldNotBeNull();
			serviceDescriptor.Lifetime.ShouldBe(ServiceLifetime.Transient);
		}

		[Fact]
		public void AddMapZillaExtensionDefaultDoubleDelegateWithProfileTypeCollection()
		{
			//arrange
			var serviceCollection = new ServiceCollection();

			//act
			serviceCollection.AddMapZilla((sp, cfg) => { },new[] {typeof(ServiceLifetimeTests)});
			var serviceDescriptor = serviceCollection.FirstOrDefault(sd => sd.ServiceType == typeof(IMapper));

			//assert
			serviceDescriptor.ShouldNotBeNull();
			serviceDescriptor.Lifetime.ShouldBe(ServiceLifetime.Transient);
		}

		//Explicitly Singleton
		[Fact]
		public void AddMapZillaExtensionSingletonWithAssemblySingleDelegateArgCollection()
		{
			//arrange
			var serviceCollection = new ServiceCollection();

			//act
			serviceCollection.AddMapZilla(cfg => { }, new List<Assembly>(), ServiceLifetime.Singleton);
			var serviceDescriptor = serviceCollection.FirstOrDefault(sd => sd.ServiceType == typeof(IMapper));

			//assert
			serviceDescriptor.ShouldNotBeNull();
			serviceDescriptor.Lifetime.ShouldBe(ServiceLifetime.Singleton);
		}

		[Fact]
		public void AddMapZillaExtensionSingletonWithAssemblyDoubleDelegateArgCollection()
		{
			//arrange
			var serviceCollection = new ServiceCollection();

			//act
			serviceCollection.AddMapZilla((sp, cfg) => { }, new List<Assembly>(), ServiceLifetime.Singleton);
			var serviceDescriptor = serviceCollection.FirstOrDefault(sd => sd.ServiceType == typeof(IMapper));

			//assert
			serviceDescriptor.ShouldNotBeNull();
			serviceDescriptor.Lifetime.ShouldBe(ServiceLifetime.Singleton);
		}

		[Fact]
		public void AddMapZillaExtensionSingletonWithAssemblyCollection()
		{
			//arrange
			var serviceCollection = new ServiceCollection();

			//act
			serviceCollection.AddMapZilla(new List<Assembly>(), ServiceLifetime.Singleton);
			var serviceDescriptor = serviceCollection.FirstOrDefault(sd => sd.ServiceType == typeof(IMapper));

			//assert
			serviceDescriptor.ShouldNotBeNull();
			serviceDescriptor.Lifetime.ShouldBe(ServiceLifetime.Singleton);
		}

		[Fact]
		public void AddMapZillaExtensionSingletonSingleDelegateWithProfileTypeCollection()
		{
			//arrange
			var serviceCollection = new ServiceCollection();

			//act
			serviceCollection.AddMapZilla(cfg => { },new[] {typeof(ServiceLifetimeTests)}, ServiceLifetime.Singleton);
			var serviceDescriptor = serviceCollection.FirstOrDefault(sd => sd.ServiceType == typeof(IMapper));

			//assert
			serviceDescriptor.ShouldNotBeNull();
			serviceDescriptor.Lifetime.ShouldBe(ServiceLifetime.Singleton);
		}

		[Fact]
		public void AddMapZillaExtensionSingletonDoubleDelegateWithProfileTypeCollection()
		{
			//arrange
			var serviceCollection = new ServiceCollection();

			//act
			serviceCollection.AddMapZilla((sp, cfg) => { },new[] {typeof(ServiceLifetimeTests)}, ServiceLifetime.Singleton);
			var serviceDescriptor = serviceCollection.FirstOrDefault(sd => sd.ServiceType == typeof(IMapper));

			//assert
			serviceDescriptor.ShouldNotBeNull();
			serviceDescriptor.Lifetime.ShouldBe(ServiceLifetime.Singleton);
		}

		//Explicitly Transient
		[Fact]
		public void AddMapZillaExtensionTransientWithAssemblySingleDelegateArgCollection()
		{
			//arrange
			var serviceCollection = new ServiceCollection();

			//act
			serviceCollection.AddMapZilla(cfg => { }, new List<Assembly>(), ServiceLifetime.Transient);
			var serviceDescriptor = serviceCollection.FirstOrDefault(sd => sd.ServiceType == typeof(IMapper));

			//assert
			serviceDescriptor.ShouldNotBeNull();
			serviceDescriptor.Lifetime.ShouldBe(ServiceLifetime.Transient);
		}

		[Fact]
		public void AddMapZillaExtensionTransientWithAssemblyDoubleDelegateArgCollection()
		{
			//arrange
			var serviceCollection = new ServiceCollection();

			//act
			serviceCollection.AddMapZilla((sp, cfg) => { }, new List<Assembly>(), ServiceLifetime.Transient);
			var serviceDescriptor = serviceCollection.FirstOrDefault(sd => sd.ServiceType == typeof(IMapper));

			//assert
			serviceDescriptor.ShouldNotBeNull();
			serviceDescriptor.Lifetime.ShouldBe(ServiceLifetime.Transient);
		}

		[Fact]
		public void AddMapZillaExtensionTransientWithAssemblyCollection()
		{
			//arrange
			var serviceCollection = new ServiceCollection();

			//act
			serviceCollection.AddMapZilla(new List<Assembly>(), ServiceLifetime.Transient);
			var serviceDescriptor = serviceCollection.FirstOrDefault(sd => sd.ServiceType == typeof(IMapper));

			//assert
			serviceDescriptor.ShouldNotBeNull();
			serviceDescriptor.Lifetime.ShouldBe(ServiceLifetime.Transient);
		}

		[Fact]
		public void AddMapZillaExtensionTransientSingleDelegateWithProfileTypeCollection()
		{
			//arrange
			var serviceCollection = new ServiceCollection();

			//act
			serviceCollection.AddMapZilla(cfg => { },new[] {typeof(ServiceLifetimeTests)}, ServiceLifetime.Transient);
			var serviceDescriptor = serviceCollection.FirstOrDefault(sd => sd.ServiceType == typeof(IMapper));

			//assert
			serviceDescriptor.ShouldNotBeNull();
			serviceDescriptor.Lifetime.ShouldBe(ServiceLifetime.Transient);
		}

		[Fact]
		public void AddMapZillaExtensionTransientDoubleDelegateWithProfileTypeCollection()
		{
			//arrange
			var serviceCollection = new ServiceCollection();

			//act
			serviceCollection.AddMapZilla((sp, cfg) => { },new[] {typeof(ServiceLifetimeTests)}, ServiceLifetime.Transient);
			var serviceDescriptor = serviceCollection.FirstOrDefault(sd => sd.ServiceType == typeof(IMapper));

			//assert
			serviceDescriptor.ShouldNotBeNull();
			serviceDescriptor.Lifetime.ShouldBe(ServiceLifetime.Transient);
		}

		//Explicitly Scoped
		[Fact]
		public void AddMapZillaExtensionScopedWithAssemblySingleDelegateArgCollection()
		{
			//arrange
			var serviceCollection = new ServiceCollection();

			//act
			serviceCollection.AddMapZilla(cfg => { }, new List<Assembly>(), ServiceLifetime.Scoped);
			var serviceDescriptor = serviceCollection.FirstOrDefault(sd => sd.ServiceType == typeof(IMapper));

			//assert
			serviceDescriptor.ShouldNotBeNull();
			serviceDescriptor.Lifetime.ShouldBe(ServiceLifetime.Scoped);
		}

		[Fact]
		public void AddMapZillaExtensionScopedWithAssemblyDoubleDelegateArgCollection()
		{
			//arrange
			var serviceCollection = new ServiceCollection();

			//act
			serviceCollection.AddMapZilla((sp, cfg) => { }, new List<Assembly>(), ServiceLifetime.Scoped);
			var serviceDescriptor = serviceCollection.FirstOrDefault(sd => sd.ServiceType == typeof(IMapper));

			//assert
			serviceDescriptor.ShouldNotBeNull();
			serviceDescriptor.Lifetime.ShouldBe(ServiceLifetime.Scoped);
		}

		[Fact]
		public void AddMapZillaExtensionScopedWithAssemblyCollection()
		{
			//arrange
			var serviceCollection = new ServiceCollection();

			//act
			serviceCollection.AddMapZilla(new List<Assembly>(), ServiceLifetime.Scoped);
			var serviceDescriptor = serviceCollection.FirstOrDefault(sd => sd.ServiceType == typeof(IMapper));

			//assert
			serviceDescriptor.ShouldNotBeNull();
			serviceDescriptor.Lifetime.ShouldBe(ServiceLifetime.Scoped);
		}

		[Fact]
		public void AddMapZillaExtensionScopedSingleDelegateWithProfileTypeCollection()
		{
			//arrange
			var serviceCollection = new ServiceCollection();

			//act
			serviceCollection.AddMapZilla(cfg => { },new[] {typeof(ServiceLifetimeTests)}, ServiceLifetime.Scoped);
			var serviceDescriptor = serviceCollection.FirstOrDefault(sd => sd.ServiceType == typeof(IMapper));

			//assert
			serviceDescriptor.ShouldNotBeNull();
			serviceDescriptor.Lifetime.ShouldBe(ServiceLifetime.Scoped);
		}

		[Fact]
		public void AddMapZillaExtensionScopedDoubleDelegateWithProfileTypeCollection()
		{
			//arrange
			var serviceCollection = new ServiceCollection();

			//act
			serviceCollection.AddMapZilla((sp, cfg) => { },new[] {typeof(ServiceLifetimeTests)}, ServiceLifetime.Scoped);
			var serviceDescriptor = serviceCollection.FirstOrDefault(sd => sd.ServiceType == typeof(IMapper));

			//assert
			serviceDescriptor.ShouldNotBeNull();
			serviceDescriptor.Lifetime.ShouldBe(ServiceLifetime.Scoped);
		}

	}
}