﻿namespace MapZilla.Configuration.Annotations;

/// <summary>
/// Specify the source member to map from. Can only reference a member on the <see cref="MapFromAttribute.SourceType" /> type
/// </summary>
/// <remarks>
/// Must be used in combination with <see cref="MapFromAttribute" />
/// </remarks>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public sealed class SourceMemberAttribute(string name) : Attribute, IMemberConfigurationProvider
{
    public string Name { get; } = name;

    public void ApplyConfiguration(IMemberConfigurationExpression memberConfigurationExpression)
    {
        var destinationMember = memberConfigurationExpression.DestinationMember;
        if (destinationMember.Has<ValueConverterAttribute>() || destinationMember.Has<ValueResolverAttribute>())
        {
            return;
        }
        memberConfigurationExpression.MapFrom(Name);
    }
}