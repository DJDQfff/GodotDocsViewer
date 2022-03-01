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
        private const string PoFilePath = @"D:\桌面\新建文件夹 (2)\locale\zh_CN\LC_MESSAGES\zh_CN.po";
        private const string FolderPath = @"D:\桌面\新建文件夹 (2)\godot-docs-master";

        private static Dictionary<string, string> PoPairs;
        private static List<string> NotInclude = new List<string>();
        private const string Regex1 = @"\.\..+\:";

        private static void Main (params string[] args)
        {
            PoPairs = PoFileParser.Core.GetDictionary(PoFilePath);

            Console.WriteLine("转换程序开始执行,输入任意键以继续：... ...");

            Console.ReadLine();

            MainLoop(FolderPath);
        }

        /// <summary>
        /// 递归对每个文件进行操作
        /// </summary>
        /// <param name="currentfolder"></param>
        internal static void MainLoop (string currentfolder)
        {
            string[] subfolders = Directory.GetDirectories(currentfolder);
            foreach (string folderpath in subfolders)
            {
                MainLoop(folderpath);
            }

            string[] filepath = Directory.GetFiles(currentfolder);
            foreach (string path in filepath)
            {
                if (path.IsRstFile())
                {
                    Console.WriteLine($"\t当前文件：{path}\r");
                    var rstlist = RstFileOperation(path);
                    var list = rstlist.ConvertToAllLines();
                    //File.WriteAllLines(path, list);             // 覆盖文件操作
                    Console.WriteLine();
                    Console.ReadKey();
                }
            }
            Console.WriteLine($"文件夹{currentfolder}已遍历完毕");
            Console.ReadKey();
        }

        private static List<RstLine> RstFileOperation (string path)
        {
            List<RstLine> rstLines = new List<RstLine>();
            var lines = File.ReadAllLines(path);
            var pragraphes = lines.SplitParagraphByEmptyLines();

            foreach (var rstpragraph in pragraphes)
            {
                rstLines.AddRange(ParagraphParser.ParseParagraph(rstpragraph));
                rstLines.Add(RstLineFactory.CreatNewLine());
            }
            Console.WriteLine($"{path}已遍历完");
            Console.ReadKey();
            NotInclude.ShowList();

            return rstLines;
        }
    }
}