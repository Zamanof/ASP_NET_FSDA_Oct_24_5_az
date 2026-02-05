using ASP_NET_14._TaskFlow_Refresh_Token.DTOs.TaskItem_DTOs;
using ASP_NET_14._TaskFlow_Refresh_Token.Models;
using FluentValidation;
using TaskStatus = ASP_NET_14._TaskFlow_Refresh_Token.Models.TaskStatus;

namespace ASP_NET_14._TaskFlow_Refresh_Token.Validators;

public class UpdateTaskItemValidator : AbstractValidator<UpdateTaskItemDto>
{
    public UpdateTaskItemValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MinimumLength(3).WithMessage("Task Item title must be at least 3 characters long");

        RuleFor(x => x.Status)
            .Must(s => new[] { TaskStatus.ToDo, TaskStatus.InProgress, TaskStatus.Done }.Contains(s))
            .WithMessage("Prority must be one of: 0(ToDo), 1(InProgress), 2(Done)");

        RuleFor(x => x.Priority)
            .Must(p => new[] { TaskPriority.Low, TaskPriority.High, TaskPriority.Medium }.Contains(p))
            .WithMessage("Prority must be one of: 0(Low), 1(Medium), 2(High)");
    }
}
