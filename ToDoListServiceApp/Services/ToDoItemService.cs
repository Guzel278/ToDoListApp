using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using ToDoList.Contracts.Models;
using ToDoList.Domain.Models;
using ToDoList.Infrastructure.Repositories;
using ToDoListServiceApp.Interfaces;

namespace ToDoListServiceApp.Services;

public class ToDoItemService : IToDoItemService
{
    private readonly IRepository<ToDoItemModel> _toDoItemRepository;
    private readonly IRepository<UserModel> _userRepository;
    private readonly IRepository<PriorityModel> _priorityRepository;

    public ToDoItemService(
        IRepository<ToDoItemModel> toDoItemRepository,
        IRepository<UserModel> userRepository,
        IRepository<PriorityModel> priorityRepository)
    {
        _toDoItemRepository = toDoItemRepository;
        _userRepository = userRepository;
        _priorityRepository = priorityRepository;
    }

    public async Task<ToDoItemModel> CreateToDoItem(ToDoItemModel toDoItem, CancellationToken cancellationToken)
    {
        //checking existing priority in db
        toDoItem.Priority = await _priorityRepository.GetByIdAsync(toDoItem.PriorityId, cancellationToken)
                            ?? throw new KeyNotFoundException($"Priority with id {toDoItem.PriorityId} not found");

        if (toDoItem.UserId.HasValue)
        {
            //checking existing user in db
            toDoItem.User = await _userRepository.GetByIdAsync(toDoItem.UserId.Value, cancellationToken)
                                ?? throw new KeyNotFoundException($"User with id {toDoItem.UserId.Value} not found");
        }

        _toDoItemRepository.Add(toDoItem);
        return toDoItem;
    }

    public async Task<ToDoItemModel> GetToDoItemByIdAsync(int id, CancellationToken cancellationToken)
    {
        var toDoItem = await _toDoItemRepository.GetByIdAsync(id, cancellationToken);
        if (toDoItem == null)
        {
            throw new KeyNotFoundException($"Item with id {id} not found");
        }
        var priority = toDoItem.Priority;
        var user = toDoItem.User;

        if (priority == null)
        {
            throw new KeyNotFoundException($"Priority with id {toDoItem.PriorityId} not found");
        }

        if (toDoItem.UserId.HasValue && user == null)
        {
            throw new KeyNotFoundException($"User with id {toDoItem.UserId.Value} not found");
        }
        return toDoItem;
    }

    public async Task<IEnumerable<ToDoItemModel>> GetAllToDoItemsAsync(CancellationToken cancellationToken)
    {
        var toDoItems = await _toDoItemRepository.GetAllAsync(cancellationToken);

        return toDoItems;
    }

    public async Task<IEnumerable<ToDoItemModel>> GetFilteredToDoItemsAsync(ToDoItemFilterRequest filterRequest, CancellationToken cancellationToken)
    {
        var query = _toDoItemRepository.GetAllQuery().AsNoTracking();

        if (filterRequest.IsCompleted.HasValue)
        {
            query = query.Where(t => t.IsCompleted == filterRequest.IsCompleted.Value);
        }

        if (filterRequest.PriorityId.HasValue)
        {
            query = query.Where(t => t.PriorityId == filterRequest.PriorityId.Value);
        }

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<ToDoItemModel> UpdateToDoListItem(ToDoItemModel toDoItem, CancellationToken cancellationToken)
    {
        var toDoItemExict = await _toDoItemRepository.GetByIdAsync(toDoItem.Id, cancellationToken);

        if (toDoItemExict != null && toDoItem != null)
        {
            toDoItemExict.Title = toDoItem.Title;
            toDoItemExict.Description = toDoItem.Description;
            toDoItemExict.IsCompleted = toDoItem.IsCompleted;
            toDoItemExict.DueDate = toDoItem.DueDate;
            toDoItemExict.PriorityId = toDoItem.PriorityId;
            toDoItemExict.Priority = await _priorityRepository.GetByIdAsync(toDoItem.PriorityId, cancellationToken)
                                         ?? throw new KeyNotFoundException($"Priority with id {toDoItem.PriorityId} not found");
            if (toDoItem.UserId.HasValue)
            {
                toDoItemExict.UserId = toDoItem.UserId.Value;
                toDoItemExict.User = await _userRepository.GetByIdAsync(toDoItem.UserId.Value, cancellationToken)
                                     ?? throw new KeyNotFoundException($"User with id {toDoItem.UserId.Value} not found");
            }

            return await _toDoItemRepository.UpdateAsync(toDoItemExict, cancellationToken);
        }
        else
        {
            throw new ArgumentNullException("toDoItemExict or toDoItem is null");
        }
    }

    public async Task DeleteToDoItemAsync(ToDoItemModel toDoItem, CancellationToken cancellationToken)
    {
        await _toDoItemRepository.DeleteAsync(toDoItem.Id, cancellationToken);
    }

}
