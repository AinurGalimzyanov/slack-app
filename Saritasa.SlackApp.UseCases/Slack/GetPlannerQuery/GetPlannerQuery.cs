using MediatR;

namespace Saritasa.SlackApp.UseCases.Slack.GetPlannerQuery;

/// <summary>
/// Get planner query.
/// </summary>
public class GetPlannerQuery : IRequest<GetPlannerQueryResultDto>
{
    /// <summary>
    /// User email to request planner data.
    /// </summary>
    public string? Email { get; init; }

    /// <summary>
    /// User id in slack.
    /// </summary>
    required public string UserId { get; init; }
}
