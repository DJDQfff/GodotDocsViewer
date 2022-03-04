using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using RstFileParser;
using MyStandard20Library;
namespace RstFileContentChange
{
    /// <summary>
    /// 管理每一个段落要进行的操作
    /// </summary>
    public class Manager
    {
        public List<int> CommandList { set; get; } = new List<int>();
        public List<int> BodyList { set; get; } = new List<int>();
        public List<int> OrderList { set; get; } = new List<int>();
        public List<int> ToctreeList { set; get; } = new List<int>();
        public List<int> TitleList { set; get; } = new List<int>();
        public List<int> TableList { set; get; } = new List<int>();
        public List<int> RefList { set; get; } = new List<int>();

        public void StartConvert (List<Paragraph> paragraphs)
        {
            for (int j = 0; j < paragraphs.Count; j++)
            {
            }

        }
    }
}
