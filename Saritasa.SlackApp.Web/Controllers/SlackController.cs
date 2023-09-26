using AutoMapper;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces.Slack.HomeDtos;
using Saritasa.SlackApp.UseCases.Slack.GetNonBillableDays;
using Saritasa.SlackApp.UseCases.Slack.GetPlannerQuery;
using Saritasa.SlackApp.UseCases.Slack.SlackEventCommand;
using Saritasa.SlackApp.Web.BackgroundJobRunner;
using Saritasa.SlackApp.Web.Controllers.Dtos;
using Saritasa.SlackApp.Web.Controllers.Enums;

namespace Saritasa.SlackApp.Web.Controllers;

/// <summary>
/// Slack api.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SlackController : ControllerBase
{
    private readonly BackgroundSlackRunner backgroundSlackRunner;
    private readonly IMediator mediator;
    private readonly IMapper mapper;
    private readonly ILogger<SlackController> logger;

    /// <summary>
    /// Constructor.
    /// </summary>
    public SlackController(BackgroundSlackRunner backgroundSlackRunner, IMapper mapper, IMediator mediator, ILogger<SlackController> logger)
    {
        this.backgroundSlackRunner = backgroundSlackRunner;
        this.mapper = mapper;
        this.mediator = mediator;
        this.logger = logger;
    }

    /// <summary>
    /// Check jobs.
    /// </summary>
    /// <param name="payloadsDto">Payloads slack dto.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>IActionResult.</returns>
    [HttpPost("checkjobs")]
    public IActionResult CheckJobs([FromForm] SlackPayloadsDto payloadsDto, CancellationToken cancellationToken)
    {
        var query = mapper.Map<GetNonBillableDaysQuery>(payloadsDto);
        BackgroundJob.Enqueue(() => backgroundSlackRunner.Execute(query, cancellationToken));

        return Accepted();
    }

    /// <summary>
    /// Planner command action.
    /// </summary>
    /// <param name="plannerInputDto">Planner input dto.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>IActionResult.</returns>
    [HttpPost("planner")]
    public IActionResult PlannerCommand([FromForm] PlannerInputDto plannerInputDto, CancellationToken cancellationToken)
    {
        var getPlannerQuery = new GetPlannerQuery
        {
            UserId = plannerInputDto.UserId,
            Email = plannerInputDto.Email
        };
        BackgroundJob.Enqueue(() => backgroundSlackRunner.Execute(getPlannerQuery, cancellationToken));
        return Accepted();
    }

    /// <summary>
    /// Events that take place on the home page.
    /// </summary>
    [HttpPost("homeEvents")]
    public void HomeEvents([FromForm] HomePagePayloadDto payload)
    {
        var userNotifications = mapper.Map<UserNotifications>(payload);

        if (userNotifications.Command == Command.CheckJobs)
        {
            var query = new GetNonBillableDaysQuery { UserId = userNotifications.UserId };
            ReactToButton(userNotifications.Button, query, userNotifications);
        }
        else if (userNotifications.Command == Command.Planner)
        {
            var query = new GetPlannerQuery { UserId = userNotifications.UserId };
            ReactToButton(userNotifications.Button, query, userNotifications);
        }
    }

    private void ReactToButton<T>(HomeButton currentButton, T query, UserNotifications userNotifications)
    {
        // TODO: This is needed for the subsequent logic of notifications in this method.
        switch (currentButton)
        {
            case HomeButton.DailyButton:
                break;
            case HomeButton.SpecificDaysButton:
                break;
            case HomeButton.UnsubscribeButton:
                break;
            default:
                logger.LogWarning($"This button is not handled {currentButton}.");
                throw new ArgumentOutOfRangeException(nameof(currentButton), currentButton,
                    "This button is not handled.");
        }
    }

    /// <summary>
    /// Events in slack app.
    /// </summary>
    /// <param name="eventDto">Current event.</param>
    [HttpPost("events")]
    public async Task<IActionResult> Events([FromBody] EventDto eventDto)
    {
        if (eventDto.Event == null)
        {
            // This is to verify the endpoint in slack.
            return Ok(eventDto);
        }

        await mediator.Send(new SlackEventCommand()
        {
            Event = eventDto.Event,
            EventType = EventDto.GetType(eventDto.Event)
        });
        return Ok();
    }
}
