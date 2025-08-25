﻿using MapZilla;
using MapZilla.Execution;

namespace MapZilla.UnitTests;

public class IncludeMembers : MapZillaSpecBase
{
    class Source
    {
        public string Name { get; set; }
        public InnerSource InnerSource { get; set; }
        public OtherInnerSource OtherInnerSource { get; set; }
    }
    class InnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    class OtherInnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
    class Destination
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
    protected override MapperConfiguration CreateConfiguration() => new(cfg=>
    {
        cfg.CreateMap<Source, Destination>().IncludeMembers(s=>s.InnerSource, s=>s.OtherInnerSource);
        cfg.CreateMap<InnerSource, Destination>(MemberList.None);
        cfg.CreateMap<OtherInnerSource, Destination>(MemberList.None);
    });
    [Fact]
    public void Should_flatten()
    {
        var source = new Source { Name = "name", InnerSource = new InnerSource{ Description = "description" }, OtherInnerSource = new OtherInnerSource{ Title = "title" } };
        var destination = Mapper.Map<Destination>(source);
        destination.Name.ShouldBe("name");
        destination.Description.ShouldBe("description");
        destination.Title.ShouldBe("title");
    }
}
public class IncludeMembersWrapperFirstOrDefault : MapZillaSpecBase
{
    class Source
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<InnerSourceWrapper> InnerSources { get; set; } = new List<InnerSourceWrapper>();
        public List<OtherInnerSource> OtherInnerSources { get; set; } = new List<OtherInnerSource>();
    }
    class InnerSourceWrapper
    {
        public InnerSource InnerSource { get; set; }
    }
    class InnerSource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Publisher { get; set; }
    }
    class OtherInnerSource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
    }
    class Destination
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
    }
    protected override MapperConfiguration CreateConfiguration() => new(cfg =>
    {
        cfg.CreateMap<Source, Destination>().IncludeMembers(s => s.InnerSources.FirstOrDefault().InnerSource, s => s.OtherInnerSources.FirstOrDefault()).ReverseMap();
        cfg.CreateMap<InnerSource, Destination>(MemberList.None);
        cfg.CreateMap<OtherInnerSource, Destination>(MemberList.None);
    });
    [Fact]
    public void Should_null_check()
    {
        Expression<Func<Source, InnerSource>> expression = s => s.InnerSources.FirstOrDefault().InnerSource;
        var result= expression.Body.NullCheck(null);
    }
    [Fact]
    public void Should_flatten()
    {
        var source = new Source
        {
            Name = "name",
            InnerSources = { new InnerSourceWrapper { InnerSource = new InnerSource { Description = "description", Publisher = "publisher" } } },
            OtherInnerSources = { new OtherInnerSource { Title = "title", Author = "author" } }
        };
        var destination = Mapper.Map<Destination>(source);
        var plan = Configuration.BuildExecutionPlan(typeof(Source), typeof(Destination));
        FirstOrDefaultCounter.Assert(plan, 2);
        destination.Name.ShouldBe("name");
        destination.Description.ShouldBe("description");
        destination.Title.ShouldBe("title");
        destination.Author.ShouldBe("author");
        destination.Publisher.ShouldBe("publisher");
    }
}
public class IncludeMembersFirstOrDefault : MapZillaSpecBase
{
    class Source
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<InnerSource> InnerSources { get; set; } = new List<InnerSource>();
        public List<OtherInnerSource> OtherInnerSources { get; set; } = new List<OtherInnerSource>();
    }
    class InnerSource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Publisher { get; set; }
    }
    class OtherInnerSource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
    }
    class Destination
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
    }
    protected override MapperConfiguration CreateConfiguration() => new(cfg =>
    {
        cfg.CreateMap<Source, Destination>().IncludeMembers(s => s.InnerSources.FirstOrDefault(), s => s.OtherInnerSources.FirstOrDefault()).ReverseMap();
        cfg.CreateMap<InnerSource, Destination>(MemberList.None);
        cfg.CreateMap<OtherInnerSource, Destination>(MemberList.None);
    });
    [Fact]
    public void Should_flatten()
    {
        var source = new Source
        {
            Name = "name",
            InnerSources = { new InnerSource { Description = "description", Publisher = "publisher" } },
            OtherInnerSources = { new OtherInnerSource { Title = "title", Author = "author" } }
        };
        var destination = Mapper.Map<Destination>(source);
        var plan = Configuration.BuildExecutionPlan(typeof(Source), typeof(Destination));
        FirstOrDefaultCounter.Assert(plan, 2);
        destination.Name.ShouldBe("name");
        destination.Description.ShouldBe("description");
        destination.Title.ShouldBe("title");
        destination.Author.ShouldBe("author");
        destination.Publisher.ShouldBe("publisher");
    }
}
public class IncludeMembersFirstOrDefaultReverseMap : MapZillaSpecBase
{
    class Source
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<InnerSource> InnerSources { get; set; } = new List<InnerSource>();
        public List<OtherInnerSource> OtherInnerSources { get; set; } = new List<OtherInnerSource>();
    }
    class InnerSource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Publisher { get; set; }
    }
    class OtherInnerSource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
    }
    class Destination
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
    }
    protected override MapperConfiguration CreateConfiguration() => new(cfg =>
    {
        cfg.CreateMap<Source, Destination>().IncludeMembers(s => s.InnerSources.FirstOrDefault(), s => s.OtherInnerSources.FirstOrDefault()).ReverseMap();
        cfg.CreateMap<InnerSource, Destination>(MemberList.None).ReverseMap();
        cfg.CreateMap<OtherInnerSource, Destination>(MemberList.None).ReverseMap();
    });
    [Fact]
    public void Should_unflatten()
    {
        var source = Mapper.Map<Source>(new Destination { Description = "description", Name = "name", Title = "title" });
        source.Name.ShouldBe("name");
    }
}
public class IncludeMembersNested : MapZillaSpecBase
{
    class Source
    {
        public string Name { get; set; }
        public InnerSource InnerSource { get; set; }
        public OtherInnerSource OtherInnerSource { get; set; }
    }
    class InnerSource
    {
        public NestedInnerSource NestedInnerSource { get; set; }
    }
    class OtherInnerSource
    {
        public NestedOtherInnerSource NestedOtherInnerSource { get; set; }
    }
    class NestedInnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    class NestedOtherInnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
    class Destination
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
    protected override MapperConfiguration CreateConfiguration() => new(cfg =>
    {
        cfg.CreateMap<Source, Destination>().IncludeMembers(s => s.InnerSource.NestedInnerSource, s => s.OtherInnerSource.NestedOtherInnerSource);
        cfg.CreateMap<NestedInnerSource, Destination>(MemberList.None);
        cfg.CreateMap<NestedOtherInnerSource, Destination>(MemberList.None);
    });
    [Fact]
    public void Should_flatten()
    {
        var source = new Source
        {
            Name = "name",
            InnerSource = new InnerSource { NestedInnerSource = new NestedInnerSource { Description = "description" } },
            OtherInnerSource = new OtherInnerSource { NestedOtherInnerSource = new NestedOtherInnerSource { Title = "title" } }
        };
        var destination = Mapper.Map<Destination>(source);
        destination.Name.ShouldBe("name");
        destination.Description.ShouldBe("description");
        destination.Title.ShouldBe("title");
    }
}

