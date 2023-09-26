using Newtonsoft.Json;

namespace Saritasa.SlackApp.Infrastructure.Dtos.Planner;

/// <summary>
/// Crm planner resource.
/// </summary>
public class CrmPlannerResource
{
    /// <summary>
    /// Project name.
    /// </summary>
    [JsonProperty("projectName")]
    required public string ProjectName { get; init; }

    /// <summary>
    /// Assigned hours.
    /// </summary>
    [JsonProperty("assignedHours")]
    required public int AssignedHours { get; init; }

    /// <summary>
    /// Spent minutes.
    /// </summary>
    [JsonProperty("spentMinutes")]
    required public int SpentMinutes { get; init; }

    /// <summary>
    /// Edited at.
    /// </summary>
    [JsonProperty("editedAt")]
    required public DateTime? EditedAt { get; init; }
}
