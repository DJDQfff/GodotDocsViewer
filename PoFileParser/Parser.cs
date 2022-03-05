using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

using MyStandard20Library;

namespace PoFileParser
{
    internal static class Parser
    {
        public static Dictionary<string, string> Parse (IList<string> lines)
        {
            List<string> newlist = new List<string>(lines);
            newlist.RemoveItemMatchRegex(@"^#");
            List<Paragraph> paragraphs = lines.SplitParagraphByEmptyLines();
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

            // TODO 存在隐患
            // 第一段是头文件，不能用于字典

            pairs.RemoveAt(0);

            return pairs.TransformToDictionary();
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