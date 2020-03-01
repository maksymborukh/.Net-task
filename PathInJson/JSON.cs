using System;
using System.Text.Json;

namespace PathInJSON
{
    public class JSON
    {
        public JSON() { }

        public string CreateJSONFromHierarchicalList<T>(T obj)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(obj, options);

            return json;
        }

        public void WriteToFile(string path, string text)
        {
            try
            {
                System.IO.File.WriteAllText(path + "\\output.json", text);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Can not write to file. \nError ", ex.Message);
            }
        }
    }
}
