namespace Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces.Slack.HomeDtos;

/// <summary>
/// Selected option.
/// </summary>
public record SelectedOptionDto
{
    /// <summary>
    /// Value.
    /// </summary>
    required public string Value { get; init; }
}
