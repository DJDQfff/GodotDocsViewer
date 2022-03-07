using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace RstFileParser
{
    /// <summary>
    /// 段落类型分类。 列表、表格、标题、文字等段落自身内容是不会影响其他段落内容的；
    /// Comand、LiteralBlock段落内容会影响后续段落内容。
    /// 其中，Command指令段落又分为好几种类型（此枚举不作区分）
    /// </summary>
    public enum RstParagraphType
    {
        /// <summary>
        /// 以 .. 咋段落开头，先确定哪些是指令，以及需要特殊处理的段落
        /// </summary>
        Command,

        /// <summary> 列表 </summary>
        OrderList,

        /// <summary> 表格 </summary>
        Table,

        /// <summary> 章节标题 </summary>
        ChapterTitle,

        /// <summary>
        /// 以 :: 为一段落 字面内容，其内容以缩进划分，且原样输出
        /// </summary>
        LiteralBlock,

        /// <summary> 文字段落，其他都不是，则按文字段落处理 </summary>
        TextBody
    }
}