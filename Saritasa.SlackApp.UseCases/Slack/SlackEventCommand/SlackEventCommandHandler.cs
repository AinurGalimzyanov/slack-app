using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces.Slack;
using SlackNet.Events;
using AppHomeOpened = Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces.Slack.Events.AppHomeOpened;

namespace Saritasa.SlackApp.UseCases.Slack.SlackEventCommand;

/// <summary>
/// Publish slack app page.
/// </summary>
public class SlackEventCommandHandler : IRequestHandler<SlackEventCommand>
{
    private readonly ISlackViewPublisher slackViewPublisher;
    private readonly ISlackViewService slackViewService;
    private readonly ILogger<SlackEventCommandHandler> logger;

    /// <summary>
    /// Constructor.
    /// </summary>
    public SlackEventCommandHandler(ISlackViewPublisher slackViewPublisher, ISlackViewService slackViewService, ILogger<SlackEventCommandHandler> logger)
    {
        this.slackViewPublisher = slackViewPublisher;
        this.slackViewService = slackViewService;
        this.logger = logger;
    }

    /// <inheritdoc />
    public async Task Handle(SlackEventCommand request, CancellationToken cancellationToken)
    {
        switch (request.EventType)
        {
            case SlackEventTypes.HomePageOpened:
                var appHomeOpened = JsonConvert.DeserializeObject<AppHomeOpened>(request.Event.ToJsonString());
                if (appHomeOpened!.Tab == AppHomeTab.Home)
                {
                    var homePage = slackViewService.GetHomePage();
                    await slackViewPublisher.PublishViewAsync(appHomeOpened.UserId, homePage, cancellationToken);
                }
                break;
            default:
                logger.LogWarning($"This event is not handled. {request.EventType}");
                break;
        }
    }
}
