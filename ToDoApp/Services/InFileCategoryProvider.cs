using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ToDoApp.Models;
using Newtonsoft.Json;
using System.Globalization;
using System.Collections;
using ToDoApp.Interfaces;
using Microsoft.AspNetCore.Razor.Language.Extensions;

namespace ToDoApp.Services
{
    public class InFileCategoryProvider : IDataProvider<Category>, IFileReaderWriter<Category>
    {
        private const string file = nameof(Category) + ".json";
        public List<Category> ReadFromFile(string file)
        {
            List<Category> result = new List<Category>();
            if (File.Exists(file))
            {
                using (StreamReader r = new StreamReader(file))
                {
                    string jsonData = r.ReadToEnd();
                    if (jsonData.Any())
                    {
                        result = JsonConvert.DeserializeObject<List<Category>>(jsonData);
                    }
                }
            }
            return result;
        }
        public void WriteToFile(string file, List<Category> list)
        {
            using (StreamWriter w = new StreamWriter(file, false))
            {
                string json = JsonConvert.SerializeObject(list, Formatting.Indented);
                w.Write(json);
            }
        }
        public ICollection<Category> GetAll()
        {
            List<Category> categories = ReadFromFile(file);
            return categories;
        }
        public Category Get(int id)
        {
            List<Category> categories = ReadFromFile(file);
            Category category = categories.FirstOrDefault(c => c.Id == id);
            if (category != null)
            {
                return category;
            }
            else
            {
                throw new KeyNotFoundException();
            }

        }
        public void Create(Category data)
        {
            List<Category> categories = ReadFromFile(file);
            data.Id = categories.Count();
            bool CategoryExists = categories.Any(item => item.Name == data.Name);
            if (!CategoryExists)
            {
                categories.Add(data);
                WriteToFile(file, categories);
            }
            else
            {
                throw new ArgumentException();
            }
        }
        public void Update(Category data)
        {
            List<Category> categories = ReadFromFile(file);
            Category categoryToBeUpdated = categories.FirstOrDefault(c => c.Id == data.Id);
            if (categoryToBeUpdated != null)
            {
                categoryToBeUpdated.Name = data.Name;
                WriteToFile(file, categories);
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }
        public void Delete(int id)
        {
            List<Category> categories = ReadFromFile(file);
            Category categoryToBeDeleted = categories.FirstOrDefault(c => c.Id == id);
            if (categoryToBeDeleted != null)
            {
                categories.Remove(categoryToBeDeleted);
                WriteToFile(file, categories);
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }
    }
}
