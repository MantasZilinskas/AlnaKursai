using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp.Interfaces
{
    public interface IFileReaderWriter<TDataClass>
    {
        public List<TDataClass> ReadFromFile(string file);
        public void WriteToFile(string file, List<TDataClass> data);
    }
}
