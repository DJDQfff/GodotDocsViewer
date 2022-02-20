using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace RstFileParser
{
    /// <summary>
    /// 所有段落必须实现该接口
    /// </summary>
    public interface IParagraph
    {
        /// <summary>
        /// 使用标准语法还是扩展语法
        /// </summary>
        SyntaxEnum Syntax { set; get; }

    }
}