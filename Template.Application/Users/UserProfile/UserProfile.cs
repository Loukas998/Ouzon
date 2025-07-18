using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Users.Commands;
using Template.Application.Users.Dtos;
using Template.Domain.Entities;
using Template.Domain.Entities.Users;

namespace Template.Application.Users.UserProfile;

public class UserProfile:Profile
{
    public UserProfile()
    {
        CreateMap<RegisterUserCommand, User>();
        CreateMap<ClinicDto, Clinic>().ReverseMap();
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Clinic, opt => opt.MapFrom(src => src.Clinic))
            .ReverseMap();
        CreateMap<User, UserDetailedDto>()
            .ForMember(dest => dest.Holidays, opt => opt.MapFrom(src => src.Holidays))
            .ForMember(dest => dest.InProcedure, opt => opt.MapFrom(src => src.InProcedure.Select(pro=>pro.Procedure)))
            .ForMember(dest => dest.ProcedureFrom, opt => opt.MapFrom(src => src.ProcedureFrom))
            .ForMember(dest => dest.Devices, opt => opt.MapFrom(src => src.Devices)).ReverseMap();


    }
    
}
