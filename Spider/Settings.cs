using System;
using System.IO;
using Microsoft.Win32;
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
                Json.SaveAsFile<T>(fileUri, settings);

                //using (StreamWriter file = File.CreateText(fileUri))
                //{
                //    var serializer = new JsonSerializer();
                //    //serialize object directly into file stream
                //    serializer.Serialize(file, settings);
                    result = true;
                //}

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                result = false;
            }

            return result;
        }

		public static void SaveRegistrySetting(string key, string value)
		{
			var regkey = Registry.CurrentUser.OpenSubKey("SpaceSpider", true);
			if (regkey == null)
			{
				regkey = Registry.CurrentUser.CreateSubKey("SpaceSpider");
			}

			if (regkey != null)
			{
				regkey.SetValue(key.ToString(), value);
				regkey.Close();
			}
		}

		public static string LoadRegistrySetting(string key)
		{
			var result = string.Empty;
			var regkey = Registry.CurrentUser.OpenSubKey("SpaceSpider");
			try
			{
				result = regkey.GetValue(key.ToString()).ToString();
			}
			catch
			{
				result = string.Empty;
			}
			return result;
		}
	}
}
