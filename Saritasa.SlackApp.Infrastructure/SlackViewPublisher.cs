using Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces.Slack;
using SlackNet;

namespace Saritasa.SlackApp.Infrastructure;

/// <inheritdoc />
public class SlackViewPublisher : ISlackViewPublisher
{
    private readonly ISlackApiClient slackApiClient;

    /// <summary>
    /// Constructor.
    /// </summary>
    public SlackViewPublisher(ISlackApiClient slackApiClient)
    {
        this.slackApiClient = slackApiClient;
    }

    /// <inheritdoc />
    public async Task PublishViewAsync(string userId, HomeViewDefinition viewDefinition, CancellationToken cancellationToken)
    {
         await slackApiClient.Views.Publish(userId, viewDefinition, cancellationToken: cancellationToken);
    }
}
