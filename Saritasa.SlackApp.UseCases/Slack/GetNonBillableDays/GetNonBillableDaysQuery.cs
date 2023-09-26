using MediatR;
using Saritasa.SlackApp.UseCases.Slack.GetNonBillableDays.Dto;

namespace Saritasa.SlackApp.UseCases.Slack.GetNonBillableDays;

/// <summary>
/// Query value for get non billable days.
/// </summary>
public record GetNonBillableDaysQuery : IRequest<IReadOnlyCollection<UserDayDto>>
{
    /// <summary>
    /// Email.
    /// </summary>
    public string? Email { get; init; }

    /// <summary>
    /// User id.
    /// </summary>
    required public string UserId { get; init; }
}
