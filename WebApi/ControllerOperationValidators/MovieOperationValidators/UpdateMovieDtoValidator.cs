using FluentValidation;
using WebApi.Dtos.MovieDtos.PutMovieDtos;

namespace WebApi.ControllerOperationValidators.MovieOperationValidators;

public class UpdateMovieDtoValidator : AbstractValidator<UpdateMovieDto>
{
    public UpdateMovieDtoValidator()
    {
        RuleFor(movie => movie).NotNull();
        RuleFor(movie => movie.Name).NotNull().NotEmpty().WithMessage("Please ensure that you have entered your {PropertyName}");
        RuleFor(movie => movie.ReleaseDate).NotNull().NotEmpty().LessThan(System.DateTime.Now);
        RuleFor(movie => movie.Price).NotNull().NotEmpty().GreaterThan(0);

        RuleFor(movie => movie.Genre).NotNull();
        RuleFor(movie => movie.Genre.Name).NotNull().NotEmpty().When(movie => movie.Genre is not null);

        RuleFor(movie => movie.Director).NotNull();
        When(movie => movie.Director is not null, () =>
        {
            RuleFor(movie => movie.Director.Name).NotNull().NotEmpty();
            RuleFor(movie => movie.Director.Surname).NotNull().NotEmpty();
        });

        RuleFor(movie => movie.Actors).NotNull().NotEmpty()
            .ForEach(actorRule =>
            {
                actorRule.ChildRules(validator =>
                {
                    validator.RuleFor(actor => actor).NotNull();
                    validator.RuleFor(actor => actor.Name).NotNull().NotEmpty();
                    validator.RuleFor(actor => actor.Surname).NotNull().NotEmpty();
                });
            });

        // RuleForEach(movie => movie.Actors).ChildRules(validator =>
        // {
        //     validator.RuleFor(actor => actor).NotNull();
        //     validator.RuleFor(actor => actor.Name).NotNull().NotEmpty();
        //     validator.RuleFor(actor => actor.Surname).NotNull().NotEmpty();
        // });
    }
}