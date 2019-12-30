using System.Collections.Generic;

namespace Crawler.Data
{
    public interface IStringListDataSource
    {
        void InitSource(string indexFolder);
        void DeleteSource(string indexFolder);
        void Add(string indexFolder, string value);
        void Remove(string indexFolder, string value);
        bool Exist(string indexFolder, string value);
        int Count(string indexFolder);
        string Checkout(string indexFolder);
        List<string> LoadSource(string indexFolder);
        void SaveSource(string indexFolder, List<string> list);
    }
}
