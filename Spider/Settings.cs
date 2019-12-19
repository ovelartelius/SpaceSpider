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

                result = true;
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
			var registryKey = Registry.CurrentUser.OpenSubKey("SpaceSpider", true);
			if (registryKey == null)
			{
				registryKey = Registry.CurrentUser.CreateSubKey("SpaceSpider");
			}

			if (registryKey != null)
			{
				registryKey.SetValue(key.ToString(), value);
				registryKey.Close();
			}
		}

		public static string LoadRegistrySetting(string key)
		{
			var result = string.Empty;
			var registryKey = Registry.CurrentUser.OpenSubKey("SpaceSpider");
			try
			{
                if (registryKey != null)
                {
                    result = registryKey.GetValue(key.ToString()).ToString();
                }
			}
			catch
			{
				result = string.Empty;
			}
			return result;
		}
	}
}
