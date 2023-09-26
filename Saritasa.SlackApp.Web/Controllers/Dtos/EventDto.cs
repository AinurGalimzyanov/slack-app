using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Saritasa.Tools.Domain.Exceptions;
using SlackNet.Events;

namespace Saritasa.SlackApp.Web.Controllers.Dtos;

/// <summary>
/// Event dto.
/// </summary>
public record EventDto
{
    /// <summary>
    /// Type.
    /// </summary>
    required public string? Type { get; init; }

    /// <summary>
    /// Challenge.
    /// </summary>
    public string? Challenge { get; init; }

    /// <summary>
    /// Token.
    /// </summary>
    required public string? Token { get; init; }

    /// <summary>
    /// Event.
    /// </summary>
    public JsonObject? Event { get; init; }

    /// <summary>
    /// Get the event type.
    /// </summary>
    /// <param name="e">Event.</param>
    /// <returns>The event type.</returns>
    public static string GetType(JsonObject? e)
    {
        var eventType = e.Deserialize<EventType>();
        if (eventType == null)
        {
            throw new ArgumentException(
                $"Something went wrong with the deserialization in the {typeof(EventType)}");
        }

        return eventType.Type;
    }
}

/// <summary>
/// Event type.
/// </summary>
public record EventType
{
    /// <summary>
    /// Type.
    /// </summary>
    [JsonPropertyName("type")]
    required public string Type { get; init; }
}
