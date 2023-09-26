using AutoMapper;
using Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces.Slack.HomeDtos;
using Saritasa.SlackApp.Web.Controllers.Dtos;
using Saritasa.SlackApp.Web.Controllers.Enums;

namespace Saritasa.SlackApp.Web.Controllers.Mappers;

/// <summary>
/// Mapping HomePayloadDto to AppNotificationCommand.
/// </summary>
public class UserNotificationsMappingProfile : Profile
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public UserNotificationsMappingProfile()
    {
        CreateMap<HomePagePayloadDto, UserNotifications>()
            .ForMember(dst => dst.UserId, opt => opt.MapFrom(src => src.User.Id))
            .ForMember(dst => dst.Command,
                opt => opt.MapFrom(src => Enum.Parse<Command>(src.View.State.Values.SelectCommand.SelectCommandValue.SelectedOption.Value)))
            .ForMember(dst => dst.Button,
                opt => opt.MapFrom(src => Enum.Parse<HomeButton>(src.Actions.ActionId)))
            .ForMember(dst => dst.NoticeTime,
                opt => opt.MapFrom(src =>
                    TimeOnly.Parse(src.View.State.Values.Timepicker.TimepickerValue.SelectedTime)))
            .ForMember(dst => dst.NoticeDays, opt => opt.MapFrom(src => src.View.State.Values.CheckboxesWeekdays
                .CheckboxesWeekdaysValue.SelectedOption
                .Select(weekDay => Enum.Parse<DayOfWeek>(weekDay.Value))
                .ToList()));
    }
}
