using Newtonsoft.Json;

namespace Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces.Slack.HomeDtos;

/// <summary>
/// Timepicker.
/// </summary>
public record TimepickerDto
{
    /// <summary>
    /// Selected time.
    /// </summary>
    [JsonProperty("selected_time")]
    required public string SelectedTime { get; init; }
}
