using AutoMapper;
using Saritasa.SlackApp.UseCases.Slack.GetNonBillableDays;
using Saritasa.SlackApp.Web.Controllers.Dtos;

namespace Saritasa.SlackApp.Web.Controllers.Mappers;

/// <summary>
/// Mapping Request to GetNonBillableDaysQuery.
/// </summary>
public class PayloadDtoMappingProfile : Profile
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public PayloadDtoMappingProfile()
    {
        CreateMap<SlackPayloadsDto, GetNonBillableDaysQuery>()
            .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Text))
            .ForMember(dst => dst.UserId, opt => opt.MapFrom(src => src.UserId));
    }
}
