using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Dtos.DirectorDtos.GetDirectorDtos;
using WebApi.Entities;

namespace WebApi.ControllerOperations.DirectorOperations;

public class DirectorOperation
{
    readonly IMovieStoreDbContext _context;
    readonly IMapper _mapper;

    public DirectorOperation(IMovieStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public GetDirectorsDto GetDirectorsQuery()
    {
        IQueryable<Director> dbDirector = _context.Directors.AsNoTracking();

        if (!dbDirector.Any()) return new GetDirectorsDto();

        return new GetDirectorsDto { Directors = _mapper.Map<List<GetDirectorDto>>(dbDirector.Include(x => x.Movies.Where(y => !y.IsDeleted)).ToList()) };
    }

    public GetDirectorDto GetDirectorCommand(int id)
    {
        if (id < 1) throw new InvalidOperationException("Director does not exist!");

        IQueryable<Director> dbDirector = _context.Directors.AsNoTracking();

        if (!dbDirector.Any(x => x.Id == id)) throw new InvalidOperationException("Director does not exist!");

        return _mapper.Map<GetDirectorDto>(dbDirector.Include(x => x.Movies.Where(y => !y.IsDeleted)).Single(x => x.Id == id));
    }
}