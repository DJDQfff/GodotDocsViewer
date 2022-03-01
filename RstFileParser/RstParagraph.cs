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
    /// 判断标准：存在大量行只用符号，而没有单词
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

    /// <summary>
    /// 标题
    /// 判断标准：
    /// </summary>
    public class RstTitle : Paragraph
    {
        public int Retract { set; get; }

        public RstTitle (IList<string> lines) : base(lines)
        {
        }
    }

    public class RstSinglePotPot : Paragraph
    {
        public int Retract { set; get; }

        public RstSinglePotPot (IList<string> lines) : base(lines)
        {
        }
    }

    public class RstPotPotWithContent : Paragraph
    {
        public int Retract { set; get; }

        public RstPotPotWithContent (IList<string> lines) : base(lines)
        {
        }
    }

    public class OrderList : Paragraph
    {
        public int Retract { set; get; }

        public OrderList (IList<string> lines) : base(lines)
        {
        }
    }

    /// <summary>
    /// 像这种：- :ref:`doc_gdscript_documentation_comments`
    /// 应该是指向文本自身页面，不用翻译
    /// </summary>
    public class RstSelfRef : Paragraph
    {
        public int Retract { set; get; }

        public RstSelfRef (IList<string> lines) : base(lines)
        {
        }
    }
}