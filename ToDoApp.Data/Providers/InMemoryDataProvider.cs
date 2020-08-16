using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Data.Interfaces;

namespace TodoApp.Data.Providers
{
    public class InMemoryDataProvider<TDataClass> : IDataProvider<TDataClass> where TDataClass : IHasId
    {
        private static List<TDataClass> values = new List<TDataClass>();

        public void Create(TDataClass data)
        {
            data.Id = values.Count();
            values.Add(data);
        }
        public void Delete(int id)
        {
            TDataClass item = values.FirstOrDefault(value => value.Id == id);
            values.Remove(item);
        }

        public TDataClass Get(int id)
        {
            TDataClass item = values.FirstOrDefault(value => value.Id == id);
            return item;
        }

        public ICollection<TDataClass> GetAll()
        {
            return values;
        }

        public void Update(TDataClass data)
        {
            TDataClass item = values.FirstOrDefault(value => value.Id == data.Id);
            values.Remove(item);
            values.Add(data);
        }
        public bool Exists(int id)
        {
            return values.Any(value => value.Id == id);
        }
    }
}
