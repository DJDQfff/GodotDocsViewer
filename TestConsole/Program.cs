using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using MyStandard20Library;

using PoFileParser;

using RstFileParser;

namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main (string[] args)
        {
            string Target_PoPath = @"D:\桌面\新建文件夹 (2)\locale\zh_CN\LC_MESSAGES\zh_CN.po";

            Dictionary<string, string> PoPairs = PoFileParser.Core.GetDictionary(Target_PoPath);

            string Path = @"D:\桌面\新建文本文档.txt";
            var lines = File.ReadAllLines(Path);
            List<string> vs = new List<string>(lines);
            var paragraph = vs.SplitParagraphByEmptyLines();

            foreach (var para in paragraph)
            {
                //para.Lines.ShowList();
                var rstLines = para.ParseOrderList();
                foreach (var r in rstLines)
                {
                    Console.WriteLine(r.Content);
                    Console.WriteLine(PoPairs.ContainsKey(r.Content));
                }
                Console.WriteLine();
                //para.Abtract.ShowList();
                //if (para.IsOrderList())
                //{
                //    Console.WriteLine("这是一段列表");
                //}

                //Console.ReadLine();
            }
            Console.ReadLine();
        }

        #region 方法

        private static void TestPo ()
        {
            string poFilePath = @"D:\桌面\新建文件夹 (2)\godot-docs-master\godot-engine-godot-docs-zh_Hans.po";

            Stream stream = new FileStream(poFilePath, FileMode.Open);
            Core.Main(stream);
        }

        private static void NewMethod ()
        {
            string rstFilePath = @"D:\桌面\新建文件夹 (2)\godot-docs-master\getting_started\step_by_step\instancing.rst";
            Stream stream = new FileStream(rstFilePath, FileMode.Open);

            Core.Main(stream);
        }

        #endregion 方法
    }
}