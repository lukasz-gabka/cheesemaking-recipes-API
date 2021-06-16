using FluentValidation;

namespace Cheesemaking_recipes_API.Models
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(r => r.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Wprowadzono niepoprawny adres e-mail");

            RuleFor(r => r.Password)
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(20)
                .WithMessage("Hasło musi się składać z 6-20 znaków");
        }
    }
}
