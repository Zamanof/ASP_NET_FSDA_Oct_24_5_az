using ASP_NET_12._TaskFlow_Authentication_and_Authorization.DTOs.Project_DTOs;
using FluentValidation;

namespace ASP_NET_12._TaskFlow_Authentication_and_Authorization.Validators;

public class CreateProjectValidator : AbstractValidator<CreateProjectDto>
{
    public CreateProjectValidator()
    {
        
        RuleFor(x => x.Name)
                    .NotEmpty().WithMessage("Project Name is required")
                    .MinimumLength(3).WithMessage("Project name must be at least 3 characters long");
    }
}
