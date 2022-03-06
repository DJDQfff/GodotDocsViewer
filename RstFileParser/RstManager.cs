using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

using MyStandard20Library;
using RstFileParser;
using static RstFileParser.RstParagraphType;
using PoFileParser;

namespace RstFileParser
{
    /// <summary>
    /// 管理一个rst文件里而所有rst段落
    /// </summary>
    public class RstManager
    {
        private List<Paragraph> paragraphs;
        private List<RstParagraph> rstParagraphs = new List<RstParagraph>();
        private List<RstLine> rstLines = new List<RstLine>();
        public List<string> Values { set; get; }

        /// <summary>
        /// 传入段落集合
        /// </summary>
        /// <param name="_paragraphs"></param>
        public RstManager (List<Paragraph> _paragraphs)
        {
            paragraphs = _paragraphs;
        }

        public void Start ()
        {
            JudgeRstParagraphType();
            ParseRstParagraph();
        }

        private void JudgeRstParagraphType ()
        {
            for (int j = 0; j < paragraphs.Count; j++)
            {
                var paragraph = paragraphs[j];
                RstParagraph rstParagraph = RstParagraphTypeJudger.JudgeType(paragraph);
                rstParagraphs.Add(rstParagraph);
                if (rstParagraph.rstParagraphType == Command)
                {
                    // TODO 段落需要特殊处理
                    j++;
                    var paragraphNext = paragraphs[j];
                    RstParagraph rstParagraphNext = new RstParagraph(paragraphNext)
                    {
                    };
                }
            }
        }

        private void ParseRstParagraph ()
        {
            foreach (var rstparagraph in rstParagraphs)
            {
                var rstlines = rstparagraph.Parse();
                rstlines.AddRange(rstlines);
            }
        }

        public void Convert (PoDictionary poDictionary)
        {
            Values = rstLines.ConvertToTranslatedAllLines(poDictionary);
        }
    }
}