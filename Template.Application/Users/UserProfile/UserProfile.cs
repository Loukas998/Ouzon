using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Users.Commands;
using Template.Domain.Entities;

namespace Template.Application.Users.UserProfile;

public class UserProfile:Profile
{
    public UserProfile()
    {
        CreateMap<RegisterUserCommand, User>();
       
    }
    
}
