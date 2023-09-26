namespace Saritasa.SlackApp.UseCases.Slack.GetNonBillableDays.Dto;

/// <summary>
/// User month dto.
/// </summary>
public record UserMonthDto
{
    /// <summary>
    /// Days.
    /// </summary>
    public IReadOnlyCollection<UserDayDto> LastWorkMonth { get; } = new List<UserDayDto>();
}
