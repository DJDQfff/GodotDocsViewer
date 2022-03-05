using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyStandard20Library;
using System.Text.RegularExpressions;

namespace RstFileParser
{
    /// <summary>
    /// 对段落类型进行判断
    /// </summary>
    public static class ParagraphJudger
    {
        /// <summary>
        /// 判断一个段落是不是排序列表
        /// </summary>
        /// <param name="paragraph">段落</param>
        /// <returns></returns>
        public static bool IsOrderList (this Paragraph paragraph)
        {
            return paragraph.FirstLineRegexMatchIndex(RstRegex.OrderList) != -1;
        }

        /// <summary>
        /// 判断一个段落是不是标题段落
        /// </summary>
        /// <param name="paragraph"></param>
        /// <returns></returns>
        public static bool IsTitle (this Paragraph paragraph)
        {
            return paragraph.LastLineRegexMatchIndex(RstRegex.TitleUnderline) != -1;
        }

        /// <summary>
        /// 判断一个段落是否是列表
        ///
        /// </summary>
        /// <param name="paragraph"></param>
        /// <returns></returns>
        public static bool IsTable (this Paragraph paragraph)
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
        public static bool IsRef (this Paragraph paragraph)
        {
            return paragraph.IsAllLinesMatchRegex(RstRegex.Ref);
        }

        #region 对Sphinx指令的精确分类

        /// <summary>
        /// 是否是指令段落
        /// </summary>
        /// <param name="paragraph"></param>
        /// <returns></returns>
        public static bool IsCommand (this Paragraph paragraph)
        {
            return paragraph.AnyLineMatchRegex(RstRegex.CommandStart);
            ////return paragraph.FirstLineRegexMatchIndex(RstRegex.CommandStart) != -1;
        }

        /// <summary>
        /// 判断段落是否是一个指令后跟着内容（要翻译）
        /// </summary>
        /// <param name="paragraph"></param>
        /// <returns></returns>
        public static bool IsCommandLineWithContent (this Paragraph paragraph)
        {
            return paragraph.FirstLineRegexMatchIndex(RstRegex.CommandWithContent) != -1;
        }

        /// <summary>
        /// 判断段落是否是单行指令
        /// </summary>
        /// <param name="paragraph"></param>
        /// <returns></returns>
        public static bool IsCommandSingleLine (this Paragraph paragraph)
        {
            return paragraph.LastLineRegexMatchIndex(RstRegex.CommandOnly) != -1;
        }

        #endregion 对Sphinx指令的精确分类
    }
}