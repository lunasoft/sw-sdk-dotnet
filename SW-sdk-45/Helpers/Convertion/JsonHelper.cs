using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace SW.Helpers.Convertion
{
    internal static class JsonHelper
    {
        internal static string SerializeJson<T>(T obj)
        {
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream stream = new MemoryStream())
            {
                js.WriteObject(stream, obj);
                stream.Position = 0;
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    string json = reader.ReadToEnd();
                    return RemoveInvalidJsonCharacters(json);
                }
            }
        }

        internal static T DeserializeJson<T>(string json)
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(T));
                return (T)deserializer.ReadObject(ms);
            }
        }

        private static string RemoveInvalidJsonCharacters(string json)
        {
            json = json.Replace("\\/", "/");
            json = json.Replace("\r\n", "");
            json = json.Replace("\n", "");
            return json;
        }
    }
}
