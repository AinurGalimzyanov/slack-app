using Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces.Crm.Options;
using SlackNet.AspNetCore;
using SlackNet.Extensions.DependencyInjection;

namespace Saritasa.SlackApp.Web.Infrastructure.Startup;

/// <summary>
/// Slack options setup.
/// </summary>
internal class SlackOptionsSetup
{
    private const string Section = "Application";

    private readonly IConfiguration configuration;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="configuration">Configuration.</param>
    public SlackOptionsSetup(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    /// <summary>
    /// Setup slack service.
    /// </summary>
    /// <param name="slackConfiguration">Slack configuration.</param>
    public void Setup(ServiceCollectionSlackServiceConfiguration slackConfiguration)
    {
        var appSettings = configuration.GetSection(Section).Get<AppSettings>();
        if (appSettings == null)
        {
            throw new InvalidOperationException($"Required {nameof(appSettings)} configuration parameter is missing.");
        }
        slackConfiguration.UseApiToken(appSettings.SlackAuthToken);
    }

    /// <summary>
    /// Register slack to middleware.
    /// </summary>
    /// <param name="endpointConfiguration">Endpoint Configuration.</param>
    public void Register(SlackEndpointConfiguration endpointConfiguration)
    {
        var appSettings = configuration.GetSection(Section).Get<AppSettings>();
        if (appSettings == null)
        {
            throw new InvalidOperationException($"Required {nameof(appSettings)} configuration parameter is missing.");
        }
        endpointConfiguration.UseSigningSecret(appSettings.SlackSigningSecret);
    }
}
