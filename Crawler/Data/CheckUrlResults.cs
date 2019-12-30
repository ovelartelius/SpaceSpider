using Spider;
using Spider.Extensions;
using Spider.Models;

namespace Crawler.Data
{
    public class CheckUrlResults
    {
        public CheckUrlResult Load(string filePath)
        {
            return Json.LoadJson<CheckUrlResult>(filePath);
        }

        public string Save(string indexFolder, CheckUrlResult checkUrlResult)
        {
            var filePath = GetFilePath(indexFolder, checkUrlResult);
            Json.SaveAsFile<CheckUrlResult>(filePath, checkUrlResult);

            return filePath;
        }

        private string GetFilePath(string indexFolder, CheckUrlResult checkUrlResult)
        {
            var filePath = $"{indexFolder}\\{checkUrlResult.Uri.AbsoluteUri.ToFileSafeName()}.json";
            return filePath;
        }
    }
}
