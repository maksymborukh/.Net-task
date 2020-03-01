using System;

namespace PathInJSON
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter path. The output file will be saved in this path folder and output on console.");

            string path = Console.ReadLine();
            Directory directory = new Directory();
            JSON json = new JSON();

            if (System.IO.Directory.Exists(path))
            {
                string jsonText = json.CreateJSONFromHierarchicalList(directory.CreateHierarchy(path));

                Console.WriteLine(jsonText);

                json.WriteToFile(path, jsonText);
            }
            else
            {
                Console.WriteLine("Folder doesn't exist!");
            }
        }


    }
}
