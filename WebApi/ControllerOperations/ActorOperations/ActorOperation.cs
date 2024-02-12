using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Dtos.ActorDtos.GetActorDtos;
using WebApi.Dtos.ActorDtos.PostActorDtos;
using WebApi.Dtos.ActorDtos.PutActorDtos;
using WebApi.Entities;

namespace WebApi.ControllerOperations.ActorOperations;

public class ActorOperation
{
    readonly IMovieStoreDbContext _context;
    readonly IMapper _mapper;

    public ActorOperation(IMovieStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public GetActorsDto GetActorsQuery()
    {
        IQueryable<Actor> dbActor = _context.Actors.AsNoTracking();
        IQueryable<MovieActor> dbMovieActor = _context.MovieActors.AsNoTracking();
        IQueryable<Movie> dbMovie = _context.Movies.AsNoTracking().Where(x => !x.IsDeleted);

        if (!dbActor.Any()) return new GetActorsDto();

        List<Actor> actors = dbActor.ToList();

        actors.ForEach(x =>
        {
            IQueryable<int> MovieIds = dbMovieActor.Where(y => y.ActorId == x.Id).Select(y => y.MovieId);
            x.Movies = dbMovie.Where(y => MovieIds.Contains(y.Id)).ToList();
        });

        return new GetActorsDto { Actors = _mapper.Map<List<GetActorDto>>(actors) };
    }

    public GetActorDto GetActorCommandById(int id)
    {
        if (id < 1) throw new InvalidOperationException("Actor does not exist!");

        IQueryable<Actor> dbActor = _context.Actors.AsNoTracking();

        if (!dbActor.Any(x => x.Id == id)) throw new InvalidOperationException("Actor does not exist!");

        Actor actor = dbActor.Single(x => x.Id == id);

        IQueryable<int> MovieIds = _context.MovieActors.AsNoTracking().Where(x => x.ActorId == actor.Id).Select(y => y.MovieId);

        actor.Movies = _context.Movies.AsNoTracking().Where(x => !x.IsDeleted && MovieIds.Contains(x.Id)).ToList();

        return _mapper.Map<GetActorDto>(actor);
    }

    public void AddActorCommand(AddActorDto addActorDto)
    {
        IQueryable<Actor> dbActor = _context.Actors.AsNoTracking();

        Actor actor = _mapper.Map<Actor>(addActorDto);

        if (dbActor.Any(x => x.Name.Equals(actor.Name) && x.Surname.Equals(actor.Surname))) throw new InvalidOperationException("Actor already exists!");

        _context.Actors.Add(actor);
        _context.SaveChanges();
    }

    public void UpdateActorCommand(int id, UpdateActorDto updateActorDto)
    {
        if (id < 1) throw new InvalidOperationException("Actor does not exist!");

        IQueryable<Actor> dbActor = _context.Actors.AsNoTracking();

        if (!dbActor.Any(x => x.Id == id)) throw new InvalidOperationException("Actor does not exist!");

        Actor actor = _mapper.Map<Actor>(updateActorDto);

        if (dbActor.Any(x => x.Name.Equals(actor.Name) && x.Surname.Equals(actor.Surname))) throw new InvalidOperationException("Actor already exists!");

        actor.Id = id;

        _context.Actors.Update(actor);
        _context.SaveChanges();
    }
}