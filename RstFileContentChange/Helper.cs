using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RstFileContentChange
{
    public static class Helper
    {
        /// <summary>
        /// 拼接字符串，返回一个新字符串
        /// 这是对rst文件特别改的。
        /// 1.多行之间用一个空格拼接
        /// 2.单行则不用后面补空格
        /// 3.Sphinx生成po文件时，会对msgrd内含冒号的进行转义，即前面补\，这里要记得反回去
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
                vs1.Add(ns.Trim());
            }
            if (vs.Count() > 1)
            {
                string str = string.Join(' ', vs1);
                return str;
            }
            else
            {
                return vs1[0];
            }
        }
    }
}