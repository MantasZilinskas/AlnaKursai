using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Buisiness.Interfaces;
using TodoApp.Data.Interfaces;
using TodoApp.Buisiness.Models;
using AutoMapper;
using TodoApp.Data.Models;

namespace TodoApp.Buisiness.Services
{
    public class TagService : IAsyncDataService<TagVO>
    {
        private readonly IAsyncDataProvider<TagDAO> _dataProvider;
        private readonly IMapper _mapper;

        public TagService(IAsyncDataProvider<TagDAO> dataProvider, IMapper mapper)
        {
            _dataProvider = dataProvider;
            _mapper = mapper;
        }

        public async Task<int> Create(TagVO tag)
        {
            int createdId  = await _dataProvider.Create(_mapper.Map<TagDAO>(tag));
            return createdId;
        }

        public async Task Delete(int id)
        {
            await _dataProvider.Delete(id);
        }

        public async Task<TagVO> Get(int? id)
        {
            return _mapper.Map<TagVO>(await _dataProvider.Get(id));
        }

        public async Task<IEnumerable<TagVO>> GetAll()
        {
            return _mapper.Map<IEnumerable<TagVO>>(await _dataProvider.GetAll());
        }

        public async Task Update(TagVO tag)
        {
            await _dataProvider.Update(_mapper.Map<TagDAO>(tag));
        }
    }
}

