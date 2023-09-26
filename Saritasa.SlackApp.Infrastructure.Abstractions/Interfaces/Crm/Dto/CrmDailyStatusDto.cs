namespace Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces.Crm.Dto;

/// <summary>
/// Data about user daily.
/// </summary>
public class CrmDailyStatusDto
{
    /// <summary>
    /// User data.
    /// </summary>
    public IReadOnlyCollection<DailyStatusData> Data { get; init; } = new List<DailyStatusData>();
}

/// <summary>
/// Same-day data.
/// </summary>
/// <param name="Date">Date.</param>
/// <param name="SpentMinutes">Spent user minutes.</param>
/// <param name="CapacityHours">Capacity hours.</param>
public record DailyStatusData(
    DateTime Date,
    double SpentMinutes,
    double CapacityHours
);
