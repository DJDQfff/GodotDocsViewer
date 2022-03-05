using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace RstFileParser
{
    public class RstParagraph
    {
        /// <summary>
        /// 段落类型，不同类型有不同操作
        /// </summary>
        public RstParagraphType rstParagraphType { set; get; }

        /// <summary>
        /// 对于段落内，相同缩进视为一句，不同缩进视为另一句
        /// </summary>
        public List<RstLine> rstLines { set; get; }
    }
}