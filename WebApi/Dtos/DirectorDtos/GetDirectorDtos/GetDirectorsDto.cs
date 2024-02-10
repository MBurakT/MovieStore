using System.Collections.Generic;

namespace WebApi.Dtos.DirectorDtos.GetDirectorDtos;

public class GetDirectorsDto
{
    public List<GetDirectorDto>? Directors { get; set; }
}