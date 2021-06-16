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
                .WithMessage("Wprowadzono niepoprawny adres e-mail");

            RuleFor(r => r.Email)
                .Custom((email, context) =>
                {
                    var isEmailInUse = dbContext.Users.Any(u => u.Email == email);
                    if (isEmailInUse)
                    {
                        context.AddFailure("Podany adres e-mail jest już zajęty");
                    }
                });

            RuleFor(r => r.Name)
                .NotEmpty()
                .MaximumLength(20)
                .WithMessage("Imię nie może mieć więcej niż 20 znaków");

            RuleFor(r => r.Password)
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(20)
                .WithMessage("Hasło musi się składać z 6-20 znaków");

            RuleFor(r => r.ConfirmPassword)
                .Equal(r => r.Password)
                .WithMessage("Podane hasła nie są takie same");
        }
    }
}
