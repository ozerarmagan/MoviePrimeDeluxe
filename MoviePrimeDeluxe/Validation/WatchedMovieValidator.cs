using FluentValidation;
using MoviePrimeDeluxe.Entities;

namespace MoviePrimeDeluxe.Validation
{
    public class WatchedMovieValidator : AbstractValidator<WatchedMovie>
    {
        public WatchedMovieValidator()
        {
           RuleFor(m => m.UserId).NotNull().NotEmpty().WithMessage("User id can not be null")
                                 .LessThanOrEqualTo(0).WithMessage("User id can not less than 1");

           RuleFor(m => m.MovieId).NotNull().NotEmpty().WithMessage("Movie id can not be null")
                                  .LessThanOrEqualTo(0).WithMessage("Movie id can not less than 1");
        }                        
    }
}
