using SlackNet;

namespace Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces.Slack;

/// <summary>
/// View publisher for slack app.
/// </summary>
public interface ISlackViewPublisher
{
    /// <summary>
    /// Publish view for slack app.
    /// </summary>
    /// <param name="userId">User id.</param>
    /// <param name="viewDefinition">View definition.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task PublishViewAsync(string userId, HomeViewDefinition viewDefinition, CancellationToken cancellationToken);
}
