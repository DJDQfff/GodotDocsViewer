using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace RstFileParser
{
    public class RstRegex
    {
        /// <summary>
        /// 以.. :开头，不管，后面还有没有东西，用于笼统判断
        /// </summary>
        public const string CommandStart = @"^\.{2}\s.+\:";

        /// <summary>
        /// 只有一行 ..  : 格式
        /// </summary>
        public const string CommandOnly = @"^\.{2}\s.+\:{1,2}$";

        /// <summary>
        /// 匹配一行 .. _doc_complying_with_licenses 后面还有内容的情况
        /// </summary>
        public const string CommandWithContent = @"\.{2}\s.+\:{1,2}\s+.+";

        /// <summary>
        /// 有序列表
        /// 如：
        /// 1.
        /// 2.
        ///     1.
        ///     2.
        /// *
        /// +
        /// 等
        /// </summary>
        public const string OrderList = @"(^\s*\d+\.\s+)|(^\s*[*+-]\s+)";

        /// <summary>
        /// 内联项，如粗体、黑体、斜体
        /// 这个一般不用管
        /// </summary>
        public const string InLineWord = @"[*']{1,2}.+[*']{1,2}|[`]{2}.+[`]{2}";

        /// <summary>
        /// 如：************
        /// 标题栏底线
        /// 标题栏
        /// </summary>
        public const string TitleUnderline = @"^\++$|^\-+$|^\=+$|^\*+$|^\^+$";

        /// <summary>
        /// 引用
        /// </summary>
        public const string Ref = @"^:.+:";

        /// <summary>
        /// 列表分行
        /// </summary>
        public const string TableLine = @"\+[+=-]+\+";

        /// <summary>
        /// 注释行
        /// 不用管，他只会在代码段内存在
        /// </summary>
        public const string ZhuShi = @"^\s*#\s";
    }
}