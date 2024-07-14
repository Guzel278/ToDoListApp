using ToDoList.Contracts.Models;
using ToDoList.Domain.Models;

namespace ToDoListServiceApp.Interfaces;

public interface IToDoItemService
{
    Task<ToDoItemModel> CreateToDoItem(ToDoItemModel toDoItem, CancellationToken cancellationToken);
    Task<ToDoItemModel> GetToDoItemByIdAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<ToDoItemModel>> GetAllToDoItemsAsync(CancellationToken cancellationToken);
    Task<ToDoItemModel> UpdateToDoListItem(ToDoItemModel toDoItem, CancellationToken cancellationToken);
    Task DeleteToDoItemAsync(ToDoItemModel toDoItem, CancellationToken cancellationToken);
    Task<IEnumerable<ToDoItemModel>> GetFilteredToDoItemsAsync(ToDoItemFilterRequest filterRequest, CancellationToken cancellationToken);
}

