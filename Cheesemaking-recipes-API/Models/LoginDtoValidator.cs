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
                .WithMessage("Please enter a valid email address");

            RuleFor(r => r.Password)
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(20)
                .WithMessage("The password must contain 6-20 characters");
        }
    }
}
