using System.Collections.Generic;
using Crawler.Models;

namespace Crawler.Data
{
    public interface IRelatedListDataSource
    {
        void InitSource(string indexFolder);
        void DeleteSource(string indexFolder);
        void Add(string indexFolder, RelatedLink value);
        void Remove(string indexFolder, RelatedLink value);
        bool Exist(string indexFolder, RelatedLink value);
        int Count(string indexFolder);
        List<string> LoadSource(string indexFolder);
        void SaveSource(string indexFolder, List<RelatedLink> list);
    }
}
