using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ToDoList.API.Models.Validators;
using ToDoList.Contracts.Models;
using ToDoListServiceApp.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace ToDoList.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ToDoListController : ControllerBase
{
    private readonly ILogger<ToDoListController> _logger;
    private readonly IToDoItemService _toDoItemService;
    private readonly IValidator<ToDoItemRequest> _validator;

    public ToDoListController(ILogger<ToDoListController> logger, IToDoItemService toDoItemService, IValidator<ToDoItemRequest> validator)
    {
        _logger = logger;
        _toDoItemService = toDoItemService;
        _validator = validator;
    }

    [HttpPost("create")]
    public async Task<ActionResult<ToDoItemResponse>> CreateToDoListItemAsync(
        [FromBody] ToDoItemRequest request, CancellationToken cancellationToken = default) =>
        ToDoItemResponse.Create(await _toDoItemService.CreateToDoItem(request.CreateModel(), cancellationToken));


    [HttpGet]
    public async Task<IActionResult> GetToDoListItemById ([FromQuery] int id,CancellationToken cancellationToken = default)
    {
        var toDoItem = await _toDoItemService.GetToDoItemByIdAsync(id, cancellationToken);
        return toDoItem == null
            ? BadRequest($"Item with id {id} not found")
            : Ok(ToDoItemResponse.Create(toDoItem));
    }

    [HttpGet("all")]
    public async Task<IEnumerable<ToDoItemResponse>> GetAllToDoListItem(CancellationToken cancellationToken = default) =>
        (await _toDoItemService.GetAllToDoItemsAsync(cancellationToken)).Select(ToDoItemResponse.Create);

    [HttpPost("filter")]
    public async Task<IEnumerable<ToDoItemResponse>> GetFilteredToDoItems(
        [FromBody] ToDoItemFilterRequest filterRequest, CancellationToken cancellationToken = default) =>
        (await _toDoItemService.GetFilteredToDoItemsAsync(filterRequest, cancellationToken)).Select(ToDoItemResponse.Create);

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteToDoListItem(int id, CancellationToken cancellationToken = default)
    {
        var toDoItem = await _toDoItemService.GetToDoItemByIdAsync(id, cancellationToken);
        await _toDoItemService.DeleteToDoItemAsync(toDoItem, cancellationToken);

        return Ok(NoContent());
    }

    [HttpPut]
    public async Task<IActionResult> UpdateToDoListItem(
        int id, [FromBody] ToDoItemRequest request, CancellationToken cancellationToken = default) =>
        Ok(ToDoItemResponse.Create(await _toDoItemService.UpdateToDoListItem(request.CreateModel(id), cancellationToken)));
}

