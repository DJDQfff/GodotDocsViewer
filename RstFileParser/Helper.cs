using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using MyStandard20Library;

namespace RstFileParser
{
    public static class Helper
    {
        public static List<Paragraph> SplitOrderListLine (this Paragraph parent, params int[] ints)
        {
            List<int> Indexes = new List<int>(ints);
            Indexes.Add(parent.Lines.Count);

            List<Paragraph> paragraphs = new List<Paragraph>();

            for (int index = 0; index < Indexes.Count - 1; index++)
            {
                var vs = parent.Lines.SubList(Indexes[index], Indexes[index + 1] - 1);

                vs.RemoveEmptyLine();

                Paragraph paragraph = new Paragraph(vs);

                paragraphs.Add(paragraph);
            }

            return paragraphs;
        }

        public static bool IsRstFile (this string path)
        {
            var extension = Path.GetExtension(path);
            if ((extension is ".rst") || (extension is ".RST"))
                return true;
            else
                return false;
        }

        /// <summary>
        /// 拼接字符串，返回一个新字符串
        /// 这是对rst文件特别改的。
        /// 1.多行之间用一个空格拼接
        /// 2.单行则不用后面补空格
        /// 3.最后一行不用补空格
        /// 4.Sphinx从rst文件导出po文件时，会对"号进行转义
        /// </summary>
        /// <param name="vs"></param>
        /// <returns></returns>
        public static string RstFileSpecialConnectString (this IList<string> vs)
        {
            List<string> vs1 = new List<string>();
            foreach (var s in vs)
            {
                string ns = s;
                ns = s.Replace("\"", "\\\"");
                //ns = s.Replace("\\\\", "\\");
                vs1.Add(ns.Trim());// 对部分行前面有缩进的移除，只保留纯文本
            }
            if (vs.Count() > 1)
            {
                string str = string.Join(" ", vs1);
                return str;
            }
            else
            {
                return vs1[0];
            }
        }
    }
}