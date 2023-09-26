namespace Saritasa.SlackApp.UseCases.Slack.GetPlannerQuery;

/// <summary>
/// ProjectDto.
/// </summary>
public class ProjectDto
{
    /// <summary>
    /// ProjectDto name.
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
