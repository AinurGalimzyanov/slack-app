using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Saritasa.SlackApp.Infrastructure.Abstractions.Converters;
using Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces.ModelBinders;

namespace Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces.Slack.HomeDtos;

/// <summary>
/// Payloads that come from the home page.
/// </summary>
[ModelBinder(BinderType = typeof(PayloadModelBinder<HomePagePayloadDto>))]
public record HomePagePayloadDto
{
    /// <summary>
    /// User.
    /// </summary>
    required public UserDto User { get; init; }

    /// <summary>
    /// View.
    /// </summary>
    required public ViewDto View { get; init; }

    /// <summary>
    /// Action.
    /// </summary>
    [JsonConverter(typeof(FirstElementArrayConverter<ActionDto>))]
    required public ActionDto Actions { get; init; }
}
