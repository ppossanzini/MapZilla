﻿using MapZilla;

namespace MapZilla.IntegrationTests.ExplicitExpansion;

public class ConstructorExplicitExpansionOverride : IntegrationTest<ConstructorExplicitExpansionOverride.DatabaseInitializer> {
    public class Entity {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class SubEntity : Entity {
    }

    record Dto(string Name);
    record SubDto(string Name) : Dto(Name) { }

    public class Context : LocalDbContext {
        public DbSet<Entity> Entities { get; set; }
        public DbSet<SubEntity> SubEntities { get; set; }
    }
    public class DatabaseInitializer : DropCreateDatabaseAlways<Context> {
        protected override void Seed(Context context) {
            context.Entities.Add(new() { Name = "base" });
            context.SubEntities.Add(new() { Name = "derived" });
            base.Seed(context);
        }
    }
    protected override MapperConfiguration CreateConfiguration() => new(c => {
        c.CreateMap<Entity, Dto>().ForCtorParam("Name", o => o.ExplicitExpansion());
        c.CreateMap<SubEntity, SubDto>().IncludeBase<Entity, Dto>().ForCtorParam("Name", o => o.ExplicitExpansion(false));
    });
    [Fact]
    public void Should_work() {
        using var context = new Context();
        var dtos = ProjectTo<Dto>(context.Entities).ToList();
        dtos.Count.ShouldBe(2);
        dtos[0].ShouldBeOfType<Dto>().Name.ShouldBeNull();
        dtos[1].ShouldBeOfType<SubDto>().Name.ShouldBe("derived");
    }
}