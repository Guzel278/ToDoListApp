using Microsoft.AspNetCore.Mvc;
using ToDoList.Contracts.Models;
using ToDoList.Domain.Models;
using ToDoListServiceApp.Interfaces;

namespace ToDoList.API.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;

    public UserController(ILogger<UserController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById([FromQuery] int id, CancellationToken cancellationToken = default)
    {
        var user = await _userService.GetUserByIdAsync(id, cancellationToken);

        return user == null
            ? NotFound($"User with id {id} not found")
            : Ok(new UserResponse { Id = user.Id, Name = user.Name });
    }

    [HttpPost("create")]
    public UserResponse CreateUser([FromBody] UserRequest request) =>
        UserResponse.Create(_userService.CreateUser(request.CreateModel()));

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UserRequest request, CancellationToken cancellationToken = default)
    {
        var existingUser = await _userService.GetUserByIdAsync(id, cancellationToken);

        if (existingUser == null)
        {
            return NotFound($"User with id {id} not found");
        }

        existingUser.Name = request.Name;

        return  Ok(UserResponse.Create(await _userService.UpdateUserAsync(existingUser, cancellationToken)));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser([FromQuery] int id, CancellationToken cancellationToken = default)
    {
        await _userService.DeleteUserAsync(id, cancellationToken);

        return Ok(NoContent()); 
    }
}
