using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Interfaces;

namespace ToDoApp.Services
{
    public class InMemoryDataProvider<TDataClass> : IDataProvider<TDataClass> where TDataClass : IHasId
    {
        private static List<TDataClass> values = new List<TDataClass>();

        public void Create(TDataClass data)
        {
            data.Id = values.Count();
            TDataClass item = values.FirstOrDefault(value => value.Id == data.Id);
            if (item == null)
            {
                values.Add(data);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public void Delete(int id)
        {
            TDataClass item = values.FirstOrDefault(value => value.Id == id);
            if (item != null)
            {
                values.Remove(item);
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public TDataClass Get(int id)
        {
            TDataClass item = values.FirstOrDefault(value => value.Id == id);
            if (item != null)
            {
                return item;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public ICollection<TDataClass> GetAll()
        {
            return values;
        }

        public void Update(TDataClass data)
        {
            TDataClass item = values.FirstOrDefault(value => value.Id == data.Id);
            if (item != null)
            {
                values.Remove(item);
                values.Add(data);
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }
    }
}
