using FluentValidation;
using MoviePrimeDeluxe.Entities;

namespace MoviePrimeDeluxe.Validation
{
    public class MovieValidator : AbstractValidator<Movie>
    {
        public MovieValidator(List<string> excludeList)
        {
            if (excludeList != null && !excludeList.Contains("Id")) 
                //böyle bir film varsa ve böyle bir id içeren film yoksa
            { 
                RuleFor(m => m.Id).NotEmpty().GreaterThanOrEqualTo(1); 
            }
            
            RuleFor(m => m.Name).NotEmpty().WithMessage("Movie name can not be null.");
            RuleFor(m => m.Description).NotEmpty().WithMessage("Movie description can not be null.");
        }
    }
}
