using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

using MyStandard20Library;

namespace PoFileParser
{
    public static class Factory
    {
        public static PoDictionary Creat (string filepath)
        {
            var lines = File.ReadAllLines(filepath);
            var a = Parser.Parse(lines);
            return new PoDictionary(a);
        }

        public static PoDictionary Creat (Stream stream)
        {
            List<string> lines = StreamOperation.ReadAllLines(stream);
            var a = Parser.Parse(lines);
            return new PoDictionary(a);
        }

        public static PoDictionary Creat (IList<string> lines)
        {
            var a = Parser.Parse(lines);
            return new PoDictionary(a);
        }
    }
}