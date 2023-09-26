namespace Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces.Slack.HomeDtos;

/// <summary>
/// State.
/// </summary>
public record StateDto
{
    /// <summary>
    /// Values.
    /// </summary>
    required public ValuesDto Values { get; init; }
}
