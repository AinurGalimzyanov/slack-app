using Newtonsoft.Json;

namespace Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces.Slack.HomeDtos;

/// <summary>
/// Values.
/// </summary>
public record ValuesDto
{
    /// <summary>
    /// Timepicker.
    /// </summary>
    [JsonProperty("timepicker")]
    required public Timepicker Timepicker { get; init; }

    /// <summary>
    /// Select events.
    /// </summary>
    [JsonProperty("select_command")]
    required public SelectCommand SelectCommand { get; init; }

    /// <summary>
    /// Checkboxes weekdays.
    /// </summary>
    [JsonProperty("checkboxes_weekdays")]
    required public CheckboxesWeekdays CheckboxesWeekdays { get; init; }
}
