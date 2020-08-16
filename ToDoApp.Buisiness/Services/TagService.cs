using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Buisiness.Interfaces;
using TodoApp.Data.Interfaces;
using TodoApp.Buisiness.Models;
using AutoMapper;
using TodoApp.Data.Models;
using System;

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
            if (await _dataProvider.IsDuplicate(_mapper.Map<TagDAO>(tag)))
            {
                throw new ArgumentException("A tag with the name " + tag.Name + " already exists");
            }
            int createdId  = await _dataProvider.Create(_mapper.Map<TagDAO>(tag));
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

        public async Task<TagVO> Get(int? id)
        {
            if (await _dataProvider.Exists(id))
            {
                return _mapper.Map<TagVO>(await _dataProvider.Get(id));
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public async Task<IEnumerable<TagVO>> GetAll()
        {
            return _mapper.Map<IEnumerable<TagVO>>(await _dataProvider.GetAll());
        }

        public async Task Update(TagVO tag)
        {
            if (await _dataProvider.Exists(tag.Id))
            {
                await _dataProvider.Update(_mapper.Map<TagDAO>(tag));
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }
    }
}

