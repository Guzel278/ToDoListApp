using ToDoList.Domain.Models;

namespace ToDoListServiceApp.Interfaces;

public interface IUserService
{
    UserModel CreateUser(UserModel user);
    Task<UserModel> GetUserByIdAsync(int id, CancellationToken cancellationToken);
    Task<UserModel> UpdateUserAsync(UserModel user, CancellationToken cancellationToken);
    Task DeleteUserAsync(int id, CancellationToken cancellationToken);
}