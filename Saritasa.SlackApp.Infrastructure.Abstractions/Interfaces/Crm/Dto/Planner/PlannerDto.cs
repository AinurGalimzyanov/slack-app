namespace Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces.Crm.Dto.Planner;

/// <summary>
/// Planner dto.
/// </summary>
public class PlannerDto
{
    /// <summary>
    /// Assigned project dto collection.
    /// </summary>
    required public IReadOnlyCollection<AssignedProjectDto> AssignedProjects { get; init; } = new List<AssignedProjectDto>();
}
