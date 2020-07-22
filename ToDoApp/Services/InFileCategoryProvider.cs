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
    public class InFileCategoryProvider : IInFileCategoryProvider
    {
        private const string file = @"Categories.json";
        public  List<Category> ReadCategoriesFromJsonData(string file)
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
        public  void WriteCategoriesToJsonData(string file, List<Category> list)
        {
            using (StreamWriter w = new StreamWriter(file, false))
            {
                string json = JsonConvert.SerializeObject(list, Formatting.Indented);
                w.Write(json);
            }
        }
        public ICollection<Category> GetAll()
        {
            List<Category> categories = ReadCategoriesFromJsonData(file);
            return categories;
        }
        public  Category Get(int id)
        {
            List<Category> categories = ReadCategoriesFromJsonData(file);
            Category category = categories.FirstOrDefault(c => c.Id == id);
            if(category != null)
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
            List<Category> categories = ReadCategoriesFromJsonData(file);
            bool CategoryExists = categories.Any(item => item.Equals(data));
            if (!CategoryExists)
            {
                categories.Add(data);
                WriteCategoriesToJsonData(file, categories);
            }
            else
            {
                throw new ArgumentException();
            }
        }
        public  void Update(Category data, int id)
        {
            List<Category> categories = ReadCategoriesFromJsonData(file);
            Category categoryToBeUpdated = categories.FirstOrDefault(c => c.Id == id);
            if(categoryToBeUpdated != null)
            {
                categoryToBeUpdated.Name = data.Name;
                WriteCategoriesToJsonData(file, categories);
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }
        public  void Delete(Category data, int id)
        {
            List<Category> categories = ReadCategoriesFromJsonData(file);
            Category categoryToBeDeleted = categories.FirstOrDefault(c => c.Id == id);
            if(categoryToBeDeleted != null)
            {
                categories.Remove(categoryToBeDeleted);
                WriteCategoriesToJsonData(file, categories);
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }
    }
}
