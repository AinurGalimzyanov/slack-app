namespace Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces.Crm.Options;

/// <summary>
/// Secrets for auth user.
/// </summary>
public class CrmSecretsOptions
{
    /// <summary>
    /// Password.
    /// </summary>
    required public string Password { get; init; }

    /// <summary>
    /// User name.
    /// </summary>
    required public string Username { get; init; }
}
