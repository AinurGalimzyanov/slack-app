using Microsoft.AspNetCore.Mvc.Rendering;
using Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces;
using Saritasa.SlackApp.Infrastructure.DataAccess;
using Saritasa.SlackApp.Web.Infrastructure.Web;

namespace Saritasa.SlackApp.Web.Infrastructure.DependencyInjection;

/// <summary>
/// System specific dependencies.
/// </summary>
internal static class SystemModule
{
    /// <summary>
    /// Register dependencies.
    /// </summary>
    /// <param name="services">Services.</param>
    public static void Register(IServiceCollection services)
    {
        services.AddSingleton<IJsonHelper, SystemTextJsonHelper>();
        services.AddScoped<IAppDbContext>(s => s.GetRequiredService<AppDbContext>());
    }
}
