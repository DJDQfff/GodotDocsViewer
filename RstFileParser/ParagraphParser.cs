using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Linq;
using MyStandard20Library;

using System.Text.RegularExpressions;

namespace RstFileParser
{
    /// <summary>
    /// 对段落进行解析
    /// </summary>
    public static class ParagraphParser
    {
        /// <summary>
        /// 对外的统一调用
        /// </summary>
        /// <param name="paragraph"></param>
        /// <returns></returns>
        public static List<RstLine> Core (Paragraph paragraph)
        {
            List<RstLine> rstLines = new List<RstLine>();
            if (paragraph.IsOrderList())
            {
                var list = ParseOrderList(paragraph);
                rstLines.AddRange(list);
                return rstLines;
            }
            if (paragraph.IsTitle())
            {
                var list = ParseTitle(paragraph);
                rstLines.AddRange(list);
                return rstLines;
            }
            if (paragraph.IsTable())
            {
                var list = ParseTable(paragraph);
                rstLines.AddRange(list);
                return rstLines;
            }
            if (paragraph.IsCommand())
            {
                var list = ParseCommand(paragraph);
                rstLines.AddRange(list);
                return rstLines;
            }
            /* 对Command进行更加详细的分析，
             * 对语法还不太会，暂时不弄
            if (paragraph.IsCommandSingleLine())
            {
                var list = ParseCommand(paragraph);
                rstLines.AddRange(list);
                return rstLines;
            }
            if (paragraph.IsCommandLineWithContent())
            {
                var list = ParseCommand(paragraph);
                rstLines.AddRange(list);
                return rstLines;
            }
            */
            if (paragraph.IsRef())
            {
                var list = ParseRef(paragraph);
                rstLines.AddRange(list);
                return rstLines;
            }
            // 都不是的情况下，  默认为正文内容
            var rstline = paragraph.ParseBody();
            return rstline;
        }

        #region 具体每个类型的解析方法

        /// <summary>
        ///
        /// </summary>
        /// <param name="paragraph"></param>
        /// <returns></returns>
        private static List<RstLine> ParseOrderList (this Paragraph paragraph)
        {
            List<RstLine> rstLines = new List<RstLine>();

            var TopListLineIndex = paragraph.RegexIndexList(RstRegex.OrderList);
            var OrderListItemes = paragraph.Lines.SplitParagraphByStartIndex(TopListLineIndex.ToArray());
            for (int lineindex = 0; lineindex < TopListLineIndex.Count; lineindex++)
            {
                var currentItem = OrderListItemes[lineindex];
                var match = Regex.Match(currentItem.Lines[0], RstRegex.OrderList);
                var firstlinecontentstart = match.Index + match.Length; // 第一行排除列表符后的首个字符索引

                if (currentItem.Lines.Count == 1)            // 一个列表项占一行
                {
                    string line = currentItem.Lines[0];
                    string content = line.Substring(firstlinecontentstart);
                    RstLine rstLine = RstLineFactory.Creat(match.Index, match.Value, content);
                    rstLines.Add(rstLine);
                }
                else                                              // 一项占多行，
                {
                    var secondline = currentItem.Lines[1];

                    int secondlinestart = secondline.FirstNotWhiteSpaceCharacterIndex();
                    if (firstlinecontentstart == secondlinestart)           // 多行仍为相同缩进
                    {
                        string source = currentItem.Lines.RstFileSpecialConnectString();
                        string strin = Regex.Replace(source, RstRegex.OrderList, string.Empty);
                        //var strin = source.Substring(firstlinecontentstart);            // 把前面的列表符消掉
                        RstLine rstLine = RstLineFactory.Creat(match.Index, match.Value, strin);
                        rstLines.Add(rstLine);
                    }
                    else   // 多行情况下，可能存在不同缩进，分成了不同句，下面的写法只能适用于只用一次不同缩进的情况

                    {
                        string source = currentItem.Lines[0];
                        string str = source.Substring(firstlinecontentstart);
                        RstLine rstLine = RstLineFactory.Creat(match.Index, match.Value, str);
                        rstLines.Add(rstLine);

                        List<string> vs = currentItem.Lines.SubList(1, currentItem.Lines.Count - 1);
                        string aaa = vs.RstFileSpecialConnectString();
                        RstLine rstLine1 = RstLineFactory.Creat(secondlinestart, string.Empty, aaa);
                        rstLines.Add(rstLine1);
                    }
                }
            }
            return rstLines;
        }

