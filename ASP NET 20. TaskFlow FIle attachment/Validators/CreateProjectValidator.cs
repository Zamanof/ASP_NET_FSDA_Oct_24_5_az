using ASP_NET_20._TaskFlow_FIle_attachment.DTOs;
using FluentValidation;

namespace ASP_NET_20._TaskFlow_FIle_attachment.Validators;

public class CreateProjectValidator : AbstractValidator<CreateProjectDto>
{
    public CreateProjectValidator()
    {
        
        RuleFor(x => x.Name)
                    .NotEmpty().WithMessage("Project Name is required")
                    .MinimumLength(3).WithMessage("Project name must be at least 3 characters long");
    }
}
