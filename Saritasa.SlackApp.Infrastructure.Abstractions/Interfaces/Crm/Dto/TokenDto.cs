using System.Text.Json.Serialization;

namespace Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces.Crm.Dto;

/// <summary>
/// Token dto.
/// </summary>
public class TokenDto
{
    /// <summary>
    /// Access token.
    /// </summary>
    [JsonPropertyName("access_token")]
    required public string AccessToken { get; init; }
}
