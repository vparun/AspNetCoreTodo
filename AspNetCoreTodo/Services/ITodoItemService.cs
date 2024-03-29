using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;

namespace AspNetCoreTodo.Services
{
    public interface ITodoItemService
    {
        Task<TodoItem[]> GetInCompleteItemsAsync(ApplicationUser user);

        Task<bool> AddItemAsync(TodoItem newItem, ApplicationUser user);

        Task<bool> MarkDoneAsync(Guid Id, ApplicationUser user);
    }
}