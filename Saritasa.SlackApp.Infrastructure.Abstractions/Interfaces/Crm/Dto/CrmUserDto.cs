using Newtonsoft.Json;
using Saritasa.SlackApp.Infrastructure.Abstractions.Converters;

namespace Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces.Crm.Dto;

/// <summary>
/// Data about user.
/// </summary>
public class CrmUserDto
{
    /// <summary>
    /// User data.
    /// </summary>
    [JsonConverter(typeof(FirstElementArrayConverter<UserData>))]
    public UserData? Data { get; init; }
}

/// <summary>
/// User data.
/// </summary>
/// <param name="UserId">User id.</param>
/// <param name="FirstName">First name.</param>
/// <param name="LastName">Last name.</param>
/// <param name="PrimaryEmail">Email.</param>
public record UserData(
    int UserId,
    string FirstName,
    string LastName,
    string PrimaryEmail
);
