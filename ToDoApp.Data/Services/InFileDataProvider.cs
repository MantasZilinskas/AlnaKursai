
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Data.Interfaces;

namespace TodoApp.Data.Services
{
    public class InFileDataProvider<TDataClass> : IFileReaderWriter<TDataClass>, IDataProvider<TDataClass> where TDataClass : IHasId
    {
        private string file = typeof(TDataClass).Name + ".json";
        public List<TDataClass> ReadFromFile(string file)
        {
            List<TDataClass> result = new List<TDataClass>();
            if (File.Exists(file))
            {
                using (StreamReader r = new StreamReader(file))
                {
                    string jsonData = r.ReadToEnd();
                    if (jsonData.Any())
                    {
                        result = JsonConvert.DeserializeObject<List<TDataClass>>(jsonData);
                    }
                }
            }
            return result;
        }
        public void WriteToFile(string file, List<TDataClass> list)
        {
            using (StreamWriter w = new StreamWriter(file, false))
            {
                string json = JsonConvert.SerializeObject(list, Formatting.Indented);
                w.Write(json);
            }
        }
        public ICollection<TDataClass> GetAll()
        {
            List<TDataClass> items = ReadFromFile(file);
            return items;
        }
        public TDataClass Get(int id)
        {
            List<TDataClass> items = ReadFromFile(file);
            TDataClass TDataClass = items.FirstOrDefault(c => c.Id == id);
            if (TDataClass != null)
            {
                return TDataClass;
            }
            else
            {
                throw new KeyNotFoundException();
            }

        }
        public void Create(TDataClass data)
        {
            List<TDataClass> items = ReadFromFile(file);
            data.Id = items.Count();
            bool TDataClassExists = items.Any(item => item.Id == data.Id);
            if (!TDataClassExists)
            {
                items.Add(data);
                WriteToFile(file, items);
            }
            else
            {
                throw new ArgumentException();
            }
        }
        public void Update(TDataClass data)
        {
            List<TDataClass> items = ReadFromFile(file);
            TDataClass item = items.FirstOrDefault(i => i.Id == data.Id);
            if (item != null)
            {
                items.Remove(item);
                items.Add(data);
                WriteToFile(file, items);
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }
        public void Delete(int id)
        {
            List<TDataClass> items = ReadFromFile(file);
            TDataClass item = items.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                items.Remove(item);
                WriteToFile(file, items);
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }
    }
}
