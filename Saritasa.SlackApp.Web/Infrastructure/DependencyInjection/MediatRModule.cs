using Saritasa.SlackApp.UseCases.Slack.GetNonBillableDays;

namespace Saritasa.SlackApp.Web.Infrastructure.DependencyInjection;

/// <summary>
/// Register Mediator as dependency.
/// </summary>
internal static class MediatRModule
{
    /// <summary>
    /// Register dependencies.
    /// </summary>
    /// <param name="services">Services.</param>
    public static void Register(IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetNonBillableDaysQuery).Assembly));
    }
}
