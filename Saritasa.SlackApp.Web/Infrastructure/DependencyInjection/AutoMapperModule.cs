using Saritasa.SlackApp.Infrastructure;
using Saritasa.SlackApp.UseCases;
using Saritasa.SlackApp.Web.Controllers.Mappers;

namespace Saritasa.SlackApp.Web.Infrastructure.DependencyInjection;

/// <summary>
/// Register AutoMapper dependencies.
/// </summary>
public class AutoMapperModule
{
    /// <summary>
    /// Register dependencies.
    /// </summary>
    /// <param name="services">Services.</param>
    public static void Register(IServiceCollection services)
    {
        services.AddAutoMapper(
            typeof(Temp).Assembly,
            typeof(PayloadDtoMappingProfile).Assembly,
            typeof(CrmClientMappingProfile).Assembly);
    }
}
