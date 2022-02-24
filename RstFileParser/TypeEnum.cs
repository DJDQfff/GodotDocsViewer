using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace RstFileParser
{
    public enum TypeEnum
    {
        undefined,
        title,
        singlelineofcommand,
        commandwithcontent,
        order123list,
        contentonly
    }

    public class RegexPatternForRst
    {
        /// <summary>
        /// 匹配单行的 .. _doc_complying_with_licenses:
        /// </summary>
        public const string str1 = @"^\.{2}\s.+\:{1,2}$";

        /// <summary>
        /// 匹配一行 .. _doc_complying_with_licenses 后面还有内容的情况
        /// </summary>
        public const string str2 = @"\.{2}\s.+\:{1,2}\s+.+";

        /// <summary>
        /// 有序列表
        /// 如：
        /// 1.
        /// 2.
        ///     1.
        ///     2.
        /// </summary>
        public const string str3 = @"^\s*\d+\.\s+.+";

        /// <summary>
        /// 无序列表
        /// 如：
        /// *
        /// -
        /// +
        /// </summary>
        public const string str4 = @"^\s*[*+-]\s+.+";

        /// <summary>
        /// 内联项，如粗体、黑体、斜体
        /// </summary>
        public const string str5 = @"[*']{1,2}.+[*']{1,2}|[`]{2}.+[`]{2}";
    }
}