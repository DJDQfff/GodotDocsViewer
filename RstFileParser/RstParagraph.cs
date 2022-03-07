using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

using MyStandard20Library;

namespace RstFileParser
{
    public class RstParagraph : IParagraph
    {
        /// <summary> 段落类型，不同类型有不同操作 </summary>
        public RstParagraphType rstParagraphType { set; get; }

        /// <summary> 对于段落内，相同缩进视为一句，不同缩进视为另一句 </summary>
        public List<RstLine> rstLines { set; get; }

        public IList<string> Lines { get; }

        /// <summary>
        /// 以段落第一行的缩进作为段落整体的缩进（首个字符出现的位置索引）
        /// </summary>
        public int Indent { get; }

        public RstParagraph (IParagraph paragraph)
        {
            Lines = paragraph.Lines;

            Indent = Lines[0].FirstNotWhiteSpaceCharacterIndex();
        }
    }
}