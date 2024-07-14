using ToDoList.Domain.Models;

namespace ToDoList.Contracts.Models;

public class ToDoItemResponse
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime DueDate { get; set; }
    public int PriorityId { get; set; }
    public int PriorityLevel { get; set; }
    public int? UserId { get; set; }
    public string UserName { get; set; }

    public static ToDoItemResponse Create(ToDoItemModel toDoItem) => new ToDoItemResponse
    {
        Id = toDoItem.Id,
        Title = toDoItem.Title,
        Description = toDoItem.Description,
        IsCompleted = toDoItem.IsCompleted,
        DueDate = toDoItem.DueDate,
        PriorityId = toDoItem.Priority.Id,
        PriorityLevel = toDoItem.Priority.Level,
        UserId = toDoItem.User?.Id,
        UserName = toDoItem.User?.Name
    };
}

