using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Schema;
using TodoApp.Buisiness.Interfaces;
using TodoApp.Buisiness.Models;
using TodoApp.Data.Interfaces;
using TodoApp.Data.Models;

namespace TodoApp.Buisiness.Services
{
    public class CategoryService : IAsyncDataService<CategoryVO>
    {
        private readonly IAsyncDataProvider<CategoryDAO> _dataProvider;
        private readonly IMapper _mapper;

        public CategoryService(IAsyncDataProvider<CategoryDAO> dataProvider, IMapper mapper)
        {
            _dataProvider = dataProvider;
            _mapper = mapper;
        }

        public async Task<int> Create(CategoryVO tag)
        {
            int createdId = await _dataProvider.Create(_mapper.Map<CategoryDAO>(tag));
            return createdId;
        }

        public async Task Delete(int id)
        {
            await _dataProvider.Delete(id);
        }

        public async Task<CategoryVO> Get(int? id)
        {
            return _mapper.Map<CategoryVO>(await _dataProvider.Get(id));
        }

        public async Task<IEnumerable<CategoryVO>> GetAll()
        {
            return _mapper.Map<IEnumerable<CategoryVO>>(await _dataProvider.GetAll());
        }

        public async Task Update(CategoryVO tag)
        {
            await _dataProvider.Update(_mapper.Map<CategoryDAO>(tag));
        }
    }
}
