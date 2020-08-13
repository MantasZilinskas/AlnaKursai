using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Buisiness.Interfaces;
using TodoApp.Buisiness.Models;
using TodoApp.Data.Interfaces;

namespace TodoApp.Buisiness.Services
{
    public class CategoryService : IAsyncDataService<CategoryVO>
    {
        private readonly IAsyncDataProvider<CategoryVO> _dataProvider;
        public CategoryService(IAsyncDataProvider<CategoryVO> dataProvider)
        {
            _dataProvider = dataProvider;
        }
        public async Task<int> Create(CategoryVO tag)
        {
            int createdId = await _dataProvider.Create(tag);
            return createdId;
        }

        public async Task Delete(int id)
        {
            await _dataProvider.Delete(id);
        }

        public async Task<CategoryVO> Get(int? id)
        {
            return await _dataProvider.Get(id);
        }

        public async Task<ICollection<CategoryVO>> GetAll()
        {
            return await _dataProvider.GetAll();
        }

        public async Task Update(CategoryVO tag)
        {
            await _dataProvider.Update(tag);
        }
    }
}
