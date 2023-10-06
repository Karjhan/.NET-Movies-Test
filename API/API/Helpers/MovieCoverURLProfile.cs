using System.Text.RegularExpressions;
using API.DTO;
using AutoMapper;
using Core.Entities;

namespace API.Helpers;

public class MovieCoverURLProfile : IValueResolver<Movie, MovieDTO, string>
{
    private IConfiguration _configuration;

    public MovieCoverURLProfile(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Resolve(Movie source, MovieDTO destination, string destMember, ResolutionContext context)
    {
        if (!string.IsNullOrEmpty(source.CoverURL))
        {
            return _configuration["ApiURL"] + source.CoverURL;
        }
        return null;
    }
}