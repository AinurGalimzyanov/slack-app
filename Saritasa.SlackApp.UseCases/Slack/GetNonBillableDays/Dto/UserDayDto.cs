namespace Saritasa.SlackApp.UseCases.Slack.GetNonBillableDays.Dto;

/// <summary>
/// User day dto.
/// </summary>
public record UserDayDto
{
    /// <summary>
    /// Capacity.
    /// </summary>
    required public TimeSpan Capacity { get; init; }

    /// <summary>
    /// Duration of paid jobs.
    /// </summary>
    required public TimeSpan Duration { get; init; }

    /// <summary>
    /// Date.
    /// </summary>
    required public DateOnly Date { get; init; }
}
