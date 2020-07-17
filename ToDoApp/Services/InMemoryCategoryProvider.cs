using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Interfaces;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    public class InMemoryCategoryProvider : IInMemoryCategoryProvider
    {
        private static List<Category> values = new List<Category>();

        public void Create(Category data)
        {
            Category category = values.FirstOrDefault(value => value.Equals(data));
            if (category == null)
            {
                values.Add(data);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public void Delete(Category data, int Id)
        {
            Category category = values.FirstOrDefault(value => value.Id == Id);
            if (category != null)
            {
                values.Remove(category);
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public Category Get(int Id)
        {
            Category category = values.FirstOrDefault(value => value.Id == Id);
            if (category != null)
            {
                return category;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public ICollection<Category> GetAll()
        {
            return values;
        }

        public void Update(Category data, int Id)
        {
            Category category = values.FirstOrDefault(value => value.Id == Id);
            if (category != null)
            {
                category.Name = data.Name;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }
    }
}
