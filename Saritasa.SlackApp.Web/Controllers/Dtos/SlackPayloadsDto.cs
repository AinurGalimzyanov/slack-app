using Microsoft.AspNetCore.Mvc;

namespace Saritasa.SlackApp.Web.Controllers.Dtos;

/// <summary>
/// Payloads slack dto.
/// </summary>
public record SlackPayloadsDto
{
    /// <summary>
    /// User id.
    /// </summary>
    [BindProperty(Name = "user_id")]
    required public string UserId { get; init; }

    /// <summary>
    /// Text.
    /// </summary>
    [BindProperty(Name = "text")]
    public string? Text { get; init; }

    /// <summary>
    /// Channel id.
    /// </summary>
    [BindProperty(Name = "channel_id")]
    required public string ChannelId { get; init; }
}
