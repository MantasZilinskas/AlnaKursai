using System.Collections.Generic;
using ToDoApp.Models;

namespace ToDoApp.Interfaces
{
    public interface IInMemoryCategoryProvider
    {
        public Category Get(int Id);
        public ICollection<Category> GetAll();
        public void Create(Category data);
        public void Update(Category data, int Id);
        public void Delete(Category data, int Id);
    }
}
