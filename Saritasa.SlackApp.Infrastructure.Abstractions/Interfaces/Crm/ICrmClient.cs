using Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces.Crm.Dto;
using Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces.Crm.Dto.Planner;

namespace Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces.Crm;

/// <summary>
/// Interface crm client.
/// </summary>
public interface ICrmClient
{
    /// <summary>
    /// User data retrieval method.
    /// </summary>
    /// <param name="email">Email.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Crm user dto.</returns>
    Task<CrmUserDto> GetUserAsync(string email, CancellationToken cancellationToken);

    /// <summary>
    /// Method of obtaining user daily data.
    /// </summary>
    /// <param name="userId">User id.</param>
    /// <param name="dateFrom">Date from.</param>
    /// <param name="dateTo">Date to.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Crm daily status dto.</returns>
    Task<CrmDailyStatusDto> GetDailyStatsAsync(int userId, DateOnly dateFrom, DateOnly dateTo, CancellationToken cancellationToken);

    /// <summary>
    /// Get planner data for user.
    /// </summary>
    /// <param name="userId">User id.</param>
    /// <param name="dateFrom">Date from.</param>
    /// <param name="dateTo">Date to.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task<PlannerDto> GetPlannerAsync(int userId, DateOnly dateFrom, DateOnly dateTo, CancellationToken cancellationToken);
}
