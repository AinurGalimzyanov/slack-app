using Newtonsoft.Json;

namespace Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces.Slack.HomeDtos;

/// <summary>
/// Select command.
/// </summary>
public record SelectCommand
{
    /// <summary>
    /// Select command.
    /// </summary>
    [JsonProperty("select_command")]
    required public SelectCommandDto SelectCommandValue { get; init; }
}
