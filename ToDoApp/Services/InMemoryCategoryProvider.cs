using System;
using System.Collections.Generic;
using System.Linq;
using ToDoApp.Interfaces;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    public class InMemoryCategoryProvider : IDataProvider<Category>
    {
        private static List<Category> values = new List<Category>();

        public void Create(Category data)
        {
            data.Id = values.Count();
            Category category = values.FirstOrDefault(value => value.Name == data.Name);
            if (category == null)
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
            Category category = values.FirstOrDefault(value => value.Id == id);
            if (category != null)
            {
                values.Remove(category);
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public Category Get(int id)
        {
            Category category = values.FirstOrDefault(value => value.Id == id);
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

        public void Update(Category data)
        {
            Category category = values.FirstOrDefault(value => value.Id == data.Id);
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
