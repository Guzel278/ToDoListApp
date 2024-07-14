namespace ToDoList.Contracts.Models;

public class ToDoItemFilterRequest
{
    public bool? IsCompleted { get; set; }
    public int? PriorityId { get; set; }
}

