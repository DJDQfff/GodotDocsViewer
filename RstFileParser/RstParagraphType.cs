using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace RstFileParser
{
    public enum RstParagraphType
    {
        Command,                        // 以 .. 咋段落开头，先确定哪些是指令，以及需要特殊处理的段落
        OrderList,                      // 列表
        Table,                          // 表格
        ChapterTitle,                   // 章节标题
        LiteralBlock,                   // 以 :: 为一段落 字面内容，其内容以缩进划分，且原样输出
        TextBody                        // 文字段落，以上都不是，则按文字段落处理
    }
}