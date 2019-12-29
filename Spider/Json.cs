using Newtonsoft.Json;
using System;
using System.IO;

namespace Spider
{
    public static class Json
    {
        public static string ToJson(this object theObject)
        {
            return JsonConvert.SerializeObject(theObject);
        }

        public static bool SaveAsFile<T>(string fileUri, T theObject)
        {
            var result = false;

            try
            {
                using (StreamWriter file = File.CreateText(fileUri))
                {
                    var serializer = new JsonSerializer();
                    //serialize object directly into file stream
                    serializer.Serialize(file, theObject);
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

        public static T LoadJson<T>(string fileUri)
        {
            var jsonObject = default(T);

            try
            {
                jsonObject = JsonConvert.DeserializeObject<T>(File.ReadAllText(fileUri));

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return jsonObject;
        }
    }
}
