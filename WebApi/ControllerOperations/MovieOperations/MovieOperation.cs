using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WebApi.ControllerOperationValidators.MovieOperationValidators;
using WebApi.DBOperations;
using WebApi.Dtos.MovieDtos.PostMovieDtos;
using WebApi.Dtos.MovieDtos.PutMovieDtos;
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

        if (dbMovie.Any(x => x.IsDeleted)) return new GetMoviesDto();

        var movies = dbMovie.Where(x => !x.IsDeleted).Include(x => x.Genre).Include(x => x.Director).OrderBy(x => x.Name).ToList();
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

        if (!dbMovie.Any(x => x.Id == id && !x.IsDeleted)) throw new InvalidOperationException("Movie does not exist!");

        Movie movie = dbMovie.Where(x => x.Id == id && !x.IsDeleted).Include(x => x.Genre).Include(x => x.Director).Single();

        movie.Actors = _context.Actors.Where(x => _context.MovieActors.Where(y => y.MovieId == id).Select(y => y.ActorId).Contains(x.Id)).ToList();

        return _mapper.Map<GetMovieDto>(movie);
    }

    public void AddMovieCommand(AddMovieDto addMovieDto)
    {
        AddMovieDtoValidator validator = new();

        validator.ValidateAndThrow(addMovieDto);

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

    public void UpdateMovieCommand(int id, UpdateMovieDto updateMovieDto)
    {
        if (id < 1) throw new InvalidOperationException("Movie does not exist!");

        UpdateMovieDtoValidator validator = new();

        validator.ValidateAndThrow(updateMovieDto);

        DbSet<Movie> dbMovie = _context.Movies;

        if (!dbMovie.Any(x => x.Id == id)) throw new InvalidOperationException("Movie does not exist!");

        Movie movie = _mapper.Map<Movie>(updateMovieDto);

        movie.Id = id;

        movie.Name = updateMovieDto.Name ?? movie.Name;
        movie.ReleaseDate = updateMovieDto.ReleaseDate ?? movie.ReleaseDate;
        movie.Price = updateMovieDto.Price ?? movie.Price;

        Genre? genre = movie.Genre;
        movie.Genre = null;

        if (genre is not null)
        {
            DbSet<Genre> dbGenre = _context.Genres;
            if (!dbGenre.Any(x => x.Name.Equals(genre.Name))) throw new InvalidOperationException("Genre does not exist!");
            movie.GenreId = dbGenre.Single(x => x.Name.Equals(genre.Name)).Id;
        }

        Director? director = movie.Director;
        movie.Director = null;

        if (director is not null)
        {
            string directorName = director.Name;
            string directorSurname = director.Surname;

            DbSet<Director> dbDirector = _context.Directors;
            if (!dbDirector.Any(x => x.Name.Equals(directorName) && x.Surname.Equals(directorSurname)))
                throw new InvalidOperationException("Director does not exist!");

            movie.DirectorId = dbDirector.Single(x => x.Name.Equals(directorName) && x.Surname.Equals(directorSurname)).Id;
        }

        ICollection<Actor>? actors = movie.Actors;

        if (actors is not null && actors.Count > 0)
        {
            IQueryable<Actor> dbActor = _context.Actors.AsNoTracking();
            List<MovieActor> movieActors = new();

            actors.ToList().ForEach(x =>
                {
                    x = dbActor.SingleOrDefault(y => y.Name.Equals(x.Name) && y.Surname.Equals(x.Surname))
                        ?? throw new InvalidOperationException($"Actor does not exist! Actor: {x.Name} {x.Surname}");
                    movieActors.Add(new MovieActor(id, x.Id));
                });

            DbSet<MovieActor> dbMovieActor = _context.MovieActors;

            dbMovieActor.AddRange(movieActors);
            dbMovieActor.RemoveRange(dbMovieActor.Where(x => x.MovieId == id));
        }

        dbMovie.Update(movie);
        _context.SaveChanges();
    }

    public void DeleteMovieCommand(int id)
    {
        if (id < 1) throw new InvalidOperationException("Movie does not exist!");

        DbSet<Movie> dbMovie = _context.Movies;

        if (!dbMovie.Any(x => x.Id == id)) throw new InvalidOperationException("Movie does not exist!");

        Movie movie = dbMovie.Single(x => x.Id == id);

        movie.IsDeleted = true;

        dbMovie.Update(movie);
        _context.SaveChanges();
    }
}