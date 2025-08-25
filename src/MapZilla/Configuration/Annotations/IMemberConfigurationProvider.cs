namespace MapZilla.Configuration;

public interface IMemberConfigurationProvider
{
    void ApplyConfiguration(IMemberConfigurationExpression memberConfigurationExpression);
}