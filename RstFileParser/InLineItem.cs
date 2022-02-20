using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace RstFileParser
{
    /// <summary>
    /// 行内标记，如粗体，斜体，黑体等
    /// </summary>
    public class InLineItem
    {
        public int Position { set; get; }
        public ItemType itemType { set; get; }
        public string Word { set; get; }
    }

    public enum ItemType
    {
        itallic,
        bold,
        code
    }
}
