using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Console;
using MyStandard20Library;

using RstFileParser;

namespace RstFileContentChange
{
    public class Program
    {
        public const string PoFilePath = @"D:\桌面\新建文件夹 (2)\godot-docs-master\zh_CN.po";
        public const string TargetFolder = @"D:\桌面\新建文件夹 (2)\godot-docs-master";
        public const string FilterFolder = @"D:\桌面\新建文件夹 (2)\godot-docs-master\classes";

        public const string TestFolder = @"D:\桌面\测试";
        public const string TestPoFile = @"D:\桌面\新建文件夹 (2)\zh_CN.po";

        public static PoFileParser.PoDictionary PoDictionary = PoFileParser.Factory.Creat(PoFilePath);

        public static List<string> strs = new List<string>();

        private static void Main (params string[] args)
        {
            Console.WriteLine("转换程序开始执行,输入任意键以继续：... ...");

            ////Console.ReadLine();

            MainLoop(TargetFolder);
            WriteLine("结束");
            ReadLine();
        }

        /// <summary> 递归所有文件夹和rst文件 </summary>
        /// <param name="currentfolder"> </param>
        internal static void MainLoop (string currentfolder)
        {
            string[] subfolders = Directory.GetDirectories(currentfolder);

            foreach (string folderpath in subfolders)
            {
                if (folderpath is FilterFolder)
                    continue;

                MainLoop(folderpath);
            }

            string[] filepath = Directory.GetFiles(currentfolder);
            foreach (string path in filepath)
            {
                if (path.IsRstFile())
                {
                    Console.WriteLine($"当前文件：{path}");
                    var lines = RstFileOperation(path);

                    //lines.ShowList();
                    //File.WriteAllLines(path, list);             // 覆盖文件操作
                    Console.WriteLine($"已执行完{path}");
                    Console.WriteLine();
                    //Console.ReadLine();
                }
            }
            Console.WriteLine($"文件夹{currentfolder}已遍历完毕");
            //Console.ReadLine();
        }

        /// <summary> 对每一个rst文件进行解析 </summary>
        /// <param name="path"> </param>
        /// <returns> </returns>
        private static List<string> RstFileOperation (string path)
        {
            var lines = File.ReadAllLines(path);

            var pragraphes = lines.SplitParagraphByEmptyLines();

            RstManager rstManager = new RstManager(pragraphes);
            var rstLines = rstManager.Get(PoDictionary);

            Console.WriteLine($"{path}已遍历完");
            //Console.ReadLine();

            return rstLines;
        }
    }
}