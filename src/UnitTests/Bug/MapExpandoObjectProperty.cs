using System.Dynamic;
using MapZilla;

namespace MapZilla.UnitTests.Bug;

public class MapExpandoObjectProperty : MapZillaSpecBase
{

    class From
    {
        public ExpandoObject ExpandoObject { get; set; }
    }

    class To
    {
        public ExpandoObject ExpandoObject { get; set; }
    }

    protected override MapperConfiguration CreateConfiguration() => new(cfg =>
    {
        cfg.CreateMap<From, To>();
    });
    [Fact]
    public void Should_work()
    {
        dynamic baseSettings = new ExpandoObject();
        var settings = Mapper.Map<To>(new From { ExpandoObject = baseSettings});
    }
}