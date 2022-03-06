using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using MyStandard20Library;

using PoFileParser;
using static RstFileContentChange.Program;
using RstFileParser;
using static System.Console;

namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main (string[] args)
        {
            var PoDic = PoFileParser.Factory.Creat(PoFilePath);

            string path = @"D:\桌面\新建文本文档.txt";
            var lines = File.ReadAllLines(path);
            List<string> vs = new List<string>(lines);
            var paragraph = vs.SplitParagraphByEmptyLines();
            Console.ReadLine();
        }

        private static void TestCOre ()
        {
            var PoDictionary = PoFileParser.Factory.Creat(PoFilePath);

            string Path = @"D:\桌面\新建文本文档.txt";
            string test = @"D:\桌面\2.txt";

            var lines = File.ReadAllLines(test);
            List<string> vs = new List<string>(lines);
            var paragraph = vs.SplitParagraphByEmptyLines();

            foreach (var para in paragraph)
            {
                //para.Lines.ShowList();
                Console.WriteLine();
                //para.Abtract.ShowList();
                //if (para.IsOrderList())
                //{
                //    Console.WriteLine("这是一段列表");
                //}

                //Console.ReadLine();
            }
        }
    }
}