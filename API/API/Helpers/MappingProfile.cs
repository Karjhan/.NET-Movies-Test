using API.DTO;
using AutoMapper;
using Core.Entities;

namespace API.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Movie, MovieDTO>()
            .ForMember(destMember => destMember.Genres,
                options => options.MapFrom(source => source.Genres.Select(genre => genre.Name)))
            .ForMember(destMember => destMember.Actors,
                options => options.MapFrom(source => source.Actors.Select(actor => actor.Name)))
            .ForMember(destMember => destMember.CoverURL, options => options.MapFrom<MovieCoverURLProfile>());
    }
}