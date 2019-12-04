using System;
using System.IO;
using Newtonsoft.Json;
using Spider.Models;

namespace Spider
{
    public static class Settings
    {
        public static T LoadSettings<T>(string fileUri) where T : ISettings
        {
            T settings;
            using (var r = new StreamReader(fileUri))
            {
                var json = r.ReadToEnd();
                settings = JsonConvert.DeserializeObject<T>(json);
            }

            return settings;
        }

        public static bool SaveSettings<T>(string fileUri, T settings) where T : ISettings
        {
            var result = false;

            try
            {
                using (StreamWriter file = File.CreateText(fileUri))
                {
                    var serializer = new JsonSerializer();
                    //serialize object directly into file stream
                    serializer.Serialize(file, settings);
                    result = true;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                result = false;
            }

            return result;
        }
    }
}
