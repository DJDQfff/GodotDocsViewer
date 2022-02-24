using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using MyStandard20Library;

using RstFileParser;

namespace RstFileContentChange
{
    internal class Program
    {
        private const string Target_PoPath = @"D:\桌面\新建文件夹 (2)\locale\zh_CN\LC_MESSAGES\zh_CN.po";
        private const string Target_FolderPath = @"D:\桌面\新建文件夹 (2)\godot-docs-master";

        private static Dictionary<string, string> PoPairs;
        private static List<string> NotInclude = new List<string>();
        private const string Regex1 = @"\.\..+\:";

        private static async Task ParagraphOperation (string path)
        {
            var lines = File.ReadAllLines(path);
            var pragraphes = lines.SplitParagraphByEmptyLines(true);

            List<List<string>> content = new List<List<string>>();
            List<Task> tasks = new List<Task>();
            foreach (var rstpragraph in pragraphes)
            {
                Task task = Task.Run(() =>
                {
                    List<string> vs = new List<string>();
                    string check = rstpragraph.Lines.RstFileSpecialConnectString();
                    if (PoPairs.ContainsKey(check))
                    {
#if DEBUG
                        //Console.WriteLine($"存在:{check}");
                        //Console.ReadKey();
#endif
                        vs.AddRange(rstpragraph.Lines);
                    }
                    else
                    {
                        NotInclude.Add(check);
                        //Console.WriteLine($"不存在:{check}");
                    }
                });
                tasks.Add(task);
            }
            await Task.WhenAll(tasks);
            Console.WriteLine($"{path}已遍历完");
            Console.ReadKey();
            NotInclude.ShowList();
        }

        internal static async Task Loop (string currentfolder)
        {
            string[] subfolders = Directory.GetDirectories(currentfolder);
            foreach (string folderpath in subfolders)
            {
                await Loop(folderpath);
            }

            string[] filepath = Directory.GetFiles(currentfolder);
            foreach (string path in filepath)
            {
                if (path.IsRstFile())
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
            Console.WriteLine($"文件夹{currentfolder}已遍历完毕");
            Console.ReadKey();
#endif
        }

        private static async Task Main (params string[] args)
        {
            PoPairs = PoFileParser.Core.GetDictionary(Target_PoPath);

            Console.WriteLine("\r转换程序开始执行,输入任意键以继续");

            Console.ReadKey();

            await Loop(Target_FolderPath);
        }
    }
}