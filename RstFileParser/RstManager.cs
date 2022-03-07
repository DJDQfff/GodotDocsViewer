using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

using MyStandard20Library;

using PoFileParser;

using RstFileParser;

using static RstFileParser.RstParagraphType;

namespace RstFileParser
{
    /// <summary> 管理一个rst文件里而所有rst段落 </summary>
    public class RstManager
    {
        private List<Paragraph> paragraphs;

        /// <summary>
        /// TODO
        /// 这里所有的段落类型都是粗略的，并不精准，只限ParseEachParagraph方法内用，以后可以精准化
        /// </summary>
        private List<RstParagraph> rstParagraphs = new List<RstParagraph>();

        private List<RstLine> rstLines = new List<RstLine>();

        /// <summary> 初始化，设置段落数据源 </summary>
        /// <param name="_paragraphs"> </param>
        public RstManager (List<Paragraph> _paragraphs)
        {
            paragraphs = _paragraphs;
            JudgeEachParagraph();
            ParseEachRstParagraph();
        }

        /// <summary> 转换段落数据，这个方法貌似没用上 </summary>
        private void JudgeEachParagraph ()
        {
            foreach (var paragraph in paragraphs)
            {
                RstParagraph rstParagraph = RstParagraphTypeJudger.JudgeType(paragraph);        //  粗略获取，所有段落类型，
                rstParagraphs.Add(rstParagraph);
            }
        }

        /// <summary> 解析每个段落里的数据 </summary>
        private void ParseEachRstParagraph ()
        {
            for (int offset = 0; offset < rstParagraphs.Count; offset++)           // 段落偏移，依次操作段落
            {
                var paragraph = rstParagraphs[offset];                          // 当前段落
                RstParagraph rstParagraph = RstParagraphTypeJudger.JudgeType(paragraph);    // 获取段落类型

                if (rstParagraph.rstParagraphType == Command)                        // 该段落是指令段落
                {
                    //TODO 以后再优化，这里就先把他按原样输出
                    var literalParas = rstParagraphs.UntilSameIndent(offset);              // 获取LiteralBlock影响的段落
                    var lines = RstLineFactory.Origin(literalParas);    // 原样返回LiterBlock
                    rstLines.AddRange(lines);

                    offset += (literalParas.Count - 1);                                           // 偏移移动到当前位置，减一是因为literalParas包含当前段落
                }
                else if (rstParagraph.rstParagraphType == LiteralBlock)                 // 该段落是字面段落
                {
                    var literalParas = rstParagraphs.UntilSameIndent(offset);              // 获取LiteralBlock影响的段落
                    var lines = RstLineFactory.Origin(literalParas);    // 原样返回LiterBlock
                    rstLines.AddRange(lines);

                    offset += (literalParas.Count - 1);                                           // 偏移移动到当前位置，减一是因为literalParas包含当前段落
                }
                else                                                          // 以上两种都不是，则该段落是是一般段落（自身段落内容不会影响别的段落）
                {
                    var rstlines = rstParagraph.ParseCommon();
                    rstLines.AddRange(rstlines);
                }
                var newrstLine = RstLineFactory.CreatNewLine();
                rstLines.Add(newrstLine);
            }
        }

        public List<string> Get (PoDictionary poDictionary)
        {
            return rstLines.ConvertToTranslatedAllLines(poDictionary);
        }
    }
}