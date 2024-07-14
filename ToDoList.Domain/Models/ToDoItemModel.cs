
namespace ToDoList.Domain.Models;

public class ToDoItemModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime DueDate { get; set; }
    public int PriorityId { get; set; }
    public int? UserId { get; set; } // Nullable, if user is not assigned

    public virtual PriorityModel Priority { get; set; }
    public virtual UserModel User { get; set; }
}