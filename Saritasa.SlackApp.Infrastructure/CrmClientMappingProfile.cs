using AutoMapper;
using Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces.Crm.Dto.Planner;
using Saritasa.SlackApp.Infrastructure.Dtos.Planner;

namespace Saritasa.SlackApp.Infrastructure;

/// <summary>
/// Crm client mapping profile.
/// </summary>
public class CrmClientMappingProfile : Profile
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public CrmClientMappingProfile()
    {
        CreateMap<CrmPlannerResource, AssignedProjectDto>()
            .ForMember(assignedProjectDto => assignedProjectDto.TimeSpent,
                expression => expression.MapFrom(resource => TimeSpan.FromMinutes(resource.SpentMinutes)))
            .ForMember(assignedProjectDto => assignedProjectDto.AssignedTime,
                expression => expression.MapFrom(resource => TimeSpan.FromHours(resource.AssignedHours)));
        CreateMap<CrmPlannerDto, PlannerDto>()
            .ForMember(plannerDto => plannerDto.AssignedProjects,
                expression => expression.MapFrom(crmPlannerDto => crmPlannerDto.CrmPlannerData.CrmPlannerResourceCollection));
    }
}
