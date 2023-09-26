using Newtonsoft.Json;
using Saritasa.SlackApp.Infrastructure.Abstractions.Converters;

namespace Saritasa.SlackApp.Infrastructure.Dtos.Planner;

/// <summary>
/// Crm planner dto.
/// </summary>
public class CrmPlannerDto
{
    /// <summary>
    /// Crm planner data collection.
    /// </summary>
    [JsonProperty("data")]
    [JsonConverter(typeof(FirstElementArrayConverter<CrmPlannerData>))]
    public CrmPlannerData CrmPlannerData { get; init; } = new CrmPlannerData();
}
