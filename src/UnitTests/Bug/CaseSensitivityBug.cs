using MapZilla;

namespace MapZilla.UnitTests.Bug;
public class CaseSensitivityBug : MapZillaSpecBase
{
    protected override MapperConfiguration CreateConfiguration() => new(cfg =>
    {
        cfg.CreateMap<Foo, Bar>();
    });
    public class Foo
    {
        public int ID { get; set; }
    }

    public class Bar
    {
        public int id { get; set; }
    }
    [Fact]
    public void Validate() => AssertConfigurationIsValid();
}