using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

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
                string value = rst.Content;

                if (rst.NeedTranslate)
                {
                    value = dic[value];

                    if (value is null)
                    {
                        value = rst.Content;                // 如果为null，则不翻译
                        //WriteLine($"失败：\n{rst.Content}\n");
                        //ReadLine();
                    }
                    else
                    {
                        //WriteLine("成功");
                        //WriteLine(rst.Content);
                        //WriteLine(key);
                    }
                }
                list.Add(rst.ConvertToString(value));
            }
            return list;
        }

        /// <summary> 传入要翻译的文本，如果为空则用原句 </summary>
        /// <param name="rstLine"> </param>
        /// <param name="translatedcontent">
        /// 使用此内容替换rstline的content
        /// </param>
        /// <returns> </returns>
        public static string ConvertToString (this RstLine rstLine, string translatedcontent)
        {
            int count = rstLine.Indent;
            StringBuilder stringBuilder = new StringBuilder();
            while (count-- > 0)
            {
                stringBuilder.Append(' ');
            }

            stringBuilder.Append(rstLine.StartChar);

            stringBuilder.Append(string.IsNullOrWhiteSpace(translatedcontent) ? rstLine.Content : translatedcontent);

            return stringBuilder.ToString();
        }
    }

    public static class RstLineFactory
    {
        public static List<RstLine> Origin<T> (IList<T> paragraphs) where T : IParagraph
        {
            List<RstLine> rstLines = new List<RstLine>();
            foreach (var para in paragraphs)
            {
                var lines = RstLineFactory.Origin(para);
                var newline = RstLineFactory.CreatNewLine();

                rstLines.AddRange(lines);
                rstLines.Add(newline);
            }
            return rstLines;
        }

        /// <summary>
        /// 段落内容保持不变，以原本形式输出。
        /// 例如，原本多行应该合并为一行的段落，使用origin方法，则保持多行输出
        /// </summary>
        /// <param name="paragraph"> </param>
        /// <returns> </returns>
        public static List<RstLine> Origin (IParagraph paragraph)
        {
            List<RstLine> lines = new List<RstLine>();
            foreach (var line in paragraph.Lines)
            {
                lines.Add(RstLineFactory.CustomCreat(0, string.Empty, line, false));
            }

            return lines;
        }

        /// <summary> 自定义创建 </summary>
        /// <param name="index"> </param>
        /// <param name="chars"> </param>
        /// <param name="content"> </param>
        /// <param name="totranslate"> </param>
        /// <returns> </returns>
        public static RstLine CustomCreat (int index, string chars, string content, bool totranslate)
        {
            return new RstLine()
            {
                Indent = index,
                StartChar = chars,
                Content = content,
                NeedTranslate = totranslate
            };
        }

        /// <summary> 创建一个换行RstLine </summary>
        /// <returns> </returns>
        public static RstLine CreatNewLine ()
        {
            return new RstLine() { Indent = 0, StartChar = "\r\n", Content = string.Empty, NeedTranslate = false };
        }
    }

    /// <summary> 合并原来的多行文字到一个完整的行上去 </summary>
    public class RstLine
    {
        /// <summary> 该行首字符起始索引，用来设置缩进 </summary>
        public int Indent { set; get; }

        /// <summary> 该行的起始标记符号，如列表标记 * ，注释符号 # </summary>
        public string StartChar { set; get; }

        /// <summary>
        /// 该行的主体内容，旧的内容可能分成了多行存储，在新的里面都保存为一行
        /// </summary>
        public string Content { set; get; }

        /// <summary>
        /// 指示该Content是否是有效的文字内容，如果是正常文字，则设为true。如果是Sphinx的段落标记啥的，则设为false
        /// </summary>
        public bool NeedTranslate { set; get; }

        /// <summary> 该行的结束标记符号，如Comment的后面有一个点 . </summary>
        public string EndChar { set; get; }
    }
}