using Saritasa.SlackApp.Web.Controllers.Enums;

namespace Saritasa.SlackApp.Web.Controllers.Dtos;

/// <summary>
/// User-selected notification configurations.
/// </summary>
public record UserNotifications
{
    /// <summary>
    /// User id.
    /// </summary>
    required public string UserId { get; init; }

    /// <summary>
    /// Command.
    /// </summary>
    required public Command Command { get; init; }

    /// <summary>
    /// Button.
    /// </summary>
    required public HomeButton Button { get; init; }

    /// <summary>
    /// Notice time.
    /// </summary>
    required public TimeOnly NoticeTime { get; init; }

    /// <summary>
    /// Notice days.
    /// </summary>
    public IReadOnlyCollection<DayOfWeek> NoticeDays { get; init; } = new List<DayOfWeek>();
}
