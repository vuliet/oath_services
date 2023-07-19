using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Auth.Application.Features.Users.Command
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(p => p.UserName)
            .NotEmpty().WithMessage("{UserName} is required.")
            .NotNull()
            .MaximumLength(50).WithMessage("{UserName} must not exceed 50 characters.");

            RuleFor(p => p.Password)
                .Equal(p => p.ConfirmPassword).WithMessage("Passwords do not match.");
        }
    }
}