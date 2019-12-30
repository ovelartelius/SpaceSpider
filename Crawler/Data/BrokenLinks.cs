using System;
using System.Collections.Generic;
using Crawler.Models;
using Spider;

namespace Crawler.Data
{
    public class BrokenLinks : BaseDataHandler, IRelatedListDataSource
    {
        public void InitSource(string indexFolder)
        {
            SaveSource(indexFolder, new List<RelatedLink>());
        }

        public override void DeleteSource(string indexFolder)
        {
            base.DeleteSource(GetFilePath(indexFolder));
        }

        public void Add(string indexFolder, RelatedLink link)
        {
            var ignoredLinks = LoadSource(indexFolder);

            ignoredLinks.Add(link);

            SaveSource(indexFolder, ignoredLinks);
        }

        public void Remove(string indexFolder, RelatedLink link)
        {
            var ignoredLinks = LoadSource(indexFolder);

            ignoredLinks.Remove(link);

            SaveSource(indexFolder, ignoredLinks);
        }

        public bool Exist(string indexFolder, RelatedLink link)
        {
            var exists = false;
            var ignoredLinks = LoadSource(indexFolder);

            if (ignoredLinks.Contains(link))
            {
                exists = true;
            }

            return exists;
        }

        public int Count(string indexFolder)
        {
            var list = LoadSource(indexFolder);

            var count = list.Count;

            return count;
        }

        public List<RelatedLink> LoadSource(string indexFolder)
        {
            var filePath = GetFilePath(indexFolder);
            var stringList = Json.LoadJson<List<RelatedLink>>(filePath);
            return stringList;
        }

        public void SaveSource(string indexFolder, List<RelatedLink> list)
        {
            var filePath = GetFilePath(indexFolder);
            if (!Json.SaveAsFile(filePath, list))
            {
                Console.WriteLine($"Could not save {filePath}");
            }
        }

        private string GetFilePath(string indexFolder)
        {
            var filePath = $"{indexFolder}\\_brokenlinks.json";
            return filePath;
        }
    }
}
