using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

using MyStandard20Library;

namespace RstFileParser
{
    public static class RstLineHelper
    {
        public static List<string> ConvertToAllLines (this List<RstLine> rstLines)
        {
            List<string> list = new List<string>();
            foreach (var rst in rstLines)
            {
                list.Add(rst.ConvertToString());
            }
            return list;
        }

        public static string ConvertToString (this RstLine rstLine)
        {
            int count = rstLine.Index;
            StringBuilder stringBuilder = new StringBuilder();
            while (count-- > 0)
            {
                stringBuilder.Append(' ');
            }
            stringBuilder.Append(rstLine.Char);
            stringBuilder.Append(rstLine.Content);

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