namespace Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces.Crm.Dto.Planner;

/// <summary>
/// Assigned project dto.
/// </summary>
public class AssignedProjectDto
{
    /// <summary>
    /// Project name.
    /// </summary>
    required public string ProjectName { get; init; }

    /// <summary>
    /// Assigned hours.
    /// </summary>
    required public TimeSpan AssignedTime { get; init; }

    /// <summary>
    /// Spent minutes.
    /// </summary>
    required public TimeSpan TimeSpent { get; init; }

    /// <summary>
    /// Edited at.
    /// </summary>
    required public DateTime? EditedAt { get; init; }
}