public class IncludeMembersWithMapFromExpression : MapZillaSpecBase
{
    class Source
    {
        public string Name { get; set; }
        public InnerSource InnerSource { get; set; }
        public OtherInnerSource OtherInnerSource { get; set; }
    }
    class InnerSource
    {
        public string Name { get; set; }
        public string Description1 { get; set; }
    }
    class OtherInnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title1 { get; set; }
    }
    class Destination
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
    protected override MapperConfiguration CreateConfiguration() => new(cfg =>
    {
        cfg.CreateMap<Source, Destination>().IncludeMembers(s => s.InnerSource, s => s.OtherInnerSource);
        cfg.CreateMap<InnerSource, Destination>(MemberList.None).ForMember(d=>d.Description, o=>o.MapFrom(s=>s.Description1));
        cfg.CreateMap<OtherInnerSource, Destination>(MemberList.None).ForMember(d=>d.Title, o=>o.MapFrom(s=>s.Title1));
    });
    [Fact]
    public void Should_flatten()
    {
        var source = new Source { Name = "name", InnerSource = new InnerSource { Description1 = "description" }, OtherInnerSource = new OtherInnerSource { Title1 = "title" } };
        var destination = Mapper.Map<Destination>(source);
        destination.Name.ShouldBe("name");
        destination.Description.ShouldBe("description");
        destination.Title.ShouldBe("title");
    }
}

public class IncludeMembersWithNullSubstitute : MapZillaSpecBase
{
    class Source
    {
        public string Name { get; set; }
        public InnerSource InnerSource { get; set; }
        public OtherInnerSource OtherInnerSource { get; set; }
    }
    class InnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    class OtherInnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
    class Destination
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
    protected override MapperConfiguration CreateConfiguration() => new(cfg =>
    {
        cfg.CreateMap<Source, Destination>().IncludeMembers(s => s.InnerSource, s => s.OtherInnerSource);
        cfg.CreateMap<InnerSource, Destination>(MemberList.None).ForMember(d => d.Description, o => o.NullSubstitute("description"));
        cfg.CreateMap<OtherInnerSource, Destination>(MemberList.None).ForMember(d=>d.Title, o => o.NullSubstitute("title"));
    });
    [Fact]
    public void Should_flatten()
    {
        var source = new Source { Name = "name" };
        var destination = Mapper.Map<Destination>(source);
        destination.Name.ShouldBe("name");
        destination.Description.ShouldBe("description");
        destination.Title.ShouldBe("title");
    }
}

public class IncludeMembersWithMapFromFunc : MapZillaSpecBase
{
    class Source
    {
        public string Name { get; set; }
        public InnerSource InnerSource { get; set; }
        public OtherInnerSource OtherInnerSource { get; set; }
    }
    class InnerSource
    {
        public string Name { get; set; }
        public string Description1 { get; set; }
    }
    class OtherInnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title1 { get; set; }
    }
    class Destination
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
    protected override MapperConfiguration CreateConfiguration() => new(cfg =>
    {
        cfg.CreateMap<Source, Destination>().IncludeMembers(s => s.InnerSource, s => s.OtherInnerSource);
        cfg.CreateMap<InnerSource, Destination>(MemberList.None).ForMember(d => d.Description, o => o.MapFrom((s, d) => s.Description1));
        cfg.CreateMap<OtherInnerSource, Destination>(MemberList.None).ForMember(d => d.Title, o => o.MapFrom((s, d) => s.Title1));
    });
    [Fact]
    public void Should_flatten()
    {
        var source = new Source { Name = "name", InnerSource = new InnerSource { Description1 = "description" }, OtherInnerSource = new OtherInnerSource { Title1 = "title" } };
        var destination = Mapper.Map<Destination>(source);
        destination.Name.ShouldBe("name");
        destination.Description.ShouldBe("description");
        destination.Title.ShouldBe("title");
    }
}

public class IncludeMembersWithResolver : MapZillaSpecBase
{
    class Source
    {
        public string Name { get; set; }
        public InnerSource InnerSource { get; set; }
        public OtherInnerSource OtherInnerSource { get; set; }
    }
    class InnerSource
    {
        public string Name { get; set; }
        public string Description1 { get; set; }
    }
    class OtherInnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title1 { get; set; }
    }
    class Destination
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
    protected override MapperConfiguration CreateConfiguration() => new(cfg =>
    {
        cfg.CreateMap<Source, Destination>().IncludeMembers(s => s.InnerSource, s => s.OtherInnerSource);
        cfg.CreateMap<InnerSource, Destination>(MemberList.None).ForMember(d => d.Description, o => o.MapFrom<DescriptionResolver>());
        cfg.CreateMap<OtherInnerSource, Destination>(MemberList.None).ForMember(d => d.Title, o => o.MapFrom<TitleResolver>());
    });
    [Fact]
    public void Should_flatten()
    {
        var source = new Source { Name = "name", InnerSource = new InnerSource { Description1 = "description" }, OtherInnerSource = new OtherInnerSource { Title1 = "title" } };
        var destination = Mapper.Map<Destination>(source);
        destination.Name.ShouldBe("name");
        destination.Description.ShouldBe("description");
        destination.Title.ShouldBe("title");
    }

    private class DescriptionResolver : IValueResolver<InnerSource, Destination, string>
    {
        public string Resolve(InnerSource source, Destination destination, string destMember, ResolutionContext context) => source.Description1;
    }

    private class TitleResolver : IValueResolver<OtherInnerSource, Destination, string>
    {
        public string Resolve(OtherInnerSource source, Destination destination, string destMember, ResolutionContext context) => source.Title1;
    }
}

