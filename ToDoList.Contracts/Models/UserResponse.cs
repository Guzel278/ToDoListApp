using ToDoList.Domain.Models;

namespace ToDoList.Contracts.Models;

public class UserResponse
{
    public int Id { get; set; }
    public string Name { get; set; }

    public static UserResponse Create(UserModel user) => new UserResponse
    {
        Id = user.Id,
        Name = user.Name
    };
}

