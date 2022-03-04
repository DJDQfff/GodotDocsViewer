using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Linq;
using MyStandard20Library;
using PoFileParser;
using static System.Console;
using static MyStandard20Library.ConsoleShow;

namespace RstFileParser
{
    public static class RstLineHelper
    {
        public static List<string> ConvertToTranslatedAllLines (this List<RstLine> rstLines, PoDictionary dic)
        {
            List<string> list = new List<string>();
            foreach (var rst in rstLines)
            {
                string key = rst.Content;

                string translated = key;

                if (!string.IsNullOrWhiteSpace(key))             // 跳过无效key
                {
                    translated = dic[key];

                    if (translated is null)
                    {
                        translated = rst.Content;                // 如果为null，则不翻译
                        WriteLine($"失败：\n{key}\n");
                    }
                }

                list.Add(rst.ConvertToString(translated));
            }
            return list;
        }

        /// <summary>
        /// 传入要翻译的文本，如果为空则用原句
        /// </summary>
        /// <param name="rstLine"></param>
        /// <param name="translatedcontent"></param>
        /// <returns></returns>
        public static string ConvertToString (this RstLine rstLine, string translatedcontent)
        {
            int count = rstLine.Index;
            StringBuilder stringBuilder = new StringBuilder();
            while (count-- > 0)
            {
                stringBuilder.Append(' ');
            }

            stringBuilder.Append(rstLine.Char);

            stringBuilder.Append(string.IsNullOrWhiteSpace(translatedcontent) ? rstLine.Content : translatedcontent);

            return stringBuilder.ToString();
        }
    }

    public static class RstLineFactory
    {
        /// <summary>
        /// 内容不变，以rstline返回，用于偷懒、懒得解析
        /// </summary>
        /// <param name="paragraph"></param>
        /// <returns></returns>
        public static List<RstLine> Origin (Paragraph paragraph)
        {
            List<RstLine> lines = new List<RstLine>();
            foreach (var para in paragraph.Lines)
            {
                lines.Add(RstLineFactory.Creat(0, string.Empty, para));
            }

            return lines;
        }

        public static RstLine Creat (int index, string chars, string content)
        {
            return new RstLine()
            {
                Index = index,
                Char = chars,
                Content = content
            };
        }

        public static RstLine CreatNewLine ()
        {
            return new RstLine() { Index = 0, Char = "\r\n", Content = string.Empty };
        }
    }

    /// <summary>
    /// 解析后的内容行，用于存储解析获得内容
    /// 第一个
    /// </summary>
    public class RstLine
    {
        /// <summary>
        /// 该行首字符起始索引
        /// </summary>
        public int Index { set; get; }

        /// <summary>
        /// 改行的标记符号，如列表标记 * 注释符号 #
        /// </summary>
        public string Char { set; get; }

        /// <summary>
        /// 该行的主体内容，旧的内容可能分成了多行存储，在新的里面都保存为一行
        /// </summary>
        public string Content { set; get; }
    }
}