using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

using MyStandard20Library;

namespace RstFileParser
{
    /// <summary>
    /// 表格
    /// </summary>
    public class RstTable : Paragraph
    {
        /// <summary>
        /// 缩进 空格
        /// </summary>
        public int Retract { set; get; }

        /// <summary>
        /// 要翻译的有效内容
        /// </summary>
        public string Content { set; get; }

        public RstTable (IList<string> lines) : base(lines)
        {
        }
    }

}