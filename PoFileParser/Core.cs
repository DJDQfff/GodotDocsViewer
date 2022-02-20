using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using MyStandard20Library;
namespace PoFileParser
{
    public class Core
    {
        public static TranslatePair[] Main (Stream stream)
        {
            List<string> lines = StreamOperation.ReadAllLines(stream);
            var a = Parser.Parse(lines);
            return a.ToArray();
        }

        public static Dictionary<string, string> GetDictionary (Stream stream)
        {
            List<string> lines = StreamOperation.ReadAllLines(stream);

            var a = Parser.Parse(lines);
            return a.TransformToDictionary();
        }

        public static Dictionary<string, string> GetDictionary (string path)
        {
            var lines = File.ReadAllLines(path);

            var a = Parser.Parse(lines);
            return a.TransformToDictionary();
        }
    }
}