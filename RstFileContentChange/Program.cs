using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MyStandard20Library;
using System.Threading.Tasks;

namespace RstFileContentChange
{
    internal class Program
    {
        private const string pofilepath = @"D:\桌面\新建文件夹 (2)\locale\zh_CN\LC_MESSAGES\zh_CN.po";

        private static Dictionary<string, string> PoPairs;

        private const string Regex1 = @"\.\..+\:";

        private static async Task Main (params string[] args)
        {
            string currentfolderpath = args[0];
            string[] folderpath = Directory.GetDirectories(currentfolderpath);
            foreach (string path in folderpath)
            {
                await Main(path);
            }

            string[] filepath = Directory.GetFiles(currentfolderpath);
            foreach (string path in filepath)
            {
                if (IsRstFile(path))
                {
                    await ParagraphOperation(path);
#if DEBUG
                    Console.WriteLine($"\t当前文件：{path}\r");
                    Console.WriteLine();
                    Console.ReadKey();
#endif
                }
            }
#if DEBUG
            Console.WriteLine($"文件夹{currentfolderpath}已遍历完毕");
            Console.ReadKey();
#endif
        }

        private static async Task ParagraphOperation (string path)
        {
            var lines = File.ReadAllLines(path);
            var pragraphes = lines.SplitParagraphByEmptyLines(true);

            List<List<string>> content = new List<List<string>>();

            foreach (var rstpragraph in pragraphes)
            {
                bool tag;
                Task task = Task.Run(() =>
                 {
                     List<string> vs = new List<string>();
                     string check = rstpragraph.Content.RstFileSpecialConnectString();
                     if ((tag = PoPairs.ContainsKey(check)))
                     {
#if DEBUG
                         Console.WriteLine($"存在:{check}");
                         Console.ReadKey();
#endif
                         tag = true;
                         vs.AddRange(rstpragraph.Content);
                     }
                     else

                         Console.WriteLine($"不存在:{check}");
                 });

                await task;
                Console.ReadKey();
            }
        }

        public static bool IsRstFile (string path)
        {
            var extension = Path.GetExtension(path);
            if ((extension is ".rst") || (extension is ".RST"))
                return true;
            else
                return false;
        }

        static Program ()
        {
            PoPairs = PoFileParser.Core.GetDictionary(pofilepath);

            Console.WriteLine("\r转换程序开始执行,输入任意键以继续");

            Console.ReadKey();
        }
    }
}