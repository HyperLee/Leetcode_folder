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
            Console.ReadLine();
        }


        /// <summary>
        /// 最直覺的排序方法
        /// h指數: h篇, 被引用 h次數
        /// 都要達到h
        /// 排序後 大至小開始找
        /// https://leetcode.cn/problems/h-index/solutions/869042/h-zhi-shu-by-leetcode-solution-fnhl/?envType=daily-question&envId=Invalid+Date
        /// 
        /// </summary>
        /// <param name="citations"></param>
        /// <returns></returns>
        public static int HIndex(int[] citations)
        {
            Array.Sort(citations);
            int h = 0, i = citations.Length - 1;

            // h從0 開始往上找, 找到就更新. 引用次大至小開始
            while (i >= 0 && citations[i] > h) 
            {
                h++;
                i--;
            }

            return h;
        }

    }
}
