using Newtonsoft.Json;
using SlackNet.Events;

namespace Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces.Slack.Events;

/// <summary>
/// App home opened event.
/// </summary>
public class AppHomeOpened
{
    /// <summary>
    /// User id.
    /// </summary>
    [JsonProperty("user")]
    required public string UserId { get; init; }

    /// <summary>
    /// Tab.
    /// </summary>
    required public AppHomeTab Tab { get; init; }
}
