using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace net_rogue
{
    internal class MapLoader
    {
        public Map LoadMapFromFile(string filename)
        {

            bool fileFound = File.Exists(filename);
            if (fileFound == false)
            {
                Console.WriteLine($"File {filename} not found");
                return LoadTestMap(); // Return the test map as fallback
            }

            string fileContents;

            using (StreamReader reader = File.OpenText(filename))
            {
     
                fileContents = File.ReadAllText(filename);
            }

            // HIGLY FUTILE: How to convert the file contents to a Map object?
            Map loadedMap = JsonConvert.DeserializeObject<Map>(fileContents); ;
            
            return loadedMap;
        }
        public void TestFileReading(string filename)
        {
            // NOTE: This is just a test, it does not load a map from the file yet

            using (StreamReader reader = File.OpenText(filename))
            {
                Console.WriteLine("File contents:");
                Console.WriteLine();

                string line;
                while (true)
                {
                    line = reader.ReadLine();
                    if (line == null)
                    {
                        break; // End of file
                    }
                    Console.WriteLine(line);
                }
            }

        }

        public Map LoadTestMap()
        {
            Map Test = new Map();
            Test.mapWidth = 8;
            Test.mapTiles = new int[] {
            2, 2, 2, 2, 2, 2, 2, 2,
            2, 1, 1, 2, 1, 1, 1, 2,
            2, 1, 1, 2, 1, 1, 1, 2,
            2, 1, 1, 1, 1, 1, 2, 2,
            2, 2, 2, 2, 1, 1, 1, 2,
            2, 1, 1, 1, 1, 1, 1, 2,
            2, 1, 1, 1, 1, 1, 1, 2,
            2, 2, 2, 2, 2, 2, 2, 2 };
            return Test;
        }
    }
}
