namespace Saritasa.SlackApp.UseCases.Slack.GetPlannerQuery;

/// <summary>
/// Get planner query dto.
/// </summary>
public class GetPlannerQueryResultDto
{
    /// <summary>
    /// ProjectDto dtos.
    /// </summary>
    required public IReadOnlyCollection<ProjectDto> ProjectDtos { get; init; } = new List<ProjectDto>();
}
