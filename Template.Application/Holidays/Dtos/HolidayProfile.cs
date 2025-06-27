using AutoMapper;
using Template.Application.Holidays.Commands.ChangeStatus;
using Template.Application.Holidays.Commands.Create;
using Template.Domain.Entities.Schedule;

namespace Template.Application.Holidays.Dtos;

public class HolidayProfile : Profile
{
    public HolidayProfile()
    {
        CreateMap<Holiday, HolidayDto>();
        CreateMap<CreateHolidayCommand, Holiday>();
        CreateMap<ChangeHolidayStatusCommand, Holiday>();
    }
}
