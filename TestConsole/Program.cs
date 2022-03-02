using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using MyStandard20Library;

using PoFileParser;
using static RstFileContentChange.Program;
using RstFileParser;

namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main (string[] args)
        {
            Dictionary<string, string> PoPairs = PoFileParser.Core.GetDictionary(PoFilePath);

            List<RstLine> rstLines = new List<RstLine>();

            rstLines.Add(RstLineFactory.Creat(0, string.Empty, "dfjas"));
            rstLines.Add(RstLineFactory.Creat(0, string.Empty, "dfjas"));
            rstLines.Add(RstLineFactory.Creat(0, string.Empty, "dfjas"));

            var list = rstLines.ConvertToTranslatedAllLines(PoPairs);
            Console.ReadLine();
        }

        private static void TestCOre ()
        {
            Dictionary<string, string> PoPairs = PoFileParser.Core.GetDictionary(PoFilePath);

            string Path = @"D:\桌面\新建文本文档.txt";
            string test = @"D:\桌面\2.txt";

            var lines = File.ReadAllLines(test);
            List<string> vs = new List<string>(lines);
            var paragraph = vs.SplitParagraphByEmptyLines();

            foreach (var para in paragraph)
            {
                //para.Lines.ShowList();
                var rstLines = ParagraphParser.Core(para);
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