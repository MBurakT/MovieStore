using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.Models.MovieDtos;

namespace WebApi.ControllerOperations.MovieOperations;

public class MovieOperation
{
    readonly IMovieStoreDbContext _context;
    readonly IMapper _mapper;

    public MovieOperation(IMovieStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public MoviesDto? GetMoviesQuery()
    {
        IQueryable<Movie> dbMovie = _context.Movies.AsNoTracking();

        if (!dbMovie.Any()) return null;

        var movies = dbMovie.Include(x => x.Genre).Include(x => x.Director).OrderBy(x => x.Name).ToList();
        var movieActors = _context.MovieActors;
        var actors = _context.Actors;

        movies.ForEach(x =>
            x.Actors = actors.Where(y => movieActors.Where(y => y.MovieId == x.Id).Select(y => y.ActorId).Contains(y.Id)).ToList());

        return new MoviesDto { Movies = _mapper.Map<List<MovieDto>>(movies) };
    }

    public MovieDto GetMovieCommandById(int id)
    {
        if (id < 1) throw new InvalidOperationException("Movie does not exist!");

        IQueryable<Movie> dbMovie = _context.Movies.AsNoTracking();

        if (!dbMovie.Any(x => x.Id == id)) throw new InvalidOperationException("Movie does not exist!");

        Movie movie = dbMovie.Where(x => x.Id == id).Include(x => x.Genre).Include(x => x.Director).Single();

        movie.Actors = _context.Actors.Where(x => _context.MovieActors.Where(y => y.MovieId == id).Select(y => y.ActorId).Contains(x.Id)).ToList();

        return _mapper.Map<MovieDto>(movie);
    }
}