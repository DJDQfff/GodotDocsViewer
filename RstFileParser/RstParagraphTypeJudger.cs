using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

using MyStandard20Library;

namespace RstFileParser
{
    public static class RstParagraphTypeJudger
    {

        #region 具体方法细节

        /// <summary>
        /// 判断一个段落是不是排序列表
        /// </summary>
        /// <param name="paragraph">段落</param>
        /// <returns></returns>
        public static bool IsOrderList (this IParagraph paragraph)
        {
            return paragraph.FirstLineRegexMatchIndex(RstRegex.OrderList) != -1;
        }

        /// <summary>
        /// 判断一个段落是不是标题段落
        /// </summary>
        /// <param name="paragraph"></param>
        /// <returns></returns>
        public static bool IsTitle (this IParagraph paragraph)
        {
            // 要修改，标题的话还有一个限制，起码两行 TODO
            return paragraph.LastLineRegexMatchIndex(RstRegex.TitleUnderline) != -1;
        }

        /// <summary>
        /// 判断一个段落是否是列表
        ///
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
        /// 一般有3中情况下会使用引用
        /// 1：引用作为列表出现，这种不用管，识别为列表就行
        /// 2：在中文内出现，这用也不用管，翻译会自带。因为引用往往很长，引用一般会放到一行开始
        /// 3：单独出现，主要针对这种
        /// </summary>
        /// <param name="paragraph"></param>
        /// <returns></returns>
        public static bool IsRef (this IParagraph paragraph)
        {
            //
            return paragraph.IsAllLinesMatchRegex(RstRegex.Ref);
        }

        public static bool IsCommand (this IParagraph paragraph)
        {
            // TODO 这段是Command
            return true;
        }

        /// <summary>
        /// 该段落为指令，且他的写一个段落需要特殊处理，不能当做文字段落，如 toctree
        /// </summary>
        /// <param name="paragraph"></param>
        /// <returns></returns>
        public static bool IsCommandLineWithNext (this IParagraph paragraph)
        {
            // TODO
            return paragraph.FirstLineRegexMatchIndex(RstRegex.CommandWithContent) != -1;
        }

        public static bool IsLiteralBlock (this IParagraph paragraph)
        {
            // TODO 适配以 :: 开头的段落
            // 他的下一段落是他的内容
            return true;
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