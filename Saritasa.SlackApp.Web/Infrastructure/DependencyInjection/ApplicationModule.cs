using Saritasa.SlackApp.Infrastructure;
using Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces.Crm;
using Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces.Slack;
using Saritasa.SlackApp.Web.BackgroundJobRunner;

namespace Saritasa.SlackApp.Web.Infrastructure.DependencyInjection;

/// <summary>
/// Application specific dependencies.
/// </summary>
internal static class ApplicationModule
{
    /// <summary>
    /// Register dependencies.
    /// </summary>
    /// <param name="services">Services.</param>
    /// <param name="configuration">Configuration.</param>
#pragma warning disable CA1801 // Review unused parameters
    public static void Register(IServiceCollection services, IConfiguration configuration)
#pragma warning restore CA1801 // Review unused parameters
    {
        services
            .AddScoped<ISlackMessageSender, SlackMessageSender>()
            .AddScoped<ISlackViewPublisher, SlackViewPublisher>()
            .AddScoped<ICrmClient, CrmClient>()
            .AddScoped<BackgroundSlackRunner>()
            .AddScoped<ISlackViewService, SlackViewService>();
    }
}
