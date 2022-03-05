using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace RstFileParser
{
    public enum RstParagraphType
    {
        OrderList,
        Table,
        Command,
        CommandNext,
        TextBody,
        ChapterTitle
    }
}