using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace GMaps_to_Galileo
{
    internal class Program
    {
        private static Dictionary<string, string> iconsMap = new Dictionary<string, string>()
        {
            {"icon-1085", "BookmarkStyle_6"},
            {"icon-1081", "BookmarkStyle_6"},
            {"icon-1101", "BookmarkStyle_10"},
            {"icon-1143", "BookmarkStyle_21"},
            {"icon-1197", "BookmarkStyle_9"},
            {"icon-1203", "BookmarkStyle_49"},
            {"icon-1417", "BookmarkStyle_12"},
            {"icon-1423", "BookmarkStyle_40"},
            {"icon-1459", "BookmarkStyle_24"},
            {"icon-1379", "BookmarkStyle_15"},
            {"icon-960-F4EB37", "BookmarkStyle_27"},
            {"icon-960-FF8277", "BookmarkStyle_0"},
            {"icon-961-FF8277", "BookmarkStyle_20"},
            {"icon-991","BookmarkStyle_14"},
            {"icon-1165","BookmarkStyle_21"},
        };

        private static void Main(string[] args)
        {
            if (args.Count() < 1)
            {
                Console.WriteLine("Enter input .kml file");
                return;
            }

            var inputFileName = args[0];

            var outputFileName = args.Count() > 1 ? args[1] : String.Format("{0}_output.{1}", Path.GetFileNameWithoutExtension(inputFileName), Path.GetExtension(inputFileName));

            var inputFilePath = Path.Combine(Directory.GetCurrentDirectory(), inputFileName);
            if (!File.Exists(inputFilePath))
            {
                Console.WriteLine("Input file not found");
                return;
            }

            string fileContents;

            using (var fr = File.OpenRead(inputFilePath))
            {
                using (var sr = new StreamReader(fr))
                {
                    fileContents = sr.ReadToEnd();
                    foreach (var iconPair in iconsMap)
                    {
                        fileContents = fileContents.Replace(iconPair.Key, iconPair.Value);
                    }
                }
            }

            var outputFilePath = Path.Combine(Directory.GetCurrentDirectory(), outputFileName);
            using (var fw = File.Create(outputFilePath))
            {
                using (var sw = new StreamWriter(fw))
                {
                    sw.Write(fileContents);
                }
            }

            Console.WriteLine("Output saved to " + outputFileName);
        }
    }
}
