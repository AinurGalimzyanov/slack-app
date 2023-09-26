using Microsoft.EntityFrameworkCore;
using Saritasa.SlackApp.Domain.Slack;

namespace Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces;

/// <summary>
/// Application abstraction for unit of work.
/// </summary>
public interface IAppDbContext : IDbContextWithSets, IDisposable
{
    /// <summary>
    /// List of registered chats.
    /// </summary>
    DbSet<Chat> Chats { get; }
}
