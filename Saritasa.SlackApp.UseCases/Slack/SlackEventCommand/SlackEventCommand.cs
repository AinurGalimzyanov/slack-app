using System.Text.Json.Nodes;
using MediatR;

namespace Saritasa.SlackApp.UseCases.Slack.SlackEventCommand;

/// <summary>
/// Command for publish slack app page..
/// </summary>
public record SlackEventCommand : IRequest
{
    /// <summary>
    /// Event type.
    /// </summary>
    required public string EventType { get; init; }

    /// <summary>
    /// Event.
    /// </summary>
    required public JsonObject Event { get; init; }
}
