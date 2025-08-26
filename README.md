![MapZilla](logo.png)



### What is MapZilla?


MapZilla is a open-source derivate work of [AutoMapper](https://github.com/jbogard/Automapper) version 14.0.0



MapZilla is a simple little library built to solve a deceptively complex problem - getting rid of code that mapped one object to another. This type of code is rather dreary and boring to write, so why not invent a tool to do it for us?

This is the main repository for MapZilla, but there's more:

* [Collection Extensions](https://github.com/MapZilla/MapZilla.Collection)
* [Expression Mapping](https://github.com/MapZilla/MapZilla.Extensions.ExpressionMapping)
* [EF6 Extensions](https://github.com/MapZilla/MapZilla.EF6)
* [IDataReader/Record Extensions](https://github.com/MapZilla/MapZilla.Data)
* [Enum Extensions](https://github.com/MapZilla/MapZilla.Extensions.EnumMapping)

### How do I get started?

First, configure MapZilla to know what types you want to map, in the startup of your application:

```csharp
var configuration = new MapperConfiguration(cfg => 
{
    cfg.CreateMap<Foo, FooDto>();
    cfg.CreateMap<Bar, BarDto>();
});
// only during development, validate your mappings; remove it before release
#if DEBUG
configuration.AssertConfigurationIsValid();
#endif
// use DI (http://docs.MapZilla.org/en/latest/Dependency-injection.html) or create the mapper yourself
var mapper = configuration.CreateMapper();
```
Then in your application code, execute the mappings:

```csharp
var fooDto = mapper.Map<FooDto>(foo);
var barDto = mapper.Map<BarDto>(bar);
```

Check out the [getting started guide](https://MapZilla.readthedocs.io/en/latest/Getting-started.html). When you're done there, the [wiki](https://MapZilla.readthedocs.io/en/latest/) goes in to the nitty-gritty details. If you have questions, you can post them to [Stack Overflow](https://stackoverflow.com/questions/tagged/MapZilla) or in our [Gitter](https://gitter.im/MapZilla/MapZilla).

### Where can I get it?

First, [install NuGet](http://docs.nuget.org/docs/start-here/installing-nuget). Then, install [MapZilla](https://www.nuget.org/packages/MapZilla/) from the package manager console:

```
PM> Install-Package MapZilla
```
Or from the .NET CLI as:
```
dotnet add package MapZilla
```

### Do you have an issue?
\
First check if it's already fixed by trying the [MyGet build](https://MapZilla.readthedocs.io/en/latest/The-MyGet-build.html).

You might want to know exactly what [your mapping does](https://MapZilla.readthedocs.io/en/latest/Understanding-your-mapping.html) at runtime.

If you're still running into problems, file an issue above.

### License, etc.

This project has adopted the code of conduct defined by the Contributor Covenant to clarify expected behavior in our community.
For more information see the [.NET Foundation Code of Conduct](https://dotnetfoundation.org/code-of-conduct).

MapZilla is Copyright &copy; 2025 [Paolo Possanzini] and other contributors under the [MIT license](https://github.com/MapZilla/MapZilla?tab=MIT-1-ov-file#MIT-1-ov-file).
MapZilla is a Derivate Work of AutoMapper from [Jimmy Bogard](https://jimmybogard.com)
