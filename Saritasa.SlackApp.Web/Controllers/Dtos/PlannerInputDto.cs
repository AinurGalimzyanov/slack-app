using Microsoft.AspNetCore.Mvc;

namespace Saritasa.SlackApp.Web.Controllers.Dtos;

/// <summary>
/// Planner input dto.
/// </summary>
public class PlannerInputDto
{
    /// <summary>
    /// Email.
    /// </summary>
    [BindProperty(Name = "text")]
    public string? Email { get; init; }

    /// <summary>
    /// User id in slack.
    /// </summary>
    [BindProperty(Name = "user_id")]
    required public string UserId { get; init; }
}
