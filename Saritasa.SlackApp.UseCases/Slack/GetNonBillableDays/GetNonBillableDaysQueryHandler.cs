using AutoMapper;
using MediatR;
using Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces.Crm;
using Saritasa.SlackApp.UseCases.Slack.GetNonBillableDays.Dto;
using SlackNet;

namespace Saritasa.SlackApp.UseCases.Slack.GetNonBillableDays;

/// <summary>
///  Output of all user non-billable days.
/// </summary>
public class GetNonBillableDaysQueryHandler : IRequestHandler<GetNonBillableDaysQuery, IReadOnlyCollection<UserDayDto>>
{
    private static readonly DateOnly DateNow = DateOnly.FromDateTime(DateTime.UtcNow);
    private static readonly DateOnly DateMonthAgo = DateNow.AddMonths(-1);
    private readonly ICrmClient crmClient;
    private readonly IMapper mapper;
    private readonly ISlackApiClient slackClient;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetNonBillableDaysQueryHandler(ICrmClient crmClient, IMapper mapper, ISlackApiClient slackClient)
    {
        this.crmClient = crmClient;
        this.mapper = mapper;
        this.slackClient = slackClient;
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyCollection<UserDayDto>> Handle(GetNonBillableDaysQuery request, CancellationToken cancellationToken)
    {
        var slackUser = await slackClient.Users.Info(request.UserId, false, cancellationToken);
        if (slackUser == null)
        {
            throw new ArgumentException("Something went wrong when requesting user information.", nameof(request.UserId));
        }

        var email = string.IsNullOrEmpty(request.Email) ? slackUser.Profile.Email : request.Email;

        var crmUser = await crmClient.GetUserAsync(email, cancellationToken);
        if (crmUser.Data == null)
        {
            throw new ArgumentException("Something went wrong when requesting a user from crm.", nameof(email));
        }

        var dailyStats = await crmClient.GetDailyStatsAsync(crmUser.Data.UserId, DateMonthAgo, DateNow, cancellationToken);

        var lastWorkDays = mapper.Map<UserMonthDto>(dailyStats);

        var nonBillableDays = lastWorkDays.LastWorkMonth
            .Where(day => day.Capacity > day.Duration)
            .ToList();

        return nonBillableDays;
    }
}
