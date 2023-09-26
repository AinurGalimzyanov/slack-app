using MediatR;
using Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces.Slack;
using Saritasa.SlackApp.UseCases.Slack.GetNonBillableDays;
using Saritasa.SlackApp.UseCases.Slack.GetPlannerQuery;
using Saritasa.SlackApp.Web.Converters;
using Saritasa.Tools.Domain.Exceptions;
using SlackNet.WebApi;

namespace Saritasa.SlackApp.Web.BackgroundJobRunner;

/// <summary>
/// Background job runner.
/// </summary>
public class BackgroundSlackRunner
{
    private readonly IMediator mediator;
    private readonly ISlackMessageSender slackMessageSender;
    private readonly ILogger<BackgroundSlackRunner> logger;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="mediator">Mediator.</param>
    /// <param name="slackMessageSender">Slack message sender.</param>
    /// <param name="logger">Logger.</param>
    public BackgroundSlackRunner(IMediator mediator, ISlackMessageSender slackMessageSender, ILogger<BackgroundSlackRunner> logger)
    {
        this.mediator = mediator;
        this.slackMessageSender = slackMessageSender;
        this.logger = logger;
    }

    /// <summary>
    /// Executes background job.
    /// </summary>
    public async Task Execute(GetPlannerQuery getPlannerQuery, CancellationToken cancellationToken)
    {
        var message = new Message()
        {
            Channel = getPlannerQuery.UserId
        };
        try
        {
            var result = await mediator.Send(getPlannerQuery, cancellationToken);
            message.Text = new DtoToStringConverter().ConvertToString(result);
        }
        catch (DomainException domainException)
        {
            logger.LogError(domainException, "Something went wrong!");
            message.Text = domainException.Message;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Something went wrong!");
            message.Text = "Something went wrong. Try again later.";
        }
        finally
        {
            await slackMessageSender.SendMessageAsync(message, cancellationToken);
        }
    }

    /// <summary>
    /// Executes background job.
    /// </summary>
    public async Task Execute(GetNonBillableDaysQuery getNonBillableDaysQuery, CancellationToken cancellationToken)
    {
        var message = new Message()
        {
            // So the message is sent to the user personally..
            Channel = getNonBillableDaysQuery.UserId
        };
        try
        {
            var result = await mediator.Send(getNonBillableDaysQuery, cancellationToken);
            message.Text = new DtoToStringConverter().MapUserDaysToString(result);
        }
        catch (DomainException domainException)
        {
            logger.LogError(domainException, domainException.Message);
            message.Text = domainException.Message;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Something went wrong!");
            message.Text = "Something went wrong. Try again later.";
        }
        finally
        {
            await slackMessageSender.SendMessageAsync(message, cancellationToken);
        }
    }
}
