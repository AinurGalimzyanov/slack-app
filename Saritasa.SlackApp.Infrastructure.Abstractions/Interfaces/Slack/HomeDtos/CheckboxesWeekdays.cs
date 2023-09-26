using Newtonsoft.Json;

namespace Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces.Slack.HomeDtos;

/// <summary>
/// Checkboxes weekdays.
/// </summary>
public record CheckboxesWeekdays
{
    /// <summary>
    /// Checkboxes weekdays.
    /// </summary>
    [JsonProperty("checkboxes_weekdays")]
    required public CheckboxesWeekdaysDto CheckboxesWeekdaysValue { get; init; }
}