        /// <summary>
        /// 解析命令段落，对于单行，或多行，都调用这个方法
        /// 原样返回，不用管
        /// </summary>
        /// <param name="paragraph"></param>
        /// <returns></returns>
        private static List<RstLine> ParseCommand (this Paragraph paragraph)
        {
            return RstLineFactory.Origin(paragraph);
        }

        /// <summary>
        /// 解析表格
        /// </summary>
        /// <param name="paragraph"></param>
        /// <returns></returns>
        private static List<RstLine> ParseTable (this Paragraph paragraph)
        {
            // TODO： 表格以后再说吧，懒得弄了
            return RstLineFactory.Origin(paragraph);
        }

        /// <summary>
        /// 解析引用段落
        /// 不用管，原样返回
        /// </summary>
        /// <param name="paragraph"></param>
        /// <returns></returns>
        private static List<RstLine> ParseRef (this Paragraph paragraph)
        {
            return RstLineFactory.Origin(paragraph);
        }

        /// <summary>
        /// 解析正文
        /// </summary>
        /// <param name="paragraph"></param>
        /// <returns></returns>
        private static List<RstLine> ParseBody (this Paragraph paragraph)
        {
            List<RstLine> lines = new List<RstLine>();
            int index = paragraph.Lines[0].FirstNotWhiteSpaceCharacterIndex();
            string str = paragraph.Lines.RstFileSpecialConnectString();

            lines.Add(RstLineFactory.Creat(index, string.Empty, str));
            return lines;
        }

        /// <summary>
        /// 返回Title的内容
        /// </summary>
        /// <param name="paragraph"></param>
        /// <returns></returns>
        private static List<RstLine> ParseTitle (this Paragraph paragraph)
        {
            List<RstLine> lines = new List<RstLine>();

            int count = paragraph.Lines.Count;
            string content = paragraph.Lines[count - 2];
            string underline = paragraph.Lines.LastItem();

            lines.Add(RstLineFactory.Creat(0, string.Empty, content));
            lines.Add(RstLineFactory.Creat(0, underline + underline + underline + underline, string.Empty));

            return lines;
        }

        #endregion 具体每个类型的解析方法

        #region 废弃的方法

        private static void ParseOrderList_3 (this Paragraph paragraph)
        {
            List<RstLine> rstLines = new List<RstLine>();
            List<(int, int)> ps = new List<(int, int)>();
            foreach (var line in paragraph.Lines)
            {
                var i1 = line.SearchOrderListIndex(RstRegex.OrderList);
                var i2 = i1 + 3;
                ps.Add((i1, i2));
            }
            int lineIndex = 0;
            while (lineIndex < paragraph.Lines.Count)
            {
                if (ps[lineIndex].Item1 != -1)              // 这一行是以列表开头
                {
                }
            }
        }

        private static List<RstLine> ParseOrderList_2 (this Paragraph paragraph)
        {
            List<RstLine> rstLines = new List<RstLine>();

            return null;
        }

        private static List<RstLine> ParserOrderList_1 (this Paragraph paragraph)
        {
            List<RstLine> rstLines = new List<RstLine>();
            int firstIndex = paragraph.FirstLineRegexMatchIndex(RstRegex.OrderList);

            ListLoop(paragraph);

            return rstLines;

            void ListLoop (Paragraph parent)
            {
                var splitedParagraph = parent.SplitOrderListLine();
                foreach (var child in splitedParagraph)
                {
                    child.Lines.ShowList();
                    ListLoop(child);
                }
            }
        }

        #endregion 废弃的方法
    }
}