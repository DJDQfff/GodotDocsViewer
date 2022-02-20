using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

using MyStandard20Library;

namespace PoFileParser
{
    public static class Parser
    {
        public static List<TranslatePair> Parse (IList<string> lines)
        {
            List<Paragraph> paragraphs = lines.SplitParagraphByEmptyLines(true);
            List<TranslatePair> pairs = new List<TranslatePair>();

            foreach (var pa in paragraphs)
            {
                TranslatePair translatePair = new TranslatePair(pa);
                pairs.Add(translatePair);

#if DEBUG
                //Console.WriteLine(translatePair.Msgid);
                //Console.WriteLine(translatePair.Msgstr);
                //Console.WriteLine();
                //Console.ReadKey();
#endif
            }

            pairs.RemoveAt(0); // 第一段是头文件,移除
            return pairs;
        }

        public static Dictionary<string, string> TransformToDictionary (this List<TranslatePair> translatePairs)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            foreach (var pair in translatePairs)
            {
                keyValuePairs.Add(pair.Msgid, pair.Msgstr);
            }
            return keyValuePairs;
        }
    }
}