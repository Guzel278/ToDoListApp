using ToDoList.Domain.Models;
using ToDoList.Infrastructure.Repositories;
using ToDoListServiceApp.Interfaces;

namespace ToDoListServiceApp.Services;

public class UserService : IUserService
{
    private readonly IRepository<UserModel> _userRepository;

    public UserService(IRepository<UserModel> userRepository)
    {
        _userRepository = userRepository;
    }
    public UserModel CreateUser(UserModel user)
    {
        return  _userRepository.Add(user);
    }

    public async Task DeleteUserAsync(int id, CancellationToken cancellationToken)
    {
        await _userRepository.DeleteAsync(id, cancellationToken);
    }

    public async Task<UserModel> GetUserByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _userRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<UserModel> UpdateUserAsync(UserModel user, CancellationToken cancellationToken)
    {
        return await _userRepository.UpdateAsync(user, cancellationToken);
    }

    public async Task<UserModel> GetUserName(int id, CancellationToken cancellationToken)
    {
        return await _userRepository.GetByIdAsync(id, cancellationToken);
    }
}

