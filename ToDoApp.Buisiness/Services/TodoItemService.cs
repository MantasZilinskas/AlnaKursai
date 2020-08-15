using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Buisiness.Interfaces;
using TodoApp.Data.Interfaces;
using TodoApp.Buisiness.Models;
using AutoMapper;
using TodoApp.Data.Models;

namespace TodoApp.Buisiness.Services
{
    public class TodoItemService : IAsyncDataService<TodoItemVO>
    {
        private readonly IAsyncDataProvider<TodoItemDAO> _dataProvider;
        private readonly IMapper _mapper;

        public TodoItemService(IAsyncDataProvider<TodoItemDAO> dataProvider, IMapper mapper)
        {
            _dataProvider = dataProvider;
            _mapper = mapper;
        }

        public async Task<int> Create(TodoItemVO todoItem)
        {
            int createdId = await _dataProvider.Create(_mapper.Map<TodoItemDAO>(todoItem));
            return createdId;
        }

        public async Task Delete(int id)
        {
            await _dataProvider.Delete(id);
        }

        public async Task<TodoItemVO> Get(int? id)
        {
            return _mapper.Map<TodoItemVO>(await _dataProvider.Get(id));
        }

        public async Task<IEnumerable<TodoItemVO>> GetAll()
        {
            return _mapper.Map<IEnumerable<TodoItemVO>>(await _dataProvider.GetAll());
        }

        public async Task Update(TodoItemVO todoItem)
        {
            await _dataProvider.Update(_mapper.Map<TodoItemDAO>(todoItem));
        }
    }
}
