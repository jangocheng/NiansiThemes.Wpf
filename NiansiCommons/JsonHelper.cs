using Newtonsoft.Json;
using System.IO;

namespace NiansiCommons
{
    public static class JsonHelper
    {
        public static T JsonReader<T>(JsonSerializer serializer, string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                JsonTextReader reader = new JsonTextReader(sr);
                return serializer.Deserialize<T>(reader);
            }
        }

        public static T JsonReader<T>(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                JsonTextReader reader = new JsonTextReader(sr);
                return serializer.Deserialize<T>(reader);
            }
        }
    }
}
