using MapZilla;

namespace MapZilla.UnitTests.Bug;

public class AssertConfigurationIsValidNullables : MapZillaSpecBase
{
    class Source
    {
        public int? Number { get; set; }
    }
    class Destination
    {
        public decimal? Number { get; set; }
    }

    protected override MapperConfiguration CreateConfiguration() => new(cfg =>
    {
        cfg.CreateMap<Source, Destination>();
    });
    [Fact]
    public void Validate() => AssertConfigurationIsValid();
}