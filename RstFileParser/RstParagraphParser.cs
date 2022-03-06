using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

using MyStandard20Library;

namespace RstFileParser
{
    public static class RstParagraphParser
    {
        public static List<RstLine> Parse (this RstParagraph rstParagraph)
        {
            switch (rstParagraph.rstParagraphType)
            {
                case RstParagraphType.Command:
                    return rstParagraph.ParseCommand();

                case RstParagraphType.OrderList:
                    return rstParagraph.ParseOrderList();

                case RstParagraphType.Table:
                    return rstParagraph.ParseTable();

                case RstParagraphType.ChapterTitle:
                    return rstParagraph.ParseTitle();

                case RstParagraphType.TextBody:
                    return rstParagraph.ParseBody();

                default:
                    return null;
            }
        }

        #region 具体到每个类型的解析方法

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="paragraph"></param>
        /// <returns></returns>
        private static List<RstLine> ParseOrderList (this IParagraph paragraph)
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
                    RstLine rstLine = RstLineFactory.Creat(match.Index, match.Value, content, true);
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
                        RstLine rstLine = RstLineFactory.Creat(match.Index, match.Value, strin, true);
                        rstLines.Add(rstLine);
                    }
                    else   // 多行情况下，可能存在不同缩进，分成了不同句，下面的写法只能适用于只用一次不同缩进的情况

                    {
                        string source = currentItem.Lines[0];
                        string str = source.Substring(firstlinecontentstart);
                        RstLine rstLine = RstLineFactory.Creat(match.Index, match.Value, str, true);
                        rstLines.Add(rstLine);

                        List<string> vs = currentItem.Lines.SubList(1, currentItem.Lines.Count - 1);
                        string aaa = vs.RstFileSpecialConnectString();
                        RstLine rstLine1 = RstLineFactory.Creat(secondlinestart, string.Empty, aaa, true);
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
        private static List<RstLine> ParseCommand (this IParagraph paragraph)
        {
            return RstLineFactory.Origin(paragraph);
        }

        private static List<RstLine> ParseCommandNext (this IParagraph rstParagraph)
        {
            // TODO
            return null;
        }

        /// <summary>
        /// 解析表格
        /// </summary>
        /// <param name="paragraph"></param>
        /// <returns></returns>
        private static List<RstLine> ParseTable (this IParagraph paragraph)
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
        private static List<RstLine> ParseRef (this IParagraph paragraph)
        {
            return RstLineFactory.Origin(paragraph);
        }

        /// <summary>
        /// 解析正文
        /// </summary>
        /// <param name="paragraph"></param>
        /// <returns></returns>
        private static List<RstLine> ParseBody (this IParagraph paragraph)
        {
            List<RstLine> lines = new List<RstLine>();
            int index = paragraph.Lines[0].FirstNotWhiteSpaceCharacterIndex();
            string str = paragraph.Lines.RstFileSpecialConnectString();

            lines.Add(RstLineFactory.Creat(index, string.Empty, str, true));
            return lines;
        }

        /// <summary>
        /// 返回Title的内容
        /// </summary>
        /// <param name="paragraph"></param>
        /// <returns></returns>
        private static List<RstLine> ParseTitle (this IParagraph paragraph)
        {
            List<RstLine> lines = new List<RstLine>();

            int count = paragraph.Lines.Count;
            string content = paragraph.Lines[count - 2];
            string underline = paragraph.Lines.LastItem();

            lines.Add(RstLineFactory.Creat(0, string.Empty, content, true));
            lines.Add(RstLineFactory.Creat(0, underline + underline + underline + underline, string.Empty, false));

            return lines;
        }

        private static List<RstLine> ParseUnknown (this IParagraph rstParagraph)
        {
            //TODO
            return null;
        }

        #endregion 具体到每个类型的解析方法
    }
}