using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Buisiness.Interfaces;
using TodoApp.Data.Interfaces;

namespace TodoApp.Buisiness.Services
{
    public class ItemTagService : IItemTagService
    {
        private readonly IItemTagProvider _dataProvider;

        public ItemTagService(IItemTagProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }
        public async Task Create(int todoItemId, List<int> tagIdList)
        {
            await _dataProvider.Create(todoItemId, tagIdList);
        }
        public async Task Delete(int todoItemId)
        {
            await _dataProvider.Delete(todoItemId);
        }
        public async Task Update(int todoItemId, List<int> tagIdList)
        {
            await _dataProvider.Update(todoItemId, tagIdList);
        }
    }
}