public class IncludeMembersWithMemberResolver : MapZillaSpecBase
{
    class Source
    {
        public string Name { get; set; }
        public InnerSource InnerSource { get; set; }
        public OtherInnerSource OtherInnerSource { get; set; }
    }
    class InnerSource
    {
        public string Name { get; set; }
        public string Description1 { get; set; }
    }
    class OtherInnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title1 { get; set; }
    }
    class Destination
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
    protected override MapperConfiguration CreateConfiguration() => new(cfg =>
    {
        cfg.CreateMap<Source, Destination>().IncludeMembers(s => s.InnerSource, s => s.OtherInnerSource);
        cfg.CreateMap<InnerSource, Destination>(MemberList.None).ForMember(d => d.Description, o => o.MapFrom<DescriptionResolver,string>(s=>s.Description1));
        cfg.CreateMap<OtherInnerSource, Destination>(MemberList.None).ForMember(d => d.Title, o => o.MapFrom<TitleResolver,string>("Title1"));
    });
    [Fact]
    public void Should_flatten()
    {
        var source = new Source { Name = "name", InnerSource = new InnerSource { Description1 = "description" }, OtherInnerSource = new OtherInnerSource { Title1 = "title" } };
        var destination = Mapper.Map<Destination>(source);
        destination.Name.ShouldBe("name");
        destination.Description.ShouldBe("description");
        destination.Title.ShouldBe("title");
    }

    private class DescriptionResolver : IMemberValueResolver<InnerSource, Destination, string, string>
    {
        public string Resolve(InnerSource source, Destination destination, string sourceMember, string destMember, ResolutionContext context) => sourceMember;
    }

    private class TitleResolver : IMemberValueResolver<OtherInnerSource, Destination, string, string>
    {
        public string Resolve(OtherInnerSource source, Destination destination, string sourceMember, string destMember, ResolutionContext context) => sourceMember;
    }
}
public class IncludeMembersWithValueConverter : MapZillaSpecBase
{
    class Source
    {
        public string Name { get; set; }
        public InnerSource InnerSource { get; set; }
        public OtherInnerSource OtherInnerSource { get; set; }
    }
    class InnerSource
    {
        public string Name { get; set; }
        public string Description1 { get; set; }
    }
    class OtherInnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title1 { get; set; }
    }
    class Destination
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
    protected override MapperConfiguration CreateConfiguration() => new(cfg =>
    {
        cfg.CreateMap<Source, Destination>().IncludeMembers(s => s.InnerSource, s => s.OtherInnerSource);
        cfg.CreateMap<InnerSource, Destination>(MemberList.None).ForMember(d => d.Description, o => o.ConvertUsing<ValueConverter, string>(s => s.Description1));
        cfg.CreateMap<OtherInnerSource, Destination>(MemberList.None).ForMember(d => d.Title, o => o.ConvertUsing<ValueConverter, string>("Title1"));
    });
    [Fact]
    public void Should_flatten()
    {
        var source = new Source { Name = "name", InnerSource = new InnerSource { Description1 = "description" }, OtherInnerSource = new OtherInnerSource { Title1 = "title" } };
        var destination = Mapper.Map<Destination>(source);
        destination.Name.ShouldBe("name");
        destination.Description.ShouldBe("description");
        destination.Title.ShouldBe("title");
    }

    private class ValueConverter : IValueConverter<string, string>
    {
        public string Convert(string sourceMember, ResolutionContext context) => sourceMember;
    }
}

public class IncludeMembersWithConditions : MapZillaSpecBase
{
    class Source
    {
        public string Name { get; set; }
        public InnerSource InnerSource { get; set; }
        public OtherInnerSource OtherInnerSource { get; set; }
    }
    class InnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    class OtherInnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
    class Destination
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
    protected override MapperConfiguration CreateConfiguration() => new(cfg =>
    {
        cfg.CreateMap<Source, Destination>().IncludeMembers(s => s.InnerSource, s => s.OtherInnerSource);
        cfg.CreateMap<InnerSource, Destination>(MemberList.None).ForMember(d => d.Description, o => o.Condition((s, d, sm, dm, c) => false));
        cfg.CreateMap<OtherInnerSource, Destination>(MemberList.None).ForMember(d => d.Description, o => o.Condition((s, d, sm, dm, c) => true));
    });
    [Fact]
    public void Should_flatten()
    {
        var source = new Source { Name = "name", InnerSource = new InnerSource { Description = "description" }, OtherInnerSource = new OtherInnerSource { Title = "title" } };
        var destination = Mapper.Map<Destination>(source);
        destination.Name.ShouldBe("name");
        destination.Description.ShouldBeNull();
        destination.Title.ShouldBe("title");
    }
}
public class IncludeMembersWithPreConditions : MapZillaSpecBase
{
    class Source
    {
        public string Name { get; set; }
        public InnerSource InnerSource { get; set; }
        public OtherInnerSource OtherInnerSource { get; set; }
    }
    class InnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    class OtherInnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
    class Destination
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
    protected override MapperConfiguration CreateConfiguration() => new(cfg =>
    {
        cfg.CreateMap<Source, Destination>().IncludeMembers(s => s.InnerSource, s => s.OtherInnerSource);
        cfg.CreateMap<InnerSource, Destination>(MemberList.None).ForMember(d => d.Description, o => o.PreCondition((s, d, c) => false));
        cfg.CreateMap<OtherInnerSource, Destination>(MemberList.None).ForMember(d => d.Description, o => o.PreCondition((s, d, c) => true));
    });
    [Fact]
    public void Should_flatten()
    {
        var source = new Source { Name = "name", InnerSource = new InnerSource { Description = "description" }, OtherInnerSource = new OtherInnerSource { Title = "title" } };
        var destination = Mapper.Map<Destination>(source);
        destination.Name.ShouldBe("name");
        destination.Description.ShouldBeNull();
        destination.Title.ShouldBe("title");
    }
}
public class IncludeMembersCycle : MapZillaSpecBase
{
    class Source
    {
        public string Name { get; set; }
        public InnerSource InnerSource { get; set; }
        public OtherInnerSource OtherInnerSource { get; set; }
    }
    class InnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Source Parent { get; set; }
    }
    class OtherInnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public Source Parent { get; set; }
    }
    class Destination
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public Destination Parent { get; set; }
    }
    protected override MapperConfiguration CreateConfiguration() => new(cfg =>
    {
        cfg.CreateMap<Source, Destination>().IncludeMembers(s => s.InnerSource, s => s.OtherInnerSource);
        cfg.CreateMap<InnerSource, Destination>(MemberList.None).IncludeMembers(s=>s.Parent);
        cfg.CreateMap<OtherInnerSource, Destination>(MemberList.None).IncludeMembers(s=>s.Parent);
    });
    [Fact]
    public void Should_flatten()
    {
        var source = new Source { Name = "name", InnerSource = new InnerSource { Description = "description" }, OtherInnerSource = new OtherInnerSource { Title = "title" } };
        source.InnerSource.Parent = source;
        source.OtherInnerSource.Parent = source;
        var destination = Mapper.Map<Destination>(source);
        destination.Name.ShouldBe("name");
        destination.Description.ShouldBe("description");
        destination.Title.ShouldBe("title");
        destination.Parent.ShouldBe(destination);
    }
}
public class IncludeMembersReverseMap : MapZillaSpecBase
{
    class Source
    {
        public string Name { get; set; }
        public InnerSource InnerSource { get; set; }
        public OtherInnerSource OtherInnerSource { get; set; }
    }
    class InnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    class OtherInnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
    class Destination
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
    protected override MapperConfiguration CreateConfiguration() => new(cfg =>
    {
        cfg.CreateMap<Source, Destination>().IncludeMembers(s => s.InnerSource, s => s.OtherInnerSource).ReverseMap();
        cfg.CreateMap<InnerSource, Destination>(MemberList.None).ReverseMap();
        cfg.CreateMap<OtherInnerSource, Destination>(MemberList.None).ReverseMap();
    });
    [Fact]
    public void Should_unflatten()
    {
        var source = Mapper.Map<Source>(new Destination { Description = "description", Name = "name", Title = "title" });
        source.Name.ShouldBe("name");
        source.InnerSource.Name.ShouldBe("name");
        source.OtherInnerSource.Name.ShouldBe("name");
        source.InnerSource.Description.ShouldBe("description");
        source.OtherInnerSource.Description.ShouldBe("description");
        source.OtherInnerSource.Title.ShouldBe("title");
    }
}
public class IncludeMembersReverseMapOverride : MapZillaSpecBase
{
    class Source
    {
        public string Name { get; set; }
        public InnerSource InnerSource { get; set; }
        public OtherInnerSource OtherInnerSource { get; set; }
    }
    class InnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    class OtherInnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
    class Destination
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
    protected override MapperConfiguration CreateConfiguration() => new(cfg =>
    {
        cfg.CreateMap<Source, Destination>().IncludeMembers(s => s.InnerSource, s => s.OtherInnerSource).ReverseMap()
            .ForMember(d=>d.InnerSource, o=>o.Ignore())
            .ForMember(d=>d.OtherInnerSource, o=>o.Ignore());
        cfg.CreateMap<InnerSource, Destination>(MemberList.None);
        cfg.CreateMap<OtherInnerSource, Destination>(MemberList.None);
    });
    [Fact]
    public void Should_unflatten()
    {
        var source = Mapper.Map<Source>(new Destination { Description = "description", Name = "name", Title = "title" });
        source.Name.ShouldBe("name");
        source.InnerSource.ShouldBeNull();
        source.OtherInnerSource.ShouldBeNull();
    }
}

