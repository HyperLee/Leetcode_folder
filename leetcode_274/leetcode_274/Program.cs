using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_274
{
    internal class Program
    {
        /// <summary>
        /// 274. H-Index
        /// https://leetcode.com/problems/h-index/
        /// 274. H 指数
        /// https://leetcode.cn/problems/h-index/?envType=daily-question&envId=Invalid%20Date
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 3, 0, 6, 1, 5 };
            Console.WriteLine(HIndex(input));
            Console.ReadKey();
        }


        /// <summary>
        /// 排序方法
        /// H 指數: h篇, 被引用 h 次數
        /// 都要達到h
        /// https://leetcode.cn/problems/h-index/solutions/869042/h-zhi-shu-by-leetcode-solution-fnhl/?envType=daily-question&envId=Invalid+Date
        /// 
        /// H 指數
        /// https://zh.wikipedia.org/zh-tw/H%E6%8C%87%E6%95%B0
        /// 根據 wiki 說明 算法 如下
        /// 1: 將其發表的所有SCI論文按被引次數從高到低排序
        /// 2: 從前往後查找排序後的列表，只要當前的引用量大於當前的索引值，則 H 指數加 1 ，最後得到的結果即為最終的 H 指數。
        /// </summary>
        /// <param name="citations"></param>
        /// <returns></returns>
        public static int HIndex(int[] citations)
        {
            Array.Sort(citations);
            int h = 0;
            // 從引用次數大的開始跑
            int i = citations.Length - 1;

            // h 從 0 開始往上找, 找到就更新 + 1. 引用次數大至小開始
            while (i >= 0 && citations[i] > h) 
            {
                // 從前往後查找排序後的列表，只要當前的引用量大於當前的索引值，則 H 指數加 1 
                h++;
                // 找下一篇
                i--;
            }

            return h;
        }

    }
}
