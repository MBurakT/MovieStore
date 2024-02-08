using System.Collections.Generic;

namespace WebApi.Models.MovieDtos.GetMovieDtos;

public class GetMoviesDto
{
    public List<GetMovieDto>? Movies { get; set; }
}