using Newtonsoft.Json;

namespace Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces.Slack.HomeDtos;

/// <summary>
/// Timepicker.
/// </summary>
public record Timepicker
{
    /// <summary>
    /// Timepicker.
    /// </summary>
    [JsonProperty("timepicker")]
    required public TimepickerDto TimepickerValue { get; init; }
}
