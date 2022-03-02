﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using MyStandard20Library;

using RstFileParser;

namespace RstFileContentChange
{
    public class Program
    {
        public const string PoFilePath = @"D:\桌面\新建文件夹 (2)\godot-docs-master\zh_CN.po";
        public const string FolderPath = @"D:\桌面\新建文件夹 (2)\godot-docs-master";
        public const string FilterFolder = @"D:\桌面\新建文件夹 (2)\godot-docs-master\classes";

        private static Dictionary<string, string> PoPairs;

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
                    var rstlist = RstFileOperation(path);
                    var list = rstlist.ConvertToTranslatedAllLines(PoPairs);
                    //list.ShowList();
                    File.WriteAllLines(path, list);             // 覆盖文件操作
                    Console.WriteLine($"已执行完{path}");
                    Console.WriteLine();
                    //Console.ReadLine();
                }
            }
            Console.WriteLine($"文件夹{currentfolder}已遍历完毕");
            //Console.ReadLine();
        }

        private static List<RstLine> RstFileOperation (string path)
        {
            List<RstLine> rstLines = new List<RstLine>();
            var lines = File.ReadAllLines(path);
            var pragraphes = lines.SplitParagraphByEmptyLines();

            bool flag = true;
            for (int index = 0; index < pragraphes.Count; index++)
            {
                var rstpragraph = pragraphes[index];
                if (flag)
                {
                    rstLines.AddRange(ParagraphParser.Core(rstpragraph));
                }
                else
                {
                    rstLines.AddRange(RstLineFactory.Origin(rstpragraph));
                }

                rstLines.Add(RstLineFactory.CreatNewLine());

                if (rstpragraph.IsCommand())  // TODO 这个判断方法存在隐患
                {
                    flag = false;    // tortree为一个命令，他下面的一段都是给Sphinx用的，不需要翻译，跳过
                }
                else
                    flag = true;
            }
            Console.WriteLine($"{path}已遍历完");
            //Console.ReadLine();

            return rstLines;
        }
    }
}