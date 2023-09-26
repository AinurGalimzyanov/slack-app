using SlackNet;

namespace Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces.Slack;

/// <summary>
/// Page getter for slack home.
/// </summary>
public interface ISlackViewService
{
    /// <summary>
    /// Get home page.
    /// </summary>
    /// <returns>HomeViewDefinition.</returns>
    HomeViewDefinition GetHomePage();
}
