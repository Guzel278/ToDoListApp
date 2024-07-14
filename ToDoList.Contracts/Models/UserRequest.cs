using ToDoList.Domain.Models;

namespace ToDoList.Contracts.Models;

public class UserRequest
{
    public string Name { get; set; }

    public UserModel CreateModel() => new()
    {
        Name = Name
    };
}

