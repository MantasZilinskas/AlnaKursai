using System.Collections.Generic;
using ToDoApp.Models;

namespace ToDoApp.Interfaces
{
    public interface IInFileCategoryProvider
    {
        public Category Get(int id);
        public ICollection<Category> GetAll();
        public void Create(Category data);
        public void Update(Category data, int id);
        public void Delete(Category data, int id);
    }
}
