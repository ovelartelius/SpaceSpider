using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading;

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

            var fileNotSaved = true;
            var iteration = 0;
            while (fileNotSaved || iteration > 15)
            {
                try
                {
                    using (StreamWriter file = File.CreateText(fileUri))
                    {
                        var serializer = new JsonSerializer();
                        //serialize object directly into file stream
                        serializer.Serialize(file, theObject);
                        result = true;
                        fileNotSaved = false;
                    }
                }
                catch (Exception e)
                {
                    var rnd = new Random(DateTime.Now.Millisecond);
                    int waitMs = rnd.Next(30, 1000);

                    Console.WriteLine(e.Message);
                    Console.WriteLine($"Could not save {fileUri}");
                    Console.WriteLine($"Will try again in {waitMs} ms");
                    Thread.Sleep(waitMs);
                }

                iteration++;
            }

            if (fileNotSaved)
            {
                throw new ApplicationException($"Failed to save file {fileUri}.");
            }

            return result;
        }

        public static T LoadJson<T>(string fileUri)
        {
            var jsonObject = default(T);

            var fileLoaded = false;
            var iteration = 0;
            while (!fileLoaded || iteration > 15)
            {
                try
                {
                    using (TextReader notesReader = new StreamReader(fileUri))
                    {
                        jsonObject = JsonConvert.DeserializeObject<T>(notesReader.ReadToEnd());
                        fileLoaded = true;
                    }
                }
                catch (Exception e)
                {
                    var rnd = new Random(DateTime.Now.Millisecond);
                    int waitMs = rnd.Next(30, 1000);

                    Console.WriteLine(e.Message);
                    Console.WriteLine($"Could not load {fileUri}");
                    Console.WriteLine($"Will try again in {waitMs} ms");
                    Thread.Sleep(waitMs);
                }

                iteration++;
            }

            if (!fileLoaded)
            {
                throw new ApplicationException($"Failed to load file {fileUri}.");
            }

            return jsonObject;
        }
    }
}
