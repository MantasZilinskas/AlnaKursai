using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp.Interfaces
{
    public interface IGenericInFileProvider<T>
    {
        public T Get();
        public ICollection<T> GetAll();
        public void Create(T data);
        public void Update(T data);
        public void Delete(T data);
    }
}
