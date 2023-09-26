using Newtonsoft.Json;

namespace Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces.Slack.HomeDtos;

/// <summary>
/// Checkboxes weekdays.
/// </summary>
public record CheckboxesWeekdaysDto
{
    /// <summary>
    /// Selected options.
    /// </summary>
    [JsonProperty("selected_options")]
    required public IReadOnlyCollection<SelectedOptionDto> SelectedOption { get; init; } = new List<SelectedOptionDto>();
}
