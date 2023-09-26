using Newtonsoft.Json;

namespace Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces.Slack.HomeDtos;

/// <summary>
/// Select events dto.
/// </summary>
public record SelectCommandDto
{
    /// <summary>
    /// Selected options.
    /// </summary>
    [JsonProperty("selected_option")]
    required public SelectedOptionDto SelectedOption { get; init; }
}
