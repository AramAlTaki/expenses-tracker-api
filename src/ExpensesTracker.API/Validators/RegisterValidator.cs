using ExpensesTracker.API.Contracts.Requests;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace ExpensesTracker.API.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterRequest>
    {
        private readonly UserManager<IdentityUser> _userManager;

        public RegisterValidator(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;

            RuleFor(u => u.Username)
                .NotEmpty().WithMessage("Username is required.")
                .MaximumLength(50).WithMessage("Username must not exceed 50 characters.")
                .MustAsync(BeUniqueUsername).WithMessage("Username is already taken.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MustAsync(BeUniqueEmail).WithMessage("Email is already taken.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches("[A-Z]").WithMessage("Password must contain an uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain an lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain a digit.");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage("Passwords do not match.");
        }

        private async Task<bool> BeUniqueUsername(string username, CancellationToken ct)
        {
            return await _userManager.FindByNameAsync(username) == null;
        }

        private async Task<bool> BeUniqueEmail(string username, CancellationToken ct)
        {
            return await _userManager.FindByNameAsync(username) == null;
        }
    }
}
