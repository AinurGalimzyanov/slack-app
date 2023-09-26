using Newtonsoft.Json;

namespace Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces.Slack.HomeDtos;

/// <summary>
/// Actions.
/// </summary>
public record ActionDto
{
    /// <summary>
    /// Id.
    /// </summary>
    [JsonProperty("action_id")]
    required public string ActionId { get; init; }
}
