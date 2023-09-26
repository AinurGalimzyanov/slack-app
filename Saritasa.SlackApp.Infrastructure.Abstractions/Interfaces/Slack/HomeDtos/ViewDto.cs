namespace Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces.Slack.HomeDtos;

/// <summary>
/// View.
/// </summary>
public record ViewDto
{
    /// <summary>
    /// State.
    /// </summary>
    required public StateDto State { get; init; }
}
