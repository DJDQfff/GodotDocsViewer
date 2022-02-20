using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MyStandard20Library;

namespace RstFileParser
{
    public static class Class1
    {
        public static List<Paragraph> Main (Stream stream)
        {
            List<string> lines = StreamOperation.ReadAllLines(stream);

            List<Paragraph> paragraphs = lines.SplitParagraphByEmptyLines(true);
            return paragraphs;
        }
    }
}