public class ReverseMapToIncludeMembers : MapZillaSpecBase
{
    class Source
    {
        public string Name { get; set; }
        public InnerSource InnerSource { get; set; }
        public OtherInnerSource OtherInnerSource { get; set; }
    }
    class InnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    class OtherInnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
    class Destination
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
    protected override MapperConfiguration CreateConfiguration() => new(cfg =>
    {
        cfg.CreateMap<Destination, Source>()
            .ForMember(d => d.InnerSource, o => o.MapFrom(s => s))
            .ForMember(d => d.OtherInnerSource, o => o.MapFrom(s => s))
            .ReverseMap();
        cfg.CreateMap<InnerSource, Destination>(MemberList.None).ReverseMap();
        cfg.CreateMap<OtherInnerSource, Destination>(MemberList.None).ReverseMap();
    });
    [Fact]
    public void Should_flatten()
    {
        var source = new Source { Name = "name", InnerSource = new InnerSource { Description = "description" }, OtherInnerSource = new OtherInnerSource { Title = "title" } };
        var destination = Mapper.Map<Destination>(source);
        destination.Name.ShouldBe("name");
        destination.Description.ShouldBe("description");
        destination.Title.ShouldBe("title");
    }
}
public class ReverseMapToIncludeMembersOverride : MapZillaSpecBase
{
    class Source
    {
        public string Name { get; set; }
        public InnerSource InnerSource { get; set; }
        public OtherInnerSource OtherInnerSource { get; set; }
    }
    class InnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    class OtherInnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
    class Destination
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
    protected override MapperConfiguration CreateConfiguration() => new(cfg =>
    {
        cfg.CreateMap<Destination, Source>(MemberList.None)
            .ForMember(d => d.InnerSource, o => o.MapFrom(s => s))
            .ForMember(d => d.OtherInnerSource, o => o.MapFrom(s => s))
            .ReverseMap()
            .IncludeMembers();
        cfg.CreateMap<InnerSource, Destination>(MemberList.None).ReverseMap();
        cfg.CreateMap<OtherInnerSource, Destination>(MemberList.None).ReverseMap();
    });
    [Fact]
    public void Should_flatten()
    {
        var source = new Source { Name = "name", InnerSource = new InnerSource { Description = "description" }, OtherInnerSource = new OtherInnerSource { Title = "title" } };
        var destination = Mapper.Map<Destination>(source);
        destination.Name.ShouldBe("name");
        destination.Description.ShouldBeNull();
        destination.Title.ShouldBeNull();
    }
}
public class IncludeMembersWithAfterMap : MapZillaSpecBase
{
    class Source
    {
        public string Name { get; set; }
        public InnerSource InnerSource { get; set; }
        public OtherInnerSource OtherInnerSource { get; set; }
    }
    class InnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    class OtherInnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
    class Destination
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
    bool afterMap, beforeMap;
    protected override MapperConfiguration CreateConfiguration() => new(cfg =>
    {
        cfg.CreateMap<Source, Destination>().IncludeMembers(s => s.InnerSource, s => s.OtherInnerSource);
        cfg.CreateMap<InnerSource, Destination>(MemberList.None).AfterMap((s,d)=>afterMap=true);
        cfg.CreateMap<OtherInnerSource, Destination>(MemberList.None).BeforeMap((s, d, c) => beforeMap = true);
    });
    [Fact]
    public void Should_flatten()
    {
        var source = new Source { Name = "name", InnerSource = new InnerSource { Description = "description" }, OtherInnerSource = new OtherInnerSource { Title = "title" } };
        var destination = Mapper.Map<Destination>(source);
        destination.Name.ShouldBe("name");
        destination.Description.ShouldBe("description");
        destination.Title.ShouldBe("title");
        afterMap.ShouldBeTrue();
        beforeMap.ShouldBeTrue();
    }
}

