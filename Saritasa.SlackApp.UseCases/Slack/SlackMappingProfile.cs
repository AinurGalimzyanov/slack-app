using AutoMapper;
using Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces.Crm.Dto;
using Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces.Crm.Dto.Planner;
using Saritasa.SlackApp.UseCases.Slack.GetNonBillableDays.Dto;
using Saritasa.SlackApp.UseCases.Slack.GetPlannerQuery;

namespace Saritasa.SlackApp.UseCases.Slack;

/// <summary>
/// Slack mapping profile.
/// </summary>
public class SlackMappingProfile : Profile
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public SlackMappingProfile()
    {
        CreateMap<AssignedProjectDto, ProjectDto>();
        CreateMap<PlannerDto, GetPlannerQueryResultDto>()
            .ForMember(dto => dto.ProjectDtos, expression => expression.MapFrom(plannerDto => plannerDto.AssignedProjects));

        CreateMap<CrmDailyStatusDto, UserMonthDto>()
            .ForMember(dst => dst.LastWorkMonth, opt => opt.MapFrom(src => src.Data));
        CreateMap<DailyStatusData, UserDayDto>()
            .ForMember(dst => dst.Capacity, opt => opt.MapFrom(src => TimeSpan.FromHours(src.CapacityHours)))
            .ForMember(dst => dst.Duration, opt => opt.MapFrom(src => TimeSpan.FromMinutes(src.SpentMinutes)))
            .ForMember(dst => dst.Date, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.Date)));
    }
}
