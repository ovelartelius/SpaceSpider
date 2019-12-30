using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Spider;

namespace Crawler.Data
{
    public abstract class BaseDataHandler
    {
        public virtual void InitSource(string filePath, List<string> list)
        {
            if (!Json.SaveAsFile(filePath, list))
            {
                Console.WriteLine($"Could not init {filePath}");
            }
        }

        public virtual void DeleteSource(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Console.WriteLine($"Deleted file {filePath}");
            }
        }

        public virtual string Checkout(string indexFolder)
        {
            var stringList = this.LoadSource(indexFolder);
            var result = string.Empty;
            if (stringList != null && stringList.Any())
            {
                result = stringList.First();
            }
            return result;
        }

        public virtual List<string> LoadSource(string filePath)
        {
            var stringList = Json.LoadJson<List<string>>(filePath);
            return stringList;
        }

        public virtual void SaveSource(string filePath, List<string> list)
        {
            if (!Json.SaveAsFile(filePath, list))
            {
                Console.WriteLine($"Could not save {filePath}");
            }
        }
    }
}
