using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Buisiness.Interfaces;
using TodoApp.Data.Interfaces;
using TodoApp.Buisiness.Models;

namespace TodoApp.Buisiness.Services
{
    public class TodoItemService : IAsyncDataService<TodoItemVO>
    {
        private readonly IAsyncDataProvider<TodoItemVO> _dataProvider;
        public TodoItemService(IAsyncDataProvider<TodoItemVO> dataProvider)
        {
            _dataProvider = dataProvider;
        }
        public async Task<int> Create(TodoItemVO todoItem)
        {
            int createdId = await _dataProvider.Create(todoItem);
            return createdId;
        }

        public async Task Delete(int id)
        {
            await _dataProvider.Delete(id);
        }

        public async Task<TodoItemVO> Get(int? id)
        {
            return await _dataProvider.Get(id);
        }

        public async Task<ICollection<TodoItemVO>> GetAll()
        {
            return await _dataProvider.GetAll();
        }

        public async Task Update(TodoItemVO todoItem)
        {
            await _dataProvider.Update(todoItem);
        }
    }
}
