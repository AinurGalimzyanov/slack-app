using Newtonsoft.Json;

namespace Saritasa.SlackApp.Infrastructure.Dtos.Planner;

/// <summary>
/// Crm planner data.
/// </summary>
public class CrmPlannerData
{
    /// <summary>
    /// Crm planner resources collection.
    /// </summary>
    [JsonProperty("resources")]
    public IReadOnlyCollection<CrmPlannerResource> CrmPlannerResourceCollection { get; init; } = new List<CrmPlannerResource>();
}
