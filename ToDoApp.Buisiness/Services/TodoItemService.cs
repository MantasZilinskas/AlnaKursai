using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Buisiness.Interfaces;
using TodoApp.Data.Interfaces;
using TodoApp.Buisiness.Models;
using AutoMapper;
using TodoApp.Data.Models;
using System;
using TodoApp.Commons.Enums;

namespace TodoApp.Buisiness.Services
{
    public class TodoItemService : IAsyncDataService<TodoItemVO>
    {
        private readonly ITodoItemProvider _dataProvider;
        private readonly IMapper _mapper;

        public TodoItemService(ITodoItemProvider dataProvider, IMapper mapper)
        {
            _dataProvider = dataProvider;
            _mapper = mapper;
        }

        public async Task<int> Create(TodoItemVO todoItem)
        {
            if (await _dataProvider.IsDuplicate(_mapper.Map<TodoItemDAO>(todoItem)))
            {
                throw new ArgumentException("An item with the name " + todoItem.Name + " already exists");
            }
            if(await _dataProvider.WipStatusWithPriority1Exists())
            {
                if(todoItem.Priority == 1 && todoItem.Status == Status.Wip)
                {
                    throw new ArgumentException("Only 1 Wip status item with priority 1 can exist");
                }
            }
            if(await _dataProvider.ThreeItemsOfWipStatusWithPriority2Exists())
            {
                if (todoItem.Priority == 2 && todoItem.Status == Status.Wip)
                {
                    throw new ArgumentException("Only 3 Wip status items with priority 2 can exist");
                }
            }
            int createdId = await _dataProvider.Create(_mapper.Map<TodoItemDAO>(todoItem));
            return createdId;
        }

        public async Task Delete(int id)
        {
            if (await _dataProvider.Exists(id))
            {
                await _dataProvider.Delete(id);
            }
            else
            {
                throw new KeyNotFoundException();
            }
            
        }

        public async Task<TodoItemVO> Get(int? id)
        {
            if (await _dataProvider.Exists(id))
            {
                return _mapper.Map<TodoItemVO>(await _dataProvider.Get(id));
            }
            else
            {
                throw new KeyNotFoundException();
            }
            
        }

        public async Task<IEnumerable<TodoItemVO>> GetAll()
        {
            return _mapper.Map<IEnumerable<TodoItemVO>>(await _dataProvider.GetAll());
        }

        public async Task Update(TodoItemVO todoItem)
        {
            if (await _dataProvider.Exists(todoItem.Id))
            {
                await _dataProvider.Update(_mapper.Map<TodoItemDAO>(todoItem));
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }
    }
}