public class IncludeMembersWithForPath : MapZillaSpecBase
{
    class Source
    {
        public string Name { get; set; }
        public InnerSource InnerSource { get; set; }
        public OtherInnerSource OtherInnerSource { get; set; }
    }
    class InnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    class OtherInnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
    class Destination
    {
        public string Name { get; set; }
        public InnerDestination InnerDestination { get; set; }
    }
    class InnerDestination
    {
        public string Description { get; set; }
        public string Title { get; set; }
    }
    protected override MapperConfiguration CreateConfiguration() => new(cfg =>
    {
        cfg.CreateMap<Source, Destination>().IncludeMembers(s => s.InnerSource, s => s.OtherInnerSource);
        cfg.CreateMap<InnerSource, Destination>(MemberList.None).ForPath(d=>d.InnerDestination.Description, o=>
        {
            o.MapFrom(s => s.Description);
            o.Condition(c => true);
        });
        cfg.CreateMap<OtherInnerSource, Destination>(MemberList.None).ForPath(d=>d.InnerDestination.Title, o=>
        {
            o.MapFrom(s => s.Title);
            o.Condition(c => true);
        });
    });
    [Fact]
    public void Should_flatten()
    {
        var source = new Source { Name = "name", InnerSource = new InnerSource { Description = "description" }, OtherInnerSource = new OtherInnerSource { Title = "title" } };
        var destination = Mapper.Map<Destination>(source);
        destination.Name.ShouldBe("name");
        destination.InnerDestination.Description.ShouldBe("description");
        destination.InnerDestination.Title.ShouldBe("title");
    }
}
public class IncludeMembersTransformers : MapZillaSpecBase
{
    class Source
    {
        public string Name { get; set; }
        public InnerSource InnerSource { get; set; }
        public OtherInnerSource OtherInnerSource { get; set; }
    }
    class InnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    class OtherInnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
    class Destination
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
    protected override MapperConfiguration CreateConfiguration() => new(cfg =>
    {
        cfg.CreateMap<Source, Destination>().IncludeMembers(s => s.InnerSource, s => s.OtherInnerSource).AddTransform<string>(s => s + "Main");
        cfg.CreateMap<InnerSource, Destination>(MemberList.None).ForMember(d=>d.Description, o=>o.AddTransform(s=>s+"Extra")).AddTransform<string>(s => s + "Ex");
        cfg.CreateMap<OtherInnerSource, Destination>(MemberList.None).ForMember(d => d.Title, o => o.AddTransform(s => s + "Extra")).AddTransform<string>(s => s + "Ex");
    });
    [Fact]
    public void Should_flatten()
    {
        var source = new Source { Name = "name", InnerSource = new InnerSource { Description = "description" }, OtherInnerSource = new OtherInnerSource { Title = "title" } };
        var destination = Mapper.Map<Destination>(source);
        destination.Name.ShouldBe("nameMain");
        destination.Description.ShouldBe("descriptionExtraExMain");
        destination.Title.ShouldBe("titleExtraExMain");
    }
}
public class IncludeMembersTransformersPerMember : MapZillaSpecBase
{
    class Source
    {
        public string Name { get; set; }
        public InnerSource InnerSource { get; set; }
        public OtherInnerSource OtherInnerSource { get; set; }
    }
    class InnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    class OtherInnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
    class Destination
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
    protected override MapperConfiguration CreateConfiguration() => new(cfg =>
    {
        cfg.CreateMap<Source, Destination>().IncludeMembers(s => s.InnerSource, s => s.OtherInnerSource);
        cfg.CreateMap<InnerSource, Destination>(MemberList.None).ForMember(d=>d.Description, o=>o.AddTransform(s=>s+"Ex"));
        cfg.CreateMap<OtherInnerSource, Destination>(MemberList.None).ForMember(d => d.Title, o => o.AddTransform(s => s + "Ex"));
    });
    [Fact]
    public void Should_flatten()
    {
        var source = new Source { Name = "name", InnerSource = new InnerSource { Description = "description" }, OtherInnerSource = new OtherInnerSource { Title = "title" } };
        var destination = Mapper.Map<Destination>(source);
        destination.Name.ShouldBe("name");
        destination.Description.ShouldBe("descriptionEx");
        destination.Title.ShouldBe("titleEx");
    }
}
public class IncludeMembersWithGenerics : MapZillaSpecBase
{
    class Source<TInnerSource, TOtherInnerSource>
    {
        public string Name { get; set; }
        public TInnerSource InnerSource { get; set; }
        public TOtherInnerSource OtherInnerSource { get; set; }
    }
    class InnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    class OtherInnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
    class Destination
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
    protected override MapperConfiguration CreateConfiguration() => new(cfg =>
    {
        cfg.CreateMap(typeof(Source<,>), typeof(Destination), MemberList.None).IncludeMembers("InnerSource", "OtherInnerSource");
        cfg.CreateMap<InnerSource, Destination>(MemberList.None);
        cfg.CreateMap<OtherInnerSource, Destination>(MemberList.None);
    });
    [Fact]
    public void Should_flatten()
    {
        var source = new Source<InnerSource, OtherInnerSource> { Name = "name", InnerSource = new InnerSource { Description = "description" }, OtherInnerSource = new OtherInnerSource { Title = "title" } };
        var destination = Mapper.Map<Destination>(source);
        destination.Name.ShouldBe("name");
        destination.Description.ShouldBe("description");
        destination.Title.ShouldBe("title");
    }
}

public class IncludeMembersWithGenericsInvalidStrings
{
    class Source<TInnerSource, TOtherInnerSource>
    {
        public string Name { get; set; }
        public TInnerSource InnerSource { get; set; }
        public TOtherInnerSource OtherInnerSource { get; set; }
    }
    class Destination
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
    [Fact]
    public void Should_throw()
    {
        new MapperConfiguration(cfg =>
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => cfg.CreateMap(typeof(Source<,>), typeof(Destination), MemberList.None).IncludeMembers("dInnerSource", "fOtherInnerSource"));
        });
    }
}

