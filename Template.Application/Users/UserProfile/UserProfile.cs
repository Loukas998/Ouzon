using AutoMapper;
using Template.Application.Users.Commands;
using Template.Application.Users.Dtos;
using Template.Domain.Entities;
using Template.Domain.Entities.Users;

namespace Template.Application.Users.UserProfile;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegisterUserCommand, User>();
        CreateMap<Clinic, ClinicDto>().ReverseMap();
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Clinic, opt => opt.MapFrom(src => src.Clinic))
            .ForMember(dest => dest.Rate, opt => opt.Ignore())
            .ForMember(dest => dest.ProfileImagePath, opt => opt.MapFrom<UserImageUrlResolver>())
            .ReverseMap();


        CreateMap<User, UserDetailedDto>()
            .ForMember(dest => dest.Holidays, opt => opt.MapFrom(src => src.Holidays))
            .ForMember(dest => dest.InProcedure, opt => opt.MapFrom(src => src.InProcedure.Select(pro => pro.Procedure)))
            .ForMember(dest => dest.ProcedureFrom, opt => opt.MapFrom(src => src.ProcedureFrom))
            .ForMember(dest => dest.Devices, opt => opt.MapFrom(src => src.Devices))
            .ForMember(dest => dest.Rate, opt => opt.Ignore())
            .ForMember(dest => dest.ProfileImagePath, opt => opt.MapFrom<DetailedUserImageUrlResolver>())
            .ReverseMap();

        CreateMap<(User user, string roleName), UserDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.user.Id))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.user.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.user.PhoneNumber))
            .ForMember(dest => dest.Clinic, opt => opt.MapFrom(src => src.user.Clinic))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.roleName));
    }

}
