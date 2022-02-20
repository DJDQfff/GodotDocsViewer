using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public static class Helper
    {
        /// <summary>
        /// 拼接字符串，返回一个新字符串
        /// 这是对rst文件特别改的，妈的rst文件换行有病。
        /// 如果这行没结束，就会自动把单词后面的空格消掉.
        /// 如果是单行，则不用加空格
        /// </summary>
        /// <param name="vs"></param>
        /// <returns></returns>
        public static string RstFileSpecialConnectString (this IList<string> vs)
        {
            List<string> vs1 = new List<string>();
            foreach (var s in vs)
            {
                vs1.Add(s.Trim());
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
            //StringBuilder stringBuilder = new StringBuilder();
            //foreach (var str in vs)
            //{
            //    stringBuilder.Append(str);

            //    if (vs.Count() > 1)                         // 如果只有一行，则不用加空格
            //    {
            //        if (!str.EndsWith('.'))
            //        {
            //            stringBuilder.Append(' ');
            //        }

            //    }
            //}
            //return stringBuilder.ToString();
        }
    }
}