using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WebApi.ControllerOperationValidators.DirectorOperationValidators;
using WebApi.DBOperations;
using WebApi.Dtos.DirectorDtos.GetDirectorDtos;
using WebApi.Dtos.DirectorDtos.PostDirectorDtos;
using WebApi.Dtos.DirectorDtos.PutDirectorDtos;
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

    public void AddDirectorCommand(AddDirectorDto addDirectorDto)
    {
        AddDirectorDtoValidator validator = new();

        validator.ValidateAndThrow(addDirectorDto);

        IQueryable<Director> dbDirector = _context.Directors.AsNoTracking();

        Director director = _mapper.Map<Director>(addDirectorDto);

        if (dbDirector.Any(x => x.Name.Equals(director.Name) && x.Surname.Equals(director.Name))) throw new InvalidOperationException("Director already exists!");

        _context.Directors.Add(director);
        _context.SaveChanges();
    }

    public void UpdateDirectorCommand(int id, UpdateDirectorDto updateDirectorDto)
    {
        if (id < 1) throw new InvalidOperationException("Director does not exist!");

        UpdateDirectorDtoValidator validator = new();

        validator.ValidateAndThrow(updateDirectorDto);

        IQueryable<Director> dbDirector = _context.Directors.AsNoTracking();

        if (!dbDirector.Any(x => x.Id == id)) throw new InvalidOperationException("Director does not exist!");

        Director director = _mapper.Map<Director>(updateDirectorDto);

        if (dbDirector.Any(x => x.Name.Equals(director.Name) && x.Surname.Equals(director.Surname))) throw new InvalidOperationException("Director already exists!");

        director.Id = id;

        _context.Directors.Update(director);
        _context.SaveChanges();
    }
}