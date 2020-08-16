using System;
using System.Collections.Generic;
using TodoApp.Buisiness.Interfaces;
using TodoApp.Data.Interfaces;

namespace TodoApp.Buisiness.Services
{
    public class DataService<TDataClass> : IDataService<TDataClass> where TDataClass : Data.Interfaces.IHasId
    {
        private readonly IDataProvider<TDataClass> _dataProvider;

        public DataService(IDataProvider<TDataClass> dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public void Create(TDataClass data)
        {
            if (_dataProvider.Exists(data.Id))
            {
                _dataProvider.Create(data);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public void Delete(int id)
        {
            if (_dataProvider.Exists(id))
            {
                _dataProvider.Delete(id);
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public TDataClass Get(int id)
        {
            if (_dataProvider.Exists(id))
            {
                return _dataProvider.Get(id);
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public ICollection<TDataClass> GetAll()
        {
            return _dataProvider.GetAll();
        }

        public void Update(TDataClass data)
        {
            if (_dataProvider.Exists(data.Id))
            {
                _dataProvider.Update(data);
            }
            else
            {
                throw new KeyNotFoundException();
            }
            
        }
    }
}
