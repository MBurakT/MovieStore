using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Dtos.MovieDtos.PostMovieDtos;
using WebApi.Entities;
using WebApi.Models.MovieDtos.GetMovieDtos;

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

    public GetMoviesDto GetMoviesQuery()
    {
        IQueryable<Movie> dbMovie = _context.Movies.AsNoTracking();

        if (!dbMovie.Any()) return new GetMoviesDto();

        var movies = dbMovie.Include(x => x.Genre).Include(x => x.Director).OrderBy(x => x.Name).ToList();
        var movieActors = _context.MovieActors;
        var actors = _context.Actors;

        movies.ForEach(x =>
            x.Actors = actors.Where(y => movieActors.Where(y => y.MovieId == x.Id).Select(y => y.ActorId).Contains(y.Id)).ToList());

        return new GetMoviesDto { Movies = _mapper.Map<List<GetMovieDto>>(movies) };
    }

    public GetMovieDto GetMovieCommandById(int id)
    {
        if (id < 1) throw new InvalidOperationException("Movie does not exist!");

        IQueryable<Movie> dbMovie = _context.Movies.AsNoTracking();

        if (!dbMovie.Any(x => x.Id == id)) throw new InvalidOperationException("Movie does not exist!");

        Movie movie = dbMovie.Where(x => x.Id == id).Include(x => x.Genre).Include(x => x.Director).Single();

        movie.Actors = _context.Actors.Where(x => _context.MovieActors.Where(y => y.MovieId == id).Select(y => y.ActorId).Contains(x.Id)).ToList();

        return _mapper.Map<GetMovieDto>(movie);
    }

    public void AddMovieCommand(AddMovieDto addMovieDto)
    {
        if (_context.Movies.Any(x => x.Name.Equals(addMovieDto.Name))) throw new Exception("Movie already exists!");

        Movie movie = _mapper.Map<Movie>(addMovieDto);

        movie.Genre = _context.Genres.SingleOrDefault(x => x.Name.Equals(movie.Genre.Name)) ?? throw new Exception("Genre does not exist!");

        string directorName = movie.Director.Name;
        string directorSurname = movie.Director.Surname;

        movie.Director = _context.Directors.SingleOrDefault(x => x.Name.Equals(directorName) && x.Surname.Equals(directorSurname))
            ?? new Director(directorName, directorSurname);

        DbSet<MovieActor> dbMovieActors = _context.MovieActors;

        addMovieDto.Actors.ForEach(x => dbMovieActors.Add(new MovieActor(movie,
            _context.Actors.SingleOrDefault(y => y.Name.Equals(x.Name) && y.Surname.Equals(y.Surname)) ?? new Actor(x.Name, x.Surname))));

        _context.SaveChanges();
    }
}