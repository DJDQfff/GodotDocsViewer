﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

using iText.Kernel.Geom;
using iText.Layout.Element;

using MyStandard20Library;
using MyAlgorithm.Compare.EnglishSentence;

namespace PoFileParser
{
    public class PoDictionary
    {
        private Dictionary<string, string> keyValuePairs;

        public PoDictionary (Dictionary<string, string> pairs)
        {
            keyValuePairs = pairs;
        }

        /// <summary>
        /// 输入Msgid，查找对应的Msgtr。 如果Msgid存在，则直接返回对应的Msgtr；
        /// 如果不存在，则进行模糊匹配，按范围内的误差进行查找。 如果模糊匹配也不存在，则返回null
        /// </summary>
        /// <param name="key"> Msgid不能为null或空字符串 </param>
        /// <returns> 翻译后内容，为null则说明不存在 </returns>
        public string this[string key]
        {
            get
            {
                if (!string.IsNullOrEmpty(key))
                {
                    string value;

                    if (keyValuePairs.TryGetValue(key, out value))
                    {
                        return value;
                    }
                    else
                    {
                        string dickey = key.SelectByWordsFrequency(keyValuePairs.Keys, 10, 0.6d);// 模糊匹配找到钥匙

                        if (dickey is null)
                        {
                            return null;
                        }

                        value = keyValuePairs[dickey];
                        return value;
                        //return key.SelectByWordsSequence(keyValuePairs.Keys, CompareSense.ByLengthAuto(key, 10), CompareSense.Creat(0, 0, 0, 0));

                        //return key.PoolSelect(keyValuePairs.Keys, CompareSense.Creat(3, 2, 4, 4), CompareSense.Creat(0, 0, 0, 0));
                    }
                }
                else
                    return null;
            }
        }
    }
}