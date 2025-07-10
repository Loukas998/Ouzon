using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Users.Commands;
using Template.Application.Users.Dtos;
using Template.Domain.Entities;

namespace Template.Application.Users.UserProfile;

public class UserProfile:Profile
{
    public UserProfile()
    {
        CreateMap<RegisterUserCommand, User>();
        CreateMap<User, UserDto>().ReverseMap();
       
    }
    
}
