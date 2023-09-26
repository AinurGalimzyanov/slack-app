namespace Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces.Slack.HomeDtos;

/// <summary>
/// User.
/// </summary>
public record UserDto
{
    /// <summary>
    /// Id.
    /// </summary>
    required public string Id { get; init; }

    /// <summary>
    /// Name.
    /// </summary>
    required public string Name { get; init; }
}
