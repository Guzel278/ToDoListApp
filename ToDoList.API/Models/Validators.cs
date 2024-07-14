using FluentValidation;
using ToDoList.Contracts.Models;

namespace ToDoList.API.Models.Validators;

public class ToDoItemRequestValidator : AbstractValidator<ToDoItemRequest>
{
    public ToDoItemRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title must be less than 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.");

        RuleFor(x => x.DueDate)
            .NotEmpty().WithMessage("DueDate is required.")
            .GreaterThan(DateTime.Now).WithMessage("DueDate must be in the future.");

        RuleFor(x => x.PriorityId)
            .GreaterThan(0).WithMessage("PriorityId must be greater than 0.");
    }
}

public class UserRequestValidator : AbstractValidator<UserRequest>
{
    public UserRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name must be less than 50 characters.");
    }
}
