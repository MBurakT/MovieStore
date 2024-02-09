using System.Collections.Generic;

namespace WebApi.Dtos.GenreDtos;

public class GetGenresDto
{
    public List<GetGenreDto>? Genres { get; set; }
}