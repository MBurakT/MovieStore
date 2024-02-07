using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.Models.MovieModels.GetMovieCommandById;
using WebApi.Models.MovieModels.GetMoviesQuery;

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

    public List<GetMoviesQueryMovieModel>? GetMoviesQuery()
    {
        var movies = _context.Movies.Include(x => x.Genre).Include(x => x.Director).OrderBy(x => x.Name).ToList();
        var movieActors = _context.MovieActors;
        var actors = _context.Actors;

        if (movies is null) return [];

        movies.ForEach(x =>
            x.Actors = actors.Where(y => movieActors.Where(y => y.MovieId == x.Id).Select(y => y.ActorId).Contains(y.Id)).ToList());

        return _mapper.Map<List<GetMoviesQueryMovieModel>>(movies);
    }

    public GetMovieCommandMovieModel GetMovieCommandById(int id)
    {
        if (id < 1 || !_context.Movies.Any(x => x.Id == id)) throw new InvalidOperationException("Movie does not exist!");

        Movie movie = _context.Movies.Where(x => x.Id == id).Include(x => x.Genre).Include(x => x.Director).Single();

        movie.Actors = _context.Actors.Where(x => _context.MovieActors.Where(y => y.MovieId == id).Select(y => y.ActorId).Contains(x.Id)).ToList();

#pragma warning disable CS8603 // Possible null reference return.
        return _mapper.Map<GetMovieCommandMovieModel>(movie);
#pragma warning restore CS8603 // Possible null reference return.
    }
}