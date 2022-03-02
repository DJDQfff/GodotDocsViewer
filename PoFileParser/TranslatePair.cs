using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

using MyStandard20Library;

namespace PoFileParser
{
    /// <summary>
    /// 翻译语句对
    /// </summary>
    public class TranslatePair : Paragraph
    {
        private IList<string> content;

        /// <summary>
        /// 引用路径
        /// </summary>
        public string[] UsedPosition { set; get; }

        /// <summary>
        /// 目标文本
        /// </summary>
        public string Msgid { set; get; }

        /// <summary>
        /// 翻译文本
        /// </summary>
        public string Msgstr { set; get; }

        public TranslatePair (Paragraph paragraph) : base(paragraph.Lines)
        {
            MemberParser(this.Lines);
        }

        private void MemberParser (IList<string> lines)
        {
            string uses = string.Empty;

            int msgidstart = 0;
            int msgstrstart = 0;

            List<string> uselist = new List<string>();
            List<string> msgidlist = new List<string>();
            List<string> msgstrlist = new List<string>();

            foreach (var line in lines)
            {
                if (line.StartsWith("#"))
                {
                    uselist.Add(line);
                }
                if (line.StartsWith("msgid"))
                {
                    msgidstart = lines.IndexOf(line);
                }
                if (line.StartsWith("msgstr"))
                {
                    msgstrstart = lines.IndexOf(line);
                }
            }

            for (int index = msgidstart; index < msgstrstart; index++)
            {
                msgidlist.Add(lines[index]);
            }

            for (int index = msgstrstart; index < lines.Count; index++)
            {
                msgstrlist.Add(lines[index]);
            }

            foreach (var line in msgidlist)
            {
                string temp = line;
                if (line.StartsWith("msgid"))
                {
                    temp = temp.Remove(0, 5);
                }
                string temp2 = temp.Trim();
                int length = temp2.Length;
                string temp3 = temp2.Substring(1, length - 2);
                Msgid += temp3;
            }

            foreach (var line in msgstrlist)
            {
                string temp = line;

                if (line.StartsWith("msgstr"))
                {
                    temp = temp.Remove(0, 6);
                }
                string temp2 = temp.Trim();
                int length = temp2.Length;
                string temp3 = temp2.Substring(1, length - 2);
                Msgstr += temp3;
            }
        }
    }
}