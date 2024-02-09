using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Dtos.GenreDtos;
using WebApi.Entities;

namespace WebApi.ControllerOperations.GenreOperations;

public class GenreOperation
{
    readonly IMovieStoreDbContext _context;
    readonly IMapper _mapper;

    public GenreOperation(IMovieStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public GetGenresDto GetGenreQuery()
    {
        IQueryable<Genre> dbGenre = _context.Genres.AsNoTracking();

        if (!dbGenre.Any()) return new GetGenresDto();

        return new GetGenresDto { Genres = _mapper.Map<List<GetGenreDto>>(dbGenre.Include(x => x.Movies.Where(y => !y.IsDeleted))) };
    }

    public GetGenreDto GetGenreCommandById(int id)
    {
        if (id < 1) throw new InvalidOperationException("Genre does not exist!");

        IQueryable<Genre> dbGenre = _context.Genres.AsNoTracking();

        if (!dbGenre.Any(x => x.Id == id)) throw new InvalidOperationException("Genre does not exist!");

        return _mapper.Map<GetGenreDto>(dbGenre.Include(x => x.Movies.Where(y => !y.IsDeleted)).Single(x => x.Id == id));
    }
}