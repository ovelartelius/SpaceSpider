using System.Collections.Generic;

namespace Crawler.Data
{
    public class NotCrawled : BaseDataHandler, IStringListDataSource
    {
        public void InitSource(string indexFolder)
        {
            base.InitSource(GetFilePath(indexFolder), new List<string>());
        }

        public override void DeleteSource(string indexFolder)
        {
            base.DeleteSource(GetFilePath(indexFolder));
        }

        public void Add(string indexFolder, string link)
        {
            var ignoredLinks = LoadSource(indexFolder);

            ignoredLinks.Add(link);

            SaveSource(indexFolder, ignoredLinks);
        }

        public void Remove(string indexFolder, string link)
        {
            var ignoredLinks = LoadSource(indexFolder);

            ignoredLinks.Remove(link);

            SaveSource(indexFolder, ignoredLinks);
        }

        public bool Exist(string indexFolder, string link)
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
            var ignoredLinks = LoadSource(indexFolder);

            var count = ignoredLinks.Count;

            return count;
        }

        public override string Checkout(string indexFolder)
        {
            return base.Checkout(indexFolder);
        }

        public override List<string> LoadSource(string indexFolder)
        {
            var ignoredLinks = base.LoadSource(GetFilePath(indexFolder));
            return ignoredLinks;
        }

        public override void SaveSource(string indexFolder, List<string> list)
        {
            base.SaveSource(GetFilePath(indexFolder), list);
        }

        private string GetFilePath(string indexFolder)
        {
            var filePath = $"{indexFolder}\\_notcrawledurls.json";
            return filePath;
        }
    }
}
