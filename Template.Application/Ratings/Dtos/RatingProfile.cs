using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Ratings.Commands.Update;
using Template.Domain.Entities.Users;

namespace Template.Application.Ratings.Dtos;

public class RatingProfile :Profile
{
    public RatingProfile()
    {
        CreateMap<Rating, AddRatingToProcedureCommand>().ReverseMap();
    }
}
