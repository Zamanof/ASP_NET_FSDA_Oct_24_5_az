using ASP_NET_14._TaskFlow_Refresh_Token.DTOs.Auth_DTOs;
using FluentValidation;

namespace ASP_NET_14._TaskFlow_Refresh_Token.Validators;

public class RegisterValidator : AbstractValidator<RegisterRequestDto>
{
    public RegisterValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Firstname is required")
            .MinimumLength(2).WithMessage("Firstname must be at least 2 characters long");

        RuleFor(x => x.LastName)
           .NotEmpty().WithMessage("Lastname is required")
           .MinimumLength(2).WithMessage("Lastname must be at least 2 characters long");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Lastname is required")
            .EmailAddress().WithMessage("Email is not valid");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)").WithMessage("Passwords must have at least one digit ('0'-'9').,Passwords must have at least one lowercase ('a'-'z').,Passwords must have at least one lowercase ('A'-'Z')");

        RuleFor(x => x.ConfirmedPassword)
            .NotEmpty().WithMessage("Confirmed password is required")
            .Equal(x => x.Password).WithMessage("Passwords do not match");

    }
}

public class LoginValidator : AbstractValidator<LoginRequestDto>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Lastname is required")
            .EmailAddress().WithMessage("Email is not valid");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)").WithMessage("Passwords must have at least one digit ('0'-'9').,Passwords must have at least one lowercase ('a'-'z').,Passwords must have at least one lowercase ('A'-'Z')");
    }
}

public class RefreshValidator : AbstractValidator<RefreshTokenRequest>
{
    public RefreshValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty().WithMessage("Refresh Token is required");
    }
}