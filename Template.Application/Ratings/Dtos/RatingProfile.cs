using AutoMapper;
using Template.Application.Ratings.Commands.Rate;
using Template.Domain.Entities.Users;

namespace Template.Application.Ratings.Dtos;

public class RatingProfile : Profile
{
    public RatingProfile()
    {
        CreateMap<Rating, RatingsDto>()
            .ForMember(r => r.DoctorName, opt => opt.Ignore());

        CreateMap<AddRatingToAssistantCommand, Rating>();
    }
}
