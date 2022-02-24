using System;
using System.IO;

using MyStandard20Library;

using PoFileParser;

using RstFileParser;

namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main (string[] args)

        {
            string str = @"D:\桌面\新建文本文档.txt";
            var lines = File.ReadAllLines(str);
            var paragraph = lines.SplitParagraphByEmptyLines(false);

            var a = paragraph[0].Lines.RstFileSpecialConnectString();
            a.Show();
            Console.ReadKey();
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