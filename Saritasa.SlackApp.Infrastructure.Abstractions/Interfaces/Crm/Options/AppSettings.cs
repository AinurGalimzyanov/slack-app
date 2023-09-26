namespace Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces.Crm.Options;

/// <summary>
/// Global application settings.
/// </summary>
public class AppSettings
{
    /// <summary>
    /// Slack auth token.
    /// </summary>
    required public string SlackAuthToken { get; init; }

    /// <summary>
    /// Slack signing secret.
    /// </summary>
    required public string SlackSigningSecret { get; init; }

    /// <summary>
    /// Crm url.
    /// </summary>
    required public string CrmUrl { get; init; }
}