public class IncludeMembersReverseMapGenerics : MapZillaSpecBase
{
    class Source<TInnerSource, TOtherInnerSource>
    {
        public string Name { get; set; }
        public TInnerSource InnerSource { get; set; }
        public TOtherInnerSource OtherInnerSource { get; set; }
    }
    class InnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    class OtherInnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
    class Destination
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
    protected override MapperConfiguration CreateConfiguration() => new(cfg =>
    {
        cfg.CreateMap(typeof(Source<,>), typeof(Destination), MemberList.None).IncludeMembers("InnerSource", "OtherInnerSource").ReverseMap();
        cfg.CreateMap<InnerSource, Destination>(MemberList.None).ReverseMap();
        cfg.CreateMap<OtherInnerSource, Destination>(MemberList.None).ReverseMap();
    });
    [Fact]
    public void Should_unflatten()
    {
        var source = Mapper.Map<Source<InnerSource, OtherInnerSource>>(new Destination { Description = "description", Name = "name", Title = "title" });
        source.Name.ShouldBe("name");
        source.InnerSource.Name.ShouldBe("name");
        source.OtherInnerSource.Name.ShouldBe("name");
        source.InnerSource.Description.ShouldBe("description");
        source.OtherInnerSource.Description.ShouldBe("description");
        source.OtherInnerSource.Title.ShouldBe("title");
    }
}
public class IncludeMembersReverseMapGenericsOverride : MapZillaSpecBase
{
    class Source<TInnerSource, TOtherInnerSource>
    {
        public string Name { get; set; }
        public TInnerSource InnerSource { get; set; }
        public TOtherInnerSource OtherInnerSource { get; set; }
    }
    class InnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    class OtherInnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
    class Destination
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
    protected override MapperConfiguration CreateConfiguration() => new(cfg =>
    {
        cfg.CreateMap(typeof(Source<,>), typeof(Destination), MemberList.None).IncludeMembers("InnerSource", "OtherInnerSource").ReverseMap()
            .ForMember("InnerSource", o=>o.Ignore())
            .ForMember("OtherInnerSource", o=>o.Ignore());
        cfg.CreateMap<InnerSource, Destination>(MemberList.None);
        cfg.CreateMap<OtherInnerSource, Destination>(MemberList.None);
    });
    [Fact]
    public void Should_unflatten()
    {
        var source = Mapper.Map<Source<InnerSource, OtherInnerSource>>(new Destination { Description = "description", Name = "name", Title = "title" });
        source.Name.ShouldBe("name");
        source.InnerSource.ShouldBeNull();
        source.OtherInnerSource.ShouldBeNull();
    }
}
public class ReverseMapToIncludeMembersGenerics : MapZillaSpecBase
{
    class Source<TInnerSource, TOtherInnerSource>
    {
        public string Name { get; set; }
        public TInnerSource InnerSource { get; set; }
        public TOtherInnerSource OtherInnerSource { get; set; }
    }
    class InnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    class OtherInnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
    class Destination
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
    protected override MapperConfiguration CreateConfiguration() => new(cfg =>
    {
        cfg.CreateMap(typeof(Destination), typeof(Source<,>))
            .ForMember("InnerSource", o => o.MapFrom(s => s))
            .ForMember("OtherInnerSource", o => o.MapFrom(s => s))
            .ReverseMap();
        cfg.CreateMap<InnerSource, Destination>(MemberList.None);
        cfg.CreateMap<OtherInnerSource, Destination>(MemberList.None);
    });
    [Fact]
    public void Should_flatten()
    {
        var source = new Source<InnerSource, OtherInnerSource> { Name = "name", InnerSource = new InnerSource { Description = "description" }, OtherInnerSource = new OtherInnerSource { Title = "title" } };
        var destination = Mapper.Map<Destination>(source);
        destination.Name.ShouldBe("name");
        destination.Description.ShouldBe("description");
        destination.Title.ShouldBe("title");
    }
}
public class ReverseMapToIncludeMembersGenericsOverride : MapZillaSpecBase
{
    class Source<TInnerSource, TOtherInnerSource>
    {
        public string Name { get; set; }
        public TInnerSource InnerSource { get; set; }
        public TOtherInnerSource OtherInnerSource { get; set; }
    }
    class InnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    class OtherInnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
    class Destination
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
    protected override MapperConfiguration CreateConfiguration() => new(cfg =>
    {
        cfg.CreateMap(typeof(Destination), typeof(Source<,>))
            .ForMember("InnerSource", o => o.MapFrom(s => s))
            .ForMember("OtherInnerSource", o => o.MapFrom(s => s))
            .ReverseMap()
            .IncludeMembers();
        cfg.CreateMap<InnerSource, Destination>(MemberList.None);
        cfg.CreateMap<OtherInnerSource, Destination>(MemberList.None);
    });
    [Fact]
    public void Should_flatten()
    {
        var source = new Source<InnerSource, OtherInnerSource> { Name = "name", InnerSource = new InnerSource { Description = "description" }, OtherInnerSource = new OtherInnerSource { Title = "title" } };
        var destination = Mapper.Map<Destination>(source);
        destination.Name.ShouldBe("name");
        destination.Description.ShouldBeNull();
        destination.Title.ShouldBeNull();
    }
}
public class IncludeMembersSourceValidation : MapZillaSpecBase
{
    class Source
    {
        public string Name { get; set; }
        public InnerSource InnerSource { get; set; }
        public OtherInnerSource OtherInnerSource { get; set; }
    }
    class InnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    class OtherInnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
    class Destination
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
    protected override MapperConfiguration CreateConfiguration() => new(cfg =>
    {
        cfg.CreateMap<Source, Destination>(MemberList.Source).IncludeMembers(s => s.InnerSource, s => s.OtherInnerSource);
        cfg.CreateMap<InnerSource, Destination>(MemberList.None);
        cfg.CreateMap<OtherInnerSource, Destination>(MemberList.None);
    });
    [Fact]
    public void Should_flatten()
    {
        var source = new Source { Name = "name", InnerSource = new InnerSource { Description = "description" }, OtherInnerSource = new OtherInnerSource { Title = "title" } };
        var destination = Mapper.Map<Destination>(source);
        destination.Name.ShouldBe("name");
        destination.Description.ShouldBe("description");
        destination.Title.ShouldBe("title");
    }

}
public class IncludeMembersWithGenericsSourceValidation : MapZillaSpecBase
{
    class Source<TInnerSource, TOtherInnerSource>
    {
        public string Name { get; set; }
        public TInnerSource InnerSource { get; set; }
        public TOtherInnerSource OtherInnerSource { get; set; }
    }
    class InnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    class OtherInnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
    class Destination
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
    protected override MapperConfiguration CreateConfiguration() => new(cfg =>
    {
        cfg.CreateMap(typeof(Source<,>), typeof(Destination), MemberList.Source).IncludeMembers("InnerSource", "OtherInnerSource");
        cfg.CreateMap<InnerSource, Destination>(MemberList.None);
        cfg.CreateMap<OtherInnerSource, Destination>(MemberList.None);
    });
    [Fact]
    public void Should_flatten()
    {
        var source = new Source<InnerSource, OtherInnerSource> { Name = "name", InnerSource = new InnerSource { Description = "description" }, OtherInnerSource = new OtherInnerSource { Title = "title" } };
        var destination = Mapper.Map<Destination>(source);
        destination.Name.ShouldBe("name");
        destination.Description.ShouldBe("description");
        destination.Title.ShouldBe("title");
    }
}
public class IncludeMembersWithInclude : MapZillaSpecBase
{
    public class ParentOfSource
    {
        public Source InnerSource { get; set; }
    }
    public class Source : SourceBase
    {
    }
    public class SourceBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public class Destination
    {
        public string FullName { get; set; }
    }
    protected override MapperConfiguration CreateConfiguration() => new(cfg =>
    {
        cfg.CreateMap<SourceBase, Destination>().ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName)).IncludeAllDerived();
        cfg.CreateMap<ParentOfSource, Destination>().IncludeMembers(src => src.InnerSource);
        cfg.CreateMap<Source, Destination>();
    });
    [Fact]
    public void Should_inherit_configuration() => Mapper.Map<Destination>(new ParentOfSource { InnerSource = new Source { FirstName = "first", LastName = "last" } }).FullName.ShouldBe("first last");
}
public class IncludeMembersWithIncludeDifferentOrder : MapZillaSpecBase
{
    public class ParentOfSource
    {
        public Source InnerSource { get; set; }
    }
    public class Source : SourceBase
    {
    }
    public class SourceBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public class Destination
    {
        public string FullName { get; set; }
    }
    protected override MapperConfiguration CreateConfiguration() => new(cfg =>
    {
        cfg.CreateMap<SourceBase, Destination>().ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName)).IncludeAllDerived();
        cfg.CreateMap<Source, Destination>();
        cfg.CreateMap<ParentOfSource, Destination>().IncludeMembers(src => src.InnerSource);
    });
    [Fact]
    public void Should_inherit_configuration() => Mapper.Map<Destination>(new ParentOfSource { InnerSource = new Source { FirstName = "first", LastName = "last" } }).FullName.ShouldBe("first last");
}
public class IncludeMembersWithIncludeBase : MapZillaSpecBase
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
    }
    public class Address
    {
        public string Line1 { get; set; }
        public string Postcode { get; set; }
    }
    public class CustomerDtoBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AddressLine1 { get; set; }
        public string Postcode { get; set; }
    }
    public class CreateCustomerDto : CustomerDtoBase
    {
        public string CreatedBy { get; set; }
    }
    protected override MapperConfiguration CreateConfiguration() => new(cfg=>
    {
        cfg.CreateMap<Customer, CustomerDtoBase>().IncludeMembers(x => x.Address) .ForMember(m => m.Id, o => o.Ignore());
        cfg.CreateMap<Address, CustomerDtoBase>(MemberList.None).ForMember(m => m.AddressLine1, o => o.MapFrom(x => x.Line1));
        cfg.CreateMap<Customer, CreateCustomerDto>().IncludeBase<Customer, CustomerDtoBase>().ForMember(m => m.CreatedBy, o => o.Ignore());
    });
    [Fact]
    public void Should_inherit_IncludeMembers() => Mapper.Map<CreateCustomerDto>(new Customer { Address = new Address { Postcode = "Postcode" } }).Postcode.ShouldBe("Postcode");
}
public class IncludeMembersWithIncludeBaseOverride : MapZillaSpecBase
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public Address NewAddress { get; set; }
    }
    public class Address
    {
        public string Line1 { get; set; }
        public string Postcode { get; set; }
    }
    public class CustomerDtoBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AddressLine1 { get; set; }
        public string Postcode { get; set; }
    }
    public class CreateCustomerDto : CustomerDtoBase
    {
        public string CreatedBy { get; set; }
    }
    protected override MapperConfiguration CreateConfiguration() => new(cfg =>
    {
        cfg.CreateMap<Customer, CustomerDtoBase>().IncludeMembers(x => x.Address).ForMember(m => m.Id, o => o.Ignore());
        cfg.CreateMap<Address, CustomerDtoBase>(MemberList.None).ForMember(m => m.AddressLine1, o => o.MapFrom(x => x.Line1));
        cfg.CreateMap<Address, CreateCustomerDto>(MemberList.None).IncludeBase<Address, CustomerDtoBase>();
        cfg.CreateMap<Customer, CreateCustomerDto>().IncludeMembers(s => s.NewAddress).IncludeBase<Customer, CustomerDtoBase>().ForMember(m => m.CreatedBy, o => o.Ignore());
    });
    [Fact]
    public void Should_override_IncludeMembers() => Mapper.Map<CreateCustomerDto>(new Customer { NewAddress = new Address { Postcode = "Postcode" } }).Postcode.ShouldBe("Postcode");
}
public class IncludeMembersWithIncludeBaseOverrideMapFrom : MapZillaSpecBase
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
    }
    public class Address
    {
        public string Line1 { get; set; }
        public string Postcode { get; set; }
    }
    public class CustomerDtoBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AddressLine1 { get; set; }
        public string Postcode { get; set; }
    }
    public class CreateCustomerDto : CustomerDtoBase
    {
        public string CreatedBy { get; set; }
    }
    protected override MapperConfiguration CreateConfiguration() => new(cfg =>
    {
        cfg.CreateMap<Customer, CustomerDtoBase>().IncludeMembers(x => x.Address).ForMember(m => m.Id, o => o.Ignore());
        cfg.CreateMap<Address, CustomerDtoBase>(MemberList.None).ForMember(m => m.AddressLine1, o => o.MapFrom(x => x.Line1));
        cfg.CreateMap<Customer, CreateCustomerDto>()
            .IncludeBase<Customer, CustomerDtoBase>()
            .ForMember(d=>d.Postcode, o=>o.MapFrom((s, d)=>s.Name))
            .ForMember(m => m.CreatedBy, o => o.Ignore());
    });
    [Fact]
    public void Should_override_IncludeMembers() => Mapper.Map<CreateCustomerDto>(new Customer { Name = "Postcode", Address = new Address() }).Postcode.ShouldBe("Postcode");
}
public class IncludeMembersWithIncludeBaseOverrideConvention : MapZillaSpecBase
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
    }
    public class NewCustomer : Customer
    {
        public string Postcode { get; set; }
    }
    public class Address
    {
        public string Line1 { get; set; }
        public string Postcode { get; set; }
    }
    public class CustomerDtoBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AddressLine1 { get; set; }
        public string Postcode { get; set; }
    }
    public class CreateCustomerDto : CustomerDtoBase
    {
        public string CreatedBy { get; set; }
    }
    protected override MapperConfiguration CreateConfiguration() => new(cfg =>
    {
        cfg.CreateMap<Customer, CustomerDtoBase>().IncludeMembers(x => x.Address).ForMember(m => m.Id, o => o.Ignore());
        cfg.CreateMap<Address, CustomerDtoBase>(MemberList.None).ForMember(m => m.AddressLine1, o => o.MapFrom(x => x.Line1));
        cfg.CreateMap<NewCustomer, CreateCustomerDto>().IncludeBase<Customer, CustomerDtoBase>().ForMember(m => m.CreatedBy, o => o.Ignore());
    });
    [Fact]
    public void Should_override_IncludeMembers() => Mapper.Map<CreateCustomerDto>(new NewCustomer { Postcode = "Postcode", Address = new Address() }).Postcode.ShouldBe("Postcode");
}
public class IncludeMembersWithValueTypeValidation : MapZillaSpecBase
{
    class Source
    {
        public InnerSource InnerSource { get; set; }
    }
    struct InnerSource
    {
        public string Name { get; set; }
    }
    class Destination
    {
        public string Name { get; set; }
    }
    protected override MapperConfiguration CreateConfiguration() => new(cfg =>
    {
        cfg.CreateMap<Source, Destination>().IncludeMembers(s => s.InnerSource);
        cfg.CreateMap<InnerSource, Destination>(MemberList.None);
    });
    [Fact]
    public void Validate() => AssertConfigurationIsValid();
}
public class CascadedIncludeMembers : MapZillaSpecBase
{
    public class Source
    {
        public int Id;
        public Level1 FieldLevel1;
    }
    public class Level1
    {
        public Level2 FieldLevel2;
        public long Level1Field;
    }
    public class Level2
    {
        public long TheField;
    }
    public class Destination
    {
        public int Id;
        public long TheField;
        public long Level1Field;
    }
    protected override MapperConfiguration CreateConfiguration() => new(cfg =>
    {
        cfg.CreateMap<Source, Destination>().IncludeMembers(s => s.FieldLevel1);
        cfg.CreateMap<Level1, Destination>(MemberList.None).IncludeMembers(s => s.FieldLevel2);
        cfg.CreateMap<Level2, Destination>(MemberList.None);
    });
    [Fact]
    public void Should_work()
    {
        var dest = Map<Destination>(new Source { Id = 1, FieldLevel1 = new Level1 { Level1Field = 3, FieldLevel2 = new Level2 { TheField = 2 } } });
        dest.Id.ShouldBe(1);
        dest.TheField.ShouldBe(2);
        dest.Level1Field.ShouldBe(3);
    }
}
public class CascadedIncludeMembersForPath : MapZillaSpecBase
{
    public class Source
    {
        public int Id;
        public Level1 FieldLevel1;
    }
    public class Level1
    {
        public Level2 FieldLevel2;
        public long Level1Field;
    }
    public class Level2
    {
        public long TheField;
    }
    public class Destination
    {
        public int Id;
        public long TheField;
        public long Level1Field;
    }
    protected override MapperConfiguration CreateConfiguration() => new(cfg =>
    {
        cfg.CreateMap<Source, Destination>().IncludeMembers(s => s.FieldLevel1);
        cfg.CreateMap<Level1, Destination>(MemberList.None).IncludeMembers(s => s.FieldLevel2);
        cfg.CreateMap<Level2, Destination>(MemberList.None).ForPath(d => d.TheField, o => o.MapFrom(s => s.TheField));
    });
    [Fact]
    public void Should_work()
    {
        var dest = Map<Destination>(new Source { Id = 1, FieldLevel1 = new Level1 { Level1Field = 3, FieldLevel2 = new Level2 { TheField = 2 } } });
        dest.Id.ShouldBe(1);
        dest.TheField.ShouldBe(2);
        dest.Level1Field.ShouldBe(3);
    }
}
public class IncludeMembersWithCascadedIncludeBase : MapZillaSpecBase
{
    class Item
    {
        public int Id { get; set; }
        public MetaData MetaData { get; set; }
        public string Signature { get; set; }
    }
    class MetaData
    {
        public string Hash { get; set; }
    }
    class ExpiredItem : Item
    {
        public DateTime Expired { get; set; }
    }
    class Response
    {
        public int Id { get; set; }
        public string Hash { get; set; }
    }
    class SignedResponse : Response
    {
        public string Signature { get; set; }
        public DateTime Expired { get; set; }
    }
    protected override MapperConfiguration CreateConfiguration() => new(cfg =>
    {
        cfg.CreateMap<MetaData, Response>(MemberList.None);
        cfg.CreateMap<Item, Response>().IncludeMembers(src => src.MetaData);
        cfg.CreateMap<Item, SignedResponse>().IncludeBase<Item, Response>().ForMember(dest => dest.Expired, opt => opt.Ignore());
        cfg.CreateMap<ExpiredItem, SignedResponse>().IncludeBase<Item, SignedResponse>();
    });
    [Fact]
    public void Should_inherit_IncludeMembers() => Mapper.Map<SignedResponse>(new ExpiredItem { MetaData = new MetaData { Hash = "hash" } }).Hash.ShouldBe("hash");
}
public class IncludeMembersConstructorMapping : MapZillaSpecBase
{
    public class Source
    {
        public int Id;
        public Level1 FieldLevel1;
    }
    public class Level1
    {
        public Level2 FieldLevel2;
        public long Level1Field;
    }
    public class Level2
    {
        public long TheField;
    }
    public record Destination(int Id, long TheField, long Level1Field)
    {
    }
    protected override MapperConfiguration CreateConfiguration() => new(cfg =>
    {
        cfg.ShouldUseConstructor = c => c.IsPublic;
        cfg.CreateMap<Source, Destination>().IncludeMembers(s => s.FieldLevel1);
        cfg.CreateMap<Level1, Destination>(MemberList.None).IncludeMembers(s => s.FieldLevel2);
        cfg.CreateMap<Level2, Destination>(MemberList.None);
    });
    [Fact]
    public void Should_work()
    {
        var dest = Map<Destination>(new Source { Id = 1, FieldLevel1 = new Level1 { Level1Field = 3, FieldLevel2 = new Level2 { TheField = 2 } } });
        dest.Id.ShouldBe(1);
        dest.TheField.ShouldBe(2);
        dest.Level1Field.ShouldBe(3);
    }
}
public class IncludeMembersMultipleConstructorMapping : MapZillaSpecBase
{
    public class Source
    {
        public int Id;
        public Level1 FieldLevel1;
    }
    public class Level1
    {
        public Level2 FieldLevel2;
        public long Level1Field;
    }
    public class Level2
    {
        public long TheField;
    }
    public record Destination(int Id, long TheField, long Level1Field)
    {
        public Destination(Level2 fieldLevel2, long level1Field) : this(-1, fieldLevel2.TheField, level1Field) { }
        public Destination(long theField) : this(-2, theField, -2) { }
    }
    protected override MapperConfiguration CreateConfiguration() => new(cfg =>
    {
        cfg.ShouldUseConstructor = c => c.IsPublic;
        cfg.CreateMap<Source, Destination>().IncludeMembers(s => s.FieldLevel1);
        cfg.CreateMap<Level1, Destination>(MemberList.None).IncludeMembers(s => s.FieldLevel2);
        cfg.CreateMap<Level2, Destination>(MemberList.None);
    });
    [Fact]
    public void Should_work()
    {
        var dest = Map<Destination>(new Source { Id = 1, FieldLevel1 = new Level1 { Level1Field = 3, FieldLevel2 = new Level2 { TheField = 2 } } });
        dest.Id.ShouldBe(1);
        dest.TheField.ShouldBe(2);
        dest.Level1Field.ShouldBe(3);
    }
}
public class IncludeMembersNullCheck : MapZillaSpecBase
{
    class Source
    {
        public string Name { get; set; }
        public InnerSource InnerSource { get; set; }
    }
    class InnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    class Destination
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    protected override MapperConfiguration CreateConfiguration() => new(cfg =>
    {
        cfg.CreateMap<Source, Destination>().IncludeMembers(s => s.InnerSource);
        cfg.CreateMap<InnerSource, Destination>(MemberList.None);
    });
    [Fact]
    public void Should_flatten() => Mapper.Map<Destination[]>(new[] { default(Source) })[0].ShouldBeNull();
}
public class IncludeMembersCascadedNullCheck : MapZillaSpecBase
{
    public class Grandchild
    {
        public string C { get; set; }
    }
    public class Child
    {
        public string B { get; set; }
        public Grandchild Grandchild { get; set; }
    }
    public class Parent
    {
        public string A { get; set; }
        public Child Child { get; set; }
    }
    public class Dto
    {
        public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }
    }
    protected override MapperConfiguration CreateConfiguration() => new(c =>
    {
        c.CreateMap<Parent, Dto>().IncludeMembers(s => s.Child);
        c.CreateMap<Child, Dto>(MemberList.None).IncludeMembers(s => s.Grandchild);
        c.CreateMap<Grandchild, Dto>(MemberList.None);
    });
    [Fact]
    public void Should_flatten() => Mapper.Map<Dto>(new Parent { A = "a" }).A.ShouldBe("a");
}