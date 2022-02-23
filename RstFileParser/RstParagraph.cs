using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

using MyStandard20Library;

namespace RstFileParser
{
    public static class Factory_RstParagraph
    {
        public void
    }

    public class RstParagraph : Paragraph
    {
        public RstParagraph Paarent { set; get; }
        public RstParagraph Child { set; get; }

        public RstParagraph (Paragraph paragraph) : base(paragraph.Content)
        {
        }
    }
}