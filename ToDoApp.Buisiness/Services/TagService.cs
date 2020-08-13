using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Buisiness.Interfaces;
using TodoApp.Data.Interfaces;
using TodoApp.Buisiness.Models;

namespace TodoApp.Buisiness.Services
{
    public class TagService : IAsyncDataService<TagVO>
    {
        private readonly IAsyncDataProvider<TagVO> _dataProvider;
        public TagService(IAsyncDataProvider<TagVO> dataProvider)
        {
            _dataProvider = dataProvider;
        }
        public async Task<int> Create(TagVO tag)
        {
            int createdId  = await _dataProvider.Create(tag);
            return createdId;
        }

        public async Task Delete(int id)
        {
            await _dataProvider.Delete(id);
        }

        public async Task<TagVO> Get(int? id)
        {
            return await _dataProvider.Get(id);
        }

        public async Task<ICollection<TagVO>> GetAll()
        {
            return await _dataProvider.GetAll();
        }

        public async Task Update(TagVO tag)
        {
            await _dataProvider.Update(tag);
        }
    }
}

