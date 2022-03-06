using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

using MyStandard20Library;

namespace RstFileParser
{
    public static class RstParagraphTypeJudger
    {
        #region 具体方法细节

        public static bool IsCommand (this IParagraph paragraph)
        {
            return paragraph.FirstLineRegexMatchIndex(RstRegex.Command) != -1;
        }

        /// <summary>
        /// 判断一个段落是不是排序列表
        /// 要求：以* - + 数字 等开头，后接空格
        ///      且必须在段落头
        /// </summary>
        /// <param name="paragraph">段落</param>
        /// <returns></returns>
        public static bool IsOrderList (this IParagraph paragraph)
        {
            return paragraph.FirstLineRegexMatchIndex(RstRegex.OrderList) != -1;
        }

        /// <summary>
        /// 判断一个段落是不是标题段落
        /// 要求：标题根下划线，上划线可选，起码两行，下划线比标题长
        /// </summary>
        /// <param name="paragraph"></param>
        /// <returns></returns>
        public static bool IsTitle (this IParagraph paragraph)
        {
            if (paragraph.Lines.Count >= 2)
            {
                return paragraph.LastLineRegexMatchIndex(RstRegex.TitleUnderline) != -1;
            }
            else
                return false;
        }

        /// <summary>
        /// 判断一个段落是否是列表
        /// TODO 有好几种列表，这里只弄了一种，存在隐患
        /// </summary>
        /// <param name="paragraph"></param>
        /// <returns></returns>
        public static bool IsTable (this IParagraph paragraph)
        {
            var bool1 = paragraph.FirstLineRegexMatchIndex(RstRegex.TableLine);
            var bool2 = paragraph.LastLineRegexMatchIndex(RstRegex.TableLine);

            if ((bool1 + bool2) >= 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 他的后面几段都是他的有效范围（取决于后续段落的缩进）
        /// 判断：他有3中使用情况，但这里只判断一种（即单独作为段落使用）
        /// </summary>
        /// <param name="paragraph"></param>
        /// <returns></returns>
        public static bool IsLiteralBlock (this IParagraph paragraph)
        {
            if (paragraph.Lines.Count == 1)
            {
                var match = Regex.Match(paragraph.Lines[0], RstRegex.LiteralBlock);
                return match.Success;
            }
            else
                return false;
        }

        /// <summary>
        /// 一般有3中情况下会使用引用
        /// 1：引用作为列表出现，这种不用管，识别为列表就行
        /// 2：在中文内出现，这用也不用管，翻译会自带。因为引用往往很长，引用一般会放到一行开始
        /// 3：单独出现，主要针对这种
        /// </summary>
        /// <param name="paragraph"></param>
        /// <returns></returns>
        public static bool IsRef (this IParagraph paragraph)
        {
            // TODO Ref还没仔细看语法，暂时不管
            return paragraph.IsAllLinesMatchRegex(RstRegex.Ref);
        }

        #endregion 具体方法细节

        public static RstParagraph JudgeType (Paragraph _paragraph)
        {
            RstParagraph rstParagraph = new RstParagraph(_paragraph);

            if (_paragraph.IsCommand())
            {
                rstParagraph.rstParagraphType = RstParagraphType.Command;
                return rstParagraph;
            }

            if (_paragraph.IsOrderList())
            {
                rstParagraph.rstParagraphType = RstParagraphType.OrderList;
                return rstParagraph;
            }

            if (_paragraph.IsTitle())
            {
                rstParagraph.rstParagraphType = RstParagraphType.ChapterTitle;
                return rstParagraph;
            }

            if (_paragraph.IsTable())
            {
                rstParagraph.rstParagraphType = RstParagraphType.Table;
                return rstParagraph;
            }
            // 都不是的情况下，默认为TextBody
            rstParagraph.rstParagraphType = RstParagraphType.TextBody;
            return rstParagraph;

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

            // 还有一种，是跟在Command后面的段落，不在这里判断
        }
    }
}