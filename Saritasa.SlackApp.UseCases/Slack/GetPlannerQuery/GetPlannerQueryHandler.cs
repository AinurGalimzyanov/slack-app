using AutoMapper;
using MediatR;
using Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces.Crm;
using Saritasa.Tools.Domain.Exceptions;
using SlackNet;

namespace Saritasa.SlackApp.UseCases.Slack.GetPlannerQuery;

/// <summary>
/// Handler for get planner query.
/// </summary>
public class GetPlannerQueryHandler : IRequestHandler<GetPlannerQuery, GetPlannerQueryResultDto>
{
    private readonly ICrmClient crmClient;
    private readonly ISlackApiClient slackApiClient;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="slackApiClient">Slack api client.</param>
    /// <param name="crmClient">Crm client.</param>
    /// <param name="mapper">Mapper.</param>
    public GetPlannerQueryHandler(ISlackApiClient slackApiClient, ICrmClient crmClient, IMapper mapper)
    {
        this.slackApiClient = slackApiClient;
        this.crmClient = crmClient;
        this.mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<GetPlannerQueryResultDto> Handle(GetPlannerQuery request, CancellationToken cancellationToken)
    {
        var userEmail = request.Email;
        if (string.IsNullOrEmpty(userEmail))
        {
            var currentSlackUser = await slackApiClient.Users.Info(request.UserId, false, cancellationToken);
            userEmail = currentSlackUser.Profile.Email;
        }

        var crmUser = await crmClient.GetUserAsync(userEmail, cancellationToken);

        if (crmUser.Data == null)
        {
            throw new NotFoundException("User with such email not found");
        }

        var slackUSer = await slackApiClient.Users.LookupByEmail(userEmail, cancellationToken);
        var userTimeZone = GetTimeZoneInfo(slackUSer.Tz);
        var dates = CalculateMondayAndSundayDates(userTimeZone);

        var plannerDto = await crmClient.GetPlannerAsync(crmUser.Data.UserId, dates.MondayDate, dates.SundayDate, cancellationToken);

        return mapper.Map<GetPlannerQueryResultDto>(plannerDto);
    }

    // Calculates first and last dates of current week.
    private (DateOnly MondayDate, DateOnly SundayDate) CalculateMondayAndSundayDates(TimeZoneInfo timeZoneInfo)
    {
        DateOnly mondayDate;
        DateOnly sundayDate;

        var dateTimeInTimeZone = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, timeZoneInfo.Id);
        var currentDate = DateOnly.FromDateTime(dateTimeInTimeZone);

        const int daysFromMondayToSunday = 6;
        if (currentDate.DayOfWeek == DayOfWeek.Sunday)
        {
            sundayDate = currentDate;
            mondayDate = sundayDate.AddDays(-daysFromMondayToSunday);
        }
        else
        {
            var daysAfterMonday = currentDate.DayOfWeek - DayOfWeek.Monday;
            mondayDate = currentDate.AddDays(-daysAfterMonday);
            sundayDate = mondayDate.AddDays(daysFromMondayToSunday);
        }

        return (MondayDate: mondayDate, SundayDate: sundayDate);
    }

    private TimeZoneInfo GetTimeZoneInfo(string timeZoneId)
    {
        return TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
    }
}
