using Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces.Slack;
using SlackNet;
using SlackNet.WebApi;

namespace Saritasa.SlackApp.Infrastructure;

/// <inheritdoc />
public class SlackMessageSender : ISlackMessageSender
{
    private readonly ISlackApiClient slackApiClient;

    /// <summary>
    /// Constructor.
    /// </summary>
    public SlackMessageSender(ISlackApiClient slackApiClient)
    {
        this.slackApiClient = slackApiClient;
    }

    /// <inheritdoc />
    public async Task SendMessageAsync(Message message, CancellationToken cancellationToken)
    {
        await slackApiClient.Chat.PostMessage(message, cancellationToken);
    }
}
