using ToDoList.Domain.Models;

namespace ToDoList.Contracts.Models;

public class ToDoItemRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime DueDate { get; set; }
    public int PriorityId { get; set; }
    public int? UserId { get; set; } // May be nullable if user is not assigned

    public ToDoItemModel CreateModel() => new()
    {
        Title = Title,
        Description = Description,
        IsCompleted = IsCompleted,
        DueDate = DueDate,
        PriorityId = PriorityId,
        UserId = UserId
    };
    public ToDoItemModel CreateModel(int id) => new()
    {
        Id = id,
        Title = Title,
        Description = Description,
        IsCompleted = IsCompleted,
        DueDate = DueDate,
        PriorityId = PriorityId,
        UserId = UserId
    };

}

