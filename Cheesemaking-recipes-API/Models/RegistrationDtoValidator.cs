using Cheesemaking_recipes_API.Entities;
using FluentValidation;
using System.Linq;

namespace Cheesemaking_recipes_API.Models
{
    public class RegistrationDtoValidator : AbstractValidator<RegistrationDto>
    {
        public RegistrationDtoValidator(ApiDbContext dbContext)
        {
            RuleFor(r => r.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Please enter a valid email address");

            RuleFor(r => r.Email)
                .Custom((email, context) =>
                {
                    var isEmailInUse = dbContext.Users.Any(u => u.Email == email);
                    if (isEmailInUse)
                    {
                        context.AddFailure("This email address has been taken");
                    }
                });

            RuleFor(r => r.Name)
                .NotEmpty()
                .MaximumLength(20)
                .WithMessage("The name field cannot be longer than 20 characters");

            RuleFor(r => r.Password)
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(20)
                .WithMessage("The password must contain 6-20 characters");

            RuleFor(r => r.ConfirmPassword)
                .Equal(r => r.Password)
                .WithMessage("The passwords provided do not match");
        }
    }
}
