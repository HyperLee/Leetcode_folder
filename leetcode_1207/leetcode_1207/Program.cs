using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1207
{
    internal class Program
    {
        /// <summary>
        /// leetcode 1207
        /// https://leetcode.com/problems/unique-number-of-occurrences/
        /// 
        /// 给你一个整数数组 arr，请你帮忙统计数组中每个数的出现次数。
        /// 如果每个数的出现次数都是独一无二的，就返回 true；否则返回 false。
        /// 
        /// 每個文字出現的次數 不要重覆
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 1, 2, 2, 1, 1, 3 };
            Console.WriteLine(UniqueOccurrences(input));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/unique-number-of-occurrences/solution/xue-xi-c-by-shi-jian-de-feng-xi-zhong/
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static bool UniqueOccurrences(int[] arr)
        {
            // 計算每個文字個數
            Dictionary<int, int> dict = new Dictionary<int, int>();
            foreach (var i in arr)
            {
                if (!dict.ContainsKey(i))
                {
                    dict.Add(i, 0);
                }
                dict[i]++;
            }

            // 統計有沒有重覆
            var hashSet = new HashSet<int>();
            foreach (var pair in dict)
            {
                if (hashSet.Contains(pair.Value))
                {
                    return false;
                }
                hashSet.Add(pair.Value);
            }
            return true;
        }

        public static bool UniqueOccurrences2(int[] arr)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            foreach (var i in arr)
            {
                if (!dict.ContainsKey(i))
                {
                    dict.Add(i, 0);
                }
                dict[i]++;
            }
            var hashSet = new HashSet<int>();
            foreach (var pair in dict)
            {
                if(hashSet.Contains(pair.Value))
                {
                    return false;
                }
                hashSet.Add(pair.Value);
            }
            return true;
        }

    }
}
