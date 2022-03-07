using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

using MyStandard20Library;

namespace RstFileParser
{
    /// <summary>
    /// 判断Command指令具体是哪一种指令, godot中不包含的有： line-block、
    /// parsed-literal
    /// </summary>
    public static class RstCommandJudger
    {
        /// <summary> .. _doc_kdfj:这种，他的结尾只有一个冒号 </summary>
        /// <param name="paragraph"> </param>
        /// <returns> </returns>
        public static bool IsCommand_Single (IParagraph paragraph)
        {
            return true;
        }

        /// <summary>
        /// .. This is a comment. 评论结尾没有冒号
        /// </summary>
        /// <param name="paragraph"> </param>
        /// <returns> </returns>
        public static bool IsCommand_comment (IParagraph paragraph)
        {
            //TODO
            return true;
        }

        /// <summary> toctree，用于划分目录，他的后续段落都要按原样输出 </summary>
        /// <param name="paragraph"> </param>
        /// <returns> </returns>
        public static bool IsCommand_toctree (IParagraph paragraph)
        {
            //TODO
            return true;
        }

        /// <summary>
        /// note、warning、tip、important、danger、attention等
        /// 这些指令后的内容按索引及段落作为一句话、他自身第一段也可能包含文字内容
        /// </summary>
        /// <param name="paragraph"> </param>
        /// <returns> </returns>
        public static bool IsCommand_note (IParagraph paragraph)
        {
            //TODO
            return true;
        }

        /// <summary> .. image:: 形式的指令，其后一律原样返回 </summary>
        /// <param name="paragraph"> </param>
        /// <returns> </returns>
        public static bool IsCommand_image (IParagraph paragraph)
        {
            return true;
        }

        /// <summary> 其后一律原样返回 </summary>
        /// <param name="paragraph"> </param>
        /// <returns> </returns>
        public static bool IsCommand_codeblock (IParagraph paragraph)
        {
            return true;
            //TODO
        }

        /// <summary> .. tabs:: 他的后面一律原样返回 </summary>
        /// <param name="paragraph"> </param>
        /// <returns> </returns>
        public static bool IsCommand_tabs (IParagraph paragraph)
        {
            return true;
        }
    }
}