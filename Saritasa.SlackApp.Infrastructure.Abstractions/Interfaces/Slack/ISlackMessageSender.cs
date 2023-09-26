using SlackNet.WebApi;

namespace Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces.Slack;

/// <summary>
/// Message sender for slack app.
/// </summary>
public interface ISlackMessageSender
{
    /// <summary>
    /// Send message to slack app.
    /// </summary>
    /// <param name="message">Message.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task SendMessageAsync(Message message, CancellationToken cancellationToken);
}
