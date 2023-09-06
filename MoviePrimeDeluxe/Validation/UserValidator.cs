using FluentValidation;
using MoviePrimeDeluxe.Entities;
using MoviePrimeDeluxe.Entities.Models;

namespace MoviePrimeDeluxe.Validation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator() 
        {
            
        }
    }
}

